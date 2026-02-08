namespace generador_crud.Modelos.Objetos
{
    /// <summary>
    /// Enumeración para indicar el tipo de base de datos que se almacena en el objeto.
    /// </summary>
    public enum TiposBaseDatos
    {
        NULO = 0,       ///< Opción para indicar que se desconoce la base de datos leída.
        SQLite = 1,     ///< Opción para indicar que la base de datos es SQLite.
        Firebird = 2,   ///< Opción para indicar que la base de datos es Firebird.
    }

    /// <summary>
    /// Representa una tabla de la base de datos.
    /// </summary>
    public class Tabla
    {
        /// <summary>
        /// Nombre de la tabla de base de datos a representar.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Define el tipo de base de datos a la que pertenece la columna.
        /// </summary>
        public TiposBaseDatos TipoBD { get; set; }

        /// <summary>
        /// Lista de columnas que pertenecen a la tabla.
        /// </summary>
        public List<Columna> Columnas { get; set; }

        /// <summary>
        /// Constructor inicializa a vacío una instancia de la clase Tabla.
        /// </summary>
        public Tabla()
        {
            Nombre = string.Empty;
            TipoBD = TiposBaseDatos.NULO;
            Columnas = new List<Columna>();
        }

        /// <summary>
        /// Constructor inicializa una instancia de la clase Tabla con el nombre especificado.
        /// </summary>
        /// <param name="param_nombre">Nombre de la tabla a representar.</param>
        public Tabla(string param_nombre)
        {
            Nombre = param_nombre;
            Columnas = new List<Columna>();
        }

        /// <summary>
        /// Constructor inicializa una instancia de la clase Tabla con el nombre y las columnas especificadas.
        /// </summary>
        /// <param name="param_nombre">Nombre de la tabla a representar.</param>
        /// <param name="param_columnas">Lista de columnas de la tabla.</param>
        public Tabla(string param_nombre,
            TiposBaseDatos param_tipo_bd,
            List<Columna> param_columnas)
        {
            Nombre = param_nombre;
            TipoBD = param_tipo_bd;
            Columnas = param_columnas;
        }

        /// <summary>
        /// Agrega una columna a la lista de columnas de la tabla.
        /// </summary>
        /// <param name="columna">Objeto de tipo columna.</param>
        public void AgregarColumna(Columna columna)
        {
            Columnas.Add(columna);
        }

    }
}
