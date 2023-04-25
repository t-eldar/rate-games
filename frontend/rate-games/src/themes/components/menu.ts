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
  colorScheme: 'main',
  bg: mode('main.200', 'main.900')(props),
  color: mode('main.900', 'main.200')(props),
});

const variants = {
  primary: (props: StyleFunctionProps) => ({
    button: {
      ...defaultColors(props),
      px: 2,
      borderRadius: 'md',
      _hover: {
        bg: mode('main.300', 'main.800')(props),
      },
    },
    list: {
      // this will style the MenuList component
      ...defaultColors(props),
      borderColor: mode('main.300', 'main.800')(props),
      py: '4',
      boxShadow: 'md',
      borderRadius: 'xl',
    },
    item: {
      ...defaultColors(props),
      _hover: {
        bg: mode('main.400', 'main.700')(props),
        color: mode('main.100', 'main.900')(props),
        fontWeight: "semibold"
      },
    },
    comand: {
      _hover: {
        fontWeight: 'lg',
      },
    },
    divider: {
      colorScheme: 'main',
    },
  }),
};

export const menuTheme = defineMultiStyleConfig({
  variants,
  defaultProps: { variant: 'primary' },
});
