using BookStoreApi.Services;
using DPOBackend.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<TestSettings>(
    builder.Configuration.GetSection("TestStoreDatabase"));

builder.Services.AddSingleton<TestService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
