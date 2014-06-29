using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Extensions;
using Nancy.Authentication.Forms;
using Nancy.Security;
using MyNancy.Models;

namespace MyNancy.Modules
{
    public class AdminModule : BaseModule
    {
        public AdminModule() : base("/admin")
        {
            this.RequiresAuthentication();
            Get["/"] = x =>
            {
                Model.Index = new MyNancy.Models.admin.indexModel();
                Model.Index.UserName = Context.CurrentUser.UserName;
                return View["admin/index", Model];
            };
        }
    }
}
