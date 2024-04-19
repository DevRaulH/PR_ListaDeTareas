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
    public class DataGridFilteringController : ApiController
    {

        static string cadenaConexion = "Data Source=DESKTOP-COEFNTP;Initial Catalog=listadoDeTareas;Integrated Security=true;";

        static List<ListadoTareas> olistaTarea = new List<ListadoTareas>();


        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {

            olistaTarea = new List<ListadoTareas>();



            using (SqlConnection sqlConexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand sqlCmd = new SqlCommand("select * from ListadoTareas;", sqlConexion);
                sqlCmd.CommandType = CommandType.Text;
                sqlConexion.Open();

                using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        ListadoTareas busqueda = new ListadoTareas();
                        busqueda.IdTarea = Convert.ToInt32(sqlReader["IdTarea"]);
                        busqueda.Tarea = sqlReader["Tarea"].ToString();
                        busqueda.DescripcionTarea = sqlReader["DescripcionTarea"].ToString();
                        busqueda.Estado = sqlReader["Estado"].ToString();
                        busqueda.CreacionTarea = Convert.ToDateTime(sqlReader["CreacionTarea"]);

                        olistaTarea.Add(busqueda);
                    }

                }
            }



            return Request.CreateResponse(DataSourceLoader.Load(olistaTarea, loadOptions));
        }

        






    }
}
