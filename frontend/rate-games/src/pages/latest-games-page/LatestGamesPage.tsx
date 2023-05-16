import { GameList } from '@/components/lists/game-list';
import { Loader } from '@/components/loader';
import { ErrorResult } from '@/components/results/error-result';
import { usePagedFetch } from '@/hooks/use-paged-fetch';
import { getLatestGames } from '@/services/game-service';
import { Button, Center, Text } from '@chakra-ui/react';
import { useState } from 'react';

const limit = 20;
export const LatestGamesPage = () => {
  const [page, setPage] = useState(0);
  const {
    data: games,
    hasMore,
    isLoading,
    error,
  } = usePagedFetch(
    async (limit, offset, signal) => {
      return await getLatestGames(signal, limit, offset);
    },
    page,
    limit
  );

  return (
    <Center flexWrap='wrap' flexDirection='column'>
      {!games ? null : (
        <GameList
          flexWrap='wrap'
          games={games}
          justifyContent='space-evenly'
          p='6'
        />
      )}
      <Center flexDirection='column' mb='10'>
        {isLoading ? (
          <Center h={!games ? '80vh' : '10vh'}>
            <Loader />
          </Center>
        ) : !games && error ? (
          <ErrorResult />
        ) : hasMore ? (
          <Button onClick={(_) => setPage((p) => p + 1)}>Load more...</Button>
        ) : (
          <Text fontSize='2xl'>That&apos;s all</Text>
        )}
      </Center>
    </Center>
  );
};
