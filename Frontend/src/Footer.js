import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";

export default function Footer({ data }) {
  return (
    <footer className="bg-dark text-white p-3 mt-5 shadow">
      <div className="container d-flex justify-content-between align-items-center">
        <span>{data.texto}</span>
        {data.year && <span>© {data.year}</span>}
      </div>
    </footer>
  );
}
