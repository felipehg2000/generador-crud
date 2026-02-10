using Microsoft.Win32;
using System;
using System.IO;

namespace generador_crud.Modelos.Objetos
{
    /// <summary>
    /// Clase que representa una columna de la base de datos Firebird.
    /// </summary>
    public class ColumnaFirebird : Columna
    {
        /// <summary>
        /// Enumeración para los tipos de datos soportados en la base de datos.
        /// </summary>
        public enum TiposDatos
        {
            DECIMAL = 0,                ///< Equivale a DECIMAL
            DOBLE_PRECISION = 1,        ///< Equivale a DOUBLE
            NUMERICO = 2,               ///< Equivale a NUMERIC
            FLOTANTE = 3,               ///< Equivale a FLOAT
            ENTERO = 4,                 ///< Equivale a INTEGER
            PEQUENO_ENTERO = 5,         ///< Equivale a SMALLINT
            GRAN_ENTERO = 6,            ///< Equivale a BIGINT
            VARCHAR = 7,                ///< Equivale a VARCHAR
            CADENA_CARACTERES = 8,      ///< Equivale a CHAR
            FECHA = 9,                  ///< Equivale a DATE
            MARCA_TIEMPO = 10,          ///< Equivale a TIMESTAMP
            BLOB = 11,                  ///< Equivale a BLOB
            TIEMPO = 12,                ///< Equivale a TIME
        }

        /// <summary>
        /// Tipo de dato de la columna en la tabla de la base de datos.
        /// </summary>
        public TiposDatos TipoDato { get; set; }

        /// <summary>
        /// Constructor, inicializa valores e instancia una clase de tipo ColumnaFirebird.
        /// </summary>
        /// <param name="param_nombre">Nombre de la columna.</param>
        /// <param name="param_tipo_dato">Tipo de dato de la columna.</param>
        /// <param name="param_es_pk">True si es PK, false si no.</param>
        public ColumnaFirebird(bool param_es_pk,
            string param_nombre,
            TiposDatos param_tipo_dato) :
            base(param_nombre, param_es_pk)
        {
            TipoDato = param_tipo_dato;
        }

        #region "Parser TiposDatos"

        /// <summary>
        /// Hace un casteo de string a TiposDatos.
        /// </summary>
        /// <param name="param_tipo_dato">Tipo de dato a castear.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        static public TiposDatos ConvertirTipoDato(string param_tipo_dato)
        {
            return param_tipo_dato.Trim().ToUpper() switch
            {
                "BIGINT" => TiposDatos.GRAN_ENTERO,
                "BLOB" => TiposDatos.BLOB,
                "CHAR" => TiposDatos.CADENA_CARACTERES,
                "DATE" => TiposDatos.FECHA,
                "DECIMAL" => TiposDatos.DECIMAL,
                "DOUBLE" => TiposDatos.DOBLE_PRECISION,
                "FLOAT" => TiposDatos.FLOTANTE,
                "INTEGER" => TiposDatos.ENTERO,
                "NUMERIC" => TiposDatos.NUMERICO,
                "SMALLINT" => TiposDatos.PEQUENO_ENTERO,
                "TIME" => TiposDatos.TIEMPO,
                "TIMESTAMP" => TiposDatos.MARCA_TIEMPO,
                "VARCHAR" => TiposDatos.VARCHAR,
                _ => throw new ArgumentException($"Tipo de dato no reconocido: {param_tipo_dato}"),
            };
        }

        /// <summary>
        /// Castea la enumeración del tipo de dato por el texto.
        /// </summary>
        /// <returns>Texto de tipo de dato.</returns>
        /// <exception cref="ArgumentException">Enumeración fuera de rango.</exception>
        public string ObtenerCampoDDL()
        {
            return TipoDato switch
            {
                TiposDatos.GRAN_ENTERO => "BIGINT",
                TiposDatos.BLOB => "BLOB",
                TiposDatos.CADENA_CARACTERES => "CHAR",
                TiposDatos.FECHA => "DATE",
                TiposDatos.DECIMAL => "DECIMAL",
                TiposDatos.DOBLE_PRECISION => "DOUBLE",
                TiposDatos.FLOTANTE => "FLOAT",
                TiposDatos.ENTERO => "INTEGER",
                TiposDatos.NUMERICO => "NUMERIC",
                TiposDatos.PEQUENO_ENTERO => "SMALLINT",
                TiposDatos.TIEMPO => "TIME",
                TiposDatos.MARCA_TIEMPO => "TIMESTAMP",
                TiposDatos.VARCHAR => "VARCHAR",
                _ => throw new ArgumentException($"Tipo de dato no reconocido."),
            };
        }

