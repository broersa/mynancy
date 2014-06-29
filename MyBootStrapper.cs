using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using TinyIoC;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using System.IO;

namespace MyNancy
{
    public class MyBootStrapper : DefaultNancyBootstrapper
    {
        byte[] favicon;

        protected override byte[] DefaultFavIcon
        {
            get
            {
                if (favicon == null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Resources.FavIcon.favicon.Save(ms);
                        favicon = ms.ToArray();
                    }
                }
                return favicon;
            }
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            container.Register<IUserMapper, MyUserMapper>();
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            var formsAuthConfiguration =
                new FormsAuthenticationConfiguration()
                {
                    RedirectUrl = "~/login",
                    UserMapper = container.Resolve<IUserMapper>()
                };

            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
        }
    }
}
