using generador_crud.Modelos.Objetos;
using System.Text.Json;

namespace generador_crud.Modelos.ControladoresDAO
{
    /// <summary>
    /// Clase para el manejo de la persistencia de la configuración.
    /// </summary>
    internal class ControladorConfiguracionDAO
    {
        /// <summary>
        /// Carga un archivo JSON que contiene la definición de la UI.
        /// </summary>
        /// <param name="param_ruta">Ruta donde se encuentra el fichero JSON.</param>
        /// <returns>Objeto de tipo DefinicionUI.</returns>
        /// <exception cref="FileNotFoundException">Excepción si el archivo de configuración no se encuentra.</exception>
        /// <exception cref="JsonException">Error al leer el fichero JSON.</exception>
        public static Configuracion CargarConfiguracion(string param_ruta)
        {
            // Comprobar que el archivo existe
            if (!File.Exists(param_ruta))
            {
                throw new FileNotFoundException("El archivo de configuración no existe.", param_ruta);
            }

            // Leer el contenido del archivo
            string jsonString = File.ReadAllText(param_ruta);

            Configuracion? retConfiguracion = JsonSerializer.Deserialize<Configuracion>(jsonString);

            if (retConfiguracion == null)
            {
                throw new JsonException("Error al deserializar el archivo de configuración.");
            }

            // Devolver la configuración cargada
            return retConfiguracion;
        }

        /// <summary>
        /// Carga la información de definición de UI y la guarda en un archivo JSON.
        /// </summary>
        /// <param name="param_ruta">Ruta donde almacenar la información</param>
        /// <param name="param_definicion_ui">Definición de UI a guardar en formato JSON.</param>
        /// <exception cref="IOException">Excepción si el archivo no tiene extensión JSON.</exception>
        public static void GuardarConfiguracion(string param_ruta, Configuracion param_configuracion)
        {
            // Comprobar que la extensión del archivo es .json
            if (!param_ruta.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                throw new IOException("La ruta del archivo de configuración debe tener extensión .json");
            }

            // Serializar el objeto a formato JSON
            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json_string = JsonSerializer.Serialize(param_configuracion, opciones);

            // Escribir el contenido en el archivo
            File.WriteAllText(param_ruta, json_string);
        }
    }
}
