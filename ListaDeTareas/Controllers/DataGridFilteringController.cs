using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ListaDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net;
using System.Threading;
using System.Collections;
using System.Linq;

namespace ListaDeTareasCSHARP.Controllers
{
    //Controlador API por defecto del Framework de DevExpress
    public class DataGridFilteringController : ApiController
    {
        //Cadena de conexion con la BD SQL Server con los parametros, solo esta visible para las prueba debe ocultarse
        static string cadenaConexion = "Data Source=DESKTOP-COEFNTP;Initial Catalog=listadoDeTareas;Integrated Security=true;";

        //Una lista de objetos para su uso y manipulacion de los datos que se vayan a iterar
        static List<ListadoTareas> olistaTarea = new List<ListadoTareas>();

        //Funcuion de Carga del DataSource para la comunicacion con la pagina Index
        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            //Uso del objeto de lista para el manejo de los datos
            olistaTarea = new List<ListadoTareas>();


            //Uso del metodo using para abrir y cerrar la "sesion o conexion"
            //Conexion con la BD
            using (SqlConnection sqlConexion = new SqlConnection(cadenaConexion))
            {
                //Uso del SqlCommand para enviar los comandos o consultas del lenguaje SQL + la cadena de conexión
                SqlCommand sqlCmd = new SqlCommand("select * from ListadoTareas;", sqlConexion);
                //Se especifica que el comando es de tipo texto
                sqlCmd.CommandType = CommandType.Text;
                //Se abre la conexion a la BD
                sqlConexion.Open();

                //Uso del metodo using para abrir y cerrar la "ejecucion" del reader
                using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                {
                    //Uso de un ciclo para la lectura con el Reader mientras se almacena los valores encontrados
                    while (sqlReader.Read())
                    {
                        //Uso de una lista para almacenar los datos encontrados
                        ListadoTareas busqueda = new ListadoTareas();
                        busqueda.IdTarea = Convert.ToInt32(sqlReader["IdTarea"]);
                        busqueda.Tarea = sqlReader["Tarea"].ToString();
                        busqueda.DescripcionTarea = sqlReader["DescripcionTarea"].ToString();
                        busqueda.Estado = sqlReader["Estado"].ToString();
                        busqueda.CreacionTarea = Convert.ToDateTime(sqlReader["CreacionTarea"]);

                        //Añadir todos los datos encontrados con el Reader al objeto tipo lista
                        olistaTarea.Add(busqueda);
                    }

                }//Se cierra la conexion del Reader
            }//Se cierra la conexion a la BD


            //Retornar los valores al Datasoruce cargando los datos en la vista Index
            return Request.CreateResponse(DataSourceLoader.Load(olistaTarea, loadOptions));
        }

        






    }
}
