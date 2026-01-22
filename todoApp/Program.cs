using AppContext;
using Microsoft.EntityFrameworkCore;
using TodoEntity;
using TodosDTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("listtodos", () =>
{
    using var context = new AppDbContext();
    var todos = context.TodoTask.ToList();
    return todos;
});

app.MapPost("addtodo",(CreateTodoDto createDTo) =>
{
    TodoItem todoItem = new(){
        Id = Guid.CreateVersion7(),
        Description = createDTo.Description,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
        isCompleted = false
        };
    using var context = new AppDbContext();
    context.TodoTask.Add(todoItem);
    context.SaveChanges();
    return Results.Ok("Tarea Anadida");
});

app.MapPatch("editodo/{uuid}", (string uuid,PatchTodoDto patchTodo) =>
{
    
    using var context = new AppDbContext();
    
    if(Guid.TryParse(uuid, out Guid GuidUuid))
    {
    TodoItem? todo = context.TodoTask.FirstOrDefault(x => x.Id.Equals(GuidUuid));
    if(todo is null)
    {
        return Results.BadRequest("Todo not Found");
    }
    todo.Description = patchTodo.Description;
    todo.UpdatedAt = DateTime.UtcNow;
    context.SaveChanges();
    return Results.Ok("Tarea Editada con Exito");
    }
    return Results.BadRequest("Invalid UUID");

});

app.MapPatch("completetask/{uuid}",(string uuid) =>
{
    using var context = new AppDbContext();
    if(Guid.TryParse(uuid,out Guid uuidParsed))
    {
        TodoItem? todoTask = context.TodoTask.FirstOrDefault( x => x.Id.Equals(uuidParsed));
        if(todoTask is null)
        {
            return Results.BadRequest("Todo not Found");
        }
        todoTask.UpdatedAt = DateTime.UtcNow;
        todoTask.isCompleted = true;
        context.SaveChanges();
        return Results.Ok("Completed Task");
    }
    return Results.BadRequest("Invalid UUID");

});

app.Run();
