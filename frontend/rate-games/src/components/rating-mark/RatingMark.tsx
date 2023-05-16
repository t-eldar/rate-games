import { Box, BoxProps } from '@chakra-ui/react';

type RatingMarkProps = BoxProps & {
  value: number | undefined;
};

export const RatingMark = ({ value, ...rest }: RatingMarkProps) => {
  if (!value) {
    return null;
  }
  const color =
    value > 75 ? 'green.500' : value > 50 ? 'yellow.500' : 'red.500';
  return (
    <Box rounded='md' p='2' bg={color} h='fit-content' {...rest}>
      {value.toFixed(0)}
    </Box>
  );
};
