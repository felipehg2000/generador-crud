using generador_crud.Modelos.Objetos;
using generador_crud.Plantillas.CMasMas;

namespace generador_crud.Controladores
{
    internal class ControladorSalidaFirebird
    {
        static public void Generar(Configuracion param_configuracion,
            Tabla param_tabla,
            DefinicionUI param_definicion_ui)
        {
            // Generar fichero de modelo .h
            string texto = GenerarModeloEncabezados(param_tabla);
            string ruta_modelo = param_configuracion.RutaModelos + "\\" + param_tabla.Nombre + ".h";
            string texto_custom = "";
            if (!param_configuracion.SobreescribirArchivosExistentes)
                texto_custom = RescatarCustom.Obtener(ruta_modelo);
            System.IO.File.WriteAllText(ruta_modelo, texto);

            // Generar fichero de modelo .cpp
            texto = GenerarModeloImplementacion(param_tabla);
            ruta_modelo = param_configuracion.RutaModelos + "\\" + param_tabla.Nombre + ".cpp";
            System.IO.File.WriteAllText(ruta_modelo, texto);

            // Generar fichero de controlador DAO .h
            texto = GenerarControladorDAOEncabezados(param_tabla);
            string ruta_controlador_dao = param_configuracion.RutaControladoresDAO + "\\ControladorDAO" + param_tabla.Nombre + ".h";
            System.IO.File.WriteAllText(ruta_controlador_dao, texto);

            // Generar fichero de controlador DAO .cpp
            texto = GenerarControladorDAOImplementacion(param_tabla);
            ruta_controlador_dao = param_configuracion.RutaControladoresDAO + "\\ControladorDAO" + param_tabla.Nombre + ".cpp";
            System.IO.File.WriteAllText(ruta_controlador_dao, texto);

            // Generar fichero de controlador .h
            texto = GenerarControladorEncabezados(param_tabla);
            string ruta_controlador = param_configuracion.RutaControladores + "\\Controlador" + param_tabla.Nombre + ".h";
            System.IO.File.WriteAllText(ruta_controlador, texto);

            // Generar fichero de controlador .cpp
            texto = GenerarControladorImplementacion(param_tabla);
            ruta_controlador = param_configuracion.RutaControladores + "\\Controlador" + param_tabla.Nombre + ".cpp";
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

        static private string GenerarModeloImplementacion(Tabla param_tabla)
        {
            string texto = Objeto.PlantillaCpp;

            string inicializacion_copia = string.Empty;
            string inicializacion_vacio = string.Empty;
            string limpieza_atributos = string.Empty;
            List<ColumnaFirebird> columnas_ordenadas = ObtenerColumnasOrdenadas(param_tabla.Columnas);
            foreach (ColumnaFirebird columna in columnas_ordenadas)
            {
                inicializacion_copia += "\t" + columna.GenerarInicializacionDeCopia() + "\n";
                inicializacion_vacio += "\t" + columna.GenerarInicializacionVacia() + "\n";
                limpieza_atributos += "\n" + columna.GenerarLimpiezaAtributos();
            }

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{IncializacionCopia}", inicializacion_copia);
            texto = texto.Replace("{InicializacionVacio}", inicializacion_vacio);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());
            texto = texto.Replace("{LimpiarAtributos}", limpieza_atributos);

            return texto;
        }

        static private string GenerarModeloFiltrosEncabezados(Tabla param_tabla)
        {
            throw new NotImplementedException();
        }

        static private string GenerarModeloFiltrosImplementacion(Tabla param_tabla)
        {
            throw new NotImplementedException();
        }

        static private string GenerarControladorDAOEncabezados(Tabla param_tabla)
        {
            string texto = ControladorDAO.PlantillaH;

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());

            return texto;
        }

        static private string GenerarControladorDAOImplementacion(Tabla param_tabla)
        {
            string texto = ControladorDAO.PlantillaCpp;

            string campos = string.Empty;
            string parametros = string.Empty;
            string campos_set = string.Empty;
            string carga_parametros = string.Empty;
            string carga_campos = string.Empty;
            string filtro_pk = string.Empty;
            List<ColumnaFirebird> columnas_ordenadas = ObtenerColumnasOrdenadas(param_tabla.Columnas);
            foreach (ColumnaFirebird columna in columnas_ordenadas)
            {
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
                    campos_set += "\t\"\t" + columna.Nombre + " = " + columna.GetNombreParametro();
                    if (columna != columnas_ordenadas.Last())
                    {
                        campos_set += ", \\n\"";
                        campos_set += "\n";
                    }
                }

                // Rellena los parámetros
                parametros += "\t\"\t" + columna.GetNombreParametro();
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
                    filtro_pk += "\t@P_" + columna.ObtenerNombreCapitalizado();
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

            return texto;
        }

