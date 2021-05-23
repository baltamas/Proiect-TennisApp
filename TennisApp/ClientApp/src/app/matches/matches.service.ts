import { Injectable } from '@angular/core';
import { Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Matches } from './matches.model';

@Injectable({
  providedIn: 'root'
})
export class MatchesService {
  /*
  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    http.get<Matches[]>(apiUrl + 'matches').subscribe(result => {
      this.matches = result;
    }, error => console.error(error));
  }
  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string ){
    
  }
  getMatches(): Observable<Matches[]> {
  return this.httpClient.get<Matches[]>(apiUrl + 'matches');
}*/
}
