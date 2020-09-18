using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITest.BO
{
    public class ImageData
    {
        public string id { get; set; }
        public string owner { get; set; }
        public string secret { get; set; }
        public string server { get; set; }
        public int farm { get; set; }
        public string title { get; set; }
        public bool ispublic { get; set; }
        public bool isfriend { get; set; }
        public bool isfamily { get; set; }
    }
}
