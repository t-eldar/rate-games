export type Game = {
  id: number;
  name?: string;
  summary?: string;
  firstReleaseDate?: number;
  aggregatedRating?: number;
  rating?: number;
  category?: GameCategory;
  cover?: number | Image;
  gameModes?: (number | GameMode)[];
  screenshots?: (number | Image)[];
  involvedCompanies?: (number | InvolvedCompany)[];
  genres?: (number | Genre)[];
  gameEngines?: (number | GameEngine)[];
  similarGames?: (number | Game)[];
  platforms?: (number | Platform)[];
};

export type Company = {
  id: number;
  name?: string;
  country?: number;
  startDate?: number;
  logo?: number | Image;
  published?: (number | Game)[];
  developed?: (number | Game)[];
};

export type GameEngine = {
  id: number;
  name?: string;
  description?: string;
  logo?: number | Image;
};

export type GameMode = {
  id: number;
  name?: string;
};

export type Genre = {
  id: number;
  name?: string;
};

export type Image = {
  id: number;
  width?: number;
  height?: number;
  url?: string;
  imageId?: string;
};

export type InvolvedCompany = {
  id: number;
  company?: number | Company;
  developer?: boolean;
  porting?: boolean;
  publisher?: boolean;
  supporting?: boolean;
};

export type Platform = {
  id: number;
  abbreviation?: string;
  name?: string;
  generation?: number;
  category?: PlatformCategory;
  summary?: string;
  platformLogo?: number | Image;
};

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
