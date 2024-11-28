import React, { useContext } from "react";
import { UsuarioContext } from "../../UsuarioContext";
import { httpGet } from "../../Utils/httpApi";

const Logout = ({ setFalha }) => {
  const [usuario, setUsuario] = useContext(UsuarioContext);
  const realizarLogout = () => {
    httpGet(
      "login/logout",
      () => {
        setUsuario(null);
      },
      setFalha,
      setUsuario
    );
  };

  var nomeUsuario = usuario ? usuario.nome : null;

  if (nomeUsuario) {
    return (
      <div style={{ display: "flex", alignItems: "center" }}>
        <label style={{ marginRight: "10px", color: "white" }}>
          Usuário: {nomeUsuario}
        </label>
        <button
          className="btn btn-primary m-2"
          onClick={realizarLogout}
          style={{ padding: "5px 10px", cursor: "pointer" }}
        >
          Logout
        </button>
      </div>
    );
  } else {
    return null;
  }
};

export default Logout;
