import {
  MaxGameInfo,
  MaxNormalizableGame,
  MinGameInfo,
  MinNormalizableGame,
} from '@/types/entities';
import { changeResolution } from './images';

export const normalizeMaxGame = (game: MaxNormalizableGame): MaxGameInfo => {
  if (typeof game.cover === 'number' || !game.cover.url) {
    throw new Error('Incorrect data');
  }
  if (game.screenshots) {
    if (typeof game.screenshots[0] === 'number') {
      throw new Error('Incorrect data');
    }
    game.screenshots.forEach((s) => {
      if (typeof s !== 'number' && s.url) {
        s.url = changeResolution(s.url, '1080p');
      }
    });
  }
  game.cover.url = changeResolution(game.cover.url, '1080p');
  if (game.firstReleaseDate) {
    const firstReleaseDate = new Date(game.firstReleaseDate);
    return { ...game, firstReleaseDate };
  }
  return { ...game, firstReleaseDate: undefined };
};

export const normalizeMinGame = (game: MinNormalizableGame): MinGameInfo => {
  if (typeof game.cover === 'number' || !game.cover.url) {
    throw new Error('Incorrect data');
  }
  game.cover.url = changeResolution(game.cover.url, '1080p');
  if (game.firstReleaseDate) {
    const firstReleaseDate = new Date(game.firstReleaseDate);
    return { ...game, firstReleaseDate };
  }
  return { ...game, firstReleaseDate: undefined };
};

export const normalizeGames = (games: MinNormalizableGame[]): MinGameInfo[] => {
  const result = new Array<MinGameInfo>();
  for (const game of games) {
    result.push(normalizeMinGame(game));
  }

  return result;
};
