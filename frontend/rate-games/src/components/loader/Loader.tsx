import { Spinner, SpinnerProps, useColorModeValue } from '@chakra-ui/react';

export const Loader = (props: SpinnerProps) => {
  const color = useColorModeValue('minor.600', 'minor.400');
  return <Spinner color={color} size='xl' {...props} />;
};
