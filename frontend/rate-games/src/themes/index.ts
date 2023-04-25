import {
  extendTheme,
  theme as baseTheme,
  StyleFunctionProps,
  withDefaultColorScheme,
} from '@chakra-ui/react';
import { buttonTheme } from './components/button';
import { menuTheme } from './components/menu';
import { mode } from '@chakra-ui/theme-tools';

const defaultTheme = extendTheme({
  colors: {
    main: baseTheme.colors.purple,
  },
  components: {
    Button: buttonTheme,
    Menu: menuTheme,
  },
});

export const theme = extendTheme(
  defaultTheme
);
