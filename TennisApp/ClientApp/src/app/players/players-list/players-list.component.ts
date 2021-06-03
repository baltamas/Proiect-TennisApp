import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Player } from '../player.model';
import { PlayersService } from '../players.service';

@Component({
  selector: 'app-list-players',
  templateUrl: './players-list.component.html',
  styleUrls: ['./players-list.component.css']
})
export class PlayersListComponent implements OnInit {

  public players: Player[];


  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    http.get<Player[]>(apiUrl + 'players').subscribe(result => {
      this.players = result;
    }, error => console.error(error));
  }


  ngOnInit() {
  }

}
