import { Box, BoxProps, Center, Heading } from '@chakra-ui/react';

type HeroBlockProps = BoxProps & {
  imageUrl: string;
  children: JSX.Element | JSX.Element[];
};

export const HeroBlock = ({ imageUrl, children, ...rest }: HeroBlockProps) => {
  return (
    <Box
      backgroundImage={`url(${imageUrl})`}
      backgroundPosition='center'
      backgroundSize='cover'
      {...rest}
    >
      <Box
        background='rgba(255, 255, 255, 0.07)'
        backdropFilter={'blur(3px)'}
        h='100%'
        w='100%'
      >
        <Center h='100%' flexDirection='column'>
          {children}
        </Center>
      </Box>
    </Box>
  );
};
