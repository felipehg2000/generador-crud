using generador_crud.Modelos.Objetos;
using generador_crud.Plantillas.C_Sharp;

namespace generador_crud.Controladores
{
    /// <summary>
    /// Clase encargada de generar los archivos de salida.
    /// </summary>
    internal class ControladorSalidaSQLite
    {
        /// <summary>
        /// Método principal, orquesta la generación de cada uno de los ficheros.
        /// </summary>
        /// <param name="param_configuracion">Configuración que ha hecho el usuario en la interfaz. Rutas de salida.</param>
        /// <param name="param_tabla">Objeto de la tabla de base de datos de la que se va a generar la salida.</param>
        /// <exception cref="ArgumentNullException">Excepción lanzada si algún parámetro es nulo.</exception>
        static public void Generar(Configuracion param_configuracion,
            Tabla param_tabla,
            DefinicionUI param_definicion_ui)
        {
            // Comprobar parámetros.
            if (param_configuracion == null || param_tabla == null)
            {
                throw new ArgumentNullException("No se encuentra la información suficiente.");
            }

            // Generar modelo
            string texto = GenerarModelo(param_tabla);
            string ruta_modelo = param_configuracion.RutaModelos + "\\" + param_tabla.Nombre + ".cs";
            string custom = string.Empty;
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = RescatarCustom.Obtener(ruta_modelo);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_modelo, texto);

            // Generar modelo filtros
            texto = GenerarModeloFiltros(param_tabla, param_definicion_ui);
            ruta_modelo = param_configuracion.RutaModelos + "\\" + "Filtros" + param_tabla.Nombre + ".cs";
            custom = string.Empty;
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = RescatarCustom.Obtener(ruta_modelo);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_modelo, texto);

