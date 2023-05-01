import { debounce } from '@/utils/debounce';
import { useState, useCallback, useLayoutEffect } from 'react';

type Dimensions = {
  width: number;
  height: number;
  top: number;
  left: number;
  x: number;
  y: number;
  right: number;
  bottom: number;
};

function getDimensionObject(node: Element): Dimensions {
  const rect = node.getBoundingClientRect();
  return {
    width: rect.width,
    height: rect.height,
    top: rect.top,
    left: rect.left,
    x: rect.x,
    y: rect.y,
    right: rect.right,
    bottom: rect.bottom,
  };
}

export const useBoundingRect = (limit?: number) => {
  const [dimensions, setDimensions] = useState<Dimensions>();
  const [node, setNode] = useState<Element | null>(null);

  const ref = useCallback((node: Element | null) => {
    setNode(node);
  }, []);

  useLayoutEffect(() => {
    if (typeof window !== 'undefined' && node) {
      const measure = () =>
        window.requestAnimationFrame(() =>
          setDimensions(getDimensionObject(node))
        );

      measure();

      const listener = debounce(limit ? limit : 100, measure);

      window.addEventListener('resize', listener);
      window.addEventListener('scroll', listener);
      return () => {
        window.removeEventListener('resize', listener);
        window.removeEventListener('scroll', listener);
      };
    }
  }, [node, limit]);

  return { ref, dimensions, node };
};
