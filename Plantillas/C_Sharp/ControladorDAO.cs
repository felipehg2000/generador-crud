namespace generador_crud.Plantillas.C_Sharp
{
    internal class ControladorDAO
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
 * Clase: ControladorDAO{NombreTabla}
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
using PruebaPlantillas.Controladores;
using PruebaPlantillas.Modelos.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaPlantillas.Modelos.ControladoresDAO
{
    /// <summary>
    /// Clase DAO para acceder a la tabla {NombreTabla} de la base de datos.
    /// </summary>
    internal class ControladorDAO{NombreTabla}
    {
        /// <summary>
        /// Componente con la persistencia de la conexión a la base de datos.
        /// </summary>
        public SQLiteConnection ConexionBD { get; }
        
        /// <summary>
        /// Constructor, inicializa componentes.
        /// </summary>
        /// <param name=""conexionBD"">Conexión con la base de datos.</param>
        /// <exception cref=""ArgumentNullException"">Se lanza si la conexión a base de datos es nula.</exception>
        public ControladorDAO{NombreTabla}(SQLiteConnection conexionBD)
        {
            ConexionBD = conexionBD ?? throw new ArgumentNullException(""Conexion a base de datos incorrecta.""); 
        }

        /// <summary>
        /// Consulta de creación de un objeto de tipo Campo.
        /// </summary>
        /// <param name=""param_campo"">Objeto a insertar en la base de datos.</param>
        /// <returns>True si el proceso es correcto, false si no.</returns>
        public bool Crear({NombreTabla} param_modelo)
        {
            const string texto = @""
INSERT INTO 
    {NombreTabla}(
{CamposInsert})
    VALUES(
{ParametrosInsert});"";

            using (SQLiteCommand consulta = new SQLiteCommand(texto, ConexionBD))
            {
                // Crea parametros
                Parametrizar(consulta, param_modelo);
                ParametrizarPK(consulta, param_modelo);
                return consulta.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        /// Consulta de edición de un objeto de tipo Campo
        /// </summary>
        /// <param name=""param_campo"">Objeto a editar en la base de datos.</param>
        /// <returns>True si el proceso es correcto, false si no.</returns>
        public bool Actualizar({NombreTabla} param_modelo)
        {
            const string texto = @""
UPDATE 
    {NombreTabla} 
SET
{CamposSet}
WHERE 
{FiltroPK};"";

            using (SQLiteCommand consulta_sql = new SQLiteCommand(texto, ConexionBD))
            {
                Parametrizar(consulta_sql, param_modelo);
                ParametrizarPK(consulta_sql, param_modelo);
                return consulta_sql.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        /// Consulta de eliminación de un objeto de tipo Campo.
        /// </summary>
        /// <param name=""param_campo"">Objeto a eliminar de la base de datos.</param>
        /// <returns>True si el proceso es correcto, false si no.</returns>
        public bool Eliminar({NombreTabla} param_modelo)
        {
            const string texto = @""
DELETE FROM 
    {NombreTabla}
WHERE 
    {FiltroPK};"";

            using (SQLiteCommand consulta_sql = new SQLiteCommand(texto, ConexionBD))
            {
                ParametrizarPK(consulta_sql, param_modelo);
                return consulta_sql.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        /// Consulta de lectura de objetos de tipo Campo a la base de datos.
        /// </summary>
        /// <param name=""param_filtros"">Filtros para la consulta de selección.</param>
        /// <returns>Lista de registros.</returns>
        public List<{NombreTabla}>Leer(Filtros{NombreTabla} param_filtros)
        {
            List<{NombreTabla}> resultado = new List<{NombreTabla}>();

            string texto = @""
SELECT
    *
FROM 
    {NombreTabla} 
WHERE "" + 
    GenerarFiltros(param_filtros);

            using (SQLiteCommand consulta_sql  = new SQLiteCommand(texto, ConexionBD))
            {
                ParametrizarFiltros(consulta_sql, param_filtros);

                using (SQLiteDataReader respuesta = consulta_sql.ExecuteReader())
                {
                    while (respuesta.Read())
                    {
                        resultado.Add(CargarCampos(respuesta));
                    }
                }
            }

            return resultado;
        }

        /// <summary>
        /// Método para consultar si un campo está en una consulta.
        /// </summary>
        /// <param name=""param_consulta_sql"">Consulta en la que buscar el campo.</param>
        /// <param name=""param_nombre_columna"">Nombre del campo a buscar.</param>
        /// <returns>True si el campo existe, false si no.</returns>
        private bool ExisteCampo(SQLiteDataReader param_consulta_sql, string param_nombre_columna)
        {
            for (int campo = 0; campo < param_consulta_sql.FieldCount; campo++)
            {
                if (param_consulta_sql.GetName(campo).Equals(param_nombre_columna, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Método para cargar los datos de un objeto en el objeto con una consulta de base de datos.
        /// </summary>
        /// <param name=""param_respuesta"">Respuesta a una consulta de lectura a base de datos.</param>
        /// <returns>Objeto de tipo Campo leído.</returns>
        private {NombreTabla} CargarCampos(SQLiteDataReader param_respuesta)
        {
            {NombreTabla} ret_modelo = new {NombreTabla}();

{CargaCampos}

            return ret_modelo;
        }

        private void Parametrizar(SQLiteCommand param_consulta_sql, {NombreTabla} param_modelo)
        {
{ParametrizacionCampos}
        }

        private void ParametrizarPK(SQLiteCommand param_consulta_sql, {NombreTabla} param_modelo)
        {
{ParametrizacionPK}
        }


        private void ParametrizarFiltros(SQLiteCommand param_consulta_sql, Filtros{NombreTabla} param_filtros)
        {
            if (param_filtros == null)
            {
                return;
            }

{ParametrizacionFiltros}
        }

        /// <summary>
        /// Método para generar los filtros a introducir en una consulta de lectura de la base de datos.
        /// </summary>
        /// <param name=""param_filtros"">Objeto con los filtros a generar.</param>
        /// <returns>Texto que irá en la parte WHERE de la consulta de lectura a la base de datos.</returns>
        private string GenerarFiltros(Filtros{NombreTabla} param_filtros)
        {
            string filtros = ""\n\t1 = 1"";

            if (param_filtros == null)
            {
                return filtros;
            }

{GeneracionFiltros}
            
            return filtros;
        }

        // <custom>{CodigoCustom}
        // </custom>
    }
}
";
    }
}
