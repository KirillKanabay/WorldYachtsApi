using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Services;
using WorldYachts.Services.Accessories;
using WorldYachts.Services.AccessoriesToBoat;
using WorldYachts.Services.Admins;
using WorldYachts.Services.Boats;
using WorldYachts.Services.BoatTypes;
using WorldYachts.Services.BoatWood;
using WorldYachts.Services.BoatWoods;
using WorldYachts.Services.Contracts;
using WorldYachts.Services.Customers;
using WorldYachts.Services.Invoices;
using WorldYachts.Services.OrderDetails;
using WorldYachts.Services.Orders;
using WorldYachts.Services.Partners;
using WorldYachts.Services.SalesPersons;
using WorldYachts.Services.Users;
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

            //Мапперы
            services.AddAutoMapper(
                typeof(UserMapper),
                typeof(BoatMapper),
                typeof(AccessoryMapper),
                typeof(OrderMapper),
                typeof(ContractMapper)
                );

            //Сервис контроллеров
            services.AddControllers();
            
            //Сервис сериализации ответа в XML формат
            services.AddMvc().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Сервис кэширования ответов
            services.AddResponseCaching();

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
            services.AddScoped<IAccessoryToBoatService, AccessoryToBoatService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IPartnerService, PartnerService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IInvoiceService, InvoiceService>();

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

            app.UseMiddleware<JwtMiddleware>();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
