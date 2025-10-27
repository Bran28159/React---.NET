const API_URL = "http://localhost:5159/api"; // sin barra al final

export async function getCombos(endpoint) {
  const res = await fetch(`${API_URL}${endpoint}`);
  if (!res.ok) throw new Error(`Error al obtener combos ${endpoint}: ${res.statusText}`);
  return await res.json();
}

export async function GetRoles() {
  const res = await fetch(`${API_URL}/RolesApi`); // correcto
  if (!res.ok) throw new Error(`Error al obtener roles: ${res.statusText}`);
  return await res.json();
}
// Funciones para RespuestasApi
export async function getRespuesta(id) {
  const res = await fetch(`${API_URL}/RespuestasApi/${id}`);
  if (!res.ok) throw new Error(`Error al obtener respuesta ${id}: ${res.statusText}`);
  return await res.json();
}

export async function putRespuesta(id, payload) {
  const res = await fetch(`${API_URL}/RespuestasApi/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });
  if (!res.ok) throw new Error(`Error al actualizar respuesta ${id}: ${res.statusText}`);
  return await res.json();
}

// Mantener tus funciones originales si las necesitas
export async function getEncuesta(id) {
  const res = await fetch(`${API_URL}/EncuestaApi/${id}`);
  if (!res.ok) throw new Error(`Error al obtener encuesta ${id}: ${res.statusText}`);
  return await res.json();
}

export async function postEncuesta(payload) {
  const res = await fetch(`${API_URL}/EncuestaApi`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });
  if (!res.ok) throw new Error(`Error al enviar encuesta: ${res.statusText}`);
  return await res.json();
}

export async function putEncuesta(id, payload) {
  const res = await fetch(`${API_URL}/EncuestaApi/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });
  if (!res.ok) throw new Error(`Error al actualizar encuesta ${id}: ${res.statusText}`);
  return await res.json();
}
