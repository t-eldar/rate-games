import { Rating } from '@/components/rating';
import { Review } from '@/types/entities';
import {
  Avatar,
  Box,
  Button,
  Card,
  CardBody,
  CardHeader,
  CardProps,
  Center,
  Flex,
  Stack,
  Text,
  useColorModeValue,
} from '@chakra-ui/react';
import { FiEdit, FiTrash } from 'react-icons/fi';

type ReviewItemProps = CardProps & {
  review: Review;
  userId: string | undefined;
  onClickEdit: (value: Review) => void;
  onClickDelete: (id: number) => void;
};
export const ReviewItem = ({
  review,
  userId,
  onClickEdit,
  onClickDelete,
  ...rest
}: ReviewItemProps) => {
  const bgColor = useColorModeValue('major.200', 'major.800');
  const fontColor = useColorModeValue('major.700', 'major.300');

  return (
    <Card rounded='xl' bg={bgColor} {...rest}>
      <CardHeader pb='0'>
        <Flex justifyContent='space-between'>
          <Flex>
            <Avatar src={review.userInfo.avatarUrl} size='lg' />
            <Stack ml='5'>
              <Text fontSize='xl'>{review.userInfo.userName}</Text>
              <Text fontSize='md' color={fontColor}>
                {review.dateCreated.toDateString()}
              </Text>
            </Stack>
          </Flex>
          <Center ml='4'>
            <Rating value={review.rating} />
          </Center>
          {userId === review.userId ? (
            <Flex gap='2'>
              <Button onClick={() => onClickEdit(review)}>
                <FiEdit />
              </Button>
              <Button onClick={() => onClickDelete(review.id)}>
                <FiTrash />
              </Button>
            </Flex>
          ) : null}
        </Flex>
      </CardHeader>
      <CardBody pt='2' p='6' display='flex' justifyContent='space-between'>
        <Box>
          <Text fontSize='xl'>{review.title}</Text>
          <Text>{review.description}</Text>
        </Box>
      </CardBody>
    </Card>
  );
};
