import React, { createContext, useContext, useState, useEffect } from "react";
import {
  BrowserRouter,
  Routes,
  Route,
  Link,
  useNavigate,
  useLocation,
} from "react-router-dom";

import Home from "./pages/Home";
import Login from "./pages/Login";
import Registro from "./pages/Registro";
import Dashboard from "./pages/Dashboard";
import LlenarEncuesta from "./pages/LlenarEncuesta";
import VerEncuestas from "./pages/VerEncuestas";
import ActualizarEncuesta from "./pages/ActualizarEncuesta";
import EliminarUsuarios from "./pages/EliminarUsuarios";

import PublicRoute from "./PublicRoute";
import ProtectedRoute from "./ProtectedRoute";

import "./App.css";

// Import Bootstrap CSS y JS bundle (necesario para navbar colapsable)
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min.js";

/* ======================
   Contexto global de usuario
   ====================== */
const UserContext = createContext();

export function UserProvider({ children }) {
  const [user, setUser] = useState(() => {
    try {
      const storedUser = JSON.parse(localStorage.getItem("usuarioSesion"));
      if (storedUser && storedUser.expiracion > Date.now()) return storedUser;
    } catch (e) {
      console.warn("usuarioSesion no válido en localStorage", e);
    }
    return null;
  });

  const [sessionExpired, setSessionExpired] = useState(false);

  const logout = () => {
    sessionStorage.removeItem("usuarioSesion");
       localStorage.removeItem('usuarioSesion'); // elimina la sesión
    setUser(null);
  };

  useEffect(() => {
    let timer;
    if (!user || !user.expiracion) return () => clearTimeout(timer);

    const ahora = Date.now();

    if (ahora >= user.expiracion) {
      setSessionExpired(true);
      logout();
      return;
    }

    const tiempoRestante = user.expiracion - ahora;
    timer = setTimeout(() => {
      setSessionExpired(true);
      logout();
    }, tiempoRestante);

    return () => clearTimeout(timer);
  }, [user]);

  const clearSessionExpired = () => setSessionExpired(false);

  return (
    <UserContext.Provider
      value={{ user, setUser, logout, sessionExpired, clearSessionExpired }}
    >
      {children}
    </UserContext.Provider>
  );
}

export const useUser = () => useContext(UserContext);

/* ======================
   Navbar
   ====================== */
function Navbar() {
  const { user, logout } = useUser();
  const navigate = useNavigate();
  const location = useLocation();

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  const getLinkClass = (path) =>
    location.pathname === path
      ? "btn btn-primary btn-sm px-4"
      : "btn btn-outline-primary btn-sm px-4";

  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-white shadow-sm sticky-top">
      <div className="container">
        <Link className="navbar-brand fw-bold text-primary" to="/">
          EncuestasApp
        </Link>
        <button
          className="navbar-toggler border-0"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav ms-auto align-items-lg-center">
            {!user && (
              <>
                <li className="nav-item me-2">
                  <Link className={getLinkClass("/login")} to="/login">
                    Iniciar Sesión
                  </Link>
                </li>
                <li className="nav-item">
                  <Link className={getLinkClass("/registro")} to="/registro">
                    Registro
                  </Link>
                </li>
              </>
            )}

            {user && (
              <>
                {user.idrol === 1 && (
                  <>
                    <li className="nav-item me-2">
                      <Link className="nav-link text-secondary" to="/dashboard">
                        Panel Admin
                      </Link>
                    </li>
                    <li className="nav-item me-2">
                      <Link className="nav-link text-secondary" to="/llenar-encuesta">
                        Llenar Encuesta
                      </Link>
                    </li>
                    <li className="nav-item me-2">
                      <Link className="nav-link text-secondary" to="/ver-encuestas">
                        Ver Encuestas
                      </Link>
                    </li>
                        <li className="nav-item me-2">
                      <Link className="nav-link text-secondary" to="/eliminar-usuarios">
                        Administrar Usuarios
                      </Link>
                    </li>
                  </>
                )}

                {user.idrol === 2 && (
                  <>
                    <li className="nav-item me-2">
                      <Link className="nav-link text-secondary" to="/dashboard">
                        Panel Estudiante
                      </Link>
                    </li>
                    <li className="nav-item me-2">
                      <Link className="nav-link text-secondary" to="/llenar-encuesta">
                        Llenar Encuesta
                      </Link>
                    </li>
                  </>
                )}

                {user.idrol === 3 && (
                  <>
                    <li className="nav-item me-2">
                      <Link className="nav-link text-secondary" to="/dashboard">
                        Panel Docente
                      </Link>
                    </li>
                    <li className="nav-item me-2">
                      <Link className="nav-link text-secondary" to="/ver-encuestas">
                        Ver Encuestas
                      </Link>
                    </li>
                  </>
                )}

                <li className="nav-item">
                  <button
                    className="btn btn-outline-primary btn-sm px-3"
                    onClick={handleLogout}
                  >
                    Cerrar Sesión
                  </button>
                </li>
              </>
            )}
          </ul>
        </div>
      </div>
    </nav>
  );
}

