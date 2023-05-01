import { useAuth } from '@/hooks/use-auth';
import { useLoreleiAvatar } from '@/hooks/use-lorelei-avatar';
import { isPasswordValid, isUsernameValid } from '@/utils/validation';
import {
  Avatar,
  Box,
  Button,
  Center,
  Flex,
  FlexProps,
  FormControl,
  FormHelperText,
  FormLabel,
  HStack,
  Heading,
  Input,
  Link,
  Stack,
  Text,
  useColorModeValue,
} from '@chakra-ui/react';
import { ChangeEventHandler, MouseEventHandler, useState } from 'react';

type SignUpFormProps = FlexProps & {
  onRedirectToSignIn: () => void;
  onSuccess?: () => void;
  onError?: () => void;
};

export const SignUpForm = ({
  onRedirectToSignIn,
  onSuccess,
  onError,
  ...rest
}: SignUpFormProps) => {
  const [email, setEmail] = useState('');
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const { url: avatarUrl, generate } = useLoreleiAvatar();

  const [isUsernameInvalid, setIsUsernameInvalid] = useState(false);
  const [isPasswordInvalid, setIsPasswordInvalid] = useState(false);

  const { register } = useAuth();

  const handleIconChange: MouseEventHandler<HTMLButtonElement> = (e) => {
    e.preventDefault();
    generate();
  };
  const handleEmailChange: ChangeEventHandler<HTMLInputElement> = (e) => {
    e.preventDefault();
    setEmail(e.target.value);
  };
  const handleUsernameChange: ChangeEventHandler<HTMLInputElement> = (e) => {
    e.preventDefault();
    setIsUsernameInvalid(false);
    setUsername(e.target.value);
  };
  const handlePasswordChange: ChangeEventHandler<HTMLInputElement> = (e) => {
    e.preventDefault();
    setIsPasswordInvalid(false);
    setPassword(e.target.value);
  };
  const handleSignUp: MouseEventHandler<HTMLButtonElement> = async (e) => {
    e.preventDefault();
    const pasValid = isPasswordValid(password);
    const nameValid = isUsernameValid(username);
    if (!pasValid) {
      setIsPasswordInvalid(true);
    }
    if (!nameValid) {
      setIsUsernameInvalid(true);
    }
    if (!register) {
      return;
    }
    if (pasValid && nameValid) {
      const status = await register.invoke({
        username,
        email,
        password,
        avatarUrl,
      });
      if (status === 'success' && onSuccess) {
        onSuccess();
      }
      if (status === 'error' && onError) {
        onError();
      }
    }
  };
  return (
    <Flex {...rest}>
      <Stack spacing={8} mx={'auto'} maxW={'lg'} p='8'>
        <Stack align={'center'}>
          <Heading fontSize={'4xl'}>Sign up</Heading>
        </Stack>
        <Box rounded={'lg'} bg={useColorModeValue('major.200', 'major.800')}>
          <FormControl id='userName'>
            <FormLabel>User Icon</FormLabel>
            <Stack direction={['column', 'row']} spacing={6}>
              <Center>
                <Avatar size='xl' src={avatarUrl}></Avatar>
              </Center>
              <Center w='full'>
                <Button onClick={handleIconChange} w='full' variant='secondary'>
                  Change Icon
                </Button>
              </Center>
            </Stack>
          </FormControl>
          <Stack mt={3} spacing={4}>
            <FormControl isInvalid={isUsernameInvalid} isRequired>
              <FormLabel>Username</FormLabel>
              <Input
                value={username}
                onChange={handleUsernameChange}
                type='text'
              />
            </FormControl>
            <FormControl isRequired>
              <FormLabel>Email address</FormLabel>
              <Input value={email} onChange={handleEmailChange} type='email' />
            </FormControl>
            <FormControl isInvalid={isPasswordInvalid} isRequired>
              <FormLabel>Password</FormLabel>
              <Input
                value={password}
                onChange={handlePasswordChange}
                type='password'
              />
              <FormHelperText>
                Password must contain at least 6 characters, at least one digit,
                uppercase and lowercase.
              </FormHelperText>
            </FormControl>
            <HStack>
              <Text align={'center'} color='major.500'>
                Already have an account?
              </Text>
              <Link color={'minor.600'} onClick={() => onRedirectToSignIn()}>
                Sign in
              </Link>
            </HStack>
            <Stack spacing={10}>
              <Button onClick={handleSignUp} variant='secondary'>
                Sign up
              </Button>
            </Stack>
          </Stack>
        </Box>
      </Stack>
    </Flex>
  );
};
