using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Gestao_Acesso03
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
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            // Para fazer a Injeção de Dependencia
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Forma bem simplificada  = o que está em aspas ( controleAcesso ) tem que ser passado na claim
            //services.AddAuthentication("controleAcesso").AddCookie("controleAcesso");

            // Para a CLAIM funcionar
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromSeconds(15);
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.LoginPath = new PathString("/AccessUser/Login");
                options.LogoutPath = new PathString("/AccessUser/Logout");
                options.AccessDeniedPath = new PathString("/AccessUser/AcessoNegado");
            });

            // Para controlar a POLICY
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Autenticados", policy => policy.RequireRole("Administrador","Gerente","Supervisor","Usuario"));
                options.AddPolicy("Tecnologia", policy => policy.RequireRole("Gerente","Supervisor","Usuario"));
            });



        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // Fazendo a tratativa de Erro de Página 404
        app.Use(async (context, next) =>
        {
            await next();
            if (context.Response.StatusCode == 404)
            {
                context.Request.Path = "/PageNotFound";
                await next();
            }
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();





        // Por Causa das CLAIMS
        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
}
