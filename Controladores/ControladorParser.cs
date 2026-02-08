using generador_crud.Modelos.Objetos;

namespace generador_crud.Controladores
{
    /// <summary>
    /// Controlador padre para traducir la DDL al objeto Tabla
    /// </summary>
    internal class ControladorParser
    {
        /// <summary>
        /// Busca y marca las columnas que son claves primarias en la tabla.
        /// </summary>
        /// <param name="param_tabla">Tabla cuyas claves primarias se van a marcar.</param>
        /// <param name="param_claves_primarias">Texto que contiene las claves primarias.</param>
        static protected void MarcarPK(Tabla param_tabla,
            string[] param_claves_primarias)
        {
            // Marcar las columnas que son claves primarias
            foreach (string clave_primaria in param_claves_primarias)
            {
                string clave_limpia = clave_primaria.Trim();

                // Quitar comillas si existen
                if (clave_primaria.Contains("\""))
                {
                    clave_limpia = clave_primaria.Replace("\"", "").Trim();
                }

                // Buscar la columna y marcarla
                Columna? columna = param_tabla.Columnas.FirstOrDefault(c => c.Nombre.Equals(clave_limpia, StringComparison.OrdinalIgnoreCase));
                if (columna != null)
                {
                    columna.EsClavePrimaria = true;
                }
            }
        }
    }
}
