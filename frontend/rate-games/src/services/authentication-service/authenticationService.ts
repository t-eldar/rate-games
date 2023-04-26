import { AuthenticatedUser } from '@/types/authentication';
import type { SignInRequest, SignUpRequest } from '@/types/requests';

const baseURL = 'https://localhost:7179';

export const signIn = async (request: SignInRequest) =>
  await fetch(`${baseURL}/sign-in`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify(request),
  });

export const signUp = async (request: SignUpRequest) =>
  await fetch(`${baseURL}/sign-up`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify(request),
  });

export const signOut = async () =>
  await fetch(`${baseURL}/sign-out`, {
    method: 'GET',
    credentials: 'include',
  });

export const getUserInfo = async () => {
  const response = await fetch(`${baseURL}/user-info`, {
    method: 'GET',
    credentials: 'include',
  });
  const data = await response.json();
  if (!isAuthenticatedUser(data)) {
    throw new Error('Data has incorrect type')
  }
  return data;
};

const isAuthenticatedUser = (data: unknown): data is AuthenticatedUser => {
  if (
    data &&
    typeof data === 'object' &&
    'id' in data &&
    typeof data.id === 'string' &&
    'userName' in data &&
    typeof data.userName === 'string'
  ) {
    return true;
  }
  return false;
};