        #endregion"Parser TiposDatos"
        
        #region "Objeto"

        /// <summary>
        /// Genera la definición de un atributo en Embarcadero C++.
        /// </summary>
        /// <returns>Definición del atributo</returns>
        public string GenerarAtributoEmbarcadero()
        {
            string ret_atributo = ConvertirTipoDatoCMasMas();
            ret_atributo += " _" + ObtenerNombreCapitalizado() + ";";

            return ret_atributo;
        }

        /// <summary>
        /// Genera la definición de una propiedad en Embarcadero C++.
        /// </summary>
        /// <returns>Definición de la propiedad</returns>
        public string GenerarPropiedadEmbarcadero()
        {
            string ret_propiedad = "__property ";
            ret_propiedad += ConvertirTipoDatoCMasMas();
            ret_propiedad += " " + ObtenerNombreCapitalizado() + " = {" + "read = _" + ObtenerNombreCapitalizado() + ", write = _" + ObtenerNombreCapitalizado() + "};";

            return ret_propiedad;
        }

        /// <summary>
        /// Inicializa el argumento de otro modelo para copiar los valores de sus atributos al nuevo modelo.
        /// </summary>
        /// <returns>Texto de la inicialización.</returns>
        public string GenerarInicializacionDeCopia()
        {
            if (TipoDato == TiposDatos.BLOB)
            {
                string texto = "\n_" + ObtenerNombreCapitalizado() + " = new TMemoryStream();";
                texto += "\n_" + ObtenerNombreCapitalizado() + "->LoadFromStream(param_otro_modelo->" + ObtenerNombreCapitalizado() + ");";
                return texto;
            }

            return "_" + ObtenerNombreCapitalizado() + "(param_otro_modelo->" + ObtenerNombreCapitalizado() + ")";

        }

        /// <summary>
        /// Inicializa el atributo con un valor vacío dependiendo del tipo de dato.
        /// </summary>
        /// <returns>Texto de la inicialización.</returns>
        public string GenerarInicializacionVacia()
        {
            string ret_inicializacion = TipoDato == TiposDatos.BLOB ? "_" + ObtenerNombreCapitalizado() + " = " :
                "_" + ObtenerNombreCapitalizado() + "(";

            ret_inicializacion += TipoDato switch
            {
                TiposDatos.DECIMAL => "0",
                TiposDatos.DOBLE_PRECISION => "0",
                TiposDatos.NUMERICO => "0",
                TiposDatos.FLOTANTE => "0",
                TiposDatos.ENTERO => "0",
                TiposDatos.PEQUENO_ENTERO => "0",
                TiposDatos.GRAN_ENTERO => "0",
                TiposDatos.VARCHAR => "\"\"",
                TiposDatos.CADENA_CARACTERES => "\"\"",
                TiposDatos.FECHA => "TDate()",
                TiposDatos.MARCA_TIEMPO => "TDateTime()",
                TiposDatos.BLOB => "new TMemoryStream()",
                TiposDatos.TIEMPO => "TTime()",
                _ => "\"\""
            };

            ret_inicializacion += TipoDato == TiposDatos.BLOB ? ";" : ")";

            return ret_inicializacion;

        }