        static private string GenerarControladorEncabezados(Tabla param_tabla)
        {
            string texto = Controlador.PlantillaH;

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());

            return texto;
        }

        static private string GenerarControladorImplementacion(Tabla param_tabla)
        {
            string texto = Controlador.PlantillaCpp;

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{FechaGeneracion}", DateTime.Now.ToString());

            return texto;
        }

        static private string GenerarUIEncabezados(Tabla param_tabla, DefinicionUI param_definicion)
        {
            string texto = Formulario.PlantillaH;

            string definicion_atributos = string.Empty;
            string enumeracion_columnas = string.Empty;
            foreach (ColumnaFirebird columna in param_tabla.Columnas)
            {
                bool existe = param_definicion.MostrarEnTabla.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (existe)
                {
                    enumeracion_columnas += "\n\t\t" + columna.ObtenerNombreColumna() + ",";
                }

                enumeracion_columnas.TrimEnd(',');

                definicion_atributos += "\n\t" + columna.GenerarAtributoEmbarcadero();
                definicion_atributos += "\n\t" + columna.GenerarDefinicionLabel();
            }

            texto = texto.Replace("{DefinicionAtributos}", definicion_atributos);
            texto = texto.Replace("{EnumeracionColumnas}", enumeracion_columnas);
            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);  

            return texto;
        }

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
            foreach (ColumnaFirebird columna in param_tabla.Columnas)
            {
                bool existe = param_definicion.MostrarEnTabla.Any(item => item.Nombre == columna.Nombre && item.Seleccionado);
                if (existe)
                {
                    titulo_columnas += "\nStringGridListado->Cells[(int)ColumnasTabla::" + columna.ObtenerNombreColumna() + "][0] = \"" + columna.ObtenerNombreCapitalizado() + "\";";
                    ancho_columnas += "\nStringGridListado->ColWidths[(int)ColumnasTabla::" + columna.ObtenerNombreColumna() + "] = ancho * 20 / 100;";
                    limpiar_columnas += "\n\tStringGridListado->Cells[(int)ColumnasTabla::" + columna.ObtenerNombreColumna() + "][fila] = \"\"";
                    rellenar_columnas += "\n\tStringGridListado->Cells[(int)ColumnasTabla::" + columna.ObtenerNombreColumna() + "][StringGridListado->RowCount - 1] = (*it)->" + columna.ObtenerNombreCapitalizado() + ";";
                }

                existe = param_definicion.CamposEdicion.Any(item => item.Nombre == columna.Nombre && !item.Seleccionado);
                if (existe)
                {
                    gestionar_edicion += "\n" + columna.ObtenerNombreComponente() + "->Enabled = false;";
                    gestionar_edicion += "\n" + columna.ObtenerNombreComponente() + "->Color = clWhite;";
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

            return texto;
        }

        static private string GenerarUIDesigner(Tabla param_tabla, DefinicionUI param_definicion)
        {
            string texto = Formulario.PlantillaDFM;

            int indice_tag = 0;
            int posicion_top = 5;
            int posicion_left = 25;
            string componentes = string.Empty;
            foreach (ColumnaFirebird columna in param_tabla.Columnas)
            {
                componentes += columna.ObtenerDefinicionComponente(indice_tag, posicion_top, posicion_left);
                indice_tag++;
                posicion_top += 30;
            }

            texto = texto.Replace("{NombreTabla}", param_tabla.Nombre);
            texto = texto.Replace("{DefinicionComponentes}", componentes);

            return texto;
        }

        static private List<ColumnaFirebird> ObtenerColumnasOrdenadas(List<Columna> param_columna)
        {
            List<ColumnaFirebird> columnas = param_columna.Cast<ColumnaFirebird>().ToList();
            List<ColumnaFirebird> columnas_ordenadas = columnas.OrderBy(dato => dato.TipoDato).ToList();
            return columnas_ordenadas;
        }
    }
}

