import { useEffect, useState } from 'react';
import { useAwait } from './use-await';

export const useFetch = <
  TFetch extends () => Promise<any> // eslint-disable-line
>(
  fetch: TFetch
): {
  data: Awaited<ReturnType<typeof fetch>> | undefined;
  isLoading: boolean;
  error: unknown;
} => {
  const [data, setData] = useState<Awaited<ReturnType<typeof fetch>>>();
  const { promise, isLoading, error } = useAwait<TFetch>(fetch);

  useEffect(() => {
    const func = async () => {
      const result = await promise(...([] as Parameters<TFetch>));

      setData(result);
    };
    func();
  }, []);
  return { data, isLoading, error };
};
