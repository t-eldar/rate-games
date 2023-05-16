import { Sidebar } from '@/components/sidebar';
import { useAuth } from '@/hooks/use-auth';
import { Button, MenuItem } from '@chakra-ui/react';
import { FiHome, FiSearch } from 'react-icons/fi';
import { TbDiscountCheck } from 'react-icons/tb';
import { Outlet } from 'react-router-dom';

export const Layout = () => {
  const { logout } = useAuth();
  const linkItems = [
    { name: 'Home', icon: FiHome, href: '/' },
    { name: 'Search', icon: FiSearch, href: '/search' },
    { name: 'Latest releases', icon: TbDiscountCheck, href: '/latest' },
  ];
  const headerMenuItems = [
    <MenuItem key='1' as={Button} onClick={() => logout?.invoke()}>
      Sign out
    </MenuItem>,
  ];
  return (
    <>
      <Sidebar headerMenuItems={headerMenuItems} linkItems={linkItems}>
        <Outlet />
      </Sidebar>
    </>
  );
};
