export const debounce = <
  // eslint-disable-next-line
  TFunction extends ((...args: any[]) => void) | (() => void)
>(
  limit: number | undefined,
  callback: TFunction
) => {
  let timeoutId: NodeJS.Timeout | undefined;
  return (...args: Parameters<TFunction>) => {
    if (timeoutId) {
      clearTimeout(timeoutId);
    }
    timeoutId = setTimeout(callback, limit, args);
  };
};
