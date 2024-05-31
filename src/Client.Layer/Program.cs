using Asp.Versioning.ApiExplorer;
using Client.Layer.Configuration;
using Data.Layer.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.ConfigureVersioning();
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureApiResponseBehavior();
builder.Services.ConfigureCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.ResolveDependencies();

// Configure the HTTP request pipeline.
var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
if (app.Environment.IsDevelopment())
    app.UseCors("Development");
else
    app.UseCors("Production");

app.UseHsts();
app.UseHttpsRedirection();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerConfig(apiVersionDescriptionProvider);
app.MapControllers();

app.Run();
