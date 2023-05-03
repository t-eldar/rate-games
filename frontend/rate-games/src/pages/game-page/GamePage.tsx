import { NukaCarousel } from '@/components/nuka-carousel';
import { getMockedGame } from '@/services/game-service';
import { Box, Image } from '@chakra-ui/react';

export const GamePage = () => {
  const game = getMockedGame();

  return (
    <Box p='3' w='100%'>
      <NukaCarousel>
        {game.screenshots.map((s) => (
          <Box
            h='lg'
            key={s.id}
            display='flex'
            justifyContent='center'
            alignItems='center'
          >
            <Image h='md' borderRadius='lg' draggable='false' src={s.url} />
          </Box>
        ))}
      </NukaCarousel>
    </Box>
  );
};
