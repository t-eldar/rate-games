import { inputAnatomy } from '@chakra-ui/anatomy';
import {
  StyleFunctionProps,
  createMultiStyleConfigHelpers,
} from '@chakra-ui/react';
import { mode } from '@chakra-ui/theme-tools';

const { defineMultiStyleConfig } = createMultiStyleConfigHelpers(
  inputAnatomy.keys
);

const defaultColors = (props: StyleFunctionProps) => ({
  colorScheme: 'major',
  bg: mode('major.100', 'major.700')(props),
  color: mode('major.900', 'major.200')(props),
});

const variants = {
  primary: (props: StyleFunctionProps) => ({
    field: {
      ...defaultColors(props),
      border: '1px solid',
      borderColor: mode('major.300', 'major.600')(props),
      px: 2,
      borderRadius: 'md',
      _hover: {
        border: '2px solid',
        borderColor: mode('minor.100', 'minor.700')(props),
      },
      _focus: {
        border: '2px solid',
        borderColor: mode('minor.100', 'minor.700')(props),
        boxShadow: "xl",
      },
      _disabled: {
        opacity: 0.4,
        cursor: 'not-allowed',
      },
      _invalid: {
        border: '2px solid',
        borderColor: mode('red.400', 'red.800')(props),
      },
    },
  }),
};

export const inputTheme = defineMultiStyleConfig({
  variants,
  defaultProps: { variant: 'primary' },
  
});