        /// <summary>
        /// Iguala el atributo a un valor vacío dependiendo del tipo de dato, para limpiar los valores del modelo.
        /// </summary>
        /// <returns>Texto de igualdad a vacío.</returns>
        public string GenerarLimpiezaAtributos()
        {
            if (TipoDato == TiposDatos.BLOB)
            {
                string ret_texto = "\nif (_" + ObtenerNombreCapitalizado() + ")";
                ret_texto += "\n\t{";
                ret_texto += "\n\t_" + ObtenerNombreCapitalizado() + "->Clear();";
                ret_texto += "\n\t{";
                return ret_texto;
            }

            string ret_limpieza = "_" + ObtenerNombreCapitalizado() + "= ";

            ret_limpieza += TipoDato switch
            {
                TiposDatos.DECIMAL => "0",
                TiposDatos.DOBLE_PRECISION => "0",
                TiposDatos.NUMERICO => "0",
                TiposDatos.FLOTANTE => "0",
                TiposDatos.ENTERO => "0",
                TiposDatos.PEQUENO_ENTERO => "0",
                TiposDatos.GRAN_ENTERO => "0",
                TiposDatos.VARCHAR => "\"\"",
                TiposDatos.CADENA_CARACTERES => "\"\"",
                TiposDatos.FECHA => "TDate()",
                TiposDatos.MARCA_TIEMPO => "TDateTime()",
                TiposDatos.TIEMPO => "TTime()",
                _ => "\"\""
            };

            ret_limpieza += ";";

            return ret_limpieza;
        }

        #endregion "Objeto"

        #region "Métodos DAO"

        /// <summary>
        /// Obtiene el nombre del parámetro para consultas SQL.
        /// </summary>
        /// <returns>Parametro en una consulta SQL.</returns>
        public string GetNombreParametro()
        {
            return "P_" + ObtenerNombreCapitalizado();
        }

        /// <summary>
        /// Genera el texto para cargar un atributo desde una consulta SQL.
        /// </summary>
        /// <returns>Texto de la carga del atributo.</returns>
        public string GenerarCargarCampos()
        {
            if (TipoDato == TiposDatos.BLOB)
            {
                return string.Empty;
            }
            string ret_texto =
@"
// Carga el campo {Nombre}
campo = _consulta_sql->FindField(""{Nombre}"");
if (campo)
    {
    ret_modelo->{Nombre} = campo->{TipoDato};
    }
";
            ret_texto = ret_texto.Replace("{Nombre}", ObtenerNombreCapitalizado());
            ret_texto = ret_texto.Replace("{TipoDato}", ObtenerTipoDatoTIBC());

            return ret_texto;
        }

        /// <summary>
        /// Genera la carga de parámetros de una consulta SQL.
        /// </summary>
        /// <returns>Texto de la carga de parámetros.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Tipo de dato no reconocido.</exception>
        public string GenerarCargaParametros()
        {
            if (TipoDato == TiposDatos.TIEMPO || TipoDato == TiposDatos.FECHA || TipoDato == TiposDatos.MARCA_TIEMPO)
            {
                string ret_texto =
@"
// Carga el parámetro {Nombre}
parametro = _consulta_sql->FindParam(""{Parametro}"");
if (parametro)
	 {
	 if (param_modelo->{NombrePropiedad} == {InicializacionVacia})
		 {
		 parametro->Clear();
		 }
	 else
		{
	 	parametro->{TipoDatoIBC} = param_modelo->{NombrePropiedad};
		}
	 }
";
                ret_texto = ret_texto.Replace("{Nombre}", Nombre);
                ret_texto = ret_texto.Replace("{NombrePropiedad}", ObtenerNombreCapitalizado());
                ret_texto = ret_texto.Replace("{Parametro}", GetNombreParametro());
                ret_texto = ret_texto.Replace("{TipoDatoIBC}", TipoDato switch
                {
                    TiposDatos.FECHA => "AsDate",
                    TiposDatos.MARCA_TIEMPO => "AsDateTime",
                    TiposDatos.TIEMPO => "AsTime",
                    _ => throw new ArgumentOutOfRangeException("El tipo de dato no se ha podido reconocer")
                });
                ret_texto = ret_texto.Replace("{InicializacionVacia}", TipoDato switch
                {
                    TiposDatos.FECHA => "TDate(0)",
                    TiposDatos.MARCA_TIEMPO => "TDateTime(0)",
                    TiposDatos.TIEMPO => "TTime(0)",
                    _ => throw new ArgumentOutOfRangeException("El tipo de dato no se ha podido reconocer")
                });

                return ret_texto;
            }
            else if (TipoDato == TiposDatos.BLOB)
            {
                return string.Empty;
            }
            else
            {

                string ret_texto =
    @"
// Carga el parámetro {Nombre}
parametro = _consulta_sql->FindParam(""{Parametro}"");
if (parametro)
    {
    parametro->{TipoDato} = param_modelo->{NombrePropiedad}; 
    }
";

                ret_texto = ret_texto.Replace("{Nombre}", Nombre);
                ret_texto = ret_texto.Replace("{NombrePropiedad}", ObtenerNombreCapitalizado());
                ret_texto = ret_texto.Replace("{Parametro}", GetNombreParametro());
                ret_texto = ret_texto.Replace("{TipoDato}", ObtenerTipoDatoTIBC());

                return ret_texto;
            }
        }

