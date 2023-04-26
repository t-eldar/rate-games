import {
  Box,
  Button,
  Checkbox,
  Flex,
  FormControl,
  FormLabel,
  Heading,
  Input,
  Stack,
  useColorModeValue
} from '@chakra-ui/react';

export const SignInForm = () => {
  return (
    <Flex minH={'100%'} align={'center'} justify={'center'}>
      <Stack spacing={8} mx={'auto'} maxW={'lg'} py={12} px={6}>
        <Stack align={'center'}>
          <Heading fontSize={'4xl'}>Sign in to your account</Heading>
        </Stack>
        <Box
          rounded={'lg'}
          bg={useColorModeValue('major.200', 'major.800')}
          boxShadow={'lg'}
          p={8}
        >
          <Stack spacing={4}>
            <FormControl>
              <FormLabel>Email address</FormLabel>
              <Input type='email' />
            </FormControl>
            <FormControl>
              <FormLabel>Password</FormLabel>
              <Input type='password' />
            </FormControl>
            <Stack spacing={10}>
              <Stack
                direction={{ base: 'column', sm: 'row' }}
                align={'start'}
                justify={'space-between'}
              >
                <Checkbox>Remember me</Checkbox>
              </Stack>
              <Button variant='secondary'>Sign in</Button>
            </Stack>
          </Stack>
        </Box>
      </Stack>
    </Flex>
  );
};
