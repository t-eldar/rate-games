import { GameCard } from '@/components/game-card';
import { MinGameInfo } from '@/types/entities';
import { Flex, FlexProps } from '@chakra-ui/react';

type GameListProps = FlexProps & {
  games: MinGameInfo[];
};

export const GameList = ({ games, ...rest }: GameListProps) => {
  return (
    <Flex {...rest}>
      {games.map((g) => (
        <GameCard
          flexGrow='1'
          maxH='lg'
          maxW='xl'
          minW='3xs'
          my='3'
          key={g.id}
          game={g}
        />
      ))}
    </Flex>
  );
};
