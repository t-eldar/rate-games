import { Carousel } from '@/components/carousel';
import { getMockedGame } from '@/services/game-service';
import { Box, Image } from '@chakra-ui/react';

export const GamePage = () => {
  const game = getMockedGame();

  return (
    <Box p='3' w='100%'>
      <Carousel gap={4}>
        {game.screenshots.map((s) => (
          <Image borderRadius='lg' key={s.id} draggable='false' src={s.url} />
        ))}
      </Carousel>
    </Box>
  );
};
