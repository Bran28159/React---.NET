const API_URL = "http://localhost:5159/api";

export async function getRespuestas() {
  const res = await fetch(`${API_URL}/RespuestasApi`);
  if (!res.ok) throw new Error(`Error al obtener respuestas: ${res.statusText}`);
  return await res.json();
}

export const deleteRespuesta = async (id) => {
  const res = await fetch(`http://localhost:5159/api/RespuestasApi/${id}`, {
    method: "DELETE",
  });

  if (!res.ok) throw new Error("Error al eliminar");

  // Solo intenta parsear JSON si hay contenido
  const text = await res.text();
  return text ? JSON.parse(text) : null;
};


export function exportarPDF(id) {
  window.open(`${API_URL}/RespuestasApi/exportarpdf/${id}`, "_blank");
}

export function exportarExcel(id) {
  window.open(`${API_URL}/RespuestasApi/exportarexcel/${id}`, "_blank");
}
