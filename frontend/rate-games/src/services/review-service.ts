import { Review } from '@/types/entities';
import { isReview, isReviews } from '@/utils/assertion';
import { dateReviver } from '@/utils/serialization';
import { combineURLs } from '@/utils/url';

const baseURL = 'https://localhost:7082/reviews';

export type CreateReviewRequest = {
  title: string;
  description: string;
  rating: number;
  gameId: number;
};
export const createReview = async (
  request: CreateReviewRequest
): Promise<Response> => {
  const response = await fetch(baseURL, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify({ ...request }),
  });

  return response;
};

export const deleteReview = async (id: number): Promise<Response> => {
  const url = new URL(combineURLs(baseURL, `/${id}`));
  const response = await fetch(url, {
    method: 'DELETE',
    credentials: 'include',
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
  request: UpdateReviewRequest
): Promise<Response> => {
  const url = new URL(combineURLs(baseURL, `/${request.id}`));

  const response = await fetch(url, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify({ ...request }),
  });

  return response;
};

export const getReviewsByGame = async (gameId: number): Promise<Review[]> => {
  const url = new URL(combineURLs(baseURL, `by-game/${gameId}`));
  const response = await fetch(url, {
    method: 'GET',
    credentials: 'include',
  });
  const text = await response.text();
  const result = JSON.parse(text, dateReviver);
  if (!isReviews(result)) {
    throw new Error('Data is incorrect');
  }

  return result;
};

export const getReviewsByUser = async (userId: string): Promise<Review[]> => {
  const url = new URL(combineURLs(baseURL, `by-user/${userId}`));
  const response = await fetch(url, {
    method: 'GET',
    credentials: 'include',
  });
  const text = await response.text();
  const result = JSON.parse(text, dateReviver);
  if (!isReviews(result)) {
    throw new Error('Data is incorrect');
  }

  return result;
};

export const getReviewByUserAndGame = async (
  gameId: number
): Promise<Review> => {
  const url = new URL(combineURLs(baseURL, `by-user-and-game`));
  url.searchParams.append('gameId', gameId.toString());

  const response = await fetch(url, {
    method: 'GET',
    credentials: 'include',
  });
  const text = await response.text();
  const result = JSON.parse(text, dateReviver);
  if (!isReview(result)) {
    throw new Error('Data is incorrect');
  }

  return result;
};