            // Generar DAO
            texto = GenerarDAO(param_tabla, param_definicion_ui);
            string ruta_controlador_bd = param_configuracion.RutaControladoresDAO + "\\ControladorDAO" + param_tabla.Nombre + ".cs";
            custom = string.Empty;
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = RescatarCustom.Obtener(ruta_controlador_bd);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_controlador_bd, texto);

            // Generar controlador
            texto = GenerarControlador(param_tabla);
            string ruta_controlador = param_configuracion.RutaControladores + "\\Controlador" + param_tabla.Nombre + ".cs";
            custom = string.Empty;
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = RescatarCustom.Obtener(ruta_controlador);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_controlador, texto);

            // Generar vista cs
            texto = GenerarVistaCS(param_tabla, param_definicion_ui);
            string ruta_vista = param_configuracion.RutaUI + "\\Formulario" + param_tabla.Nombre + ".cs";
            custom = string.Empty;
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = RescatarCustom.Obtener(ruta_vista);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_vista, texto);

            // Generar diseño
            texto = GenerarVistaDesigner(param_tabla, param_definicion_ui);
            string ruta_disenio = param_configuracion.RutaUI + "\\Formulario" + param_tabla.Nombre + ".Designer.cs";
            System.IO.File.WriteAllText(ruta_disenio, texto);

            // Generar recursos
            texto = GenerarVistaRecursos();
            string ruta_recursos = param_configuracion.RutaUI + "\\Formulario" + param_tabla.Nombre + ".resx";
            System.IO.File.WriteAllText(ruta_recursos, texto);
        }

        /// <summary>
        /// Genera la salida del objeto que representa la tabla que se está procesando.
        /// </summary>
        /// <param name="param_tabla">Tabla que se está procesando.</param>
        /// <returns>Texto a extraer en el fichero del objeto.</returns>
        static private string GenerarModelo(Tabla param_tabla)
        {
            string texto = Objeto.Plantilla;

            string definicion_atributos = string.Empty;
            string inicializacion_vacia = string.Empty;
            string inicializacion_copia = string.Empty;
            string limpiar_atributos = string.Empty;
            foreach (ColumnaSQLite columna in param_tabla.Columnas)
            {
                definicion_atributos += "\t\t" + columna.GenerarDefAtributoNet() + "\n";
                inicializacion_vacia += "\t\t" + columna.GenerarInicializacionVacia() + "\n";
                inicializacion_copia += "\t\t" + columna.GenerarInicializacionCopia() + "\n";
                limpiar_atributos += "\t\t" + columna.GenerarLimpiezaAtributos() + "\n";
            }

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());
            texto = texto.Replace("{DefinicionAtributos}", definicion_atributos);
            texto = texto.Replace("{InicializacionVacia}", inicializacion_vacia);
            texto = texto.Replace("{InicializacionCopia}", inicializacion_copia);
            texto = texto.Replace("{LimpiarAtributos}", limpiar_atributos);

            return texto;
        }

        /// <summary>
        /// Genera la salida del objeto de filtros que representa la tabla que se está procesando.
        /// </summary>
        /// <param name="param_tabla">Tabla que se está procesando.</param>
        /// <param name="param_definicion">Configuración de la vista creada por el usuario.</param>
        /// <returns>Texto a extraer en el fichero del objeto.</returns>
        static private string GenerarModeloFiltros(Tabla param_tabla, DefinicionUI param_definicion_ui)
        {
            string texto = Objeto.Plantilla;

            string definicion_atributos = string.Empty;
            string inicializacion_vacia = string.Empty;
            string inicializacion_copia = string.Empty;
            string limpiar_atributos = string.Empty;
            foreach (ColumnaSQLite columna in param_tabla.Columnas)
            {
                bool existe = param_definicion_ui.CamposFiltro.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (!existe)
                    continue;

                ColumnaSQLite modelo_tmp_desde = new ColumnaSQLite(false, columna.Nombre + "_desde", columna.TipoDato);
                ColumnaSQLite modelo_tmp_hasta = new ColumnaSQLite(false, columna.Nombre + "_hasta", columna.TipoDato);

                definicion_atributos += "\t\t" + modelo_tmp_desde.GenerarDefAtributoNet() + "\n";
                definicion_atributos += "\t\t" + modelo_tmp_hasta.GenerarDefAtributoNet() + "\n";
                inicializacion_vacia += "\t\t" + modelo_tmp_desde.GenerarInicializacionVacia() + "\n";
                inicializacion_vacia += "\t\t" + modelo_tmp_hasta.GenerarInicializacionVacia() + "\n";
                inicializacion_copia += "\t\t" + modelo_tmp_desde.GenerarInicializacionCopia() + "\n";
                inicializacion_copia += "\t\t" + modelo_tmp_hasta.GenerarInicializacionCopia() + "\n";
                limpiar_atributos += "\t\t" + modelo_tmp_desde.GenerarLimpiezaAtributos() + "\n";
                limpiar_atributos += "\t\t" + modelo_tmp_hasta.GenerarLimpiezaAtributos() + "\n";
            }

            texto = texto.Replace("{NombreTabla}", "Filtros" + param_tabla.Nombre);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());
            texto = texto.Replace("{DefinicionAtributos}", definicion_atributos);
            texto = texto.Replace("{InicializacionVacia}", inicializacion_vacia);
            texto = texto.Replace("{InicializacionCopia}", inicializacion_copia);
            texto = texto.Replace("{LimpiarAtributos}", limpiar_atributos);

            return texto;
        }

        /// <summary>
        /// Genera la salida del fichero DAO, conexiones con la base de datos.
        /// </summary>
        /// <param name="param_tabla">Objeto de tipo tabla con la información de la ddl leida.</param>
        /// <param name="param_filtros">Lista de posibles filtros.</param>
        /// <returns>Texto del fichero DAO.</returns>
        /// <exception cref="ArgumentException">Error con el listado de filtros.</exception>
        static private string GenerarDAO(Tabla param_tabla, DefinicionUI param_definicion_ui)
        {
            string texto = ControladorDAO.Plantilla;

            string campos_insert = string.Empty;
            string parametros_insert = string.Empty;
            string campo_set = string.Empty;
            string filtro_pk = string.Empty;
            string carga_campos = string.Empty;
            string parametrizar_campos = string.Empty;
            string parametrizar_pk = string.Empty;
            string generar_filtros = string.Empty;
            string parametrizar_filtros = string.Empty;
            foreach (ColumnaSQLite columna in param_tabla.Columnas)
            {
                campos_insert += "\t\t" + columna.Nombre;
                parametros_insert += "\t\t" + columna.GetNombreParametro();
                                
                if (columna != param_tabla.Columnas.Last())
                {
                    campos_insert += ", \n";
                    parametros_insert += ", \n";
                }

                if (!columna.EsClavePrimaria)
                {
                    campo_set += "\t" + columna.Nombre + " = " + columna.GetNombreParametro();
                    if (columna != param_tabla.Columnas.Last())
                        campo_set += ", \n";
                }
                

                if (columna.EsClavePrimaria && filtro_pk != string.Empty)
                {
                    filtro_pk += " AND \n";
                }
                filtro_pk += "\t" + columna.GenerarFiltroPK();

                carga_campos += columna.GenerarCargaCampos();

                // Parametrizar
                if (columna.EsClavePrimaria)
                {
                    parametrizar_pk += "\t\t\tparam_consulta_sql.Parameters.AddWithValue(\"" + columna.GetNombreParametro() + "\", param_modelo." + columna.ObtenerNombreCapitalizado() + ");\n";
                }
                else
                {
                    parametrizar_campos += "\t\t\tparam_consulta_sql.Parameters.AddWithValue(\"" + columna.GetNombreParametro() + "\", param_modelo." + columna.ObtenerNombreCapitalizado() + ");\n";
                }

                bool existe = param_definicion_ui.CamposFiltro.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (existe)
                {
                    generar_filtros += columna.GenerarFiltros();
                    parametrizar_filtros += "\t\t\tparam_consulta_sql.Parameters.AddWithValue(\"@P_" + columna.ObtenerNombreCapitalizado() + "_desde\", param_filtros." + columna.ObtenerNombreCapitalizado() + "_desde);\n";
                    parametrizar_filtros += "\t\t\tparam_consulta_sql.Parameters.AddWithValue(\"@P_" + columna.ObtenerNombreCapitalizado() + "_hasta\", param_filtros." + columna.ObtenerNombreCapitalizado() + "_hasta);\n";
                }

            }

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());
            texto = texto.Replace("{CamposInsert}", campos_insert);
            texto = texto.Replace("{ParametrosInsert}", parametros_insert);
            texto = texto.Replace("{CamposSet}", campo_set);
            texto = texto.Replace("{FiltroPK}", filtro_pk);
            texto = texto.Replace("{CargaCampos}", carga_campos);
            texto = texto.Replace("{ParametrizacionCampos}", parametrizar_campos);
            texto = texto.Replace("{ParametrizacionPK}", parametrizar_pk);
            texto = texto.Replace("{ParametrizacionFiltros}", parametrizar_filtros);
            texto = texto.Replace("{GeneracionFiltros}", generar_filtros);

            return texto;
        }

        /// <summary>
        /// Genera el texto del controlador para la tabla.
        /// </summary>
        /// <param name="param_tabla">Tabla que se está procesando.</param>
        /// <returns>Texto a extraer en el fichero controlador.</returns>
        static private string GenerarControlador(Tabla param_tabla)
        {
            string texto = Controlador.Plantilla;

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());

            return texto;
        }

        /// <summary>
        /// Genera el código de control de la vista.
        /// </summary>
        /// <param name="param_tabla">Tabla que se está procesando.</param>
        /// <param name="param_definicion">Configuración de la vista creada por el usuario.</param>
        /// <returns>Texto de la vista.</returns>
        static private string GenerarVistaCS(Tabla param_tabla, DefinicionUI param_definicion)
        {
            string texto = Vista.PlantillaCS;

            string definicion_columna = string.Empty;
            string ancho_columnas = string.Empty;
            string limpiar_campos = string.Empty;
            string carga_modelo = string.Empty;
            string mostrar_modelo = string.Empty;
            string agregar_filas = string.Empty;
            string color_campos = string.Empty;
            string visibilidad_campos = string.Empty;
            string desactivar_campos = "";
            string cargar_filtros = string.Empty;
            foreach (ColumnaSQLite columna in param_tabla.Columnas)
            {
                bool existe = param_definicion.MostrarEnTabla.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (existe)
                {
                    definicion_columna += "\n\t\t\t" + columna.ObtenerNombreColumna() + ",";
                    ancho_columnas += "\n\t\t\tdataGridViewTabla.Columns[(int)Columnas." + columna.ObtenerNombreColumna() + "].Width = (int)(ancho_data_grid * 0.20);";
                    if (agregar_filas.Length > 0)
                        agregar_filas += ", \n\t\t\t\t";
                    agregar_filas += "param_modelo." + columna.ObtenerNombreCapitalizado();
                }

                existe = param_definicion.CamposEdicion.Any(item => item.Nombre == columna.Nombre && !item.Seleccionado);
                if (existe)
                {
                    desactivar_campos += "\n\t\t\t\t" + columna.ObtenerNombreComponente() + ".BackColor = Color.Beige;";
                    desactivar_campos += "\n\t\t\t\t" + columna.ObtenerNombreComponente() + ".Enabled = false;";
                }

                existe = param_definicion.CamposFiltro.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (existe)
                {
                    ColumnaSQLite modelo_tmp_desde = new ColumnaSQLite(false, columna.Nombre + "_desde", columna.TipoDato);
                    ColumnaSQLite modelo_tmp_hasta = new ColumnaSQLite(false, columna.Nombre + "_hasta", columna.TipoDato);

                    cargar_filtros += modelo_tmp_desde.GenerarCargaModelos(true);
                    cargar_filtros += modelo_tmp_hasta.GenerarCargaModelos(true);
                }

                limpiar_campos += "\n\t\t\t" + columna.ObtenerNombreComponente() + ".Clear();";
                carga_modelo += columna.GenerarCargaModelos();
                mostrar_modelo += columna.GenerarMuestraModelo();
                color_campos += "\n\t\t\t" + columna.ObtenerNombreComponente() + ".BackColor = param_color;";
                visibilidad_campos += "\n\t\t\t" + columna.ObtenerNombreComponente() + ".Enabled = param_activar;";
            }

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());
            texto = texto.Replace("{Columnas}", definicion_columna);
            texto = texto.Replace("{AnchoColumnas}", ancho_columnas);
            texto = texto.Replace("{LimpiarCampos}", limpiar_campos);
            texto = texto.Replace("{CargarModelo}", carga_modelo);
            texto = texto.Replace("{MostrarModelo}", mostrar_modelo);
            texto = texto.Replace("{AgregarFilas}", agregar_filas);
            texto = texto.Replace("{ColorCamops}", color_campos);
            texto = texto.Replace("{VisibilidadCampo}", visibilidad_campos);
            texto = texto.Replace("{DesactivarCampos}", desactivar_campos);
            texto = texto.Replace("{CargarFiltros}", cargar_filtros);

            return texto;
        }
    
        /// <summary>
        /// Genera el código del diseño de la vista.
        /// </summary>
        /// <param name="param_tabla">Tabla que se está procesando.</param>
        /// <param name="param_definicion">Configuración de la vista creada por el usuario.</param>
        /// <returns>Texto del diseño de la vista.</returns>
        static private string GenerarVistaDesigner(Tabla param_tabla, DefinicionUI param_definicion)
        {
            string texto = Vista.PlantillaDesigner;

            int indice_tab = 0;
            int posicion_x = 60;
            int posicion_y = 60;
            int indice_tab_filtros = 0;
            int posicion_x_filtros = 60;
            int posicion_y_filtros = 60;

            string definicion_componentes = string.Empty;
            string agregar_panel = string.Empty;
            string atributos_componentes = string.Empty;
            string definicion_final_componentes = string.Empty;

            string definicion_columnas = string.Empty;
            string rango_columnas = string.Empty;
            string atributos_columnas = string.Empty;
            string definicion_final_columnas = string.Empty;

            string definicion_componentes_filtros = string.Empty;
            string atributos_componentes_filtros = string.Empty;
            string agregar_a_panel_filtros = string.Empty;

            foreach (ColumnaSQLite columna in param_tabla.Columnas)
            {
                definicion_componentes += "\n\t\t\tthis." + columna.ObtenerNombreLabel() + " = new System.Windows.Forms.Label();";
                definicion_componentes += "\n\t\t\tthis." + columna.ObtenerNombreComponente() + " = new System.Windows.Forms.TextBox();";


                atributos_componentes += GenerarLabel(columna, posicion_x, posicion_y, indice_tab);
                indice_tab++;
                posicion_y += 10;

                atributos_componentes += GenerarComponente(columna, posicion_x, posicion_y, indice_tab);
                indice_tab++;
                posicion_y += 10;

                agregar_panel += "\n\t\t\tthis.panelCentralCampos.Controls.Add(this." + columna.ObtenerNombreLabel() + ");";
                agregar_panel += "\n\t\t\tthis.panelCentralCampos.Controls.Add(this." + columna.ObtenerNombreComponente() + ");";

                definicion_final_componentes += "\n\t\t\tprivate System.Windows.Forms.Label " + columna.ObtenerNombreLabel() + ";";
                definicion_final_componentes += "\n\t\t\tprivate System.Windows.Forms.TextBox " + columna.ObtenerNombreComponente() + ";";

                bool existe = param_definicion.MostrarEnTabla.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (existe)
                {
                    definicion_columnas += "\n\t\t\tthis." + columna.ObtenerNombreColumna() + " = new System.Windows.Forms.DataGridViewTextBoxColumn();";
                    rango_columnas += "\n\t\t\tthis." + columna.ObtenerNombreColumna() + ",";
                    
                    atributos_columnas += "\n\t\t\t//";
                    atributos_columnas += "\n\t\t\t// Columna " + columna.ObtenerNombreCapitalizado(); ;
                    atributos_columnas += "\n\t\t\t//";
                    atributos_columnas += "\n\t\t\tthis." + columna.ObtenerNombreColumna() + ".Frozen = true;";
                    atributos_columnas += "\n\t\t\tthis." + columna.ObtenerNombreColumna() + ".HeaderText = \"" + columna.ObtenerNombreCapitalizado() + "\";";
                    atributos_columnas += "\n\t\t\tthis." + columna.ObtenerNombreColumna() + ".Name = \"" + columna.ObtenerNombreColumna() + "\";";
                    atributos_columnas += "\n\t\t\tthis." + columna.ObtenerNombreColumna() + ".ReadOnly = true;";

                    definicion_final_columnas += "\n\t\t\tprivate System.Windows.Forms.DataGridViewTextBoxColumn " + columna.ObtenerNombreColumna() + ";";
                }

                existe = param_definicion.CamposFiltro.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (existe)
                {
                    ColumnaSQLite modelo_tmp_desde = new ColumnaSQLite(false, columna.Nombre + "_desde", columna.TipoDato);
                    ColumnaSQLite modelo_tmp_hasta = new ColumnaSQLite(false, columna.Nombre + "_hasta", columna.TipoDato);

                    definicion_componentes_filtros += "\n\t\t\tthis." + modelo_tmp_desde.ObtenerNombreLabel() + " = new System.Windows.Forms.Label();";
                    definicion_componentes_filtros += "\n\t\t\tthis." + modelo_tmp_desde.ObtenerNombreComponente() + " = new System.Windows.Forms.TextBox();";
                    definicion_componentes_filtros += "\n\t\t\tthis." + modelo_tmp_hasta.ObtenerNombreLabel() + " = new System.Windows.Forms.Label();";
                    definicion_componentes_filtros += "\n\t\t\tthis." + modelo_tmp_hasta.ObtenerNombreComponente() + " = new System.Windows.Forms.TextBox();";

                    atributos_componentes_filtros += GenerarLabel(modelo_tmp_desde, posicion_x_filtros, posicion_y_filtros, indice_tab_filtros);
                    indice_tab_filtros++;
                    posicion_y_filtros += 5;

                    atributos_componentes_filtros += GenerarComponente(modelo_tmp_desde, posicion_x, posicion_y, indice_tab);
                    indice_tab_filtros++;
                    posicion_y_filtros += 5;

                    atributos_componentes_filtros += GenerarLabel(modelo_tmp_hasta, posicion_x_filtros, posicion_y_filtros, indice_tab_filtros);
                    indice_tab_filtros++;
                    posicion_y_filtros += 5;

                    atributos_componentes_filtros += GenerarComponente(modelo_tmp_hasta, posicion_x, posicion_y, indice_tab);
                    indice_tab_filtros++;
                    posicion_y_filtros += 5;

                    agregar_a_panel_filtros += "\n\t\t\tthis.panelCentralFiltros.Controls.Add(this." + modelo_tmp_desde.ObtenerNombreLabel() + ");";
                    agregar_a_panel_filtros += "\n\t\t\tthis.panelCentralFiltros.Controls.Add(this." + modelo_tmp_desde.ObtenerNombreComponente() + ");";
                    agregar_a_panel_filtros += "\n\t\t\tthis.panelCentralFiltros.Controls.Add(this." + modelo_tmp_hasta.ObtenerNombreLabel() + ");";
                    agregar_a_panel_filtros += "\n\t\t\tthis.panelCentralFiltros.Controls.Add(this." + modelo_tmp_hasta.ObtenerNombreComponente() + ");";

                    definicion_final_componentes += "\n\t\t\tprivate System.Windows.Forms.Label " + modelo_tmp_desde.ObtenerNombreLabel() + ";";
                    definicion_final_componentes += "\n\t\t\tprivate System.Windows.Forms.TextBox " + modelo_tmp_desde.ObtenerNombreComponente() + ";";
                    definicion_final_componentes += "\n\t\t\tprivate System.Windows.Forms.Label " + modelo_tmp_hasta.ObtenerNombreLabel() + ";";
                    definicion_final_componentes += "\n\t\t\tprivate System.Windows.Forms.TextBox " + modelo_tmp_hasta.ObtenerNombreComponente() + ";";
                }
            }

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);

            texto = texto.Replace("{DefinicionComponentes}", definicion_componentes);
            texto = texto.Replace("{AgregacionAPanel}", agregar_panel);
            texto = texto.Replace("{AtributosComponentes}", atributos_componentes);
            texto = texto.Replace("{DefinicionFinalComponentes}", definicion_final_componentes);

            rango_columnas = rango_columnas.TrimEnd(',');
            texto = texto.Replace("{DefinicionColumnas}", definicion_columnas);
            texto = texto.Replace("{RangoColumnas}", rango_columnas);
            texto = texto.Replace("{AtributosColumna}", atributos_columnas);
            texto = texto.Replace("{DefinicionFinalColumnas}", definicion_final_columnas);

            texto = texto.Replace("{DefinicionComponentesFiltros}", definicion_componentes_filtros);
            texto = texto.Replace("{AtributosComponentesFiltros}", atributos_componentes_filtros);
            texto = texto.Replace("{AgregarAPanelFiltros}", agregar_a_panel_filtros);

            return texto;
        }
    
        /// <summary>
        /// Genera el código de los recursos de la vista.
        /// </summary>
        /// <returns>Texto de los recursos de la vista.</returns>
        static private string GenerarVistaRecursos()
        {
            return Vista.PlantillaResx;
        }
    
        /// <summary>
        /// Genera el código necesario para crear un componente visual de tipo label y su configuración.
        /// </summary>
        /// <param name="param_columna">Columna para la que se configura el label.</param>
        /// <param name="param_posicion_x">Posición del label en el eje x.</param>
        /// <param name="param_posicion_y">Posición del label en el eje y.</param>
        /// <param name="param_indice_tab">Orden de tabulación del label.</param>
        /// <returns></returns>
        static private string GenerarLabel(ColumnaSQLite param_columna,
            int param_posicion_x,
            int param_posicion_y,
            int param_indice_tab)
        {
            string ret_texto =
@"          
            // 
            // {NombreComponente}
            // 
            this.{NombreComponente}.AutoSize = true;
            this.{NombreComponente}.Location = new System.Drawing.Point({PosicionX}, {PosicionY});
            this.{NombreComponente}.Name = ""{NombreComponente}"";
            this.{NombreComponente}.Size = new System.Drawing.Size(74, 13);
            this.{NombreComponente}.TabIndex = {TabIndex};
            this.{NombreComponente}.Text = ""{NombreCapitalizado}"";";

            ret_texto = ret_texto.Replace("{NombreComponente}", param_columna.ObtenerNombreLabel());
            ret_texto = ret_texto.Replace("{PosicionX}", param_posicion_x.ToString());
            ret_texto = ret_texto.Replace("{PosicionY}", param_posicion_y.ToString());
            ret_texto = ret_texto.Replace("{TabIndex}", param_indice_tab.ToString());
            ret_texto = ret_texto.Replace("{NombreCapitalizado}", param_columna.ObtenerNombreCapitalizado());

            return ret_texto;
        }

        /// <summary>
        /// Genera el text box para una columna pasada por parámetro.
        /// </summary>
        /// <param name="param_columna">Columna cuyo textbox se va a generar.</param>
        /// <param name="param_posicion_x">Posición del componente en el eje x.</param>
        /// <param name="param_posicion_y">Posición del componente en el eje y.</param>
        /// <param name="param_tab_index">Orden de tabulación del componente</param>
        /// <returns></returns>
        static private string GenerarComponente(ColumnaSQLite param_columna,
            int param_posicion_x,
            int param_posicion_y,
            int param_tab_index)
        {
            string ret_texto =
@"          
            // 
            // {NombreComponente}
            // 
            this.{NombreComponente}.Location = new System.Drawing.Point({PosicionX}, {PosicionY});
            this.{NombreComponente}.Name = ""{NombreComponente}"";
            this.{NombreComponente}.Size = new System.Drawing.Size(100, 20);
            this.{NombreComponente}.TabIndex = {TabIndex};";

            ret_texto = ret_texto.Replace("{NombreComponente}", param_columna.ObtenerNombreComponente());
            ret_texto = ret_texto.Replace("{PosicionX}", param_posicion_x.ToString());
            ret_texto = ret_texto.Replace("{PosicionY}", param_posicion_y.ToString());
            ret_texto = ret_texto.Replace("{TabIndex}", param_tab_index.ToString());

            return ret_texto;
        }
    }
}

