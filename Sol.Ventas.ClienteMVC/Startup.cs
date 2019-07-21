using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sol.Ventas.Contexto;
using Sol.Ventas.Services;

namespace Sol.Ventas.ClienteMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FundamentosContext>(
                opt => {
                    opt.UseSqlServer(Configuration.GetConnectionString("CnnBD"));
                }
                );
            services.AddDbContext<SeguridadContext>(
                opt => {
                    opt.UseSqlServer(Configuration.GetConnectionString("CnnBD"));
                }
                );

            services.AddTransient<IArticuloServices, ArticuloServicesSQL>();
            services.AddTransient<IUsuarioServices, UsuarioServicesSQL>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<SeguridadContext>()
                .AddDefaultTokenProviders();

            //Uso con google
            services.AddAuthentication().AddGoogle(
                opt => {
                    opt.ClientId = "687742780953-bh93gknqvaveq6c7rsg2d7uj3tjl42uq.apps.googleusercontent.com";
                    opt.ClientSecret = "q_tohkqK7iaox5dBdF5xTsbi";
                }
                );

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();
            services.AddSession(
                options => {
                    options.IdleTimeout = TimeSpan.FromSeconds(60);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                }
                );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
