import { Genre } from '@/types/igdb-models';
import {
  Box,
  Flex,
  FlexProps,
  Text,
  useColorModeValue,
} from '@chakra-ui/react';

type GenreListProps = FlexProps & {
  genres: Genre[];
};

export const GenreList = ({ genres, ...rest }: GenreListProps) => {
  const color = useColorModeValue('major.300', 'major.700');
  return (
    <Flex {...rest}>
      {genres.map((g) => (
        <Box
          h='fit-content'
          display='flex'
          justifyContent='center'
          alignItems='center'
          bg={color}
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
