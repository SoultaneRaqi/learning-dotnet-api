using GameStore.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<GameDto> games = [
    new GameDto(
        1,
        "Cyberpunk 2077",
        "RPG",
        "$59.99",
        new DateOnly(2020, 12, 10)
    ),

    new GameDto(
        2,
        "Elden Ring",
        "Action RPG",
        "$69.99",
        new DateOnly(2022, 2, 25)
    ),

    new GameDto(
        3,
        "Minecraft",
        "Sandbox",
        "$29.99",
        new DateOnly(2011, 11, 18)
    ),

    new GameDto(
        4,
        "FIFA 25",
        "Sports",
        "$49.99",
        new DateOnly(2024, 9, 27)
    ),

    new GameDto(
        5,
        "Call of Duty: Modern Warfare III",
        "Shooter",
        "$69.99",
        new DateOnly(2023, 11, 10)
    ),

    new GameDto(
        6,
        "The Witcher 3",
        "Open World RPG",
        "$39.99",
        new DateOnly(2015, 5, 19)
    ),

    new GameDto(
        7,
        "Forza Horizon 5",
        "Racing",
        "$59.99",
        new DateOnly(2021, 11, 9)
    ),

    new GameDto(
        8,
        "Hades",
        "Roguelike",
        "$24.99",
        new DateOnly(2020, 9, 17)
    )
];

app.MapGet("/games", () => games);

app.Run();