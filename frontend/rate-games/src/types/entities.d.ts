export type Rating = {
  id: number;
  value: number;
};

export type Review = {
  id: number;
  title: string;
  description: string;
  dateCreated: Date;
  gameId: number;
  userId: string;
  ratingId: number;
};
