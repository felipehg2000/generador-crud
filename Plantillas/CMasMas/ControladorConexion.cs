using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generador_crud.Plantillas.CMasMas
{
    internal class ControladorConexion
    {
        static readonly public string PlantillaH =
@"//---------------------------------------------------------------------------

#ifndef ControladorConexionH
#define ControladorConexionH
//---------------------------------------------------------------------------
#include <System.SysUtils.hpp>
#include <IBC.hpp>
//---------------------------------------------------------------------------
/**
 * @brief   Clase para gestionar la conexión a base de datos
 * @details Código generado de manera automática
 * @author  Generador CRUD
 */
class TControladorConexion
	{
	private:
		// Atributos
		String _ruta_bd;                 ///< Ruta de la base de datos a la que estamos conectados.
		TIBCConnection* _conexion_bd;    ///< Componente de conexión a base de datos.

		// Metodos
		void SetRuta(const String &param_ruta);

	public:
		// Constructor y destructor
		TControladorConexion();
		TControladorConexion(const String &param_ruta_bd);
		~TControladorConexion();

		// Métodos
		void ConectarBD();
		void DesconectarBD();


		// Propiedades
		__property String RutaBD = { read = _ruta_bd, write = SetRuta };

		__property TIBCConnection* ConexionBD = { read = _conexion_bd };
   };
//---------------------------------------------------------------------------
#endif
";

		static readonly public string PlantillaCPP =
@"//---------------------------------------------------------------------------

#pragma hdrstop

#include ""ControladorConexion.h""
//---------------------------------------------------------------------------
#pragma package(smart_init)

#pragma region Constructor y destructor
//---------------------------------------------------------------------------
TControladorConexion::TControladorConexion() :
	_ruta_bd(""""),
	_conexion_bd(NULL)
{

}
//---------------------------------------------------------------------------
TControladorConexion::TControladorConexion(const String &param_ruta_bd):
	_ruta_bd(param_ruta_bd),
   _conexion_bd(NULL)
{
ConectarBD();
}
//---------------------------------------------------------------------------
TControladorConexion::~TControladorConexion()
{
if (_conexion_bd != NULL)
	{
   DesconectarBD();
	delete _conexion_bd;
	_conexion_bd = NULL;
	}
}
//---------------------------------------------------------------------------
#pragma end_region Constructor y destructor

#pragma region Metodos privados
//---------------------------------------------------------------------------
void TControladorConexion::SetRuta(const String &param_ruta)
{
DesconectarBD();
_ruta_bd = param_ruta;
ConectarBD();
}
//---------------------------------------------------------------------------
#pragma end_region Metodos privados

#pragma region Metodos publicos
//---------------------------------------------------------------------------
void TControladorConexion::ConectarBD()
{
if (_conexion_bd == NULL)
	{
	_conexion_bd = new TIBCConnection(NULL);
	_conexion_bd->Server          = ""localhost"";
	String ruta_dll = ExtractFilePath(Application->ExeName);
	ruta_dll += ""BaseDatos//fbclient.dll"";
	_conexion_bd->ClientLibrary   = ruta_dll;
	_conexion_bd->Port            = ""4718"";
	_conexion_bd->Database        = _ruta_bd;
	_conexion_bd->Username        = ""SYSDBA"";
	_conexion_bd->Password        = ""masterkey"";
	_conexion_bd->Connect();
	}
}
//---------------------------------------------------------------------------
void TControladorConexion::DesconectarBD()
{
if (_conexion_bd->Connected)
	{
   _conexion_bd->Close();
	}

delete _conexion_bd;
_conexion_bd = NULL;
}
//---------------------------------------------------------------------------
#pragma end_region Metodos publicos

";
    }
}
