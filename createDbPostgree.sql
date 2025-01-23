-- Crear la base de datos
CREATE DATABASE RestauranteDB;

-- Seleccionar información de la base de datos
-- PostgreSQL no tiene una vista como sys.databases directamente. Puedes consultar información en pg_database:
SELECT datname, oid, pg_catalog.pg_database_size(datname) AS size_bytes
FROM pg_database
WHERE datname = 'restaurantedb';

-- Usar la base de datos
\c RestauranteDB

-- Crear la tabla PlatoPrincipal
CREATE TABLE PlatoPrincipal (
    Id SERIAL PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Precio NUMERIC(10, 2) NOT NULL CHECK (Precio >= 0),
    Ingredientes TEXT NOT NULL
);

-- Insertar datos en la tabla
INSERT INTO PlatoPrincipal (Nombre, Precio, Ingredientes)
VALUES 
('Plato combinado', 12.50, 'Pollo, patatas, tomate'),
('Plato vegetariano', 10.00, 'Tofu, verduras, arroz');

-- Consultar todos los datos de la tabla
SELECT * FROM PlatoPrincipal;

-- Consultar registros que contengan 'Tofu' en los ingredientes
SELECT * 
FROM PlatoPrincipal
WHERE Ingredientes LIKE '%Tofu%';

-- Consultar registros ordenados por precio de forma ascendente
SELECT * 
FROM PlatoPrincipal
ORDER BY Precio ASC;

-- Consultar nombres y precios distintos
SELECT DISTINCT Nombre, Precio FROM PlatoPrincipal;
