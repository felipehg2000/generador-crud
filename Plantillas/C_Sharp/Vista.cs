namespace generador_crud.Plantillas.C_Sharp
{
    /// <summary>
    /// Define la clase de la vista, que contiene la interfaz gráfica y la interacción con el usuario.
    /// </summary>
    internal class Vista
    {
        static readonly public string PlantillaCS =
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
 * Clase: Formulario{NombreTabla}
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
using PruebaPlantillas.Controladores;
using PruebaPlantillas.Modelos.ControladoresDAO;
using PruebaPlantillas.Modelos.Objetos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace PruebaPlantillas
{
    public partial class Formulario{NombreTabla} : Form
    {
        #region ""Enumeración""
        private enum Columnas
        {{Columnas}
        };

        /// <summary>
        /// Enumeración para indicar para qué proceso se muestra el panel de campos.
        /// </summary>
        public enum TipoApertura
        {
            NULO = 0,               ///< No se muestra el panel de campos
            CREACION = 1,           ///< Se muestra el panel de campos para la creación de un registro
            EDICION = 2,            ///< Se muestra el panel de campos para la edición de un registro
            CONSULTA = 3,           ///< Se muestra el panel de campos para la consulta de un registro
        };

        #endregion ""Enumeración""

        #region ""Definicion de atributos""
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport(""user32.dll"")]
        private static extern bool ReleaseCapture();

        [DllImport(""user32.dll"")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private TipoApertura _apertura;

        private Controlador{NombreTabla} _controlador;

        private {NombreTabla} _modelo_seleccionado;
        private List<{NombreTabla}> _modelos;
        private Filtros{NombreTabla} _modelo_filtros;

        private bool _es_modal;
        #endregion ""Definicion de atributos""

        #region ""Constructor""
        //----------------------------------------------------------
        /// <summary>
        /// Constructor de la clase, inicializa componentes y atributos.
        /// </summary>
        /// <param name=""param_ruta_bd"">Ruta de la base de datos a la que se conectará el controlador.</param>
        public Formulario{NombreTabla}(string param_ruta_bd)
        {
            InitializeComponent();

            string ruta_bd = param_ruta_bd;
            _controlador = new Controlador{NombreTabla}(ruta_bd);

            _es_modal = false;
            _apertura = TipoApertura.NULO;
            _modelo_filtros = new Filtros{NombreTabla}();
            _modelo_seleccionado = new {NombreTabla}();

            panelPrincipal.Visible = true;
            panelPrincipal.BringToFront();

            // Configura ancho de columnas
            int ancho_data_grid = dataGridViewTabla.Width - SystemInformation.VerticalScrollBarWidth;{AnchoColumnas}

            ActualizarTabla();
        }
        //----------------------------------------------------------
        #endregion ""Constructor""

        #region ""Eventos""

            #region ""Formulario""
        //----------------------------------------------------------
        /// <summary>
        /// Mueve el panel completo al pulsar su título.
        /// </summary>
        /// <param name=""sender"">Componente que lanza el evento.</param>
        /// <param name=""e"">Argumentos del evento lanzado.</param>
        private void labelTextoTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        //----------------------------------------------------------    
            #endregion ""Formulario""

            #region ""PanelPrincipal""
        //----------------------------------------------------------
        /// <summary>
        /// Muestra el panel de edición en modo creación
        /// </summary>
        /// <param name=""sender"">Componente que lanza el evento.</param>
        /// <param name=""e"">Argumentos del evento lanzado.</param>
        private void buttonCrear_Click(object sender, EventArgs e)
        {
            labelTextoCampos.Text = ""Crear registro."";
            _apertura = TipoApertura.CREACION;

            panelCampos.Visible = true;
            panelCampos.BringToFront();
        }
        //----------------------------------------------------------
        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            labelTextoCampos.Text = ""Editar registro."";
            _apertura = TipoApertura.EDICION;

            MostrarModelo();

            panelCampos.Visible = true;
            panelCampos.BringToFront();
        }
        //----------------------------------------------------------
        private void buttonConsultar_Click(object sender, EventArgs e)
        {
            labelTextoCampos.Text = ""Consultar registro."";
            _apertura = TipoApertura.CONSULTA;

            MostrarModelo();

            panelCampos.Visible = true;
            panelCampos.BringToFront();
        }
        //----------------------------------------------------------
        private void buttonFiltrar_Click(object sender, EventArgs e)
        {
            panelFiltros.Visible = true;
            panelFiltros.BringToFront();
        }
        //----------------------------------------------------------
        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(""¿Está seguro de que desea eliminar este registro?"",
                ""Confirmar eliminación"",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (respuesta == DialogResult.No)
            {
                return;
            }

            int fila = dataGridViewTabla.CurrentRow.Index;
            if (_controlador.Eliminar(_modelos.ElementAt(fila)))
            {
                _modelos.RemoveAt(fila);
                ActualizarTabla();
                MessageBox.Show(""Registro eliminado correctamente."",
                    ""Información"",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(""Ha ocurrido un problema en el proceso de eliminación."",
                    ""Error"",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        //----------------------------------------------------------
        private void buttonCerrar_Click(object sender, EventArgs e)
        {
            if (_es_modal)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                Close();
            }
        }
        //----------------------------------------------------------
        private void dataGridViewTabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            buttonActualizar_Click(sender, e);
        }
        //----------------------------------------------------------
        #endregion ""PanelPrincipal""

            #region ""PanelCampos""
        //----------------------------------------------------------
        private void buttonGrabarCampos_Click(object sender, EventArgs e)
        {
            if (!CargarModelo())
            {
                return; 
            }

            bool proceso_correcto = false;
            switch (_apertura)
            {
                case TipoApertura.CREACION:
                    proceso_correcto = _controlador.Crear(_modelo_seleccionado);
                    {NombreTabla} nuevo_nodo = new {NombreTabla}(_modelo_seleccionado);
                    _modelos.Add(nuevo_nodo);
                    ActualizarTabla();
                    break;
                case TipoApertura.EDICION:
                    proceso_correcto = _controlador.Actualizar(_modelo_seleccionado);
                    int fila = dataGridViewTabla.CurrentRow.Index;
                    _modelos[fila] = _modelo_seleccionado;
                    ActualizarTabla();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(""Ha ocurrido un problema y el proceso no se ha completado."");
            }

            if (!proceso_correcto)
            {
                MessageBox.Show(""Ha ocurrido un error en el proceso."",
                    ""Error"",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);      
            }

            buttonCerrarCampos_Click(null, null);
        }
        //----------------------------------------------------------
        private void buttonCerrarCampos_Click(object sender, EventArgs e)
        {
            _modelo_seleccionado.Limpiar();
            _apertura = TipoApertura.NULO;
{LimpiarCampos}

            GestionarActivarCampos(true, Color.White);

            panelPrincipal.Visible = true;
            panelPrincipal.BringToFront();
            panelCampos.Visible = false;
            panelCampos.SendToBack();
        }
        //----------------------------------------------------------
            #endregion ""PanelCampos""

            #region ""PanelFiltros""
        //----------------------------------------------------------
        private void buttonGenerarFiltros_Click(object sender, EventArgs e)
        {
            ActualizarTabla(_modelo_filtros);
            buttonCerrarFiltros_Click(null, null);
        }
        //----------------------------------------------------------
        private void buttonCerrarFiltros_Click(object sender, EventArgs e)
        {
            panelPrincipal.Visible = true;
            panelPrincipal.BringToFront();
            panelFiltros.Visible = false;
            panelFiltros.SendToBack();
        }
        //----------------------------------------------------------
        #endregion ""PanelFiltros""

        #endregion ""Eventos""

        #region ""Método privado""
        //----------------------------------------------------------
        private void ActualizarTabla(Filtros{NombreTabla} param_filtros = null)
        {
            DataGridViewRow fila = null;

            if (dataGridViewTabla.SelectedRows.Count > 0)
            {
                // Tomamos la primera fila seleccionada
                fila = dataGridViewTabla.SelectedRows[0];
            }

            // Limpia filas actuales
            if (!CargarFiltros())
                return;

            dataGridViewTabla.Rows.Clear();
            _modelos = _controlador.Leer(param_filtros);
            foreach ({NombreTabla} modelo in _modelos)
            {
                AgregarFila(modelo);
            }

            dataGridViewTabla.SelectedRows.Equals(fila);
        }
        //----------------------------------------------------------
        private bool CargarModelo()
        {{CargarModelo}

            return true;
        }
        //----------------------------------------------------------
        private void MostrarModelo()
        {
            bool activar = TipoApertura.CONSULTA == _apertura ? false : true;
            Color color = TipoApertura.CONSULTA == _apertura ? Color.Beige : Color.White;

            if (_apertura == TipoApertura.NULO)
            {
                throw new Exception(""Error de acceso."");
            }

            GestionarActivarCampos(activar, color);

            int fila = dataGridViewTabla.CurrentRow.Index;

{MostrarModelo}
        }
        //----------------------------------------------------------
        private bool CargarFiltros()
        {{CargarFiltros}

            return true;
        }
        //----------------------------------------------------------
        private void AgregarFila({NombreTabla} param_modelo)
        {
            dataGridViewTabla.Rows.Add({AgregarFilas});
        }
        //----------------------------------------------------------
        void GestionarActivarCampos(bool param_activar, Color param_color)
        {{ColorCamops}
{VisibilidadCampo}

            if (_apertura == TipoApertura.EDICION)
            {{DesactivarCampos}
            }
        }
        //----------------------------------------------------------
        #endregion ""Método privado""

        // <custom>
{CodigoCustom}
        // </custom>
    }
}

";

        static readonly public string PlantillaDesigner =
@"namespace PruebaPlantillas
{
    partial class Formulario{NombreTabla}
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name=""disposing"">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.labelTextoTitulo = new System.Windows.Forms.Label();
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.panelCentral = new System.Windows.Forms.Panel();
            this.dataGridViewTabla = new System.Windows.Forms.DataGridView();{DefinicionColumnas}
            this.panelRegistros = new System.Windows.Forms.Panel();
            this.labelRegistros = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.buttonFiltrar = new System.Windows.Forms.Button();
            this.buttonCerrar = new System.Windows.Forms.Button();
            this.buttonConsultar = new System.Windows.Forms.Button();
            this.buttonEliminar = new System.Windows.Forms.Button();
            this.buttonActualizar = new System.Windows.Forms.Button();
            this.buttonCrear = new System.Windows.Forms.Button();
            this.panelCampos = new System.Windows.Forms.Panel();
            this.panelCamposPrincipal = new System.Windows.Forms.Panel();
            this.panelBotonesCampos = new System.Windows.Forms.Panel();
            this.buttonCerrarCampos = new System.Windows.Forms.Button();
            this.buttonGrabarCampos = new System.Windows.Forms.Button();
            this.panelCentralCampos = new System.Windows.Forms.Panel();{DefinicionComponentes}
            this.panelSuperiorCampos = new System.Windows.Forms.Panel();
            this.labelTextoCampos = new System.Windows.Forms.Label();
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.panelFiltrosPrincipal = new System.Windows.Forms.Panel();
            this.panelBotonesFiltros = new System.Windows.Forms.Panel();
            this.buttonCerrarFiltros = new System.Windows.Forms.Button();
            this.buttonGenerarFiltros = new System.Windows.Forms.Button();
            this.panelSuperiorFiltros = new System.Windows.Forms.Panel();
            this.labelTituloFiltros = new System.Windows.Forms.Label();
            this.panelCentralFiltros = new System.Windows.Forms.Panel();{DefinicionComponentesFiltros}
            this.panelTitulo.SuspendLayout();
            this.panelPrincipal.SuspendLayout();
            this.panelCentral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTabla)).BeginInit();
            this.panelRegistros.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.panelCampos.SuspendLayout();
            this.panelCamposPrincipal.SuspendLayout();
            this.panelBotonesCampos.SuspendLayout();
            this.panelCentralCampos.SuspendLayout();
            this.panelSuperiorCampos.SuspendLayout();
            this.panelFiltros.SuspendLayout();
            this.panelFiltrosPrincipal.SuspendLayout();
            this.panelBotonesFiltros.SuspendLayout();
            this.panelSuperiorFiltros.SuspendLayout();
            this.panelCentralFiltros.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitulo
            // 
            this.panelTitulo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelTitulo.Controls.Add(this.labelTextoTitulo);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(3, 0);
            this.panelTitulo.Name = ""panelTitulo"";
            this.panelTitulo.Size = new System.Drawing.Size(693, 33);
            this.panelTitulo.TabIndex = 4;
            // 
            // labelTextoTitulo
            // 
            this.labelTextoTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTextoTitulo.Font = new System.Drawing.Font(""Times New Roman"", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTextoTitulo.Location = new System.Drawing.Point(0, 0);
            this.labelTextoTitulo.Name = ""labelTextoTitulo"";
            this.labelTextoTitulo.Size = new System.Drawing.Size(693, 33);
            this.labelTextoTitulo.TabIndex = 0;
            this.labelTextoTitulo.Text = ""Gestión de {NombreTabla}"";
            this.labelTextoTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTextoTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTextoTitulo_MouseDown);
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.Controls.Add(this.panelCentral);
            this.panelPrincipal.Controls.Add(this.panelBotones);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(3, 33);
            this.panelPrincipal.Name = ""panelPrincipal"";
            this.panelPrincipal.Size = new System.Drawing.Size(693, 647);
            this.panelPrincipal.TabIndex = 5;
            // 
            // panelCentral
            // 
            this.panelCentral.BackColor = System.Drawing.Color.White;
            this.panelCentral.Controls.Add(this.dataGridViewTabla);
            this.panelCentral.Controls.Add(this.panelRegistros);
            this.panelCentral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentral.Location = new System.Drawing.Point(0, 0);
            this.panelCentral.Name = ""panelCentral"";
            this.panelCentral.Padding = new System.Windows.Forms.Padding(10);
            this.panelCentral.Size = new System.Drawing.Size(693, 604);
            this.panelCentral.TabIndex = 1;
            // 
            // dataGridViewTabla
            // 
            this.dataGridViewTabla.AllowUserToAddRows = false;
            this.dataGridViewTabla.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTabla.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTabla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle1.Font = new System.Drawing.Font(""Arial"", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTabla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {{RangoColumnas}});
            this.dataGridViewTabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTabla.EnableHeadersVisualStyles = false;
            this.dataGridViewTabla.GridColor = System.Drawing.Color.White;
            this.dataGridViewTabla.Location = new System.Drawing.Point(10, 33);
            this.dataGridViewTabla.Name = ""dataGridViewTabla"";
            this.dataGridViewTabla.RowHeadersVisible = false;
            this.dataGridViewTabla.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewTabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTabla.Size = new System.Drawing.Size(673, 561);
            this.dataGridViewTabla.TabIndex = 2;
            this.dataGridViewTabla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTabla_CellDoubleClick);{AtributosColumna}
            // 
            // panelRegistros
            // 
            this.panelRegistros.Controls.Add(this.labelRegistros);
            this.panelRegistros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRegistros.Location = new System.Drawing.Point(10, 10);
            this.panelRegistros.Name = ""panelRegistros"";
            this.panelRegistros.Size = new System.Drawing.Size(673, 23);
            this.panelRegistros.TabIndex = 0;
            // 
            // labelRegistros
            // 
            this.labelRegistros.AutoSize = true;
            this.labelRegistros.Font = new System.Drawing.Font(""Arial"", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRegistros.Location = new System.Drawing.Point(3, 0);
            this.labelRegistros.Name = ""labelRegistros"";
            this.labelRegistros.Size = new System.Drawing.Size(64, 15);
            this.labelRegistros.TabIndex = 0;
            this.labelRegistros.Text = ""Registros:"";
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.White;
            this.panelBotones.Controls.Add(this.buttonFiltrar);
            this.panelBotones.Controls.Add(this.buttonCerrar);
            this.panelBotones.Controls.Add(this.buttonConsultar);
            this.panelBotones.Controls.Add(this.buttonEliminar);
            this.panelBotones.Controls.Add(this.buttonActualizar);
            this.panelBotones.Controls.Add(this.buttonCrear);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 604);
            this.panelBotones.Name = ""panelBotones"";
            this.panelBotones.Size = new System.Drawing.Size(693, 43);
            this.panelBotones.TabIndex = 5;
            // 
            // buttonFiltrar
            // 
            this.buttonFiltrar.Location = new System.Drawing.Point(367, 6);
            this.buttonFiltrar.Name = ""buttonFiltrar"";
            this.buttonFiltrar.Size = new System.Drawing.Size(75, 23);
            this.buttonFiltrar.TabIndex = 6;
            this.buttonFiltrar.Text = ""Filtrar ⚙️"";
            this.buttonFiltrar.UseVisualStyleBackColor = true;
            this.buttonFiltrar.Click += new System.EventHandler(this.buttonFiltrar_Click);
            // 
            // buttonCerrar
            // 
            this.buttonCerrar.Location = new System.Drawing.Point(605, 6);
            this.buttonCerrar.Name = ""buttonCerrar"";
            this.buttonCerrar.Size = new System.Drawing.Size(75, 23);
            this.buttonCerrar.TabIndex = 5;
            this.buttonCerrar.Text = ""Cerrar ❌"";
            this.buttonCerrar.UseVisualStyleBackColor = true;
            this.buttonCerrar.Click += new System.EventHandler(this.buttonCerrar_Click);
            // 
            // buttonConsultar
            // 
            this.buttonConsultar.Location = new System.Drawing.Point(248, 6);
            this.buttonConsultar.Name = ""buttonConsultar"";
            this.buttonConsultar.Size = new System.Drawing.Size(75, 23);
            this.buttonConsultar.TabIndex = 4;
            this.buttonConsultar.Text = ""Consultar 👁"";
            this.buttonConsultar.UseVisualStyleBackColor = true;
            this.buttonConsultar.Click += new System.EventHandler(this.buttonConsultar_Click);
            // 
            // buttonEliminar
            // 
            this.buttonEliminar.Location = new System.Drawing.Point(486, 6);
            this.buttonEliminar.Name = ""buttonEliminar"";
            this.buttonEliminar.Size = new System.Drawing.Size(75, 23);
            this.buttonEliminar.TabIndex = 3;
            this.buttonEliminar.Text = ""Eliminar 🗑"";
            this.buttonEliminar.UseVisualStyleBackColor = true;
            this.buttonEliminar.Click += new System.EventHandler(this.buttonEliminar_Click);
            // 
            // buttonActualizar
            // 
            this.buttonActualizar.Location = new System.Drawing.Point(129, 6);
            this.buttonActualizar.Name = ""buttonActualizar"";
            this.buttonActualizar.Size = new System.Drawing.Size(75, 23);
            this.buttonActualizar.TabIndex = 2;
            this.buttonActualizar.Text = ""Editar ✏"";
            this.buttonActualizar.UseVisualStyleBackColor = true;
            this.buttonActualizar.Click += new System.EventHandler(this.buttonActualizar_Click);
            // 
            // buttonCrear
            // 
            this.buttonCrear.Location = new System.Drawing.Point(10, 6);
            this.buttonCrear.Name = ""buttonCrear"";
            this.buttonCrear.Size = new System.Drawing.Size(75, 23);
            this.buttonCrear.TabIndex = 1;
            this.buttonCrear.Text = ""Crear ➕"";
            this.buttonCrear.UseVisualStyleBackColor = true;
            this.buttonCrear.Click += new System.EventHandler(this.buttonCrear_Click);
            // 
            // panelCampos
            // 
            this.panelCampos.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelCampos.Controls.Add(this.panelCamposPrincipal);
            this.panelCampos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCampos.Location = new System.Drawing.Point(3, 0);
            this.panelCampos.Name = ""panelCampos"";
            this.panelCampos.Padding = new System.Windows.Forms.Padding(30, 50, 30, 50);
            this.panelCampos.Size = new System.Drawing.Size(693, 680);
            this.panelCampos.TabIndex = 3;
            // 
            // panelCamposPrincipal
            // 
            this.panelCamposPrincipal.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelCamposPrincipal.Controls.Add(this.panelBotonesCampos);
            this.panelCamposPrincipal.Controls.Add(this.panelCentralCampos);
            this.panelCamposPrincipal.Controls.Add(this.panelSuperiorCampos);
            this.panelCamposPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCamposPrincipal.Location = new System.Drawing.Point(30, 50);
            this.panelCamposPrincipal.Name = ""panelCamposPrincipal"";
            this.panelCamposPrincipal.Size = new System.Drawing.Size(633, 580);
            this.panelCamposPrincipal.TabIndex = 0;
            // 
            // panelBotonesCampos
            // 
            this.panelBotonesCampos.BackColor = System.Drawing.Color.White;
            this.panelBotonesCampos.Controls.Add(this.buttonCerrarCampos);
            this.panelBotonesCampos.Controls.Add(this.buttonGrabarCampos);
            this.panelBotonesCampos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotonesCampos.Location = new System.Drawing.Point(0, 544);
            this.panelBotonesCampos.Name = ""panelBotonesCampos"";
            this.panelBotonesCampos.Size = new System.Drawing.Size(633, 36);
            this.panelBotonesCampos.TabIndex = 2;
            // 
            // buttonCerrarCampos
            // 
            this.buttonCerrarCampos.Location = new System.Drawing.Point(356, 3);
            this.buttonCerrarCampos.Name = ""buttonCerrarCampos"";
            this.buttonCerrarCampos.Size = new System.Drawing.Size(75, 23);
            this.buttonCerrarCampos.TabIndex = 1;
            this.buttonCerrarCampos.Text = ""Cerrar ❌"";
            this.buttonCerrarCampos.UseVisualStyleBackColor = true;
            this.buttonCerrarCampos.Click += new System.EventHandler(this.buttonCerrarCampos_Click);
            // 
            // buttonGrabarCampos
            // 
            this.buttonGrabarCampos.Location = new System.Drawing.Point(171, 3);
            this.buttonGrabarCampos.Name = ""buttonGrabarCampos"";
            this.buttonGrabarCampos.Size = new System.Drawing.Size(75, 23);
            this.buttonGrabarCampos.TabIndex = 0;
            this.buttonGrabarCampos.Text = ""Grabar ➕"";
            this.buttonGrabarCampos.UseVisualStyleBackColor = true;
            this.buttonGrabarCampos.Click += new System.EventHandler(this.buttonGrabarCampos_Click);
            // 
            // panelCentralCampos
            // 
            this.panelCentralCampos.BackColor = System.Drawing.Color.White;{AgregacionAPanel}
            this.panelCentralCampos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentralCampos.Location = new System.Drawing.Point(0, 33);
            this.panelCentralCampos.Name = ""panelCentralCampos"";
            this.panelCentralCampos.Size = new System.Drawing.Size(633, 547);
            this.panelCentralCampos.TabIndex = 1;{AtributosComponentes}
            // 
            // panelSuperiorCampos
            // 
            this.panelSuperiorCampos.Controls.Add(this.labelTextoCampos);
            this.panelSuperiorCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperiorCampos.Location = new System.Drawing.Point(0, 0);
            this.panelSuperiorCampos.Name = ""panelSuperiorCampos"";
            this.panelSuperiorCampos.Size = new System.Drawing.Size(633, 33);
            this.panelSuperiorCampos.TabIndex = 0;
            // 
            // labelTextoCampos
            // 
            this.labelTextoCampos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTextoCampos.Font = new System.Drawing.Font(""Times New Roman"", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTextoCampos.Location = new System.Drawing.Point(0, 0);
            this.labelTextoCampos.Name = ""labelTextoCampos"";
            this.labelTextoCampos.Size = new System.Drawing.Size(633, 33);
            this.labelTextoCampos.TabIndex = 1;
            this.labelTextoCampos.Text = ""Grabar - Editar - Consultar"";
            this.labelTextoCampos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFiltros
            // 
            this.panelFiltros.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelFiltros.Controls.Add(this.panelFiltrosPrincipal);
            this.panelFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiltros.Location = new System.Drawing.Point(3, 33);
            this.panelFiltros.Name = ""panelFiltros"";
            this.panelFiltros.Padding = new System.Windows.Forms.Padding(30, 50, 30, 50);
            this.panelFiltros.Size = new System.Drawing.Size(693, 647);
            this.panelFiltros.TabIndex = 4;
            // 
            // panelFiltrosPrincipal
            // 
            this.panelFiltrosPrincipal.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelFiltrosPrincipal.Controls.Add(this.panelBotonesFiltros);
            this.panelFiltrosPrincipal.Controls.Add(this.panelSuperiorFiltros);
            this.panelFiltrosPrincipal.Controls.Add(this.panelCentralFiltros);
            this.panelFiltrosPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiltrosPrincipal.Location = new System.Drawing.Point(30, 50);
            this.panelFiltrosPrincipal.Name = ""panelFiltrosPrincipal"";
            this.panelFiltrosPrincipal.Size = new System.Drawing.Size(633, 547);
            this.panelFiltrosPrincipal.TabIndex = 0;
            // 
            // panelBotonesFiltros
            // 
            this.panelBotonesFiltros.BackColor = System.Drawing.Color.White;
            this.panelBotonesFiltros.Controls.Add(this.buttonCerrarFiltros);
            this.panelBotonesFiltros.Controls.Add(this.buttonGenerarFiltros);
            this.panelBotonesFiltros.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotonesFiltros.Location = new System.Drawing.Point(0, 511);
            this.panelBotonesFiltros.Name = ""panelBotonesFiltros"";
            this.panelBotonesFiltros.Size = new System.Drawing.Size(633, 36);
            this.panelBotonesFiltros.TabIndex = 2;
            // 
            // buttonCerrarFiltros
            // 
            this.buttonCerrarFiltros.Location = new System.Drawing.Point(356, 3);
            this.buttonCerrarFiltros.Name = ""buttonCerrarFiltros"";
            this.buttonCerrarFiltros.Size = new System.Drawing.Size(75, 23);
            this.buttonCerrarFiltros.TabIndex = 1;
            this.buttonCerrarFiltros.Text = ""Cerrar ❌"";
            this.buttonCerrarFiltros.UseVisualStyleBackColor = true;
            this.buttonCerrarFiltros.Click += new System.EventHandler(this.buttonCerrarFiltros_Click);
            // 
            // buttonGenerarFiltros
            // 
            this.buttonGenerarFiltros.Location = new System.Drawing.Point(171, 3);
            this.buttonGenerarFiltros.Name = ""buttonGenerarFiltros"";
            this.buttonGenerarFiltros.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerarFiltros.TabIndex = 0;
            this.buttonGenerarFiltros.Text = ""Filtrar 🔎"";
            this.buttonGenerarFiltros.UseVisualStyleBackColor = true;
            this.buttonGenerarFiltros.Click += new System.EventHandler(this.buttonGenerarFiltros_Click);
            // 
            // panelSuperiorFiltros
            // 
            this.panelSuperiorFiltros.Controls.Add(this.labelTituloFiltros);
            this.panelSuperiorFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperiorFiltros.Location = new System.Drawing.Point(0, 0);
            this.panelSuperiorFiltros.Name = ""panelSuperiorFiltros"";
            this.panelSuperiorFiltros.Size = new System.Drawing.Size(633, 33);
            this.panelSuperiorFiltros.TabIndex = 0;
            // 
            // labelTituloFiltros
            // 
            this.labelTituloFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTituloFiltros.Font = new System.Drawing.Font(""Times New Roman"", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTituloFiltros.Location = new System.Drawing.Point(0, 0);
            this.labelTituloFiltros.Name = ""labelTituloFiltros"";
            this.labelTituloFiltros.Size = new System.Drawing.Size(633, 33);
            this.labelTituloFiltros.TabIndex = 1;
            this.labelTituloFiltros.Text = ""Filtros"";
            this.labelTituloFiltros.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelCentralFiltros
            // 
            this.panelCentralFiltros.BackColor = System.Drawing.Color.White;{AgregarAPanelFiltros}
            this.panelCentralFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentralFiltros.Location = new System.Drawing.Point(0, 0);
            this.panelCentralFiltros.Name = ""panelCentralFiltros"";
            this.panelCentralFiltros.Size = new System.Drawing.Size(633, 547);
            this.panelCentralFiltros.TabIndex = 1;{AtributosComponentesFiltros}
            // 
            // FormularioPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(699, 683);
            this.Controls.Add(this.panelPrincipal);
            this.Controls.Add(this.panelFiltros);
            this.Controls.Add(this.panelTitulo);
            this.Controls.Add(this.panelCampos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = ""FormularioPrincipal"";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ""Gestión de {NombreTabla}"";
            this.panelTitulo.ResumeLayout(false);
            this.panelPrincipal.ResumeLayout(false);
            this.panelCentral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTabla)).EndInit();
            this.panelRegistros.ResumeLayout(false);
            this.panelRegistros.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.panelCampos.ResumeLayout(false);
            this.panelCamposPrincipal.ResumeLayout(false);
            this.panelBotonesCampos.ResumeLayout(false);
            this.panelCentralCampos.ResumeLayout(false);
            this.panelCentralCampos.PerformLayout();
            this.panelSuperiorCampos.ResumeLayout(false);
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltrosPrincipal.ResumeLayout(false);
            this.panelBotonesFiltros.ResumeLayout(false);
            this.panelSuperiorFiltros.ResumeLayout(false);
            this.panelCentralFiltros.ResumeLayout(false);
            this.panelCentralFiltros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Label labelTextoTitulo;
        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.Panel panelCentral;
        private System.Windows.Forms.DataGridView dataGridViewTabla;
        private System.Windows.Forms.Panel panelRegistros;
        private System.Windows.Forms.Label labelRegistros;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button buttonFiltrar;
        private System.Windows.Forms.Button buttonCerrar;
        private System.Windows.Forms.Button buttonConsultar;
        private System.Windows.Forms.Button buttonEliminar;
        private System.Windows.Forms.Button buttonActualizar;
        private System.Windows.Forms.Button buttonCrear;
        private System.Windows.Forms.Panel panelCampos;
        private System.Windows.Forms.Panel panelCamposPrincipal;
        private System.Windows.Forms.Panel panelBotonesCampos;
        private System.Windows.Forms.Button buttonCerrarCampos;
        private System.Windows.Forms.Button buttonGrabarCampos;
        private System.Windows.Forms.Panel panelCentralCampos;
        private System.Windows.Forms.Panel panelSuperiorCampos;{DefinicionFinalComponentes}
        private System.Windows.Forms.Label labelTextoCampos;
        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Panel panelFiltrosPrincipal;
        private System.Windows.Forms.Panel panelBotonesFiltros;
        private System.Windows.Forms.Button buttonCerrarFiltros;
        private System.Windows.Forms.Button buttonGenerarFiltros;
        private System.Windows.Forms.Panel panelCentralFiltros;
        private System.Windows.Forms.Panel panelSuperiorFiltros;
        private System.Windows.Forms.Label labelTituloFiltros;{DefinicionFinalColumnas}
    }
}
";

        static readonly public string PlantillaResx =
