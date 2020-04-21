using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Core;
using Web.Core.Configuration;
using Web.Core.Interfaces;
using Web.Dapper.UnitOfWork;

namespace Web
{
    public class Startup
    {
        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Constructor

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 註冊 Configuration
            services.AddSingleton(Configuration);

            #region 解析 DbSettings

            services.Configure<DbSettings>(Configuration.GetSection("DbSettings"));

            // 註冊區塊驗證器
            services.AddSingleton<ISettingsValidator, SettingsValidator>();

            // 注入經橋接後被解析的 DbSettings
            services.AddScoped<IDbSettingsResolved, DbSettingsBridge>();

            #endregion

            // 註冊 ConnectionFactory
            services.AddScoped<IConnectionFactory, ConnectionFactory>();

            // 每一 Request 都注入一個新實例
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(name: "customer", pattern: "{controller=Customer}/{action=Customers}/{id?}");
                endpoints.MapControllerRoute(name: "order", pattern: "{controller=Order}/{action=Orders}/{id?}");
                endpoints.MapControllerRoute(name: "uow", pattern: "{controller=TestUow}/{action=Get}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        #region Private Method

        #endregion
    }
}
