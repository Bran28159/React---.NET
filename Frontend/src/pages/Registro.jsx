import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { postJson } from "../api";
import "bootstrap/dist/css/bootstrap.min.css";
import { FaUserPlus } from "react-icons/fa";

export default function Registro() {
  const navigate = useNavigate();
  const [form, setForm] = useState({
    nombre: "",
    login: "",
    clave: "",
    idrol: 2, // 2 = Profesor, 3 = Estudiante
  });
  const [mensaje, setMensaje] = useState("");
  const [error, setError] = useState("");

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMensaje("");
    setError("");

    if (!form.nombre || !form.login || !form.clave) {
      setError("Todos los campos son obligatorios.");
      return;
    }

    try {
      const res = await postJson("/registrar", form);
      if (!res.ok) {
        const text = await res.text();
        setError(text || "Error al registrar usuario");
        return;
      }

      setMensaje("✅ Usuario registrado correctamente");
      setTimeout(() => navigate("/login"), 2000);
    } catch {
      setError("Error al conectar con el servidor.");
    }
  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100 bg-dashboard">
      <div className="card card-transparent shadow-lg rounded-4 p-5 text-center" style={{ width: "100%", maxWidth: "450px" }}>
        <div className="mb-4">
          <h3 className="fw-bold text-success">
            <FaUserPlus className="me-2" />
            Registro de Usuario
          </h3>
          <p className="text-muted small">Crea tu cuenta para acceder al sistema</p>
        </div>

        <form onSubmit={handleSubmit}>
          <div className="mb-3 text-start">
            <label className="form-label fw-semibold">Nombre completo</label>
            <input
              type="text"
              className="form-control"
              name="nombre"
              placeholder="Ej. Juan Pérez"
              value={form.nombre}
              onChange={handleChange}
            />
          </div>

          <div className="mb-3 text-start">
            <label className="form-label fw-semibold">Usuario</label>
            <input
              type="text"
              className="form-control"
              name="login"
              placeholder="Ej. juanperez"
              value={form.login}
              onChange={handleChange}
            />
          </div>

          <div className="mb-3 text-start">
            <label className="form-label fw-semibold">Clave</label>
            <input
              type="password"
              className="form-control"
              name="clave"
              placeholder="••••••••"
              value={form.clave}
              onChange={handleChange}
            />
          </div>

          <div className="mb-3 text-start">
            <label className="form-label fw-semibold">Rol</label>
            <select
              className="form-select"
              name="idrol"
              value={form.idrol}
              onChange={handleChange}
            >
              <option value={2}>Profesor</option>
              <option value={3}>Estudiante</option>
            </select>
          </div>

          {error && <div className="alert alert-danger py-2 small text-center">{error}</div>}
          {mensaje && <div className="alert alert-success py-2 small text-center">{mensaje}</div>}

          <button className="btn btn-success w-100 mt-3 fw-semibold d-flex align-items-center justify-content-center gap-2" type="submit">
            <FaUserPlus /> Registrarse
          </button>
        </form>

        {/* Enlace de ayuda debajo del formulario */}
        <div className="text-center mt-3">
          <small>
            ¿Ya tienes cuenta?{" "}
            <a href="/login" className="text-decoration-none fw-semibold">
              Inicia sesión
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
