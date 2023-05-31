using BaseApi;
using BaseApi.Handlers;
using BaseApi.Middlewares;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Context
builder.Services
    .AddDbContext<Context>(options => options.UseInMemoryDatabase("database"));

// HandlersProcessor
builder.Services
    .RegisterHandlers()
    .AddScoped<HandlersProcessor>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
