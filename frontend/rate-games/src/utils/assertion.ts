import {
  MaxNormalizableGame,
  MinNormalizableGame,
  Review,
} from '@/types/entities';

export const isMaxNormalizableGame = (
  data: unknown
): data is MaxNormalizableGame => {
  if (data && typeof data === 'object' && isGameDataCorrect(data)) {
    return true;
  }

  return false;
};

export const isMinNormalizableGames = (
  data: unknown
): data is MinNormalizableGame[] => {
  if (!Array.isArray(data)) {
    return false;
  }

  for (const item of data) {
    if (isGameDataCorrect(item)) {
      return true;
    }
  }

  return false;
};

export const isGameDataCorrect = (data: unknown): boolean =>
  data &&
  typeof data === 'object' &&
  'id' in data &&
  typeof data.id === 'number' &&
  'name' in data &&
  typeof data.name === 'string' &&
  'summary' in data &&
  typeof 'summary' === 'string' &&
  'cover' in data &&
  typeof data.cover === 'object'
    ? true
    : false;

export const isReviews = (data: unknown): data is Review[] => {
  if (!Array.isArray(data)) {
    return false;
  }
  for (const item of data) {
    if (!isReview(item)) {
      return false;
    }
  }

  return true;
};

export const isReview = (data: unknown): data is Review => {
  if (
    data &&
    typeof data === 'object' &&
    'id' in data &&
    typeof data.id === 'number' &&
    'title' in data &&
    typeof data.title === 'string' &&
    'description' in data &&
    typeof data.description === 'string' &&
    'dateCreated' in data &&
    typeof data.dateCreated === 'object' &&
    'gameId' in data &&
    typeof data.gameId === 'number' &&
    'userId' in data &&
    typeof data.userId === 'string' &&
    'userInfo' in data &&
    typeof data.userInfo === 'object' &&
    'rating' in data &&
    typeof data.rating === 'number'
  ) {
    return true;
  }

  return false;
};
