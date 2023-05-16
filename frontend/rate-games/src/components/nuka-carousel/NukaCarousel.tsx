import {
  Box,
  BoxProps,
  Icon,
  IconButton,
  List,
  ListItem,
} from '@chakra-ui/react';
import Carousel, { ControlProps } from 'nuka-carousel';
import { FaCircle, FaRegCircle } from 'react-icons/fa';
import { FiArrowLeft, FiArrowRight } from 'react-icons/fi';

type CarouselProps = Omit<
  React.ComponentProps<typeof Carousel>,
  keyof React.RefAttributes<HTMLDivElement>
>;

type NukaCarouselProps = BoxProps & {
  carouselProps?: CarouselProps;
};
export const NukaCarousel = ({
  children,
  carouselProps,
  ...rest
}: NukaCarouselProps) => {
  return (
    <Box {...rest}>
      <Carousel
        renderCenterLeftControls={PrevSlideButton}
        renderCenterRightControls={NextSlideButton}
        renderBottomCenterControls={PagingDots}
        {...carouselProps}
      >
        {children}
      </Carousel>
    </Box>
  );
};

const PrevSlideButton = ({ previousSlide }: ControlProps) => (
  <IconButton
    icon={<FiArrowLeft />}
    bg='rgba(0,0,0,0)'
    _hover={{
      bg: 'rgba(0,0,0,0.25)',
    }}
    aria-label='prev-slide'
    onClick={previousSlide}
  />
);

const NextSlideButton = ({ nextSlide }: ControlProps) => (
  <IconButton
    icon={<FiArrowRight />}
    bg='rgba(0,0,0,0)'
    _hover={{
      bg: 'rgba(0,0,0,0.25)',
    }}
    aria-label='next-slide'
    onClick={nextSlide}
  />
);

const PagingDots = ({
  pagingDotsIndices,
  defaultControlsConfig: { pagingDotsOnClick },
  currentSlide,
  onUserNavigation,
  slideCount,
  goToSlide,
}: ControlProps) => {
  const currentSlideBounded = getBoundedIndex(currentSlide, slideCount);
  return (
    <List display='flex' flexDirection='row'>
      {pagingDotsIndices.map((slideIndex, i) => {
        const isActive =
          currentSlideBounded === slideIndex ||
          (currentSlideBounded < slideIndex &&
            (i === 0 || currentSlideBounded > pagingDotsIndices[i - 1]));

        return (
          <ListItem key={slideIndex}>
            <Icon
              color='minor.500'
              cursor='pointer'
              onClick={(event) => {
                pagingDotsOnClick?.(event);
                if (event.defaultPrevented) {
                  return;
                }
                onUserNavigation(event);
                goToSlide(slideIndex);
              }}
              aria-label={`slide ${slideIndex + 1} bullet`}
              aria-selected={isActive}
            >
              {isActive ? <FaCircle /> : <FaRegCircle />}
            </Icon>
          </ListItem>
        );
      })}
    </List>
  );
};

const getBoundedIndex = (rawIndex: number, slideCount: number) => {
  return ((rawIndex % slideCount) + slideCount) % slideCount;
};
