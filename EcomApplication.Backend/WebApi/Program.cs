using Infrasturcture.Presistence;
using Application;
using Infrasturcture.shared.Models;
using Microsoft.Extensions.Configuration;
using Infrasturcture.shared;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer();

// Add services to the container.

//var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
//builder.Services.AddSingleton(emailConfig);
builder.Services.AddControllers().AddNewtonsoftJson(); ;
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

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
