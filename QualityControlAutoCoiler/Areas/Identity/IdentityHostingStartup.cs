using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(ProjectX.Areas.Identity.IdentityHostingStartup))]
namespace ProjectX.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<ProjectXContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("ProjectXContextConnection")));

            //    services.AddDefaultIdentity<ProjectXUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //        .AddEntityFrameworkStores<ProjectXContext>();
            //});
        }
    }
}