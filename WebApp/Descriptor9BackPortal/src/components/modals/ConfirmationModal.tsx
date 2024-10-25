import React from "react";

interface ConfirmationModalProps {
  onConfirm: () => void;
  onCancel: () => void;
  isModalOpen: boolean;
}

const ConfirmationModal: React.FC<ConfirmationModalProps> = ({
  isModalOpen,
  onConfirm,
  onCancel,
}) => {
  if (!isModalOpen) {
    return null;
  }

  return (
    <div className="backdrop">
      <div className="modal">
        <div className="content">
          <p>Keep in mind that you will override existing descriptions.</p>
          <p>Are you sure you want to continue?</p>
          <div>
            <button onClick={onConfirm}>Yes</button>
            <button onClick={onCancel}>No</button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ConfirmationModal;
