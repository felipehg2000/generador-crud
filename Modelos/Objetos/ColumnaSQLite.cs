namespace generador_crud.Modelos.Objetos
{
    /// <summary>
    /// Clase que representa una columna de la base de datos SQLite.
    /// </summary>
    public class ColumnaSQLite : Columna
    {
        /// <summary>
        /// Enumeración con los posibles tipos de datos.
        /// </summary>
        public enum TiposDatos
        {
            INTEGER = 0,
            TEXT = 1,
            BLOB = 2,
            REAL = 3,
            NUMERIC = 4,
        }

        /// <summary>
        /// Tipo de dato de la columna almacenada.
        /// </summary>
        public TiposDatos TipoDato { get; set; }

        /// <summary>
        /// Constructor de copia. Inicializa datos.
        /// </summary>
        /// <param name="param_es_pk">True si es clave primaria, false si no.</param>
        /// <param name="param_nombre">Nombre del campo.</param>
        /// <param name="param_tipo_dato">Tipo de dato del campo.</param>
        public ColumnaSQLite(bool param_es_pk,
            string param_nombre,
            TiposDatos param_tipo_dato) :
            base(param_nombre, param_es_pk)
        {
            TipoDato = param_tipo_dato;
        }

        #region "Parser TiposDatos"

        /// <summary>
        /// Parsea los literales de los tipos de datos de la base de datos en la enumeración de la clase.
        /// </summary>
        /// <param name="param_tipo_dato">Literal del tipo de dato de la DDL.</param>
        /// <returns>Tipo de dato de la enumeración de la clase.</returns>
        /// <exception cref="ArgumentException">Tipo de dato no reconocido.</exception>
        static public TiposDatos ConvertirTipoDato(string param_tipo_dato)
        {
            return param_tipo_dato.Trim().ToUpper() switch
            {
                "INTEGER" => TiposDatos.INTEGER,
                "TEXT" => TiposDatos.TEXT,
                "BLOB" => TiposDatos.BLOB,
                "REAL" => TiposDatos.REAL,
                "NUMERIC" => TiposDatos.NUMERIC,
                _ => throw new ArgumentException($"Tipo de dato no reconocido: {param_tipo_dato}"),
            };
        }

        /// <summary>
        /// Parsea el tipo de dato de la enumeración de la clase en el literal de la DDL.
        /// </summary>
        /// <returns>Literal del tipo de dato de la DDL</returns>
        /// <exception cref="ArgumentException">Texto, tipo de dato.</exception>
        public string ObtenerCampoDDL()
        {
            return TipoDato switch
            {
                TiposDatos.INTEGER => "INTEGER",
                TiposDatos.TEXT => "TEXT",
                TiposDatos.BLOB => "BLOB",
                TiposDatos.REAL => "REAL",
                TiposDatos.NUMERIC => "NUMERIC",
                _ => throw new ArgumentException($"Tipo de dato no reconocido."),
            };
        }

        #endregion "Parser TiposDatos"

        #region "Métodos para la generación de objetos"

        /// <summary>
        /// Genera el texto de la definición de los atributos para un modelo.
        /// </summary>
        /// <returns>Texto con la definición de un atributo.</returns>
        public string GenerarDefAtributoNet()
        {
            return "public " + ObtenerTipoDatoNet() + " " + ObtenerNombreCapitalizado() + "{ get; set; }";
        }

        /// <summary>
        /// Genera el texto para la inicialización de atributos en un constructor de copias.
        /// </summary>
        /// <returns>Texto de inicialización de copia.</returns>
        public string GenerarInicializacionCopia()
        {
            return ObtenerNombreCapitalizado() + " = param_otro." + ObtenerNombreCapitalizado() + ";";
        }

        /// <summary>
        /// Genera el texto para la inicialización vacía de atributos.
        /// </summary>
        /// <returns>Texto para la inicialización a vacío.</returns>
        public string GenerarInicializacionVacia()
        {
            string texto = ObtenerNombreCapitalizado() + " = ";

            if (TipoDato == TiposDatos.NUMERIC || TipoDato == TiposDatos.REAL || TipoDato == TiposDatos.INTEGER)
            {
                texto += "0;";
            }
            else if (TipoDato == TiposDatos.TEXT)
            {
                texto += "string.Empty;";
            }
            else if (TipoDato == TiposDatos.BLOB)
            {
                texto += "new byte[0];";
            }

            return texto;
        }

        /// <summary>
        /// Genera el texto para liberar el dato de un atributo.
        /// </summary>
        /// <returns>Texto para limpiar un atributo.</returns>
        public string GenerarLimpiezaAtributos()
        {
            string texto = ObtenerNombreCapitalizado() + " = ";

            texto += TipoDato switch
            {
                TiposDatos.INTEGER => "0;",
                TiposDatos.NUMERIC => "0;",
                TiposDatos.TEXT => "string.Empty;",
                TiposDatos.BLOB => "Array.Empty<byte>();",
                TiposDatos.REAL => "0;",
                _ => throw new ArgumentException($"Tipo de dato no reconocido."),
            };

            return texto;
        }

        #endregion "Métodos para la generación de objetos"

        #region "Métodos para la generación de DAOs"

        /// <summary>
        /// Obtiene el nombre del parámetro para consultas SQL.
        /// </summary>
        /// <returns>Parametro en una consulta SQL.</returns>
        public string GetNombreParametro()
        {
            return "@P_" + Nombre;
        }

        /// <summary>
        /// Genera la parametrización de la clave primaria.
        /// </summary>
        /// <returns>Texto de parametrización de la clave primaria.</returns>
        public string GenerarFiltroPK()
        {
            if (!EsClavePrimaria)
            {
                return string.Empty;
            }

            return Nombre + " = " + GetNombreParametro();
        }

        /// <summary>
        /// Genera el texto para la carga de campos de un modelo.
        /// </summary>
        /// <returns>Texto con el código para cargar un modelo.</returns>
        /// <exception cref="ArgumentException">Tipo de dato no reconocido.</exception>
        public string GenerarCargaCampos()
        {
            string texto =
            "\t\t\tif (ExisteCampo(param_respuesta, \"" + Nombre + "\") && !param_respuesta.IsDBNull(param_respuesta.GetOrdinal(\"" + Nombre + "\"))) \n" +
            "\t\t\t{\n" +
            "\t\t\t\tret_modelo." + ObtenerNombreCapitalizado() + " = ";
            texto += TipoDato switch
            {
                TiposDatos.INTEGER => "param_respuesta.GetInt64(param_respuesta.GetOrdinal(\"" + Nombre + "\"));",
                TiposDatos.NUMERIC => "param_respuesta.GetDecimal(param_respuesta.GetOrdinal(\"" + Nombre + "\"));",
                TiposDatos.TEXT => "param_respuesta.GetString(param_respuesta.GetOrdinal(\"" + Nombre + "\"));",
                TiposDatos.BLOB => "(byte[])param_respuesta[\"" + Nombre + "\"];",
                TiposDatos.REAL => "param_respuesta.GetDouble(param_respuesta.GetOrdinal(\"" + Nombre + "\"));",
                _ => throw new ArgumentException($"Tipo de dato no reconocido."),
            };
            texto += "\n\t\t\t}\n\n";

            return texto;
        }

        /// <summary>
        /// Genera el texto de generación de filtros.
        /// </summary>
        /// <returns>Texto para la generación de filtros.</returns>
        /// <exception cref="ArgumentException">Tipo de dato no reconocido.</exception>
        public string GenerarFiltros()
        {
            if (TipoDato == TiposDatos.BLOB)
                return string.Empty;

            string ret_filtros = string.Empty;

            ret_filtros = @"
            if (param_filtros.{NombreDesde} != {ValorNulo})
                filtros += "" AND \n {NombreCampo} >= @P_{NombreDesde}"";
            
            if (param_filtros.{NombreHasta} != {ValorNulo})
                filtros += "" AND \n {NombreCampo} >= @P_{NombreHasta}"";
";
            string vacio = TipoDato switch
            {
                TiposDatos.INTEGER => "0",
                TiposDatos.NUMERIC => "0",
                TiposDatos.TEXT => "string.Empty",
                TiposDatos.REAL => "0",
                _ => throw new ArgumentException($"Tipo de dato no reconocido."),
            };

            ret_filtros = ret_filtros.Replace("{NombreCampo}", Nombre);
            ret_filtros = ret_filtros.Replace("{NombreDesde}", ObtenerNombreCapitalizado() + "_desde");
            ret_filtros = ret_filtros.Replace("{NombreHasta}", ObtenerNombreCapitalizado() + "_hasta");
            ret_filtros = ret_filtros.Replace("{ValorNulo}", vacio);

            return ret_filtros;
        }

        #endregion "Métodos para la generación de DAOs"

        #region "Métodos para la vista"

        /// <summary>
        /// Genera el texto para cargar un modelo con los datos de los componentes del formulario.
        /// </summary>
        /// <param name="param_filtros">True si se carga el modelo de filtros, false si se carga el modelo normal.</param>
        /// <returns>Texto con la carga de un modelo a través de sus componentes.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Tipo de dato desconocido.</exception>
        public string GenerarCargaModelos(bool param_filtros = false)
        {
            string ret_texto = string.Empty;

            switch (TipoDato)
            {
                case TiposDatos.INTEGER:
                    ret_texto =
@"
            // Integer
            if (!string.IsNullOrEmpty({NombreComponente}.Text))
            {
                if (long.TryParse({NombreComponente}.Text, out long valorInt))
                {
                    {NombreModelo}.{NombreCampo} = valorInt;
                }
                else
                {
                    MessageBox.Show(""El campo llamado {NombreCampo} no tiene un valor válido"",
                        ""Error"",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    {NombreModelo}.Limpiar();
                    return false;
                }
            }";
                    break;
                case TiposDatos.NUMERIC:
                    ret_texto =
@"
            // Numeric
            if (!string.IsNullOrEmpty({NombreComponente}.Text))
            {
                if (decimal.TryParse({NombreComponente}.Text, out decimal valorNumeric))
                    {NombreModelo}.{NombreCampo} = valorNumeric;
                else
                {
                    MessageBox.Show(""El campo llamado {NombreCampo} no tiene un valor válido"",
                        ""Error"",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    {NombreModelo}.Limpiar();
                    return false;
                }
            }";
                    break;
                case TiposDatos.TEXT:
                    ret_texto =
@"
            // Text
            {NombreModelo}.{NombreCampo} = {NombreComponente}.Text;";
                    break;
                case TiposDatos.BLOB:
                    ret_texto =
@"
            // BLOB
            {NombreModelo}.{NombreCampo} = System.Text.Encoding.UTF8.GetBytes({NombreComponente}.Text);";
                    break;
                case TiposDatos.REAL:
                    ret_texto =
@"
            // Real
            if (!string.IsNullOrEmpty({NombreComponente}.Text))
            {
                if (double.TryParse({NombreComponente}.Text, out double valorReal))
                    {NombreModelo}.{NombreCampo} = valorReal;
                else
                {
                    MessageBox.Show(""El campo llamado {NombreCampo} no tiene un valor válido"",
                        ""Error"",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    {NombreModelo}.Limpiar();
                    return false;
                }
            }";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("No se reconoce el tipo de dato.");
            }

            ret_texto = ret_texto.Replace("{NombreCampo}", ObtenerNombreCapitalizado());
            ret_texto = ret_texto.Replace("{NombreComponente}", ObtenerNombreComponente());
            ret_texto = param_filtros ? ret_texto.Replace("{NombreModelo}", "_modelo_filtros") :
                ret_texto.Replace("{NombreModelo}", "_modelo_seleccionado");

            return ret_texto;
        }

        /// <summary>
        /// Genera el texto con el que se rellena un formulario con los datos de un modelo.
        /// </summary>
        /// <returns>Texto para rellenar un formulario.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Tipo de dato desconocido.</exception>
        public string GenerarMuestraModelo()
        {
            string ret_texto = string.Empty;

            switch (TipoDato)
            {
                case TiposDatos.INTEGER:
                    ret_texto =
@"
            {NombreComponente}.Text = _modelos.ElementAt(fila).{NombreCampo}.ToString();";
                    break;
                case TiposDatos.NUMERIC:
                    ret_texto =
@"
            {NombreComponente}.Text = _modelos.ElementAt(fila).{NombreCampo}.ToString();";
                    break;
                case TiposDatos.TEXT:
                    ret_texto =
@"
            {NombreComponente}.Text = _modelos.ElementAt(fila).{NombreCampo};";
                    break;
                case TiposDatos.BLOB:
                    ret_texto =
@"
            {NombreComponente}.Text = _modelos.ElementAt(fila).{NombreCampo} != null
                ? System.Text.Encoding.UTF8.GetString(_modelos.ElementAt(fila).{NombreCampo})
                : string.Empty;";
                    break;
                case TiposDatos.REAL:
                    ret_texto =
@"
            {NombreComponente}.Text = _modelos.ElementAt(fila).{NombreCampo}.ToString();";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("No se reconoce el tipo de dato.");
            }

            ret_texto = ret_texto.Replace("{NombreCampo}", ObtenerNombreCapitalizado());
            ret_texto = ret_texto.Replace("{NombreComponente}", ObtenerNombreComponente());

            return ret_texto;
        }

        #endregion "Métodos para la vista"

        #region "Métodos privados"

        /// <summary>
        /// Según el tipo de dato de la enumeración de la clase devuelve el tipo de dato del lenguaje.
        /// </summary>
        /// <returns>Tipo de dato del lenguaje.</returns>
        /// <exception cref="ArgumentException">Tipo de dato desconocido.</exception>
        private string ObtenerTipoDatoNet()
        {
            return TipoDato switch
            {
                TiposDatos.INTEGER => "long",
                TiposDatos.TEXT => "string",
                TiposDatos.BLOB => "byte[]",
                TiposDatos.REAL => "double",
                TiposDatos.NUMERIC => "decimal",
                _ => throw new ArgumentException($"Tipo de dato no reconocido."),
            };
        }

        #endregion "Métodos privados"
    }
}
