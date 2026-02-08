using generador_crud.Controladores;
using generador_crud.Modelos.Objetos;
using System.Configuration;

namespace generador_crud
{
    /// <summary>
    /// Formulario principal de la aplicación.
    /// </summary>
    public partial class FormularioPrincipal : Form
    {
        /// <summary>
        /// Controlador que orquesta el funcionamiento del programa.
        /// </summary>
        private ControladorPrincipal Controlador;

        /// <summary>
        /// Constructor, inicializa componentes y atributos
        /// </summary>
        public FormularioPrincipal()
        {
            InitializeComponent();
            Controlador = new ControladorPrincipal();
        }

        /// <summary>
        /// Botón para buscar el fichero SQL con la DDL a leer.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void buttonRutaDDL_Click(object sender, EventArgs e)
        {
            var dialogo = new OpenFileDialog();

            dialogo.Title = "Seleccionar archivo DDL";
            dialogo.Filter = "Archivos SQL (*.sql)|*.sql|Todos los archivos (*.*)|*.*";
            dialogo.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogo.Multiselect = false;

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                textBoxRutaDDL.Text = dialogo.FileName;
                Controlador.LimpiarTablas();
            }
        }

        /// <summary>
        /// Comprueba restricciones e inicia el proceso de generación de ficheros.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void buttonGenerar_Click(object sender, EventArgs e)
        {
            // Comprobar que los parametros necesarios están rellenos
            if (string.IsNullOrEmpty(textBoxRutaDDL.Text))
            {
                MessageBox.Show("Por favor, seleccione un archivo DDL primero.", "Archivo DDL no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(textBoxRutaUI.Text))
            {
                MessageBox.Show("Por favor, seleccione la ruta de las definiciones UI.", "Ruta de definiciones UI no seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!checkBoxUnificarDestinos.Checked &&
                (string.IsNullOrEmpty(textBoxRutaDestinoUI.Text) ||
                 string.IsNullOrEmpty(textBoxDestinoControlador.Text) ||
                 string.IsNullOrEmpty(textBoxDestinoControladorDAO.Text) ||
                 string.IsNullOrEmpty(textBoxDestinoModelo.Text)))
            {
                MessageBox.Show("Por favor, seleccione todas las rutas de destino.", "Rutas de destino incompletas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkBoxUnificarDestinos.Checked &&
                string.IsNullOrEmpty(textBoxRutaDestinoUI.Text))
            {
                MessageBox.Show("Por favor, seleccione la ruta de destino.", "Ruta de destino incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Configuracion configuracion = new Configuracion(
                textBoxRutaDestinoUI.Text,
                textBoxDestinoControlador.Text,
                textBoxDestinoControladorDAO.Text,
                textBoxDestinoModelo.Text,
                textBoxRutaDDL.Text,
                textBoxRutaUI.Text,
                checkBoxUnificarDestinos.Checked,
                checkBoxSobreescribir.Checked,
                checkBoxDefinicionesUI.Checked,
                radioButtonFirebird.Checked,
                radioButtonSQLite.Checked
                );

            string mensaje = "El proceso se ha cancelado.";
            if (Controlador.Generar(configuracion))
                mensaje = "Ficheros generados correctamente.";

            MessageBox.Show(mensaje,
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Comprueba restricciones e inicia el proceso de generación de ficheros de definición UI.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void generarFicherosDefinicionUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRutaDDL.Text))
            {
                MessageBox.Show("Por favor, seleccione un archivo DDL primero.", "Archivo DDL no seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Configuracion configuracion = new Configuracion(
                textBoxRutaDestinoUI.Text,
                textBoxDestinoControlador.Text,
                textBoxDestinoControladorDAO.Text,
                textBoxDestinoModelo.Text,
                textBoxRutaDDL.Text,
                textBoxRutaUI.Text,
                checkBoxUnificarDestinos.Checked,
                checkBoxSobreescribir.Checked,
                checkBoxDefinicionesUI.Checked,
                radioButtonFirebird.Checked,
                radioButtonSQLite.Checked
                );
            Controlador.MostrarGeneradorDefUI(configuracion, textBoxRutaDDL.Text);
        }

        /// <summary>
        /// Busca el directorio donde guardar los ficheros generados de la UI.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void buttonRutaDestinoUI_Click(object sender, EventArgs e)
        {
            textBoxRutaDestinoUI.Text = buttonRutaDestino_Click();

            if (checkBoxUnificarDestinos.Checked)
            {
                textBoxDestinoControlador.Text = textBoxRutaDestinoUI.Text;
                textBoxDestinoControladorDAO.Text = textBoxRutaDestinoUI.Text;
                textBoxDestinoModelo.Text = textBoxRutaDestinoUI.Text;
            }
        }

        /// <summary>
        /// Busca el directorio donde guardar los ficheros generados del controlador.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void buttonDestinoControlador_Click(object sender, EventArgs e)
        {
            textBoxDestinoControlador.Text = buttonRutaDestino_Click();
        }

        /// <summary>
        /// Busca el directorio donde guardar los ficheros generados del controlador DAO.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void buttonDestinoControladorDAO_Click(object sender, EventArgs e)
        {
            textBoxDestinoControladorDAO.Text = buttonRutaDestino_Click();
        }

        /// <summary>
        /// Busca el directorio donde guardar los ficheros generados del modelo.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void buttonDestinoModelo_Click(object sender, EventArgs e)
        {
            textBoxDestinoModelo.Text = buttonRutaDestino_Click();
        }

        /// <summary>
        /// Busca la ruta de un directorio.
        /// </summary>
        /// <returns>Ruta de un directorio</returns>
        private string buttonRutaDestino_Click()
        {
            var dialogo = new FolderBrowserDialog();
            dialogo.Description = "Seleccionar directorio de salida";
            dialogo.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogo.ShowNewFolderButton = true;
            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                return dialogo.SelectedPath;
            }

            return string.Empty;
        }

        /// <summary>
        /// Crea un objeto de tipo Configuración con los datos del formulario e inicia el proceso de guardado.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void exportarConfiguraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuracion configuracion = new Configuracion(
                textBoxRutaDestinoUI.Text,
                textBoxDestinoControlador.Text,
                textBoxDestinoControladorDAO.Text,
                textBoxDestinoModelo.Text,
                textBoxRutaDDL.Text,
                textBoxRutaUI.Text,
                checkBoxUnificarDestinos.Checked,
                checkBoxSobreescribir.Checked,
                checkBoxDefinicionesUI.Checked,
                radioButtonFirebird.Checked,
                radioButtonSQLite.Checked
                );
            string ruta = buttonRutaDestino_Click();
            ControladorPrincipal.GuardarConfiguracion(ruta, configuracion);

            MessageBox.Show("Configuración exportada correctamente.",
                "Éxito",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Busca un fichero de configuración, lo carga y actualiza los campos del formulario.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void importarConfiguraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialogo = new OpenFileDialog();

            dialogo.Title = "Seleccionar archivo DDL";
            dialogo.Filter = "Archivos JSON (*.json)|*.json|Todos los archivos (*.*)|*.*";
            dialogo.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogo.Multiselect = false;

            string ruta = string.Empty;
            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                ruta = dialogo.FileName;
            }
            else
            {
                return;
            }

            Configuracion configuracion = ControladorPrincipal.CargarConfiguracion(ruta);

            textBoxRutaDestinoUI.Text = configuracion.RutaUI;
            textBoxDestinoControlador.Text = configuracion.RutaControladores;
            textBoxDestinoControladorDAO.Text = configuracion.RutaControladoresDAO;
            textBoxDestinoModelo.Text = configuracion.RutaModelos;
            textBoxRutaDDL.Text = configuracion.RutaDDL;
            textBoxRutaUI.Text = configuracion.RutaConfiguracionUI;

            checkBoxUnificarDestinos.Checked = configuracion.UnificarRutas;
            checkBoxSobreescribir.Checked = configuracion.SobreescribirArchivosExistentes;
            checkBoxDefinicionesUI.Checked = configuracion.GenerarFicheroDefUI;

            radioButtonFirebird.Checked = configuracion.EsFirebird;
            radioButtonSQLite.Checked = configuracion.EsSQLite;
        }

        private void buttonRutaUI_Click(object sender, EventArgs e)
        {
            textBoxRutaUI.Text = buttonRutaDestino_Click();
        }

        private void checkBoxUnificarDestinos_CheckedChanged(object sender, EventArgs e)
        {
            string ruta = textBoxRutaDestinoUI.Text;

            bool activar = checkBoxUnificarDestinos.Checked ? false : true;

            if (!activar)
            {
                textBoxRutaDestinoUI.Text           = ruta;
                textBoxDestinoControlador.Text      = ruta;
                textBoxDestinoControladorDAO.Text   = ruta;
                textBoxDestinoModelo.Text           = ruta;
            }

            buttonDestinoControlador.Enabled = activar;
            buttonDestinoControladorDAO.Enabled = activar;
            buttonDestinoModelo.Enabled = activar;

            buttonDestinoControlador.Visible = activar;
            buttonDestinoControladorDAO.Visible = activar;
            buttonDestinoModelo.Visible = activar;
        }
    }
}
