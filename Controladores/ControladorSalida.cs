using System.Text.RegularExpressions;

namespace generador_crud.Controladores
{
    /// <summary>
    /// Controlador para gestionar la lectura del código customizado en los ficheros.
    /// </summary>
    internal class ControladorSalida
    {
        /// <summary>
        /// Busca el código customizado por el usuario y lo rescata.
        /// </summary>
        /// <param name="param_ruta">Ruta del fichero cuyo código hay que rescatar.</param>
        /// <returns>Código escrito por el usuario que hay que mantener.</returns>
        /// <exception cref="System.ArgumentException">Excepción lanzada si el fichero no existe.</exception>
        static public string Obtener(string param_ruta)
        {
            if (string.IsNullOrEmpty(param_ruta) || !System.IO.File.Exists(param_ruta))
            {
                return string.Empty;
            }

            string codigo = System.IO.File.ReadAllText(param_ruta);

            var expresion_regular = new Regex(
                @"^\s*//\s*<custom>\s*\r?\n(.*?)^\s*//\s*</custom>",
                RegexOptions.Singleline | RegexOptions.Multiline);

            Match match = expresion_regular.Match(codigo);
            if (match.Success)
                return match.Groups[1].Value;

            return string.Empty;
        }
    }
}
