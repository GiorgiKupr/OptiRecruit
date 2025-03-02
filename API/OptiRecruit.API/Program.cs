using Application;
using Application.Abstraction;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Services.PDFTools;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddSingleton<ResumeParserServie>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Resume API", Version = "v1" });

    c.OperationFilter<FileUploadOperationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
