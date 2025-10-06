import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getCombos, postEncuesta } from "../apiE";
import "bootstrap/dist/css/bootstrap.min.css";

export default function LlenarEncuesta() {
  const navigate = useNavigate();

  const [form, setForm] = useState({
    nombre: "",
    apellido: "",
    identidad: "",
    sexoClave: "",
    departamentoClave: "",
    ciudadClave: "",
    facultadClave: "",
    carreraClave: "",
    preguntaIX: "",
    matriculaClave: "",
    becadoClave: "",
    xiiClave: "",
    xiiiClave: "",
    xivClave: "",
    xvClave: "",
    xviClave: "",
    xviiClave: "",
  });

  const [catData, setCatData] = useState({
    sexos: [], departamentos: [], ciudades: [], facultades: [],
    carreras: [], matriculas: [], becados: [], xii: [], xiii: [],
    xiv: [], xv: [], xvi: [], xvii: []
  });

  const [loading, setLoading] = useState(true);
  const [mensaje, setMensaje] = useState("");

  useEffect(() => {
    const cargarCombos = async () => {
      try {
        const endpoints = [
          "sexos", "departamentos", "ciudades", "facultades",
          "carreras", "matriculas", "becados",
          "xii", "xiii", "xiv", "xv", "xvi", "xvii"
        ];

        const results = await Promise.all(
          endpoints.map(ep => getCombos(`/EncuestaApi/combos/${ep}`).catch(() => []))
        );

        setCatData({
          sexos: results[0], departamentos: results[1], ciudades: results[2],
          facultades: results[3], carreras: results[4], matriculas: results[5],
          becados: results[6], xii: results[7], xiii: results[8], xiv: results[9],
          xv: results[10], xvi: results[11], xvii: results[12]
        });

        setLoading(false);
      } catch (err) {
        console.error(err);
        setMensaje("❌ Error al cargar los combos");
        setLoading(false);
      }
    };

    cargarCombos();
  }, []);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMensaje("");

    try {
      await postEncuesta(form);
      setMensaje("✅ Encuesta enviada correctamente");
      setTimeout(() => navigate("/ver-encuestas"), 1000);
    } catch (err) {
      console.error(err);
      setMensaje("❌ Error al enviar la encuesta");
    }
  };

  const renderSelect = (label, name, options) => (
    <div className="mb-3">
      <label className="form-label fw-semibold">{label}</label>
      <select
        name={name}
        className="form-select"
        value={form[name]}
        onChange={handleChange}
      >
        <option value="">Seleccione...</option>
        {options.map(o => (
          <option key={o.clave} value={o.clave}>{o.valor}</option>
        ))}
      </select>
    </div>
  );

  if (loading) return <div className="container mt-5">Cargando encuestas...</div>;

  return (
    <div className="container mt-5" style={{ maxWidth: 700 }}>
      <div className="card shadow p-4">
        <h2 className="text-center text-primary mb-4">Llenar Encuesta</h2>
        <form onSubmit={handleSubmit}>
          <div className="row g-3">
            <div className="col-md-6">
              <div className="mb-3">
                <label className="form-label">Nombre</label>
                <input name="nombre" className="form-control form-control-lg" value={form.nombre} onChange={handleChange} />
              </div>
            </div>
            <div className="col-md-6">
              <div className="mb-3">
                <label className="form-label">Apellido</label>
                <input name="apellido" className="form-control form-control-lg" value={form.apellido} onChange={handleChange} />
              </div>
            </div>
          </div>

          <div className="mb-3">
            <label className="form-label">Identidad</label>
            <input name="identidad" className="form-control form-control-lg" value={form.identidad} onChange={handleChange} />
          </div>

          {renderSelect("Sexo", "sexoClave", catData.sexos)}
          {renderSelect("Departamento", "departamentoClave", catData.departamentos)}
          {renderSelect("Ciudad", "ciudadClave", catData.ciudades)}
          {renderSelect("Facultad", "facultadClave", catData.facultades)}
          {renderSelect("Carrera", "carreraClave", catData.carreras)}

          <div className="mb-3">
            <label className="form-label">Año de estudios</label>
            <input type="number" name="preguntaIX" className="form-control form-control-lg" value={form.preguntaIX} onChange={handleChange} />
          </div>

          {renderSelect("Matrícula", "matriculaClave", catData.matriculas)}
          {renderSelect("Becado", "becadoClave", catData.becados)}
          {renderSelect("I. Uso del sistema", "xiiClave", catData.xii)}
          {renderSelect("II. Opinión del sistema", "xiiiClave", catData.xiii)}
          {renderSelect("III. Accesibilidad del sistema", "xivClave", catData.xiv)}
          {renderSelect("IV. Cómo ayuda el sistema", "xvClave", catData.xv)}
          {renderSelect("V. Dinero ahorrado", "xviClave", catData.xvi)}
          {renderSelect("VI. Recomendaciones", "xviiClave", catData.xvii)}

          <button className="btn btn-success w-100 mt-4 btn-lg" type="submit">Enviar Encuesta</button>
        </form>

        {mensaje && <div className="alert alert-info mt-3 text-center">{mensaje}</div>}
      </div>
    </div>
  );
}
