namespace generador_crud.Plantillas.C_Sharp
{
    /// <summary>
    /// Define la clase Controlador, que orquesta la ejecución de las funcionalidades del programa.
    /// </summary>
    internal class Controlador
    {
        public static string Plantilla =
@"/**
 * Fichero generado automáticamente por un generador de código de tipo CRUD.
 *
 * Si se modifica este fichero, los cambios se perderán al regenerar el texto.
 * Para evitar la pérdida de cambios, el código a mantener introduzcalo en el apartado:
 * // <custom>
 *
 * // </custom>
 *
 * Fecha de generación: {FechaGeneracion}
 * Clase: Controlador{NombreTabla}
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
using PruebaPlantillas.Modelos.ControladoresDAO;
using PruebaPlantillas.Modelos.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaPlantillas.Controladores
{
    /// <summary>
    /// Controlador, orquesta la ejecución de las funcionalidades del programa.
    /// </summary>
    internal class Controlador{NombreTabla}
    {
        /// <summary>
        /// Objeto que contiene la conexión a la base de datos.
        /// </summary>
        private ControladorConexionBD _controlador_conexion_bd;

        /// <summary>
        /// Objeto para realizar las consultas a la tabla {NombreTabla} de la base de datos.
        /// </summary>
        private ControladorDAO{NombreTabla} _controlador_dao;

        /// <summary>
        /// Constructor, inicializa atributos.
        /// </summary>
        /// <param name=""param_ruta_bd"">Ruta donde se encuentra la base de datos.</param>
        /// <exception cref=""ArgumentNullException"">Se lanza si la ruta a base de datos es nula.</exception>
        public Controlador{NombreTabla}(string param_ruta_bd)
        {
            if (param_ruta_bd == null)
            {
                throw new ArgumentNullException(""Es necesaria la ruta a la base de datos"");
            }

            _controlador_conexion_bd = new ControladorConexionBD();
            _controlador_conexion_bd.Conectar(param_ruta_bd);

            _controlador_dao = new ControladorDAO{NombreTabla}(_controlador_conexion_bd.ConexionBD);
        }

        /// <summary>
        /// Orquesta la funcionalidad de creación.
        /// </summary>
        /// <param name=""param_modelo"">Objeto que se creará en la base de datos.</param>
        /// <returns>True si el proceso es correcto, false si no.</returns>
        public bool Crear({NombreTabla} param_modelo)
        {
            if (param_modelo == null)
                return false;

            return _controlador_dao.Crear(param_modelo); 
        }

        /// <summary>
        /// Orquesta la funcionalidad de edición.
        /// </summary>
        /// <param name=""param_modelo"">Objeto que se editará en la base de datos.</param>
        /// <returns>True si el proceso es correcto, false si no.</returns>
        public bool Actualizar({NombreTabla} param_modelo)
        {
            if (param_modelo == null)
                return false;

            return _controlador_dao.Actualizar(param_modelo); 
        }

        /// <summary>
        /// Orquesta la funcionalidad de eliminación.
        /// </summary>
        /// <param name=""param_modelo"">Objeto que se eliminará en la base de datos.</param>
        /// <returns>True si el proceso es correcto, false si no.</returns>
        public bool Eliminar({NombreTabla} param_modelo)
        {
            if (param_modelo == null)
                return false;

            return _controlador_dao.Eliminar(param_modelo);
        }

        /// <summary>
        /// Orquesta la funcionalidad de lectura en la base de datos.
        /// </summary>
        /// <param name=""param_filtro"">Objeto con los filtros a añadir en la consulta de base de datos.</param>
        /// <returns>Lista de objetos que respresentan la tabla {NombreTabla} de la base de datos.</returns>
        public List<{NombreTabla}> Leer(Filtros{NombreTabla} param_filtro)
        {
            return _controlador_dao.Leer(param_filtro);
        }

        // <custom>{CodigoCustom}
        // </custom>
    }
}
";
    }
}
