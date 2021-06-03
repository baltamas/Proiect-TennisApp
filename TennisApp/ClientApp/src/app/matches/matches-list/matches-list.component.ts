import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Matches } from '../matches.model';

@Component({
  selector: 'app-list-matches',
  templateUrl: './matches-list.component.html',
  styleUrls: ['./matches-list.component.css']
})
export class MatchesListComponent implements OnInit {
  public matches: Matches[];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    http.get<Matches[]>(apiUrl + 'matches').subscribe(result => {
      this.matches = result;
    }, error => console.error(error));
  }
  ngOnInit() {
  }
}
