namespace GameStore.Dtos;

/* A Dto is a contract between the client and server , it represents
 a shared agreement about how data will be transferred and used  
*/
public record GameDto(
   int Id ,
   string Name ,
   string Genre ,
   string Price ,
   DateOnly ReleaseDate 
 );