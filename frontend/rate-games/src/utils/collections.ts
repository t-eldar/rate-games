export const range = (startAt: number, count: number): number[] =>
  [...Array(count).keys()].map((i) => i + startAt);
