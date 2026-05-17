using GameStore.Data;
using GameStore.EndPoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.AddGameStoreDb();

var app = builder.Build();

app.MapGamesEndPoints();
app.MapGenresEndPoints();

app.MigrateDb();
app.Run();