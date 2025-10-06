import React from "react";
import { useNavigate } from "react-router-dom";
import { useUser } from "../App";
import "bootstrap/dist/css/bootstrap.min.css";
import { FaClipboardList, FaFileAlt, FaSignOutAlt } from "react-icons/fa";
import "../App.css";

export default function Dashboard() {
  const navigate = useNavigate();
  const { user, logout } = useUser();

  if (!user) {
    navigate("/login");
    return null;
  }

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  const { nombre, login, idrol } = user;

  const baseBtnClasses =
    "btn w-100 mb-2 d-flex align-items-center justify-content-center gap-2";

  const renderOpciones = () => {
    switch (idrol) {
      case 1:
        return (
          <>
            <button
              className={`btn btn-primary ${baseBtnClasses}`}
              onClick={() => navigate("/ver-encuestas")}
            >
              <FaClipboardList /> Ver Encuestas
            </button>
            <button
              className={`btn btn-success ${baseBtnClasses}`}
              onClick={() => navigate("/llenar-encuesta")}
            >
              <FaFileAlt /> Llenar Encuesta
            </button>
          </>
        );
      case 2:
        return (
          <button
            className={`btn btn-primary ${baseBtnClasses}`}
            onClick={() => navigate("/ver-encuestas")}
          >
            <FaClipboardList /> Ver Encuestas
          </button>
        );
      case 3:
        return (
          <button
            className={`btn btn-success ${baseBtnClasses}`}
            onClick={() => navigate("/llenar-encuesta")}
          >
            <FaFileAlt /> Llenar Encuesta
          </button>
        );
      default:
        return (
          <p className="text-center text-danger">
            No se ha asignado un rol válido.
          </p>
        );
    }
  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-dashboard">
      <div
        className="card card-transparent shadow-lg p-4 rounded-4"
        style={{ width: "420px" }}
      >
        <h3 className="text-center mb-2 text-primary">
          Bienvenido, {nombre || login}
        </h3>
        <p className="text-center text-muted mb-4 fs-6">
          {idrol === 1
            ? "Administrador"
            : idrol === 2
            ? "Profesor"
            : "Estudiante"}
        </p>

        <div className="d-grid gap-2">{renderOpciones()}</div>

        <button
          className="btn btn-outline-secondary w-100 mt-4 d-flex align-items-center justify-content-center gap-2"
          onClick={handleLogout}
        >
          <FaSignOutAlt /> Cerrar Sesión
        </button>
      </div>
    </div>
  );
}
