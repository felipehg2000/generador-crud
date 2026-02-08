using generador_crud.Modelos.ControladoresDAO;
using generador_crud.Modelos.Objetos;

namespace generador_crud.Controladores
{
    /// <summary>
    /// Controlador para la gestión de ficheros de definición de interfaz de usuario (UI).
    /// </summary>
    internal class ControladorFicheroDefUI
    {
        /// <summary>
        /// Lista de objetos de DefinicionUI, una por cada tabla procesada en la DDL.
        /// </summary>
        public List<DefinicionUI> DefinicionesUI { get; set; }

        /// <summary>
        /// Constructor, inicializa una instancia de ControladorFicheroDefUI.
        /// </summary>
        public ControladorFicheroDefUI()
        {
            DefinicionesUI = new List<DefinicionUI>();
        }

        /// <summary>
        /// Crea un objeto de tipo DefinicionUI y lo añade a la lista de definiciones.
        /// </summary>
        /// <param name="param_nombre">Nombre de la tabla procesada.</param>
        /// <param name="param_items_edicion">Columnas que se añaden al panel de edición.</param>
        /// <param name="param_items_filtro">Columnas que se añaden al panel de filtros.</param>
        /// <exception cref="ArgumentException">Se lanza si el nombre de la tabla es nulo o vacío.</exception>
        public void AgregarItem(string param_nombre,
            List<ItemSeleccionado> param_mostrar_en_tabla,
            List<ItemSeleccionado> param_items_edicion,
            List<ItemSeleccionado> param_items_filtro)
        {
            // Validar parametros de entrada
            if (string.IsNullOrEmpty(param_nombre))
            {
                throw new ArgumentException("El nombre de la tabla no puede estar vacío.", nameof(param_nombre));
            }

            // Crear nuevo objeto DefinicionUI y añadirlo a la lista
            DefinicionUI nuevo_nodo = new DefinicionUI(param_nombre,
                param_mostrar_en_tabla,
                param_items_edicion,
                param_items_filtro);
            DefinicionesUI.Add(nuevo_nodo);
        }

        /// <summary>
        /// Actualiza la seleccion de un item de mostrar en tabla.
        /// </summary>
        /// <param name="param_nombre">Nombre de la tabla procesada.</param>
        /// <param name="param_item">Nombre del ítem procesado.</param>
        /// <returns>True si el proceso es correcto, false si no.</returns>
        public bool ActualizarSeleccionMostrarTabla(string param_nombre,
            string param_item)
        {
            foreach (DefinicionUI definicion in DefinicionesUI)
            {
                if (!definicion.NombreTabla.Equals(param_nombre, StringComparison.OrdinalIgnoreCase))
                    continue;

                foreach (ItemSeleccionado item in definicion.MostrarEnTabla)
                {
                    if (!item.Nombre.Equals(param_item, StringComparison.OrdinalIgnoreCase))
                        continue;
                    item.Seleccionado = !item.Seleccionado;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Actualiza la selección de un ítem de edición.
        /// </summary>
        /// <param name="param_nombre">Nombre de la tabla procesada.</param>
        /// <param name="param_item">Nombre del ítem procesado.</param>
        /// <returns>True si el proceso es correcto, false si no.</returns>
        public bool ActualizarSeleccionEdicion(string param_nombre,
            string param_item)
        {
            // Buscar la definición de la tabla correspondiente
            foreach (DefinicionUI definicion in DefinicionesUI)
            {
                if (definicion.NombreTabla.Equals(param_nombre, StringComparison.OrdinalIgnoreCase))
                {
                    // Buscar el ítem correspondiente y actualizar su selección
                    foreach (ItemSeleccionado item in definicion.CamposEdicion)
                    {
                        if (item.Nombre.Equals(param_item, StringComparison.OrdinalIgnoreCase))
                        {
                            item.Seleccionado = !item.Seleccionado;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Actualiza la selección de un ítem de filtro.
        /// </summary>
        /// <param name="param_nombre">Nombre de la tabla procesada.</param>
        /// <param name="param_item">Nombre del ítem procesado.</param>
        /// <returns>True si el proceso es correcto, false si no.</returns>
        public bool ActualizarSeleccionFiltro(string param_nombre,
            string param_item)
        {
            // Buscar la definición de la tabla correspondiente
            foreach (DefinicionUI definicion in DefinicionesUI)
            {
                if (definicion.NombreTabla.Equals(param_nombre, StringComparison.OrdinalIgnoreCase))
                {
                    // Buscar el ítem correspondiente y actualizar su selección
                    foreach (ItemSeleccionado item in definicion.CamposFiltro)
                    {
                        if (item.Nombre.Equals(param_item, StringComparison.OrdinalIgnoreCase))
                        {
                            item.Seleccionado = !item.Seleccionado;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Genera ficheros JSON para cada definición de UI en la ruta especificada.
        /// </summary>
        /// <param name="param_ruta">Directorio donde se almacenarán los ficheros</param>
        /// <returns></returns>
        public bool GenerarFicheros(string param_ruta)
        {
            // Validar parámetros de entrada
            if (string.IsNullOrEmpty(param_ruta))
            {
                return false;
            }

            // Guardar cada definición de UI en un fichero JSON separado
            foreach (DefinicionUI definicion in DefinicionesUI)
            {
                string ruta = System.IO.Path.Combine(param_ruta, $"{definicion.NombreTabla}_DefUI.json");
                ControladorDefinicionUIDAO.GuardarDefinicionUI(ruta, definicion);
            }

            return true;
        }
    }
}
