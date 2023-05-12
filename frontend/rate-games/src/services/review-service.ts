import { User } from '@/types/authentication';
import { Review } from '@/types/entities';

type CreateReviewRequest = {
  title: string;
  description: string;
  ratingId: number;
  gameId: number;
};
export const createReview = (request: CreateReviewRequest) => {
  console.log(request);
};

export const getReviewsByGame = (gameId: number) => {
  return [
    {
      id: 1,
      title: 'title',
      description:
        "There is a rising tide of fear in The City. Hatred saturates every stone and whilst the rich prosper, the less fortunate face misery and repression. Ravaged with sickness and famine, they wait for something to change. Into this shadowy world steps Garrett, THE master thief in Thief, a reinvention of a franchise that helped define an entire genre of games. This first-person adventure features intelligent design that allows players to take full control, with freedom to choose their path through the game's levels and how they approach and overcome each challenge",
      dateCreated: new Date(),
      gameId: gameId,
      userId: '1',
      user: {
        id: 'string',
        userName: 'string',
        avatarUrl: 'https://api.dicebear.com/6.x/lorelei/svg',
      } satisfies User,
      ratingId: 3,
    } satisfies Review,
    {
      id: 1,
      title: 'title',
      description: 'description',
      dateCreated: new Date(),
      gameId: gameId,
      userId: '1',
      user: {
        id: 'string',
        userName: 'string',
        avatarUrl: 'https://api.dicebear.com/6.x/lorelei/svg',
      } satisfies User,
      ratingId: 4,
    } satisfies Review,
    {
      id: 1,
      title: 'title',
      description: 'description',
      dateCreated: new Date(),
      gameId: gameId,
      userId: '1',
      user: {
        id: 'string',
        userName: 'string',
        avatarUrl: 'https://api.dicebear.com/6.x/lorelei/svg',
      } satisfies User,
      ratingId: 5,
    } satisfies Review,
    {
      id: 1,
      title: 'title',
      description: 'description',
      dateCreated: new Date(),
      gameId: gameId,
      userId: '1',
      user: {
        id: 'string',
        userName: 'string',
        avatarUrl: 'https://api.dicebear.com/6.x/lorelei/svg',
      } satisfies User,
      ratingId: 5,
    } satisfies Review,
  ];
};
