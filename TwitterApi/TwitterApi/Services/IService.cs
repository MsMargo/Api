using System.Collections.Generic;
using TwitterApi.Models;
using TwitterApi.Models.ViewModel;

namespace TwitterApi.Services
{
    public interface IService
    {
        List<Tweet> GetTweetsByQuery(string searchQuery, ulong? maxId, DirectionEnum? direction);
        List<Tweet> GetFeedByUser(string screenName);
    }
}
