using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generador_crud.Plantillas.CMasMas
{
    internal class Controlador
    {
        static readonly public string PlantillaH =
@"//---------------------------------------------------------------------------
/**
 * Fichero generado automáticamente por un generador de código de tipo CRUD.
 *
 * Si se modifica este fichero, los cambios se perderán al regenerar el texto.
 * Para evitar la pérdida de cambios, utilice comentarios del tipo:
 * <custom method='...'> 
 *    Implementación del código. 
 * </custom>
 * Method, si es nuevo, se añadirá el literal new, si es código dentro de un
 * método generado, se pondrá el nombre del método.
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
//---------------------------------------------------------------------------
class TControlador{NombreTabla}
{
private:
	// Atributos
	TControladorDAO{NombreTabla}* _controlador_bd;

public:
	// Constructor y destructor
	TControlador{NombreTabla}();
	~TControlador{NombreTabla}();

	// Métodos
	bool Create(T{NombreTabla}* param_modelo);
	bool Update(T{NombreTabla}* param_modelo);
	bool Delete(T{NombreTabla}* param_modelo);

	std::vector<T{NombreTabla}*> Read(TFiltros{NombreTabla}* param_modelo);

	// Propiedades
	__property TControladorDAO{NombreTabla} * ControladorDAO = {read = _controlador_bd};
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
 * Para evitar la pérdida de cambios, utilice comentarios del tipo:
 * <custom method='...'> 
 *    Implementación del código. 
 * </custom>
 * Method, si es nuevo, se añadirá el literal new, si es código dentro de un
 * método generado, se pondrá el nombre del método.
 *
 * Fecha de generación: {FechaGeneracion}
 * Clase: TControlador{NombreTabla}
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
//---------------------------------------------------------------------------
#pragma hdrstop

#include ""TControlador{NombreTabla}.h""
//---------------------------------------------------------------------------
#pragma package(smart_init)

#pragma region Constructor y destructor
//---------------------------------------------------------------------------
TControlador{NombreTabla}::TControlador{NombreTabla}()
{
_controlador_bd = new TControladorDAO{NombreTabla}();
}
//---------------------------------------------------------------------------
TControlador{NombreTabla}::TControlador{NombreTabla}()
{
delete _controlador_bd;
_controlador_bd = NULL;
}
//---------------------------------------------------------------------------
#pragma end_region Constructor y destructor

#pragma region Metodos Publicos
//---------------------------------------------------------------------------
bool TControlador{NombreTabla}::Create(T{NombreTabla}* param_modelo)
{
if (param_modelo == NULL)
   {
   return false;
	}

return _controlador_bd->Create(param_modelo);
}
//---------------------------------------------------------------------------
bool TControlador{NombreTabla}::Update(T{NombreTabla}* param_modelo)
{
if (param_modelo == NULL)
   {
   return false;
	}

return _controlador_bd->Update(param_modelo);
}
//---------------------------------------------------------------------------
bool TControlador{NombreTabla}::Delete(T{NombreTabla}* param_modelo)
{
if (param_modelo == NULL)
   {
   return false;
	}

return _controlador_bd->Delete(param_modelo);
}
//---------------------------------------------------------------------------
std::vector<T{NombreTabla}*> TControlador{NombreTabla}::Read(TFiltros{NombreTabla}* param_modelo)
{
return _controlador_bd->Read(param_modelo);
}
//---------------------------------------------------------------------------
#pragma end_region Metodos Publicos

";
    }
}
