using AstroBackEnd.Repositories;
using AstroBackEnd.Repositories.Core;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Services.Implement;
using AstroBackEnd.Utilities;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            services.AddDbContext<Data.AstroDataContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<INewsService, NewsService>();

            services.AddScoped<IProfileService, ProfileService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IOrderDetailService, OrderDetailService>();

            services.AddScoped<IZodiacService, ZodiacService>();

            services.AddScoped<IHouseService, HouseService>();

            services.AddScoped<IZodiacHouseService, ZodiacHouseService>();


            services.AddScoped<IQuoteService, QuoteService>();

            services.AddScoped<IImageService, ImageService>();

            services.AddSingleton<FireabaseUtility>();

            services.AddScoped<IHoroscopeService, HoroscopeService>();

            services.AddScoped<IPlanetService, PlanetService>();

            services.AddScoped<IPlanetZodiacService, PlanetZodiacService>();

            services.AddScoped<IPlanetHouseService, PlanetHouseService>();

            services.AddDbContext<Data.AstroDataContext>();

            //api and razor setup
            services.AddControllersWithViews();
            services.AddRazorPages();

            //swagger setup
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AstroBackEnd", Version = "v1" });
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Configuration["FireBaseConfig"]),
                
            });

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AstroBackEnd v1"));

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 

            });

            Data.AstroDataInit.Seed(app);

        }
    }
}
