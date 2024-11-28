import React, { useState, useContext } from "react";
import { useNavigate, useParams } from "react-router-dom"; 
import { UsuarioContext } from "../../UsuarioContext";
import { httpPost } from "../../Utils/httpApi";

const ProdutoInserir = () => {
  const [, setUsuario] = useContext(UsuarioContext);
  const { id } = useParams(); 

  const [objeto, setObjeto] = useState({
    quantidade: 0,
    nome: "",
    valorUnitario: 0,
    galpaoId: id, 
  });

  const [falha, setFalha] = useState(null);
  const navigate = useNavigate();

  const salvar = (e) => {
    e.preventDefault();
    httpPost(
      "produtos",
      objeto,
      (resp) => {
        navigate("/galpao/produtos/" + id);
      },
      setFalha,
      setUsuario
    );
  };

  const voltar = (e) => {
    e.preventDefault();
    navigate("/galpao/produtos/" + id);
  };

  const atualizarCampo = (nome, valor) => {
    let objNovo = { ...objeto };
    objNovo[nome] = valor;
    setObjeto(objNovo);
  };

  let mensagemFalha = null;

  if (falha) {
    mensagemFalha = <div className="alert alert-danger">{falha}</div>;
    setTimeout(() => {
      setFalha(null);
    }, 10000);
  }

  return (
    <div className="p-2">
      <h3>Inserindo Produto</h3>
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
            step=".1"
          />
        </div>
        <div>
          <label className="form-label">Valor Unitario</label>
          <input
            className="form-control"
            value={objeto.valorUnitario}
            onChange={(e) => atualizarCampo("valorUnitario", e.target.value)}
            type="number"
            step=".1"
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

export default ProdutoInserir;
