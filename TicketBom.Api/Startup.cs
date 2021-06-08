using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TicketBom.Api.Container;
using TicketBom.Domain;
using TicketBom.Infra.Data;
using TicketBom.Infra.User;

namespace TicketBom.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketBom.Api", Version = "v1" });

            });

            services.AddDbContext<TicketBomContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TicketBom"));
                
            });
            
            services.AddApplicationServices();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("verystrongkeyverystrongkeyverystrongkey")),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    x.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = ctx => {
                            var provider = ctx.HttpContext.RequestServices;

                            var userAccessor = provider.GetService<IUserAccessor>();

                            if (userAccessor != null)
                                userAccessor.TenantId = ctx.Principal?.Claims.FirstOrDefault(u => u.Type == "TenantId")?.Value;

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketBom.Api v1"); c.RoutePrefix = ""; });
            }

            using (var context = new TicketBomContext(new DbContextOptionsBuilder<TicketBomContext>()
                .UseSqlServer(Configuration.GetConnectionString("TicketBom")).Options, new  UserAccessor())) { context.Database.Migrate(); }

            app.UseCors("MyPolicy");

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
