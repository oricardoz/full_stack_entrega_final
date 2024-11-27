import React, { useState } from 'react';

function ProductForm({ onSubmit, initialData = {} }) {
  const [nome, setNome] = useState(initialData.nome || '');
  const [quantidade, setQuantidade] = useState(initialData.quantidade || '');
  const [valorUnitario, setValorUnitario] = useState(initialData.valorUnitario || '');

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit({ nome, quantidade, valorUnitario: parseFloat(valorUnitario) });
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="mb-3">
        <label className="form-label">Nome</label>
        <input type="text" className="form-control" value={nome} onChange={(e) => setNome(e.target.value)} required />
      </div>
      <div className="mb-3">
        <label className="form-label">Quantidade</label>
        <input type="number" className="form-control" value={quantidade} onChange={(e) => setQuantidade(e.target.value)} required />
      </div>
      <div className="mb-3">
        <label className="form-label">Valor Unitário</label>
        <input type="number" className="form-control" step="0.01" value={valorUnitario} onChange={(e) => setValorUnitario(e.target.value)} required />
      </div>
      <button type="submit" className="btn btn-success">Salvar</button>
    </form>
  );
}

export default ProductForm;
