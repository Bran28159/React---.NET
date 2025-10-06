import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useUser } from "../App"; // usa tu contexto global de usuario
import "bootstrap/dist/css/bootstrap.min.css";

export default function SesionExpirada() {
  const { user, logout } = useUser();
  const navigate = useNavigate();
  const [mostrarModal, setMostrarModal] = useState(false);

  useEffect(() => {
    if (!user) return;

    const ahora = Date.now();
    if (user.expiracion && ahora > user.expiracion) {
      logout();
      setMostrarModal(true);
      return;
    }

    const timeout = user.expiracion - ahora;
    const timer = setTimeout(() => {
      logout();
      setMostrarModal(true);
    }, timeout);

    return () => clearTimeout(timer);
  }, [user, logout]);

  const handleCerrar = () => {
    setMostrarModal(false);
    navigate("/login");
  };

  if (!mostrarModal) return null;

  return (
    <div
      className="modal fade show"
      style={{
        display: "block",
        background: "rgba(0,0,0,0.5)",
        zIndex: 2000,
      }}
    >
      <div className="modal-dialog modal-dialog-centered">
        <div className="modal-content rounded-4 shadow">
          <div className="modal-header bg-warning text-dark">
            <h5 className="modal-title">⏰ Sesión expirada</h5>
          </div>
          <div className="modal-body text-center">
            <p className="fs-5">
              Tu sesión ha expirado por inactividad.  
              Por favor, inicia sesión nuevamente.
            </p>
          </div>
          <div className="modal-footer justify-content-center">
            <button className="btn btn-primary px-4" onClick={handleCerrar}>
              Iniciar sesión
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
