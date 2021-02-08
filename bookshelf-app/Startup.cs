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
                    builder.WithOrigins("https://localhost:8001"));
            });
            
            services.AddSingleton(new BaseDBContext(""));
            services.AddSingleton<FakeDataContext>();
            services.AddSingleton<IBaseRepository<UserBook>>(service => new DataFake(service.GetService<FakeDataContext>()));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "bookshelf_app", Version = "v1"});
            });
            
            // services.AddControllersWithViews().AddRazorRuntimeCompilation();
            
            // services.AddDbContext<BookDBContext>(opt =>
            //     opt.UseSqlite("Data Source=bookshelf.db"));
            
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