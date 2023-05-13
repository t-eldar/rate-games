import { useEffect, useState, useCallback } from 'react';
import { useAwait } from './use-await';

export const useFetch = <
  TFetch extends () => Promise<any> // eslint-disable-line
>(
  fetch: TFetch
): {
  data: Awaited<ReturnType<typeof fetch>> | undefined;
  isLoading: boolean;
  error: unknown;
  refetch: () => Promise<Awaited<ReturnType<TFetch>> | undefined>;
} => {
  const [data, setData] = useState<Awaited<ReturnType<typeof fetch>>>();
  const { promise, isLoading, error } = useAwait<TFetch>(fetch);

  const fetcher = useCallback(async (): Promise<
    Awaited<ReturnType<TFetch>> | undefined
  > => {
    const result = await promise(...([] as Parameters<TFetch>));
    setData(result);
    return result;
  }, []);

  useEffect(() => {
    fetcher();
  }, []);
  return {
    data,
    isLoading,
    error,
    refetch: fetcher,
  };
};
