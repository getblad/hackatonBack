using System.Security.Claims;
using DataAccessLibrary.Configurations;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repositories;
using DataAccessLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestApiASPNET.Authentication;
using RestApiASPNET.Controllers;
using RestApiASPNET.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddScoped(typeof(IDbService<>), typeof(DbService<>));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddSwaggerGen(opt => {
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<HpContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("HPDatabase")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = builder.Configuration["Auth0:Audience"];
    // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
    // options.TokenValidationParameters = new TokenValidationParameters
    // {
    //     NameClaimType = ClaimTypes.NameIdentifier
    // };
});
builder.Services.AddAuth0AuthenticationClient(config =>
{
    config.Domain = domain;
    config.ClientId = builder.Configuration["Auth0Management:ClientId"];
    config.ClientSecret = builder.Configuration["Auth0Management:ClientSecret"];
});
builder.Services.AddAuth0ManagementClient().AddManagementAccessToken();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:user", policy => policy.Requirements.Add(new HasScopeRequirement("read:user", domain)));
    options.AddPolicy("read:users", policy => policy.Requirements.Add(new HasScopeRequirement("read:users", domain)));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<EventService, EventService>();


builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
