import { menuAnatomy } from '@chakra-ui/anatomy';
import {
  StyleFunctionProps,
  createMultiStyleConfigHelpers,
} from '@chakra-ui/react';
import { mode } from '@chakra-ui/theme-tools';

const { defineMultiStyleConfig } = createMultiStyleConfigHelpers(
  menuAnatomy.keys
);

const defaultColors = (props: StyleFunctionProps) => ({
  colorScheme: 'major',
  bg: mode('major.200', 'major.800')(props),
  color: mode('major.900', 'major.200')(props),
});

const variants = {
  primary: (props: StyleFunctionProps) => ({
    button: {
      ...defaultColors(props),
      px: 2,
      borderRadius: 'md',
      _hover: {
        bg: mode('major.300', 'major.700')(props),
      },
    },
    list: {
      // this will style the MenuList component
      ...defaultColors(props),
      borderColor: mode('minor.300', 'minor.800')(props),
      py: '4',
      boxShadow: 'md',
      borderRadius: 'xl',
    },
    item: {
      ...defaultColors(props),
      _hover: {
        bg: mode('major.400', 'major.700')(props),
        color: "white",
        fontWeight: "semibold"
      },
    },
    comand: {
      _hover: {
        fontWeight: 'lg',
      },
    },
    divider: {
      colorScheme: 'minor',
    },
  }),
};

export const menuTheme = defineMultiStyleConfig({
  variants,
  defaultProps: { variant: 'primary' },
});
