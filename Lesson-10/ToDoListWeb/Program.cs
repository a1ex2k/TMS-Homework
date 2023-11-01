using ToDoListWeb.Services;
using ToDoListWeb.SourceProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITodoListService>(sp =>
            ActivatorUtilities.CreateInstance<StaticToDoListService>(sp,
                new JsonTodoListSourceProvider(builder.Configuration["JsonSource"]))
        );

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

app.Run();
