import { useEffect, useState } from 'react';
import { useAwait } from './use-await';

export const usePagedFetch = <
  TFetch extends (
    limit: number,
    offset: number,
    abortSignal: AbortSignal
  ) => Promise<any[]> //eslint-disable-line
>(
  fetch: TFetch,
  pageNumber: number,
  limit = 20,
  dependencies: unknown[] = []
) => {
  const [data, setData] = useState<Awaited<ReturnType<TFetch>>>();
  const [hasMore, setHasMore] = useState(false);
  const [isCleaned, setIsCleaned] = useState(false);

  const { promise, isLoading, error } = useAwait(
    async (abortSignal: AbortSignal, page: number) => {
      const result = await fetch(limit, page * limit, abortSignal);
      setData((prevData) => {
        if (prevData) {
          return [...new Set([...prevData, ...result])] as Awaited<
            ReturnType<TFetch>
          >;
        } else {
          return [...result] as Awaited<ReturnType<TFetch>>;
        }
      });
      setHasMore(result.length > 0);
    },
    [isCleaned, ...dependencies]
  );

  useEffect(() => {
    const abortController = new AbortController();
    promise(abortController.signal, pageNumber);

    return () => {
      abortController.abort();
      setIsCleaned(true);
    };
  }, [pageNumber, promise]);

  return { isLoading, error, data, hasMore };
};
