using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Services;
using WorldYachts.Services.Accessories;
using WorldYachts.Services.Admin;
using WorldYachts.Services.Boat;
using WorldYachts.Services.BoatType;
using WorldYachts.Services.BoatWood;
using WorldYachts.Services.Customer;
using WorldYachts.Services.SalesPerson;
using WorldYachts.Services.User;
using WorldYachtsApi.Middlewares;
using WorldYachtsApi.Serialization;

 namespace WorldYachtsApi
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
            //Сервис контекста БД
            services.AddDbContext<WorldYachtsDbContext>(option =>
            {
                option.EnableDetailedErrors();
                option.UseSqlServer(Configuration.GetConnectionString("Default"), 
                    b => b.MigrationsAssembly("WorldYachtsApi"));
            });

            //Маппер пользователей
            services.AddAutoMapper(
                typeof(UserMapper),
                typeof(BoatMapper),
                typeof(AccessoryMapper)
                );

            //Сервис контроллеров
            services.AddControllers();
            
            //Сервис сериализации ответа в XML формат
            services.AddMvc().AddXmlSerializerFormatters();
            
            //Сервис кэширования ответов
            //services.AddResponseCaching();

            //Сервис работы с репозиториями EF Core
            services.AddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));
            
            #region Сервисы работы со сущностями

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISalesPersonService, SalesPersonService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IBoatService, BoatService>();
            services.AddScoped<IBoatTypeService, BoatTypeService>();
            services.AddScoped<IBoatWoodService, BoatWoodService>();
            services.AddScoped<IAccessoryService, AccessoryService>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();
            
            //app.UseAuthentication();

            //app.UseAuthorization();
            
            app.UseMiddleware<JwtMiddleware>();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
