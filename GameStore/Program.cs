using GameStore.Data;
using GameStore.EndPoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();
builder.AddGameStoreDb();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev" , policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();


app.UseCors("AllowAngularDev");

app.MapGamesEndPoints();
app.MapGenresEndPoints();



app.MigrateDb();
app.Run();