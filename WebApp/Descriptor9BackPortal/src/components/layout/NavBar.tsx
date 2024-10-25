import React from "react";
import { NavLink } from "react-router-dom";

const NavBar: React.FC = () => {
  const handleCLick = (e: React.MouseEvent<HTMLAnchorElement>) => {
    const path = (window.location.href as string).split("/").pop();

    if (path?.includes("single-product")) {
      e.preventDefault();
    }
  };
  return (
    <nav>
      <NavLink className="link" to="/">
        Products
      </NavLink>
      <NavLink onClick={handleCLick} className="link" to="/single-product">
        Add Product
      </NavLink>
    </nav>
  );
};

export default NavBar;
