using GameStore.Dtos;

namespace GameStore.EndPoints;

public static class GamesEndPoints
{
    const string GetGameEndpointName = "GetGame";
    
    private static readonly  List<GameDto> Games = [
        new GameDto(
            1,
            "Cyberpunk 2077",
            "RPG",
            59.99m,
            new DateOnly(2020, 12, 10)
        ),

        new GameDto(
            2,
            "Elden Ring",
            "Action RPG",
            69.99m,
            new DateOnly(2022, 2, 25)
        ),

        new GameDto(
            3,
            "Minecraft",
            "Sandbox",
            29.99m,
            new DateOnly(2011, 11, 18)
        ),

        new GameDto(
            4,
            "FIFA 25",
            "Sports",
            49.99m,
            new DateOnly(2024, 9, 27)
        ),

        new GameDto(
            5,
            "Call of Duty: Modern Warfare III",
            "Shooter",
            69.99m,
            new DateOnly(2023, 11, 10)
        ),

        new GameDto(
            6,
            "The Witcher 3",
            "Open World RPG",
            39.99m,
            new DateOnly(2015, 5, 19)
        ),

        new GameDto(
            7,
            "Forza Horizon 5",
            "Racing",
            59.99m,
            new DateOnly(2021, 11, 9)
        ),

        new GameDto(
            8,
            "Hades",
            "Roguelike",
            24.99m,
            new DateOnly(2020, 9, 17)
        )
    ];

    public static void MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        
    //Get all games 
        group.MapGet("/", () => Games);

    //Get one game by id 

        group.MapGet("/{id}", (int id) =>
            {
                var game = Games.Find(g => g.Id == id);
                return game  is null ? Results.NotFound() :  Results.Ok(game);
            })
            .WithName(GetGameEndpointName);

    //Post :
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            /*if (string.IsNullOrEmpty(newGame.Name))
            {
                return Results.BadRequest("Game name is required");
            }*/
            GameDto game = new(
                Games.Count +1 ,
                newGame.Name ,
                newGame.Genre ,
                newGame.Price ,
                newGame.ReleaseDate 
            );
            Games.Add(game);
    
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });


    // Put :
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
        {
            var index = Games.FindIndex(g => g.Id == id);
            if (index < 0)
            {
                return Results.NotFound() ;
            }
            Games[index] = new GameDto(
                id,
                updateGame.Name,
                updateGame.Genre,
                updateGame.Price,
                updateGame.ReleaseDate
            );
    
            return Results.NoContent();
        });


    //Delete :
        group.MapDelete("/{id}", (int id) =>
        {
            Games.RemoveAll(game => game.Id == id);
    
            return Results.NoContent();
        });
        
    }
}