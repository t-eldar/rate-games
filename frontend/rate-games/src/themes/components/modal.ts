import { modalAnatomy } from '@chakra-ui/anatomy';
import {
  StyleFunctionProps,
  createMultiStyleConfigHelpers,
} from '@chakra-ui/react';
import { mode } from '@chakra-ui/theme-tools';

const { defineMultiStyleConfig } = createMultiStyleConfigHelpers(
  modalAnatomy.keys
);

const variants = {
  primary: (props: StyleFunctionProps) => ({
    dialog: {
      colorScheme: 'major',
      bg: mode('major.200', 'major.800')(props),
      borderColor: mode('minor.300', 'minor.800')(props),
      py: '4',
      boxShadow: 'md',
      borderRadius: 'xl',
    },
  }),
};

export const modalTheme = defineMultiStyleConfig({
  variants,
  defaultProps: { variant: 'primary' },
});
