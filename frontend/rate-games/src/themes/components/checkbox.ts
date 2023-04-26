import { checkboxAnatomy } from '@chakra-ui/anatomy';
import {
  StyleFunctionProps,
  createMultiStyleConfigHelpers,
} from '@chakra-ui/react';
import { mode } from '@chakra-ui/theme-tools';

const { defineMultiStyleConfig } = createMultiStyleConfigHelpers(
  checkboxAnatomy.keys
);

const variants = {
  primary: (props: StyleFunctionProps) => ({
    control: {
      colorScheme: 'minor',
      _checked: {
        _hover: {
          border: 'none',
          bg: mode('minor.600', 'minor.500')(props),
        },
        border: 'none',
        bg: mode('minor.600', 'minor.500')(props),
      },

      _focus: {
        border: '2px solid',
        borderColor: mode('minor.100', 'minor.700')(props),
      },
    },
  }),
};

export const checkboxTheme = defineMultiStyleConfig({
  variants,
  defaultProps: { variant: 'primary' },
});
