using ChatApp.Api.CustomHandleMiddleware;
using ChatApp.Api.Data;
using ChatApp.Api.Data.Helper;
using ChatApp.Api.Data.Identity;
using ChatApp.Api.Mapper;
using ChatApp.Api.Repos.ModuleRepo;
using ChatApp.Api.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;
using System.Text;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
#region DbContext
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion
#region AddIdentity
builder.Services.AddIdentity<CustomUser, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;

}).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

#endregion
#region DiRepo
builder.Services.AddRepoService().MapperService();
builder.Services.AddSignalR();
builder.Services.AddScoped<LoginUserActionFilter>();
builder.Services.AddSingleton<PresenceTracker>();
#endregion
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))

    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
            {
                context.Token=accessToken;
            }
            return Task.CompletedTask;
        }
       
    };
});


var app = builder.Build();
app.UseRouting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using(var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();
    var roleManager=scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var context=scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();   
  await   IdentitySeeder.seedRoles(roleManager);
    await  IdentitySeeder.seedUser(userManager);
    await IdentitySeeder.SeedApplicationUsers(userManager,context);
}
app.UseMiddleware<CustomExceptionHandler>();
app.UseHttpsRedirection();

#region AddCors
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.WithOrigins("http://localhost:4200");
    options.AllowCredentials();
    options.WithExposedHeaders();
});
#endregion
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endPoint =>
{
    endPoint.MapControllers();
    endPoint.MapHub<PresenceHub>("hubs/presence");
});
//app.MapControllers();

app.Run();
