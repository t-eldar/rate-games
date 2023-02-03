import { useState } from 'react';
import styles from './Dropdown.module.css';

type DropdownProps = {
  children: JSX.Element | JSX.Element[];
  header: JSX.Element | string;
  hasArrow?: boolean;
};

export const Dropdown = ({ children, header }: DropdownProps) => {
  const [isOpen, setIsOpen] = useState(false);
  if (!Array.isArray(children)) {
    children = [children];
  }
  const toggle = () => setIsOpen((prev) => !prev);

  return (
    <div>
      <button className={styles['dropdown-header']} onClick={toggle}>
        {header}
      </button>
      {isOpen ? (
        <ul className={styles['dropdown-menu']}>
          {children.map((child) => (
            <>
              <li className={styles['dropdown-item']} key={child.key}>
                {child}
              </li>
              <hr className={styles['items-divider']} />
            </>
          ))}
        </ul>
      ) : null}
    </div>
  );
};
