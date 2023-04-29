import { User } from '@/types/authentication';

const baseURL = 'https://localhost:7082';

export type SignInRequest = {
  usernameOrEmail: string;
  password: string;
  rememberMe: boolean;
};

export const signIn = async (request: SignInRequest) =>
  await fetch(`${baseURL}/sign-in`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify(request),
  });

export type SignUpRequest = {
  name: string;
  surname: string;
  email: string;
  password: string;
};
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
