using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListaDeTareas.Models
{
    public class ListadoTareas
    {
        public int IdTarea { get; set; }

        public string Tarea { get; set; }

        public string DescripcionTarea { get; set; }

        public string Estado { get; set; }

        public DateTime CreacionTarea { get; set; }

    }
}