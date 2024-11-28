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
    <div className="login-wrapper d-flex align-items-start justify-content-center vh-100 pt-5">
      <div
        className="login-container bg-light p-5 rounded shadow-lg"
        style={{ width: "400px" }}
      >
        {mensagemFalha}
        <div className="login-form-container">
          <form className="login-form">
            <h3 className="login-title text-center mb-4">Login</h3>
            <div className="form-group mb-3">
              <label className="form-label">E-mail</label>
              <input
                className="form-control"
                value={objeto.matricula}
                onChange={(e) => atualizarCampo("email", e.target.value)}
                type="email"
                placeholder="Digite seu e-mail"
              />
            </div>
            <div className="form-group mb-4">
              <label className="form-label">Senha</label>
              <input
                className="form-control"
                value={objeto.senha}
                onChange={(e) => atualizarCampo("senha", e.target.value)}
                type="password"
                placeholder="Digite sua senha"
              />
            </div>
            <button className="btn btn-primary w-100" onClick={(e) => login(e)}>
              Login
            </button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default Login;
