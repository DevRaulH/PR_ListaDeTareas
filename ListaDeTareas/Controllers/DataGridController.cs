using ListaDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;

namespace ListaDeTareas.Controllers
{
    public class DataGridController : Controller
    {

        static string cadenaConexion = "Data Source=DESKTOP-COEFNTP;Initial Catalog=listadoDeTareas;Integrated Security=true;";

        static List<ListadoTareas> olistaTarea = new List<ListadoTareas>();


        // GET: DataGrid
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult agregar()
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
            return View(olistaTarea);
        }

        public ActionResult editar(int IdTarea, string Tarea, string DescripcionTarea, string Estado, DateTime CreacionTarea)
        {

            olistaTarea = new List<ListadoTareas>();

            ListadoTareas busqueda = new ListadoTareas();
            busqueda.IdTarea = IdTarea;
            busqueda.Tarea = Tarea;
            busqueda.DescripcionTarea = DescripcionTarea;
            busqueda.Estado = Estado;
            busqueda.CreacionTarea = CreacionTarea;

            olistaTarea.Add(busqueda);


            return View(olistaTarea);
        }

        [HttpPost]
        public ActionResult AgregarTarea(string Tarea, string DescripcionTarea)
        {
            string estado = "Pendiente";

            DateTime creaciontarea = DateTime.Now;

            using (SqlConnection sqlConexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand sqlCmd = new SqlCommand("insert into ListadoTareas(Tarea,DescripcionTarea,Estado,CreacionTarea) values (@nombre,@descripcion,@estado,@creacion)", sqlConexion);
                sqlCmd.Parameters.AddWithValue("@nombre", Tarea);
                sqlCmd.Parameters.AddWithValue("@descripcion", DescripcionTarea);
                sqlCmd.Parameters.AddWithValue("@estado", estado);
                sqlCmd.Parameters.AddWithValue("@creacion", creaciontarea);
                sqlCmd.CommandType = CommandType.Text;

                sqlConexion.Open();
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index", "DataGrid");
        }

        [HttpPost]
        public ActionResult actualizarTarea(int IdTarea, string Tarea, string DescripcionTarea, string Estado, DateTime CreacionTarea)
        {
            
            using (SqlConnection sqlConexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand sqlCmd = new SqlCommand("update ListadoTareas set Tarea=@nombre, DescripcionTarea=@descripcion,  Estado=@estado, CreacionTarea=@creacion where IdTarea=@id", sqlConexion);
                sqlCmd.Parameters.AddWithValue("@nombre", Tarea);
                sqlCmd.Parameters.AddWithValue("@descripcion", DescripcionTarea);
                sqlCmd.Parameters.AddWithValue("@estado", Estado);
                sqlCmd.Parameters.AddWithValue("@creacion", CreacionTarea);
                sqlCmd.Parameters.AddWithValue("@id", IdTarea);
                sqlCmd.CommandType = CommandType.Text;

                sqlConexion.Open();
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index", "DataGrid");
        }


        [HttpPost]
        public ActionResult eliminarTarea(int IdTarea)
        {
            
            using (SqlConnection sqlConexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand sqlCmd = new SqlCommand("delete from ListadoTareas where IdTarea=@id", sqlConexion);
                sqlCmd.Parameters.AddWithValue("@id", IdTarea);
                sqlCmd.CommandType = CommandType.Text;

                sqlConexion.Open();
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index", "DataGrid");
        }


    }
}