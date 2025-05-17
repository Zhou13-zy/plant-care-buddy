import React, { useRef, useState } from 'react';

interface ImageUploadProps {
  onFileSelect: (file: File | null) => void;
  initialUrl?: string; // For edit mode or preview
}

const ImageUpload: React.FC<ImageUploadProps> = ({ onFileSelect, initialUrl }) => {
  const [preview, setPreview] = useState<string | null>(initialUrl || null);
  const fileInputRef = useRef<HTMLInputElement>(null);

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0] || null;
    onFileSelect(file);

    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => setPreview(reader.result as string);
      reader.readAsDataURL(file);
    } else {
      setPreview(null);
    }
  };

  return (
    <div>
      <input
        type="file"
        accept="image/*"
        ref={fileInputRef}
        style={{ display: 'none' }}
        onChange={handleFileChange}
      />
      <button type="button" onClick={() => fileInputRef.current?.click()}>
        Choose Image
      </button>
      {preview && (
        <div style={{ marginTop: 8 }}>
          <img src={preview} alt="Preview" style={{ maxWidth: 200, maxHeight: 200 }} />
        </div>
      )}
    </div>
  );
};

export default ImageUpload;