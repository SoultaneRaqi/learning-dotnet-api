import {HttpClient} from '@angular/common/http';
import {Injectable , inject} from '@angular/core';
import {environment} from '../../environments/environment';
import {Genre} from '../models/genre.model';
import {Observable} from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class GenreService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/genres` || "http://localhost:5189/games";

  getGenres(): Observable<Genre[]>{
    return this.http.get<Genre[]>(this.apiUrl)
  }

}
