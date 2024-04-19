using System;

namespace ListaDeTareas.Models
{
    //Modelo de la BD creada en SQL Server
    public class ListadoTareas
    {
        public int IdTarea { get; set; } //Colunma del id de la tarea con sus parametros de extraccion y envio de datos

        public string Tarea { get; set; } //Colunma del nombre de la tarea con sus parametros de extraccion y envio de datos

        public string DescripcionTarea { get; set; } //Colunma de la descripcion de la tarea con sus parametros de extraccion y envio de datos

        public string Estado { get; set; } //Colunma del estado de la tarea con sus parametros de extraccion y envio de datos

        public DateTime CreacionTarea { get; set; } //Colunma de la fecha de creacion con sus parametros de extraccion y envio de datos

    }
}