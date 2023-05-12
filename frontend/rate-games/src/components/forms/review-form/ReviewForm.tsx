import { AddRating } from '@/components/add-rating';
import { createReview } from '@/services/review-service';
import {
  Box,
  BoxProps,
  Button,
  Center,
  Flex,
  Input,
  Text,
  Textarea,
} from '@chakra-ui/react';
import { useState } from 'react';

type ReviewFormProps = BoxProps & {
  gameId: number;
};

export const ReviewForm = ({ gameId, ...rest }: ReviewFormProps) => {
  const [description, setDescription] = useState('');
  const [title, setTitle] = useState('');
  const [rating, setRating] = useState<number>();

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
  const handleSave: React.MouseEventHandler<HTMLButtonElement> = (e) => {
    e.preventDefault();
    createReview({
      gameId,
      title,
      description,
      ratingId: rating ?? 0,
    });
  };
  return (
    <Box {...rest} p='6'>
      <Flex>
        <Text mr='4'>Rate game:</Text>
        <AddRating onSelectRating={setRating} />
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
    </Box>
  );
};
