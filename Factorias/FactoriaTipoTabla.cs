using generador_crud.Controladores;
using generador_crud.Modelos.Objetos;

namespace generador_crud.Factorias
{
    /// <summary>
    /// Factoría de selección de lógica según el tipo de base de datos.
    /// </summary>
    internal class FactoriaTipoTabla
    {
        /// <summary>
        /// Según el tipo de base de datos se llama al lector correspondiente.
        /// </summary>
        /// <param name="param_configuracion">Configuración de la vista de la aplicación.</param>
        /// <param name="param_ddl">Texto de la DDL.</param>
        /// <returns>Lista de instancias del objeto Tabla con las tablas de la DDL.</returns>
        /// <exception cref="ArgumentNullException">Validación del parámetro de configuración.</exception>
        static public List<Tabla> Parse(Configuracion param_configuracion,
            string param_ddl)
        {
            if (param_configuracion == null)
            {
                throw new ArgumentNullException("Error en la configuración.");
            }

            List<Tabla> ret_tablas = new List<Tabla>();
            if (param_configuracion.EsFirebird)
            {
                ret_tablas = ControladorParserFirebird.DDLParser(param_ddl);
            }
            else if (param_configuracion.EsSQLite)
            {
                ret_tablas = ControladorParserSQLite.DDLParser(param_ddl);
            }

            return ret_tablas;
        }

        /// <summary>
        /// Según el tipo de base de datos se llama al generador de salida correspondente.
        /// </summary>
        /// <param name="param_configuracion">Configuración de la vista de la aplicación.</param>
        /// <param name="param_tabla">Tabla de la que se va a generar la salida</param>
        /// <exception cref="ArgumentNullException">Excepción lanzada si algún parámetro es nulo.</exception>
        static public void GenerarSalida(Configuracion param_configuracion,
            Tabla param_tabla,
            DefinicionUI param_definicion_ui)
        {
            if (param_configuracion == null || param_tabla == null)
            {
                throw new ArgumentNullException("Error, no ha llegado la información necesaria.");
            }

            if (param_configuracion.EsFirebird)
            {
                ControladorSalidaFirebird.Generar(param_configuracion,
                param_tabla,
                param_definicion_ui);
            }
            else if (param_configuracion.EsSQLite)
            {
                ControladorSalidaSQLite.Generar(param_configuracion,
                    param_tabla,
                    param_definicion_ui);
            }
        }
    }
}
