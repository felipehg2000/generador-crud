using Microsoft.Win32;
using System;
using System.IO;

namespace generador_crud.Modelos.Objetos
{
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
            ret_atributo += " _" + Nombre.ToLower() + ";";

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
            ret_propiedad += " " + Nombre + " = {" + "read= _" + Nombre.ToLower() + ", write= _" + Nombre.ToLower() + "};";

            return ret_propiedad;
        }

        public string GenerarInicializacionDeCopia()
        {
            return "_" + Nombre.ToLower() + "(param_otro_modelo->" + Nombre + ")";

        }

        public string GenerarInicializacionVacia()
        {
            string ret_inicializacion = "_" + Nombre.ToLower() + "(";

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

            ret_inicializacion += ")";

            return ret_inicializacion;

        }

        public string GenerarLimpiezaAtributos()
        {
            string ret_limpieza = GenerarAtributoEmbarcadero() + "= ";

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
                TiposDatos.BLOB => "new TMemoryStream()",
                TiposDatos.TIEMPO => "TTime()",
                _ => "\"\""
            };

            ret_limpieza = ";";

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
            return ":P_" + Nombre;
        }

        public string GenerarCargarCampos()
        {
            string ret_texto =
@"
// Carga el campo {Nombre}
campo = param_consulta_sql->FindParam(""{Nombre}"");
if (campo)
    {
    ret_modelo->{Nombre} = campo->As{TipoDato}
    }
";
            ret_texto = ret_texto.Replace("{Nombre}", Nombre);
            ret_texto = ret_texto.Replace("{TipoDato}", ConvertirTipoDatoCMasMas());

            return ret_texto;
        }

        public string GenerarCargaParametros()
        {
            string ret_texto =
@"
// Carga el parámetro {Nombre}
parametro = param_consulta_sql->FindParam(""{Parametro}"");
if (parametro)
    {
    parametro->As{TipoDato} = param_modelo->{Nombre}; 
    }
";

            ret_texto = ret_texto.Replace("{Nombre}", Nombre);
            ret_texto = ret_texto.Replace("{Parametro}", GetNombreParametro());
            ret_texto = ret_texto.Replace("{TipoDato}", ConvertirTipoDatoCMasMas());

            return ret_texto;
        }


        #endregion "Métodos DAO"

        #region "Métodos formulario"

        public string GenerarDefinicionComponentes()
        {
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

        public string GenerarDefinicionLabel()
        {
            return "TLabel *" + ObtenerNombreLabel() + ";";
        }

        public string CargaModelo()
        {
            string ret_texto = "_modelo_actual->" + ObtenerNombreCapitalizado() + " = ";

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

        public string CargaFormulario()
        {
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

        public string LimpiarComponentes()
        {
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
                    ret_texto = ObtenerNombreComponente() + "->Text = __/__/____;";
                    break;
                case TiposDatos.TIEMPO:
                    ret_texto = ObtenerNombreComponente() + "->Text = __:__:;";
                    break;
                case TiposDatos.BLOB:
                    ret_texto = ObtenerNombreComponente() + "->Clear();";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("El tipo de dato no se ha podido reconocer");
            }

            return ret_texto;
        }

        public string ObtenerDefinicionComponente(int param_tab_order, int param_top, int param_left)
        {
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
        public string ConvertirTipoDatoCMasMas()
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
                TiposDatos.BLOB => "TStream",
                TiposDatos.TIEMPO => "TTime",
                _ => "String"
            };
        }

        #endregion "Métodos privados"
    }
}

