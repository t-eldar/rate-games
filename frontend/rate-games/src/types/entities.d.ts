import { User } from './authentication';
import { Game } from './igdb-models';

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
  userInfo: User;
  rating: number;
};

export type MaxNormalizableGame = Pick<
  Game,
  | 'id'
  | 'name'
  | 'summary'
  | 'aggregatedRating'
  | 'rating'
  | 'category'
  | 'firstReleaseDate'
> & {
  cover?: Image;
  gameModes?: GameMode[];
  screenshots?: Image[];
  involvedCompanies?: InvolvedCompany[];
  genres?: Genre[];
  gameEngines?: GameEngine[];
  similarGames?: number[];
  platforms?: Platform[];
};

export type MaxGameInfo = Omit<MaxNormalizableGame, 'firstReleaseDate'> & {
  firstReleaseDate?: Date;
};

export type MinNormalizableGame = Omit<
  MaxNormalizableGame,
  'similarGames' | 'screenshots'
>;
export type MinGameInfo = Omit<MaxGameInfo, 'similarGames' | 'screenshots'>;
