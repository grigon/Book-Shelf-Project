using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace bookshelf_app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            /*Console.WriteLine(Guid.NewGuid());*/
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        /*genre 722c6ffe-d4d3-417f-8efa-27f39d294377
            author e8b5105a-8698-4e3c-a1b1-97affc0c0ea4*/
        
        //samsara id 7d5501c9-e9bb-4a5b-8ae1-c6036084b1a7
        //chrobot id 47ec2aef-e285-4f0c-9e43-95556e0441c7
        //marta id 4d23367b-63ef-46d9-b5ba-4dfde6a35a01
        //anna id a7ad1ecf-a540-479a-af16-36ceaabf1321
    }
}