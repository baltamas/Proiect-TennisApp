
export class Review {
  reviewId: number;
  date: string;
  text: string;
}

export class MatchWithReviews {
  reviews: Review[];
}
