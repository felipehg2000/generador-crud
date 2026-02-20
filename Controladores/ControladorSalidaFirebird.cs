using generador_crud.Modelos.Objetos;
using generador_crud.Plantillas.CMasMas;

namespace generador_crud.Controladores
{
    /// <summary>
    /// Clase encargada de generar los archivos de salida para C++ Builder.
    /// </summary>
    internal class ControladorSalidaFirebird : ControladorSalida
    {
        /// <summary>
        /// Controla la generación de texto y ficheros de modelos, controlador DAO, controlador y formulario a partir de la tabla y definición de UI proporcionada.
        /// </summary>
        /// <param name="param_configuracion">Configuración del usuario para la generación de ficheros.</param>
        /// <param name="param_tabla">Tabla de la base de datos.</param>
        /// <param name="param_definicion_ui">Definición de la interfaz de usuario.</param>
        static public void Generar(Configuracion param_configuracion,
            Tabla param_tabla,
            DefinicionUI param_definicion_ui)
        {
            string custom = string.Empty;

            // Generar ControladorConexion si no existe
            if (!File.Exists(param_configuracion.RutaControladores + "\\ControladorConexion.h") || !File.Exists(param_configuracion.RutaControladores + "\\ControladorConexion.cpp"))
            {
                System.IO.File.WriteAllText(param_configuracion.RutaControladores + "\\ControladorConexion.h", ControladorConexion.PlantillaH);
                System.IO.File.WriteAllText(param_configuracion.RutaControladores + "\\ControladorConexion.cpp", ControladorConexion.PlantillaCPP);
            }

            // Generar fichero de modelo .h
            string texto = GenerarModeloEncabezados(param_tabla);
            string ruta_modelo = param_configuracion.RutaModelos + "\\" + param_tabla.Nombre + ".h";
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = Obtener(ruta_modelo);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_modelo, texto);

            // Generar fichero de modelo .cpp
            texto = GenerarModeloImplementacion(param_tabla);
            ruta_modelo = param_configuracion.RutaModelos + "\\" + param_tabla.Nombre + ".cpp";
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = Obtener(ruta_modelo);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_modelo, texto);

