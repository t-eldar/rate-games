import {
  theme as baseTheme,
  extendTheme,
  withDefaultColorScheme,
} from '@chakra-ui/react';
import { buttonTheme } from './components/button';
import { menuTheme } from './components/menu';
import { inputTheme } from './components/input';
import { checkboxTheme } from './components/checkbox';

const defaultTheme = extendTheme({
  shadows: { outline: '0 0 0 2px var(--chakra-colors-green-500)' },
  colors: {
    major: {
      100: '#B794F4',
      200: '#A482DA',
      300: '#916FBF',
      400: '#7E5DA5',
      500: '#6B4A8B',
      600: '#583870',
      700: '#452556',
      800: '#32133B',
      900: '#1F0021',
    },
    minor: baseTheme.colors.green,
  },
  components: {
    Button: buttonTheme,
    Menu: menuTheme,
    Input: inputTheme,
    Checkbox: checkboxTheme,
  },
});

export const theme = extendTheme(
  withDefaultColorScheme({
    colorScheme: 'major',
    components: ['Flex', 'Box'],
  }),
  defaultTheme
);
