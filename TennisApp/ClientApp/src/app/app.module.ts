import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
//import { CounterComponent } from './counter/counter.component';
//import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { PlayersListComponent } from './players/players-list/players-list.component';
import { MatchesListComponent } from './matches/matches-list/matches-list.component';
import { PlayerDetailsComponent } from './players/player-details/player-details.component';
import { MatchReviewComponent } from './matches/match-review/match-review.component';
import { LoginPageComponent } from './login/login.page/login.page.component';
import { TokenInterceptor } from './interceptors/auth.token.interceptor';
import { PlayersRankingComponent } from './players/players-ranking/players-ranking.component';
import { AddPlayerComponent } from './players/admin/add-player/add-player.component';
import { AddMatchComponent } from './players/admin/add-match/add-match.component';
import { RegisterPageComponent } from './login/register.page/register.page.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    //CounterComponent,
    //FetchDataComponent,
    PlayersListComponent,
    PlayersRankingComponent,
    MatchesListComponent,
    PlayerDetailsComponent,
    MatchReviewComponent,
    LoginPageComponent,
    AddPlayerComponent,
    AddMatchComponent,
    RegisterPageComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      //{ path: 'counter', component: CounterComponent },
      //{ path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
      { path: 'players', component: PlayersListComponent },
      { path: 'ranking', component: PlayersRankingComponent },
      { path: 'matches', component: MatchesListComponent },
      { path: 'player-details', component: PlayerDetailsComponent },
      { path: 'match-reviews', component: MatchReviewComponent },
      { path: 'login-user', component: LoginPageComponent },
      { path: 'admin/players', component: AddPlayerComponent },
      { path: 'admin/matches', component: AddMatchComponent },
      { path: 'admin/register', component: RegisterPageComponent },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
