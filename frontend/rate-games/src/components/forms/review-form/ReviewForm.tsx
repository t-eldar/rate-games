import { RatingBlock } from '@/components/rating-block';
import { createReview } from '@/services/review-service';
import { Box, BoxProps, Button, Flex, Input, Textarea } from '@chakra-ui/react';
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
    <Box {...rest}>
      <Flex>
        <RatingBlock onSelectRating={setRating} />
      </Flex>
      <Input
        value={title}
        onChange={handleTitleChange}
        placeholder='Add title...'
      />
      <Textarea
        value={description}
        onChange={handleDescriptionChange}
        placeholder='Add review...'
      />
      <Button variant='secondary' onClick={handleSave}>
        Save
      </Button>
    </Box>
  );
};
