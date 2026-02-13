namespace generador_crud.Plantillas.CMasMas
{
    /// <summary>
    /// Define la plantilla para generar el código de los objetos de cada tabla de base de datos, que se encargan de representar la información de cada tabla en memoria y facilitar su manipulación en el código.
    /// </summary>
    internal class Objeto
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
 * Clase: T{NombreTabla}
 * Autor: GeneradorDeModelos
 * Autor del generador de Modelos: Felipe Hernández González
 */
//---------------------------------------------------------------------------
#ifndef {NombreTabla}H
#define {NombreTabla}H
//---------------------------------------------------------------------------
#include <System.SysUtils.hpp>
#include <System.Classes.hpp>
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
    void Limpiar();

	// Propiedades
{Propiedades}

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
T{NombreTabla}::T{NombreTabla}() :{InicializacionVacio}
{{InicializacionVacioBlob}
}
//---------------------------------------------------------------------------
/**
 * @brief   Constructor de copia, inicializa los atributos con el modelo pasado por parámetro.
 * @details Código generado de manera automática.
 * @param   param_otro_modelo objeto del mismo tipo a clonar en esta instnacia.
 * @since   {FechaGeneracion}
 * @author  Generador CRUD.
 */
T{NombreTabla}::T{NombreTabla}(T{NombreTabla}* param_otro_modelo) :{IncializacionCopia}
{{InicializacionCopiaBlob}
}
//---------------------------------------------------------------------------
#pragma end_region Constructor

#pragma region Metodos Publicos
//---------------------------------------------------------------------------
void T{NombreTabla}::Limpiar()
{{LimpiarAtributos}
}
//---------------------------------------------------------------------------
#pragma end_region Metodos Publicos

// <custom>
{CodigoCustom}
// </custom>
";
    }
}
