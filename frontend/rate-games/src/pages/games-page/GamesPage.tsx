import { GameCard } from '@/components/game-card';
import { useFetch } from '@/hooks/use-fetch';
import { getLatestGames } from '@/services/game-service';
import { Flex, Spinner } from '@chakra-ui/react';

export const GamesPage = () => {
  const {
    data: games,
    isLoading,
    error,
  } = useFetch(async () => await getLatestGames());
  return (
    <Flex flexWrap='wrap' justifyContent='space-evenly' p='6'>
      {isLoading ? (
        <Spinner size='xl' color='minor.500' />
      ) : error || !games ? (
        <></>
      ) : (
        games.map((g) => (
          <GameCard
            flexGrow='1'
            maxH='lg'
            maxW='xl'
            minW='3xs'
            my='3'
            key={g.id}
            game={g}
          />
        ))
      )}
    </Flex>
  );
};
