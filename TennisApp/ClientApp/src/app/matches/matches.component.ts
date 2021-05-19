import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Matches } from './matches.model';

@Component({
  selector: 'app-matches',
  templateUrl: './matches.component.html',
  styleUrls: ['./matches.component.css']
})
export class MatchesComponent implements OnInit {
  public matches: Matches[];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    http.get<Matches[]>(apiUrl + 'matches').subscribe(result => {
      this.matches = result;
    }, error => console.error(error));
  }
  ngOnInit() {
  }
}
