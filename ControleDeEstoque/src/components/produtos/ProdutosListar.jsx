import React, { useState, useEffect, useContext } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import Carregando from "../Carregando";
import { UsuarioContext } from "../../UsuarioContext";
import { httpDelete, httpGet } from "../../Utils/httpApi";

const ProdutosListar = () => {
  const [, setUsuario] = useContext(UsuarioContext);
  const [produtos, setProdutos] = useState(null);
  const [falha, setFalha] = useState(null);
  const navigate = useNavigate();
  const { id } = useParams();

  useEffect(() => {
    const carregarDados = () => {
      httpGet(
        `galpoes/${id}`,
        (dados) => setProdutos(dados.produtos),
        (erro) => setFalha(erro),
        setUsuario
      );
    };
    carregarDados();
  }, [id, setUsuario]);

  const excluir = (e, produtoId) => {
    e.preventDefault();
    httpDelete(
      `produtos`,
      produtoId,
      (_) => navigate(0), // Atualiza a página após exclusão
      (erro) => setFalha(erro),
      setUsuario
    );
  };

  if (!produtos) {
    return (
      <div>
        <Carregando mensagem="Carregando produtos..." />
        {falha && <div className="alert alert-danger">{falha}</div>}
      </div>
    );
  }

  return (
    <div className="p-2">
      <div className="d-flex justify-content-between align-items-center">
        <h1>Produtos do Galpão</h1>
      </div>
      <Link to={`/produtos/inserir/${id}`} className="btn btn-primary me-2">
        Inserir Produto
      </Link>
      <Link to={`/`} className="btn btn-primary">
        Voltar
      </Link>
      {falha && <div className="alert alert-danger">{falha}</div>}
      <table className="table">
        <thead>
          <tr>
            <th>Nome</th>
            <th>Quantidade</th>
            <th>Valor Unitário</th>
            <th>Valor Total</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {produtos.map((produto) => (
            <tr key={produto.id}>
              <td>{produto.nome}</td>
              <td>{produto.quantidade}</td>
              <td>R$ {produto.valorUnitario.toFixed(2)}</td>
              <td>R$ {produto.valorTotal.toFixed(2)}</td>
              <td>
                <Link
                  to={`/produtos/consultar/${produto.id}`}
                  className="btn btn-secondary btn-sm me-2"
                >
                  Consultar
                </Link>
                <button
                  className="btn btn-warning btn-sm me-2"
                  onClick={() => navigate(`/produtos/alterar/${produto.id}`)}
                >
                  Editar
                </button>
                <button
                  className="btn btn-danger btn-sm"
                  onClick={(e) => excluir(e, produto.id)}
                >
                  Deletar
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ProdutosListar;
