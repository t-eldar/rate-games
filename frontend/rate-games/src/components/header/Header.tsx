import { ThemeSwitcher } from '@/components/buttons/theme-switcher';
import {
  Avatar,
  Box,
  Flex,
  FlexProps,
  HStack,
  IconButton,
  Menu,
  MenuButton,
  MenuDivider,
  MenuItem,
  MenuList,
  Text,
  ThemingProps,
  VStack,
  useColorModeValue,
} from '@chakra-ui/react';
import { motion } from 'framer-motion';
import { useEffect, useState } from 'react';
import { FiChevronRight, FiMenu } from 'react-icons/fi';

type HeaderProps = FlexProps & {
  onOpen: () => void;
  avatarUrl: string;
};
export const Header = ({ onOpen, avatarUrl, ...rest }: HeaderProps) => {
  return (
    <Flex
      ml={{ base: 0, md: 60 }}
      px={{ base: 4, md: 4 }}
      height='20'
      alignItems='center'
      bg={useColorModeValue('purple.200', 'purple.900')}
      borderBottomWidth='1px'
      borderBottomColor={useColorModeValue('purple.200', 'purple.700')}
      justifyContent={{ base: 'space-between', md: 'flex-end' }}
      {...rest}
    >
      <IconButton
        display={{ base: 'flex', md: 'none' }}
        onClick={onOpen}
        variant='outline'
        aria-label='open menu'
        icon={<FiMenu />}
      />

      <Text
        display={{ base: 'flex', md: 'none' }}
        fontSize='2xl'
        fontFamily='monospace'
        fontWeight='bold'
      >
        Logo
      </Text>
      <HStack spacing={{ base: '0', md: '6' }}>
        <ThemeSwitcher />
        <Flex alignItems={'center'}></Flex>
      </HStack>
      <HeaderMenu avatarUrl={avatarUrl} />
    </Flex>
  );
};

type HeaderMenuProps = ThemingProps<'Menu'> & {
  avatarUrl: string;
};

const HeaderMenu = ({ avatarUrl }: HeaderMenuProps) => {
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
          <Avatar size={'sm'} src={avatarUrl} />
          <VStack
            display={{ base: 'none', md: 'flex' }}
            alignItems='flex-start'
            spacing='1px'
            ml='2'
          >
            <Text fontSize='sm'>Justina Clark</Text>
            <Text fontSize='xs'>Admin</Text>
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
      <MenuList>
        <MenuItem>Profile</MenuItem>
        <MenuItem>Settings</MenuItem>
        <MenuItem>Billing</MenuItem>
        <MenuDivider />
        <MenuItem>Sign out</MenuItem>
      </MenuList>
    </Menu>
  );
};
