// This call WebApplication.CreateBuilder() creates an object that represents our application 
// This variable allows our application to be configured before it will be executed
// Builder pattern - design patter (classic design patterns of objective programming

var builder = WebApplication.CreateBuilder(args);


// We define the element in IoC container
// Add services to the container.
builder.Services.AddControllers();
// This element is searching for our application and all the endpoints that we have defined
// GET /api/students - resource + what we want to do with this resource => endpoint
builder.Services.AddEndpointsApiExplorer();
// This element allows me to add automatically generated documentation for my application
builder.Services.AddSwaggerGen();

// This .Build(); method returns an application that is being configured according to what we have previously defined
var app = builder.Build();


//2. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // This element is basically a sample documentation that is automatically generated only available in our application
    // if it is running in development mode
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Minimal API

//var students = new List<Student>();
//{
//new Student { Id = 1, FirstName = "John", LastName = "Doe" };
//new Student { Id = 2, FirstName = "Jane", LastName = "Test" };
//};

//app.MapGet("/api/students", () =>
//{
//      Some logic
//      return Results.Ok(students); // 200 HTTP response - OK
//});

app.MapControllers();

app.Run();