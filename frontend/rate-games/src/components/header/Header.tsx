import { ThemeSwitcher } from '@/components/buttons/theme-switcher';
import { useUser } from '@/hooks/use-user';
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
  Text,
  ThemingProps,
  VStack,
  useColorModeValue,
} from '@chakra-ui/react';
import { motion } from 'framer-motion';
import { ReactNode, useEffect, useState } from 'react';
import { FiChevronRight, FiMenu } from 'react-icons/fi';

type HeaderProps = FlexProps & {
  menuItems: ReactNode[];
  onOpen: () => void;
};
export const Header = ({ onOpen, menuItems, ...rest }: HeaderProps) => {
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
      <HeaderMenu menuItems={menuItems} />
    </Flex>
  );
};

type HeaderMenuProps = ThemingProps<'Menu'> & {
  menuItems: ReactNode[];
};

const HeaderMenu = ({ menuItems }: HeaderMenuProps) => {
  const [isOpen, setIsOpen] = useState(false);
  const [menuButtonRotation, setMenuButtonRotation] = useState(0);
  const { user } = useUser();
  console.log(user);
  useEffect(() => {
    const rotation = isOpen ? 90 : 0;
    setMenuButtonRotation(rotation);
  }, [isOpen]);

  return (
    <Menu onOpen={() => setIsOpen(true)} onClose={() => setIsOpen(false)}>
      <MenuButton py={2} transition='all 0.3s'>
        <HStack>
          <Avatar size={'sm'} src={user?.avatarUrl ?? ''} />
          <VStack
            display={{ base: 'none', md: 'flex' }}
            alignItems='flex-start'
            spacing='1px'
            ml='2'
          >
            <Text fontSize='sm'>{user?.userName}</Text>
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