            // Generar fichero de modelo de filtros .h
            texto = GenerarModeloFiltrosEncabezados(param_tabla, param_definicion_ui);
            ruta_modelo = param_configuracion.RutaModelos + "\\Filtros" + param_tabla.Nombre + ".h";
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = Obtener(ruta_modelo);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_modelo, texto);

            // Generar fichero de modelo de filtros .cpp
            texto = GenerarModeloFiltrosImplementacion(param_tabla, param_definicion_ui);
            ruta_modelo = param_configuracion.RutaModelos + "\\Filtros" + param_tabla.Nombre + ".cpp";
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = Obtener(ruta_modelo);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_modelo, texto);

            // Generar fichero de controlador DAO .h
            texto = GenerarControladorDAOEncabezados(param_tabla);
            string ruta_controlador_dao = param_configuracion.RutaControladoresDAO + "\\ControladorDAO" + param_tabla.Nombre + ".h";
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = Obtener(ruta_controlador_dao);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_controlador_dao, texto);

            // Generar fichero de controlador DAO .cpp
            texto = GenerarControladorDAOImplementacion(param_tabla, param_definicion_ui);
            ruta_controlador_dao = param_configuracion.RutaControladoresDAO + "\\ControladorDAO" + param_tabla.Nombre + ".cpp";
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = Obtener(ruta_controlador_dao);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_controlador_dao, texto);

            // Generar fichero de controlador .h
            texto = GenerarControladorEncabezados(param_tabla);
            string ruta_controlador = param_configuracion.RutaControladores + "\\Controlador" + param_tabla.Nombre + ".h";
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = Obtener(ruta_controlador);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_controlador, texto);

            // Generar fichero de controlador .cpp
            texto = GenerarControladorImplementacion(param_tabla);
            ruta_controlador = param_configuracion.RutaControladores + "\\Controlador" + param_tabla.Nombre + ".cpp";
            if (!param_configuracion.SobreescribirArchivosExistentes)
                custom = Obtener(ruta_controlador);
            texto = texto.Replace("{CodigoCustom}", custom);
            System.IO.File.WriteAllText(ruta_controlador, texto);

            // Generar fichero de Formulario .h
            texto = GenerarUIEncabezados(param_tabla, param_definicion_ui);
            string ruta_vista = param_configuracion.RutaUI + "\\Formulario" + param_tabla.Nombre + ".h";
            System.IO.File.WriteAllText(ruta_vista, texto);

            // Generar fichero de Formulario .cpp
            texto = GenerarUIImplementacion(param_tabla, param_definicion_ui);
            ruta_vista = param_configuracion.RutaUI + "\\Formulario" + param_tabla.Nombre + ".cpp";
            System.IO.File.WriteAllText(ruta_vista, texto);

            // Generar fichero de Formulario .dfm
            texto = GenerarUIDesigner(param_tabla, param_definicion_ui);
            ruta_vista = param_configuracion.RutaUI + "\\Formulario" + param_tabla.Nombre + ".dfm";
            System.IO.File.WriteAllText(ruta_vista, texto);
        }

        /// <summary>
        /// Genera el texto del fichero modelo .h.
        /// </summary>
        /// <param name="param_tabla">Tabla de la base de datos.</param>
        /// <returns>Texto con el código del modelo .h.</returns>
        static private string GenerarModeloEncabezados(Tabla param_tabla)
        {
            // Obtener la plantilla base
            string texto = Objeto.PlantillaH;

            // Generar las propiedades 
            string propiedades = string.Empty;
            string atributos = string.Empty;
            List<ColumnaFirebird> columnas_ordenadas = ObtenerColumnasOrdenadas(param_tabla.Columnas);
            foreach (ColumnaFirebird columna in columnas_ordenadas)
            {
                propiedades += "\t" + columna.GenerarPropiedadEmbarcadero() + "\n";
                atributos += "\t" + columna.GenerarAtributoEmbarcadero() + "\n";
            }

            // Reemplazar los marcadores de posición
            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{Atributos}", atributos);
            texto = texto.Replace("{Propiedades}", propiedades);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());

            // Devolver el texto generado
            return texto;
        }

        /// <summary>
        /// Genera el texto del fichero modelo .cpp.
        /// </summary>
        /// <param name="param_tabla">Tabla de la base de datos.</param>
        /// <returns>Texto con el código del modelo .cpp.</returns>
        static private string GenerarModeloImplementacion(Tabla param_tabla)
        {
            string texto = Objeto.PlantillaCpp;

            string inicializacion_copia = string.Empty;
            string inicializacion_vacio = string.Empty;
            string limpieza_atributos = string.Empty;
            string inicializacion_vacio_blob = string.Empty;
            string inicializacion_copia_blob = string.Empty;
            List<ColumnaFirebird> columnas_ordenadas = ObtenerColumnasOrdenadas(param_tabla.Columnas);
            foreach (ColumnaFirebird columna in columnas_ordenadas)
            {
                if (columna.TipoDato == ColumnaFirebird.TiposDatos.BLOB)
                {
                    inicializacion_vacio_blob += "\n\t" + columna.GenerarInicializacionVacia();
                    inicializacion_copia_blob += "\n\t" + columna.GenerarInicializacionDeCopia();
                    continue;
                }
                inicializacion_copia += "\n\t" + columna.GenerarInicializacionDeCopia() + ",";
                inicializacion_vacio += "\n\t" + columna.GenerarInicializacionVacia() + ",";
                limpieza_atributos += "\n" + columna.GenerarLimpiezaAtributos();
            }

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{IncializacionCopia}", inicializacion_copia.TrimEnd(','));
            texto = texto.Replace("{InicializacionCopiaBlob}", inicializacion_copia_blob);
            texto = texto.Replace("{InicializacionVacioBlob}", inicializacion_vacio_blob);
            texto = texto.Replace("{InicializacionVacio}", inicializacion_vacio.TrimEnd(','));
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());
            texto = texto.Replace("{LimpiarAtributos}", limpieza_atributos);

            return texto;
        }

        /// <summary>
        /// Genera el texto del fichero modelo de filtros .h.
        /// </summary>
        /// <param name="param_tabla">Tabla de la base de datos.</param>
        /// <param name="param_definicion_ui">Definición de la interfaz de usuario.</param>
        /// <returns>Texto con el código del modelo de filtros .h.</returns>
        static private string GenerarModeloFiltrosEncabezados(Tabla param_tabla, DefinicionUI param_definicion_ui)
        {
            string texto = Objeto.PlantillaH;

            string propiedades = string.Empty;
            string atributos = string.Empty;
            List<ColumnaFirebird> columnas_ordenadas = ObtenerColumnasOrdenadas(param_tabla.Columnas);
            foreach (ColumnaFirebird columna in columnas_ordenadas)
            {
                bool existe = param_definicion_ui.CamposFiltro.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (!existe)
                    continue;

                ColumnaFirebird modelo_desde = new ColumnaFirebird(false, columna.Nombre + "_desde", columna.TipoDato);
                ColumnaFirebird modelo_hasta = new ColumnaFirebird(false, columna.Nombre + "_hasta", columna.TipoDato);

                propiedades += "\t" + modelo_desde.GenerarPropiedadEmbarcadero() + "\n";
                propiedades += "\t" + modelo_hasta.GenerarPropiedadEmbarcadero() + "\n";

                atributos += "\t" + modelo_desde.GenerarAtributoEmbarcadero() + "\n";
                atributos += "\t" + modelo_hasta.GenerarAtributoEmbarcadero() + "\n";
            }

            texto = texto.Replace("{NombreTabla}", "Filtros" + param_tabla.Nombre);
            texto = texto.Replace("{Atributos}", atributos);
            texto = texto.Replace("{Propiedades}", propiedades);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());

            return texto;
        }

        /// <summary>
        /// Genera el texto del fichero modelo de filtros .cpp.
        /// </summary>
        /// <param name="param_tabla">Tabla de la base de datos.</param>
        /// <param name="param_definicion_ui">Definición de la interfaz de usuario.</param>
        /// <returns>Texto con el código del modelo de filtros .cpp.</returns>
        static private string GenerarModeloFiltrosImplementacion(Tabla param_tabla, DefinicionUI param_definicion_ui)
        {
            string texto = Objeto.PlantillaCpp;

            string inicializacion_copia = string.Empty;
            string inicializacion_vacio = string.Empty;
            string limpieza_atributos = string.Empty;
            List<ColumnaFirebird> columnas_ordenadas = ObtenerColumnasOrdenadas(param_tabla.Columnas);
            foreach (ColumnaFirebird columna in columnas_ordenadas)
            {
                bool existe = param_definicion_ui.CamposFiltro.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (!existe)
                    continue;

                ColumnaFirebird modelo_desde = new ColumnaFirebird(false, columna.Nombre + "_desde", columna.TipoDato);
                ColumnaFirebird modelo_hasta = new ColumnaFirebird(false, columna.Nombre + "_hasta", columna.TipoDato);

                inicializacion_copia += "\n\t" + modelo_desde.GenerarInicializacionDeCopia() + ",";
                inicializacion_copia += "\n\t" + modelo_hasta.GenerarInicializacionDeCopia() + ",";
                inicializacion_vacio += "\n\t" + modelo_desde.GenerarInicializacionVacia() + ",";
                inicializacion_vacio += "\n\t" + modelo_hasta.GenerarInicializacionVacia() + ",";
                limpieza_atributos += "\n" + modelo_desde.GenerarLimpiezaAtributos();
                limpieza_atributos += "\n" + modelo_hasta.GenerarLimpiezaAtributos();
            }

            texto = texto.Replace("{NombreTabla}", "Filtros" + param_tabla.Nombre);
            texto = texto.Replace("{IncializacionCopia}", inicializacion_copia.TrimEnd(','));
            texto = texto.Replace("{InicializacionCopiaBlob}", string.Empty);
            texto = texto.Replace("{InicializacionVacioBlob}", string.Empty);
            texto = texto.Replace("{InicializacionVacio}", inicializacion_vacio.TrimEnd(','));
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());
            texto = texto.Replace("{LimpiarAtributos}", limpieza_atributos);

            return texto;
        }

        /// <summary>
        /// Genera el texto del fichero Controlador DAO .h.
        /// </summary>
        /// <param name="param_tabla">Tabla de la base de datos.</param>
        /// <returns>Texto con el código del Controlador DAO .h.</returns>
        static private string GenerarControladorDAOEncabezados(Tabla param_tabla)
        {
            string texto = ControladorDAO.PlantillaH;

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());

            return texto;
        }

        /// <summary>
        /// Genera el texto del fichero Controlador DAO .cpp.
        /// </summary>
        /// <param name="param_tabla">Tabla de la base de datos.</param>
        /// <param name="param_definicion_ui">Definición de la interfaz de usuario.</param>
        /// <returns>Texto con el código del Controlador DAO .cpp.</returns>
        static private string GenerarControladorDAOImplementacion(Tabla param_tabla, DefinicionUI param_definicion_ui)
        {
            string texto = ControladorDAO.PlantillaCpp;

            string campos = string.Empty;
            string parametros = string.Empty;
            string campos_set = string.Empty;
            string carga_parametros = string.Empty;
            string carga_campos = string.Empty;
            string filtro_pk = string.Empty;
            string parametros_filtros = string.Empty;
            string generar_filtros = string.Empty;
            List<ColumnaFirebird> columnas_ordenadas = ObtenerColumnasOrdenadas(param_tabla.Columnas);
            foreach (ColumnaFirebird columna in columnas_ordenadas)
            {
                if (columna.TipoDato == ColumnaFirebird.TiposDatos.BLOB)
                    continue;

                // Rellena los campos
                campos += "\t\"\t" + columna.Nombre;
                if (columna != columnas_ordenadas.Last())
                {
                    campos += ", \\n\"";
                    campos += "\n";
                }

                // Rellena los campos para el SET
                if (!columna.EsClavePrimaria)
                {
                    campos_set += "\t\"\t" + columna.Nombre + " = :" + columna.GetNombreParametro();
                    if (columna != columnas_ordenadas.Last())
                    {
                        campos_set += ", \\n\"";
                        campos_set += "\n";
                    }
                }

                // Rellena los parámetros
                parametros += "\t\"\t:" + columna.GetNombreParametro();
                if (columna != columnas_ordenadas.Last())
                {
                    parametros += ", \\n\"";
                    parametros += "\n";
                }

                carga_parametros += columna.GenerarCargaParametros();
                carga_campos += columna.GenerarCargarCampos();
                
                // Rellena el filtro de la clave primaria
                if (columna.EsClavePrimaria)
                {
                    if (!string.IsNullOrEmpty(filtro_pk))
                    {
                        filtro_pk += " AND \\n \"";
                    }
                    filtro_pk += "\t" + columna.Nombre + " = :P_" + columna.ObtenerNombreCapitalizado();
                }

                if (param_definicion_ui.CamposFiltro.Any(item => item.Nombre == columna.Nombre && item.Seleccionado))
                {
                    ColumnaFirebird modelo_desde = new ColumnaFirebird(false, columna.Nombre + "_desde", columna.TipoDato);
                    ColumnaFirebird modelo_hasta = new ColumnaFirebird(false, columna.Nombre + "_hasta", columna.TipoDato);

                    parametros_filtros += modelo_desde.GenerarCargaParametros();
                    parametros_filtros += modelo_hasta.GenerarCargaParametros();
                    generar_filtros += columna.GenerarFiltros();
                }
            }

            // Reemplaza los marcadores de posición
            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());
            texto = texto.Replace("{Campos}", campos);
            texto = texto.Replace("{CamposSet}", campos_set);
            texto = texto.Replace("{Parametros}", parametros);
            texto = texto.Replace("{CargarParametros}", carga_parametros);
            texto = texto.Replace("{CargarCampos}", carga_campos);
            texto = texto.Replace("{FiltroPK}", filtro_pk);
            texto = texto.Replace("{CargarParametrosFiltros}", parametros_filtros);
            texto = texto.Replace("{GeneracionFiltros}", generar_filtros);

            return texto;
        }

        /// <summary>
        /// Genera el texto del fichero Controlador .h.
        /// </summary>
        /// <param name="param_tabla">Tabla de la base de datos.</param>
        /// <returns>Texto con el código del Controlador .h.</returns>
        static private string GenerarControladorEncabezados(Tabla param_tabla)
        {
            string texto = Controlador.PlantillaH;

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());

            return texto;
        }

        /// <summary>
        /// Genera el texto del fichero Controlador .cpp.
        /// </summary>
        /// <param name="param_tabla">Tabla de la base de datos.</param>
        /// <returns>Texto con el código del Controlador .cpp.</returns>
        static private string GenerarControladorImplementacion(Tabla param_tabla)
        {
            string texto = Controlador.PlantillaCpp;

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());

            return texto;
        }

        /// <summary>
        /// Genera el texto del fichero Formulario .h.
        /// </summary>
        /// <param name="param_tabla">Tabla de la base de datos.</param>
        /// <param name="param_definicion">Definición de la interfaz de usuario.</param>
        /// <returns>Texto con el código del Formulario .h.</returns>
        static private string GenerarUIEncabezados(Tabla param_tabla, DefinicionUI param_definicion)
        {
            string texto = Formulario.PlantillaH;

            string definicion_atributos = string.Empty;
            string definicion_atributos_filtros = string.Empty;
            string enumeracion_columnas = string.Empty;
            foreach (ColumnaFirebird columna in param_tabla.Columnas)
            {
                bool existe = param_definicion.MostrarEnTabla.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (existe)
                {
                    enumeracion_columnas += "\n\t\t" + columna.ObtenerNombreColumna() + ",";
                }

                existe = param_definicion.CamposFiltro.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (existe)
                {
                    ColumnaFirebird modelo_desde = new ColumnaFirebird(false, columna.Nombre + "_desde", columna.TipoDato);
                    ColumnaFirebird modelo_hasta = new ColumnaFirebird(false, columna.Nombre + "_hasta", columna.TipoDato);
                    definicion_atributos_filtros += "\n\t" + modelo_desde.GenerarDefinicionComponentes();
                    definicion_atributos_filtros += "\n\t" + modelo_desde.GenerarDefinicionLabel();
                    definicion_atributos_filtros += "\n\t" + modelo_hasta.GenerarDefinicionComponentes();
                    definicion_atributos_filtros += "\n\t" + modelo_hasta.GenerarDefinicionLabel();
                }


                definicion_atributos += "\n\t" + columna.GenerarDefinicionComponentes();
                definicion_atributos += "\n\t" + columna.GenerarDefinicionLabel();
            }

            enumeracion_columnas.TrimEnd(',');
            texto = texto.Replace("{DefinicionAtributos}", definicion_atributos);
            texto = texto.Replace("{DefinicionAtributosFiltros}", definicion_atributos_filtros);
            texto = texto.Replace("{EnumeracionColumnas}", enumeracion_columnas);
            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);  

            return texto;
        }

        /// <summary>
        /// Genera el texto del fichero Formulario .cpp.
        /// </summary>
        /// <param name="param_tabla">Tabla de la base de datos.</param>
        /// <param name="param_definicion">Definición de la interfaz de usuario.</param>
        /// <returns>Texto con el código del Formulario .cpp.</returns>
        static private string GenerarUIImplementacion(Tabla param_tabla, DefinicionUI param_definicion)
        {
            string texto = Formulario.PlantillaCPP;

            string titulo_columnas = string.Empty;
            string ancho_columnas = string.Empty;
            string carga_modelos = string.Empty;
            string activar_componentes = string.Empty;
            string color_componentes = string.Empty;
            string gestionar_edicion = "";
            string limpiar_columnas = string.Empty;
            string rellenar_columnas = string.Empty;
            string carga_formulario = string.Empty;
            string limpiar_registros = string.Empty;
            string carga_filtros = string.Empty;
            foreach (ColumnaFirebird columna in param_tabla.Columnas)
            {
                if (columna.TipoDato == ColumnaFirebird.TiposDatos.BLOB)
                    continue;

                bool existe = param_definicion.MostrarEnTabla.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (existe)
                {
                    titulo_columnas += "\nStringGridListado->Cells[(int)ColumnasTabla::" + columna.ObtenerNombreColumna() + "][0] = \"" + columna.ObtenerNombreCapitalizado() + "\";";
                    ancho_columnas += "\nStringGridListado->ColWidths[(int)ColumnasTabla::" + columna.ObtenerNombreColumna() + "] = ancho * 20 / 100;";
                    limpiar_columnas += "\n\tStringGridListado->Cells[(int)ColumnasTabla::" + columna.ObtenerNombreColumna() + "][fila] = \"\";";
                    rellenar_columnas += "\n\tStringGridListado->Cells[(int)ColumnasTabla::" + columna.ObtenerNombreColumna() + "][StringGridListado->RowCount - 1] = (*it)->" + columna.ObtenerNombreCapitalizado() + ";";
                }

                existe = param_definicion.CamposEdicion.Any(item => item.Nombre == columna.Nombre && !item.Seleccionado);
                if (existe)
                {
                    gestionar_edicion += "\n" + columna.ObtenerNombreComponente() + "->Enabled = false;";
                    gestionar_edicion += "\n" + columna.ObtenerNombreComponente() + "->Color = clWhite;";
                }

                existe = param_definicion.CamposFiltro.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (existe)
                {
                    ColumnaFirebird modelo_desde = new ColumnaFirebird(false, columna.Nombre + "_desde", columna.TipoDato);
                    ColumnaFirebird modelo_hasta = new ColumnaFirebird(false, columna.Nombre + "_hasta", columna.TipoDato);
                    carga_filtros += "\n" + modelo_desde.CargaModelo(true);
                    carga_filtros += "\n" + modelo_hasta.CargaModelo(true);
                }

                carga_modelos += "\n" + columna.CargaModelo();
                activar_componentes += "\n" + columna.ObtenerNombreComponente() + "->Enabled = param_activar;";
                color_componentes += "\n" + columna.ObtenerNombreComponente() + "->Color = param_color;";
                carga_formulario += "\n" + columna.CargaFormulario();
                limpiar_registros += "\n" + columna.LimpiarComponentes();
            }

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{TituloColumnas}", titulo_columnas);
            texto = texto.Replace("{AnchoColumnas}", ancho_columnas);
            texto = texto.Replace("{CargarModelo}", carga_modelos);
            texto = texto.Replace("{ActivarComponentes}", activar_componentes);
            texto = texto.Replace("{ColorComponentes}", color_componentes);
            texto = texto.Replace("{GestionarEdicion}", gestionar_edicion);
            texto = texto.Replace("{LimpiarColumnas}", limpiar_columnas);
            texto = texto.Replace("{RellenarColumnas}", rellenar_columnas);
            texto = texto.Replace("{CargarFormulario}", carga_formulario);
            texto = texto.Replace("{LimpiarRegistros}", limpiar_registros);
            texto = texto.Replace("{CargarFiltros}", carga_filtros);

            return texto;
        }

        /// <summary>
        /// Genera el texto del fichero Formulario .dfm.
        /// </summary>
        /// <param name="param_tabla">Tabla de la base de datos.</param>
        /// <param name="param_definicion">Definición de la interfaz de usuario.</param>
        /// <returns>Texto con el código del Formulario .dfm.</returns>
        static private string GenerarUIDesigner(Tabla param_tabla, DefinicionUI param_definicion)
        {
            string texto = Formulario.PlantillaDFM;

            int indice_tag = 0;
            int posicion_top = 5;
            int posicion_left = 25;
            int indice_tag_filtros = 0;
            int posicion_top_filtros = 5;
            int posicion_left_filtros = 25;
            string componentes = string.Empty;
            string componentes_filtros = string.Empty;
            foreach (ColumnaFirebird columna in param_tabla.Columnas)
            {
                bool existe = param_definicion.CamposFiltro.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (existe)
                {
                    ColumnaFirebird modelo_desde = new ColumnaFirebird(false, columna.Nombre + "_desde", columna.TipoDato);
                    ColumnaFirebird modelo_hasta = new ColumnaFirebird(false, columna.Nombre + "_hasta", columna.TipoDato);
                    componentes_filtros += modelo_desde.ObtenerDefinicionComponente(indice_tag_filtros, posicion_top_filtros, posicion_left_filtros);
                    indice_tag++;
                    posicion_top_filtros += 30;

                    componentes_filtros += modelo_hasta.ObtenerDefinicionComponente(indice_tag_filtros, posicion_top_filtros, posicion_left_filtros);
                    indice_tag++;
                    posicion_top_filtros += 30;
                }

                componentes += columna.ObtenerDefinicionComponente(indice_tag, posicion_top, posicion_left);
                indice_tag++;
                posicion_top += 30;
            }

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{DefinicionComponentes}", componentes);
            texto = texto.Replace("{DefinicionComponentesFiltros}", componentes_filtros);

            return texto;
        }

        /// <summary>
        /// Ordena alfabéticamente las columnas por su tipo de dato para generar el código de forma ordenada y agrupada por tipos de datos.
        /// </summary>
        /// <param name="param_columna">Listado de atributos de tipo columna.</param>
        /// <returns>Listado de columnas ordenadas.</returns>
        static private List<ColumnaFirebird> ObtenerColumnasOrdenadas(List<Columna> param_columna)
        {
            List<ColumnaFirebird> columnas = param_columna.Cast<ColumnaFirebird>().ToList();
            List<ColumnaFirebird> columnas_ordenadas = columnas.OrderBy(dato => dato.TipoDato).ToList();
            return columnas_ordenadas;
        }
    }
}

