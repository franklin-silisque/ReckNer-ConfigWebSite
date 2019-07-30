using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReckNer_ConfigWebSite
{
    class BEPostData
    {
        public string type { get; set; }
        public BEProperties properties { get; set; }
     
    }
    class BEProperties {
        public string webUrl { get; set; }
        public string webDescription { get; set; }
        public string creatorName { get; set; }
        public string creatorEmail { get; set; }
        public string createdTimeUTC { get; set; }
    }
}
