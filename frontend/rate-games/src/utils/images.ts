type Resolution = '720p' | '1080p' | 'micro' | 'thumb';

export const changeResolution = (
  igdbImageUrl: string,
  resolution: Resolution
) => igdbImageUrl.replace('t_thumb', `t_${resolution}`);
