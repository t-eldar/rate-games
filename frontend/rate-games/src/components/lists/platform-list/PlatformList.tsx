import { Platform } from '@/types/igdb-models';
import {
  Box,
  Flex,
  FlexProps,
  Text,
  useColorModeValue,
} from '@chakra-ui/react';

type PlatformListProps = FlexProps & {
  platforms: Platform[];
};

export const PlatformList = ({ platforms, ...rest }: PlatformListProps) => {
  const color = useColorModeValue('major.300', 'major.700');
  return (
    <Flex {...rest}>
      {platforms.map((p) => (
        <Box
          display='flex'
          justifyContent='center'
          alignItems='center'
          bg={color}
          key={p.id}
          rounded='md'
          m='1'
          px='2'
        >
          <Text>{p.name}</Text>
        </Box>
      ))}
    </Flex>
  );
};
