import { GameCard } from '@/components/game-card';
import { useFetch } from '@/hooks/use-fetch';
import { getLatestGames, getMockedGames } from '@/services/game-service';
import { Flex } from '@chakra-ui/react';

export const GamesPage = () => {
  const {
    data: games,
    isLoading,
    error,
  } = useFetch(async () => await getLatestGames());
  return (
    <>
      {!games || error ? (
        <>
          {error && typeof error === 'object' && 'message' in error
            ? error.message
            : '[z'}
        </>
      ) : (
        <Flex flexWrap='wrap' justifyContent='space-evenly' p='6'>
          {games.map((g) => (
            <GameCard my='3' key={g.id} game={g} />
          ))}
        </Flex>
      )}
    </>
  );
};
