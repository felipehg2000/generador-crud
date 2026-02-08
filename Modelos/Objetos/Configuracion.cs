using System.Text.Json.Serialization;

namespace generador_crud.Modelos.Objetos
{
    /// <summary>
    /// Representa la configuración de rutas y opciones para el generador de código.
    /// </summary>
    public class Configuracion
    {
        /// <summary>
        /// Ruta donde se generarán los archivos de la interfaz de usuario.
        /// </summary>
        public string RutaUI { get; set; }

        /// <summary>
        /// Ruta donde se generarán los archivos de los controladores.
        /// </summary>
        public string RutaControladores { get; set; }

        /// <summary>
        /// Ruta donde se generarán los archivos de los controladores DAO.
        /// </summary>
        public string RutaControladoresDAO { get; set; }

        /// <summary>
        /// Ruta donde se generarán los archivos de los modelos.
        /// </summary>
        public string RutaModelos { get; set; }

        /// <summary>
        /// Fichero DDL a leer para obtener la estructura de la base de datos.
        /// </summary>
        public string RutaDDL { get; set; }

        /// <summary>
        /// Fichero de configuración de la interfaz de usuario a leer.
        /// </summary>
        public string RutaConfiguracionUI { get; set; }

        /// <summary>
        /// True si solo se va a utilizar una ruta para los archivos generados, false si se van a utilizar rutas separadas.
        /// </summary>
        public bool UnificarRutas { get; set; }

        /// <summary>
        /// True si se deben sobreescribir los archivos existentes, false para no sobreescribirlos.
        /// </summary>
        public bool SobreescribirArchivosExistentes { get; set; }

        /// <summary>
        /// True si se debe generar el fichero de definición de la interfaz de usuario automáticamente, false en caso contrario.
        /// </summary>
        public bool GenerarFicheroDefUI { get; set; }

        /// <summary>
        /// True si la base de datos es firebird false si es SQLite
        /// </summary>
        public bool EsFirebird { get; set; }

        public bool EsSQLite { get; set; }

        /// <summary>
        /// Constructor inicializa a vacío una instancia de la clase Configuracion.
        /// </summary>
        [JsonConstructor]
        public Configuracion()
        {
            RutaUI = string.Empty;
            RutaControladores = string.Empty;
            RutaControladoresDAO = string.Empty;
            RutaModelos = string.Empty;
            RutaDDL = string.Empty;
            RutaConfiguracionUI = string.Empty;
            UnificarRutas = false;
            SobreescribirArchivosExistentes = false;
            GenerarFicheroDefUI = false;
            EsFirebird = false;
            EsSQLite = false;
        }

        /// <summary>
        /// Constructor inicializa una instancia de la clase Configuracion con los parámetros especificados.
        /// </summary>
        /// <param name="param_ruta_ui">Ruta donde se generan los ficheros UI.</param>
        /// <param name="param_ruta_controladores">Ruta donde se generan los controladores.</param>
        /// <param name="param_ruta_controladores_dao">Ruta donde se generan los controladores DAO.</param>
        /// <param name="param_ruta_modelos">Ruta donde se generan los modelos.</param>
        /// <param name="param_ruta_ddl">Ruta para leer DDL.</param>
        /// <param name="param_ruta_configuracion_ui">Ruta para leer la definición de UI.</param>
        /// <param name="param_unificar_rutas">True para usar una ruta de salida, false para usar todas.</param>
        /// <param name="param_sobreescribir_archivos_existentes">True para sobreescribir archivos ya generados, false para no sobreescribirlos.</param>
        /// <param name="param_generar_fichero_def_ui">True para generar automáticamente los ficheros de definición de UI, false para no generarlos.</param>
        /// <param name="param_es_firebird">True si la base de datos es de tipo firebird, false si no.</param>
        /// <param name="param_es_sqlite">True si la base de datos es de tipo SQLite, false si no.</param>
        public Configuracion(string param_ruta_ui,
            string param_ruta_controladores,
            string param_ruta_controladores_dao,
            string param_ruta_modelos,
            string param_ruta_ddl,
            string param_ruta_configuracion_ui,
            bool param_unificar_rutas,
            bool param_sobreescribir_archivos_existentes,
            bool param_generar_fichero_def_ui,
            bool param_es_firebird,
            bool param_es_sqlite)
        {
            RutaUI = param_ruta_ui;
            RutaControladores = param_ruta_controladores;
            RutaControladoresDAO = param_ruta_controladores_dao;
            RutaModelos = param_ruta_modelos;
            RutaDDL = param_ruta_ddl;
            RutaConfiguracionUI = param_ruta_configuracion_ui;
            UnificarRutas = param_unificar_rutas;
            SobreescribirArchivosExistentes = param_sobreescribir_archivos_existentes;
            GenerarFicheroDefUI = param_generar_fichero_def_ui;
            EsFirebird = param_es_firebird;
            EsSQLite = param_es_sqlite;
        }
    }
}
