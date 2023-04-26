import {
  Box,
  BoxProps,
  CloseButton,
  Drawer,
  DrawerContent,
  Flex,
  FlexProps,
  Icon,
  Link,
  Text,
  useColorModeValue,
  useDisclosure,
} from '@chakra-ui/react';
import { IconType } from 'react-icons';
import { Header } from '@/components/header';
import { Link as RouterLink } from 'react-router-dom';

type LinkItem = {
  name: string;
  icon: IconType;
};
type SidebarProps = BoxProps & {
  linkItems: LinkItem[];
};

export const Sidebar = ({ children, linkItems }: SidebarProps) => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const avatarUrl =
    'https://images.unsplash.com/photo-1619946794135-5bc917a27793?ixlib=rb-0.3.5&q=80&fm=jpg&crop=faces&fit=crop&h=200&w=200&s=b616b2c5b373a80ffc9636ba24f7a4a9';
  return (
    <Box minH='100vh' bg={useColorModeValue('major.100', 'major.900')}>
      <SidebarContent
        linkItems={linkItems}
        onClose={() => onClose}
        display={{ base: 'none', md: 'block' }}
      />
      <Drawer
        //autoFocus={false}
        isOpen={isOpen}
        placement='left'
        onClose={onClose}
        returnFocusOnClose={false}
        onOverlayClick={onClose}
        size='full'
      >
        <DrawerContent>
          <SidebarContent linkItems={linkItems} onClose={onClose} />
        </DrawerContent>
      </Drawer>
      <Header avatarUrl={avatarUrl} onOpen={onOpen} />
      <Box ml={{ base: 0, md: 60 }} p='4'>
        {children}
      </Box>
    </Box>
  );
};

type SidebarContentProps = BoxProps & {
  onClose: () => void;
  linkItems: LinkItem[];
};

const SidebarContent = ({
  onClose,
  linkItems,
  ...rest
}: SidebarContentProps) => {
  return (
    <Box
      transition='3s ease'
      bg={useColorModeValue('major.200', 'major.800')}
      borderRight='1px'
      borderRightColor={useColorModeValue('minor.200', 'minor.700')}
      w={{ base: 'full', md: 60 }}
      pos='fixed'
      h='full'
      {...rest}
    >
      <Flex h='20' alignItems='center' mx='8' justifyContent='space-between'>
        <Text fontSize='2xl' fontFamily='monospace' fontWeight='bold'>
          Logo
        </Text>
        <CloseButton display={{ base: 'flex', md: 'none' }} onClick={onClose} />
      </Flex>
      {linkItems.map((link) => (
        <NavItem key={link.name} icon={link.icon}>
          {link.name}
        </NavItem>
      ))}
    </Box>
  );
};

type NavItemProps = FlexProps & {
  icon: IconType;
  children: string;
};
const NavItem = ({ icon, children, ...rest }: NavItemProps) => {
  return (
    <Link
      as={RouterLink}
      to='#'
      style={{ textDecoration: 'none' }}
      _focus={{ boxShadow: 'none' }}
    >
      <Flex
        align='center'
        p='4'
        mx='4'
        borderRadius='lg'
        cursor='pointer'
        _hover={{
          bg: useColorModeValue('major.400', 'major.600'),
          color: 'white',
          fontWeight: 'semibold',
        }}
        {...rest}
      >
        {icon && (
          <Icon
            mr='4'
            fontSize='16'
            _groupHover={{
              color: 'white',
            }}
            as={icon}
          />
        )}
        {children}
      </Flex>
    </Link>
  );
};
