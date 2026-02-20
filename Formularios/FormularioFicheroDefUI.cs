using generador_crud.Controladores;
using generador_crud.Modelos.Objetos;
using System.Runtime.InteropServices;


namespace generador_crud.Formularios
{
    /// <summary>
    /// Clase dedicada a controlar la creación de ficheros de definición de vistas.
    /// </summary>
    public partial class FormularioFicheroDefUI : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// Indica si el formulario se muestra de forma modal.
        /// </summary>
        private bool EsModal;

        /// <summary>
        /// Número de atributos seleccionados para ser mostrados en las columnas de la tabla de la interfaz a generar. Como máximo 5 (el ancho de cada columna es el 20%).
        /// </summary>
        private int NumColumnas;

        /// <summary>
        /// Tablas leidas de la DDL.
        /// </summary>
        private List<Tabla>? Tablas;

        /// <summary>
        /// Instancia del controlador para la gestión de ficheros de definición UI.
        /// </summary>
        private ControladorFicheroDefUI Controlador;

        /// <summary>
        /// Enumerado para las columnas del DataGridView.
        /// </summary>
        private enum ColumnasDataGridView
        {
            Nombre = 0,             ///< Columna del nombre del campo.
            TipoDato = 1,           ///< Columna del tipo de dato del campo.
            EsClavePrimaria = 2,    ///< Columna que indica si es clave primaria.
            MostrarTabla = 3,       ///< Columna que indica si el registro se mostrará en la tabla a crear.
            CampoEdicion = 4,       ///< Columna que indica si es campo de edición.
            CampoFiltro = 5,        ///< Columna que indica si es campo de filtro.
        }

        /// <summary>
        /// Constructor del formulario. Inicializa componentes y atributos.
        /// </summary>
        public FormularioFicheroDefUI()
        {
            InitializeComponent();
            Controlador = new ControladorFicheroDefUI();
            Tablas = null;
            NumColumnas = 0;
        }

        /// <summary>
        /// Mueve el formulario al arrastrar el título.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void labelTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        /// <summary>
        /// Muestra el formulario de manera controlada, adaptando la vista y funcionalidad.
        /// </summary>
        /// <param name="param_es_modal">True si hay que mostrarlo en modo modal, false si no.</param>
        /// <param name="param_tablas">Lista de las tablas procesadas en la DDL.</param>
        public void MostrarFormulario(bool param_es_modal, List<Tabla> param_tablas, string param_ruta)
        {
            // Rellena atributos
            EsModal = param_es_modal;

            // Configura ancho de columnas
            int ancho_data_grid = dataGridViewCampos.Width - SystemInformation.VerticalScrollBarWidth;
            dataGridViewCampos.Columns[(int)ColumnasDataGridView.Nombre].Width = (int)(ancho_data_grid * 0.35);
            dataGridViewCampos.Columns[(int)ColumnasDataGridView.TipoDato].Width = (int)(ancho_data_grid * 0.25);
            dataGridViewCampos.Columns[(int)ColumnasDataGridView.EsClavePrimaria].Width = (int)(ancho_data_grid * 0.10);
            dataGridViewCampos.Columns[(int)ColumnasDataGridView.MostrarTabla].Width = (int)(ancho_data_grid * 0.10);
            dataGridViewCampos.Columns[(int)ColumnasDataGridView.CampoEdicion].Width = (int)(ancho_data_grid * 0.10);
            dataGridViewCampos.Columns[(int)ColumnasDataGridView.CampoFiltro].Width = (int)(ancho_data_grid * 0.10);

            // Rellena combo box de tablas
            foreach (Tabla tabla in param_tablas)
            {
                comboBoxTablas.Items.Add(tabla.Nombre);
            }
            comboBoxTablas.SelectedIndex = 0;

            // Rellena campos de la tabla seleccionada
            Tablas = param_tablas;
            List<Columna> columnas = Tablas.ElementAt(0).Columnas;
            RellenarCamposTabla(columnas,
                Tablas.ElementAt(0).TipoBD);

            if (!string.IsNullOrWhiteSpace(param_ruta))
            {
                buttonDirectorioSalida.Enabled = false;
                buttonDirectorioSalida.Visible = false;
                textBoxDirectorioSalida.Text = param_ruta;
            }

            // Muestra formulario
            if (EsModal)
            {
                this.ShowDialog();
            }
            else
            {
                this.Show();
            }
        }

