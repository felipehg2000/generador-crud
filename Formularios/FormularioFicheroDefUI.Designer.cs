namespace generador_crud.Formularios
{
    partial class FormularioFicheroDefUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panelPrincipal = new Panel();
            panelSuperior = new Panel();
            labelTitulo = new Label();
            panelCentral = new Panel();
            buttonDirectorioSalida = new Button();
            textBoxDirectorioSalida = new TextBox();
            labelDirectorioSalida = new Label();
            panelHerramientas = new Panel();
            buttonSelTabla = new Button();
            buttonSelFiltro = new Button();
            buttonEdicion = new Button();
            labelTextoHerramientas = new Label();
            dataGridViewCampos = new DataGridView();
            ColumnaNombre = new DataGridViewTextBoxColumn();
            ColumnaTipoDato = new DataGridViewTextBoxColumn();
            ColumnaPK = new DataGridViewTextBoxColumn();
            ColumnaTabla = new DataGridViewTextBoxColumn();
            ColumnaEdicion = new DataGridViewTextBoxColumn();
            ColumnaFiltro = new DataGridViewTextBoxColumn();
            labelTablasProcesadas = new Label();
            comboBoxTablas = new ComboBox();
            panelBotones = new Panel();
            buttonGenerar = new Button();
            buttonCerrar = new Button();
            bindingSource1 = new BindingSource(components);
            panelPrincipal.SuspendLayout();
            panelSuperior.SuspendLayout();
            panelCentral.SuspendLayout();
            panelHerramientas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCampos).BeginInit();
            panelBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // panelPrincipal
            // 
            panelPrincipal.BackColor = SystemColors.ActiveCaption;
            panelPrincipal.BorderStyle = BorderStyle.FixedSingle;
            panelPrincipal.Controls.Add(panelSuperior);
            panelPrincipal.Controls.Add(panelCentral);
            panelPrincipal.Controls.Add(panelBotones);
            panelPrincipal.Dock = DockStyle.Fill;
            panelPrincipal.Location = new Point(0, 0);
            panelPrincipal.Name = "panelPrincipal";
            panelPrincipal.Size = new Size(800, 450);
            panelPrincipal.TabIndex = 0;
            // 
            // panelSuperior
            // 
            panelSuperior.Controls.Add(labelTitulo);
            panelSuperior.Dock = DockStyle.Top;
            panelSuperior.Location = new Point(0, 0);
            panelSuperior.Name = "panelSuperior";
            panelSuperior.Size = new Size(798, 32);
            panelSuperior.TabIndex = 0;
            // 
            // labelTitulo
            // 
            labelTitulo.Dock = DockStyle.Fill;
            labelTitulo.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitulo.Location = new Point(0, 0);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(798, 32);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "Generadro de ficheros de definición de interfaz";
            labelTitulo.TextAlign = ContentAlignment.MiddleCenter;
            labelTitulo.MouseDown += labelTitulo_MouseDown;
            // 
            // panelCentral
            // 
            panelCentral.BackColor = SystemColors.Control;
            panelCentral.Controls.Add(buttonDirectorioSalida);
            panelCentral.Controls.Add(textBoxDirectorioSalida);
            panelCentral.Controls.Add(labelDirectorioSalida);
            panelCentral.Controls.Add(panelHerramientas);
            panelCentral.Controls.Add(dataGridViewCampos);
            panelCentral.Controls.Add(labelTablasProcesadas);
            panelCentral.Controls.Add(comboBoxTablas);
            panelCentral.Dock = DockStyle.Fill;
            panelCentral.Location = new Point(0, 0);
            panelCentral.Margin = new Padding(5, 0, 5, 0);
            panelCentral.Name = "panelCentral";
            panelCentral.Size = new Size(798, 399);
            panelCentral.TabIndex = 1;
            // 
            // buttonDirectorioSalida
            // 
            buttonDirectorioSalida.Location = new Point(752, 57);
            buttonDirectorioSalida.Name = "buttonDirectorioSalida";
            buttonDirectorioSalida.Size = new Size(26, 23);
            buttonDirectorioSalida.TabIndex = 6;
            buttonDirectorioSalida.Text = "🔎";
            buttonDirectorioSalida.UseVisualStyleBackColor = true;
            buttonDirectorioSalida.Click += buttonDirectorioSalida_Click;
            // 
            // textBoxDirectorioSalida
            // 
            textBoxDirectorioSalida.BackColor = Color.Beige;
            textBoxDirectorioSalida.Enabled = false;
            textBoxDirectorioSalida.Location = new Point(289, 58);
            textBoxDirectorioSalida.Name = "textBoxDirectorioSalida";
            textBoxDirectorioSalida.Size = new Size(457, 23);
            textBoxDirectorioSalida.TabIndex = 5;
            // 
            // labelDirectorioSalida
            // 
            labelDirectorioSalida.AutoSize = true;
            labelDirectorioSalida.Location = new Point(289, 40);
            labelDirectorioSalida.Name = "labelDirectorioSalida";
            labelDirectorioSalida.Size = new Size(108, 15);
            labelDirectorioSalida.TabIndex = 4;
            labelDirectorioSalida.Text = "Directorio de salida";
            // 
            // panelHerramientas
            // 
            panelHerramientas.BackColor = SystemColors.ActiveCaption;
            panelHerramientas.Controls.Add(buttonSelTabla);
            panelHerramientas.Controls.Add(buttonSelFiltro);
            panelHerramientas.Controls.Add(buttonEdicion);
            panelHerramientas.Controls.Add(labelTextoHerramientas);
            panelHerramientas.Location = new Point(684, 102);
            panelHerramientas.Name = "panelHerramientas";
            panelHerramientas.Size = new Size(104, 276);
            panelHerramientas.TabIndex = 3;
            // 
            // buttonSelTabla
            // 
            buttonSelTabla.Dock = DockStyle.Top;
            buttonSelTabla.Location = new Point(0, 69);
            buttonSelTabla.Name = "buttonSelTabla";
            buttonSelTabla.Size = new Size(104, 23);
            buttonSelTabla.TabIndex = 1;
            buttonSelTabla.Text = "Sel. Tabla";
            buttonSelTabla.UseVisualStyleBackColor = true;
            buttonSelTabla.Click += buttonSelTabla_Click;
            // 
            // buttonSelFiltro
            // 
            buttonSelFiltro.Dock = DockStyle.Top;
            buttonSelFiltro.Location = new Point(0, 46);
            buttonSelFiltro.Name = "buttonSelFiltro";
            buttonSelFiltro.Size = new Size(104, 23);
            buttonSelFiltro.TabIndex = 3;
            buttonSelFiltro.Text = "Sel. Filtro";
            buttonSelFiltro.UseVisualStyleBackColor = true;
            buttonSelFiltro.Click += buttonSelFiltro_Click;
            // 
            // buttonEdicion
            // 
            buttonEdicion.Dock = DockStyle.Top;
            buttonEdicion.Location = new Point(0, 23);
            buttonEdicion.Name = "buttonEdicion";
            buttonEdicion.Size = new Size(104, 23);
            buttonEdicion.TabIndex = 2;
            buttonEdicion.Text = "Sel. Edicion";
            buttonEdicion.UseVisualStyleBackColor = true;
            buttonEdicion.Click += buttonEdicion_Click;
            // 
            // labelTextoHerramientas
            // 
            labelTextoHerramientas.Dock = DockStyle.Top;
            labelTextoHerramientas.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTextoHerramientas.Location = new Point(0, 0);
            labelTextoHerramientas.Name = "labelTextoHerramientas";
            labelTextoHerramientas.Size = new Size(104, 23);
            labelTextoHerramientas.TabIndex = 0;
            labelTextoHerramientas.Text = "Herramientas";
            labelTextoHerramientas.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dataGridViewCampos
            // 
            dataGridViewCampos.AllowUserToAddRows = false;
            dataGridViewCampos.BackgroundColor = SystemColors.Control;
            dataGridViewCampos.BorderStyle = BorderStyle.None;
            dataGridViewCampos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.Navy;
            dataGridViewCellStyle2.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = Color.Navy;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewCampos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCampos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCampos.Columns.AddRange(new DataGridViewColumn[] { ColumnaNombre, ColumnaTipoDato, ColumnaPK, ColumnaTabla, ColumnaEdicion, ColumnaFiltro });
            dataGridViewCampos.EnableHeadersVisualStyles = false;
            dataGridViewCampos.Location = new Point(12, 102);
            dataGridViewCampos.Name = "dataGridViewCampos";
            dataGridViewCampos.RowHeadersVisible = false;
            dataGridViewCampos.ScrollBars = ScrollBars.Vertical;
            dataGridViewCampos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCampos.Size = new Size(656, 276);
            dataGridViewCampos.TabIndex = 2;
            // 
            // ColumnaNombre
            // 
            ColumnaNombre.HeaderText = "Nombre";
            ColumnaNombre.Name = "ColumnaNombre";
            // 
            // ColumnaTipoDato
            // 
            ColumnaTipoDato.HeaderText = "Tipo Dato";
            ColumnaTipoDato.Name = "ColumnaTipoDato";
            // 
            // ColumnaPK
            // 
            ColumnaPK.HeaderText = "P.K.";
            ColumnaPK.Name = "ColumnaPK";
            // 
            // ColumnaTabla
            // 
            ColumnaTabla.HeaderText = "Mostrar en tabla";
            ColumnaTabla.Name = "ColumnaTabla";
            ColumnaTabla.ReadOnly = true;
            // 
            // ColumnaEdicion
            // 
            ColumnaEdicion.HeaderText = "Edición";
            ColumnaEdicion.Name = "ColumnaEdicion";
            // 
            // ColumnaFiltro
            // 
            ColumnaFiltro.HeaderText = "Filtro";
            ColumnaFiltro.Name = "ColumnaFiltro";
            // 
            // labelTablasProcesadas
            // 
            labelTablasProcesadas.AutoSize = true;
            labelTablasProcesadas.Location = new Point(17, 40);
            labelTablasProcesadas.Name = "labelTablasProcesadas";
            labelTablasProcesadas.Size = new Size(101, 15);
            labelTablasProcesadas.TabIndex = 1;
            labelTablasProcesadas.Text = "Tablas Procesadas";
            // 
            // comboBoxTablas
            // 
            comboBoxTablas.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTablas.FormattingEnabled = true;
            comboBoxTablas.Location = new Point(17, 58);
            comboBoxTablas.Name = "comboBoxTablas";
            comboBoxTablas.Size = new Size(217, 23);
            comboBoxTablas.TabIndex = 0;
            comboBoxTablas.SelectedIndexChanged += comboBoxTablas_SelectedIndexChanged;
            // 
            // panelBotones
            // 
            panelBotones.BackColor = SystemColors.Control;
            panelBotones.Controls.Add(buttonGenerar);
            panelBotones.Controls.Add(buttonCerrar);
            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Location = new Point(0, 399);
            panelBotones.Margin = new Padding(5, 0, 5, 5);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(798, 49);
            panelBotones.TabIndex = 2;
            // 
            // buttonGenerar
            // 
            buttonGenerar.Location = new Point(695, 13);
            buttonGenerar.Name = "buttonGenerar";
            buttonGenerar.Size = new Size(78, 26);
            buttonGenerar.TabIndex = 1;
            buttonGenerar.Text = "Generar ✔";
            buttonGenerar.UseVisualStyleBackColor = true;
            buttonGenerar.Click += buttonGenerar_Click;
            // 
            // buttonCerrar
            // 
            buttonCerrar.Location = new Point(600, 13);
            buttonCerrar.Name = "buttonCerrar";
            buttonCerrar.Size = new Size(78, 26);
            buttonCerrar.TabIndex = 0;
            buttonCerrar.Text = "Cerrar ❌";
            buttonCerrar.UseVisualStyleBackColor = true;
            buttonCerrar.Click += buttonCerrar_Click;
            // 
            // FormularioFicheroDefUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelPrincipal);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormularioFicheroDefUI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Generar ficheros de definición de interfaz";
            panelPrincipal.ResumeLayout(false);
            panelSuperior.ResumeLayout(false);
            panelCentral.ResumeLayout(false);
            panelCentral.PerformLayout();
            panelHerramientas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewCampos).EndInit();
            panelBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelPrincipal;
        private Panel panelSuperior;
        private Panel panelCentral;
        private Panel panelBotones;
        private Label labelTitulo;
        private Label labelTablasProcesadas;
        private ComboBox comboBoxTablas;
        private BindingSource bindingSource1;
        private DataGridView dataGridViewCampos;
        private Button buttonGenerar;
        private Button buttonCerrar;
        private Panel panelHerramientas;
        private Label labelTextoHerramientas;
        private Button buttonSelFiltro;
        private Button buttonEdicion;
        private Label labelDirectorioSalida;
        private Button buttonDirectorioSalida;
        private TextBox textBoxDirectorioSalida;
        private DataGridViewTextBoxColumn ColumnaNombre;
        private DataGridViewTextBoxColumn ColumnaTipoDato;
        private DataGridViewTextBoxColumn ColumnaPK;
        private DataGridViewTextBoxColumn ColumnaTabla;
        private DataGridViewTextBoxColumn ColumnaEdicion;
        private DataGridViewTextBoxColumn ColumnaFiltro;
        private Button buttonSelTabla;
    }
}