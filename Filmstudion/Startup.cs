using Filmstudion.Data;
using Filmstudion.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Filmstudion.Entities;
using Microsoft.AspNetCore.Identity;
using Filmstudion.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Filmstudion
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
            services.AddIdentity<AdminModel, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(
                cfg => {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };
                    }
                    );
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Filmstudion"));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddControllers();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieCoRepository, MovieCoRepository>();
            services.AddScoped<UserManager<AdminModel>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
