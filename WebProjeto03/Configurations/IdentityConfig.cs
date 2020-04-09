using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebProjeto03.ViewModels;

namespace WebProjeto03.Data
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<ApplicationUserViewModel, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                   //Password Settings
                options.Password.RequireDigit = false; //exigir um número dentro da senha
                options.Password.RequireLowercase = false; //uma das letras minuscula
                options.Password.RequireNonAlphanumeric = false;  //um caractere especial
                options.Password.RequireUppercase = false; //uma das letras maiuscula
                options.Password.RequiredLength = 6; //tamanho minimo da senha
                options.Password.RequiredUniqueChars = 1; //caracteres distintos na senha bloqueado

            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Lockout";
                options.AccessDeniedPath = "/Account/AccessDinied";
                options.ReturnUrlParameter = "ReturnUrl";
                options.SlidingExpiration = true;

            });



            return services;
        }
    }
}