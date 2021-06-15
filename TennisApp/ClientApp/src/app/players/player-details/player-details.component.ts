import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Player } from '../player.model';

@Component({
  selector: 'app-player-details',
  templateUrl: './player-details.component.html',
  styleUrls: ['./player-details.component.css']
})
export class PlayerDetailsComponent implements OnInit {

  id: number;
  public player: Player;

  //constructor(private route: ActivatedRoute,
  //) { }

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string,
    private route: ActivatedRoute,) {
    this.route.queryParams.subscribe(params => {
      this.id = params['id'];
      console.log("Id:" + this.id);
      http.get<Player>(apiUrl + 'players/' + this.id).subscribe(result => {
        this.player = result;
        console.log(this.player);
      }, error => console.error(error));
    });
  }

  ngOnInit() {
  }

}
