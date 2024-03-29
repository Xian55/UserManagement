using Serilog;

using UserManagement.API.Extensions;
using UserManagement.Application;
using UserManagement.Infrastructure;
using UserManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPersistence(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(builder =>
{
    builder.SupportNonNullableReferenceTypes();
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.ConfigureSwagger();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.EnsureDatabaseCreated();

app.UseSerilogRequestLogging();

app.UseCustomExceptionHandler();

//app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();