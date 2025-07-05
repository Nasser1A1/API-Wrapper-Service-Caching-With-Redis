namespace Weather_API_Wrapper_Service.Interfaces.Caching
{
    public interface ICacheService
    {
        T? GetDate<T>(string key);
        void SetData<T>(string key, T data);
    }
}
