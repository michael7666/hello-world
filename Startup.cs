using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronApp
{
    public class Startup
    {
        private string option;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public object BrowserWindowoption { get; private set; }
        public bool Show { get; private set; }
        public object BrowserWindowoptions { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            Boostrap();
        }
        public async void Boostrap()
        {
            var options =  new BrowserWindowoptions
              {
                Show = false

              };
         var mainWindow = await Electron.WindowManager.CreateWindowAsync(option);
            mainWindow.OnReadyToShow += () =>
            {
                mainWindow.Show();
            };
            var menu = new MenuItem[]
            {
                new MenuItem
                {
                    Label = "file",
                    Submenu = new MenuItem[]
                    {
                        new MenuItem
                        {
                            Label ="Exit",
                            Click = () => {Electron.App.Exit(); }
                        }
                    }

                },
                new MenuItem
                {
                    Label = "infor",
                    Click = async() =>
                    {
                       await Electron.Dialog.ShowMessageBoxAsync("i love electron. net");
                    }
                }
            };
            Electron.Menu.SetApplicationMenu(menu);
        }
    }
}
