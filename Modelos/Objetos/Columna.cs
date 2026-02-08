namespace generador_crud.Modelos.Objetos
{
    /// <summary>
    /// Representa una columna de una tabla en la base de datos.
    /// </summary>
    public class Columna
    {
        /// <summary>
        /// Nombre de la columna en la tabla de la base de datos.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// True si es clave primaria, false en caso contrario.
        /// </summary>
        public bool EsClavePrimaria { get; set; }

        /// <summary>
        /// Constructor, inicializa valores e instancia una clase de tipo Columna.
        /// </summary>
        /// <param name="param_nombre">Nombre de la columna.</param>
        /// <param name="param_es_clave_primaria">True si es PK, false si no.</param>
        public Columna(string param_nombre,
            bool param_es_clave_primaria = false)
        {
            Nombre = param_nombre;
            EsClavePrimaria = param_es_clave_primaria;
        }

        /// <summary>
        /// Obtiene el nombre para un atributo asociado a una columna de la base de datos.
        /// </summary>
        /// <returns>Nombre de un atributo.</returns>
        public string ObtenerNombreCapitalizado()
        {
            return Nombre.Substring(0, 1).ToUpper() +
                Nombre.Substring(1, Nombre.Length - 1).ToLower();
        }

        /// <summary>
        /// Genera el nombre de un atributo de la DDL para las columnas de la tabla.
        /// </summary>
        /// <returns>Nombre que se asociara a la columna de una tabla.</returns>
        public string ObtenerNombreColumna()
        {
            return "COLUMNA_" + Nombre.ToUpper();
        }

        /// <summary>
        /// Genera el nombre de un componente visual.
        /// </summary>
        /// <returns>Nombre de un componente visual.</returns>
        public string ObtenerNombreComponente()
        {
            return "Edit" + ObtenerNombreCapitalizado();
        }

        /// <summary>
        /// Genera el nombre de una etiqueta para el componente visual.
        /// </summary>
        /// <returns>Nombre de una etiqueta.</returns>
        public string ObtenerNombreLabel()
        {
            return "Label" + ObtenerNombreCapitalizado();
        }
    }
}
