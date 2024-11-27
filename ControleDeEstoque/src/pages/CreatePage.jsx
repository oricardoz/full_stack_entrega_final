import React from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import ProductForm from '../components/product/ProductForm';

function CreatePage() {
  const navigate = useNavigate();

  const handleCreate = (product) => {
    axios.post('http://sua-api.com/produtos', product)
      .then(() => navigate('/'))
      .catch(error => console.error('Erro ao criar produto:', error));
  };

  return (
    <div>
      <h1>Adicionar Produto</h1>
      <ProductForm onSubmit={handleCreate} />
    </div>
  );
}

export default CreatePage;
