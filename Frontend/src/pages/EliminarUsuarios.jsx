import React, { useEffect, useState } from "react";
import { GetUsuarios, activarUsuario, desactivarUsuario } from "../api";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";
import { useNavigate, useLocation } from "react-router-dom";
import { useUser } from "../App";
import { FaUserPlus } from "react-icons/fa";
import { GetRoles } from "../apiE";

export default function AdministrarUsuarios() {
  const [usuarios, setUsuarios] = useState([]);
  const [roles, setRoles] = useState([]);
  const [mensaje, setMensaje] = useState("");
  const navigate = useNavigate();
  const location = useLocation();
  const { user, logout } = useUser();

  const cargarDatos = async () => {
    try {
      const data = await GetUsuarios();
      const rolesData = await GetRoles();
      setUsuarios(data);
      setRoles(rolesData);
    } catch (err) {
      console.error(err);
      setMensaje("❌ Error al cargar usuarios");
    }
  };

  useEffect(() => {
    cargarDatos();
  }, []);

  // ✅ Cambiar el estado del usuario (activar/desactivar)
  const toggleEstadoUsuario = async (usuario) => {
    try {
      if (usuario.activo) {
        await desactivarUsuario(usuario.idusuario);
        setMensaje(`⚠️ Usuario ${usuario.nombre} desactivado`);
      } else {
        await activarUsuario(usuario.idusuario);
        setMensaje(`✅ Usuario ${usuario.nombre} activado`);
      }

      // Actualizar estado local
      setUsuarios((prev) =>
        prev.map((u) =>
          u.idusuario === usuario.idusuario ? { ...u, activo: !u.activo } : u
        )
      );
    } catch (err) {
      console.error(err);
      setMensaje("❌ Error al cambiar el estado del usuario");
    }
  };

  const registroBtnClass =
    location.pathname === "/registro"
      ? "btn btn-success btn-lg fw-semibold"
      : "btn btn-outline-success btn-lg fw-semibold";

  return (
    <div className="container mt-5 mb-5" style={{ maxWidth: 900 }}>
      <div className="card shadow-lg rounded-4 p-5">
        <div className="d-flex justify-content-between align-items-center mb-4">
          <h3 className="text-primary fw-bold mb-0">Administrar Usuarios</h3>
          <button
            className={`${registroBtnClass} d-flex align-items-center gap-2 shadow-sm`}
            onClick={() => navigate("/registro")}
          >
            <FaUserPlus /> Registrar usuario
          </button>
        </div>

        {mensaje && (
          <div className="alert alert-info text-center" role="alert">
            {mensaje}
          </div>
        )}

        <div className="table-responsive">
          <table className="table table-hover align-middle mb-0">
            <thead className="table-light">
              <tr className="text-center fw-semibold">
                <th style={{ width: "35%" }}>Nombre de Usuario</th>
                <th style={{ width: "35%" }}>Rol de Usuario</th>
                <th style={{ width: "30%" }}>Estado</th>
              </tr>
            </thead>
            <tbody>
              {usuarios.length > 0 ? (
                usuarios.map((usuario) => {
                  const rolNombre =
                    roles.find(
                      (r) => Number(r.idrol) === Number(usuario.idrol)
                    )?.nombre_rol || "Sin rol";

                  return (
                    <tr key={usuario.idusuario} className="text-center">
                      <td>{usuario.nombre}</td>
                      <td>{rolNombre}</td>
                      <td>
                        <div className="form-check form-switch d-flex justify-content-center">
                          <input
                            className="form-check-input"
                            type="checkbox"
                            checked={usuario.activo}
                            onChange={() => toggleEstadoUsuario(usuario)}
                            id={`switch-${usuario.idusuario}`}
                          />
                          <label
                            className="form-check-label ms-2"
                            htmlFor={`switch-${usuario.idusuario}`}
                            style={{
                              color: usuario.activo ? "green" : "red",
                              fontWeight: "500",
                            }}
                          >
                            {usuario.activo ? "Activo" : "Inactivo"}
                          </label>
                        </div>
                      </td>
                    </tr>
                  );
                })
              ) : (
                <tr>
                  <td colSpan="3" className="text-center text-muted py-3">
                    No hay usuarios registrados.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}
