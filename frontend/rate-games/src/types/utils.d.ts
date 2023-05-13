export type KeysMatching<T, TValue> = {
  [K in keyof T]-?: T[K] extends TValue ? K : never;
}[keyof T];

export type Prettify<T> = {
  [key in keyof T]: T[key];
};