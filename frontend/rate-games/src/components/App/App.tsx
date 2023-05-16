import { Layout } from '@/components/layout';
import { UserContext } from '@/context/user-context';
import { GamePage } from '@/pages/game-page';
import HomePage from '@/pages/home-page';
import { LatestGamesPage } from '@/pages/latest-games-page';
import SearchPage from '@/pages/search-page';
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
          <Route path='/' element={<HomePage />} />
          <Route path='/latest' element={<LatestGamesPage />} />
          <Route path='/games/:gameId' element={<GamePage />} />
          <Route path='/search' element={<SearchPage />} />
        </Route>
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
