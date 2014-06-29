using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Extensions;
using Nancy.Authentication.Forms;
using MyNancy.Models;

namespace MyNancy.Modules
{
    public class MainModule : BaseModule
    {
        public MainModule()
        {
            Get["/"] = x =>
            {
                Model.index = new indexModel();
                Model.index.HelloWorld = "HelloWorld!";
                return View["index", Model];
            };

            Get["/login"] = x =>
            {
                Model.login = new loginModel() { Error = this.Request.Query.error.HasValue, ReturnUrl = this.Request.Url };
                return View["login", Model];
            };

            Post["/login"] = x =>
            {
                var userGuid = MyUserMapper.ValidateUser((string)this.Request.Form.Username, (string)this.Request.Form.Password);

                if (userGuid == null)
                {
                    return Context.GetRedirect("~/login?error=true&username=" + (string)this.Request.Form.Username);
                }

                DateTime? expiry = null;
                if (this.Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays(7);
                }

                return this.LoginAndRedirect(userGuid.Value, expiry);
            };
            Post["/logout"] = x =>
            {
                return this.LogoutAndRedirect("/");
            };

        }
    }
}