        /// <summary>
        /// Cierra el formulario de manera controlada.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void buttonCerrar_Click(object sender, EventArgs e)
        {
            if (EsModal)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Rellena los campos del DataGridView con las columnas de la tabla seleccionada.
        /// </summary>
        /// <param name="param_columnas"></param>
        private void RellenarCamposTabla(List<Columna> param_columnas, TiposBaseDatos param_tipo_bd)
        {
            // Limpia filas actuales
            dataGridViewCampos.Rows.Clear();

            // Crea listas de edición y filtro
            List<ItemSeleccionado> tabla = new List<ItemSeleccionado>();
            List<ItemSeleccionado> edicion = new List<ItemSeleccionado>();
            List<ItemSeleccionado> filtro = new List<ItemSeleccionado>();

            // Rellena filas del DataGridView
            foreach (Columna columna in param_columnas)
            {
                string clave_primaria = columna.EsClavePrimaria ? "✔" : "❌";
                string mostrar_tabla = columna.EsClavePrimaria ? "✔" : "❌";
                string es_campo_edicion = "✔";
                string es_campo_filtro = string.Empty;

                if (mostrar_tabla == "✔")
                {
                    if (NumColumnas == 5)
                        mostrar_tabla = "❌";
                    else
                        NumColumnas++;
                }

                    string tipo_dato = string.Empty;
                if (param_tipo_bd == TiposBaseDatos.SQLite)
                {
                    if (columna is ColumnaSQLite columna_sqlite)
                    { 
                        tipo_dato = columna_sqlite.ObtenerCampoDDL();
                        es_campo_filtro = columna_sqlite.TipoDato != ColumnaSQLite.TiposDatos.BLOB ? "✔" : "❌";
                    }

                }
                else if (param_tipo_bd == TiposBaseDatos.Firebird)
                {
                    if (columna is ColumnaFirebird columna_firebird)
                    {
                        tipo_dato = columna_firebird.ObtenerCampoDDL();
                        es_campo_filtro = columna_firebird.TipoDato != ColumnaFirebird.TiposDatos.BLOB ? "✔" : "❌";
                    }
                }

                ItemSeleccionado item_edicion = new ItemSeleccionado(columna.Nombre, es_campo_edicion == "✔", tipo_dato);
                edicion.Add(item_edicion);
                ItemSeleccionado item_filtro = new ItemSeleccionado(columna.Nombre, es_campo_filtro == "✔", tipo_dato);
                filtro.Add(item_filtro);
                ItemSeleccionado item_tabla = new ItemSeleccionado(columna.Nombre, mostrar_tabla == "✔", tipo_dato);
                tabla.Add(item_tabla);

                dataGridViewCampos.Rows.Add(columna.Nombre,
                    tipo_dato,
                    clave_primaria,
                    mostrar_tabla,
                    es_campo_edicion,
                    es_campo_filtro);
            }

            // Crea definición UI y la añade a la lista
            string nombreTabla = comboBoxTablas.SelectedItem?.ToString() ?? string.Empty;
            Controlador.AgregarItem(nombreTabla, tabla, edicion, filtro);
        }

        /// <summary>
        /// Marca o desmarca la opción de mostrar en tabla.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void buttonSelTabla_Click(object sender, EventArgs e)
        {
            // Verifica que hay una fila seleccionada
            if (dataGridViewCampos.SelectedRows.Count <= 0)
            {
                return;
            }

            // Obtiene la fila seleccionada
            int fila_seleccionada = dataGridViewCampos.SelectedRows[0].Index;
            DataGridViewRow fila = dataGridViewCampos.Rows[fila_seleccionada];

            string tipo_dato = (string)fila.Cells[(int)ColumnasDataGridView.TipoDato].Value;
            if (EsCampoBlob(tipo_dato))
            {
                MessageBox.Show("Los campos blob no pueden ser seleccionados como campos de filtro.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Cambia el valor de la celda de filtro
            object? valorCelda = fila.Cells[(int)ColumnasDataGridView.MostrarTabla].Value;
            string valor_actual = valorCelda?.ToString() ?? string.Empty;
            if (valor_actual == "✔")
            {
                NumColumnas--;
                fila.Cells[(int)ColumnasDataGridView.MostrarTabla].Value = "❌";
            }
            else
            {
                if (NumColumnas == 5)
                {
                    MessageBox.Show("Solo se pueden seleccionar 5 argumentos para ser mostrados en la tabla.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                NumColumnas++;
                fila.Cells[(int)ColumnasDataGridView.MostrarTabla].Value = "✔";
            }

            // Actualiza la selección en el controlador
            string tabla = comboBoxTablas.Text;
            string item = fila.Cells[(int)ColumnasDataGridView.Nombre].Value?.ToString() ?? string.Empty;
            if (!Controlador.ActualizarSeleccionMostrarTabla(tabla, item))
            {
                MessageBox.Show("Error al actualizar la selección de edición.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Marca o desmarca un campo como de edición.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void buttonEdicion_Click(object sender, EventArgs e)
        {
            // Verifica que hay una fila seleccionada
            if (dataGridViewCampos.SelectedRows.Count <= 0)
            {
                return;
            }

            // Obtiene la fila seleccionada
            int fila_seleccionada = dataGridViewCampos.SelectedRows[0].Index;
            DataGridViewRow fila = dataGridViewCampos.Rows[fila_seleccionada];

            // Cambia el valor de la celda de edición
            object? valorCelda = fila.Cells[(int)ColumnasDataGridView.CampoEdicion].Value;
            string valor_actual = valorCelda?.ToString() ?? string.Empty;
            if (valor_actual == "✔")
            {
                fila.Cells[(int)ColumnasDataGridView.CampoEdicion].Value = "❌";
            }
            else
            {
                fila.Cells[(int)ColumnasDataGridView.CampoEdicion].Value = "✔";
            }

            // Actualiza la selección en el controlador
            string tabla = comboBoxTablas.Text;
            string item = fila.Cells[(int)ColumnasDataGridView.Nombre].Value?.ToString() ?? string.Empty;
            if (!Controlador.ActualizarSeleccionEdicion(tabla, item))
            {
                MessageBox.Show("Error al actualizar la selección de edición.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Marca o desmarca un campo como de filtro.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void buttonSelFiltro_Click(object sender, EventArgs e)
        {
            // Verifica que hay una fila seleccionada
            if (dataGridViewCampos.SelectedRows.Count <= 0)
            {
                return;
            }

            // Obtiene la fila seleccionada
            int fila_seleccionada = dataGridViewCampos.SelectedRows[0].Index;
            DataGridViewRow fila = dataGridViewCampos.Rows[fila_seleccionada];

            string tipo_dato = (string)fila.Cells[(int)ColumnasDataGridView.TipoDato].Value;
            if (EsCampoBlob(tipo_dato))
            {
                MessageBox.Show("Los campos blob no pueden ser seleccionados como campos de filtro.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Cambia el valor de la celda de filtro
            object? valorCelda = fila.Cells[(int)ColumnasDataGridView.CampoFiltro].Value;
            string valor_actual = valorCelda?.ToString() ?? string.Empty;
            if (valor_actual == "✔")
            {
                fila.Cells[(int)ColumnasDataGridView.CampoFiltro].Value = "❌";
            }
            else
            {
                fila.Cells[(int)ColumnasDataGridView.CampoFiltro].Value = "✔";
            }

            // Actualiza la selección en el controlador
            string tabla = comboBoxTablas.Text;
            string item = fila.Cells[(int)ColumnasDataGridView.Nombre].Value?.ToString() ?? string.Empty;
            if (!Controlador.ActualizarSeleccionFiltro(tabla, item))
            {
                MessageBox.Show("Error al actualizar la selección de edición.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Genera los ficheros de definición UI en el directorio seleccionado.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void buttonGenerar_Click(object sender, EventArgs e)
        {
            // Comprobar directorio de salida
            if (string.IsNullOrWhiteSpace(textBoxDirectorioSalida.Text))
            {
                MessageBox.Show("Debe seleccionar un directorio de salida.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            string mensaje = string.Empty;
            DialogResult respuesta = DialogResult.OK;
            if (Controlador.GenerarFicheros(textBoxDirectorioSalida.Text))
            {
                mensaje = "Ficheros de definición UI generados correctamente.";
            }
            else
            {
                mensaje = "Error al generar los ficheros de definición UI.";
                respuesta = DialogResult.Cancel;
            }

            MessageBox.Show(mensaje,
                "Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            if (EsModal)
            {
                this.DialogResult = respuesta;
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Busca el directorio de salida para los ficheros de definición UI.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void buttonDirectorioSalida_Click(object sender, EventArgs e)
        {
            var dialogo = new FolderBrowserDialog();
            dialogo.Description = "Seleccionar directorio de salida";
            dialogo.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogo.ShowNewFolderButton = true;
            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                textBoxDirectorioSalida.Text = dialogo.SelectedPath;
            }
        }

        /// <summary>
        /// Recarga los campos del DataGridView al cambiar de tabla seleccionada.
        /// </summary>
        /// <param name="sender">Componente que lanza el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void comboBoxTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int posicion_tabla = comboBoxTablas.SelectedIndex;
            if (Tablas == null)
            {
                return;
            }

            List<Columna> columnas = Tablas.ElementAt(posicion_tabla).Columnas;
            NumColumnas = 0;
            RellenarCamposTabla(columnas,
                Tablas.ElementAt(posicion_tabla).TipoBD);
        }

        /// <summary>
        /// Comprueba si el campo que se está tratando es de tipo blob.
        /// </summary>
        /// <param name="param_tipo_dato">Tipo de dato a procesar.</param>
        /// <returns></returns>
        private bool EsCampoBlob(string param_tipo_dato)
        {
            if (Tablas == null)
                return false;

            if (Tablas.ElementAt(comboBoxTablas.SelectedIndex).TipoBD == TiposBaseDatos.SQLite)
            {
                if (ColumnaSQLite.ConvertirTipoDato(param_tipo_dato) == ColumnaSQLite.TiposDatos.BLOB)
                    return true;
            }
            else if (Tablas.ElementAt(comboBoxTablas.SelectedIndex).TipoBD == TiposBaseDatos.Firebird)
            {
                if (ColumnaFirebird.ConvertirTipoDato(param_tipo_dato) == ColumnaFirebird.TiposDatos.BLOB)
                    return true;
            }

            return false;
        }
    }
}
