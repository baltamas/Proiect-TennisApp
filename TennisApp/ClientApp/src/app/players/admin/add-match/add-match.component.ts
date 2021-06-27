import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../login/auth.service';
import { Matches, MatchWithDeps } from '../../../matches/matches.model';
import { MatchesWithPlayers } from '../../../matches/matches.with.player.model';
import { Player } from '../../player.model';

@Component({
  selector: 'app-add-match',
  templateUrl: './add-match.component.html',
  styleUrls: ['./add-match.component.css']
})
export class AddMatchComponent implements OnInit {


  public matches: Matches[];
  num_stages: number = 4;
  num_generated_matches: number = 0;
  stage: number;
  errors;

  public players: Player[];

  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string,
    private router: Router, private authSvc: AuthService)
  {
    this.loadMatches();
    this.loadPlayers();
  }

  loadMatches() {
    this.http.get<Matches[]>(this.apiUrl + 'matches').subscribe(result => {
      console.log(result);
      this.matches = result;
    }, error => console.error(error));
  }

  loadPlayers() {
    this.http.get<Player[]>(this.apiUrl + 'players').subscribe(result => {
      this.players = result;
    }, error => console.error(error));
  }

  generateMatches() {
    this.stage = this.num_stages - 1;

    this.http.delete(this.apiUrl + 'matches').subscribe(() => {
      //this.loadMatches();
      this.addMatches(this.stage);
    });
  }

  addMatches(stage: number) {
    let num_matches_per_stage = 1 << stage;
    console.log("stage");
    console.log(this.stage);

    if (this.stage == this.num_stages - 1) {
      for (let i = 0; i < num_matches_per_stage; i++) {
        this.addMatch(this.stage);
      }
    }
    else
    {
      let m2 = false;
      let match1Id = 0;
      let match2Id = 0;
      for (let i = 0; i < this.matches.length; i++) {
        console.log("MATCH.................");
        console.log(this.matches[i]);
        console.log("stagestr");
        console.log(this.matches[i].stage);
        console.log(this.stageToString(stage + 1));

        if (this.matches[i].stage == this.stageToString(stage + 1)) {
          if (!m2)
            match1Id = this.matches[i].matchId;
          else {
            match2Id = this.matches[i].matchId;
            this.addMatch(this.stage, match1Id, match2Id);
          }
          m2 = !m2;
        }
      }
    }


    
  }
  
  addMatch(stage: number, match1Id?: number, match2Id?: number) {
    console.log(`addMatch ${stage} ${match1Id} ${match2Id}`);
    let match: MatchWithDeps = new MatchWithDeps();
    match.stage = this.stageToString(stage);
    match.dep1Id = match1Id;
    match.dep2Id = match2Id;
    this.http.post(this.apiUrl + 'matches', match).subscribe(
      () => {
        this.num_generated_matches++;
        let num_matches_per_stage = 1 << stage;
        console.log("1 " + this.num_generated_matches);
        console.log("2 " + num_matches_per_stage);
        if (this.num_generated_matches == num_matches_per_stage) {
          //this.loadMatches();
          this.http.get<Matches[]>(this.apiUrl + 'matches').subscribe(result => {
            //console.log(result);
            this.matches = result;
            this.num_generated_matches = 0;
            if (this.stage > 0) {
              this.stage--;
              this.addMatches(this.stage);
            }
          }, error => console.error(error));
        }
      },
      (err) => {
        this.errors = JSON.stringify(err);
      })
  }

  stageToString(stage: number): string {
    switch (stage) {
      case 0: return "finala";
      case 1: return "semifinala";
      case 2: return "sferturi";
      default: return `1/${1<<stage}`;
    }
  }

  updateMatch(match: Matches) {
    this.http.put(this.apiUrl + 'matches/' + match.matchId, match).subscribe(result => {
      this.loadMatches();
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
