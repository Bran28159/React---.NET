import React from "react";
import { useNavigate } from "react-router-dom";

export default function ProtectedRoute({ user, roles, children }) {
  const navigate = useNavigate();

  // Si no hay usuario o no tiene rol permitido, no renderiza los children
  if (!user || !roles.includes(user.idrol)) {
      navigate("/dashboard");
  }

  return <>{children}</>;
}
