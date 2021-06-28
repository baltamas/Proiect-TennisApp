import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Player } from '../../player.model';

@Component({
  selector: 'app-add-player',
  templateUrl: './add-player.component.html',
  styleUrls: ['./add-player.component.css']
})
export class AddPlayerComponent implements OnInit {

  public players: Player[];
  public player: Player = new Player();
  errors;

  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string) {
    this.loadPlayers();
  }

  loadPlayers() {
    this.http.get<Player[]>(this.apiUrl + 'players').subscribe(result => {
      this.players = result;
    }, error => console.error(error));
  }

  addPlayer() {
    this.http.post(this.apiUrl + 'players', this.player).subscribe(
      () => { this.loadPlayers(); this.errors = ""; },
      (err) => {
        this.errors = JSON.stringify(err);
      }    )
  }

  updatePlayer(player: Player) {
    this.http.put(this.apiUrl + 'players/' + player.id, player).subscribe(result => {
      
    }, error => console.error(error));
  }

  deletePlayer(id: number) {
    this.http.delete(this.apiUrl + 'players/' + id).subscribe(
      () => { this.loadPlayers(); this.errors = ""; },
      (err) => {
        this.errors = JSON.stringify(err);
      })
  }


  ngOnInit() {
  }

}
