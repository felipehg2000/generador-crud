namespace generador_crud.Plantillas.CMasMas
{
	/// <summary>
	/// Define la plantilla para generar el código de los controladores de cada tabla de base de datos, que se encargan de conectar la vista con el modelo y la base de datos.
	/// </summary>
    internal class Controlador
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
 * Clase: TControlador{NombreTabla}
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
//---------------------------------------------------------------------------

#ifndef Controlador{NombreTabla}H
#define Controlador{NombreTabla}H
//---------------------------------------------------------------------------
#include ""ControladorDAO{NombreTabla}.h""
#include ""ControladorConexion.h""
//---------------------------------------------------------------------------
class TControlador{NombreTabla}
{
private:
	// Atributos
	bool _creada_conexion;
	TControladorConexion* _conexion_bd;       
	TControladorDAO{NombreTabla}* _controlador_bd;

public:
	// Constructor y destructor
	TControlador{NombreTabla}();
	TControlador{NombreTabla}(TControladorConexion* param_conexion_bd);
	~TControlador{NombreTabla}();

	// Métodos
	bool Create(T{NombreTabla}* param_modelo);
	bool Update(T{NombreTabla}* param_modelo);
	bool Delete(T{NombreTabla}* param_modelo);

	std::vector<T{NombreTabla}*> Read(TFiltros{NombreTabla}* param_modelo);

	// Propiedades
	__property TControladorDAO{NombreTabla} * ControladorDAO = {read = _controlador_bd};
	__property TControladorConexion * ConexionBD = {read = _conexion_bd};

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
 * Clase: TControlador{NombreTabla}
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
//---------------------------------------------------------------------------
#pragma hdrstop

#include ""Controlador{NombreTabla}.h""
//---------------------------------------------------------------------------
#pragma package(smart_init)

#pragma region Constructor y destructor
//---------------------------------------------------------------------------
/**
 * @brief   Constructor de la clase, inicializa atributos
 * @details Código generado de manera automática.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
TControlador{NombreTabla}::TControlador{NombreTabla}() :
	_creada_conexion(true)
{
_conexion_bd = new TControladorConexion(""C://Users//ApliUser//Desktop//Pruebas Generador//BDAUXGENERADOR.FDB"");
_controlador_bd = new TControladorDAOCAMPOS(_conexion_bd->ConexionBD);
}
//---------------------------------------------------------------------------
/**
 * @brief   Constructor de la clase, inicializa atributos.
 * @details Código generado de manera automática.
 * @param   param_conexion_bd Conexión a la base de datos, si es nula se lanza una excepción.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
TControlador{NombreTabla}::TControlador{NombreTabla}(TControladorConexion* param_conexion_bd) :
	_creada_conexion(false)
{
_conexion_bd = param_conexion_bd;
if (param_conexion_bd == NULL)
   {
	throw EArgumentNilException(""Error, la conexión es nula"");
	}
}
//---------------------------------------------------------------------------
/**
 * @brief   Destructor de la clase, libera memoria.
 * @details Código generado de manera automática.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
TControlador{NombreTabla}::~TControlador{NombreTabla}()
{
if (_controlador_bd != NULL)
   {
	delete _controlador_bd;
	_controlador_bd = NULL;
	}

if (_conexion_bd != NULL && _creada_conexion)
	{
	delete _conexion_bd;
   _conexion_bd = NULL;
	}
}
//---------------------------------------------------------------------------
#pragma end_region Constructor y destructor

#pragma region Metodos Publicos
//---------------------------------------------------------------------------
/**
 * @brief   Comprueba datos de entrada y conecta la vista con el modelo para una creación.
 * @details Código generado de manera automática.
 * @param   param_modelo objeto de tipo T{NombreTabla} con los datos a insertar en la base de datos.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
bool TControlador{NombreTabla}::Create(T{NombreTabla}* param_modelo)
{
if (param_modelo == NULL)
   {
   return false;
	}

return _controlador_bd->Create(param_modelo);
}
//---------------------------------------------------------------------------
/**
 * @brief   Comprueba datos de entrada y conecta la vista con el modelo para una edición.
 * @details Código generado de manera automática.
 * @param   param_modelo objeto de tipo T{NombreTabla} con los datos a editar en la base de datos.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
bool TControlador{NombreTabla}::Update(T{NombreTabla}* param_modelo)
{
if (param_modelo == NULL)
   {
   return false;
	}

return _controlador_bd->Update(param_modelo);
}
//---------------------------------------------------------------------------
/**
 * @brief   Comprueba datos de entrada y conecta la vista con el modelo para eliminación.
 * @details Código generado de manera automática.
 * @param   param_modelo objeto de tipo T{NombreTabla} con, mínimo, la clave primaria de la tupla a eliminar de la base de datos.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
bool TControlador{NombreTabla}::Delete(T{NombreTabla}* param_modelo)
{
if (param_modelo == NULL)
   {
   return false;
	}

return _controlador_bd->Delete(param_modelo);
}
//---------------------------------------------------------------------------
/**
 * @brief   Conecta la vista con la lectura de campos de una base de datos.
 * @details Código generado de manera automática.
 * @param   param_modelo objeto de tipo TFiltros{NombreTabla} con los campos por los que filtar la consulta a base de datos. Puede ser NULL.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
std::vector<T{NombreTabla}*> TControlador{NombreTabla}::Read(TFiltros{NombreTabla}* param_modelo)
{
return _controlador_bd->Read(param_modelo);
}
//---------------------------------------------------------------------------
#pragma end_region Metodos Publicos

// <custom>
{CodigoCustom}
// </custom>

";
    }
}
