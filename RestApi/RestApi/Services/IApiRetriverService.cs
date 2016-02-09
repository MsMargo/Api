
namespace RestApi.Services
{
    public interface IApiRetriverService
    {
        T GetDataFromApi<T>(string urlApi) where T : class;
    }
}
