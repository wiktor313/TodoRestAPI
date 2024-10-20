using Microsoft.EntityFrameworkCore;
using Kotarski_Wiktor_ToDo_API.Models;
using Kotarski_Wiktor_ToDo_API.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoContext>
    (opt => opt.UseInMemoryDatabase("TodoDb"));
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
