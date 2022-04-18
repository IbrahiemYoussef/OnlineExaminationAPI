using FinalYearProject.Data;
using FinalYearProject.Data.Models;
using FinalYearProject.Models;
using FinalYearProject.Services;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace FinalYearProject
{
    public class Startup
    {
        public string ConnectionString { get; set; }

        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            string conn="";
            /*
            if(System.Environment.MachineName == "LAPTOP-GSR45SKK")
                conn = Configuration.GetConnectionString("Abusarie");
            else
                conn = Configuration.GetConnectionString("DefaultConnectionString");
            */
            conn = Configuration.GetConnectionString("DefaultConnectionString");
            ConnectionString = conn;


 
        }

        public IConfiguration Configuration { get; }
        readonly string allowmyspecificcors = "_allowmyspecificcors";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //for cors
            services.AddCors(c =>
            {
                c.AddPolicy("_allowmyspecificcors",
                    options => options.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });


            services.AddControllers();
            services.AddDbContext<mydbcon>(options => options.UseSqlServer(ConnectionString));
            services.AddTransient<ExamsService>();
            services.AddTransient<CoursesService>();
            services.AddTransient<QuestionBankService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<mydbcon>()
                .AddDefaultTokenProviders();
            
            
            services.AddAuthentication(
        CertificateAuthenticationDefaults.AuthenticationScheme)
        .AddCertificate();

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                
            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinalYearProject", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinalYearProject v1"));
            }


            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors(allowmyspecificcors);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AppInitializer.seed(app);

        }
    }
}
