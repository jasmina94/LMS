using System.Collections.Generic;

namespace LMS.Models.ViewModels.Permission
{
    public class AdministrationUserPermissionViewModel
    {
        public List<PermissionViewModel> Assigned { get; set; }

        public List<PermissionViewModel> Available { get; set; }
    }
}