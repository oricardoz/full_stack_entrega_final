import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";
import GalpaoListar from "./components/galpao/GalpaoListar";
import GalpaoInserir from "./components/galpao/GalpaoCriar";
import Login from "./components/auth/Login";
import ProdutosListar from "./components/produtos/ProdutosListar";
import ProdutoInserir from "./components/produtos/ProdutosInserir";
import ProdutoConsultar from "./components/produtos/ProdutoConsultar";
import ProdutoAlterar from "./components/produtos/ProdutoAlterar";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index path="/" element={<GalpaoListar />} />
          <Route path="/galpoes/inserir" element={<GalpaoInserir />} />
          <Route path="/galpao/produtos/:id" element={<ProdutosListar />} />
          <Route path="/produtos/inserir/:id" element={<ProdutoInserir />} />
          <Route
            path="/produtos/consultar/:id"
            element={<ProdutoConsultar />}
          />
          <Route path="/produtos/alterar/:id" element={<ProdutoAlterar />} />
          <Route path="/login" element={<Login />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
};

export default App;
