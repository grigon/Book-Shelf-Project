using bookshelf;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace bookshelf_app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            RunSeeding(host);
            
            host.Run();
        }

        private static void RunSeeding(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<DataSeeder>();
                seeder.SeedAsync().Wait();
            }
            
        }
        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        /*travel 722c6ffe-d4d3-417f-8efa-27f39d294377
         //romans e6226e28-f214-4a55-a602-da2d8cb5f65d
         //crime d144cec9-e195-4f13-9e89-e216fc1593a8
         
         
         
        tomek e8b5105a-8698-4e3c-a1b1-97affc0c0ea4*/
        //jane d144cec9-e195-4f13-9e89-e216fc1593a8
       //sarah 5d4c1dc4-1324-4798-b427-ee67adb3acac
        
        //samsara id 7d5501c9-e9bb-4a5b-8ae1-c6036084b1a7
        //chrobot id 47ec2aef-e285-4f0c-9e43-95556e0441c7
        
        
        //marta id 4d23367b-63ef-46d9-b5ba-4dfde6a35a01
        //anna id a7ad1ecf-a540-479a-af16-36ceaabf1321
    }
}