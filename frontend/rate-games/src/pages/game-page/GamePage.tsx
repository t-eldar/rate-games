import { ReviewForm } from '@/components/forms/review-form';
import { GenreList } from '@/components/lists/genre-list';
import { PlatformList } from '@/components/lists/platform-list';
import { ReviewList } from '@/components/lists/review-list';
import { Loader } from '@/components/loader';
import { NukaCarousel } from '@/components/nuka-carousel';
import { ErrorResult } from '@/components/results/error-result';
import { NotFoundResult } from '@/components/results/not-found-result';
import { ReviewItem } from '@/components/review-item';
import { useAuth } from '@/hooks/use-auth';
import { useAwait } from '@/hooks/use-await';
import { useFetch } from '@/hooks/use-fetch';
import { usePagedFetch } from '@/hooks/use-paged-fetch';
import { getGameById } from '@/services/game-service';
import {
  deleteReview,
  getReviewByUserAndGame,
  getReviewsByGame,
} from '@/services/review-service';
import { Review } from '@/types/entities';
import {
  Box,
  Button,
  Center,
  Flex,
  Heading,
  Image,
  Modal,
  ModalBody,
  ModalCloseButton,
  ModalContent,
  ModalHeader,
  ModalOverlay,
  Stack,
  Text,
  useBoolean,
  useColorModeValue,
  useDisclosure,
} from '@chakra-ui/react';
import { useState } from 'react';
import { useParams } from 'react-router-dom';

