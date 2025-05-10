using AutenticacaoEAutorizacaoCorreto.Services.IService;
using Microsoft.Extensions.Caching.Memory;

namespace AutenticacaoEAutorizacaoCorreto.Services
{
    public class MemCacheService (IMemoryCache Cache) : ICacheService
    {
        private readonly IMemoryCache _memoryCache = Cache;
        public object get(string key) => _memoryCache.TryGetValue(key, out var cachedValue) ? cachedValue : null;

        public void set(string key, object content) => _memoryCache.Set(key, content, TimeSpan.FromMinutes(10));
    }
}
