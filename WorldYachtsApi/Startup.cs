using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Services.Admin;
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
            //������ ��������� ��
            services.AddDbContext<WorldYachtsDbContext>(option =>
            {
                option.EnableDetailedErrors();
                option.UseSqlServer(Configuration.GetConnectionString("Default"), 
                    b => b.MigrationsAssembly("WorldYachtsApi"));
            });

            //������ �������������
            services.AddAutoMapper(typeof(UserMapper));

            //������ ������������
            services.AddControllers();
            
            //������ ������������ ������ � XML ������
            services.AddMvc().AddXmlSerializerFormatters();
            
            //������ ����������� �������
            services.AddResponseCaching();

            #region ������� ������ �� ����������

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISalesPersonService, SalesPersonService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAdminService, AdminService>();

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
