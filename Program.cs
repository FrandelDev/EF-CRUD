using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test.Models;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext)=>{
    dbContext.Database.EnsureCreated();
    return  Results.Ok("Database en memoria: "+ dbContext.Database.IsInMemory());
});

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext)=>{
    return Results.Ok(dbContext.Tareas.Include(prop => prop.Categoria));
});

app.MapPost("/api/tareas",async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea)=>{

    tarea.TareaId= Guid.NewGuid();
    tarea.FechaCreacion= DateTime.Now;
    await dbContext.AddAsync(tarea);
    await dbContext.SaveChangesAsync();

    return Results.Ok();

});

app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id)=>{
    var tareaActual = dbContext.Tareas.Find(id);
if(tareaActual != null){
    tareaActual.Nombre = tarea.Nombre;
    tareaActual.Descripcion = tarea.Descripcion;
    tareaActual.PrioridadTarea = tarea.PrioridadTarea;
    await dbContext.SaveChangesAsync();

    return Results.Ok();
}
return Results.NotFound();
});

app.MapDelete("/api/tareas/{id}",async ([FromServices] TareasContext dbContext, [FromRoute] Guid id)=>{
var taskToDelete = dbContext.Tareas.Find(id);
if(taskToDelete != null){
    dbContext.Remove(taskToDelete);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
}
return Results.NotFound();
});

app.Run();
