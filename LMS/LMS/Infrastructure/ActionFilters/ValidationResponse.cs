namespace LMS.Infrastructure.ActionFilters
{
    public class ValidationResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string RedirectionUrl { get; set; }
    }
}