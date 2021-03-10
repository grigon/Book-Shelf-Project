using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using bookshelf;
using bookshelf_app.Auth;
using bookshelf.Context;
using bookshelf.DAL;
using bookshelf.DTO;
using bookshelf.Model.Users;
using bookshelf.DTO.Book;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace bookshelf_app
{
    public class Startup
    {
        private RSACryptoServiceProvider _rsa;
        private RsaSecurityKey _key;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _rsa = new RSACryptoServiceProvider(2048);
            _key = new RsaSecurityKey(_rsa);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options => { options.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<BaseDbContext>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider("BookShelf", typeof(DataProtectorTokenProvider<User>));


            services.AddAuthorization(config =>
            {
                // Add a new Policy with requirement to check for Admin
                config.AddPolicy("ShouldBeAnAdmin", options =>
                {
                    options.RequireAuthenticatedUser();
                    options.AuthenticationSchemes.Add(
                        JwtBearerDefaults.AuthenticationScheme);
                    options.Requirements.Add(new ShouldBeAnAdminRequirement());
                });
            });

            services.AddSingleton(_key);
            services.AddTransient<TokenManagerMiddleware>();
            // services.AddTransient<AuthorizationHandler<ShouldBeAnAdminRequirement>, ShouldBeAnAdminRequirementHandler>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],
                        IssuerSigningKey = _key,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            // services.AddDbContext<BaseDbContext>(
            //     options => options.UseSqlite(Configuration.GetConnectionString("BookShelf"), 
            //         b => b.MigrationsAssembly("bookshelf-app")));


            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins", builder =>
                    builder.WithOrigins("https://localhost:8001").AllowAnyMethod().AllowAnyHeader());
            });

            services.AddDbContext<BaseDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("BookShelf"),
                    b => b.MigrationsAssembly("bookshelf-app")));

            services.AddTransient<DataSeeder>();
            services.AddDistributedRedisCache(r =>
                r.Configuration = Configuration["redis:ConnectionString"]
            );

            services.AddScoped<IUserRepository<User>, UserRepository>();
            // services.AddScoped<UserRepository<UserBook>, UserBookRepository>();

            services.AddAutoMapper(typeof(UserProfile).GetTypeInfo().Assembly);
            services.AddScoped<BookRepository>();
            services.AddAutoMapper(typeof(BookProfile).GetTypeInfo().Assembly);

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "bookshelf_app", Version = "v1"});
            });
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

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<TokenManagerMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}