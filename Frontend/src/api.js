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
