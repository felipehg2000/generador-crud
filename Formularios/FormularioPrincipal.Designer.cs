namespace generador_crud
{
    partial class FormularioPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelPrincipal = new Panel();
            labelTipoBD = new Label();
            radioButtonSQLite = new RadioButton();
            radioButtonFirebird = new RadioButton();
            checkBoxUnificarDestinos = new CheckBox();
            buttonDestinoModelo = new Button();
            textBoxDestinoModelo = new TextBox();
            labelDestinoModelo = new Label();
            buttonDestinoControladorDAO = new Button();
            textBoxDestinoControladorDAO = new TextBox();
            labelDestinoControladorDAO = new Label();
            buttonDestinoControlador = new Button();
            textBoxDestinoControlador = new TextBox();
            labelDestinoControlador = new Label();
            buttonRutaDestinoUI = new Button();
            textBoxRutaDestinoUI = new TextBox();
            labelDestinoInterfaz = new Label();
            checkBoxSobreescribir = new CheckBox();
            checkBoxDefinicionesUI = new CheckBox();
            buttonRutaUI = new Button();
            textBoxRutaUI = new TextBox();
            labelRutaUI = new Label();
            buttonRutaDDL = new Button();
            textBoxRutaDDL = new TextBox();
            labelRutaDDL = new Label();
            panelBotones = new Panel();
            buttonGenerar = new Button();
            menuPrincipal = new MenuStrip();
            ayudaToolStripMenuItem = new ToolStripMenuItem();
            exportarConfiguraciónToolStripMenuItem = new ToolStripMenuItem();
            importarConfiguraciónToolStripMenuItem = new ToolStripMenuItem();
            generarFicherosDefinicionUIToolStripMenuItem = new ToolStripMenuItem();
            panelPrincipal.SuspendLayout();
            panelBotones.SuspendLayout();
            menuPrincipal.SuspendLayout();
            SuspendLayout();
            // 
            // panelPrincipal
            // 
            panelPrincipal.Controls.Add(labelTipoBD);
            panelPrincipal.Controls.Add(radioButtonSQLite);
            panelPrincipal.Controls.Add(radioButtonFirebird);
            panelPrincipal.Controls.Add(checkBoxUnificarDestinos);
            panelPrincipal.Controls.Add(buttonDestinoModelo);
            panelPrincipal.Controls.Add(textBoxDestinoModelo);
            panelPrincipal.Controls.Add(labelDestinoModelo);
            panelPrincipal.Controls.Add(buttonDestinoControladorDAO);
            panelPrincipal.Controls.Add(textBoxDestinoControladorDAO);
            panelPrincipal.Controls.Add(labelDestinoControladorDAO);
            panelPrincipal.Controls.Add(buttonDestinoControlador);
            panelPrincipal.Controls.Add(textBoxDestinoControlador);
            panelPrincipal.Controls.Add(labelDestinoControlador);
            panelPrincipal.Controls.Add(buttonRutaDestinoUI);
            panelPrincipal.Controls.Add(textBoxRutaDestinoUI);
            panelPrincipal.Controls.Add(labelDestinoInterfaz);
            panelPrincipal.Controls.Add(checkBoxSobreescribir);
            panelPrincipal.Controls.Add(checkBoxDefinicionesUI);
            panelPrincipal.Controls.Add(buttonRutaUI);
            panelPrincipal.Controls.Add(textBoxRutaUI);
            panelPrincipal.Controls.Add(labelRutaUI);
            panelPrincipal.Controls.Add(buttonRutaDDL);
            panelPrincipal.Controls.Add(textBoxRutaDDL);
            panelPrincipal.Controls.Add(labelRutaDDL);
            panelPrincipal.Dock = DockStyle.Fill;
            panelPrincipal.Location = new Point(0, 0);
            panelPrincipal.Name = "panelPrincipal";
            panelPrincipal.Size = new Size(494, 501);
            panelPrincipal.TabIndex = 0;
            // 
            // labelTipoBD
            // 
            labelTipoBD.AutoSize = true;
            labelTipoBD.Location = new Point(351, 388);
            labelTipoBD.Name = "labelTipoBD";
            labelTipoBD.Size = new Size(126, 15);
            labelTipoBD.TabIndex = 20;
            labelTipoBD.Text = "Tipo de Base De Datos:";
            // 
            // radioButtonSQLite
            // 
            radioButtonSQLite.AutoSize = true;
            radioButtonSQLite.Location = new Point(365, 422);
            radioButtonSQLite.Name = "radioButtonSQLite";
            radioButtonSQLite.Size = new Size(59, 19);
            radioButtonSQLite.TabIndex = 19;
            radioButtonSQLite.TabStop = true;
            radioButtonSQLite.Text = "SQLite";
            radioButtonSQLite.UseVisualStyleBackColor = true;
            // 
            // radioButtonFirebird
            // 
            radioButtonFirebird.AutoSize = true;
            radioButtonFirebird.Location = new Point(365, 406);
            radioButtonFirebird.Name = "radioButtonFirebird";
            radioButtonFirebird.Size = new Size(65, 19);
            radioButtonFirebird.TabIndex = 18;
            radioButtonFirebird.TabStop = true;
            radioButtonFirebird.Text = "Firebird";
            radioButtonFirebird.UseVisualStyleBackColor = true;
            // 
            // checkBoxUnificarDestinos
            // 
            checkBoxUnificarDestinos.AutoSize = true;
            checkBoxUnificarDestinos.Location = new Point(12, 438);
            checkBoxUnificarDestinos.Name = "checkBoxUnificarDestinos";
            checkBoxUnificarDestinos.Size = new Size(114, 19);
            checkBoxUnificarDestinos.TabIndex = 14;
            checkBoxUnificarDestinos.Text = "Unificar destinos";
            checkBoxUnificarDestinos.UseVisualStyleBackColor = true;
            checkBoxUnificarDestinos.CheckedChanged += checkBoxUnificarDestinos_CheckedChanged;
            // 
            // buttonDestinoModelo
            // 
            buttonDestinoModelo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonDestinoModelo.Location = new Point(451, 230);
            buttonDestinoModelo.Name = "buttonDestinoModelo";
            buttonDestinoModelo.Size = new Size(26, 23);
            buttonDestinoModelo.TabIndex = 7;
            buttonDestinoModelo.Text = "🔎";
            buttonDestinoModelo.UseVisualStyleBackColor = true;
            buttonDestinoModelo.Click += buttonDestinoModelo_Click;
            // 
            // textBoxDestinoModelo
            // 
            textBoxDestinoModelo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDestinoModelo.BackColor = Color.Beige;
            textBoxDestinoModelo.Enabled = false;
            textBoxDestinoModelo.Location = new Point(12, 230);
            textBoxDestinoModelo.Name = "textBoxDestinoModelo";
            textBoxDestinoModelo.Size = new Size(433, 23);
            textBoxDestinoModelo.TabIndex = 6;
            // 
            // labelDestinoModelo
            // 
            labelDestinoModelo.AutoSize = true;
            labelDestinoModelo.Location = new Point(12, 212);
            labelDestinoModelo.Name = "labelDestinoModelo";
            labelDestinoModelo.Size = new Size(161, 15);
            labelDestinoModelo.TabIndex = 17;
            labelDestinoModelo.Text = "Directorio de destino modelo";
            // 
            // buttonDestinoControladorDAO
            // 
            buttonDestinoControladorDAO.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonDestinoControladorDAO.Location = new Point(451, 168);
            buttonDestinoControladorDAO.Name = "buttonDestinoControladorDAO";
            buttonDestinoControladorDAO.Size = new Size(26, 23);
            buttonDestinoControladorDAO.TabIndex = 5;
            buttonDestinoControladorDAO.Text = "🔎";
            buttonDestinoControladorDAO.UseVisualStyleBackColor = true;
            buttonDestinoControladorDAO.Click += buttonDestinoControladorDAO_Click;
            // 
            // textBoxDestinoControladorDAO
            // 
            textBoxDestinoControladorDAO.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDestinoControladorDAO.BackColor = Color.Beige;
            textBoxDestinoControladorDAO.Enabled = false;
            textBoxDestinoControladorDAO.Location = new Point(12, 168);
            textBoxDestinoControladorDAO.Name = "textBoxDestinoControladorDAO";
            textBoxDestinoControladorDAO.Size = new Size(433, 23);
            textBoxDestinoControladorDAO.TabIndex = 4;
            // 
            // labelDestinoControladorDAO
            // 
            labelDestinoControladorDAO.AutoSize = true;
            labelDestinoControladorDAO.Location = new Point(12, 150);
            labelDestinoControladorDAO.Name = "labelDestinoControladorDAO";
            labelDestinoControladorDAO.Size = new Size(210, 15);
            labelDestinoControladorDAO.TabIndex = 14;
            labelDestinoControladorDAO.Text = "Directorio de destino controlador DAO";
            // 
            // buttonDestinoControlador
            // 
            buttonDestinoControlador.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonDestinoControlador.Location = new Point(451, 110);
            buttonDestinoControlador.Name = "buttonDestinoControlador";
            buttonDestinoControlador.Size = new Size(26, 23);
            buttonDestinoControlador.TabIndex = 3;
            buttonDestinoControlador.Text = "🔎";
            buttonDestinoControlador.UseVisualStyleBackColor = true;
            buttonDestinoControlador.Click += buttonDestinoControlador_Click;
            // 
            // textBoxDestinoControlador
            // 
            textBoxDestinoControlador.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDestinoControlador.BackColor = Color.Beige;
            textBoxDestinoControlador.Enabled = false;
            textBoxDestinoControlador.Location = new Point(12, 110);
            textBoxDestinoControlador.Name = "textBoxDestinoControlador";
            textBoxDestinoControlador.Size = new Size(433, 23);
            textBoxDestinoControlador.TabIndex = 2;
            // 
            // labelDestinoControlador
            // 
            labelDestinoControlador.AutoSize = true;
            labelDestinoControlador.Location = new Point(12, 92);
            labelDestinoControlador.Name = "labelDestinoControlador";
            labelDestinoControlador.Size = new Size(182, 15);
            labelDestinoControlador.TabIndex = 11;
            labelDestinoControlador.Text = "Directorio de destino controlador";
            // 
            // buttonRutaDestinoUI
            // 
            buttonRutaDestinoUI.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRutaDestinoUI.Location = new Point(451, 53);
            buttonRutaDestinoUI.Name = "buttonRutaDestinoUI";
            buttonRutaDestinoUI.Size = new Size(26, 23);
            buttonRutaDestinoUI.TabIndex = 1;
            buttonRutaDestinoUI.Text = "🔎";
            buttonRutaDestinoUI.UseVisualStyleBackColor = true;
            buttonRutaDestinoUI.Click += buttonRutaDestinoUI_Click;
            // 
            // textBoxRutaDestinoUI
            // 
            textBoxRutaDestinoUI.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxRutaDestinoUI.BackColor = Color.Beige;
            textBoxRutaDestinoUI.Enabled = false;
            textBoxRutaDestinoUI.Location = new Point(12, 53);
            textBoxRutaDestinoUI.Name = "textBoxRutaDestinoUI";
            textBoxRutaDestinoUI.Size = new Size(433, 23);
            textBoxRutaDestinoUI.TabIndex = 0;
            // 
            // labelDestinoInterfaz
            // 
            labelDestinoInterfaz.AutoSize = true;
            labelDestinoInterfaz.Location = new Point(12, 35);
            labelDestinoInterfaz.Name = "labelDestinoInterfaz";
            labelDestinoInterfaz.Size = new Size(187, 15);
            labelDestinoInterfaz.TabIndex = 8;
            labelDestinoInterfaz.Text = "Directorio de destino de la interfaz";
            // 
            // checkBoxSobreescribir
            // 
            checkBoxSobreescribir.AutoSize = true;
            checkBoxSobreescribir.Location = new Point(12, 413);
            checkBoxSobreescribir.Name = "checkBoxSobreescribir";
            checkBoxSobreescribir.Size = new Size(239, 19);
            checkBoxSobreescribir.TabIndex = 13;
            checkBoxSobreescribir.Text = "Sobreescribir ficheros de salida si existen";
            checkBoxSobreescribir.UseVisualStyleBackColor = true;
            // 
            // checkBoxDefinicionesUI
            // 
            checkBoxDefinicionesUI.AutoSize = true;
            checkBoxDefinicionesUI.Location = new Point(12, 388);
            checkBoxDefinicionesUI.Name = "checkBoxDefinicionesUI";
            checkBoxDefinicionesUI.Size = new Size(304, 19);
            checkBoxDefinicionesUI.TabIndex = 12;
            checkBoxDefinicionesUI.Text = "Generar Fichero de Definiciones UI automáticamente";
            checkBoxDefinicionesUI.UseVisualStyleBackColor = true;
            // 
            // buttonRutaUI
            // 
            buttonRutaUI.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRutaUI.Location = new Point(451, 349);
            buttonRutaUI.Name = "buttonRutaUI";
            buttonRutaUI.Size = new Size(26, 23);
            buttonRutaUI.TabIndex = 11;
            buttonRutaUI.Text = "🔎";
            buttonRutaUI.UseVisualStyleBackColor = true;
            buttonRutaUI.Click += buttonRutaUI_Click;
            // 
            // textBoxRutaUI
            // 
            textBoxRutaUI.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxRutaUI.BackColor = Color.Beige;
            textBoxRutaUI.Enabled = false;
            textBoxRutaUI.Location = new Point(12, 349);
            textBoxRutaUI.Name = "textBoxRutaUI";
            textBoxRutaUI.Size = new Size(433, 23);
            textBoxRutaUI.TabIndex = 10;
            // 
            // labelRutaUI
            // 
            labelRutaUI.AutoSize = true;
            labelRutaUI.Location = new Point(12, 331);
            labelRutaUI.Name = "labelRutaUI";
            labelRutaUI.Size = new Size(147, 15);
            labelRutaUI.TabIndex = 3;
            labelRutaUI.Text = "Ruta formato de la interfaz";
            // 
            // buttonRutaDDL
            // 
            buttonRutaDDL.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRutaDDL.Location = new Point(451, 289);
            buttonRutaDDL.Name = "buttonRutaDDL";
            buttonRutaDDL.Size = new Size(26, 23);
            buttonRutaDDL.TabIndex = 9;
            buttonRutaDDL.Text = "🔎";
            buttonRutaDDL.UseVisualStyleBackColor = true;
            buttonRutaDDL.Click += buttonRutaDDL_Click;
            // 
            // textBoxRutaDDL
            // 
            textBoxRutaDDL.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxRutaDDL.BackColor = Color.Beige;
            textBoxRutaDDL.Enabled = false;
            textBoxRutaDDL.Location = new Point(12, 289);
            textBoxRutaDDL.Name = "textBoxRutaDDL";
            textBoxRutaDDL.Size = new Size(433, 23);
            textBoxRutaDDL.TabIndex = 8;
            // 
            // labelRutaDDL
            // 
            labelRutaDDL.AutoSize = true;
            labelRutaDDL.Location = new Point(12, 271);
            labelRutaDDL.Name = "labelRutaDDL";
            labelRutaDDL.Size = new Size(56, 15);
            labelRutaDDL.TabIndex = 0;
            labelRutaDDL.Text = "Ruta DDL";
            // 
            // panelBotones
            // 
            panelBotones.Controls.Add(buttonGenerar);
            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Location = new Point(0, 461);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(494, 40);
            panelBotones.TabIndex = 6;
            // 
            // buttonGenerar
            // 
            buttonGenerar.Location = new Point(365, 5);
            buttonGenerar.Name = "buttonGenerar";
            buttonGenerar.Size = new Size(112, 23);
            buttonGenerar.TabIndex = 0;
            buttonGenerar.Text = "Generar Salida ✔ ";
            buttonGenerar.UseVisualStyleBackColor = true;
            buttonGenerar.Click += buttonGenerar_Click;
            // 
            // menuPrincipal
            // 
            menuPrincipal.Items.AddRange(new ToolStripItem[] { ayudaToolStripMenuItem, generarFicherosDefinicionUIToolStripMenuItem });
            menuPrincipal.Location = new Point(0, 0);
            menuPrincipal.Name = "menuPrincipal";
            menuPrincipal.Size = new Size(494, 24);
            menuPrincipal.TabIndex = 6;
            menuPrincipal.Text = "menuStrip1";
            // 
            // ayudaToolStripMenuItem
            // 
            ayudaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportarConfiguraciónToolStripMenuItem, importarConfiguraciónToolStripMenuItem });
            ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            ayudaToolStripMenuItem.Size = new Size(95, 20);
            ayudaToolStripMenuItem.Text = "Configuración";
            // 
            // exportarConfiguraciónToolStripMenuItem
            // 
            exportarConfiguraciónToolStripMenuItem.Name = "exportarConfiguraciónToolStripMenuItem";
            exportarConfiguraciónToolStripMenuItem.Size = new Size(199, 22);
            exportarConfiguraciónToolStripMenuItem.Text = "Exportar Configuración";
            exportarConfiguraciónToolStripMenuItem.Click += exportarConfiguraciónToolStripMenuItem_Click;
            // 
            // importarConfiguraciónToolStripMenuItem
            // 
            importarConfiguraciónToolStripMenuItem.Name = "importarConfiguraciónToolStripMenuItem";
            importarConfiguraciónToolStripMenuItem.Size = new Size(199, 22);
            importarConfiguraciónToolStripMenuItem.Text = "Importar Configuración";
            importarConfiguraciónToolStripMenuItem.Click += importarConfiguraciónToolStripMenuItem_Click;
            // 
            // generarFicherosDefinicionUIToolStripMenuItem
            // 
            generarFicherosDefinicionUIToolStripMenuItem.Name = "generarFicherosDefinicionUIToolStripMenuItem";
            generarFicherosDefinicionUIToolStripMenuItem.Size = new Size(178, 20);
            generarFicherosDefinicionUIToolStripMenuItem.Text = "Generar Ficheros Definicion UI";
            generarFicherosDefinicionUIToolStripMenuItem.Click += generarFicherosDefinicionUIToolStripMenuItem_Click;
            // 
            // FormularioPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(494, 501);
            Controls.Add(menuPrincipal);
            Controls.Add(panelBotones);
            Controls.Add(panelPrincipal);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MainMenuStrip = menuPrincipal;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormularioPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Generador CRUD";
            panelPrincipal.ResumeLayout(false);
            panelPrincipal.PerformLayout();
            panelBotones.ResumeLayout(false);
            menuPrincipal.ResumeLayout(false);
            menuPrincipal.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelPrincipal;
        private Button buttonRutaDDL;
        private TextBox textBoxRutaDDL;
        private Label labelRutaDDL;
        private Button buttonRutaUI;
        private TextBox textBoxRutaUI;
        private Label labelRutaUI;
        private Panel panelBotones;
        private Button buttonGenerar;
        private CheckBox checkBoxDefinicionesUI;
        private MenuStrip menuPrincipal;
        private ToolStripMenuItem ayudaToolStripMenuItem;
        private ToolStripMenuItem exportarConfiguraciónToolStripMenuItem;
        private ToolStripMenuItem importarConfiguraciónToolStripMenuItem;
        private Button buttonRutaDestinoUI;
        private TextBox textBoxRutaDestinoUI;
        private Label labelDestinoInterfaz;
        private CheckBox checkBoxSobreescribir;
        private Button buttonDestinoControladorDAO;
        private TextBox textBoxDestinoControladorDAO;
        private Label labelDestinoControladorDAO;
        private Button buttonDestinoControlador;
        private TextBox textBoxDestinoControlador;
        private Label labelDestinoControlador;
        private Button buttonDestinoModelo;
        private TextBox textBoxDestinoModelo;
        private Label labelDestinoModelo;
        private CheckBox checkBoxUnificarDestinos;
        private ToolStripMenuItem generarFicherosDefinicionUIToolStripMenuItem;
        private RadioButton radioButtonSQLite;
        private RadioButton radioButtonFirebird;
        private Label labelTipoBD;
    }
}
