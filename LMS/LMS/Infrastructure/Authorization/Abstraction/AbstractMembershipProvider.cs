using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.User.Interfaces;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace LMS.Infrastructure.Authorization.Abstraction
{
    public abstract class AbstractMembershipProvider : MembershipProvider
    {
        private IUserRepository _userRepository;

        private string applicationName = "LMS";

        public override string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }

        protected IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = DependencyResolver.Current.GetService<IUserRepository>();
                    if (_userRepository == null)
                        throw new InvalidOperationException("You need to assign a locator!");

                }
                return _userRepository;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipUser membershipUser = null;
            UserData user = UserRepository.GetUserByUsername(username);

            if (user != null)
            {
                membershipUser = new MembershipUser("LMSMembershipProvider", user.Username, user.Password, user.Email,
                   null, null, user.IsActive, false, user.DateTimeCreatedOn, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
            }

            return membershipUser;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            bool valid = false;
            UserData user = UserRepository.GetUserByUsername(username);

            if (user != null)
            {
                //Heshing mechanism possible instead
                if (password.Equals(user.Password))
                {
                    valid = true;
                }
            }

            return valid;
        }
    }
}