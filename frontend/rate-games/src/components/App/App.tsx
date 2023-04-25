import {
  createBrowserRouter,
  RouterProvider,
  Route,
  createRoutesFromElements,
} from 'react-router-dom';
import { Layout } from '@/components/layout';
import { ChakraProvider } from '@chakra-ui/react';
import { theme } from '@/themes';

export const App = () => {
  const router = createBrowserRouter(
    createRoutesFromElements(<Route path='/' element={<Layout />}></Route>)
  );
  return (
    <>
      <ChakraProvider theme={theme}>
        <RouterProvider router={router} />
      </ChakraProvider>
    </>
  );
};
