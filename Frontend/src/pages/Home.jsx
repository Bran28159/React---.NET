import React from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { useUser } from "../App";
import "bootstrap/dist/css/bootstrap.min.css";
import { FaSignInAlt, FaUserPlus } from "react-icons/fa";

export default function Home() {
  const navigate = useNavigate();
  const location = useLocation(); // para saber en qué ruta estamos
  const { user, logout } = useUser();

  const handleLogout = () => {
    logout();
    localStorage.removeItem('usuarioSesion'); // elimina la sesión
    navigate("/login");
  };

  // Colores dinámicos para botones según la ruta
  const loginBtnClass = location.pathname === "/login"
    ? "btn btn-primary btn-lg fw-semibold"
    : "btn btn-outline-primary btn-lg fw-semibold";


  return (
    <div className="d-flex flex-column justify-content-center align-items-center vh-100 bg-dashboard">
      <div className="card card-transparent shadow-lg rounded-4 p-5 text-center" style={{ maxWidth: "450px" }}>
        {!user ? (
          <>
            <h2 className="fw-bold mb-3 text-primary">¡Bienvenido!</h2>
            <p className="text-muted mb-4">
              Accede con tu cuenta o crea una nueva para continuar.
            </p>

            <div className="d-grid gap-3">
              <button
                className={`${loginBtnClass} d-flex align-items-center justify-content-center gap-2`}
                onClick={() => navigate("/login")}
              >
                <FaSignInAlt /> Iniciar Sesión
              </button>
            </div>
          </>
        ) : (
          <>
            <h2 className="fw-bold text-success mb-3">
              ¡Hola, {user.nombre || user.login}!
            </h2>
            <p className="text-muted">Has iniciado sesión correctamente.</p>

            <div className="mt-4 d-grid gap-3">
              {user.idrol === 1 && (
                <button
                  className="btn btn-warning fw-semibold"
                  onClick={() => navigate("/dashboard")}
                >
                  Ir al Panel de Administración
                </button>
              )}
                {user.idrol === 2 && (
                  <button
                    className="btn btn-warning fw-semibold"
                    onClick={() => navigate("/dashboard")}
                  >
                    Ir al Panel de Estudiante
                </button>
              )}
              {user.idrol === 3 && (
                  <button
                    className="btn btn-warning fw-semibold"
                    onClick={() => navigate("/dashboard")}
                  >
                    Ir al Panel de Profesor
                  </button>
              )}
              <button
                className="btn btn-outline-danger fw-semibold"
                onClick={handleLogout}
              >
                Cerrar sesión
              </button>
            </div>
          </>
        )}
      </div>
    </div>
  );
}
