import { SignInForm } from '@/components/forms/sign-in';
import { SignUpForm } from '@/components/forms/sign-up-form';
import { Layout } from '@/components/layout';
import { UserContext } from '@/context/user-context';
import { GamesPage } from '@/pages/games-page';
import { theme } from '@/themes';
import { User } from '@/types/authentication';
import { ChakraProvider } from '@chakra-ui/react';
import { useState } from 'react';
import {
  Route,
  RouterProvider,
  createBrowserRouter,
  createRoutesFromElements,
} from 'react-router-dom';

export const App = () => {
  const [user, setUser] = useState<User | null>(null);
  const router = createBrowserRouter(
    createRoutesFromElements(
      <>
        <Route path='/' element={<Layout />}>
          <Route path='/sign-in' element={<SignInForm />} />
          <Route path='/sign-up' element={<SignUpForm />} />
          <Route path='/games' element={<GamesPage />} />
        </Route>
        <Route
          path='/test'
          element={<SignInForm onSuccess={() => alert('ffffffffffffffff')} />}
        />
      </>
    )
  );
  return (
    <>
      <ChakraProvider theme={theme}>
        <UserContext.Provider value={{ user, setUser }}>
          <RouterProvider router={router} />
        </UserContext.Provider>
      </ChakraProvider>
    </>
  );
};
