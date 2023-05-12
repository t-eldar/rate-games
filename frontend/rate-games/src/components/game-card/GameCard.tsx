import { useState } from 'react';

import { GenreList } from '@/components/lists/genre-list';
import { PlatformList } from '@/components/lists/platform-list';
import { RatingMark } from '@/components/rating-mark';
import { MinGameInfo } from '@/types/entities';
import {
  Box,
  Card,
  CardBody,
  CardProps,
  Heading,
  Image,
  SlideFade,
  Stack,
  Text,
  useColorModeValue,
} from '@chakra-ui/react';
import { Link as RouterLink } from 'react-router-dom';

type GameCardProps = CardProps & {
  game: MinGameInfo;
};

export const GameCard = ({ game, ...rest }: GameCardProps) => {
  const [isRatingShown, setIsRatingShown] = useState(false);
  return (
    <Card
      onMouseEnter={() => setIsRatingShown(true)}
      onMouseLeave={() => setIsRatingShown(false)}
      bg={useColorModeValue('major.200', 'major.800')}
      border='2px solid'
      borderColor={useColorModeValue('major.200', 'major.800')}
      rounded='xl'
      _hover={{
        border: '2px solid',
        boxShadow: 'xl',
        borderColor: useColorModeValue('minor.500', 'minor.400'),
      }}
      as={RouterLink}
      to={`/games/${game.id}`}
      {...rest}
    >
      <CardBody display='flex' flexDirection={{ base: 'column', sm: 'row' }}>
        <SlideFade
          in={isRatingShown}
          offsetY='20px'
          style={{ position: 'absolute', right: '20px' }}
        >
          <RatingMark value={game.aggregatedRating} />
        </SlideFade>
        <Image h='2xs' src={game.cover.url} alt={game.name} borderRadius='lg' />
        <Stack
          maxW={{ base: '3xs', lg: 'xs' }}
          mt='3'
          px='3'
          justifyContent='space-between'
        >
          <Stack>
            <Heading size='md'>{game.name}</Heading>
            <Text>{game.firstReleaseDate?.getFullYear().toString()}</Text>
          </Stack>
          <Text noOfLines={{ base: 1, sm: 3 }}>{game.summary}</Text>
          <Box h='fit-content'>
            {!game.genres ? null : (
              <GenreList
                css={{ '&::-webkit-scrollbar': { display: 'none' } }}
                overflow='auto'
                genres={game.genres}
              />
            )}
            {!game.platforms ? null : (
              <PlatformList
                css={{ '&::-webkit-scrollbar': { display: 'none' } }}
                overflow='auto'
                platforms={game.platforms}
              />
            )}
          </Box>
        </Stack>
      </CardBody>
    </Card>
  );
};
