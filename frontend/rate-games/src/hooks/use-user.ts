import { useUserContext } from '@/context/user-context';
import { useLocalStorage } from '@/hooks/use-local-storage';
import { User } from '@/types/authentication';

export const useUser = () => {
  const context = useUserContext();
  if (!context) {
    return { error: 'UserContext has no value' };
  }

  const { user, setUser } = context;
  const { setItem } = useLocalStorage();

  const addUser = (user: User) => {
    setUser(user);
    setItem('user', JSON.stringify(user));
  };

  const removeUser = () => {
    setUser(null);
    setItem('user', '');
  };

  return { user, addUser, removeUser };
};
