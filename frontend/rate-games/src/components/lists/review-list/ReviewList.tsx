import { Rating } from '@/components/rating';
import { Review } from '@/types/entities';
import {
  Avatar,
  Box,
  Card,
  CardBody,
  CardHeader,
  CardProps,
  Center,
  Flex,
  Stack,
  Text,
  useColorModeValue
} from '@chakra-ui/react';

type ReviewListProps = {
  reviews: Review[];
};
export const ReviewList = ({ reviews, ...rest }: ReviewListProps) => {
  return (
    <Box {...rest}>
      {reviews.map((r) => (
        <ReviewItem key={r.id} review={r} mt="4" />
      ))}
    </Box>
  );
};

type ReviewItemProps = CardProps & {
  review: Review;
};
const ReviewItem = ({ review, ...rest }: ReviewItemProps) => {
  const bgColor = useColorModeValue('major.200', 'major.800');
  const fontColor = useColorModeValue('major.700', 'major.200');

  return (
    <Card rounded='xl' bg={bgColor} {...rest}>
      <CardHeader pb='0'>
        <Flex>
          <Avatar src={review.user.avatarUrl} size='lg' />
          <Stack ml='5'>
            <Text fontSize='xl'>{review.user.userName}</Text>
            <Text fontSize='md' color={fontColor}>
              {review.dateCreated.toDateString()}
            </Text>
          </Stack>
          <Center ml='4'>
            <Rating value={review.ratingId} />
          </Center>
        </Flex>
      </CardHeader>
      <CardBody pt='2' p='6' display='flex' justifyContent='space-between'>
        {review.description}
      </CardBody>
    </Card>
  );
};
