import React from "react";

export default function PublicRoute({ user, children }) {
  // Si está logueado, no renderiza el contenido público
  if (user) return <>{/* Mantener lo que ya se está mostrando */}</>;
  return children;
}