export const GamePage = () => {
  const params = useParams();
  const id = Number(params.gameId);

  const [reviewsPage, setReviewsPage] = useState(0);

  const { isOpen, onOpen, onClose } = useDisclosure();
  const [isDelete, { on: onIsDelete, off: offIsDelete }] = useBoolean();

  const [editingValue, setEditingValue] = useState<Review>();
  const [deletingId, setDeletingId] = useState<number>();

  const { user } = useAuth();

  const {
    data: game,
    isLoading: isGameLoading,
    error: gameError,
  } = useFetch(async () => {
    return await getGameById(id);
  });

  const {
    data: userReview,
    isLoading: isUserReviewLoading,
    refetch: fetchUserReview,
  } = useFetch(async () => {
    return await getReviewByUserAndGame(id);
  });

  // const { data: reviews } = useFetch(async () => {
  //   return await getReviewsByGame(id);
  // });
  const {
    data: reviews,
    isLoading: isReviewsLoading,
    error: reviewsError,
    hasMore,
  } = usePagedFetch(async (limit, offset, signal) => {
    return await getReviewsByGame(id, signal, limit, offset);
  }, reviewsPage);
  const {
    promise: callDelete,
    isLoading: isDeleteLoading,
    error: deleteError,
  } = useAwait(deleteReview);
  const color = useColorModeValue('major.200', 'major.800');

  const handleClickEdit = (value: Review) => {
    setEditingValue({ ...value });
    offIsDelete();
    onOpen();
  };
  const handleClickDelete = (id: number) => {
    setDeletingId(id);
    onIsDelete();
    onOpen();
  };
  const handleDelete: React.MouseEventHandler<HTMLButtonElement> = async (
    e
  ) => {
    e.preventDefault();
    if (!deletingId) {
      return;
    }
    await callDelete(deletingId);
    if (!deleteError) {
      onSend();
    }
  };

  const onSend = async () => {
    onClose();
    await fetchUserReview();
    console.log(userReview);
  };

  if (!game) {
    if (isGameLoading) {
      return (
        <Center h='80vh'>
          <Loader />
        </Center>
      );
    }
    if (gameError) {
      return <ErrorResult />;
    }
    return <NotFoundResult />;
  }
  return (
    <Stack>
      <Modal isOpen={isOpen} onClose={onClose} isCentered>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>
            {isDelete ? 'Delete review' : 'Edit review'}
          </ModalHeader>
          <ModalCloseButton />
          <ModalBody px='6'>
            {isDelete ? (
              isDeleteLoading ? (
                <Loader />
              ) : deleteError ? (
                <ErrorResult />
              ) : (
                <Box>
                  <Text>Are you sure you want to delete the review?</Text>
                  <Center mt='4' gap='2'>
                    <Button onClick={handleDelete}>Yes</Button>
                    <Button variant='secondary' onClick={onClose}>
                      No
                    </Button>
                  </Center>
                </Box>
              )
            ) : (
              <ReviewForm
                onSend={onSend}
                gameId={editingValue?.gameId ?? -1}
                isEdit={true}
                initialValue={
                  editingValue ?? {
                    id: 0,
                    title: '',
                    description: '',
                    rating: 0,
                  }
                }
              />
            )}
          </ModalBody>
        </ModalContent>
      </Modal>
      <Flex
        p='10'
        w='100%'
        direction={{ base: 'column-reverse', md: 'row' }}
        justifyContent='space-between'
      >
        <Box w={{ md: 'md', lg: '2xl' }}>
          <Heading mb='5'>{game.name}</Heading>
          {!game.screenshots ? null : (
            <NukaCarousel
              mb='4'
              carouselProps={{ cellSpacing: 10, wrapAround: true }}
            >
              {game.screenshots.map((s) => (
                <Box
                  h='lg'
                  key={s.id}
                  display='flex'
                  justifyContent='center'
                  alignItems='center'
                >
                  <Image
                    w='full'
                    borderRadius='lg'
                    draggable='false'
                    src={s.url}
                  />
                </Box>
              ))}
            </NukaCarousel>
          )}
          <Box p='6' rounded='xl' bg={color}>
            <Text>{game.summary}</Text>
          </Box>
          {!userReview ? (
            isUserReviewLoading ? (
              <Loader />
            ) : (
              <>
                <ReviewForm
                  onSend={onSend}
                  isEdit={false}
                  w='full'
                  mt='4'
                  gameId={game.id}
                  p='6'
                  rounded='xl'
                  bg={color}
                />
              </>
            )
          ) : (
            <ReviewItem
              mt='6'
              userId={user?.id ?? ''}
              review={userReview}
              onClickEdit={handleClickEdit}
              onClickDelete={handleClickDelete}
            />
          )}
          {!reviews ? null : (
            <ReviewList
              removeUserReviews
              w='full'
              userId={user?.id ?? ''}
              reviews={reviews}
              onClickEdit={handleClickEdit}
              onClickDelete={handleClickDelete}
            />
          )}
          {isReviewsLoading ? (
            <Center>
              <Loader />
            </Center>
          ) : !reviews && reviewsError ? (
            <ErrorResult />
          ) : hasMore ? (
            <Button onClick={() => setReviewsPage((p) => p + 1)}>
              Load more...
            </Button>
          ) : (
            <Text fontSize='2xl'>That&apos;s all</Text>
          )}
        </Box>
        <Box
          display='flex'
          flexDirection='column'
          justifyItems='center'
          alignItems='center'
          w={{ md: 'xs', lg: 'md' }}
        >
          <Image rounded='lg' w='full' src={game.cover.url} />
          <Box w='full' p='6' rounded='xl' bg={color} mt='3'>
            {!game.genres ? null : (
              <>
                <Text>Genres: </Text>
                <GenreList flexWrap='wrap' genres={game.genres} />
              </>
            )}
            {!game.platforms ? null : (
              <>
                <Text>Available on: </Text>
                <PlatformList
                  w='full'
                  flexWrap='wrap'
                  platforms={game.platforms}
                />
              </>
            )}
            {!game.firstReleaseDate ? null : (
              <Text>Released: {game.firstReleaseDate.toDateString()}</Text>
            )}
          </Box>
        </Box>
      </Flex>
    </Stack>
  );
};
