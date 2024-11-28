import React, { useState, useContext } from "react";
import { UsuarioContext } from "../../UsuarioContext";
import { httpPost } from "../../Utils/httpApi";

const Login = () => {
  const [, setUsuario] = useContext(UsuarioContext);
  const [objeto, setObjeto] = useState({ email: 0, senha: "" });
  const [falha, setFalha] = useState(null);

  const atualizarCampo = (nome, valor) => {
    let objNovo = { ...objeto };
    objNovo[nome] = valor;
    setObjeto(objNovo);
  };

  const sucessoLogin = (usuario) => {
    setUsuario(usuario);
  };

  const login = (e) => {
    e.preventDefault();
    httpPost("login/navegador", objeto, sucessoLogin, setFalha, setUsuario);
  };

  let mensagemFalha = null;

  if (falha) {
    mensagemFalha = <div className="alert alert-danger">{falha}</div>;
    setTimeout(() => {
      setFalha(null);
    }, 10000);
  }

  return (
    <div className="login-wrapper">
      <div className="login-container">
        {mensagemFalha}
        <div className="login-form-container">
          <form className="login-form">
            <h3 className="login-title">Login</h3>
            <div className="form-group">
              <label className="form-label">E-mail</label>
              <input
                className="form-control"
                value={objeto.matricula}
                onChange={(e) => atualizarCampo("email", e.target.value)}
                type="email"
              />
            </div>
            <div className="form-group">
              <label className="form-label">Senha</label>
              <input
                className="form-control"
                value={objeto.senha}
                onChange={(e) => atualizarCampo("senha", e.target.value)}
                type="password"
              />
            </div>
            <button className="btn btn-primary mt-2" onClick={(e) => login(e)}>
              Login
            </button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default Login;
