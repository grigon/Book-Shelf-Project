using bookshelf.Context;
using bookshelf.DAL;
//using bookshelf.FakeData;
using bookshelf.Model.Books;
using bookshelf.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace bookshelf_app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<BaseDBContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("BookShelf"), b => b.MigrationsAssembly("bookshelf-app")));
            // var contextOptions = new DbContextOptionsBuilder<BaseDBContext>()
            //     .UseSqlServer(Configuration["ConnectionString"])
            //     .Options;
            //services.AddDbContext<BaseDBContext>(
            //    options => options.UseSqlServer(Configuration["ConnectionString"][0],
            //        b => b.MigrationsAssembly("bookshelf-app")));
            services.AddScoped<IChatRepository, ChatRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins", builder =>
                    builder.WithOrigins("https://localhost:8001").AllowAnyMethod().AllowAnyHeader());
            });
            //services.AddAutoMapper(Assembly.GetExecutingAssembly("bookshelf"));
            services.AddAutoMapper(typeof(ChatProfile).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(ChatProfile).GetTypeInfo().Assembly);
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            //services.AddDbContext<BaseDBContext>(
            //    options => options.UseSqlServer(Configuration["ConnectionString"]));
            //services.AddSingleton<IBaseRepository<UserBook>>(service => new DataFakeRepository());

            services.AddMvcCore();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "bookshelf_app", Version = "v1"});
            });
        }
        
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