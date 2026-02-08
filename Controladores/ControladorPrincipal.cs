using generador_crud.Factorias;
using generador_crud.Formularios;
using generador_crud.Modelos.ControladoresDAO;
using generador_crud.Modelos.Objetos;
using System.Configuration;

namespace generador_crud.Controladores
{
    /// <summary>
    /// Controlador principal de la aplicación, asociado al FormularioPrincipal.
    /// </summary>
    internal class ControladorPrincipal
    {
        /// <summary>
        /// Lista de tablas procesadas desde el DDL.
        /// </summary>
        public List<Tabla>? Tablas { get; set; }

        /// <summary>
        /// Procesa la DDL y generar los ficheros de salida. Si es necesario se generan los ficheros de definición UI.
        /// </summary>
        /// <param name="param_configuracion">Opciones seleccionadas por el usuario para el proceso.</param>
        public bool Generar(Configuracion param_configuracion)
        {
            if (param_configuracion.GenerarFicheroDefUI)
            {
                bool respuesta = MostrarGeneradorDefUI(param_configuracion,
                    param_configuracion.RutaDDL, 
                    false);

                if (!respuesta)
                    return false;
            }
            else if (Tablas == null)
            {
                Tablas = FactoriaTipoTabla.Parse(param_configuracion,
                    System.IO.File.ReadAllText(param_configuracion.RutaDDL));
            }

            if (Tablas == null)
            {
                throw new Exception("No se han procesado tablas desde la DDL.");
            }

            foreach (Tabla tabla in Tablas)
            {
                string ruta_tmp = param_configuracion.RutaConfiguracionUI;
                param_configuracion.RutaConfiguracionUI += "\\" + tabla.Nombre + "_DefUI.json";
                FactoriaTipoTabla.GenerarSalida(param_configuracion,
                    tabla,
                    ControladorDefinicionUIDAO.CargarDefinicionUI(param_configuracion.RutaConfiguracionUI));
                param_configuracion.RutaConfiguracionUI = ruta_tmp;
            }

            return true;
        }

        /// <summary>
        /// Muestra el formulario para la generación de ficheros de definición UI.
        /// </summary>
        /// <param name="param_ruta_ddl">Ruta donde está el fichero con la DDL a procesar.</param>
        /// <param name="configuracion">True para permitir seleccionar la ruta en la ventana, false para no permitirlo.</param>
        public bool MostrarGeneradorDefUI(Configuracion configuracion, string param_ruta_ddl, bool param_sel_ruta = true)
        {
            if (configuracion == null)
            {
                throw new ArgumentNullException("Configuración incorrecta.");
            }
            if (Tablas == null)
            {
                Tablas = FactoriaTipoTabla.Parse(configuracion,
                    System.IO.File.ReadAllText(param_ruta_ddl));
            }

            if (Tablas == null)
            {
                throw new Exception("No se han podido leer los datos de la DDL.");
            }

            FormularioFicheroDefUI formulario = new FormularioFicheroDefUI();
            if (param_sel_ruta)
                formulario.MostrarFormulario(true, Tablas, string.Empty);
            else
                formulario.MostrarFormulario(true, Tablas, configuracion.RutaConfiguracionUI);

            if (DialogResult.Cancel == formulario.DialogResult)
                return false;

            return true;
        }

        /// <summary>
        /// Guarda la configuración en un fichero JSON.
        /// </summary>
        /// <param name="param_ruta">Ruta donde se guardará la configuración en un fichero JSON.</param>
        /// <param name="param_configuracion">Configuración a almacenar en el fichero JSON.</param>
        static public void GuardarConfiguracion(string param_ruta, Configuracion param_configuracion)
        {
            string nombre_fichero = "Configuracion.json";
            param_ruta += "\\" + nombre_fichero;
            ControladorConfiguracionDAO.GuardarConfiguracion(param_ruta, param_configuracion);
        }

        /// <summary>
        /// Carga la configuración desde un fichero JSON.
        /// </summary>
        /// <param name="param_ruta">Fichero JSON a leer.</param>
        /// <returns></returns>
        static public Configuracion CargarConfiguracion(string param_ruta)
        {
            Configuracion configuracion = ControladorConfiguracionDAO.CargarConfiguracion(param_ruta);
            return configuracion;
        }

        /// <summary>
        /// Libera la memoria ocupada por las tablas procesadas.
        /// </summary>
        /// 
        public void LimpiarTablas()
        {
            if (Tablas != null)
            {
                Tablas.Clear();
                Tablas = null;
            }
        }
    }
}
