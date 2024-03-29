import { range } from '@/utils/collections';
import { Box, BoxProps, Icon, useColorModeValue } from '@chakra-ui/react';
import { useState } from 'react';
import { FaRegStar, FaStar } from 'react-icons/fa';

type AddRatingProps = BoxProps & {
  onSelectRating: (value: number) => void;
  count?: number;
  initial?: number;
};
export const AddRating = ({
  onSelectRating,
  count = 5,
  initial,
  ...rest
}: AddRatingProps) => {
  const [clicked, setClicked] = useState<number | undefined>(
    typeof initial !== 'undefined' ? initial - 1 : initial
  );
  const [areIconsFilled, setAreIconsFilled] = useState<boolean[]>(() =>
    range(0, count).map((_, i) => {
      if (!initial) {
        return false;
      }

      return initial > i;
    })
  );

  const color = useColorModeValue('minor.600', 'minor.400');
  return (
    <Box {...rest}>
      {range(1, count).map((value, index) => (
        <Icon
          key={index}
          boxSize={9}
          cursor='pointer'
          color={color}
          aria-label='add rating'
          onClick={() => {
            if (clicked === index) {
              setClicked(undefined);
            } else {
              setClicked(index);
            }
            onSelectRating(value);
          }}
          onMouseEnter={() => {
            if (clicked === index) {
              setAreIconsFilled([...range(0, count).map((_) => false)]);
            } else {
              const filledIcons = range(0, index + 1).map((_) => true);
              const emptyIcons = range(0, count - index - 1).map((_) => false);
              setAreIconsFilled([...filledIcons, ...emptyIcons]);
            }
          }}
          onMouseLeave={() => {
            if (typeof clicked === 'undefined') {
              setAreIconsFilled([...range(0, count).map((_) => false)]);
            } else {
              const filledIcons = range(0, clicked + 1).map((_) => true);
              const emptyIcons = range(0, count - clicked - 1).map(
                (_) => false
              );
              setAreIconsFilled([...filledIcons, ...emptyIcons]);
            }
          }}
        >
          {!areIconsFilled[index] ? <FaRegStar /> : <FaStar />}
        </Icon>
      ))}
    </Box>
  );
};
