using AutoMapper;
using RestApi.Models.DataModel;
using RestApi.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RestApi.Services
{
    public class Service : IService
    {
        private IApiRetriverService _apiService { get; set; }

        public Service(IApiRetriverService apiService)
        {
            _apiService = apiService;
        }

        public List<UserView> GetUsersList()
        {
            var url = "http://jsonplaceholder.typicode.com/users";
            var userList = _apiService.GetDataFromApi<List<User>>(url);

            var userViewList = userList.Select(user => Mapper.Map<User, UserView>(user)).ToList();

            GetPhoneNumberByMask(ref userViewList);

            return userViewList;
        }

        public List<PostView> GetUserPosts(int userId)
        {
            var url = String.Format("http://jsonplaceholder.typicode.com/posts?userId={0}", userId);
            var postList = _apiService.GetDataFromApi<List<Post>>(url);

            var postViewList = postList.Select(post => Mapper.Map<Post, PostView>(post)).ToList();


            return postViewList;
        }

        public Post GetPostById(int postId)
        {
            var url = String.Format("http://jsonplaceholder.typicode.com/posts/{0}", postId);
            var post = _apiService.GetDataFromApi<Post>(url);

            return post;
        }

        public PostDetailedInfo GetDetailsPostById(int postId)
        {
            var postDetails = new PostDetailedInfo();
            postDetails.Comments = GetComments(postId).ToList();
            var currentPostText = GetPostById(postId);

            var listWordsForStatisctics = new List<string>();
            var stringSeparators = new string[] { "\n", " " };
            listWordsForStatisctics.AddRange(currentPostText.Body.Split(stringSeparators, StringSplitOptions.None));
            foreach (var comment in postDetails.Comments)
            {
                listWordsForStatisctics.AddRange(comment.Body.Split(stringSeparators, StringSplitOptions.None));
            }

            postDetails.Statistics = GetStatisticForPost(listWordsForStatisctics);

            return postDetails;
        }

        public List<StatisticView> GetStatisticForPost(List<string> words)
        {
            var wordCounts = new Dictionary<string, int>();

            foreach (string value in words)
            {
                if (wordCounts.ContainsKey(value))
                {
                    wordCounts[value] = wordCounts[value] + 1;
                }
                else
                {
                    wordCounts.Add(value, 1);
                }
            }

            var result = wordCounts.Select(x => new StatisticView()
            {
                Count = x.Value,
                Word = x.Key
            }).OrderByDescending(x => x.Count).Take(10).ToList();

            return result;

        }
        public List<CommentView> GetComments(int postId)
        {
            var url = String.Format("http://jsonplaceholder.typicode.com/comments?postId={0}", postId);
            var commentList = _apiService.GetDataFromApi<List<Comment>>(url);

            var commentsViewList = commentList.Select(post => Mapper.Map<Comment, CommentView>(post)).ToList();

            return commentsViewList;
        }

        private void GetPhoneNumberByMask(ref List<UserView> userViewList)
        {
            foreach (var user in userViewList)
            {
                var numberWithExtension = Regex.Replace(user.Phone, @"[^0-9a-zA-Z]+", "");
                var indexStartExtension = numberWithExtension.IndexOf("x");
                if (indexStartExtension != -1)
                {
                    user.Phone = numberWithExtension.Substring(0, indexStartExtension);
                    user.ExtensionNumber = numberWithExtension.Substring(indexStartExtension + 1);
                }
                else
                {
                    user.Phone = numberWithExtension;
                }

                user.Phone = FormatTelephoneNmber(user.Phone);
            }
        }

        private string FormatTelephoneNmber(string phoneNumber)
        {
            if (String.IsNullOrEmpty(phoneNumber))
            {
                return phoneNumber;
            }

            Regex phoneParser = null;
            string format = String.Empty;

            switch (phoneNumber.Length)
            {

                case 5:
                    phoneParser = new Regex(@"(\d{3})(\d{2})");
                    format = "$1 $2";
                    break;

                case 6:
                    phoneParser = new Regex(@"(\d{2})(\d{2})(\d{2})");
                    format = "$1 $2 $3";
                    break;

                case 7:
                    phoneParser = new Regex(@"(\d{3})(\d{2})(\d{2})");
                    format = "$1 $2 $3";
                    break;

                case 8:
                    phoneParser = new Regex(@"(\d{4})(\d{2})(\d{2})");
                    format = "$1 $2 $3";
                    break;

                case 9:
                    phoneParser = new Regex(@"(\d{4})(\d{3})(\d{2})(\d{2})");
                    format = "$1 $2 $3 $4";
                    break;

                case 10:
                    phoneParser = new Regex(@"(\d{3})(\d{3})(\d{4})");
                    format = "$1 $2 $3";
                    break;

                case 11:
                    phoneParser = new Regex(@"(\d{1})(\d{3})(\d{3})(\d{4})");
                    format = "+($1) $2 $3 $4";
                    break;

                case 12:
                    phoneParser = new Regex(@"(\d{2})(\d{3})(\d{3})(\d{2})(\d{2})");
                    format = "+($1) $2 $3 $4 $5";
                    break;

                default:
                    return phoneNumber;
            }

            return phoneParser.Replace(phoneNumber, format);
        }
    }
}