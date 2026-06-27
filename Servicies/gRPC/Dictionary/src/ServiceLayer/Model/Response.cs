namespace ServiceLayer.Model
{
    public class Response<T>(string message, string errors, bool status, T? result) : IResponse<T>
    {
        public string Errors => errors;
        public bool Status => status;
        public string Message => message;
        public T? Result => result;
    }
}
