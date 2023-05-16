import { HeroBlock } from '@/components/hero-block';
import { NukaCarousel } from '@/components/nuka-carousel';
import { Box, Center, Heading, Image } from '@chakra-ui/react';

export const HomePage = () => {
  return (
    <>
      <NukaCarousel
        w='full'
        h='full'
        carouselProps={{
          autoplay: true,
          wrapAround: true,
        }}
      >
        <HeroBlock
          w='full'
          h='80vh'
          imageUrl='https://cq.ru/storage/uploads/posts/1137427/1.jpg'
        >
          <Heading color='white' fontSize='3.5rem'>
            Explore new games
          </Heading>
          <Heading color='white' fontSize='2.5rem'>
            using our website.
          </Heading>
        </HeroBlock>
        <HeroBlock
          w='full'
          h='80vh'
          imageUrl='https://i.playground.ru/p/F3RpVzzl_U_HIAqGLcoNBA.jpeg'
        >
          <Heading color='major.900' fontSize='3.5rem'>
            Rate
          </Heading>
          <Heading color='major.900' fontSize='2.5rem'>
            your favorite games.
          </Heading>
        </HeroBlock>
      </NukaCarousel>
    </>
  );
};
export default HomePage;