        /// <summary>
        /// Genera los filtros desde y hasta de un atributo.
        /// </summary>
        /// <returns>Texto de generación de los filtros.</returns>
        public string GenerarFiltros()
        {
            if (TipoDato == TiposDatos.BLOB)
            {
                return string.Empty;
            }

            string plantilla =
@"
// carga el filtro {NombreFiltro}
if (param_modelo->{NombreFiltro} != {CampoVacio})
    {
    ret_filtros += "" AND \n   {Nombre} {Signo} :{Parametro} "";
    }
";
            string campo_vacio = TipoDato switch
            {
                TiposDatos.DECIMAL => "0",
                TiposDatos.DOBLE_PRECISION => "0",
                TiposDatos.NUMERICO => "0",
                TiposDatos.FLOTANTE => "0",
                TiposDatos.ENTERO => "0",
                TiposDatos.PEQUENO_ENTERO => "0",
                TiposDatos.GRAN_ENTERO => "0",
                TiposDatos.VARCHAR => "\"\"",
                TiposDatos.CADENA_CARACTERES => "\"\"",
                TiposDatos.FECHA => "TDate()",
                TiposDatos.MARCA_TIEMPO => "TDateTime()",
                TiposDatos.TIEMPO => "TTime()",
                _ => "\"\""
            };

            string desde = plantilla;
            desde = desde.Replace("{NombreFiltro}", ObtenerNombreCapitalizado() + "_desde");
            desde = desde.Replace("{CampoVacio}", campo_vacio);
            desde = desde.Replace("{Nombre}", Nombre);
            desde = desde.Replace("{Signo}", ">=");
            desde = desde.Replace("{Parametro}", "P_" + ObtenerNombreCapitalizado() + "_desde");
            string hasta = plantilla;
            hasta = hasta.Replace("{NombreFiltro}", ObtenerNombreCapitalizado() + "_hasta");
            hasta = hasta.Replace("{CampoVacio}", campo_vacio);
            hasta = hasta.Replace("{Nombre}", Nombre);
            hasta = hasta.Replace("{Signo}", "<=");
            hasta = hasta.Replace("{Parametro}", GetNombreParametro() + "_hasta");

            return desde + hasta;
        }

        #endregion "Métodos DAO"

        #region "Métodos formulario"

        /// <summary>
        /// Según el tipo de dato, genera la definición del componente visual para cargar el valor del atributo en el formulario.
        /// </summary>
        /// <returns>Texto de definición del componente visual.</returns>
        /// <exception cref="ArgumentOutOfRangeException">El tipo de dato no se ha podido reconocer.</exception>
        public string GenerarDefinicionComponentes()
        {
            if (TipoDato == TiposDatos.BLOB)
                return string.Empty;
            
            string ret_texto = string.Empty;

            switch (TipoDato)
            {
                case TiposDatos.DECIMAL:
                case TiposDatos.DOBLE_PRECISION:
                case TiposDatos.NUMERICO:
                case TiposDatos.FLOTANTE:
                case TiposDatos.ENTERO:
                case TiposDatos.PEQUENO_ENTERO:
                case TiposDatos.GRAN_ENTERO:
                case TiposDatos.VARCHAR:
                case TiposDatos.CADENA_CARACTERES:
                    ret_texto = "TEdit *" + ObtenerNombreComponente() + ";";
                    break;
                case TiposDatos.FECHA:
                case TiposDatos.MARCA_TIEMPO:
                case TiposDatos.TIEMPO:
                    ret_texto = "TMaskEdit *" + ObtenerNombreComponente() + ";";
                    break;
                case TiposDatos.BLOB:
                    ret_texto = "TMemo *" + ObtenerNombreComponente() + ";";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("El tipo de dato no se ha podido reconocer");
            }

            return ret_texto;
        }

