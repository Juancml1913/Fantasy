namespace Fantasy.Fronted.Repositories;

public interface IRepository
{
    Task<HttpResponseWrapper<T>> GetAsync<T>(string url);

    Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model);

    Task<HttpResponseWrapper<IActionResponse>> PostAsync<T, IActionResponse>(string url, T model);
}