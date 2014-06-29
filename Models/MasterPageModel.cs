using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNancy.Models
{
    public class MasterPageModel
    {
        public string Title { get; set; }
        public string LastVisit { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
