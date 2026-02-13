namespace generador_crud.Plantillas.CMasMas
{
    /// <summary>
    /// Define la plantilla para generar el código de los controladores DAO de cada tabla de base de datos, que se encargan de conectar el modelo con la base de datos.
    /// </summary>
    internal class ControladorDAO
    {
        static readonly public string PlantillaH =
@"//---------------------------------------------------------------------------
/**
 * Fichero generado automáticamente por un generador de código de tipo CRUD.
 *
 * Si se modifica este fichero, los cambios se perderán al regenerar el texto.
 * Para evitar la pérdida de cambios, el código a mantener introduzcalo en el apartado:
 * // <custom>
 *
 * // </custom>
 *
 * Fecha de generación: {FechaGeneracion}
 * Clase: TControladorDAO{NombreTabla}
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
//---------------------------------------------------------------------------
#ifndef ControladorDAO{NombreTabla}H
#define ControladorDAO{NombreTabla}H
//---------------------------------------------------------------------------
#include ""{NombreTabla}.h""
#include ""Filtros{NombreTabla}.h""
#include <vector>
#include <IBC.hpp>
#include <System.SysUtils.hpp>
//---------------------------------------------------------------------------
/**
 * @brief   Controlador de acceso a base de datos para la tabla {NombreTabla}
 * @author  Generador CRUD.
 */
class TControladorDAO{NombreTabla}
{
private:
	// Atributos
	TIBCConnection* _conexion_bd;
	TIBCQuery* _consulta_sql;

	// Métodos
	std::vector<T{NombreTabla}*> EjecutarConsultaLectura(const String &param_consulta,
		TFiltros{NombreTabla}* param_filtros);
	String GenerarFiltros(TFiltros{NombreTabla}* param_filtros);

	T{NombreTabla}* CargarModelo();

	void EjecutarConsultaEscritura(const String &param_consulta,
		T{NombreTabla}* param_modelo);
	void LimpiarConsulta();
	void Parametrizar(T{NombreTabla}* param_datos);
	void ParametrizarFiltros(TFiltros{NombreTabla}* param_filtros);

public:
	// Constructor y destructor
	TControladorDAO{NombreTabla}(TIBCConnection* param_conexion);
	~TControladorDAO{NombreTabla}();

	// Métodos
	bool Create(T{NombreTabla}* param_modelo);
	bool Update(T{NombreTabla}* param_modelo);
	bool Delete(T{NombreTabla}* param_modelo);

	std::vector<T{NombreTabla}*> Read(TFiltros{NombreTabla}* param_modelo);

	// Propiedades
    __property TIBCConnection * ConexionBD = {read = _conexion_bd};
	__property TIBCQuery * ConsultaSQL = {read = _consulta_sql};

// <custom>
{CodigoCustom}
// </custom>

};
//---------------------------------------------------------------------------
#endif

";

		static readonly public string PlantillaCpp =
@"//---------------------------------------------------------------------------
/**
 * Fichero generado automáticamente por un generador de código de tipo CRUD.
 *
 * Si se modifica este fichero, los cambios se perderán al regenerar el texto.
 * Para evitar la pérdida de cambios, el código a mantener introduzcalo en el apartado:
 * // <custom>
 *
 * // </custom>
 *
 * Fecha de generación: {FechaGeneracion}
 * Clase: TControladorDAO{NombreTabla}
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
//---------------------------------------------------------------------------
#pragma hdrstop

#include ""ControladorDAO{NombreTabla}.h""
//---------------------------------------------------------------------------
#pragma package(smart_init)

#pragma region Constructor y destructor
//---------------------------------------------------------------------------
/**
 * @brief   Constructor, inicializa los atributos a vacío.
 * @details Código generado de manera automática.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
TControladorDAO{NombreTabla}::TControladorDAO{NombreTabla}(TIBCConnection* param_conexion_bd) :
	_conexion_bd(param_conexion_bd)
{
_consulta_sql = new TIBCQuery(NULL);
_consulta_sql->Connection = _conexion_bd;
}
//---------------------------------------------------------------------------
/**
 * @brief   Destructor, libera la memoria ocupada por los atributos.
 * @details Código generado de manera automática.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
TControladorDAO{NombreTabla}::~TControladorDAO{NombreTabla}()
{
delete _consulta_sql;
_consulta_sql = NULL;
}
//---------------------------------------------------------------------------
#pragma end_region Constructor y destructor

#pragma region Metodos publicos
//---------------------------------------------------------------------------
/**
 * @brief   Inserta un nuevo registro en la tabla {NombreTabla}.
 * @details Código generado de manera automática.
 * @param   param_modelo objeto que representa la tabla {NombreTabla} con los datos a insertar.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
bool TControladorDAO{NombreTabla}::Create(T{NombreTabla}* param_modelo)
{
String consulta =	
	""INSERT INTO \n""
	""{NombreTabla}( \n""
{Campos}) \n""
	""VALUES ( \n""
{Parametros})\n"";

EjecutarConsultaEscritura(consulta, param_modelo);
return true;
}
//---------------------------------------------------------------------------
/**
 * @brief   Inserta un nuevo registro en la tabla {NombreTabla}.
 * @details Código generado de manera automática.
 * @param   param_modelo objeto que representa la tabla {NombreTabla} con los datos a insertar.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
bool TControladorDAO{NombreTabla}::Update(T{NombreTabla}* param_modelo)
{
String consulta =	
	""UPDATE \n""
	""	{NombreTabla} \n""
	""SET \n""
{CamposSet} \n""
	""WHERE \n""
	""{FiltroPK}"";

EjecutarConsultaEscritura(consulta, param_modelo);
return true;
}
//---------------------------------------------------------------------------
/**
 * @brief   Elimina un registro de la tabla {NombreTabla}.
 * @details Código generado de manera automática.
 * @param   param_modelo objeto que representa la tabla {NombreTabla} con los datos del registro a eliminar (con tener la PK cargada es suficiente).
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
bool TControladorDAO{NombreTabla}::Delete(T{NombreTabla}* param_modelo)
{
String consulta = 
	""DELETE FROM \n""
	""	{NombreTabla} \n""
	""WHERE \n""
	""{FiltroPK}"";

EjecutarConsultaEscritura(consulta, param_modelo);
return true;
}
//---------------------------------------------------------------------------
/**
 * @brief   Lee registros de la tabla {NombreTabla} que cumplen los filtros indicados.
 * @details Código generado de manera automática.
 * @param   param_modelo objeto que representa los filtros a aplicar en la consulta.
 * @return  Vector de punteros a objetos T{NombreTabla} con los registros obtenidos.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
std::vector<T{NombreTabla}*> TControladorDAO{NombreTabla}::Read(TFiltros{NombreTabla}* param_modelo)
{
String filtros = GenerarFiltros(param_modelo);
String consulta =	
	""SELECT \n""
	""  * \n""
	""FROM \n""
	""	{NombreTabla} \n""
	""WHERE \n""
	""  "" + filtros + "";"";

return EjecutarConsultaLectura(consulta, param_modelo);
}
//---------------------------------------------------------------------------
#pragma end_region Metodos publicos

#pragma region Metodos privados
//---------------------------------------------------------------------------
/**
 * @brief   Ejecuta una consulta de lectura y devuelve los resultados.
 * @details Código generado de manera automática.
 * @param   param_consulta Consulta SQL a ejecutar.
 * @return  Vector de punteros a objetos T{NombreTabla} con los registros obtenidos.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
std::vector<T{NombreTabla}*> TControladorDAO{NombreTabla}::EjecutarConsultaLectura(const String &param_consulta, TFiltros{NombreTabla}* param_filtros)
{
std::vector<T{NombreTabla}*> ret_modelo;

LimpiarConsulta();
_consulta_sql->SQL->Text = param_consulta;
ParametrizarFiltros(param_filtros);

_consulta_sql->Open();
for (_consulta_sql->First(); !_consulta_sql->Eof; _consulta_sql->Next())
	{
	T{NombreTabla}* nodo = CargarModelo();
	ret_modelo.push_back(nodo);
	}
_consulta_sql->Close();

LimpiarConsulta();

return ret_modelo;
}
//---------------------------------------------------------------------------
/**
 * @brief   Genera los filtros para introducirlos en la base de datos.
 * @details Código generado de manera automática.
 * @param   param_modelo Modelo con los filtros a añadir en la consulta.
 * @return  ret_filtros Filtros a introducir en la consulta.
 * @since	{FechaGeneracion}
 * @author  Generador CRUD.
 */
String TControladorDAO{NombreTabla}::GenerarFiltros(TFiltros{NombreTabla}* param_modelo)
{
String ret_filtros = ""1 = 1"";
if (param_modelo == NULL)
	{
	return ret_filtros;
	}

{GeneracionFiltros}

return ret_filtros;
}
//---------------------------------------------------------------------------
/**
 * @brief   Carga un objeto T{NombreTabla} con los datos del registro actual del conjunto de resultados.
 * @details Código generado de manera automática.
 * @return  Puntero a un objeto T{NombreTabla} con los datos del registro actual.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
T{NombreTabla}* TControladorDAO{NombreTabla}::CargarModelo()
{
T{NombreTabla}* ret_modelo = new T{NombreTabla}();
TField* campo = NULL;
{CargarCampos}
return ret_modelo;
}
//---------------------------------------------------------------------------
/**
 * @brief   Ejecuta una consulta de escritura (INSERT, UPDATE, DELETE).
 * @details Código generado de manera automática.
 * @param   param_consulta Consulta SQL a ejecutar.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
void TControladorDAO{NombreTabla}::EjecutarConsultaEscritura(const String &param_consulta, T{NombreTabla}* param_modelo)
{
LimpiarConsulta();

_consulta_sql->SQL->Text = param_consulta;
Parametrizar(param_modelo);

_consulta_sql->ExecSQL();

LimpiarConsulta();
}
//---------------------------------------------------------------------------
/**
 * @brief   Limpia la consulta SQL preparada.
 * @details Código generado de manera automática.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
void TControladorDAO{NombreTabla}::LimpiarConsulta()
{
_consulta_sql->Close();
_consulta_sql->SQL->Clear();
}
//---------------------------------------------------------------------------
/**
 * @brief   Parametriza la consulta SQL con los datos del modelo.
 * @details Código generado de manera automática.
 * @param   param_modelo Objeto T{NombreTabla} con los datos a parametrizar.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
void TControladorDAO{NombreTabla}::Parametrizar(T{NombreTabla}* param_modelo)
{
TParam* parametro = NULL;
{CargarParametros}
}
//---------------------------------------------------------------------------
/**
 * @brief   Parametriza la consulta SQL con los filtros indicados.
 * @details Código generado de manera automática.
 * @param   param_modelo Objeto TFiltros{NombreTabla} con los filtros a parametrizar.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
void TControladorDAO{NombreTabla}::ParametrizarFiltros(TFiltros{NombreTabla}* param_modelo)
{
TParam* parametro = NULL;
{CargarParametrosFiltros}
}
//---------------------------------------------------------------------------
#pragma end_region Metodos privados

// <custom>
{CodigoCustom}
// </custom>

";
    }
}
