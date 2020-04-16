using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            // 註冊Configuration
            services.AddSingleton(Configuration);

            // 每一 Request 都注入一個新實例
            services.AddScoped<IUnitOfWork>(x => new UnitOfWork(GetConnection(Configuration)));

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
