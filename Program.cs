using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Bookstore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews()
                            .AddRazorRuntimeCompilation();

            builder.Services.AddDbContext<BookStoreDBContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IBookstoreRepository<Author>, AuthorDBRepository>();
            builder.Services.AddScoped<IBookstoreRepository<Book>, BookDBRepository>();
            builder.Services.AddTransient<IAttachmecntService , AttachmentService>();


            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.MapGet("/", () => "Hello World!");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Author}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}