@"<?xml version=""1.0"" encoding=""utf-8""?>
<root>
  <!-- 
    Microsoft ResX Schema 
    
    Version 2.0
    
    The primary goals of this format is to allow a simple XML format 
    that is mostly human readable. The generation and parsing of the 
    various data types are done through the TypeConverter classes 
    associated with the data types.
    
    Example:
    
    ... ado.net/XML headers & schema ...
    <resheader name=""resmimetype"">text/microsoft-resx</resheader>
    <resheader name=""version"">2.0</resheader>
    <resheader name=""reader"">System.Resources.ResXResourceReader, System.Windows.Forms, ...</resheader>
    <resheader name=""writer"">System.Resources.ResXResourceWriter, System.Windows.Forms, ...</resheader>
    <data name=""Name1""><value>this is my long string</value><comment>this is a comment</comment></data>
    <data name=""Color1"" type=""System.Drawing.Color, System.Drawing"">Blue</data>
    <data name=""Bitmap1"" mimetype=""application/x-microsoft.net.object.binary.base64"">
        <value>[base64 mime encoded serialized .NET Framework object]</value>
    </data>
    <data name=""Icon1"" type=""System.Drawing.Icon, System.Drawing"" mimetype=""application/x-microsoft.net.object.bytearray.base64"">
        <value>[base64 mime encoded string representing a byte array form of the .NET Framework object]</value>
        <comment>This is a comment</comment>
    </data>
                
    There are any number of ""resheader"" rows that contain simple 
    name/value pairs.
    
    Each data row contains a name, and value. The row also contains a 
    type or mimetype. Type corresponds to a .NET class that support 
    text/value conversion through the TypeConverter architecture. 
    Classes that don't support this are serialized and stored with the 
    mimetype set.
    
    The mimetype is used for serialized objects, and tells the 
    ResXResourceReader how to depersist the object. This is currently not 
    extensible. For a given mimetype the value must be set accordingly:
    
    Note - application/x-microsoft.net.object.binary.base64 is the format 
    that the ResXResourceWriter will generate, however the reader can 
    read any of the formats listed below.
    
    mimetype: application/x-microsoft.net.object.binary.base64
    value   : The object must be serialized with 
            : System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            : and then encoded with base64 encoding.
    
    mimetype: application/x-microsoft.net.object.soap.base64
    value   : The object must be serialized with 
            : System.Runtime.Serialization.Formatters.Soap.SoapFormatter
            : and then encoded with base64 encoding.

    mimetype: application/x-microsoft.net.object.bytearray.base64
    value   : The object must be serialized into a byte array 
            : using a System.ComponentModel.TypeConverter
            : and then encoded with base64 encoding.
    -->
  <xsd:schema id=""root"" xmlns="""" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:msdata=""urn:schemas-microsoft-com:xml-msdata"">
    <xsd:import namespace=""http://www.w3.org/XML/1998/namespace"" />
    <xsd:element name=""root"" msdata:IsDataSet=""true"">
      <xsd:complexType>
        <xsd:choice maxOccurs=""unbounded"">
          <xsd:element name=""metadata"">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name=""value"" type=""xsd:string"" minOccurs=""0"" />
              </xsd:sequence>
              <xsd:attribute name=""name"" use=""required"" type=""xsd:string"" />
              <xsd:attribute name=""type"" type=""xsd:string"" />
              <xsd:attribute name=""mimetype"" type=""xsd:string"" />
              <xsd:attribute ref=""xml:space"" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name=""assembly"">
            <xsd:complexType>
              <xsd:attribute name=""alias"" type=""xsd:string"" />
              <xsd:attribute name=""name"" type=""xsd:string"" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name=""data"">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name=""value"" type=""xsd:string"" minOccurs=""0"" msdata:Ordinal=""1"" />
                <xsd:element name=""comment"" type=""xsd:string"" minOccurs=""0"" msdata:Ordinal=""2"" />
              </xsd:sequence>
              <xsd:attribute name=""name"" type=""xsd:string"" use=""required"" msdata:Ordinal=""1"" />
              <xsd:attribute name=""type"" type=""xsd:string"" msdata:Ordinal=""3"" />
              <xsd:attribute name=""mimetype"" type=""xsd:string"" msdata:Ordinal=""4"" />
              <xsd:attribute ref=""xml:space"" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name=""resheader"">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name=""value"" type=""xsd:string"" minOccurs=""0"" msdata:Ordinal=""1"" />
              </xsd:sequence>
              <xsd:attribute name=""name"" type=""xsd:string"" use=""required"" />
            </xsd:complexType>
          </xsd:element>
        </xsd:choice>
      </xsd:complexType>
    </xsd:element>
  </xsd:schema>
  <resheader name=""resmimetype"">
    <value>text/microsoft-resx</value>
  </resheader>
  <resheader name=""version"">
    <value>2.0</value>
  </resheader>
  <resheader name=""reader"">
    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
  <resheader name=""writer"">
    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
</root>
";
    }
}
