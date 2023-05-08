type CreateReviewRequest = {
  title: string;
  description: string;
  ratingId: number;
  gameId: number;
};
export const createReview = (request: CreateReviewRequest) => {
  console.log(request);
};
