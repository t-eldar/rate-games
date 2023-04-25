import {
  Button,
  ButtonProps,
  useColorMode,
  useColorModeValue,
} from '@chakra-ui/react';
import { FiMoon, FiSun } from 'react-icons/fi';

export const ThemeSwitcher = (props: ButtonProps) => {
  const { colorMode, toggleColorMode } = useColorMode();
  return (
    <Button
      aria-label='Toggle Color Mode'
      onClick={toggleColorMode}
      _focus={{ boxShadow: 'none' }}
      w='fit-content'
      {...props}
    >
      {colorMode === 'light' ? <FiMoon /> : <FiSun />}
    </Button>
  );
};
