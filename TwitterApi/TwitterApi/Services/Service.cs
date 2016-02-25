using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using TwitterApi.Models;
using TwitterApi.Models.ViewModel;

namespace TwitterApi.Services
{
    public class Service : IService
    {
        private const string consumerKey = "qLT42Pa4Pm7FxY4v7fqtBw";
        private const string consumerSecret = "s49oOJabVbS305j5yMVWcHOp3YX9XExl8pUHEv9g";
        private const string accessToken = "39523825-pCBfpmVcbyopUEXtwdOmMERq7VMtPk937YKO911tj";
        private const string accessTokenSecret = "E2EpHYquZTRJ3NYLK9JYeGN0jGD5P8jH9bQHtdFb7JI";
        private const int tweetCount = 10;
        private const int feedCount = 5;

        SingleUserAuthorizer authorizer = new SingleUserAuthorizer
        {
            CredentialStore = new InMemoryCredentialStore
            {
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                OAuthToken = accessToken,
                OAuthTokenSecret = accessTokenSecret
            }
        };
        TwitterContext twitterContext;
        public Service()
        {
            twitterContext = new TwitterContext(authorizer);
        }

        public List<Tweet> GetTweetsByQuery(string searchQuery, ulong? maxId, DirectionEnum? direction)
        {
            var query =
               (from search in twitterContext.Search
                where search.Type == SearchType.Search &&
                    search.Query == searchQuery &&
                      search.Count == tweetCount
                select search);

            if (maxId.HasValue)
            {
                query = direction == DirectionEnum.Next ?
                    (from q in query where q.MaxID == maxId - 1 select q) :
                    (from q in query where q.MaxID == maxId select q);
            }

            var tweets = query.SingleOrDefault();
            var resultTweets = new List<Tweet>();

            if (tweets != null && tweets.Statuses != null)
            {
                resultTweets = tweets.Statuses.Select(tweet => new Tweet()
                {
                    UserName = tweet.User.ScreenNameResponse,
                    TweetText = tweet.Text,
                    TweetDate = tweet.CreatedAt.Date,
                    StatusId = tweet.StatusID
                }).ToList();
            }
            GetTrimmedLengthTweet(ref resultTweets);
            return resultTweets;
        }

        public List<Tweet> GetFeedByUser(string screenName)
        {
            var statusTweets = (from tweet in twitterContext.Status
                                where tweet.Type == StatusType.User
                                      && tweet.ScreenName == screenName
                                      && tweet.Count == feedCount
                                select tweet).ToList();

            var possibleTweets = new List<Tweet>();
            if (statusTweets != null)
            {
                possibleTweets = statusTweets.Select(tweet => new Tweet()
                {
                    UserName = tweet.User.ScreenNameResponse,
                    TweetText = tweet.Text,
                    TweetDate = tweet.CreatedAt.Date,
                    StatusId = tweet.StatusID
                }).ToList();
            }
            return possibleTweets;
        }

        private void GetTrimmedLengthTweet(ref List<Tweet> resultTweets)
        {
            foreach (var tweet in resultTweets)
            {
                if (tweet.TweetText.Length > 140)
                {
                    tweet.TweetText = String.Format("{0}...", tweet.TweetText.Remove(137));
                }
            }
        }
    }

}