        /// <summary>
        /// Genera la definición de un label para el componente visual del formulario, con el nombre del atributo como caption.
        /// </summary>
        /// <returns>Texto de definición del label.</returns>
        public string GenerarDefinicionLabel()
        {
            if (TipoDato == TiposDatos.BLOB)
                return string.Empty;

            return "TLabel *" + ObtenerNombreLabel() + ";";
        }

        /// <summary>
        /// Genera el texto para cargar el atributo desde una consulta SQL.
        /// </summary>
        /// <param name="es_filtro">True si se carga el modelo de filtros, false si se carga el modelo de datos.</param>
        /// <returns>Texto para cargar el atributo desde una consulta.</returns>
        /// <exception cref="ArgumentOutOfRangeException">El tipo de dato no se ha podido reconocer.</exception>
        public string CargaModelo(bool es_filtro = false)
        {
            if (TipoDato == TiposDatos.BLOB)
                return string.Empty;

            string ret_texto = es_filtro ? "_modelo_filtros->" + ObtenerNombreCapitalizado() + " = " :
                "_modelo_actual->" + ObtenerNombreCapitalizado() + " = ";

            switch (TipoDato)
            {
                case TiposDatos.DECIMAL:
                case TiposDatos.DOBLE_PRECISION:
                case TiposDatos.NUMERICO:
                case TiposDatos.FLOTANTE:
                    ret_texto += "StrToFloatDef(" + ObtenerNombreComponente() + "->Text, 0);";
                    break;
                case TiposDatos.ENTERO:
                case TiposDatos.PEQUENO_ENTERO:
                    ret_texto += "StrToIntDef(" + ObtenerNombreComponente() + "->Text, 0);";
                    break;
                case TiposDatos.GRAN_ENTERO:
                    ret_texto += "StrToInt64Def(" + ObtenerNombreComponente() + "->Text, 0);";
                    break;
                case TiposDatos.VARCHAR:
                case TiposDatos.CADENA_CARACTERES:
                    ret_texto += ObtenerNombreComponente() + "->Text;";
                    break;
                case TiposDatos.FECHA:
                    ret_texto += "StrToDateDef(" + ObtenerNombreComponente() + "->Text, 0);";
                    break;
                case TiposDatos.MARCA_TIEMPO:
                    ret_texto += "StrToDateTimeDef(" + ObtenerNombreComponente() + "->Text, 0);";
                    break;
                case TiposDatos.TIEMPO:
                    ret_texto += "StrToTimeDef(" + ObtenerNombreComponente() + "->Text, 0);";
                    break;
                case TiposDatos.BLOB:
                    ret_texto = "";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("El tipo de dato no se ha podido reconocer");
            }

            return ret_texto;
        }

        /// <summary>
        /// Genera el texto para cargar el valor del atributo en el componente visual del formulario, desde el modelo de datos.
        /// </summary>
        /// <returns>Texto para cargar un componente visual desde el modelo de datos.</returns>
        /// <exception cref="ArgumentOutOfRangeException">El tipo de dato no se ha podido reconocer.</exception>
        public string CargaFormulario()
        {
            if (TipoDato == TiposDatos.BLOB)
                return string.Empty;

            string ret_texto = ObtenerNombreComponente() + "->Text = ";

            switch (TipoDato)
            {
                case TiposDatos.DECIMAL:
                case TiposDatos.DOBLE_PRECISION:
                case TiposDatos.NUMERICO:
                case TiposDatos.FLOTANTE:
                    ret_texto += "FloatToStr(registro->" + ObtenerNombreCapitalizado() + ");";
                    break;
                case TiposDatos.ENTERO:
                case TiposDatos.PEQUENO_ENTERO:
                    ret_texto += "IntToStr(registro->" + ObtenerNombreCapitalizado() + ");";
                    break;
                case TiposDatos.GRAN_ENTERO:
                    ret_texto += "String(registro->" + ObtenerNombreCapitalizado() + ");";
                    break;
                case TiposDatos.VARCHAR:
                case TiposDatos.CADENA_CARACTERES:
                    ret_texto += "registro->" + ObtenerNombreCapitalizado() + ";";
                    break;
                case TiposDatos.FECHA:
                    ret_texto += "DateToStr(registro->" + ObtenerNombreCapitalizado() + ");";
                    break;
                case TiposDatos.MARCA_TIEMPO:
                    ret_texto += "DateTimeToStr(registro->" + ObtenerNombreCapitalizado() + ");";
                    break;
                case TiposDatos.TIEMPO:
                    ret_texto += "TimeToStr(registro->" + ObtenerNombreCapitalizado() + ");";
                    break;
                case TiposDatos.BLOB:
                    ret_texto = "";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("El tipo de dato no se ha podido reconocer");
            }

            return ret_texto;
        }

