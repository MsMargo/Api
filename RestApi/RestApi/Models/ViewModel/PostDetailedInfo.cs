using System.Collections.Generic;

namespace RestApi.Models.ViewModel
{
    public class PostDetailedInfo
    {
        public List<CommentView> Comments { get; set; }

        public List<StatisticView> Statistics { get; set; }
    }
}