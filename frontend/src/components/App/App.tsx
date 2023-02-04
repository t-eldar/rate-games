import { Select } from '../Select';

export const App = () => {
  type Person = {
    id: number;
    name: string;
    date: Date;
  };
  const options: Person[] = [
    { id: 1, name: 'gello', date: new Date() },
    { id: 2, name: 'ffffllo', date: new Date() },
    { id: 2, name: 'ffffffs', date: new Date() },
  ];

  return (
    <>
      <Select
        options={options}
        label='name'
        placeholder='select'
        onSelect={(opt) => console.log('ffffffffffffffffffffff' + opt)}
      />
    </>
  );
};
