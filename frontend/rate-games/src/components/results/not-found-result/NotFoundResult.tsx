import { Box, Heading, Text, Button } from '@chakra-ui/react';
import { useNavigate } from 'react-router-dom';

export const NotFoundResult = () => {
  const navigate = useNavigate();
  return (
    <Box textAlign='center' py={10} px={6}>
      <Heading
        display='inline-block'
        size='2xl'
        bgGradient='linear(to-r, minor.400, minor.600)'
        backgroundClip='text'
      >
        404
      </Heading>
      <Text fontSize='2rem' mt={3} mb={2}>
        Data Not Found
      </Text>
      <Text mb={6}>
        The data you&apos;re looking for does not seem to exist
      </Text>

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
