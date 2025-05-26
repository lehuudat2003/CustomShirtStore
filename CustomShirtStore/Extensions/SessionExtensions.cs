using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CustomShirtStore.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var jsonData = JsonConvert.SerializeObject(value);
            session.SetString(key, jsonData);
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var jsonData = session.GetString(key);
            return jsonData == null ? default : JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}
