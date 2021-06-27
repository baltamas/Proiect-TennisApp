import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Player, PlayerWithRanking } from '../player.model';
import { PlayersService } from '../players.service';

@Component({
  selector: 'app-ranking-players',
  templateUrl: './players-ranking.component.html',
  styleUrls: ['./players-ranking.component.css']
})
export class PlayersRankingComponent implements OnInit {

  public players: PlayerWithRanking[];

    constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
      http.get<PlayerWithRanking[]>(apiUrl + 'players/ranking').subscribe(result => {
        this.players = result;
        console.log(result);
    }, error => console.error(error));
  }


  ngOnInit() {
  }

}
