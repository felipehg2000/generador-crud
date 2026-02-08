using System.Text.Json.Serialization;

namespace generador_crud.Modelos.Objetos
{
    /// <summary>
    /// Representa si un ítem está seleccionado o no.
    /// </summary>
    public class ItemSeleccionado
    {
        /// <summary>
        /// Nombre del item que se va a instanciar.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// True si el item está seleccionado, false en caso contrario.
        /// </summary>
        public bool Seleccionado { get; set; }

        /// <summary>
        /// Tipo de dato del item que se va a instanciar.
        /// </summary>
        public string TipoDato { get; set; }

        /// <summary>
        /// Constructor de la clase, inicializa atributos a privado.
        /// </summary>
        [JsonConstructor]
        public ItemSeleccionado()
        {
            Nombre = string.Empty;
            Seleccionado = false;
            TipoDato = string.Empty;
        }

        /// <summary>
        /// Constructor, inicializa valores e instancia una clase de tipo ItemSeleccionado.
        /// </summary>
        /// <param name="param_nombre">Nombre del item.</param>
        /// <param name="param_seleccionado">Selección o no del item.</param>
        /// <param name="param_tipo_dato">Tipo de dato del item en la base de datos.</param>
        public ItemSeleccionado(string param_nombre,
            bool param_seleccionado,
            string param_tipo_dato)
        {
            Nombre = param_nombre;
            Seleccionado = param_seleccionado;
            //TipoBD = param_tipo_bd;
            TipoDato = param_tipo_dato;
        }
    }
}
