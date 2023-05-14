import { ThemeSwitcher } from '@/components/buttons/theme-switcher';
import { Logo } from '@/components/logo';
import { SignInForm } from '@/components/forms/sign-in';
import { SignUpForm } from '@/components/forms/sign-up-form';
import { useAuth } from '@/hooks/use-auth';
import { User } from '@/types/authentication';
import {
  Avatar,
  Box,
  Flex,
  FlexProps,
  HStack,
  IconButton,
  Menu,
  MenuButton,
  MenuList,
  Modal,
  ModalBody,
  ModalCloseButton,
  ModalContent,
  ModalOverlay,
  Text,
  ThemingProps,
  VStack,
  useBoolean,
  useColorModeValue,
  useDisclosure,
} from '@chakra-ui/react';
import { motion } from 'framer-motion';
import { ReactNode, useEffect, useState } from 'react';
import { FiChevronRight, FiLogIn, FiMenu } from 'react-icons/fi';

type HeaderProps = FlexProps & {
  menuItems: ReactNode[];
  onOpen: () => void;
};
export const Header = ({ onOpen, menuItems, ...rest }: HeaderProps) => {
  const { user } = useAuth();
  const {
    isOpen: isModalOpen,
    onOpen: onModalOpen,
    onClose: onModalClose,
  } = useDisclosure();

  return (
    <Flex
      ml={{ base: 0, md: 60 }}
      px={{ base: 4, md: 4 }}
      height='20'
      alignItems='center'
      bg={useColorModeValue('major.200', 'major.800')}
      borderBottomWidth='1px'
      borderBottomColor={useColorModeValue('minor.200', 'minor.700')}
      justifyContent={{ base: 'space-between', md: 'flex-end' }}
      {...rest}
    >
      <Flex gap='4'>
        <IconButton
          display={{ base: 'flex', md: 'none' }}
          onClick={onOpen}
          variant='outline'
          aria-label='open menu'
          icon={<FiMenu />}
        />
        <Box display={{ base: 'flex', md: 'none' }}>
          <Logo width='50px' height='50px' />
        </Box>
      </Flex>
      <HStack spacing={{ base: '0', md: '6' }}>
        <ThemeSwitcher />
        {!user ? (
          <IconButton
            onClick={onModalOpen}
            aria-label='open sign in modal'
            icon={<FiLogIn />}
          />
        ) : (
          <HeaderMenu user={user} menuItems={menuItems} />
        )}
      </HStack>
      <AuthModal isOpen={isModalOpen} onClose={onModalClose} />
    </Flex>
  );
};

type AuthModalProps = {
  isOpen: boolean;
  onClose: () => void;
};

const AuthModal = ({ isOpen, onClose }: AuthModalProps) => {
  const [isSignIn, { on: onIsSignIn, off: offIsSignIn }] = useBoolean(true);
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent p='0'>
        <ModalCloseButton />
        <ModalBody p='0'>
          {isSignIn ? (
            <SignInForm
              p='3'
              boxShadow='none'
              onRedirectToSignUp={offIsSignIn}
              onSuccess={onClose}
            />
          ) : (
            <SignUpForm
              p='3'
              boxShadow='none'
              onRedirectToSignIn={onIsSignIn}
              onSuccess={onClose}
            />
          )}
        </ModalBody>
      </ModalContent>
    </Modal>
  );
};

type HeaderMenuProps = ThemingProps<'Menu'> & {
  user: User;
  menuItems: ReactNode[];
};

const HeaderMenu = ({ user, menuItems }: HeaderMenuProps) => {
  const [isOpen, setIsOpen] = useState(false);
  const [menuButtonRotation, setMenuButtonRotation] = useState(0);
  useEffect(() => {
    const rotation = isOpen ? 90 : 0;
    setMenuButtonRotation(rotation);
  }, [isOpen]);

  return (
    <Menu onOpen={() => setIsOpen(true)} onClose={() => setIsOpen(false)}>
      <MenuButton py={2} transition='all 0.3s'>
        <HStack>
          <Avatar size={'sm'} src={user.avatarUrl ?? ''} />
          <VStack
            display={{ base: 'none', md: 'flex' }}
            alignItems='flex-start'
            spacing='1px'
            ml='2'
          >
            <Text fontSize='sm'>{user.userName}</Text>
          </VStack>
          <Box
            as={motion.div}
            animate={{ rotate: menuButtonRotation }}
            display={{ base: 'none', md: 'flex' }}
          >
            <FiChevronRight />
          </Box>
        </HStack>
      </MenuButton>
      <MenuList>{menuItems.map((item) => item)}</MenuList>
    </Menu>
  );
};
