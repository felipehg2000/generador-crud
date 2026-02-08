using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generador_crud.Plantillas.CMasMas
{
    internal class Objeto
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
 * Clase: T{NombreTabla}
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
//---------------------------------------------------------------------------
#ifndef {NombreTabla}H
#define {NombreTabla}H
//---------------------------------------------------------------------------
#include <System.SysUtils.hpp>
//---------------------------------------------------------------------------
/**
 * @brief   Objeto que representa la tabla de base de datos {NombreTabla}
 * @author  Generador CRUD.
 */
class T{NombreTabla}
{
private:
	// Atributos
{Atributos}

public:
	// Constructor y destructor
	T{NombreTabla}();
    T{NombreTabla}(T{NombreTabla}* param_otro_modelo);
	~T{NombreTabla}(){};

    // Métodos
    Limpiar();

	// Propiedades
{Propiedades}
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
 * Clase: T{NombreTabla}
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
//---------------------------------------------------------------------------
#pragma hdrstop

#include ""{NombreTabla}.h""
//---------------------------------------------------------------------------
#pragma package(smart_init)

#pragma region Constructor
//---------------------------------------------------------------------------
/**
 * @brief   Constructor, inicializa los atributos a vacío.
 * @details Código generado de manera automática.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
T{NombreTabla}::T{NombreTabla}() :
{InicializacionVacio}
{

}
//---------------------------------------------------------------------------
/**
 * @brief   Constructor de copia, inicializa los atributos con el modelo pasado por parámetro.
 * @details Código generado de manera automática.
 * @param   param_otro_modelo objeto del mismo tipo a clonar en esta instnacia.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
T{NombreTabla}::T{NombreTabla}(T{NombreTabla}* param_otro_modelo) :
{IncializacionCopia}
{

}
//---------------------------------------------------------------------------
#pragma end_region Constructor

#pragma region Metodos Publicos
//---------------------------------------------------------------------------
T{NombreTabla}::Limpiar()
{{LimpiarAtributos}
}
//---------------------------------------------------------------------------
#pragma end_region Metodos Publicos

";
    }
}
