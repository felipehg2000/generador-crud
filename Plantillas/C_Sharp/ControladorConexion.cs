namespace generador_crud.Plantillas.C_Sharp
{
    /// <summary>
    /// Define la clase ControladorConexionBD, que gestiona la conexión a la base de datos SQLite.
    /// </summary>
    internal class ControladorConexion
    {
        public static string Plantilla =
@"
/**
 * Fichero generado automáticamente por un generador de código de tipo CRUD.
 *
 * Si se modifica este fichero, los cambios se perderán al regenerar el texto.
 * Para evitar la pérdida de cambios, utilice comentarios del tipo:
 * <custom method='...'> 
 *    Implementación del código. 
 * </custom>
 * Method, si es nuevo, se añadirá el literal new, si es código dentro de un
 * método generado, se pondrá el nombre del método.
 *
 * Fecha de generación: {FechaGeneracion}
 * Clase: ControladorConexionBD
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace PruebaPlantillas.Controladores
{
    internal class ControladorConexionBD
    {
        private SQLiteConnection _conexion_bd; 


        private static readonly string TextoConexion =
            ""Data Source={ruta};Version=3;"";

        public SQLiteConnection ConexionBD
        {
            get
            {
                if (_conexion_bd == null)
                {
                    throw new InvalidOperationException(""La conexión a la base de datos no está establecida."");
                }

                return _conexion_bd;
            }
        }

        public ControladorConexionBD()
        {
            _conexion_bd = null;
        }

        public void Conectar(string param_ruta)
        {
            if (string.IsNullOrEmpty(param_ruta))
            {
                throw new ArgumentException(""La ruta a la base de datos es incorrecta."");
            }

            if (_conexion_bd != null)
            {
                return;
            }

            string textoConexion = TextoConexion.Replace(""{ruta}"", param_ruta);
            _conexion_bd = new SQLiteConnection(textoConexion);
            _conexion_bd.Open();
        }

        public void Desconectar()
        {
            if (_conexion_bd == null)
            {
                return;
            }

            _conexion_bd.Close();
            _conexion_bd.Dispose();
            _conexion_bd = null;
        }

        public void Dispose()
        {
            Desconectar();
        }
    }
}
";
    }
}
