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
builder.Services.Configure<ImageServiceSettings>(
    builder.Configuration.GetSection("UserServiceDatabase"));

builder.Services.AddSingleton<TestService>();
builder.Services.AddSingleton<ImageService>();
builder.Services.AddSingleton<UserService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy  =>
        {
            policy
                .WithOrigins("*")
                .WithMethods("*")
                .WithHeaders("*");
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,

            ValidateLifetime = true,
            
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
