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
import { useAwait } from '@/hooks/use-await';

type Status = 'success' | 'error';

export const useAuth = () => {
  const userData = useUser();
  if (userData.error || !userData.addUser || !userData.removeUser) {
    return { error: `useUser hook is invalid! Inner Error: ${userData.error}` };
  }
  const { user, addUser, removeUser } = userData;
  const { getItem } = useLocalStorage();

  const {
    promise: fetchLogin,
    isLoading: isLoginLoading,
    error: loginError,
  } = useAwait<typeof signIn>(signIn);
  const {
    promise: fetchRegister,
    isLoading: isRegisterLoading,
    error: registerError,
  } = useAwait<typeof signUp>(signUp);
  const {
    promise: fetchLogout,
    isLoading: isLogoutLoading,
    error: logoutError,
  } = useAwait<typeof signOut>(signOut);

  useEffect(() => {
    const user = getItem('user');
    console.log(user);
    
    if (user) {
      addUser(JSON.parse(user));
    }
  }, []);

  const login = {
    invoke: async (request: SignInRequest) => {
      let status: Status = 'error';
      const response = await fetchLogin(request);
      if (loginError || !response) {
        status = 'error';
      } else if (response.status === 200) {
        status = 'success';
        const userInfo = await getUserInfo();
        addUser(userInfo);
      }
      return status;
    },
    isLoading: isLoginLoading,
    error: loginError,
  };
  const register = {
    invoke: async (request: SignUpRequest) => {
      let status: Status = 'error';
      const response = await fetchRegister(request);
      if (registerError || !response) {
        status = 'error';
      } else if (response.status === 200) {
        status = 'success';
        const userInfo = await getUserInfo();
        addUser(userInfo);
      }
      return status;
    },
    isLoading: isRegisterLoading,
    error: registerError,
  };
  const logout = {
    invoke: async () => {
      let status: Status = 'error';
      const response = await fetchLogout();
      if (registerError || !response) {
        status = 'error';
      } else if (response.status === 200) {
        status = 'success';
        removeUser();
      }
      return status;
    },
    isLoading: isLogoutLoading,
    error: logoutError,
  };

  return { user, login, register, logout };
};
