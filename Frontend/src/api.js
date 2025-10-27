const API_URL = "http://localhost:5159/api/UsuarioApi"; 

export async function postJson(endpoint, data) {
  const response = await fetch(`${API_URL}${endpoint}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });
  return response;
}

export async function GetUsuarios() {
  const res = await fetch(API_URL); 
  if (!res.ok) throw new Error(`Error al obtener usuarios: ${res.statusText}`);
  return await res.json();
}

export const deleteUsuario = async (id) => {
  const res = await fetch(`${API_URL}/${id}`, {
    method: "DELETE",
  });

  if (!res.ok) throw new Error("Error al eliminar usuario");

  const text = await res.text();
  return text ? JSON.parse(text) : null;
};

export async function activarUsuario(id) {
  const res = await fetch(`${API_URL}/activar/${id}`, { method: "PUT" });
  if (!res.ok) throw new Error("Error al activar usuario");
  return await res.text();
}

export async function desactivarUsuario(id) {
  const res = await fetch(`${API_URL}/desactivar/${id}`, { method: "PUT" });
  if (!res.ok) throw new Error("Error al desactivar usuario");
  return await res.text();
}
