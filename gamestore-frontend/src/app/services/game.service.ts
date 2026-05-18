import {HttpClient} from '@angular/common/http';
import {Injectable , inject} from '@angular/core';
import {environment} from '../../environments/environment';
import {GameSummary , GameDetails} from '../models/game.model';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/games` || "http://localhost:5189/games";

  // Get all games
  getGames(): Observable<GameSummary[]> {
    return this.http.get<GameSummary[]>(this.apiUrl);
  }

  // Get one game
  getGame(id: number): Observable<GameDetails> {
    return this.http.get<GameDetails>(`${this.apiUrl}/${id}`);
  }

  // Create a game
  createGame(game: GameDetails): Observable<GameDetails> {
    return this.http.post<GameDetails>(this.apiUrl, game);
  }

  // Update a game
  updateGame(id: number, game: GameDetails): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, game);
  }

  // Delete a game
  deleteGame(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
