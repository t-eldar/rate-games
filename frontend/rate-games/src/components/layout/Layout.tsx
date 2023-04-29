import { Outlet } from 'react-router-dom';
import { Sidebar } from '@/components/sidebar';
import {
  FiCompass,
  FiHome,
  FiSettings,
  FiStar,
  FiTrendingUp,
} from 'react-icons/fi';

export const Layout = () => {
  const linkItems = [
    { name: 'Home', icon: FiHome },
    { name: 'Trending', icon: FiTrendingUp },
    { name: 'Explore', icon: FiCompass },
    { name: 'Favourites', icon: FiStar },
    { name: 'Settings', icon: FiSettings },
  ];
  const headerMenuItems = [<FiStar key='1' />];
  return (
    <>
      <Sidebar headerMenuItems={headerMenuItems} linkItems={linkItems}>
        <Outlet />
      </Sidebar>
    </>
  );
};
