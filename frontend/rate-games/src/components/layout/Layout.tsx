import { Outlet } from 'react-router-dom';
import { Sidebar } from '@/components/sidebar';
import {
  FiCompass,
  FiHome,
  FiSettings,
  FiStar,
  FiTrendingUp,
} from 'react-icons/fi';
import { Button, MenuItem } from '@chakra-ui/react';
import { useAuth } from '@/hooks/use-auth';

export const Layout = () => {
  const { logout } = useAuth();
  const linkItems = [
    { name: 'Home', icon: FiHome, href: '#' },
    { name: 'Trending', icon: FiTrendingUp, href: '#' },
    { name: 'Explore', icon: FiCompass, href: '#' },
    { name: 'Favourites', icon: FiStar, href: '#' },
    { name: 'Settings', icon: FiSettings, href: '#' },
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
