import { defineStyleConfig } from '@chakra-ui/react';
import type { StyleFunctionProps } from '@chakra-ui/styled-system';
import { mode } from '@chakra-ui/theme-tools';
export const buttonTheme = defineStyleConfig({
  variants: {
    primary: (props: StyleFunctionProps) => ({
      colorScheme: 'main',
      bg: mode('main.200', 'main.900')(props),
      color: mode('main.900', 'main.200')(props),
      _hover: {
        bg: mode('main.300', 'main.800')(props),
      },
    }),
  },
  defaultProps: {
    variant: 'primary',
  },
});
