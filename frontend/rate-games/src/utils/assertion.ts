import { MaxNormalizableGame, MinNormalizableGame } from '@/types/entities';

export const isMaxNormalizableGame = (
  data: unknown
): data is MaxNormalizableGame => {
  if (data && typeof data === 'object' && isDataCorrect(data)) {
    return true;
  }
  return false;
};

export const isMinNormalizableGames = (
  data: unknown
): data is MinNormalizableGame[] => {
  console.log(data);
  
  if (!Array.isArray(data)) {
    return false;
  }
  for (const item of data) {
    if (!isDataCorrect(item)) {
      return false;
    }
  }
  return true;
};

const isDataCorrect = (data: unknown): boolean =>
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