        /// <summary>
        /// Según el tipo de argumento genera el texto para dejar vacío el componente visual.
        /// </summary>
        /// <returns>Texto para dejar en blanco el componente visual.</returns>
        /// <exception cref="ArgumentOutOfRangeException">El tipo de dato no se ha podido reconocer.</exception>
        public string LimpiarComponentes()
        {
            if (TipoDato == TiposDatos.BLOB)
                return string.Empty;

            string ret_texto = string.Empty;

            switch (TipoDato)
            {
                case TiposDatos.DECIMAL:
                case TiposDatos.DOBLE_PRECISION:
                case TiposDatos.NUMERICO:
                case TiposDatos.FLOTANTE:
                case TiposDatos.ENTERO:
                case TiposDatos.PEQUENO_ENTERO:
                case TiposDatos.GRAN_ENTERO:
                case TiposDatos.VARCHAR:
                case TiposDatos.CADENA_CARACTERES:
                    ret_texto = ObtenerNombreComponente() + "->Text = \"\";";
                    break;
                case TiposDatos.FECHA:
                case TiposDatos.MARCA_TIEMPO:
                    ret_texto = ObtenerNombreComponente() + "->Text = \"__/__/____\";";
                    break;
                case TiposDatos.TIEMPO:
                    ret_texto = ObtenerNombreComponente() + "->Text = \"__:__\";";
                    break;
                case TiposDatos.BLOB:
                    ret_texto = ObtenerNombreComponente() + "->Clear();";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("El tipo de dato no se ha podido reconocer");
            }

            return ret_texto;
        }

        /// <summary>
        /// Genera la definición del componente visual para cargar el valor del atributo en el formulario, con su label correspondiente, dependiendo del tipo de dato.
        /// </summary>
        /// <param name="param_tab_order">Índice de tabulación del componente dentro del panel.</param>
        /// <param name="param_top">Posición vertical del componente dentro del panel.</param>
        /// <param name="param_left">Posición horizontal del componente dentro del panel.</param>
        /// <returns>Texto para definir el componente visual en el formulario.</returns>
        /// <exception cref="ArgumentOutOfRangeException">El tipo de dato no se ha podido reconocer.</exception>
        public string ObtenerDefinicionComponente(int param_tab_order, int param_top, int param_left)
        {
            if (TipoDato == TiposDatos.BLOB)
                return string.Empty;

            string ret_texto = string.Empty;

            switch (TipoDato)
            {
                case TiposDatos.DECIMAL:
                case TiposDatos.DOBLE_PRECISION:
                case TiposDatos.NUMERICO:
                case TiposDatos.FLOTANTE:
                case TiposDatos.ENTERO:
                case TiposDatos.PEQUENO_ENTERO:
                case TiposDatos.GRAN_ENTERO:
                case TiposDatos.VARCHAR:
                case TiposDatos.CADENA_CARACTERES:
                    ret_texto =
@"        
          object {Label}: TLabel
            Left = {LeftLabel}
            Top = {TopLabel}
            Width = 70
            Height = 15
            Caption = '{Nombre}'
          end
          object {Componente}: TEdit
            Left = {LeftEdit}
            Top = {TopEdit}
            Width = 121
            Height = 23
            NumbersOnly = False
            TabOrder = {TabOrder}
          end";
                    break;
                case TiposDatos.FECHA:
                case TiposDatos.MARCA_TIEMPO:
                    ret_texto =
@"        
          object {Label}: TLabel
            Left = {LeftLabel}
            Top = {TopLabel}
            Width = 70
            Height = 15
            Caption = '{Nombre}'
          end
          object {Componente}: TMaskEdit
            Left = {LeftEdit}
            Top = {TopEdit}
            Width = 120
            Height = 23
            EditMask = '!99/99/0000;1;_'
            MaxLength = 10
            TabOrder = {TabOrder}
            Text = '  /  /    '
          end";
                    break;
                case TiposDatos.TIEMPO:
                    ret_texto =
@"        
          object {Label}: TLabel
            Left = {LeftLabel}
            Top = {TopLabel}
            Width = 70
            Height = 15
            Caption = '{Nombre}'
          end
          object {Componente}: TMaskEdit
            Left = {LeftEdit}
            Top = {TopEdit}
            Width = 120
            Height = 23
            EditMask = '!90:00;1;_'
            MaxLength = 5
            TabOrder = {TabOrder}
            Text = '  :  '
          end";
                    break;
                case TiposDatos.BLOB:
                    ret_texto = "";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("El tipo de dato no se ha podido reconocer");
            }

            if (!string.IsNullOrEmpty(ret_texto))
            {
                ret_texto = ret_texto.Replace("{Label}", ObtenerNombreLabel());
                ret_texto = ret_texto.Replace("{Nombre}", ObtenerNombreCapitalizado());
                ret_texto = ret_texto.Replace("{Componente}", ObtenerNombreComponente());
                ret_texto = ret_texto.Replace("{LeftLabel}", param_left.ToString());
                ret_texto = ret_texto.Replace("{TopLabel}", param_top.ToString());
                param_top += 10;
                ret_texto = ret_texto.Replace("{LeftEdit}", param_left.ToString());
                ret_texto = ret_texto.Replace("{TopEdit}", param_top.ToString());
                ret_texto = ret_texto.Replace("{TabOrder}", param_tab_order.ToString());
            }


            return ret_texto;
        }

