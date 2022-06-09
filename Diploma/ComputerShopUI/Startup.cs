using ComputerShopUI.Serivces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ComputerShopUI
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
            Configuration.Bind("Project", new Config());
            services.AddHttpContextAccessor();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        
                        ValidIssuer = AuthOptions.ISSUER,
 
                        ValidateAudience = true,
                        
                        ValidAudience = AuthOptions.AUDIENCE,

                        ValidateLifetime = true,
 
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        
                        ValidateIssuerSigningKey = true,
                    };
                });
            services.AddControllersWithViews();
            
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
 
            app.UseDefaultFiles();
            app.UseStaticFiles();
 
            app.UseRouting();
 
            app.UseAuthentication();
            app.UseAuthorization();
 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/home/homepage");
                    return Task.CompletedTask;
                });
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
