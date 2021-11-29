using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Data.Models
{
    public class AppInitializer
    {
        public static void seed(IApplicationBuilder applicationBuilder)
        {
            //using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            //    if (!context.Books.Any())
            //    {
            //        context.Books.AddRange(
            //            new Book()
            //            {
            //                Name="casper"

            //            },
            //            new Book()
            //            {
            //                Name = "cyber"
            //            },
            //            new Book()
            //            {
            //                Name = "jwt"

            //            },
            //            new Book()
            //            {
            //                Name = "jwt2"

            //            }
            //         );
            //    }
            //    context.SaveChanges();
            //}
        }
    }
}
