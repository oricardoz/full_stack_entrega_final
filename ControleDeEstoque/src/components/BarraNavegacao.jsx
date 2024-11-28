import React from "react";
import { Link } from "react-router-dom";
import Logout from "./auth/Logout";

const BarraNavegacao = ({ setFalha }) => {
  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary shadow-sm">
      <div className="container-fluid d-flex justify-content-between align-items-center">
        <h3 className="text-white m-0">Gestão de Galpões</h3>
        <div className="d-flex align-items-center">
          <Link className="navbar-brand text-white mx-3" to="/">
            Galpões
          </Link>
          <Logout setFalha={setFalha} />
        </div>
      </div>
    </nav>
  );
};

export default BarraNavegacao;
