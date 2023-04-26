import { Game } from '@/types/igdb-models';

export const getMockedGames = () => [
  {
    id: 1,
    name: 'Thief II: The Metal Age',
    summary:
      'The ultimate thief is back! Tread softly as you make your way through 15 new complex, non-linear levels full of loot to steal and guards to outsmart. Improved enemy AI, new gadgets and a riveting story will draw you into the world of Thief II: The Metal Age, a place of powerful new technologies, fanatical religions and corruption.',
    firstReleaseDate: 953596800,
    aggregatedRating: 90,
    rating: 86.6902706277657,
    category: 0,
    gameModes: [
      {
        id: 1,
        name: 'Single player',
      },
    ],
    screenshots: [
      {
        id: 16867,
        url: '//images.igdb.com/igdb/image/upload/t_thumb/puvydf5d6v0zirxfhzpg.jpg',
      },
      {
        id: 16868,
        url: '//images.igdb.com/igdb/image/upload/t_thumb/z0b9mqcqbtmnnxigekjc.jpg',
      },
      {
        id: 40856,
        url: '//images.igdb.com/igdb/image/upload/t_thumb/ja4t61c1ndvtzufbdrf4.jpg',
      },
      {
        id: 40857,
        url: '//images.igdb.com/igdb/image/upload/t_thumb/yf2hsfdd4fe9ev6wmpye.jpg',
      },
      {
        id: 40858,
        url: '//images.igdb.com/igdb/image/upload/t_thumb/h4eotdbgoo30v40jtus0.jpg',
      },
      {
        id: 377572,
        url: '//images.igdb.com/igdb/image/upload/t_thumb/sc83c4.jpg',
      },
      {
        id: 377573,
        url: '//images.igdb.com/igdb/image/upload/t_thumb/sc83c5.jpg',
      },
      {
        id: 377574,
        url: '//images.igdb.com/igdb/image/upload/t_thumb/sc83c6.jpg',
      },
      {
        id: 377575,
        url: '//images.igdb.com/igdb/image/upload/t_thumb/sc83c7.jpg',
      },
      {
        id: 377576,
        url: '//images.igdb.com/igdb/image/upload/t_thumb/sc83c8.jpg',
      },
    ],
    involvedCompanies: [
      {
        id: 2739,
        company: {
          id: 4,
          name: 'Eidos Interactive',
          logo: {
            id: 357,
            url: '//images.igdb.com/igdb/image/upload/t_thumb/vrwhlkcmxvr7sqmu86nw.jpg',
          },
        },
        developer: false,
        porting: false,
        publisher: true,
        supporting: false,
      },
      {
        id: 2740,
        company: {
          id: 3,
          name: 'Looking Glass Studios',
          logo: {
            id: 13,
            url: '//images.igdb.com/igdb/image/upload/t_thumb/a8jgm46jckpac7fi2rbc.jpg',
          },
        },
        developer: true,
        porting: false,
        publisher: false,
        supporting: false,
      },
      {
        id: 21014,
        company: {
          id: 26,
          name: 'Square Enix',
          logo: {
            id: 121,
            url: '//images.igdb.com/igdb/image/upload/t_thumb/yqsot4fytthxblnyvcia.jpg',
          },
        },
        developer: false,
        porting: false,
        publisher: true,
        supporting: false,
      },
    ],
    genres: [
      {
        id: 5,
        name: 'Shooter',
      },
      {
        id: 13,
        name: 'Simulator',
      },
      {
        id: 31,
        name: 'Adventure',
      },
    ],
    gameEngines: [
      {
        id: 43,
        name: 'Dark Engine',
      },
    ],
    similarGames: [2, 3, 4, 41, 471, 564, 3025, 9377, 11118, 11171],
    platforms: [
      {
        id: 6,
        name: 'PC (Microsoft Windows)',
        platformLogo: {
          id: 203,
          url: '//images.igdb.com/igdb/image/upload/t_thumb/irwvwpl023f8y19tidgq.jpg',
        },
      },
    ],
  } satisfies Game,
];
