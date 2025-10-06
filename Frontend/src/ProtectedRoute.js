import React from "react";

export default function ProtectedRoute({ user, roles, children }) {
  // Si no hay usuario o no tiene rol permitido, no renderiza los children
  if (!user || !roles.includes(user.idrol)) {
    return null; // mantiene la URL, no muestra nada
  }

  return <>{children}</>;
}
