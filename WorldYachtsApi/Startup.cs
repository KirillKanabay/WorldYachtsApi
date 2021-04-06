 using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using Microsoft.AspNetCore.Authentication.JwtBearer;
 using Microsoft.EntityFrameworkCore;
 using WorldYachtsApi.Data;
 using WorldYachtsApi.Middlewares;
 using WorldYachtsApi.Serialization;
 using WorldYachtsApi.Services;

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
                option.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            //Репозиторий работы с EF
            services.AddScoped(typeof(IEfRepository<>), typeof(UserRepository<>));

            //Маппер пользователей
            services.AddAutoMapper(typeof(UserMapper));

            //Сервис контроллеров
            services.AddControllers();
            
            //Сервис сериализации ответа в XML формат
            services.AddMvc().AddXmlSerializerFormatters();
            
            //Сервис кэширования ответов
            services.AddResponseCaching();
            
            //Сервис работы с пользователями
            services.AddScoped<IUserService, UserService>();
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
