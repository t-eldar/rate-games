import { useAuth } from '@/hooks/use-auth';
import { signIn } from '@/services/authentication-service';
import {
  Box,
  Button,
  Checkbox,
  Flex,
  FlexProps,
  FormControl,
  FormLabel,
  Heading,
  Input,
  Stack,
  useColorModeValue,
} from '@chakra-ui/react';
import { ChangeEventHandler, MouseEventHandler, useState } from 'react';
import { useQuery } from 'react-query';

type SignInFormProps = FlexProps & {
  onSuccess?: () => void;
  onError?: () => void;
};

export const SignInForm = ({
  onSuccess,
  onError,
  ...rest
}: SignInFormProps) => {
  const [usernameOrEmail, setUsernameOrEmail] = useState('');
  const [password, setPassword] = useState('');
  const [rememberMe, setRememberMe] = useState(false);
  const [isInvalid, setIsInvalid] = useState(false);

  const { login } = useAuth();
  const handleSignIn: MouseEventHandler<HTMLButtonElement> = async (e) => {
    e.preventDefault();
    if (!login) {
      return;
    }
    const response = await login({
      usernameOrEmail,
      password,
      rememberMe,
    });
    if (response.status === 200 && onSuccess) {
      onSuccess();
    }
    if (response.status !== 200 && onError) {
      onError();
      setIsInvalid(true);
    }
  };

  const handleRememberMeChange: ChangeEventHandler<HTMLInputElement> = (e) => {
    e.preventDefault();
    setRememberMe(e.target.checked);
  };
  const handleUsernameChange: ChangeEventHandler<HTMLInputElement> = (e) => {
    e.preventDefault();
    setUsernameOrEmail(e.target.value);
  };
  const handlePasswordChange: ChangeEventHandler<HTMLInputElement> = (e) => {
    e.preventDefault();
    setPassword(e.target.value);
  };
  return (
    <Flex minH={'100%'} align={'center'} justify={'center'} {...rest}>
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
            <FormControl isInvalid={isInvalid}>
              <FormLabel>Username or email address</FormLabel>
              <Input
                value={usernameOrEmail}
                onChange={handleUsernameChange}
                type='email'
              />
            </FormControl>
            <FormControl isInvalid={isInvalid}>
              <FormLabel>Password</FormLabel>
              <Input
                value={password}
                onChange={handlePasswordChange}
                type='password'
              />
            </FormControl>
            <Stack spacing={10}>
              <Stack
                direction={{ base: 'column', sm: 'row' }}
                align={'start'}
                justify={'space-between'}
              >
                <Checkbox
                  onChange={handleRememberMeChange}
                  checked={rememberMe}
                >
                  Remember me
                </Checkbox>
              </Stack>
              <Button onClick={handleSignIn} variant='secondary'>
                Sign in
              </Button>
            </Stack>
          </Stack>
        </Box>
      </Stack>
    </Flex>
  );
};
