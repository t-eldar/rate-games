import { GameCard } from '@/components/game-card';
import { getMockedGames } from '@/services/game-service';
import { Flex } from '@chakra-ui/react';

export const GamesPage = () => {
  const games = getMockedGames();
  return (
    <Flex flexWrap='wrap' justifyContent='space-between' p='6'>
      {games.map((g) => (
        <GameCard my='3' key={g.id} game={g} />
      ))}
    </Flex>
  );
};
