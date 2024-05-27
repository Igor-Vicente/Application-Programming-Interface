using Client.Layer.Configuration;
using Data.Layer.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureApiResponseBehavior();
builder.Services.ConfigureCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ResolveDependencies();

// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseCors("Development");
else
    app.UseCors("Production");

app.UseHsts();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
