using Sample.GraphQL.Application;
using Sample.GraphQL.Application.Endpoints;
using Sample.GraphQL.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence();
builder.Services.AddPresentationLayer();

var app = builder.Build();

app.UseRouting();
app.MapGraphQL();


var rider = app.MapGroup("/v1/cinema");
rider.AddEndpoints();

SeedDb.Initialize(app);

app.Run();

