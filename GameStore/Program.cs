
using GameStore.Data;
using GameStore.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connString = "Date Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndPoints();

app.Run();