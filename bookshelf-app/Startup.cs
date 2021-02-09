using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookshelf;
using bookshelf.Context;
using bookshelf.DAL;
using bookshelf.FakeData;
using bookshelf.Model.Books;
using bookshelf.Model.Chats;
using bookshelf.Model.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace bookshelf_app
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
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins", builder =>
                    builder.WithOrigins("https://localhost:8001").AllowAnyMethod().AllowAnyHeader());
            });
            /*services.AddDbContext<BaseDBContext>(
                options => options.UseSqlServer(Configuration["ConnectionString"]));*/
            var contextOptions = new DbContextOptionsBuilder<BaseDBContext>()
                .UseSqlServer(Configuration["ConnectionString"])
                .Options;
            using var context = new BaseDBContext(contextOptions);
            services.AddSingleton<FakeDataContext>();
            services.AddSingleton<IBaseRepository<UserBook>>(service => new DataFake(service.GetService<BaseDBContext>()));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "bookshelf_app", Version = "v1"});
            });
            
            // services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "bookshelf_app v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors("MyAllowSpecificOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}