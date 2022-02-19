using AstroBackEnd.Repositories;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Services.Implement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AstroBackEnd
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

            //jwt setup
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtSetting:Issuer"],
                        ValidAudience = Configuration["JwtSetting:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSetting:SecurityKey"]))
                    };
                });

            //Dependency injection part
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<INewsService, NewsService>();

            services.AddScoped<IProfileService, ProfileService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IOrderDetailService, OrderDetailService>();


            services.AddDbContext<Data.AstroDataContext>();

            //api and razor setup
            services.AddControllersWithViews();
            services.AddRazorPages();

            //swagger setup
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AstroBackEnd", Version = "v1" });
            });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AstroBackEnd v1"));

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers(); 

            });

            Data.AstroDataInit.Seed(app);

        }
    }
}
