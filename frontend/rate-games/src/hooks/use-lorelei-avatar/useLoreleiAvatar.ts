import { lorelei } from '@dicebear/collection';
import { createAvatar } from '@dicebear/core';
import { useState } from 'react';

export const useLoreleiAvatar = () => {
  const generateRandomSeed = () =>
    (Math.random() + 1).toString(36).substring(7);

  const [url, setUrl] = useState(() =>
    createAvatar(lorelei, {
      seed: generateRandomSeed(),
    }).toDataUriSync()
  );
  console.log(url);

  return {
    url,
    generate: (seed?: string) => {
      if (!seed) {
        seed = generateRandomSeed();
      }
      const value = createAvatar(lorelei, {
        seed: seed,
      }).toDataUriSync();
      setUrl(value);
    },
  };
};
