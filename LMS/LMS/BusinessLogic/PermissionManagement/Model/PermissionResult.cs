using System.Collections.Generic;
using System.Text;

namespace LMS.BusinessLogic.PermissionManagement.Model
{
    public class PermissionResult
    {
        public enum OperationType
        {
            Remove,
            Assign
        }

        public bool Success { get; set; }

        public string Message { get; set; }

        public List<int> Ids { get; set; }

        public PermissionResult(bool success, OperationType operation)
        {
            Success = success;
            Ids = new List<int>();
            Message = GenerateMessage(success, operation);
        }

        public PermissionResult(bool success, OperationType operation, List<int> ids)
        {
            Success = success;
            Ids = ids;
            Message = GenerateMessage(success, operation, ids);            
        }

        public PermissionResult()
        { 
        }

        private string GenerateMessage(bool success, OperationType operation, List<int> ids = null)
        {
            string result = success ? "Successfully" : "Unsuccessfully";
            string action = operation.Equals(OperationType.Assign) ? "assigned" : "removed";

            string idToString = ids != null ? GenerateIdString(ids) : string.Empty;

            var builder = new StringBuilder();
            builder
                .Append(result)
                .Append(action)
                .Append("permissions. ")
                .Append(idToString);

            return builder.ToString();
        }

        private string GenerateIdString(List<int> ids)
        {
            var builder = new StringBuilder();
            builder.Append("Permission ids: ");
            
            foreach(int id in ids)
            {
                builder.Append(id).Append(", ");
            }

            string result = builder.ToString();
            result = result.Substring(0, result.Length - 2);

            return result;
        }
    }
}