using System;

namespace TwitterApi.Models.ViewModel
{
    public class Tweet
    {
        public string UserName { get; set; }
        public string TweetText { get; set; }
        public DateTime TweetDate { get; set; }
        public ulong StatusId { get; set; }
    }
}