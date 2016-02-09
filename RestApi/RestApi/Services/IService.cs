using RestApi.Models.DataModel;
using RestApi.Models.ViewModel;
using System.Collections.Generic;

namespace RestApi.Services
{
    public interface IService
    {
        List<UserView> GetUsersList();
        List<PostView> GetUserPosts(int userId);
        Post GetPostById(int postId);
        PostDetailedInfo GetDetailsPostById(int postId);
        List<StatisticView> GetStatisticForPost(List<string> words);
        List<CommentView> GetComments(int postId);
    }
}
