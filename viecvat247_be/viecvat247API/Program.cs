using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using viecvat247API.Hubs;
using viecvat247Service;
using viecvat247Service.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger EravolGroup Solution", Version = "v1" });
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "@JWT Authorization header using the Bearer schema. \r\n\r\n" +
            "Enter 'Bearer' [Space] and then your token in the text input below. \r\n\r\n" +
            "Example: 123456abcdef",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    config.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});


//odata
builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100);
    // Bật tính năng EnableQuery
    options.EnableQueryFeatures();

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services.AddAutoMapper(typeof(Program).Assembly);*/
//builder.Services.AddCors(builder =>
//    builder.AddPolicy("corsapp", b => { b.WithOrigins("*").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); }));


//MAPPER
var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new BussinessObject.Mapper.AutoMapper()); });
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


//Declare DI
builder.Services.AddScoped<IAuthenService, AuthenService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

//Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        //builder.WithOrigins("*")
        //       .AllowAnyHeader()
        //       .AllowAnyMethod();
        builder.WithOrigins("http://localhost:3000")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
        builder.WithOrigins("https://viecvat247.com")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
    });
});

builder.Services.AddSignalR();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());
builder.Services.AddSingleton<IDictionary<string, string>>(opts => new Dictionary<string, string>());
var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{

    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto

});
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}
app.UseRouting();
app.UseCors();
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chat");
    endpoints.MapHub<NotificationHub>("/notificationHub");
});
app.MapControllers();

app.Run();
