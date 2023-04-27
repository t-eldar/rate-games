import {
  createBrowserRouter,
  RouterProvider,
  Route,
  createRoutesFromElements,
} from 'react-router-dom';
import { ChakraProvider } from '@chakra-ui/react';
import { theme } from '@/themes';
import { Layout } from '@/components/layout';
import { SignInForm } from '@/components/forms/sign-in';
import { SignUpForm } from '@/components/forms/sign-up-form';
import {GamesPage} from '@/pages/games-page';

export const App = () => {
  const router = createBrowserRouter(
    createRoutesFromElements(
      <>
        <Route path='/' element={<Layout />}>
          <Route path='/sign-in' element={<SignInForm />} />
          <Route path='/sign-up' element={<SignUpForm />} />
          <Route path='/games' element={<GamesPage />} />
        </Route>
        <Route path='/test' element={<SignInForm />} />
      </>
    )
  );
  return (
    <>
      <ChakraProvider theme={theme}>
        <RouterProvider router={router} />
      </ChakraProvider>
    </>
  );
};
