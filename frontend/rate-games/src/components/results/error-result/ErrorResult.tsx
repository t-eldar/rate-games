import { Box, Button, Flex, Heading, Text } from '@chakra-ui/react';
import { CloseIcon } from '@chakra-ui/icons';
import { useNavigate } from 'react-router-dom';

export const ErrorResult = () => {
  const navigate = useNavigate();
  return (
    <Box textAlign='center' py={10} px={6}>
      <Box display='inline-block'>
        <Flex
          flexDirection='column'
          justifyContent='center'
          alignItems='center'
          bg={'red.500'}
          rounded={'50px'}
          w={'55px'}
          h={'55px'}
          textAlign='center'
        >
          <CloseIcon boxSize={'20px'} color={'white'} />
        </Flex>
      </Box>
      <Heading as='h2' size='xl' mt={6} mb={2}>
        Error occured!
      </Heading>
      <Text color={'gray.500'}>Please try again later.</Text>
      <Button
        colorScheme='minor'
        bgGradient='linear(to-r, minor.400, minor.500, minor.600)'
        color='white'
        variant='solid'
        onClick={() => navigate('/', { replace: true })}
      >
        Go to Home
      </Button>
    </Box>
  );
};
