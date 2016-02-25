using System.Web.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using TwitterApi.Services;
using TwitterApi.Models;

namespace TwitterApi.Controllers
{
    public class TwitterController : Controller
    {
        JsonSerializerSettings jsonSettings { get; set; }
        private IService _service { get; set; }

        public TwitterController(IService service)
        {
            _service = service;
            jsonSettings = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }

        public ActionResult Index()
        {
            return View();
        }

        public string GetTweetsByQuery(string query, ulong? maxId, DirectionEnum? direction)
        {
            var model = _service.GetTweetsByQuery(query, maxId, direction);
            return JsonConvert.SerializeObject(model, jsonSettings);
        }

        public string GetTweetsByUser(string screenName)
        {
            var model = _service.GetFeedByUser(screenName);
            return JsonConvert.SerializeObject(model, jsonSettings);
        }
    }
}