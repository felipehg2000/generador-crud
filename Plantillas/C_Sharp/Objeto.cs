namespace generador_crud.Plantillas.C_Sharp
{
    /// <summary>
    /// Define un objeto que representa un campo de una tabla de una tabla.
    /// </summary>
    internal class Objeto
    {
        public static string Plantilla =
@"/**
 * Fichero generado automáticamente por un generador de código de tipo CRUD.
 *
 * Si se modifica este fichero, los cambios se perderán al regenerar el texto.
 * Para evitar la pérdida de cambios, el código a mantener introduzcalo en el apartado:
 * // <custom>
 *
 * // </custom>
 *
 * Fecha de generación: {FechaGeneracion}
 * Clase: {NombreTabla}
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace PruebaPlantillas.Modelos.Objetos
{
    /// <summary>
    /// Objeto que representa un campo de una tabla de una tabla.
    /// </summary>
    internal class {NombreTabla}
    {
{DefinicionAtributos}

        // <custom>{CodigoCustom}
        // </custom>

        /// <summary>
        /// Constructor de la clase, inicializa atributos.
        /// </summary>
        public {NombreTabla}()
        {
{InicializacionVacia}
        }

        /// <summary>
        /// Constructor de la clase, clona otro objeto igual.
        /// </summary>
        /// <param name=""param_otro"">Objeto a clonar.</param>
        public {NombreTabla}({NombreTabla} param_otro)
        {
{InicializacionCopia}           
        }

        /// <summary>
        /// Libera la memoria y los datos de los atributos del objeto.
        /// </summary>
        public void Limpiar()
        {
{LimpiarAtributos}
        }
    }
}
";
    }
}
