import React from "react";
import { Product } from "../types/Product";
import { createSearchParams, useNavigate } from "react-router-dom";

export const ProductDetails: React.FC<Product> = ({ id, title, imageUrl }) => {
  const navigation = useNavigate();

  const handleNavigation = () => {
    navigation({
      pathname: "single-product",
      search: createSearchParams({
        id: id,
      }).toString(),
    });
  };

  return (
    <div className="single-product">
      <img
        onClick={handleNavigation}
        className="single-product-img"
        src={imageUrl}
        alt={title}
      />
      <p>{title}</p>
    </div>
  );
};
