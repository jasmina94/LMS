using System.Collections.Generic;

namespace LMS.BusinessLogic.PermissionManagement.Model
{
    public class PermissionWrapper
    {
        public List<int> Permissions { get; set; }

        public int User { get; set; }

        public bool Result { get; set; }
    }
}