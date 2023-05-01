import { lorelei } from '@dicebear/collection';
import { createAvatar } from '@dicebear/core';
import { useCallback, useState } from 'react';

export const useLoreleiAvatar = () => {
  const generateRandomSeed = () =>
    (Math.random() + 1).toString(36).substring(7);

  const generateAvatar = useCallback(
    (seed?: string) =>
      createAvatar(lorelei, {
        seed: seed,
      }).toDataUriSync(),
    []
  );
  const [url, setUrl] = useState(() => generateAvatar(generateRandomSeed()));

  return {
    url,
    generate: (seed?: string) => {
      if (!seed) {
        seed = generateRandomSeed();
      }
      const value = generateAvatar(seed);
      setUrl(value);
    },
  };
};
