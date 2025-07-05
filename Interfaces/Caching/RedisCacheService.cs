using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Weather_API_Wrapper_Service.Interfaces.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T? GetDate<T>(string key)
        {
            var data = _cache.GetString(key);
            if (string.IsNullOrEmpty(data))
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(data);
        }

        public void SetData<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // Set cache expiration time
            };
            var jsonData = JsonSerializer.Serialize(data);
            _cache.SetString(key, jsonData, options);
        }
    }
}
