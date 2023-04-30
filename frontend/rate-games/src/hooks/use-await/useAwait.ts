import { useState } from 'react';

export const useAwait = <
  // eslint-disable-next-line
  TFunction extends ((...args: any[]) => Promise<any>) | (() => Promise<any>)
>(
  callback: TFunction
) => {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<unknown>();

  const promise = async (
    ...args: Parameters<TFunction>
  ): Promise<Awaited<ReturnType<TFunction>> | undefined> => {
    try {
      setIsLoading(true);
      return await callback(...args);
    } catch (e) {
      setError(e);
    } finally {
      setIsLoading(false);
    }
  };
  return { promise, isLoading, error };
};
