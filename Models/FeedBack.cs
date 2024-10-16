using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// phản hồi của khách hàng
namespace KOI.Core.Models
{
    public class FeedBack : Customer
    {
        public string Id { get; set; }  // id phản hồi
        public string FeedBackContent { get; set; }  // nội dung phản hồi
        public DateTime FeedBackTime { get; set; }  // thời gian phản hồi
        public string FeedBackTitle { get; set; }  // tiêu đề phản hồi
        public ReviewStar ReviewStar { get; set; }  // đánh giá sao

    }
    public enum ReviewStar
    {
        oneStar,
        twoStar,
        threeStar,
        fourStar,
        fiveStar
    }
}
