using GameStore.Data;
using GameStore.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.EndPoints;

public static class GenresEndPoints
{
    public static void MapGenresEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");
        // Get all genres 

        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            await dbContext.Genres
                .Select(genre => new GenreDto(genre.Id, genre.Name))
                .AsNoTracking()
                .ToListAsync();
        });
    }
}