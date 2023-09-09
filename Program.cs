

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using person_dogs;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<PersonContext>(builder.Configuration.GetConnectionString("dbSQL"));
//builder.Services.AddDbContext<PersonContext>(dbcontext => dbcontext.UseInMemoryDatabase("PersonDB"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/persons",async ([FromServices] PersonContext dbcontext)=>{
    dbcontext.Database.EnsureCreated();
  
    return Results.Ok(dbcontext.Persons);
});
app.MapGet("/dogs",async ([FromServices] PersonContext dbcontext)=>{
    dbcontext.Database.EnsureCreated();
    
    return Results.Ok(dbcontext.Dogs.Include(prop => prop.Amo));
});
app.Run();
