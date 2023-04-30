import { useLoreleiAvatar } from '@/hooks/use-lorelei-avatar';
import {
  Avatar,
  Box,
  Button,
  Center,
  FormControl,
  FormLabel,
  Heading,
  Input,
  Stack,
  useColorModeValue,
} from '@chakra-ui/react';
import { MouseEventHandler } from 'react';

export const SignUpForm = () => {
  const { url, generate } = useLoreleiAvatar();

  const handleIconChange: MouseEventHandler<HTMLButtonElement> = (e) => {
    e.preventDefault();
    generate();
  };
  return (
    <Stack spacing={8} mx={'auto'} maxW={'lg'} minW={'sm'} py={12} px={6}>
      <Stack align={'center'}>
        <Heading fontSize={'4xl'}>Sign up</Heading>
      </Stack>
      <Box
        rounded={'lg'}
        bg={useColorModeValue('major.200', 'major.800')}
        boxShadow={'lg'}
        p={8}
      >
        <FormControl id='userName'>
          <FormLabel>User Icon</FormLabel>
          <Stack direction={['column', 'row']} spacing={6}>
            <Center>
              <Avatar size='xl' src={url}></Avatar>
            </Center>
            <Center w='full'>
              <Button onClick={handleIconChange} w='full' variant='secondary'>
                Change Icon
              </Button>
            </Center>
          </Stack>
        </FormControl>
        <Stack mt={3} spacing={4}>
          <FormControl>
            <FormLabel>Email address</FormLabel>
            <Input type='email' />
          </FormControl>
          <FormControl>
            <FormLabel>Password</FormLabel>
            <Input type='password' />
          </FormControl>
          <Stack spacing={10}>
            <Button variant='secondary'>Sign up</Button>
          </Stack>
        </Stack>
      </Box>
    </Stack>
  );
};
