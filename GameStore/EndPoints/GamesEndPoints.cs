using GameStore.Data;
using GameStore.Dtos;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.EndPoints;

public static class GamesEndPoints
{
    const string GetGameEndpointName = "GetGame";
    
    private static readonly  List<GameSummaryDto> Games = [
        new GameSummaryDto(
            1,
            "Cyberpunk 2077",
            "RPG",
            59.99m,
            new DateOnly(2020, 12, 10)
        ),

        new GameSummaryDto(
            2,
            "Elden Ring",
            "Action RPG",
            69.99m,
            new DateOnly(2022, 2, 25)
        ),

        new GameSummaryDto(
            3,
            "Minecraft",
            "Sandbox",
            29.99m,
            new DateOnly(2011, 11, 18)
        ),

        new GameSummaryDto(
            4,
            "FIFA 25",
            "Sports",
            49.99m,
            new DateOnly(2024, 9, 27)
        ),

        new GameSummaryDto(
            5,
            "Call of Duty: Modern Warfare III",
            "Shooter",
            69.99m,
            new DateOnly(2023, 11, 10)
        ),

        new GameSummaryDto(
            6,
            "The Witcher 3",
            "Open World RPG",
            39.99m,
            new DateOnly(2015, 5, 19)
        ),

        new GameSummaryDto(
            7,
            "Forza Horizon 5",
            "Racing",
            59.99m,
            new DateOnly(2021, 11, 9)
        ),

        new GameSummaryDto(
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
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            return await dbContext.Games
                .Include(game => game.Genre)
                .Select(game => new GameSummaryDto(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
                )).AsNoTracking().ToListAsync();
        });

    //Get one game by id 

        group.MapGet( "/{id}", async (int id , GameStoreContext dbContext) =>
            {
                var game = await dbContext.Games.FindAsync(id);
                
                return game  is null ? Results.NotFound() :  Results.Ok(
                    new GameDetailsDto(
                        game.Id,
                        game.Name,
                        game.GenreId,
                        game.Price,
                        game.ReleaseDate
                        ));
            })
            .WithName(GetGameEndpointName);

    //Post :
        group.MapPost("/", async (CreateGameDto newGame , GameStoreContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();    

            GameDetailsDto gameDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
                );
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDto.Id }, gameDto);
        });


    // Put :
        group.MapPut("/{id}", async (int id, UpdateGameDto updateGame  , GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame is  null)
            {
                return Results.NotFound() ;
            }
            existingGame.Name = updateGame.Name;
            existingGame.GenreId = updateGame.GenreId;
            existingGame.Price = updateGame.Price;
            existingGame.ReleaseDate = updateGame.ReleaseDate;
                
            await  dbContext.SaveChangesAsync();
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