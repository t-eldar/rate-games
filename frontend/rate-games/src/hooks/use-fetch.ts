import { useEffect, useState, useCallback } from 'react';
import { useAwait } from './use-await';

export const useFetch = <
  TFetch extends (abortSignal: AbortSignal) => Promise<unknown>
>(
  fetch: TFetch,
  dependencies: unknown[] = []
): {
  data: Awaited<ReturnType<typeof fetch>> | undefined;
  isLoading: boolean;
  error: unknown;
  refetch: () => Promise<Awaited<ReturnType<TFetch>> | undefined>;
} => {
  const [data, setData] = useState<Awaited<ReturnType<typeof fetch>>>();
  const [isCleaned, setIsCleaned] = useState(false);
  const { promise, isLoading, error } = useAwait<TFetch>(fetch, [
    isCleaned,
    ...dependencies,
  ]);

  const abortController = new AbortController();

  const fetcher = useCallback(async (): Promise<
    Awaited<ReturnType<TFetch>> | undefined
  > => {
    const result = await promise(
      ...([abortController.signal] as Parameters<TFetch>)
    );
    setData(result);

    return result;
  }, [promise]);

  useEffect(() => {
    fetcher();
    return () => {
      setIsCleaned(true);
      abortController.abort();
    };
  }, [fetcher]);
  return {
    data,
    isLoading,
    error,
    refetch: fetcher,
  };
};
