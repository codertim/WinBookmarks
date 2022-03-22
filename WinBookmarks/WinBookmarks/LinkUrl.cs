using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBookmarks
{
    public class LinkUrl
    {
        public string Description { get; set; }
        public string Url { get; set; }
        public bool isNew { get; set; }

        public LinkUrl(string description, string url, bool isNew)
        {
            this.Description = description;
            this.Url = url;
            this.isNew = isNew;
        }
    }
}
