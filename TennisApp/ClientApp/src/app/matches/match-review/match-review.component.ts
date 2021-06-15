import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../login/auth.service';
import { MatchWithReviews, Review } from './reviews.models';

@Component({
  selector: 'app-match-review',
  templateUrl: './match-review.component.html',
  styleUrls: ['./match-review.component.css']
})
export class MatchReviewComponent implements OnInit {

  id: number;
  reviews: MatchWithReviews;
  userLogedIn: boolean;

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string,
    private route: ActivatedRoute, private authSvc: AuthService) {
    this.userLogedIn = this.authSvc.userLogedIn();
    this.route.queryParams.subscribe(params => {
      this.id = params['id'];
      console.log("id " + this.id);
      console.log("Id:" + this.id);
      http.get<MatchWithReviews>(apiUrl + 'matches/' + this.id+'/reviews').subscribe(result => {
        this.reviews = result;
        console.log(this.reviews);
        console.log("reviews");
        console.log(this.reviews.reviews);
      }, error => console.error(error));
    });
  }

  ngOnInit() {
  }

  logOut() {
    this.authSvc.removeToken();
    this.userLogedIn = false;
    window.location.reload();
  }
}
