using generador_crud.Modelos.Objetos;
using System.Text.RegularExpressions;

namespace generador_crud.Controladores
{
    /// <summary>
    /// Controla la conversión de DDL a objetos Tabla y Columna para las tablas de Firebird.
    /// </summary>
    internal class ControladorParserFirebird : ControladorParser
    {
        /// <summary>
        /// Transforma la DDL proporcionada en una lista de objetos Tabla.
        /// </summary>
        /// <param name="param_ddl">Texto a procesar.</param>
        /// <returns>Lista de objetos de tipo Tabla.</returns>
        /// <exception cref="ArgumentException">Excepción, el texto está vacío</exception>
        static public List<Tabla> DDLParser(string param_ddl)
        {
            // Validar entrada
            if (string.IsNullOrEmpty(param_ddl))
            {
                throw new ArgumentException("El texto DDL proporcionado está vacío.");
            }

            // Rellenar lista de tablas
            List<Tabla> tablas = new List<Tabla>();

            foreach (Match match in Regex.Matches(param_ddl, @"CREATE TABLE\s+(\w+)\s*\(([^;]+?)\);", RegexOptions.IgnoreCase | RegexOptions.Singleline))
            {
                string nombre_tabla = match.Groups[1].Value;
                string columnas = match.Groups[2].Value;

                Tabla tabla = new Tabla(nombre_tabla,
                    TiposBaseDatos.Firebird,
                    ParsearColumnas(columnas));

                // Rellenar claves primarias
                foreach (Match pkMatch in Regex.Matches(param_ddl, @"PRIMARY KEY\s*\(([^)]+)\)", RegexOptions.IgnoreCase))
                {
                    string[] claves_primarias = pkMatch.Groups[1].Value.Split(',').Select(c => c.Trim()).ToArray();
                    MarcarPK(tabla, claves_primarias);
                }

                tablas.Add(tabla);
            }

            return tablas;
        }

        /// <summary>
        /// Transforma el texto de las columnas en una lista de objetos Columna.
        /// </summary>
        /// <param name="param_columnas">Texto de las columnas de una tabla.</param>
        /// <returns>Lista de objetos de tipo columna.</returns>
        static private List<Columna> ParsearColumnas(string param_columnas)
        {
            List<Columna> ret_columnas = new List<Columna>();

            // Separar por comas y procesar cada línea
            foreach (string linea in param_columnas.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Match columnaMatch = Regex.Match(linea.Trim(), @"^(\w+)\s+([\w()]+)(?:\s+NOT NULL)?", RegexOptions.IgnoreCase);
                if (columnaMatch.Success)
                {
                    // Ignorar líneas que son comentarios
                    if (linea.Trim().StartsWith("--", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // Extraer nombre y tipo de dato
                    string nombre_columna = columnaMatch.Groups[1].Value;
                    string tipo_dato = columnaMatch.Groups[2].Value;

                    // Limpiar tipo de dato
                    if (tipo_dato.EndsWith("("))
                    {
                        tipo_dato = tipo_dato[..^1];
                    }

                    // Ignorar restricciones
                    if (nombre_columna == "CONSTRAINT")
                    {
                        continue;
                    }

                    // Crear objeto Columna y agregar a la lista
                    ColumnaFirebird columna = new ColumnaFirebird(false, nombre_columna, ColumnaFirebird.ConvertirTipoDato(tipo_dato));
                    ret_columnas.Add(columna);
                }
            }

            return ret_columnas;
        }
    }
}
