import { range } from '@/utils/collections';
import { Box, BoxProps, Icon, useColorModeValue } from '@chakra-ui/react';
import { FaRegStar, FaStar } from 'react-icons/fa';

type RatingProps = BoxProps & {
  value: number;
  count?: number;
};
export const Rating = ({ value, count = 5, ...rest }: RatingProps) => {
  const color = useColorModeValue('minor.600', 'minor.400');
  return (
    <Box {...rest}>
      {range(1, count).map((val, index) => (
        <Icon key={index} boxSize={9} color={color} aria-label='add rating'>
          {val > value ? <FaRegStar /> : <FaStar />}
        </Icon>
      ))}
    </Box>
  );
};
