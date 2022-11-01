namespace Frontend.HttpService
{
    public interface IHttpService
    {
        public string FinancialOperationUri { get; }
        public string OperationTypesUri { get; }
        public string ReportUri { get; }

        Task DeleteObject(string uri, int id);
        Task<T> GetObjectAsync<T>(string uri) where T : class;
        Task<T> GetObjectByIdAsync<T>(string uri, int id) where T : class;
        Task PutObject<T>(string uri, T? @object) where T : class;
        Task PostObject<T>(string uri, T? @object) where T : class;
    }
}
