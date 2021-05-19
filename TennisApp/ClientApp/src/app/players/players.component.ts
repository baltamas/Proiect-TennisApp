import { Component, Inject, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Player } from './player.model';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.css']
})
export class PlayersComponent implements OnInit {

  public players: Player[];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    http.get<Player[]>(apiUrl + 'players').subscribe(result => {
      this.players = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
