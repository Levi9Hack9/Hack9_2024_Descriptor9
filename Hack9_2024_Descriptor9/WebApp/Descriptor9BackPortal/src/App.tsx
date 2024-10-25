import { Route, Routes } from 'react-router-dom';
import './App.css';
import NavBar from './components/layout/NavBar';
import { AllProductsPage } from './pages/AllProductsPage';
import { SingleProductPage } from './pages/SingleProductPage';
import { useEffect, useState } from 'react';
import { Product } from './types/Product';
import { getAll } from './services/ProductService';

function App() {
  const [data, setData] = useState<Product[]>([]);
  const [refetchToggle, setrRefetchToggle] = useState<boolean>(false);

  useEffect(() => {
    const fetchProducts = async () => {
      const data = await getAll();
      setData(data);
    };
    fetchProducts();
  }, [refetchToggle]);

  const onSubmit = () => {
    setrRefetchToggle(!refetchToggle);
  };

  const onUpdateTranslations = () => {
    setrRefetchToggle(!refetchToggle);
  };

  return (
    <div className='App'>
      <NavBar />
      <Routes>
        <Route path='/' element={<AllProductsPage products={data} onUpdateTranslations={onUpdateTranslations} />} />
        <Route path='/single-product' element={<SingleProductPage products={data} onSubmit={onSubmit} />} />
      </Routes>
    </div>
  );
}

export default App;
