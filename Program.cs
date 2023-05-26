using System.Text;
using BookStoreApi.Services;
using DPOBackend.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TokenApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<TestSettings>(
    builder.Configuration.GetSection("TestStoreDatabase"));
builder.Services.Configure<ImageServiceSettings>(
    builder.Configuration.GetSection("ImageServiceDatabase"));
builder.Services.Configure<UserSettings>(
    builder.Configuration.GetSection("UserServiceDatabase"));

builder.Services.AddSingleton<TestService>();
builder.Services.AddSingleton<ImageService>();
builder.Services.AddSingleton<UserService>();

// Add services to the container.
builder.Services.AddMvc();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy =>
        {
            policy
                .WithOrigins("*")
                .WithMethods("*")
                .WithHeaders("*");
        });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidAudience = AuthOptions.AUDIENCE,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.KEY)),
        };
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();