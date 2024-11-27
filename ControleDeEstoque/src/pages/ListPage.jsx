import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function ListPage() {
  const [products, setProducts] = useState([]);
  const navigate = useNavigate();

  // Buscar produtos
  useEffect(() => {
    axios.get('http://sua-api.com/produtos')
      .then(response => setProducts(response.data))
      .catch(error => console.error('Erro ao buscar produtos:', error));
  }, []);

  // Excluir produto
  const handleDelete = (id) => {
    axios.delete(`http://sua-api.com/produtos/${id}`)
      .then(() => setProducts(products.filter(product => product.id !== id)))
      .catch(error => console.error('Erro ao excluir produto:', error));
  };

  return (
    <div>
      <h1>Lista de Produtos</h1>
      <button className="btn btn-primary mb-3" onClick={() => navigate('/create')}>Adicionar Produto</button>
      <table className="table">
        <thead>
          <tr>
            <th>Nome</th>
            <th>Quantidade</th>
            <th>Valor Unitário</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {products.map(product => (
            <tr key={product.id}>
              <td>{product.nome}</td>
              <td>{product.quantidade}</td>
              <td>R$ {product.valorUnitario.toFixed(2)}</td>
              <td>
                <button className="btn btn-warning me-2" onClick={() => navigate(`/edit/${product.id}`)}>Editar</button>
                <button className="btn btn-danger" onClick={() => handleDelete(product.id)}>Excluir</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ListPage;
