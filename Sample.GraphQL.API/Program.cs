using Sample.GraphQL.Application.Endpoints;
using Sample.GraphQL.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistence();

var app = builder.Build();

var rider = app.MapGroup("/v1/cinema");
rider.AddEnpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

SeedDb.Initialize(app);

app.Run();

