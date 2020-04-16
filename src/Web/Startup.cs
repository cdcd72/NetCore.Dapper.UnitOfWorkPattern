using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Web.Core;
using Web.Core.Enum;
using Web.Repositories.Implement;
using Web.Repositories.Interface;

namespace Web
{
    public class Startup
    {
        #region Properties

        public IConfiguration Configuration { get; }

        // 常數定義
        private const string DB_SETTINGS = "DBSettings",
                             CONNECTION_TYPE = "ConnectionType";

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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // 註冊Configuration
            services.AddSingleton(Configuration);

            // 每一 Request 都注入一個新實例
            services.AddScoped<IUnitOfWork>(x => new UnitOfWork(GetConnection(Configuration)));

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
                routes.MapRoute(
                    name: "customer",
                    template: "{controller=Customer}/{action=Customers}/{id?}");
                routes.MapRoute(
                    name: "order",
                    template: "{controller=Order}/{action=Orders}/{id?}");
                routes.MapRoute(
                    name: "uow",
                    template: "{controller=TestUow}/{action=Get}/{id?}");
            });
        }

        #region Private Method

        /// <summary>
        /// 取得連線
        /// </summary>
        /// <returns></returns>
        private IDbConnection GetConnection(IConfiguration configuration)
        {
            #region 取得資料庫設定

            var dbSettingsSection = configuration.GetSection(DB_SETTINGS);

            // 連線類型及字串
            string connectionType = dbSettingsSection.GetSection(CONNECTION_TYPE).Value,
                   connectionString = dbSettingsSection.GetConnectionString(connectionType);

            // 資料庫來源提供者
            DBProvider dBProvider = connectionType.ConvertFromString<DBProvider>();

            #endregion

            var factory = new ConnectionFactory(dBProvider, connectionString);
            var connection = factory.CreateConnection();
            connection.Open();
            return connection;
        }

        #endregion
    }
}
