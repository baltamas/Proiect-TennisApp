  
export class Matches {
  matchId: number;  
  stage: string;     
  date: string;
  player1Id: number;
  player2Id: number;
  winner: boolean;
  reviews: string;
}

export class MatchWithDeps {
  stage: string;
  date: Date;
  dep1Id: number;
  dep2Id: number;
}
