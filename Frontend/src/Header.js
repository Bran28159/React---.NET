import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";

export default function Header({ data }) {
  return (
    <header className="bg-primary text-white p-3 mb-4 shadow">
      <div className="container d-flex justify-content-between align-items-center">
        <h1 className="h4 m-0">{data.titulo}</h1>
        {data.subtitulo && <span>{data.subtitulo}</span>}
      </div>
    </header>
  );
}
