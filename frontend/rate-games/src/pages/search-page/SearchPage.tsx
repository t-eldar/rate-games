import { GameList } from '@/components/lists/game-list';
import { Loader } from '@/components/loader';
import { ErrorResult } from '@/components/results/error-result';
import { NotFoundResult } from '@/components/results/not-found-result';
import { useFetch } from '@/hooks/use-fetch';
import { getGamesBySearch } from '@/services/game-service';
import { Button, Center, Flex, Input } from '@chakra-ui/react';
import { useState } from 'react';
import { FiSearch } from 'react-icons/fi';

export const SearchPage = () => {
  const defaultSearch = 'Witcher';
  const [search, setSearch] = useState(defaultSearch);

  const {
    data: games,
    isLoading: gamesLoading,
    error: gamesError,
    refetch: fetchGames,
  } = useFetch(async () => {
    return await getGamesBySearch(search);
  });

  const handleSearchChange: React.ChangeEventHandler<HTMLInputElement> = (
    e
  ) => {
    e.preventDefault();
    setSearch(e.target.value);
  };
  const handleClickSearch: React.MouseEventHandler<HTMLButtonElement> = async (
    e
  ) => {
    e.preventDefault();

    await fetchGames();
  };
  return (
    <>
      <Center>
        <Flex
          w='50%'
          minW='md'
          justifyContent='center'
          alignItems='center'
          m='6'
          gap='3'
        >
          <Input
            p='4'
            size='lg'
            placeholder={'Search...'}
            value={search}
            onChange={handleSearchChange}
          />
          <Button onClick={handleClickSearch}>
            <FiSearch />
          </Button>
        </Flex>
      </Center>
      {gamesLoading ? (
        <Center h='80vh'>
          <Loader />
        </Center>
      ) : gamesError ? (
        <ErrorResult />
      ) : !games || games.length === 0 ? (
        <NotFoundResult />
      ) : (
        <Center>
          <GameList
            justifyContent='center'
            gap='4'
            flexWrap='wrap'
            games={games}
          />
        </Center>
      )}
    </>
  );
};
export default SearchPage;
