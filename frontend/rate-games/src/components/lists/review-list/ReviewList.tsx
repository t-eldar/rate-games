import { ReviewItem } from '@/components/review-item';
import { Review } from '@/types/entities';
import { Box, BoxProps } from '@chakra-ui/react';

type ReviewListProps = BoxProps & {
  removeUserReviews?: boolean;
  reviews: Review[];
  userId: string;
  onClickEdit: (review: Review) => void;
  onClickDelete: (id: number) => void;
};
export const ReviewList = ({
  reviews,
  userId,
  removeUserReviews = false,
  onClickEdit,
  onClickDelete,
  ...rest
}: ReviewListProps) => {
  return (
    <Box {...rest}>
      {(removeUserReviews
        ? reviews.filter((r) => r.userId != userId)
        : reviews
      ).map((r) => (
        <ReviewItem
          key={r.id}
          review={r}
          mt='4'
          userId={userId}
          onClickEdit={onClickEdit}
          onClickDelete={onClickDelete}
        />
      ))}
    </Box>
  );
};
