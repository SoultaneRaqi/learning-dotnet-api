using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos;

public record CreateGameDto(
    [Required][StringLength(50)]string Name ,
    [Required][StringLength(20)]string Genre ,
    [Range(1,150)]decimal Price ,
    DateOnly ReleaseDate 
);  