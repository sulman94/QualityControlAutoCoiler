using Entities.Models;
using Logger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectX.Middleware;
using Services.Interfaces;
using Services.Models;
using Services.Services;
using System;
using System.Globalization;

namespace ProjectX
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
            services.AddDbContext<QualityControlAutoCoilerContext>(options =>
                    options.UseSqlServer(
                       Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<QualityControlAutoCoilerUser>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Default User settings.
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<QualityControlAutoCoilerContext>();
            services.AddRazorPages().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNamingPolicy = null; // Disable property naming policy (use property names as-is)
                options.JsonSerializerOptions.IgnoreReadOnlyProperties = false; // Include read-only properties during serialization
                options.JsonSerializerOptions.WriteIndented = true; // Indent JSON for better readability
            });
            services.AddDistributedMemoryCache();
            // For Temp Data 
            services.AddMvc().AddSessionStateTempDataProvider();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.Cookie.Name= Configuration["AppSetting:CookieName"].ToString();
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                //options.Cookie.HttpOnly = true;
                //options.Cookie.SameSite = SameSiteMode.Strict;
                //options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.SlidingExpiration = true;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = Configuration["AppSetting:AppCookieName"].ToString();
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddSession(options =>
            {
                options.Cookie.Name = Configuration["AppSetting:SessionCookieName"].ToString();
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = Configuration["AppSetting:AddAntiforgeryCookieName"].ToString();
            });
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IAdmin, AdminServices>();
            services.AddScoped<IUserAccessService, UserAccessService>();
            services.AddScoped<IMachines, MachineServices>();
            services.AddScoped<IColors, ColorServices>();
            services.AddScoped<ISizeCategory, SizeCategoryServices>();
            services.AddScoped<IAutoCoiler, AutoCoilerServices>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Configure Global Culture
            var cultureInfo = new CultureInfo("en-GB") // UK English uses dd/MM/yyyy by default
            {
                DateTimeFormat = { ShortDatePattern = "dd/MM/yyyy", LongDatePattern = "dd MMMM yyyy" }
            };
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    var headers = ctx.Context.Response.Headers;
                    headers["Cache-Control"] = "public, max-age=31536000";
                    headers["Expires"] = DateTime.Now.AddYears(1).ToString("R");
                }
            }); ;

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCustomMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
