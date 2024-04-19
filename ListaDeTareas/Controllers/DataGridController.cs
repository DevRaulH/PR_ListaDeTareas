using ListaDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using System.Runtime.Remoting.Messaging;

namespace ListaDeTareas.Controllers
{
    public class DataGridController : Controller
    {
        //Cadena de conexion con la BD SQL Server con los parametros, solo esta visible para las prueba debe ocultarse
        static string cadenaConexion = "Data Source=DESKTOP-COEFNTP;Initial Catalog=listadoDeTareas;Integrated Security=true;";

        //Una lista de objetos para su uso y manipulacion de los datos que se vayan a iterar
        static List<ListadoTareas> olistaTarea = new List<ListadoTareas>();


        // GET: DataGrid
        //Vista Index
        public ActionResult Index()
        {
            return View();
        }

        //Vista agregar 
        public ActionResult agregar()
        {
            return View();
        }

        //Vista editar, recibiendo los parametros del DataGrid de la vista Index
        public ActionResult editar(int IdTarea, string Tarea, string DescripcionTarea, string Estado, DateTime CreacionTarea)
        {
            //Uso del objeto de lista para el manejo de los datos
            olistaTarea = new List<ListadoTareas>();

            //Uso de una lista para almacenar los datos recibidos
            ListadoTareas recibido = new ListadoTareas();
            recibido.IdTarea = IdTarea;
            recibido.Tarea = Tarea;
            recibido.DescripcionTarea = DescripcionTarea;
            recibido.Estado = Estado;
            recibido.CreacionTarea = CreacionTarea;

            //Añadir todos los datos recibidos al objeto tipo lista
            olistaTarea.Add(recibido);

            //Retornar a la vista con el objeto tipo lista para el manejo de datos en el formulario
            return View(olistaTarea);
        }

        //Vista editar, recibiendo los parametros del DataGrid de la vista Index
        [HttpPost]
        public ActionResult AgregarTarea(string Tarea, string DescripcionTarea)
        {
            
            //En una variable al momento de crear una tarea su estado siempre es "Pendiente"
            string estado = "Pendiente";

            //En una variable se almacena la fecha y hora de su creacion al momento
            DateTime creaciontarea = DateTime.Now;


            //Uso del metodo using para abrir y cerrar la "sesion o conexion"
            //Conexion con la BD
            using (SqlConnection sqlConexion = new SqlConnection(cadenaConexion))
            {
                //Uso del SqlCommand para enviar los comandos o consultas del lenguaje SQL + la cadena de conexión
                SqlCommand sqlCmd = new SqlCommand("insert into ListadoTareas(Tarea,DescripcionTarea,Estado,CreacionTarea) values (@nombre,@descripcion,@estado,@creacion)", sqlConexion);
                //Para la insercion con el comando se envia con el parametro con "valor"
                sqlCmd.Parameters.AddWithValue("@nombre", Tarea);
                //Para la insercion con el comando se envia con el parametro con "valor"
                sqlCmd.Parameters.AddWithValue("@descripcion", DescripcionTarea);
                //Para la insercion con el comando se envia con el parametro con "valor"
                sqlCmd.Parameters.AddWithValue("@estado", estado);
                //Para la insercion con el comando se envia con el parametro con "valor"
                sqlCmd.Parameters.AddWithValue("@creacion", creaciontarea);
                //Se especifica que el comando es de tipo texto
                sqlCmd.CommandType = CommandType.Text;

                //Se abre la conexion a la BD
                sqlConexion.Open();

                //Se ejecuta el comando
                sqlCmd.ExecuteNonQuery();
            }//Se cierra la conexion a la BD

            //Redirecciona a la vista principal
            return RedirectToAction("Index", "DataGrid");
        }

        [HttpPost]
        public ActionResult actualizarTarea(int IdTarea, string Tarea, string DescripcionTarea, string Estado, DateTime CreacionTarea)
        {
            
            using (SqlConnection sqlConexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand sqlCmd = new SqlCommand("update ListadoTareas set Tarea=@nombre, DescripcionTarea=@descripcion,  Estado=@estado, CreacionTarea=@creacion where IdTarea=@id", sqlConexion);
                //Para la actualizacion con el comando se envia el parametro con "valor"
                sqlCmd.Parameters.AddWithValue("@nombre", Tarea);
                //Para la actualizacion con el comando se envia el parametro con "valor"
                sqlCmd.Parameters.AddWithValue("@descripcion", DescripcionTarea);
                //Para la actualizacion con el comando se envia el parametro con "valor"
                sqlCmd.Parameters.AddWithValue("@estado", Estado);
                //Para la actualizacion con el comando se envia el parametro con "valor"
                sqlCmd.Parameters.AddWithValue("@creacion", CreacionTarea);
                //Para la actualizacion con el comando se envia el parametro con "valor"
                sqlCmd.Parameters.AddWithValue("@id", IdTarea);

                //Se especifica que el comando es de tipo texto
                sqlCmd.CommandType = CommandType.Text;
                
                //Se abre la conexion a la BD
                sqlConexion.Open();

                //Se ejecuta el comando
                sqlCmd.ExecuteNonQuery();
            }//Se cierra la conexion a la BD

            //Redirecciona a la vista principal
            return RedirectToAction("Index", "DataGrid");
        }

        [HttpPost]
        public ActionResult marcar(int IdTarea, string Estado)
        {
            //Condicional para validar el estado actual de la tarea y cambiarlo
            if (Estado=="pendiente" || Estado == "Pendiente")
            {
                Estado = "Completado";
            }
            else if (Estado == "completado" || Estado == "Completado")
            {
                Estado = "Pendiente";
            } else
            {
                return View("El Estado de la tarea actual no corresponde a ninguno de los estados predefinidos. Por favor actualizar el campo.");
            }

            using (SqlConnection sqlConexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand sqlCmd = new SqlCommand("update ListadoTareas set Estado=@estado where IdTarea=@id", sqlConexion);
                //Para cambiar el estado con el comando se envia el parametro con "valor"
                sqlCmd.Parameters.AddWithValue("@estado", Estado);
                //Para cambiar el estado con el comando se envia el parametro con "valor"
                sqlCmd.Parameters.AddWithValue("@id", IdTarea);
                //Se especifica que el comando es de tipo texto
                sqlCmd.CommandType = CommandType.Text;
                
                //Se abre la conexion a la BD
                sqlConexion.Open();

                //Se ejecuta el comando
                sqlCmd.ExecuteNonQuery();
            }//Se cierra la conexion a la BD

            //Redirecciona a la vista principal
            return RedirectToAction("Index", "DataGrid");
        }


        [HttpPost]
        public ActionResult eliminarTarea(int IdTarea)
        {
            
            using (SqlConnection sqlConexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand sqlCmd = new SqlCommand("delete from ListadoTareas where IdTarea=@id", sqlConexion);
                //Para eliminar con el comando se envia el parametro con "valor"
                sqlCmd.Parameters.AddWithValue("@id", IdTarea);
                //Se especifica que el comando es de tipo texto
                sqlCmd.CommandType = CommandType.Text;
                
                //Se abre la conexion a la BD
                sqlConexion.Open();

                //Se ejecuta el comando
                sqlCmd.ExecuteNonQuery();
            }//Se cierra la conexion a la BD


            //Redirecciona a la vista principal
            return RedirectToAction("Index", "DataGrid");
        }


    }
}