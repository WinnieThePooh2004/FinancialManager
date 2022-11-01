namespace Frontend.HttpService
{
    public interface IHttpService
    {
        Task DeleteObject(string uri, int id);
        Task<T> GetObjectAsync<T>(string uri) where T : class;
        Task<T> GetObjectBuyIdAsync<T>(string uri, int id) where T : class;
        Task PutObject<T>(string uri, T? @object) where T : class;
        Task PostObject<T>(string uri, T? @object) where T : class;
    }
}
