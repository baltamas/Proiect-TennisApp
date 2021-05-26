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

  constructor(private playersService: PlayersService) {
  }

  getPlayers() {
    this.playersService.getPlayers().subscribe(p => this.players = p);
  }

  ngOnInit() {
  }

}
