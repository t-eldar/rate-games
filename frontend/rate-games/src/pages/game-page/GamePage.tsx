import { ReviewForm } from '@/components/forms/review-form';
import { GenreList } from '@/components/lists/genre-list';
import { PlatformList } from '@/components/lists/platform-list';
import { NukaCarousel } from '@/components/nuka-carousel';
import { useFetch } from '@/hooks/use-fetch';
import { getGameById } from '@/services/game-service';
import {
  Box,
  Flex,
  Heading,
  Image,
  Stack,
  Text,
  useColorModeValue,
} from '@chakra-ui/react';
import { useParams } from 'react-router-dom';

export const GamePage = () => {
  const params = useParams();
  const id = Number(params.gameId);

  const { data: game } = useFetch(async () => {
    return await getGameById(id);
  });

  if (!game) {
    return <></>;
  }
  return (
    <Stack>
      <Flex
        p='10'
        w='100%'
        direction={{ base: 'column', md: 'row' }}
        justifyContent='space-between'
      >
        <Box w='2xl'>
          <Heading>{game.name}</Heading>
          {!game.screenshots ? (
            <Text>No screenshots</Text>
          ) : (
            <NukaCarousel mb='4'>
              {game.screenshots.map((s) => (
                <Box
                  h='lg'
                  key={s.id}
                  display='flex'
                  justifyContent='center'
                  alignItems='center'
                >
                  <Image
                    w='xl'
                    borderRadius='lg'
                    draggable='false'
                    src={s.url}
                  />
                </Box>
              ))}
            </NukaCarousel>
          )}
          <Box
            p='4'
            rounded='xl'
            bg={useColorModeValue('major.200', 'major.800')}
          >
            <Text>{game.summary}</Text>
          </Box>
          <ReviewForm
            gameId={game.id}
            p='4'
            rounded='xl'
            bg={useColorModeValue('major.200', 'major.800')}
          />
        </Box>
        <Box
          display='flex'
          flexDirection='column'
          justifyItems='center'
          alignItems='center'
        >
          <Image rounded='lg' w='md' src={game.cover.url} />
          <Box
            w='md'
            p='4'
            rounded='xl'
            bg={useColorModeValue('major.200', 'major.800')}
            mt='3'
          >
            {!game.genres ? null : (
              <>
                <Text>Genres: </Text>
                <GenreList flexWrap='wrap' genres={game.genres} />
              </>
            )}
            {!game.platforms ? null : (
              <>
                <Text>Available on: </Text>
                <PlatformList
                  w='md'
                  flexWrap='wrap'
                  platforms={game.platforms}
                />
              </>
            )}
            {!game.firstReleaseDate ? null : (
              <Text>Released: {game.firstReleaseDate.toDateString()}</Text>
            )}
          </Box>
        </Box>
      </Flex>
    </Stack>
  );
};
