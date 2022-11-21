namespace MyMessageApp.Core.CacheServices
{
    public interface ICacheService
    {
        public T Set<T>(object key, T value, int expirationInMinutes = 60);
        public bool Contains(object key);
        public T Get<T>(object key);
        public void Clear(object key);
        public void Reset();

        //Task<string> GetValueAsync(string key);
        //Task<bool> SetValueAsync(string key, string value);
        //Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class;
        //T GetOrAdd<T>(string key, Func<T> action) where T : class;
        //Task Clear(string key);
    }
}
