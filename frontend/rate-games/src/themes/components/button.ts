import { defineStyleConfig } from '@chakra-ui/react';
import type { StyleFunctionProps } from '@chakra-ui/styled-system';
import { mode } from '@chakra-ui/theme-tools';
export const buttonTheme = defineStyleConfig({
  variants: {
    primary: (props: StyleFunctionProps) => ({
      colorScheme: 'major',
      bg: mode('major.200', 'major.800')(props),
      color: mode('major.900', 'major.200')(props),
      _hover: {
        bg: mode('major.300', 'major.700')(props),
      },
    }),
    secondary: (props: StyleFunctionProps) => ({
      colorScheme: 'minor',
      bg: mode('minor.600', 'minor.500')(props),
      color: mode('white', 'black')(props),
      _hover: {
        bg: mode('minor.700', 'minor.300')(props),
      },
    }),
  },
  defaultProps: {
    variant: 'primary',
  },
});
