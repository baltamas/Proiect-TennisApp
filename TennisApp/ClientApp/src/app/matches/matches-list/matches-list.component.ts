import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Matches } from '../matches.model';
import { Player } from '../../players/player.model';
import { MatchesWithPlayers } from '../matches.with.player.model';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { Review } from '../match-review/reviews.models';
import { AuthService } from '../../login/auth.service';

@Component({
  selector: 'app-list-matches',
  templateUrl: './matches-list.component.html',
  styleUrls: ['./matches-list.component.css']
})
export class MatchesListComponent implements OnInit {
  //public matches: Matches[];
  //public players: Player[];
  public matcheswithplayers: MatchesWithPlayers[];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string,
    private router: Router, private authSvc: AuthService  ) {
    //http.get<Matches[]>(apiUrl + 'matches').subscribe(result => {
    //  this.matches = result;
    //}, error => console.error(error));
    //http.get<Player[]>(apiUrl + 'players').subscribe(result => {
    //  this.players = result;
    //}, error => console.error(error));
    http.get<MatchesWithPlayers[]>(apiUrl + 'matches/players').subscribe(result => {
      console.log(result);
      this.matcheswithplayers = result;
    }, error => console.error(error));
  }
  ngOnInit() {
  }

  viewReviews(matchId: number) {
    this.goToReviews(matchId);
  }

  addReview(matchId: number) {
    if (this.authSvc.userLogedIn()) {
      this.goToReviews(matchId);
    }
    else {
      this.router.navigate(['login-user']);
    }
  }

  goToReviews(matchId: number) {
    this.router.navigate(['match-reviews'], { queryParams: { id: matchId } });
  }

  firstWinnerStyle(match: MatchesWithPlayers) {
    if (match.winner != null && match.winner) {
      return { 'font-weight': 'bold'};
    }
  }

  secondWinnerStyle(match: MatchesWithPlayers) {
      if (match.winner != null && !match.winner)
        return { 'font-weight': 'bold'};
    }
}
