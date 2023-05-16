import { GameList } from '@/components/lists/game-list';
import { Loader } from '@/components/loader';
import { ErrorResult } from '@/components/results/error-result';
import { NotFoundResult } from '@/components/results/not-found-result';
import { useFetch } from '@/hooks/use-fetch';
import { getLatestGames } from '@/services/game-service';
import { Center } from '@chakra-ui/react';

export const LatestGamesPage = () => {
  const {
    data: games,
    isLoading,
    error,
  } = useFetch(
    async (abortSignal: AbortSignal) => await getLatestGames(abortSignal)
  );
  return (
    <Center flexWrap='wrap' justifyContent='space-evenly' p='6'>
      {isLoading ? (
        <Center h='80vh'>
          <Loader />
        </Center>
      ) : error ? (
        <ErrorResult />
      ) : !games ? (
        <NotFoundResult />
      ) : (
        <GameList 
          games={games}
          flexWrap='wrap'
          justifyContent='space-evenly'
          p='6'
        />
      )} 
    </Center>
  );
};
