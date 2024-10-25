import React, { useState } from 'react';
import Papa from 'papaparse';
import { aiDescriptorBulk, updateProducts } from '../../services/ProductService';

interface ModalProps {
  onConfirm: () => void;
  onCancel: () => void;
  onUpdateTranslations: () => void;
  isModalOpen: boolean;
}

const FileUploaderModal: React.FC<ModalProps> = ({ isModalOpen, onConfirm, onCancel, onUpdateTranslations }) => {
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [updatedProducts, setUpdatedProducts] = useState<string[]>([]);
  const [isRequestInProgress, setIsRequestInProgress] = React.useState<boolean>(false);

  if (!isModalOpen) {
    return null;
  }

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0] || null;
    setSelectedFile(file);

    if (file) {
      Papa.parse(file, {
        complete: (results) => {
          const data = results.data as { Title: string }[];
          // Extract only the first column (Column A)
          const podaci = data.map((row) => row.Title);
          setUpdatedProducts(podaci);
        },
        header: true, // Assuming the first row isn't a header
      });
    }
  };

  const formatArray = (arr: string[]) => {
    if (arr.length === 0) return '';
    if (arr.length === 1) return arr[0]; // Only one element, no commas needed
    return `${arr.slice(0, -1).join(', ')} and ${arr[arr.length - 1]}`;
  };

  const handleConfirmClick = async () => {
    setIsRequestInProgress(() => true);
    const formData = new FormData();
    formData.append('csvFile', selectedFile!);

    const result = await aiDescriptorBulk(formData);
    await updateProducts(result);
    setIsRequestInProgress(() => false);
    onUpdateTranslations();
    onConfirm();
  };

  return (
    <div className='backdrop'>
      <div className='modal'>
        <div className='content'>
          <button className='closeButton' onClick={onCancel}>
            ×
          </button>
          {!selectedFile ? (
            <>
              <p>Keep in mind that you will override existing descriptions.</p>
              <p>If you want to continue upload file in csv format to update descriptions in bulk</p>
            </>
          ) : (
            <>
              <p>
                Descriptions for products: <b>{formatArray(updatedProducts)}</b> will be updated
              </p>
              <p>Do you want to proceed?</p>
            </>
          )}

          <label className='customFileUpload'>
            <input className='inputFileUpload' type='file' accept='.csv' onChange={handleFileChange} />
            Choose File
          </label>
          {selectedFile && <p>Selected file: {selectedFile.name}</p>}

          {selectedFile && (
            <div>
              <button onClick={handleConfirmClick}>Yes</button>
              <button onClick={onCancel}>No</button>
            </div>
          )}
        </div>
      </div>
      {isRequestInProgress && (
        <div className='backdrop'>
          <span className='loader'></span>
        </div>
      )}
    </div>
  );
};

export default FileUploaderModal;
