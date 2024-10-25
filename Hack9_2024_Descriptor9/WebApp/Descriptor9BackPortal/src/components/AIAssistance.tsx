import React from 'react';

interface AIAssistanceProps {
  onClick: () => void;
}

const AIAssistance: React.FC<AIAssistanceProps> = ({ onClick }) => {
  return (
    <button onClick={onClick} className="cta active ai-btn">
      <div className="ai-btn-content">NEED 
        <span>
          <img className="ai-icon" src="https://proddescriptorstorage.blob.core.windows.net/product-images/microchip_9733176.png" alt="AI Icon" />
        </span> 
        ASSISTANCE?
      </div>
      <span className="shape">
        <span></span>
        <span></span>
        <span></span>
        <span></span>
        <span></span>
        <span></span>
        <span></span>
        <span></span>
      </span>
    </button>
  );
};

export default AIAssistance;