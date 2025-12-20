using APIServiceFactory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "IngTracker API",
        Version = "v1",
        Description = "API REST para el seguimiento de materias de carrera universitaria",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "IngTracker",
            Email = "contact@ingtracker.com"
        }
    });
});

ServiceFactory.AddServices(builder.Services);
ServiceFactory.AddConnectionString(builder.Services, builder.Configuration.GetConnectionString("DefaultConnection"));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "IngTracker API v1");
        options.RoutePrefix = string.Empty; // Swagger UI en la raíz
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
