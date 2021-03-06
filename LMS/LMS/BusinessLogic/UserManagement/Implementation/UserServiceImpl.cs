﻿using LMS.Infrastructure.ModelBuilders.Implementation.User;
using LMS.Infrastructure.ModelConstructor.Interfaces;
using LMS.DomainModel.Repository.Relation.Interfaces;
using LMS.BusinessLogic.UserManagement.Interfaces;
using LMS.BusinessLogic.RoleManagement.Interfaces;
using LMS.DomainModel.Repository.User.Interfaces;
using LMS.BusinessLogic.UserManagement.Model;
using LMS.DomainModel.DomainObject.Relation;
using LMS.Infrastructure.Authorization;
using LMS.DomainModel.DomainObject;
using System.Collections.Generic;
using LMS.Models.ViewModels.User;
using LMS.Services.Interfaces;
using System;
using System.Configuration;
using System.Security.Cryptography;

namespace LMS.BusinessLogic.LanguageManagement.Implementations
{
    public class UserServiceImpl : IUserService
    {
        #region Injection
        public IUserRepository UserRepository { get; set; }

        public IRelationUserBookCopyRepository RelationUserBookCopyRepository { get; set; }

        public IRoleService RoleService { get; set; }

        public IModelConstructor Constructor { get; set; }

        public IBuilderResolverService BuilderResolverService { get; set; }
        #endregion

        public UserViewModel Get(int? userId)
        {
            var viewModel = new UserViewModel();
            if (userId.HasValue)
            {
                UserData domainModel = UserRepository.GetDataById(userId.Value);
                UserViewModelBuilder builder = BuilderResolverService.Get<UserViewModelBuilder, UserData>(domainModel);
                Constructor.ConstructViewModelData(builder);
                viewModel = builder.GetViewModel();
            }

            return viewModel;
        }

        public List<UserViewModel> GetAll(bool active)
        {
            var viewModels = new List<UserViewModel>();
            var domainModels = new List<UserData>();

            domainModels = active ? UserRepository.GetAllActiveData() : UserRepository.GetAllData();
            viewModels = ConvertDataToViewModels(domainModels);

            return viewModels;
        }

        public SaveUserResult Save(UserViewModel viewModel, UserSessionObject currentUser)
        {
            var result = new SaveUserResult();

            if (viewModel.IsNew)
            {
                viewModel.UserPassword = ConfigurationManager.AppSettings["DefaultPassword"];
            }

            UserDomainModelBuilder builder = BuilderResolverService.Get<UserDomainModelBuilder, UserViewModel>(viewModel);
            Constructor.ConstructDomainModelData(builder);
            UserData domainModel = builder.GetDataModel();

            if (viewModel.Id == 0)
                domainModel.RefUserCreatedBy = currentUser.UserId;

            int id = UserRepository.SaveData(domainModel);
            if (id != 0)
            {
                result = new SaveUserResult(id, domainModel.FullFirstAndLastName);
            }

            return result;
        }

        public DeleteUserResult Delete(int? userId, UserSessionObject currentUser)
        {
            var result = new DeleteUserResult();
            if (userId.HasValue)
            {
                UserData domainModel = UserRepository.GetDataById(userId.Value);
                if (domainModel != null)
                {
                    List<RelationUserBookCopyData> loans = RelationUserBookCopyRepository.GetLoansForUser(userId.Value);
                    if(loans.Count == 0)
                    {
                        UserRepository.DeleteById(userId.Value, currentUser.UserId);
                        result = new DeleteUserResult(userId.Value, domainModel.FullFirstAndLastName);
                    }
                    else
                    {
                        result.Message = "Can't delete this user. There are some not returned loans.";
                    }                    
                }
            }

            return result;
        }

        public Tuple<bool, int> CheckUniqueUsername(string username)
        {
            Tuple<bool, int> result;
            UserData user = UserRepository.GetUserByUsername(username);
            if (user != null)
            {
                result = new Tuple<bool, int>(false, user.Id);
            }
            else
            {
                result = new Tuple<bool, int>(true, 0);
            }                

            return result;
        }

        public Tuple<bool, int> CheckUniqueEmail(string email)
        {
            Tuple<bool, int> result;
            UserData user = UserRepository.GetUserByEmail(email);

            if (user != null)
            {
                result = new Tuple<bool, int>(false, user.Id);
            }
            else
            {
                result = new Tuple<bool, int>(true, 0);
            }
                

            return result;
        }

        public string HashPassword(string password)
        {
            string savedPasswordHash;

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }

        public bool IsPasswordValid(string given, string real)
        {
            bool valid = true;
            byte[] hashBytes = Convert.FromBase64String(real);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(given, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    valid = false;
                    break;
                }
            }

            return valid;
        }

        #region Private
        private List<UserViewModel> ConvertDataToViewModels(List<UserData> domainModels)
        {
            var viewModels = new List<UserViewModel>();

            foreach (var item in domainModels)
            {
                UserViewModelBuilder builder = BuilderResolverService.Get<UserViewModelBuilder, UserData>(item);
                Constructor.ConstructViewModelData(builder);
                viewModels.Add(builder.GetViewModel());
            }

            return viewModels;
        }
        #endregion
    }
}