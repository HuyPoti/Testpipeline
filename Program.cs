var builder = WebApplication.CreateBuilder(args);

// Thay vì AddOpenApi(), hãy dùng Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Thay vì MapOpenApi(), hãy dùng Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", () => "Hello World!")
   .WithName("GetWeatherForecast");

app.Run();