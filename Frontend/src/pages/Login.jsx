import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { postJson } from "../api";
import { useUser } from "../App";
import "bootstrap/dist/css/bootstrap.min.css";
import { FaUser, FaLock } from "react-icons/fa";

export default function Login() {
  const { setUser } = useUser();
  const [usuario, setUsuario] = useState("");
  const [clave, setClave] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    if (!usuario || !clave) {
      setError("Por favor ingresa usuario y clave.");
      return;
    }

   try {
  const res = await postJson("/login", { Usuario: usuario, Clave: clave });
  if (!res.ok) {
    setError("Usuario o clave incorrectos");
    return;
  }

  const data = await res.json();

  const expiracion = Date.now() + 5 * 60 * 1000; // 5 minutos

  const usuarioSesion = {
    ...data,
    expiracion,
  };

  // Guardar en sessionStorage
  sessionStorage.setItem("usuarioSesion", JSON.stringify(usuarioSesion));

  // Actualizar el contexto global para que se active el temporizador de expiración
  setUser(usuarioSesion);

  // Redirigir
  navigate("/dashboard");
} catch (err) {
  setError("Error al conectar con el servidor");
  console.error(err);
}
  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-dashboard">
      <div className="card card-transparent shadow-lg p-4 rounded-4" style={{ width: "100%", maxWidth: "400px" }}>
        <div className="text-center mb-4">
          <h3 className="fw-bold text-primary">Iniciar Sesión</h3>
          <p className="text-muted small">Ingresa tus credenciales para continuar</p>
        </div>

        <form onSubmit={handleSubmit}>
          <div className="mb-3 position-relative">
            <label className="form-label fw-semibold">Usuario</label>
            <div className="input-group">
              <span className="input-group-text bg-white border-end-0"><FaUser /></span>
              <input
                type="text"
                className="form-control border-start-0"
                placeholder="Tu nombre de usuario"
                value={usuario}
                onChange={(e) => setUsuario(e.target.value)}
              />
            </div>
          </div>

          <div className="mb-3 position-relative">
            <label className="form-label fw-semibold">Clave</label>
            <div className="input-group">
              <span className="input-group-text bg-white border-end-0"><FaLock /></span>
              <input
                type="password"
                className="form-control border-start-0"
                placeholder="••••••••"
                value={clave}
                onChange={(e) => setClave(e.target.value)}
              />
            </div>
          </div>

          {error && (
            <div className="alert alert-danger py-2 small text-center">{error}</div>
          )}

          <button type="submit" className="btn btn-primary w-100 mt-3 fw-semibold">
            Ingresar
          </button>
        </form>

        {/* Enlace de ayuda debajo del formulario */}
        <div className="text-center mt-3">
          <small>
            ¿Aún no tienes cuenta?{" "}
            <a href="/registro" className="text-decoration-none fw-semibold">
              Regístrate
            </a>
          </small>
        </div>

        <div className="text-center mt-3">
          <small className="text-muted">© 2025 Proyecto Web Final</small>
        </div>
      </div>
    </div>
  );
}
