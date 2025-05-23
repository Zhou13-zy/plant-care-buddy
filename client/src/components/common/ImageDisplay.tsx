import React, { useState } from 'react';
import Lightbox from 'react-image-lightbox';
import 'react-image-lightbox/style.css';

const API_BASE = "https://localhost:7226";

interface ImageDisplayProps {
  imagePath?: string | null;
  alt?: string;
  style?: React.CSSProperties;
  className?: string;
  fallbackSrc?: string;
}

const ImageDisplay: React.FC<ImageDisplayProps> = ({
  imagePath,
  alt = '',
  style,
  className,
  fallbackSrc,
}) => {
  const [isLightboxOpen, setIsLightboxOpen] = useState(false);

  if (!imagePath) {
    return fallbackSrc ? (
      <img src={fallbackSrc} alt={alt} style={style} className={className} />
    ) : null;
  }

  // Normalize path: remove leading "images/" if present
  const normalizedPath = imagePath.replace(/^images[\\/]/, '');
  const src = `${API_BASE}/images/${normalizedPath}`;

  return (
    <>
      <img
        src={src}
        alt={alt}
        style={{ ...style, cursor: 'pointer' }}
        className={className}
        onClick={() => setIsLightboxOpen(true)}
        onError={e => {
          if (fallbackSrc) (e.target as HTMLImageElement).src = fallbackSrc;
        }}
      />
      {isLightboxOpen && (
        <Lightbox
          mainSrc={src}
          onCloseRequest={() => setIsLightboxOpen(false)}
          imageTitle={alt}
        />
      )}
    </>
  );
};

export default ImageDisplay;
