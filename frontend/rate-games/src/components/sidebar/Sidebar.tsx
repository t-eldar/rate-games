import { Header } from '@/components/header';
import { Logo } from '@/components/logo';
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
import { ReactNode } from 'react';
import { IconType } from 'react-icons';
import { Link as RouterLink } from 'react-router-dom';

type LinkItem = {
  href: string;
  name: string;
  icon: IconType;
};
type SidebarProps = BoxProps & {
  headerMenuItems: ReactNode[];
  linkItems: LinkItem[];
};

export const Sidebar = ({
  children,
  linkItems,
  headerMenuItems,
  ...rest
}: SidebarProps) => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  return (
    <Box
      minH='100vh'
      minW='xs'
      bg={useColorModeValue('major.100', 'major.900')}
      {...rest}
    >
      <SidebarContent
        linkItems={linkItems}
        onClose={() => onClose}
        display={{ base: 'none', md: 'block' }}
      />
      <Drawer
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
      <Header menuItems={headerMenuItems} onOpen={onOpen} />
      <Box ml={{ base: 0, md: 60 }}>{children}</Box>
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
      transition='1s ease'
      bg={useColorModeValue('major.200', 'major.800')}
      borderRight='1px'
      borderRightColor={useColorModeValue('minor.200', 'minor.700')}
      w={{ base: 'full', md: 60 }}
      pos='fixed'
      minH='100vh'
      {...rest}
    >
      <Flex
        h='20'
        alignItems='center'
        mx='8'
        justifyContent='space-between'
        gap='3'
      >
        <Logo width='50px' height='50px' />
        <Text fontSize='2xl' fontFamily='monospace' fontWeight='bold'>
          rategames
        </Text>
        <CloseButton display={{ base: 'flex', md: 'none' }} onClick={onClose} />
      </Flex>
      {linkItems.map((link) => (
        <NavItem key={link.name} icon={link.icon} href={link.href}>
          {link.name}
        </NavItem>
      ))}
    </Box>
  );
};

type NavItemProps = FlexProps & {
  href: string;
  icon: IconType;
  children: string;
};
const NavItem = ({ icon, children, href, ...rest }: NavItemProps) => {
  return (
    <Link
      as={RouterLink}
      to={href}
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
