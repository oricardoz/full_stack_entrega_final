import React, { useState, useEffect, useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import Carregando from "../Carregando";
import { UsuarioContext } from "../../UsuarioContext";
import { httpDelete, httpGet } from "../../Utils/httpApi";

const GalpaoListar = () => {
  const [, setUsuario] = useContext(UsuarioContext);
  const [objetos, setObjetos] = useState(null);
  const [falha, setFalha] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const carregarDados = () => {
      httpGet(
        "galpoes",
        (dados) => setObjetos(dados),
        (erro) => setFalha(erro),
        setUsuario
      );
    };
    carregarDados();
  }, [setUsuario]);

  const excluir = (e, id) => {
    e.preventDefault();
    httpDelete(
      "galpoes",
      id,
      (_) => navigate(0),
      (erro) => setFalha(erro),
      setUsuario
    );
  };

  let mensagemFalha = null;

  if (falha) {
    mensagemFalha = <div className="alert alert-danger">{falha}</div>;
  }

  if (!objetos) {
    return (
      <div>
        <Carregando mensagem="" />
        {mensagemFalha}
      </div>
    );
  }

  return (
    <div className="p-2">
      <div className="d-flex justify-content-between">
        <h1>Galpões</h1>
      </div>
      {mensagemFalha}
      <Link to="/galpoes/inserir" className="btn btn-primary">
        Inserir
      </Link>
      <table className="table">
        <thead>
          <tr>
            <th>Nome</th>
            <th>Endereco</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {objetos.map((x) => {
            return (
              <tr key={x.id}>
                <td>{x.nome}</td>
                <td>{x.endereco}</td>
                <td>
                  <Link
                    to={`/galpao/produtos/${x.id}`}
                    className="btn btn-secondary"
                  >
                    Produtos
                  </Link>
                  <button
                    className="btn btn-danger"
                    onClick={(e) => excluir(e, x.id)}
                  >
                    Deletar
                  </button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
};

export default GalpaoListar;