        #endregion "Métodos formulario"

        #region "Métodos Privados"

        /// <summary>
        /// Convierte el tipo de dato a su equivalente en Embarcadero.
        /// </summary>
        /// <returns>Tipo de dato para C++</returns>
        private string ConvertirTipoDatoCMasMas()
        {
            return TipoDato switch
            {
                TiposDatos.DECIMAL => "double",
                TiposDatos.DOBLE_PRECISION => "double",
                TiposDatos.NUMERICO => "double",
                TiposDatos.FLOTANTE => "float",
                TiposDatos.ENTERO => "int",
                TiposDatos.PEQUENO_ENTERO => "int",
                TiposDatos.GRAN_ENTERO => "long",
                TiposDatos.VARCHAR => "String",
                TiposDatos.CADENA_CARACTERES => "String",
                TiposDatos.FECHA => "TDate",
                TiposDatos.MARCA_TIEMPO => "TDateTime",
                TiposDatos.BLOB => "TMemoryStream*",
                TiposDatos.TIEMPO => "TTime",
                _ => "String"
            };
        }

        /// <summary>
        /// Según el tipo de dato devuelve el casteo de IBC para cargar el campo desde la consulta SQL.
        /// </summary>
        /// <returns>Tipo de casteo de IBC.</returns>
        private string ObtenerTipoDatoTIBC()
        {
            string ret_tipo_dato = string.Empty;

            switch (TipoDato)
            {
                case TiposDatos.DECIMAL:
                case TiposDatos.DOBLE_PRECISION:
                case TiposDatos.NUMERICO:
                case TiposDatos.FLOTANTE:
                    ret_tipo_dato = "AsFloat";
                    break;
                case TiposDatos.ENTERO:
                case TiposDatos.PEQUENO_ENTERO:
                case TiposDatos.GRAN_ENTERO:
                    ret_tipo_dato = "AsInteger";
                    break;
                case TiposDatos.VARCHAR:
                case TiposDatos.CADENA_CARACTERES:
                    ret_tipo_dato = "AsString";
                    break;  
                case TiposDatos.FECHA:
                case TiposDatos.MARCA_TIEMPO:
                case TiposDatos.TIEMPO:
                    ret_tipo_dato = "AsDateTime";
                    break;
            }

            return ret_tipo_dato;
        }

        #endregion "Métodos privados"
    }
}
