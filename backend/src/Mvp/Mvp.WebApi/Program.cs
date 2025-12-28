var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "Ca.WebApi");
        }
    );
}

app.MapPost("/api/echo", (EchoRequest request) =>
{
    

    return Results.Ok($"Hello, {request.Name}");
});

app.UseHttpsRedirection();

app.Run();

record EchoRequest(string Name);
