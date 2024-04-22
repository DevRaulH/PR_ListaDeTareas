----------------------------------
-- CREACION DE LA BASE DE DATOS --
----------------------------------
create database listadoDeTareas;

----------------------------------
-- USO DE LA BASE DE DATOS -------
----------------------------------
use listadoDeTareas;

--------------------------------------
-- CREACION DE LA TABLA DE ARCHIVOS --
--------------------------------------
create table ListadoTareas(
IdTarea int identity(1,1) primary key not null,
Tarea varchar (60) not null,
DescripcionTarea varchar(300) not null,
Estado varchar(15) not null,
CreacionTarea Datetime not null
);

----------------------------------
-- CONSULTA BASICA DE LA TABLA ---
----------------------------------
select * from ListadoTareas;
