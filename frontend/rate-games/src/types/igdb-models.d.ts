export type Game = {
  id: number;
  name: string;
  summary: string;
  firstReleaseDate: number;
  aggregatedRating: number;
  rating: number;
  category: GameCategory;
  gameModes: (number | GameModeResponse)[];
  screenshots: (number | ImageResponse)[];
  involvedCompanies: (number | InvolvedCompanyResponse)[];
  genres: (number | GenreResponse)[];
  gameEngines: (number | GameEngineResponse)[];
  similarGames: (number | GameResponse)[];
  platforms: (number | PlatformResponse)[];
};
export type GameResponse = Partial<Game>;

export type Company = {
  id: number;
  name: string;
  country: number;
  startDate: number;
  logo: number | ImageResponse;
  published: (number | GameResponse)[];
  developed: (number | GameResponse)[];
};
export type CompanyResponse = Partial<Company>;

export type GameEngine = {
  id: number;
  name: string;
  description: string;
  logo: number | ImageResponse;
};
export type GameEngineResponse = Partial<GameEngine>;

export type GameMode = {
  id: number;
  name: string;
};
export type GameModeResponse = Partial<GameMode>;

export type Genre = {
  id: number;
  name: string;
};
export type GenreResponse = Partial<Genre>;

export type Image = {
  id: number;
  width: number;
  height: number;
  url: string;
  imageId: string;
};
export type ImageResponse = Partial<Image>;

export type InvolvedCompany = {
  id: number;
  company: number | CompanyResponse;
  developer: boolean;
  porting: boolean;
  publisher: boolean;
  supporting: boolean;
};
export type InvolvedCompanyResponse = Partial<InvolvedCompany>;

export type Platform = {
  id: number;
  abbreviation: string;
  name: string;
  generation: number;
  category: PlatformCategory;
  summary: string;
  platformLogo: number | ImageResponse;
};
export type PlatformResponse = Partial<Platform>;

enum GameCategory {
  MainGame = 0,
  DlcAddon = 1,
  Expansion = 2,
  Bundle = 3,
  StandaloneExpansion = 4,
  Mod = 5,
  Episode = 6,
  Season = 7,
  Remake = 8,
  Remaster = 9,
  ExpandedGame = 10,
  Port = 11,
  Fork = 12,
  Pack = 13,
  Update = 14,
}

enum PlatformCategory {
  Console = 1,
  Arcade = 2,
  Platform = 3,
  OperatingSystem = 4,
  PortableConsole = 5,
  Computer = 6,
}
