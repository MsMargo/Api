using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestApi.Services;
using System.Web.Mvc;

namespace RestApi.Controllers
{
    public class UserController : Controller
    {
        private IService _service { get; set; }
        JsonSerializerSettings jsonSettings { get; set; }
        public UserController(IService service)
        {
            _service = service;
            jsonSettings = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }

        public ActionResult List()
        {
            var userList = _service.GetUsersList();
            return View(userList);
        }

        public ActionResult Details(int userId)
        {
            return View(userId);
        }


        public string GetPostsByUserId(int userId)
        {
            var model = _service.GetUserPosts(userId);

            return JsonConvert.SerializeObject(model, jsonSettings);
        }


        public string GetPostDetailsById(int postId)
        {
            var postDetails = _service.GetDetailsPostById(postId);

            return JsonConvert.SerializeObject(postDetails, jsonSettings);
        }
    }
}