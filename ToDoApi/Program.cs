using ToDoApi;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoDBContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenPolicy",
   policy =>
   {
       policy.WithOrigins("http://localhost:3000")
       .AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
   });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapGet("/item", (ToDoDBContext todo) =>
{
    return todo.Items.ToList();
});
app.MapPost("/item/{item}", async (string item,ToDoDBContext todo) =>
{
     Item newItem=new Item();
     newItem.Name=item;
    newItem.IsComplete=false;
    todo.Items.Add(newItem);
    await todo.SaveChangesAsync();
    return item;
});
app.MapPut("/item/{id}/{iscomplete}", async (int id,bool iscomplete,ToDoDBContext todo) =>
{
     var oldItem = await todo.Items.FindAsync(id);
    if (oldItem is null)
      return null;
    oldItem.IsComplete = iscomplete;
     await todo.SaveChangesAsync();
    return oldItem;
});
app.MapDelete("/item/{id}", async (int id,ToDoDBContext todo) =>
{
    var item = await todo.Items.FindAsync(id);
    if (item != null)
    {
        todo.Items.Remove(item);
        await todo.SaveChangesAsync();
    }
    return "thank you";
});
app.UseCors("OpenPolicy");
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
//app.MapGet("/",()=>"ToDo Api is running:)");
app.Run();