/* ======================
   Header y Footer
   ====================== */
function Header({ data }) {
  return (
    <header className="bg-primary text-white py-3 shadow-sm mb-4">
      <div className="container text-center">
        <h1 className="fw-bold">{data.titulo}</h1>
        {data.subtitulo && <p className="mb-0">{data.subtitulo}</p>}
      </div>
    </header>
  );
}

function Footer({ data }) {
  return (
    <footer className="bg-light text-center py-3 mt-5 shadow-top">
      <div className="container">
        <small className="text-muted">
          {data.texto} &copy; {data.year}
        </small>
      </div>
    </footer>
  );
}

/* ======================
   Modal global de sesión expirada
   ====================== */
function SesionExpiradaModal() {
  const { sessionExpired, clearSessionExpired } = useUser();
  const navigate = useNavigate();

  const handleCerrar = () => {
    clearSessionExpired();
    navigate("/login");
  };

  if (!sessionExpired) return null;

  return (
    <div
      className="modal fade show"
      style={{ display: "block", background: "rgba(0,0,0,0.5)", zIndex: 2000 }}
    >
      <div className="modal-dialog modal-dialog-centered">
        <div className="modal-content rounded-4 shadow">
          <div className="modal-header bg-warning text-dark">
            <h5 className="modal-title">⏰ Sesión expirada</h5>
          </div>
          <div className="modal-body text-center">
            <p className="fs-5 mb-0">
              Tu sesión ha expirado por inactividad.<br />
              Por favor, inicia sesión nuevamente.
            </p>
          </div>
          <div className="modal-footer justify-content-center">
            <button className="btn btn-primary px-4" onClick={handleCerrar}>
              Iniciar sesión
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

/* ======================
   App principal
   ====================== */
const headerData = { titulo: "Sistema de Encuestas", subtitulo: "Bienvenido a la plataforma" };
const footerData = { texto: "Proyecto Web Final", year: 2025 };

export default function App() {
  return (
    <UserProvider>
      <BrowserRouter>
        <SesionExpiradaModal />
        <Navbar />
        <Header data={headerData} />

        <div className="container mt-4">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/login" element={<PublicRouteWrapper><Login /></PublicRouteWrapper>} />
            <Route path="/registro" element={<ProtectedRouteWrapper roles={[1]}><Registro /></ProtectedRouteWrapper>} />
            <Route path="/dashboard" element={<ProtectedRouteWrapper roles={[1,2,3]}><Dashboard /></ProtectedRouteWrapper>} />
            <Route path="/llenar-encuesta" element={<ProtectedRouteWrapper roles={[1,2]}><LlenarEncuesta /></ProtectedRouteWrapper>} />
            <Route path="/ver-encuestas" element={<ProtectedRouteWrapper roles={[1,3]}><VerEncuestas /></ProtectedRouteWrapper>} />
            <Route path="/actualizar/:id" element={<ProtectedRouteWrapper roles={[1,3]}><ActualizarEncuesta /></ProtectedRouteWrapper>} />
            <Route path="/eliminar-usuarios" element={<ProtectedRouteWrapper roles={[1]}><EliminarUsuarios /></ProtectedRouteWrapper>} />
          </Routes>
        </div>

        <Footer data={footerData} />
      </BrowserRouter>
    </UserProvider>
  );
}

/* ======================
   Wrappers
   ====================== */
function PublicRouteWrapper({ children }) {
  const { user } = useUser();
  return <PublicRoute user={user}>{children}</PublicRoute>;
}

function ProtectedRouteWrapper({ children, roles }) {
  const { user } = useUser();
  return <ProtectedRoute user={user} roles={roles}>{children}</ProtectedRoute>;
}
