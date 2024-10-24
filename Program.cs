using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using WebApplication3.Domain.Abstractions.Data;
using WebApplication3.Domain.Hubs;
using WebApplication3.WebApi.Middlewares;
using WebApplication3.Domain.Database.DbContexts;
using WebApplication3.Domain.Features.Auth.Repository;
//using WebApplication3.Domain.Features.Images.Repository;
using WebApplication3.Domain.Features.Games.Repository;
using WebApplication3.Domain.Features.Games.Entities;

using WebApplication3.Domain.Data.Repositories;
using WebApplication3.Domain.Features.Players.Repository;
using WebApplication3.Domain.Features.Players.Entities;
using WebApplication3.Domain.Features.Messages.Repository;

using WebApplication3.Domain.Database;
using WebApplication3.Domain.Mappings;
using WebApplication3.Domain.Features.Friends.Entities;
using WebApplication3.Domain.Features.Friends.Repository;

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/Chess_log.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();


try
{
    Log.Information("Starting up the application");
    var builder = WebApplication.CreateBuilder(args);


    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);

    builder.Services.AddControllers();

    builder.Services.AddHttpContextAccessor();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options => {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Chess", Version = "v1" });
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {

            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    },
                    Scheme = "Oauth2",
                    Name = JwtBearerDefaults.AuthenticationScheme,
                    In = ParameterLocation.Header
                },

                new List<string>()

            }

        });
    });

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters  {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),


        } );


    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));



    builder.Services.AddDbContext<AuthDbContext>(options => options.
    UseSqlServer(builder.Configuration.GetConnectionString("ChessAuthConnectionString")));

    builder.Services.AddDbContext<ApplicationDbContext>(options => options.
    UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnectionString")));


    builder.Services.AddSignalR();


    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<GamesHelpers>();


    builder.Services.AddScoped<IRepository<GameEntity>, GameRepositorySql>();
    builder.Services.AddScoped<IRepository<PlayerEntity>, PlayerRepositoryMySql>();
    builder.Services.AddScoped<MessagesRepositorySql>();
    builder.Services.AddScoped<FriendsRepositorySql>();
    builder.Services.AddScoped<ITokenRepository, TokenRepository>();
   

    //builder.Services.AddScoped<IimageRepository, LocalImageRepository>();


    builder.Services.AddAutoMapper(typeof (AutomapperProfiles));

    builder.Services.AddIdentityCore<IdentityUser>()
        .AddRoles<IdentityRole>()
        .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Default")
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddDefaultTokenProviders();


    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
    }

    );
    
    var app = builder.Build();

    // Apply pending migrations
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            Log.Information("Applying migrations for AuthDbContext");
            var authDbContext = services.GetRequiredService<AuthDbContext>();
            authDbContext.Database.Migrate();
            Log.Information("Applied migrations for AuthDbContext");

            Log.Information("Applying migrations for ApplicationDbContext");
            var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.Migrate();
            Log.Information("Applied migrations for ApplicationDbContext");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "An error occurred while applying migrations");
            throw; // Re-throw the exception to ensure Azure logs it
        }
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();


        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials()); // allow credentials
    }

    app.UseRouting();
 

    app.UseMiddleware<ExceptionHandlerMiddleware>();
    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    //app.UseStaticFiles(new StaticFileOptions {
    //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    //    RequestPath = "/Images"
    //});


    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHub<GameHub>("/gameHub"); // Map SignalR hubs
    });

    app.Run();

    }
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
    
}
finally
{
    Log.CloseAndFlush();
}
