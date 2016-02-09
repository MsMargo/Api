using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RestApi.Services
{
    public class ApiRetriverService : IApiRetriverService
    {
        public T GetDataFromApi<T>(string urlApi) where T : class
        {
            var syncClient = new WebClient();
            var content = syncClient.DownloadString(urlApi);
            T result;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
            {
                result = serializer.ReadObject(ms) as T;
            }

            return result;
        }
    }
}