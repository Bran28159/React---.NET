import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  getRespuestas,
  deleteRespuesta,
  exportarPDF,
  exportarExcel,
} from "../apiR";
import "bootstrap/dist/css/bootstrap.min.css";
import { FaFilePdf, FaFileExcel, FaEdit, FaTrash } from "react-icons/fa";

export default function VerEncuestas() {
  const [respuestas, setRespuestas] = useState([]);
  const [mensaje, setMensaje] = useState("");
  const [mostrarModal, setMostrarModal] = useState(false);
  const [idAEliminar, setIdAEliminar] = useState(null);
  const navigate = useNavigate();

  const cargarDatos = async () => {
    try {
      const data = await getRespuestas();
      setRespuestas(data);
    } catch (err) {
      console.error("Error al cargar encuestas:", err);
      setMensaje("❌ Error al cargar encuestas");
    }
  };

  useEffect(() => {
    cargarDatos();
  }, []);

  // Abre el modal de confirmación
  const eliminar = (id) => {
    setIdAEliminar(id);
    setMostrarModal(true);
  };

  // Confirma y ejecuta la eliminación real
  const confirmarEliminacion = async () => {
    try {
      await deleteRespuesta(idAEliminar);
      setMensaje("✅ Eliminada correctamente");
      setMostrarModal(false);
      cargarDatos();
    } catch (err) {
      console.error("Error al eliminar:", err);
      setMensaje("❌ Error al eliminar");
      setMostrarModal(false);
      cargarDatos();
    }
  };

  const exportarTodoPDF = () =>
    window.open(
      "http://localhost:5159/api/RespuestasApi/exportartodospdf",
      "_blank"
    );
  const exportarTodoExcel = () =>
    window.open(
      "http://localhost:5159/api/RespuestasApi/exportartodosexcel",
      "_blank"
    );

  return (
    <div className="container mt-5">
      <div className="card shadow-lg rounded-4 p-4">
        {/* Encabezado */}
        <div className="d-flex justify-content-between align-items-center mb-4 flex-wrap gap-2">
          <h3 className="text-primary mb-0">Lista de Encuestas</h3>
          <div className="d-flex gap-2 flex-wrap">
            <button
              className="btn btn-danger d-flex align-items-center gap-1"
              onClick={exportarTodoPDF}
            >
              <FaFilePdf /> Exportar todas PDF
            </button>
            <button
              className="btn btn-success d-flex align-items-center gap-1"
              onClick={exportarTodoExcel}
            >
              <FaFileExcel /> Exportar todas Excel
            </button>
          </div>
        </div>

        {/* Mensaje */}
        {mensaje && (
          <div
            className={`alert ${
              mensaje.includes("✅") ? "alert-success" : "alert-danger"
            } text-center fw-bold rounded-4 shadow-sm`}
          >
            {mensaje}
          </div>
        )}

        {/* Tabla */}
        <div className="table-responsive shadow rounded">
          <table className="table table-hover table-bordered align-middle text-center mb-0">
            <thead className="table-dark">
              <tr>
                <th>#</th>
                <th>Nombre</th>
                <th>Apellido</th>
                <th>Identidad</th>
                <th>Sexo</th>
                <th>Departamento</th>
                <th>Ciudad</th>
                <th>Facultad</th>
                <th>Carrera</th>
                <th>Año</th>
                <th>Matrícula</th>
                <th>Becado</th>
                <th>I</th>
                <th>II</th>
                <th>III</th>
                <th>IV</th>
                <th>V</th>
                <th>VI</th>
                <th>Acciones</th>
              </tr>
            </thead>
            <tbody>
              {respuestas.length ? (
                respuestas.map((r) => (
                  <tr key={r.numero}>
                    <td>{r.numero}</td>
                    <td>{r.nombre}</td>
                    <td>{r.apellido}</td>
                    <td>{r.identidad}</td>
                    <td>{r.sexo}</td>
                    <td>{r.departamento}</td>
                    <td>{r.ciudad}</td>
                    <td>{r.facultad}</td>
                    <td>{r.carrera}</td>
                    <td>{r.preguntaIX}</td>
                    <td>{r.matricula}</td>
                    <td>{r.becado}</td>
                    <td>{r.xii}</td>
                    <td>{r.xiii}</td>
                    <td>{r.xiv}</td>
                    <td>{r.xv}</td>
                    <td>{r.xvi}</td>
                    <td>{r.xvii}</td>
                    <td>
                      <div className="d-flex flex-wrap justify-content-center gap-1">
                        <button
  className="btn btn-sm btn-warning"
  onClick={() => navigate(`/actualizar/${r.numero}`)}
  title="Editar encuesta"
>
  <FaEdit />
</button>
<button
  className="btn btn-sm btn-primary"
  onClick={() => exportarPDF(r.numero)}
  title="Exportar PDF"
>
  <FaFilePdf />
</button>
<button
  className="btn btn-sm btn-success"
  onClick={() => exportarExcel(r.numero)}
  title="Exportar Excel"
>
  <FaFileExcel />
</button>
<button
  className="btn btn-sm btn-danger"
  onClick={() => eliminar(r.numero)}
  title="Eliminar encuesta"
>
  <FaTrash />
</button>

                      </div>
                    </td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan="19" className="text-center py-3 text-muted">
                    No hay encuestas registradas
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </div>

      {/* Modal de confirmación */}
      {mostrarModal && (
        <div
          className="modal fade show"
          style={{ display: "block", background: "rgba(0,0,0,0.5)" }}
        >
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content rounded-4 shadow">
              <div className="modal-header bg-danger text-white">
                <h5 className="modal-title">Confirmar eliminación</h5>
                <button
                  type="button"
                  className="btn-close"
                  onClick={() => setMostrarModal(false)}
                ></button>
              </div>
              <div className="modal-body text-center">
                <p className="fs-5">
                  ¿Seguro que deseas eliminar esta encuesta?
                </p>
              </div>
              <div className="modal-footer justify-content-center">
                <button
                  className="btn btn-secondary px-4"
                  onClick={() => setMostrarModal(false)}
                >
                  Cancelar
                </button>
                <button
                  className="btn btn-danger px-4"
                  onClick={confirmarEliminacion}
                >
                  Eliminar
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
