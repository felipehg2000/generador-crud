using generador_crud.Modelos.Objetos;
using System.Text.Json;

namespace generador_crud.Modelos.ControladoresDAO
{
    /// <summary>
    /// Clase para el manejo de la persistencia de la definición de la UI.
    /// </summary>
    internal class ControladorDefinicionUIDAO
    {
        /// <summary>
        /// Carga un archivo JSON que contiene la definición de la UI.
        /// </summary>
        /// <param name="param_ruta">Ruta donde se encuentra el fichero JSON.</param>
        /// <returns>Objeto de tipo DefinicionUI.</returns>
        /// <exception cref="FileNotFoundException">Excepción si el archivo de configuración no se encuentra.</exception>
        /// <exception cref="JsonException">Error al leer el fichero JSON.</exception>
        public static DefinicionUI CargarDefinicionUI(string param_ruta)
        {
            // Verificar que el archivo existe
            if (!File.Exists(param_ruta))
            {
                throw new FileNotFoundException("El archivo de configuración no existe.", param_ruta);
            }

            // Leer el contenido del archivo JSON
            string jsonString = File.ReadAllText(param_ruta);

            DefinicionUI? ret_definicion = JsonSerializer.Deserialize<DefinicionUI>(jsonString);

            if (ret_definicion == null)
            {
                throw new JsonException("Error al deserializar el archivo de configuración.");
            }

            // Devolver el objeto DefinicionUI
            return ret_definicion;
        }

        /// <summary>
        /// Carga la información de definición de UI y la guarda en un archivo JSON.
        /// </summary>
        /// <param name="param_ruta">Ruta donde almacenar la información</param>
        /// <param name="param_definicion_ui">Definición de UI a guardar en formato JSON.</param>
        /// <exception cref="IOException">Excepción si el archivo no tiene extensión JSON.</exception>
        public static void GuardarDefinicionUI(string param_ruta, DefinicionUI param_definicion_ui)
        {
            // Verificar que la ruta tiene extensión .json
            if (!param_ruta.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                throw new IOException("La ruta del archivo de configuración debe tener extensión .json");
            }

            // Serializar el objeto DefinicionUI a JSON
            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json_string = JsonSerializer.Serialize(param_definicion_ui, opciones);

            // Escribir el JSON en el archivo
            File.WriteAllText(param_ruta, json_string);
        }
    }
}
