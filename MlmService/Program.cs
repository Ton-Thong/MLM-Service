using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MlmService.Database;
using MlmService.Services;
using MlmService.Services.Interface;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;
using MlmService.Api;
using MlmService.Middleware;
using MlmService.Options;
using MlmService.Repository.Interface;
using MlmService.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtSettings = new JwtSettings();
builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
    };
});

builder.Services.AddDbContextPool<CoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<SharedContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SupportConnection")));

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.AllowTrailingCommas = true;
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

builder.Services.AddSwaggerGen(e =>
{
    e.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MLM API",
        Version = "v1"
    });

    e.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "using bearer scheme",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });

    e.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme,
                }
            },
            new List<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    e.IncludeXmlComments(xmlPath);
});

builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddHealthChecks();
builder.Services.AddOutputCache();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
    builder.SetIsOriginAllowed((_) => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //using (var scope = app.Services.CreateScope())
    //{
    //    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //    await dataContext.Database.MigrateAsync();
    //}

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.UseCors("corsapp");
app.UseMiddleware<ErrorHandling>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapAuthRoutes();
app.MapSupportRoutes();

app.Run();