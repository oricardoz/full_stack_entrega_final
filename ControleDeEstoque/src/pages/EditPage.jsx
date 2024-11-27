import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import ProductForm from '../components/product/ProductForm';


function EditPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [product, setProduct] = useState(null);

  useEffect(() => {
    axios.get(`http://sua-api.com/produtos/${id}`)
      .then(response => setProduct(response.data))
      .catch(error => console.error('Erro ao carregar produto:', error));
  }, [id]);

  const handleEdit = (updatedProduct) => {
    axios.put(`http://sua-api.com/produtos/${id}`, updatedProduct)
      .then(() => navigate('/'))
      .catch(error => console.error('Erro ao atualizar produto:', error));
  };

  return (
    <div>
      <h1>Editar Produto</h1>
      {product ? <ProductForm initialData={product} onSubmit={handleEdit} /> : <p>Carregando...</p>}
    </div>
  );
}

export default EditPage;
