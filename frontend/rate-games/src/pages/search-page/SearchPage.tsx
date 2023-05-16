import { GameList } from '@/components/lists/game-list';
import { Loader } from '@/components/loader';
import { ErrorResult } from '@/components/results/error-result';
import { usePagedFetch } from '@/hooks/use-paged-fetch';
import { getGamesBySearch } from '@/services/game-service';
import {
  Button,
  Center,
  Flex,
  Input,
  Text,
  useBoolean,
} from '@chakra-ui/react';
import { useState } from 'react';
import { FiSearch } from 'react-icons/fi';

const limit = 20;
export const SearchPage = () => {
  const defaultSearch = 'Witcher';
  const [search, setSearch] = useState('');
  const [searchClicked, { toggle }] = useBoolean();
  const [page, setPage] = useState(0);
  const {
    data: games,
    hasMore,
    isLoading,
    error,
    refetch,
    refresh,
  } = usePagedFetch(
    async (limit, offset, signal) => {
      return await getGamesBySearch(
        search.length === 0 ? defaultSearch : search,
        signal,
        limit,
        offset
      );
    },
    page,
    limit,
    [searchClicked]
  );

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
    setPage(0);
    toggle();
    refresh();
    await refetch();
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
            placeholder={defaultSearch}
            value={search}
            onChange={handleSearchChange}
          />
          <Button onClick={handleClickSearch}>
            <FiSearch />
          </Button>
        </Flex>
      </Center>
      <Center flexWrap='wrap' flexDirection='column'>
        {!games ? null : (
          <GameList
            flexWrap='wrap'
            games={[...new Map(games.map((item) => [item.id, item])).values()]}
            justifyContent='space-evenly'
            p='6'
          />
        )}
        <Center flexDirection='column' mb='10'>
          {isLoading ? (
            <Center h={!games ? '50vh' : '10vh'}>
              <Loader />
            </Center>
          ) : !games && error ? (
            <ErrorResult />
          ) : hasMore ? (
            <Button onClick={(_) => setPage((p) => p + 1)}>Load more...</Button>
          ) : (
            <Text fontSize='2xl'>That&apos;s all</Text>
          )}
        </Center>
      </Center>
    </>
  );
};
export default SearchPage;
