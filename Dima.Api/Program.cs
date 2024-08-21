using Dima.Api.Data;
using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder
    .Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(
        x =>
        {
            x.UseSqlServer(cnnStr);
        }
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});
builder.Services.AddTransient<Handler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//Requisição -> Header e Body
//Get, Post, Put e Delete - CRUD
//Get (Não tem body)
//POST, PUT, DELETE (Com body)
// JSON - JavaScript Object Notation

//Endpoints
//Usar Convernções de Mercado
//https://localhost:0000/
//Os endpoints geralmente estão no plural Ex: O category seria /Categories
app.MapPost(
    "/v1/categories", 
    (Request request, Handler handler) => handler.Handle(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response>();
app.Run();