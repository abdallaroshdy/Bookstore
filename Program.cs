using Bookstore.Models;
using Bookstore.Models.Repositories;

namespace Bookstore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews()
                            .AddRazorRuntimeCompilation();
            builder.Services.AddSingleton<IBookstoreRepository<Author>, AuthorRepository>();
            builder.Services.AddSingleton<IBookstoreRepository<Book>, BookRepository>();



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
