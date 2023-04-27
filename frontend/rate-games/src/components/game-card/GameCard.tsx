import { useState } from 'react';

import {
  Game,
  Genre,
  Image as ImageModel,
  Platform,
} from '@/types/igdb-models';
import {
  Box,
  Card,
  CardBody,
  CardFooter,
  CardProps,
  Divider,
  Flex,
  FlexProps,
  HStack,
  Heading,
  Image,
  Stack,
  Text,
  VStack,
  useColorModeValue,
} from '@chakra-ui/react';
import { RatingMark } from '@/components/rating-mark';
import { motion } from 'framer-motion';

type GameCardProps = CardProps & {
  game: Game;
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
      maxH='lg'
      maxW='xl'
      minW='sm'
      _hover={{
        border: '2px solid',
        boxShadow: 'xl',
        borderColor: useColorModeValue('minor.500', 'minor.400'),
      }}
      {...rest}
    >
      <CardBody as={Flex} direction={{ base: 'column', sm: 'row' }}>
        <RatingMark
          transition='1s ease'
          display={isRatingShown ? 'block' : 'none'}
          position='absolute'
          right='20px'
          value={game.aggregatedRating}
        />
        <Image
          h='2xs'
          src={(game.cover as ImageModel).url}
          alt={game.name}
          borderRadius='lg'
        />
        <Stack maxW='xs' mt='3' px='3' justifyContent='space-between'>
          <Stack>
            <Heading size='md'>{game.name}</Heading>
            <Text>{new Date(game.firstReleaseDate! * 1000).getFullYear()}</Text>
          </Stack>
          <Text noOfLines={{ base: 1, sm: 3 }}>{game.summary}</Text>
          <Flex h='fit-content' display='column'>
            <GenreList genres={game.genres as Genre[]} />
            <PlatformList platforms={game.platforms as Platform[]} />
          </Flex>
        </Stack>
      </CardBody>
    </Card>
  );
};

type GenreListProps = FlexProps & {
  genres: Genre[];
};
const GenreList = ({ genres, ...rest }: GenreListProps) => {
  return (
    <Flex
      css={{ '&::-webkit-scrollbar': { display: 'none' } }}
      overflow='auto'
      {...rest}
    >
      {genres.map((g) => (
        <Box
          h='fit-content'
          display='flex'
          justifyContent='center'
          alignItems='center'
          bg={useColorModeValue('major.300', 'major.700')}
          key={g.id}
          rounded='md'
          m='1'
          px='2'
        >
          <Text>{g.name}</Text>
        </Box>
      ))}
    </Flex>
  );
};

type PlatformListProps = FlexProps & {
  platforms: Platform[];
};

const PlatformList = ({ platforms, ...rest }: PlatformListProps) => {
  return (
    <Flex
      css={{ '&::-webkit-scrollbar': { display: 'none' } }}
      overflow='auto'
      {...rest}
    >
      {platforms.map((p) => (
        <Box
          display='flex'
          justifyContent='center'
          alignItems='center'
          bg={useColorModeValue('major.300', 'major.700')}
          key={p.id}
          rounded='md'
          m='1'
          px='2'
        >
          <Text>{p.name}</Text>
        </Box>
      ))}
    </Flex>
  );
};
