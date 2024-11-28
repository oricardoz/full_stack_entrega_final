import React, { useState, useContext } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Carregando from "../Carregando";
import { UsuarioContext } from "../../UsuarioContext";
import { httpGet } from "../../Utils/httpApi";

const ProdutoConsultar = () => {
  const [, setUsuario] = useContext(UsuarioContext);
  const [objeto, setObjeto] = useState(null);
  const [falha, setFalha] = useState(null);
  const navigate = useNavigate();

  const { id } = useParams();

  if (objeto == null) {
    httpGet(
      `produtos/${id}`,
      (dado) => {
        setObjeto(dado);
      },
      setFalha,
      setUsuario
    );
  }

  const voltar = (e) => {
    e.preventDefault();
    navigate("/galpao/produtos/" + objeto.galpaoId);
  };

  let mensagemFalha = null;

  if (falha) {
    mensagemFalha = <div className="alert alert-danger">{falha}</div>;
    setTimeout(() => {
      setFalha(null);
    }, 10000);
  }

  if (!objeto) {
    return (
      <div>
        <Carregando mensagem="" />
        {mensagemFalha}
      </div>
    );
  }

  return (
    <div className="p-2">
      <h3>Consultando Produtos</h3>
      {mensagemFalha}
      <form>
        <div>
          <label className="form-label">Nome</label>
          <input
            className="form-control"
            disabled={true}
            value={objeto.nome}
            type="text"
          />
        </div>
        <div>
          <label className="form-label">Quantidade</label>
          <input
            className="form-control"
            disabled={true}
            value={objeto.quantidade}
            type="text"
          />
        </div>
        <div>
          <label className="form-label">Valor Unitario</label>
          <input
            className="form-control"
            disabled={true}
            value={objeto.valorUnitario}
            type="text"
          />
        </div>
        <div>
          <label className="form-label">Valor Total</label>
          <input
            className="form-control"
            disabled={true}
            value={objeto.valorTotal}
            type="text"
          />
        </div>
        <button className="btn btn-secondary mt-2" onClick={(e) => voltar(e)}>
          Voltar
        </button>
      </form>
    </div>
  );
};

export default ProdutoConsultar;
