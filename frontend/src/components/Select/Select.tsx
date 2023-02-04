import { MouseEventHandler, ReactNode, useEffect, useState } from 'react';
import styles from './Select.module.css';

type ExtendingValuesOf<T, TExtending> = {
  [K in keyof T as T[K] extends TExtending ? K : never]: T[K];
};

type ExtendingKeysOf<T, TExtending> = keyof ExtendingValuesOf<T, TExtending>;

type SelectProps<T> = {
  options: T[];
  label: ExtendingKeysOf<T, ReactNode>;
  onSelect?: (option: T) => void;
  placeholder: ReactNode;
};

export const Select = <T extends object>({
  options,
  label,
  onSelect,
  placeholder,
}: SelectProps<T>) => {
  const [isOpen, setIsOpen] = useState(false);
  const [selectedOption, setSelectedOption] = useState<T>();

  const handleOpen: MouseEventHandler = () => {
    setIsOpen((prev) => !prev);
  };

  useEffect(() => {
    if (onSelect && selectedOption) {
      onSelect(selectedOption);
      console.log(selectedOption);
    }
  }, [selectedOption]);

  return (
    <>
      <button className={styles.header} onClick={handleOpen}>
        {placeholder}
      </button>
      {isOpen ? (
        <ul className={styles.options}>
          {options.map((option, i) => (
            <li key={i}>
              <button
                className={styles.option}
                onClick={() => setSelectedOption(option)}
              >
                {option[label] as ReactNode}
              </button>
            </li>
          ))}
        </ul>
      ) : null}
    </>
  );
};
