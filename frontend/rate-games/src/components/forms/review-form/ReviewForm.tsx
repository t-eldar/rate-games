import { AddRating } from '@/components/add-rating';
import { Loader } from '@/components/loader';
import { ErrorResult } from '@/components/results/error-result';
import { useAwait } from '@/hooks/use-await';
import { createReview, updateReview } from '@/services/review-service';
import { Review } from '@/types/entities';
import {
  Box,
  BoxProps,
  Button,
  Center,
  Flex,
  Input,
  Text,
  Textarea,
  useToast,
} from '@chakra-ui/react';
import { useState } from 'react';

type FormProps = {
  onSend?: () => void;
  gameId: number;
} & (
  | {
      isEdit: true;
      initialValue: {
        id: number;
        title: string;
        description: string;
        rating: number;
      };
    }
  | {
      isEdit: false;
    }
);

type ReviewFormProps = BoxProps & FormProps;

export const ReviewForm = (props: ReviewFormProps) => {
  const { isEdit, gameId, onSend, ...rest } = props;

  let boxRest: BoxProps;
  if ('initialValue' in rest) {
    const { initialValue: _, ...other } = rest;
    boxRest = other;
  } else {
    boxRest = rest;
  }

  const initialValue =
    isEdit && 'initialValue' in rest && rest.initialValue
      ? rest.initialValue
      : { title: '', description: '', rating: 0 };

  const toast = useToast();

  const [description, setDescription] = useState(initialValue.description);
  const [title, setTitle] = useState(initialValue.title);
  const [rating, setRating] = useState(initialValue.rating);

  const {
    promise: create,
    isLoading: isCreateLoading,
    error: createError,
  } = useAwait(createReview);
  const {
    promise: edit,
    isLoading: isEditLoading,
    error: editError,
  } = useAwait(updateReview);

  const handleDescriptionChange: React.ChangeEventHandler<
    HTMLTextAreaElement
  > = (e) => {
    e.preventDefault();
    setDescription(e.target.value);
  };
  const handleTitleChange: React.ChangeEventHandler<HTMLInputElement> = (e) => {
    e.preventDefault();
    setTitle(e.target.value);
  };
  const handleSave: React.MouseEventHandler<HTMLButtonElement> = async (e) => {
    e.preventDefault();
    if (isEdit) {
      if (!('id' in initialValue)) {
        toast({
          title: 'Internal error! Try again later!',
          isClosable: true,
          status: 'error',
        });
        return;
      }
      await edit({
        id: initialValue.id,
        title,
        description,
        ratingValue: rating,
      });
    } else {
      await create({
        gameId,
        title,
        description,
        rating,
      });
    }
    if (onSend) {
      onSend();
    }
  };
  return (
    <Box {...boxRest} p='6'>
      {isCreateLoading || isEditLoading ? (
        <Loader />
      ) : createError || editError ? (
        <ErrorResult />
      ) : (
        <>
          <Flex>
            <Text mr='4'>Rate game:</Text>
            <AddRating
              onSelectRating={setRating}
              initial={initialValue.rating}
            />
          </Flex>
          <Input
            value={title}
            onChange={handleTitleChange}
            placeholder='Add title...'
            mb='2'
          />
          <Textarea
            value={description}
            onChange={handleDescriptionChange}
            placeholder='Add review...'
            mb='2'
          />
          <Center>
            <Button w='3xs' variant='secondary' onClick={handleSave}>
              Save
            </Button>
          </Center>
        </>
      )}
    </Box>
  );
};
