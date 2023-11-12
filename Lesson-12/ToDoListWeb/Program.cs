using ToDoListWeb.Services;
using ToDoListWeb.SourceProviders;
using ToDoListWeb.Utility;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITodoListService>(sp =>
            ActivatorUtilities.CreateInstance<FileToDoListService>(sp,
                new JsonTodoListSourceProvider(builder.Configuration["JsonSource"]))
        );

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseErrorHandlingMiddleware();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDoList}/{action=AllTasks}/{param?}");

app.Run();
