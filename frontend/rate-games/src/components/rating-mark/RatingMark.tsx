import { Box, BoxProps } from '@chakra-ui/react';
import { motion } from 'framer-motion';

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
    <Box
      as={motion.div}
      initial={{ opacity: 0 }}
      whileInView={{ opacity: 1, scale: 1.1 }}
      rounded='md'
      p='2'
      bg={color}
      h='fit-content'
      {...rest}
    >
      {value}
    </Box>
  );
};
