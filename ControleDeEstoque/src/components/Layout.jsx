import React from 'react';
import { Link, Outlet, useNavigate } from 'react-router-dom';

function Layout() {
  const navigate = useNavigate();

  const handleLogout = () => {
    console.log('Usuário deslogado');
    navigate('/');
  };

  return (
    <div>
      {/* Menu */}
      <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
        <div className="container-fluid">
          <Link to="/" className="navbar-brand">Meu App</Link>
          <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav me-auto">
              <li className="nav-item">
                <Link to="/" className="nav-link">Home</Link>
              </li>
              <li className="nav-item">
                <Link to="/" className="nav-link">Listar Produtos</Link>
              </li>
            </ul>
            <button className="btn btn-outline-danger" onClick={handleLogout}>Sair</button>
          </div>
        </div>
      </nav>

      {/* Área de conteúdo */}
      <div className="container mt-4">
        <Outlet />
      </div>
    </div>
  );
}

export default Layout;
