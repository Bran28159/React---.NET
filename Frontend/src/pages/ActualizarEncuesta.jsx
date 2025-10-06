import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getCombos, getEncuesta, putEncuesta } from "../apiE";
import "bootstrap/dist/css/bootstrap.min.css";

export default function ActualizarEncuesta() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [form, setForm] = useState({
    nombre: "", apellido: "", identidad: "",
    sexoClave: "", departamentoClave: "", ciudadClave: "",
    facultadClave: "", carreraClave: "", preguntaIx: "",
    matriculaClave: "", becadoClave: "",
    xiiClave: "", xiiiClave: "", xivClave: "",
    xvClave: "", xviClave: "", xviiClave: "",
  });

  const [catData, setCatData] = useState({
    sexos: [], departamentos: [], ciudades: [], facultades: [], carreras: [],
    matriculas: [], becados: [], xii: [], xiii: [], xiv: [], xv: [], xvi: [], xvii: [],
  });

  const [loading, setLoading] = useState(true);
  const [mensaje, setMensaje] = useState("");

  useEffect(() => {
    const cargarDatos = async () => {
      setLoading(true);
      try {
        const endpoints = ["sexos","departamentos","ciudades","facultades","carreras","matriculas","becados","xii","xiii","xiv","xv","xvi","xvii"];
        const results = await Promise.all(endpoints.map(ep => getCombos(`/EncuestaApi/combos/${ep}`).catch(() => [])));

        setCatData({
          sexos: results[0], departamentos: results[1], ciudades: results[2],
          facultades: results[3], carreras: results[4], matriculas: results[5],
          becados: results[6], xii: results[7], xiii: results[8], xiv: results[9],
          xv: results[10], xvi: results[11], xvii: results[12],
        });

        const encuesta = await getEncuesta(id);
        const getKey = (obj, keys) => keys.find(k => obj[k] !== undefined && obj[k] !== null) ? keys.map(k => obj[k] ?? "")[0] : "";
        setForm({
          nombre: getKey(encuesta, ["nombre"]),
          apellido: getKey(encuesta, ["apellido"]),
          identidad: getKey(encuesta, ["identidad"]),
          sexoClave: getKey(encuesta, ["sexoClave","SexoClave","sexo_clave"])?.toString() || "",
          departamentoClave: getKey(encuesta, ["departamentoClave","DepartamentoClave"])?.toString() || "",
          ciudadClave: getKey(encuesta, ["ciudadClave","CiudadClave"])?.toString() || "",
          facultadClave: getKey(encuesta, ["facultadClave","FacultadClave"])?.toString() || "",
          carreraClave: getKey(encuesta, ["carreraClave","CarreraClave"])?.toString() || "",
          preguntaIx: encuesta?.preguntaIx ?? encuesta?.preguntaIX ?? "", // sin .toString()
          matriculaClave: getKey(encuesta, ["matriculaClave","MatriculaClave"])?.toString() || "",
          becadoClave: getKey(encuesta, ["becadoClave","BecadoClave"])?.toString() || "",
          xiiClave: getKey(encuesta, ["xiiClave","XiiClave"])?.toString() || "",
          xiiiClave: getKey(encuesta, ["xiiiClave","XiiiClave"])?.toString() || "",
          xivClave: getKey(encuesta, ["xivClave","XivClave"])?.toString() || "",
          xvClave: getKey(encuesta, ["xvClave","XvClave"])?.toString() || "",
          xviClave: getKey(encuesta, ["xviClave","XviClave"])?.toString() || "",
          xviiClave: getKey(encuesta, ["xviiClave","XviiClave"])?.toString() || "",
        });
      } catch (err) {
        console.error(err);
        setMensaje("❌ Error cargando encuesta.");
      } finally { setLoading(false); }
    };
    cargarDatos();
  }, [id]);

  const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMensaje("");
    try {
      const safeNumber = v => (v !== "" && !isNaN(v) ? Number(v) : 0);
      const original = await getEncuesta(id);
      const payload = { ...original,
        nombre: form.nombre?.trim()||null, apellido: form.apellido?.trim()||null, identidad: form.identidad?.trim()||null,
        sexoClave: safeNumber(form.sexoClave), departamentoClave: safeNumber(form.departamentoClave),
        ciudadClave: safeNumber(form.ciudadClave), facultadClave: safeNumber(form.facultadClave),
        carreraClave: safeNumber(form.carreraClave), preguntaIX: safeNumber(form.preguntaIx),
        matriculaClave: safeNumber(form.matriculaClave), becadoClave: safeNumber(form.becadoClave),
        xiiClave: safeNumber(form.xiiClave), xiiiClave: safeNumber(form.xiiiClave),
        xivClave: safeNumber(form.xivClave), xvClave: safeNumber(form.xvClave),
        xviClave: safeNumber(form.xviClave), xviiClave: safeNumber(form.xviiClave),
      };
      await putEncuesta(id,payload);
      setMensaje("✅ Encuesta actualizada correctamente");
      setTimeout(() => navigate("/ver-encuestas"), 1000);
    } catch (err) {
      console.error(err);
      setMensaje(err.response?.data?.mensaje || err.message || "❌ Error al actualizar la encuesta");
    }
  };

  const renderSelect = (label,name,options) => (
    <div className="mb-3">
      <label className="form-label">{label}</label>
      <select name={name} className="form-select" value={form[name]} onChange={handleChange}>
        <option value="">Seleccione...</option>
        {options.map(o => <option key={o.clave} value={o.clave.toString()}>{o.valor}</option>)}
      </select>
    </div>
  );

  if (loading) return <div className="container mt-5 text-center"><div className="spinner-border text-primary" role="status"><span className="visually-hidden">Cargando...</span></div></div>;

  return (
    <div className="container mt-5 mb-5" style={{ maxWidth: 750 }}>
      <div className="card shadow-lg rounded-4 p-5">
        <h3 className="text-center text-primary mb-4 fw-bold">Actualizar Encuesta</h3>

        <form onSubmit={handleSubmit}>
          <div className="row g-3">
            <div className="col-md-6">{/* Nombre */}
              <label className="form-label">Nombre</label>
              <input name="nombre" className="form-control form-control-lg" value={form.nombre} onChange={handleChange}/>
            </div>
            <div className="col-md-6">{/* Apellido */}
              <label className="form-label">Apellido</label>
              <input name="apellido" className="form-control form-control-lg" value={form.apellido} onChange={handleChange}/>
            </div>
          </div>

          <div className="mb-3 mt-3">{/* Identidad */}
            <label className="form-label">Identidad</label>
            <input name="identidad" className="form-control form-control-lg" value={form.identidad} onChange={handleChange}/>
          </div>

          <div className="row g-3">
            <div className="col-md-6">{renderSelect("Sexo","sexoClave",catData.sexos)}</div>
            <div className="col-md-6">{renderSelect("Departamento","departamentoClave",catData.departamentos)}</div>
          </div>

          <div className="row g-3">
            <div className="col-md-6">{renderSelect("Ciudad","ciudadClave",catData.ciudades)}</div>
            <div className="col-md-6">{renderSelect("Facultad","facultadClave",catData.facultades)}</div>
          </div>

          {renderSelect("Carrera","carreraClave",catData.carreras)}

          <div className="mb-3">
            <label className="form-label">Año de estudios</label>
            <input type="number" name="preguntaIx" className="form-control form-control-lg" value={form.preguntaIx} onChange={handleChange}/>
          </div>

          <div className="row g-3">
            <div className="col-md-6">{renderSelect("Matrícula","matriculaClave",catData.matriculas)}</div>
            <div className="col-md-6">{renderSelect("Becado","becadoClave",catData.becados)}</div>
          </div>

          <div className="row g-3">
            <div className="col-md-6">{renderSelect("I. Uso del sistema","xiiClave",catData.xii)}</div>
            <div className="col-md-6">{renderSelect("II. Opinión del sistema","xiiiClave",catData.xiii)}</div>
          </div>

          <div className="row g-3">
            <div className="col-md-6">{renderSelect("III. Accesibilidad del sistema","xivClave",catData.xiv)}</div>
            <div className="col-md-6">{renderSelect("IV. Cómo ayuda el sistema","xvClave",catData.xv)}</div>
          </div>

          <div className="row g-3">
            <div className="col-md-6">{renderSelect("V. Dinero ahorrado","xviClave",catData.xvi)}</div>
            <div className="col-md-6">{renderSelect("VI. Recomendaciones","xviiClave",catData.xvii)}</div>
          </div>

          <button className="btn btn-success btn-lg w-100 mt-4 shadow-sm" type="submit">Actualizar</button>
        </form>

        {mensaje && <div className="alert alert-info mt-4 text-center fs-6">{mensaje}</div>}
      </div>
    </div>
  );
}
