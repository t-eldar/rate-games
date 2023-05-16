import { useState, useCallback } from 'react';

export const useAwait = <
  // eslint-disable-next-line
  TFunction extends ((...args: any[]) => Promise<any>) | (() => Promise<any>)
>(
  callback: TFunction,
  dependencies: unknown[] = []
) => {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<unknown>();  

  const promise = useCallback(
    async (
      ...args: Parameters<TFunction>
    ): Promise<Awaited<ReturnType<TFunction>> | undefined> => {
      try {
        setError(undefined);
        setIsLoading(true);
        return await callback(...args);
      } catch (e) {
        setError(e);
      } finally {
        setIsLoading(false);
      }
    },
    dependencies
  );
  return { promise, isLoading, error };
};
