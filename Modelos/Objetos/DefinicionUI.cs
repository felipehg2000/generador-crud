using System.Text.Json.Serialization;

namespace generador_crud.Modelos.Objetos
{
    /// <summary>
    /// Representa el archivo de definición de la interfaz de usuario a generar.
    /// </summary>
    public class DefinicionUI
    {
        /// <summary>
        /// Nombre de la tabla de base de datos que representará la interfaz de usuario.
        /// </summary>
        public string NombreTabla { get; set; }

        /// <summary>
        /// Lista de los campos que se mostrarán en la tabla de la vista.
        /// </summary>
        public List<ItemSeleccionado> MostrarEnTabla { get; set; }

        /// <summary>
        /// Lista de los campos que se mostrarán en el panel de edición.
        /// </summary>
        public List<ItemSeleccionado> CamposEdicion { get; set; }

        /// <summary>
        /// Lista de los campos que se mostrarán en el panel de filtros.
        /// </summary>
        public List<ItemSeleccionado> CamposFiltro { get; set; }

        /// <summary>
        /// Constructor de la clase, inicializa atributos a vacío.
        /// </summary>
        [JsonConstructor]
        public DefinicionUI()
        {
            NombreTabla = string.Empty;
            MostrarEnTabla = new();
            CamposEdicion = new();
            CamposFiltro = new();
        }

        /// <summary>
        /// Constructor, inicializa atributos e instancia una clase de tipo DefinicionUI.
        /// </summary>
        /// <param name="param_nombre_tabla">Nombre de la tabla que se está procesando.</param>
        /// <param name="param_campos_edicion">Lista de campos a mostrar en el panel de edición.</param>
        /// <param name="param_campos_filtro">Lista de campos a mostrar en el panel de filtros.</param>
        public DefinicionUI(string param_nombre_tabla,
            List<ItemSeleccionado> param_mostrar_en_tabla,
            List<ItemSeleccionado> param_campos_edicion,
            List<ItemSeleccionado> param_campos_filtro)
        {
            NombreTabla = param_nombre_tabla;
            MostrarEnTabla = param_mostrar_en_tabla;
            CamposEdicion = param_campos_edicion;
            CamposFiltro = param_campos_filtro;
        }
    }
}
