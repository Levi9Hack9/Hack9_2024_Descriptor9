import React, { useState } from "react";
import { ProductDetails } from "../components/ProductDetails";
import { Product } from "../types/Product";
import FileUploaderModal from "../components/modals/FileUploaderModal";

interface AllProductsPageProps {
  products: Product[];
  onUpdateTranslations: () => void;
}

export const AllProductsPage: React.FC<AllProductsPageProps> = ({
  products,
  onUpdateTranslations,
}) => {
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

  const openModal = () => {
    setIsModalOpen(true);
  };

  const onConfirm = async () => {
    setIsModalOpen(false);
  };

  const onCancel = () => {
    setIsModalOpen(false);
  };

  return (
    <>
      <div className="products-container">
        {products.map((product) => (
          <ProductDetails key={product.id} {...product} />
        ))}
      </div>
      <p className="ai-update-text" onClick={openModal}>
        Update product descriptions in bulk with AI—upload a file now!
      </p>
      <FileUploaderModal
        isModalOpen={isModalOpen}
        onConfirm={onConfirm}
        onCancel={onCancel}
        onUpdateTranslations={onUpdateTranslations}
      />
    </>
  );
};
