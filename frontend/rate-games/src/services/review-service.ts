import { Review } from '@/types/entities';
import { isReview, isReviews } from '@/utils/assertion';
import { dateReviver } from '@/utils/serialization';
import { combineURLs } from '@/utils/url';

const baseURL = 'https://localhost:7082/reviews';
const LIMIT = 50;
const OFFSET = 0;
export type CreateReviewRequest = {
  title: string;
  description: string;
  rating: number;
  gameId: number;
};
export const createReview = async (
  request: CreateReviewRequest,
  abortSignal?: AbortSignal
): Promise<Response> => {
  const response = await fetch(baseURL, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify({ ...request }),
    signal: abortSignal,
  });

  return response;
};

export const deleteReview = async (
  id: number,
  abortSignal?: AbortSignal
): Promise<Response> => {
  const url = new URL(combineURLs(baseURL, `/${id}`));
  const response = await fetch(url, {
    method: 'DELETE',
    credentials: 'include',
    signal: abortSignal,
  });

  return response;
};

export type UpdateReviewRequest = {
  id: number;
  title?: string;
  description?: string;
  ratingValue: number;
};
export const updateReview = async (
  request: UpdateReviewRequest,
  abortSignal?: AbortSignal
): Promise<Response> => {
  const url = new URL(combineURLs(baseURL, `/${request.id}`));

  const response = await fetch(url, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify({ ...request }),
    signal: abortSignal,
  });

  return response;
};

export const getReviewsByGame = async (
  gameId: number,
  abortSignal?: AbortSignal,
  limit = LIMIT,
  offset = OFFSET
): Promise<Review[]> => {
  const url = new URL(combineURLs(baseURL, `by-game/${gameId}`));
  url.searchParams.append('limit', limit.toString());
  url.searchParams.append('offset', offset.toString());
  
  const response = await fetch(url, {
    method: 'GET',
    credentials: 'include',
    signal: abortSignal,
  });
  const text = await response.text();
  const result = JSON.parse(text, dateReviver);
  if (!isReviews(result)) {
    throw new Error('Data is incorrect');
  }

  return result;
};

export const getReviewsByUser = async (
  userId: string,
  abortSignal?: AbortSignal,
  limit = LIMIT,
  offset = OFFSET
): Promise<Review[]> => {
  const url = new URL(combineURLs(baseURL, `by-user/${userId}`));
  url.searchParams.append('limit', limit.toString());
  url.searchParams.append('offset', offset.toString());

  const response = await fetch(url, {
    method: 'GET',
    credentials: 'include',
    signal: abortSignal,
  });
  const text = await response.text();
  const result = JSON.parse(text, dateReviver);
  if (!isReviews(result)) {
    throw new Error('Data is incorrect');
  }

  return result;
};

export const getReviewByUserAndGame = async (
  gameId: number,
  abortSignal?: AbortSignal
): Promise<Review> => {
  const url = new URL(combineURLs(baseURL, `by-user-and-game`));
  url.searchParams.append('gameId', gameId.toString());

  const response = await fetch(url, {
    method: 'GET',
    credentials: 'include',
    signal: abortSignal,
  });
  const text = await response.text();
  const result = JSON.parse(text, dateReviver);
  if (!isReview(result)) {
    throw new Error('Data is incorrect');
  }

  return result;
};
