import React, { useState, useContext } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Carregando from "../Carregando";
import { UsuarioContext } from "../../UsuarioContext";
import { httpGet, httpPut } from "../../Utils/httpApi";

const ProdutoAlterar = () => {
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

  const atualizarCampo = (nome, valor) => {
    let objNovo = { ...objeto };
    objNovo[nome] = valor;
    setObjeto(objNovo);
  };

  const salvar = (e) => {
    e.preventDefault();
    objeto.valorTotal = objeto.quantidade * objeto.valorUnitario;
    objeto.galpaoId = objeto.galpaoId;
    httpPut(
      `produtos/${id}`,
      id,
      objeto,
      (_) => {
        navigate("/galpao/produtos/" + objeto.galpaoId);
      },
      setFalha,
      setUsuario
    );
  };

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
      <h3>Alterando Produto</h3>
      {mensagemFalha}
      <form>
        <div>
          <label className="form-label">Nome</label>
          <input
            className="form-control"
            value={objeto.nome}
            onChange={(e) => atualizarCampo("nome", e.target.value)}
            type="text"
          />
        </div>
        <div>
          <label className="form-label">Quantidade</label>
          <input
            className="form-control"
            value={objeto.quantidade}
            onChange={(e) => atualizarCampo("quantidade", e.target.value)}
            type="number"
            step="0.1"
          />
        </div>
        <div>
          <label className="form-label">Valor Unitario</label>
          <input
            className="form-control"
            value={objeto.valorUnitario}
            onChange={(e) => atualizarCampo("valorUnitario", e.target.value)}
            type="number"
            step="0.1"
          />
        </div>
        <button className="btn btn-primary mt-2" onClick={(e) => salvar(e)}>
          Salvar
        </button>
        <button className="btn btn-secondary mt-2" onClick={(e) => voltar(e)}>
          Voltar
        </button>
      </form>
    </div>
  );
};

export default ProdutoAlterar;
