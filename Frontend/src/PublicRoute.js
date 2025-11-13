import React from "react";

export default function PublicRoute({ user, children }) {

  if (user) return <>{/* Mantener lo que ya se está mostrando */}</>;
  return children;
}
