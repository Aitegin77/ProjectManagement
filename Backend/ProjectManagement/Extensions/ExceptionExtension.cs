namespace AppProjectManagement.Extensions
{
    public static class ExceptionExtension
    {
        public static string JoinInnerExceptions(this Exception ex)
        {
            var message = ex.Message;
            if (ex.InnerException != null)
                message = $"{message}|#-#|{ex.InnerException!.JoinInnerExceptions()}";

            return message;
        }
    }
}
