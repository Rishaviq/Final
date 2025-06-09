using Final.Models;
using Final.Repositories;
using Final.Repositories.Implementations.BankAccount;
using Final.Repositories.Implementations.Transfer;
using Final.Repositories.Implementations.User;
using Final.Repositories.Implementations.UsersPerAcc;
using Final.Repositories.Interfaces.BankAccount;
using Final.Repositories.Interfaces.Transfer;
using Final.Repositories.Interfaces.User;
using Final.Repositories.Interfaces.UsersPerAcc;
using Final.Services.Implementations.BankAccount;
using Final.Services.Implementations.Transfer;
using Final.Services.Implementations.User;
using Final.Services.Interfaces.BankAccount;
using Final.Services.Interfaces.Transfer;
using Final.Services.Interfaces.User;
using Final.Services.Interfaces.UsersPerAcc;

namespace Final.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            builder.Services.AddScoped<ITransferRepository, TransferRepository>();
            builder.Services.AddScoped<IUsersPerAccRepository, UsersPerAccRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBankAccountService, BankAccountService>();
            builder.Services.AddScoped<ITransferService, TransferService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            ConnectionFactory.Initialize(builder.Configuration["SQL:Connection"]);

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
