import { ReactComponent as LogoSvg } from '@/images/logo.svg';
import {
  useColorModeValue,
  useToken
} from '@chakra-ui/react';

type LogoProps = React.ComponentProps<typeof LogoSvg>;

export const Logo = (props: LogoProps) => {
  const [minor300, minor700] = useToken('colors', ['minor.300', 'minor.700']);
  const color = useColorModeValue(minor700, minor300);
  
  return <LogoSvg fill={color} {...props} />;
};
