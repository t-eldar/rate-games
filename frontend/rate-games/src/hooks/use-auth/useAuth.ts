import { useLocalStorage } from '@/hooks/use-local-storage';
import { useUser } from '@/hooks/use-user';
import {
  SignInRequest,
  SignUpRequest,
  getUserInfo,
  signIn,
  signOut,
  signUp,
} from '@/services/authentication-service';
import { useEffect } from 'react';

export const useAuth = () => {
  const userData = useUser();
  if (userData.error || !userData.addUser || !userData.removeUser) {
    return { error: `useUser hook is invalid! Inner Error: ${userData.error}` };
  }
  const { user, addUser, removeUser } = userData;
  const { getItem } = useLocalStorage();

  useEffect(() => {
    const user = getItem('user');
    if (user) {
      addUser(JSON.parse(user));
    }
  }, []);

  const login = async (request: SignInRequest) => {
    const response = await signIn(request);
    if (response.status === 200) {
      const userInfo = await getUserInfo();
      addUser(userInfo);
    }
    return response;
  };
  const register = async (request: SignUpRequest) => {
    const response = await signUp(request);
    if (response.status === 200) {
      const userInfo = await getUserInfo();
      addUser(userInfo);
    }
    return response;
  };

  const logout = async () => {
    const response = await signOut();
    if (response.status === 200) {
      removeUser();
    }
    return response;
  };

  return { user, login, register, logout };
};
