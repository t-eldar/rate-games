import { User } from '@/types/authentication';

const baseURL = 'https://localhost:7082';

export type SignInRequest = {
  usernameOrEmail: string;
  password: string;
  rememberMe: boolean;
};

export const signIn = async (
  request: SignInRequest,
  abortSignal?: AbortSignal
) =>
  await fetch(`${baseURL}/sign-in`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify(request),
    signal: abortSignal,
  });

export type SignUpRequest = {
  username: string;
  email: string;
  password: string;
  avatarUrl: string;
};
export const signUp = async (
  request: SignUpRequest,
  abortSignal?: AbortSignal
) =>
  await fetch(`${baseURL}/sign-up`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify(request),
    signal: abortSignal,
  });

export const signOut = async (abortSignal?: AbortSignal) =>
  await fetch(`${baseURL}/sign-out`, {
    method: 'GET',
    credentials: 'include',
    signal: abortSignal,
  });

export const getUserInfo = async (abortSignal?: AbortSignal) => {
  const response = await fetch(`${baseURL}/user-info`, {
    method: 'GET',
    credentials: 'include',
    signal: abortSignal,
  });
  const data = await response.json();
  if (!isUser(data)) {
    throw new Error('Data has incorrect type');
  }
  return data;
};

const isUser = (data: unknown): data is User => {
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
