using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;

namespace MyNancy.Models
{
    public class loginModel
    {
        public bool Error { get; set; }
        public Url ReturnUrl { get; set; }
    }
}
