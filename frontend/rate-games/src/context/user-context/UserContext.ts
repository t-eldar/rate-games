import { User } from '@/types/authentication';
import { createContext, useContext } from 'react';

type UserContext = {
  user: User | null;
  setUser: (user: User | null) => void;
};

export const UserContext = createContext<UserContext | null>(null);

export const useUserContext = () => useContext(UserContext);
