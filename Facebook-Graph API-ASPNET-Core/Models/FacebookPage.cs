using System;
using System.Collections.Generic;

namespace Mastersi.Visualization.Models
{
    public class FacebookPage
    {
        public long Id { get; set; }
        public string About { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public long Fan_count { get; set; }
    }
    public class FacebookPost
    {
        public string Message { get; set; }
        public string Created_Time { get; set; }
        public string Id { get; set; }
    }
    public class FacebookPostsPaging
    {
        public string Previous { get; set; }
        public string Next { get; set; }
    }
    public class FacebookPostData
    {
        public List<FacebookPost> Data { get; set; }
        public FacebookPostsPaging Paging { get; set; }
    }

}