const dateFormat =
  /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*))(?:Z|(\+|-)([\d|:]*))?$/;

export const dateReviver = (key: string, value: unknown) => {
  if (typeof value === 'string' && dateFormat.test(value)) {
    return new Date(value);
  }

  return value;
};
