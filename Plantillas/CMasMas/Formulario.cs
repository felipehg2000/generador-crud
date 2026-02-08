using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generador_crud.Plantillas.CMasMas
{
    internal class Formulario
    {
        static readonly public string PlantillaH=
@"
//---------------------------------------------------------------------------
#ifndef Formulario{NombreTabla}H
#define Formulario{NombreTabla}H
//---------------------------------------------------------------------------
#include <System.Classes.hpp>
#include <Vcl.Controls.hpp>
#include <Vcl.StdCtrls.hpp>
#include <Vcl.Forms.hpp>
#include <Vcl.ExtCtrls.hpp>
#include <Vcl.WinXPanels.hpp>
#include <Vcl.Grids.hpp>
#include <Vcl.Mask.hpp>
#include ""DBAccess.hpp""
#include ""IBC.hpp""
#include <Data.DB.hpp>
#include ""Controlador{NombreTabla}.h""
#include ""{NombreTabla}.h""
#include ""DASQLMonitor.hpp""
#include ""IBCSQLMonitor.hpp""
#include <vector>
//---------------------------------------------------------------------------
class TForm{NombreTabla} : public TForm
{
__published:	// IDE-managed Components
	TPanel *PanelTitulo;
	TCardPanel *CardPanelCRUDGenera;
	TCard *CardPrincipal;
	TCard *CardEdicionConsulta;
	TCard *CardFiltros;
	TGridPanel *GridPanelBotones;
	TButton *ButtonCrear;
	TButton *ButtonEditar;
	TButton *ButtonConsultar;
	TButton *ButtonFiltrar;
	TButton *ButtonBorrar;
	TStringGrid *StringGridListado;
	TLabel *LabelRegistros;
	TPanel *PanelEdicion;
	TPanel *PanelSuperiorEdicion;
	TPanel *PanelCentralEdicion;
	TGridPanel *GridPanelBotonesEdicion;
	TButton *ButtonEditarPnlEditar;
	TButton *ButtonCerrarPnlEditar;
	TPanel *PanelFiltros;
	TPanel *PanelTituloFiltros;
	TPanel *PanelCentralFiltros;
	TLabel *Label2;
	TGridPanel *GridPanelBotonesFiltro;
	TButton *ButtonFiltrarFiltros;
	TButton *ButtonCerrarFiltros;
	TButton *ButtonCerrar;{DefinicionAtributos}
	TEdit *EditIdDesde;
	TEdit *EditIdHasta;
	TIBCSQLMonitor *IBCSQLMonitor1;
	void __fastcall ButtonCrearClick(TObject *Sender);
	void __fastcall ButtonEditarClick(TObject *Sender);
	void __fastcall ButtonConsultarClick(TObject *Sender);
	void __fastcall ButtonFiltrarClick(TObject *Sender);
	void __fastcall ButtonBorrarClick(TObject *Sender);
	void __fastcall ButtonCerrarClick(TObject *Sender);
	void __fastcall ButtonCerrarPnlEditarClick(TObject *Sender);
	void __fastcall PanelTituloMouseDown(TObject *Sender, TMouseButton Button, TShiftState Shift,
          int X, int Y);
	void __fastcall StringGridListadoDrawCell(TObject *Sender, System::LongInt ACol,
          System::LongInt ARow, TRect &Rect, TGridDrawState State);
	void __fastcall ButtonEditarPnlEditarClick(TObject *Sender);
	void __fastcall ButtonCerrarFiltrosClick(TObject *Sender);
	void __fastcall ButtonFiltrarFiltrosClick(TObject *Sender);
private:
	// Enumeraciones
	enum class ColumnasTabla
		{{EnumeracionColumnas}
		};

	enum class TipoAperturaEdicion
		{
		NULO     = 0,
		CREACION = 1,
		EDICION  = 2,
		CONSULTA = 3
		};

	// Atributos
	bool _es_modal;

	TControlador{NombreTabla}* _controlador;
	TFiltros{NombreTabla}* _modelo_filtros;
	T{NombreTabla}* _modelo_actual;
	TipoAperturaEdicion _apertura_edicion;

	std::vector<T{NombreTabla}*> _lista_campos;

	// Métodos
	void RellenarModelo();
	void CargarFiltros();
	void LimpiarCampos();

	void ActivarCampos(const bool &param_activar,
		const TColor &param_color);
	void ActualizarTabla();
	void RellenarFormulario();

public:
	// Constructor
	__fastcall TForm{NombreTabla}(TComponent* Owner);

	// Métodos
	void MostrarFormulario(const bool &param_es_modal);

	// Propiedades
	__property bool EsModal = {read = _es_modal, write = _es_modal};
};
//---------------------------------------------------------------------------
extern PACKAGE TForm{NombreTabla} *Form{NombreTabla};
//---------------------------------------------------------------------------
#endif";

		static readonly public string PlantillaCPP =
@"
//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include ""Formulario{NombreTabla}.h""
#include ""ControladorDAO{NombreTabla}.h""
#include ""Filtros{NombreTabla}.h""
#include ""Controlador{NombreTabla}.h""

#include <System.SysUtils.hpp>
#include <System.Classes.hpp>
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma link ""DBAccess""
#pragma link ""IBC""
#pragma link ""DASQLMonitor""
#pragma link ""IBCSQLMonitor""
#pragma resource ""*.dfm""
TForm{NombreTabla} *Form{NombreTabla};

#pragma region Constructor
//---------------------------------------------------------------------------
__fastcall TForm{NombreTabla}::TForm{NombreTabla}(TComponent* Owner) : 
	TForm(Owner),
	_es_modal(false),
	_modelo_actual(NULL),
	_modelo_filtros(NULL),
	_apertura_edicion(TipoAperturaEdicion::NULO)
{
MostrarFormulario(false);
}
//---------------------------------------------------------------------------
#pragma end_region Constructor

#pragma region Eventos

	#pragma region Formulario
//---------------------------------------------------------------------------
void __fastcall TForm{NombreTabla}::PanelTituloMouseDown(TObject *Sender, TMouseButton Button, TShiftState Shift, int X, int Y)
{
if (Button == mbLeft)
  {
  ReleaseCapture();
  SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE | HTCAPTION, 0);
  }
}
//---------------------------------------------------------------------------
	#pragma end_region Formulario

	#pragma region CardPrincipal
//---------------------------------------------------------------------------
void __fastcall TForm{NombreTabla}::ButtonCrearClick(TObject *Sender)
{
_apertura_edicion = TipoAperturaEdicion::CREACION;
ActivarCampos(true, clWhite);
CardEdicionConsulta->Active = true;
}
//--------------------------------------------------------------------------- 
void __fastcall TForm{NombreTabla}::ButtonEditarClick(TObject *Sender)
{
_apertura_edicion = TipoAperturaEdicion::EDICION;
RellenarFormulario();
ActivarCampos(true, clWhite);
CardEdicionConsulta->Active = true;	
}
//--------------------------------------------------------------------------- 
void __fastcall TForm{NombreTabla}::ButtonConsultarClick(TObject *Sender)
{	
_apertura_edicion = TipoAperturaEdicion::CONSULTA;
RellenarFormulario();
ActivarCampos(false, clCream);
CardEdicionConsulta->Active = true;	
}
//--------------------------------------------------------------------------- 
void __fastcall TForm{NombreTabla}::ButtonFiltrarClick(TObject *Sender)
{
CardFiltros->Active = true;	
}
//--------------------------------------------------------------------------- 
void __fastcall TForm{NombreTabla}::ButtonBorrarClick(TObject *Sender)
{
int respuesta = Application->MessageBox(L""¿Está seguro de que quiere eliminar los datos?"",
									L""Atención"",
									MB_YESNO|MB_ICONQUESTION|MB_TASKMODAL);
if (respuesta == IDNO)
	{
	return;
	}

// Elimina los datos
int indice = StringGridListado->Row - 1;
_controlador->Delete(_lista_campos.at(indice));
ActualizarTabla();
ButtonCerrarFiltrosClick(NULL);
}
//---------------------------------------------------------------------------
void __fastcall TForm{NombreTabla}::ButtonCerrarClick(TObject *Sender)
{
if (_es_modal)
   {
   ModalResult = mrOk;
	}
else
	{
	Close();
	}
}
//---------------------------------------------------------------------------
void __fastcall TForm{NombreTabla}::StringGridListadoDrawCell(TObject *Sender, System::LongInt ACol,
          System::LongInt ARow, TRect &Rect, TGridDrawState State)
{
TStringGrid * grid = dynamic_cast<TStringGrid *> (Sender);
bool dibujar_defecto = true;
if (grid)
	{
	UINT format;
	RECT R = RECT(Rect);
	String texto_celda = grid->Cells[ACol][ARow];
	grid->Canvas->Font->Name = ""MS Sans Serif"";
	// Si ponemos DT_VCENTER se invalida DT_WORDBREAK
	// DT_CENTER, DT_RIGHT, DT_LEFT
	format = DT_LEFT | DT_SINGLELINE | DT_VCENTER;

	grid->Canvas->Font->Size = grid->Font->Size;
	if (ARow == 0)
		{ // Linea Fija
		grid->Canvas->Brush->Color = grid->FixedColor;
		grid->Canvas->FillRect(Rect);
		grid->Canvas->Font->Color = clWhite;
		grid->Canvas->Font->Style = grid->Canvas->Font->Style << fsBold;
		}
	else
		{
		grid->Canvas->Font->Style  = grid->Canvas->Font->Style >> fsBold;
		if (grid->Selection.Top == ARow)
			{
         grid->Canvas->Brush->Color = clGradientInactiveCaption;
			grid->Canvas->Font->Color  = TColor(0x00954A00);
			}
		else
			{ // celda normal
			grid->Canvas->Brush->Color = grid->Color;
			grid->Canvas->Font->Color  = grid->Font->Color;
			}
		grid->Canvas->FillRect(Rect);
		}

	if (format == (DT_LEFT | DT_SINGLELINE | DT_VCENTER))
		{
		R.left = R.left + 4;
		}
	if (format == (DT_RIGHT | DT_SINGLELINE | DT_VCENTER))
		{
		R.right = R.right - 4;
		}

	if (dibujar_defecto)
		{
		DrawText(grid->Canvas->Handle,texto_celda.c_str(), texto_celda.Length(), &R, format);
		}
	}
}
//---------------------------------------------------------------------------
	#pragma end_region CardPrincipal

	#pragma region CardEdicionConsulta
//---------------------------------------------------------------------------
void __fastcall TForm{NombreTabla}::ButtonCerrarPnlEditarClick(TObject *Sender)
{
CardPrincipal->Active = true;	
LimpiarCampos();
}
//---------------------------------------------------------------------------
void __fastcall TForm{NombreTabla}::ButtonEditarPnlEditarClick(TObject *Sender)
{
if (_apertura_edicion == TipoAperturaEdicion::CREACION)
	{
	RellenarModelo();
	_controlador->Create(_modelo_actual);   
	ActualizarTabla();
	ButtonCerrarPnlEditarClick(NULL);
	
	}
else if (_apertura_edicion == TipoAperturaEdicion::EDICION)
	{
	RellenarModelo();
	_controlador->Update(_modelo_actual);	
	ActualizarTabla();
	ButtonCerrarPnlEditarClick(NULL);  
	}
else
	{
	throw Exception(""Error de flujo de trabajo."");
   }
}
//---------------------------------------------------------------------------
	#pragma end_region CardEdicionConsulta

	#pragma region CardFiltros
//---------------------------------------------------------------------------
void __fastcall TForm{NombreTabla}::ButtonCerrarFiltrosClick(TObject *Sender)
{
this->CardPrincipal->Active = true;
}
//---------------------------------------------------------------------------
void __fastcall TForm{NombreTabla}::ButtonFiltrarFiltrosClick(TObject *Sender)
{
CargarFiltros();
ActualizarTabla();
ButtonCerrarFiltrosClick(NULL);
}
//---------------------------------------------------------------------------
	#pragma end_region CardFiltros

#pragma end_region Eventos

#pragma region MetodosPublicos
//---------------------------------------------------------------------------
void TForm{NombreTabla}::MostrarFormulario(const bool &param_es_modal)
{
// Cargar atributos
_es_modal = param_es_modal;
CardPrincipal->Active = true;
_controlador = new TControladorCAMPOS();

// Rellenar StringGrid
StringGridListado->RowCount = 2;
StringGridListado->ColCount = 5;{TituloColumnas}

int ancho = StringGridListado->ClientWidth;{AnchoColumnas}

// Cargar datos
CargarFiltros();
_lista_campos = _controlador->Read(_modelo_filtros);
ActualizarTabla();

// Mostrar formulario
if (_es_modal)
	{
//	Show();
	}
else
	{
//	ShowModal();
	}
}
//---------------------------------------------------------------------------
#pragma end_region MetodosPublicos

#pragma region Metodos privados
//---------------------------------------------------------------------------
void TForm{NombreTabla}::RellenarModelo()
{
if (_modelo_actual == NULL)
   {
   _modelo_actual = new TCAMPOS();
	}
{CargarModelo}
}
//---------------------------------------------------------------------------
void TForm{NombreTabla}::CargarFiltros()
{
if (_modelo_filtros == NULL)
   {
   _modelo_filtros = new TFiltrosCAMPOS();
	}
_modelo_filtros->IdDesde = StrToInt64Def(EditIdDesde->Text, 0);
_modelo_filtros->IdHasta = StrToInt64Def(EditIdHasta->Text, 0);
}
//---------------------------------------------------------------------------
void TForm{NombreTabla}::ActivarCampos(const bool &param_activar, const TColor &param_color)
{
{ActivarComponentes}
{ColorComponentes}
if (_apertura_edicion == TiposAperturaEdicion::EDICION)
	{{GestionarEdicion}
	}
}
//---------------------------------------------------------------------------
void TForm{NombreTabla}::ActualizarTabla()
{
for (int fila = 1; fila < StringGridListado->RowCount; fila++)
	{{LimpiarColumnas}
	}
StringGridListado->RowCount = 2;

CargarFiltros();
_lista_campos.erase(_lista_campos.begin(), _lista_campos.end());
_lista_campos = _controlador->Read(_modelo_filtros);

for (std::vector<TCAMPOS*>::iterator it = _lista_campos.begin(); it != _lista_campos.end(); ++it)
	{{RellenarColumnas}

	StringGridListado->RowCount++;
	}

StringGridListado->RowCount--;
}
//---------------------------------------------------------------------------
void TForm{NombreTabla}::RellenarFormulario()
{
int indice = StringGridListado->Row - 1;
TCAMPOS* registro = _lista_campos.at(indice);
{CargarFormulario}
}
//---------------------------------------------------------------------------
void TForm{NombreTabla}::LimpiarCampos()
{
{LimpiarRegistros}

ActivarCampos(true, clWhite);
}
//---------------------------------------------------------------------------
#pragma end_region Metodos privados
";

		static readonly public string PlantillaDFM =
@"
object Form{NombreTabla}: TForm{NombreTabla}
  Left = 0
  Top = 0
  BorderStyle = bsNone
  Caption = 'Form{NombreTabla}'
  ClientHeight = 767
  ClientWidth = 780
  Color = clActiveCaption
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'Segoe UI'
  Font.Style = []
  Position = poDesktopCenter
  TextHeight = 15
  object PanelTitulo: TPanel
    Left = 0
    Top = 0
    Width = 780
    Height = 30
    Align = alTop
    BevelOuter = bvNone
    Caption = '{NombreTabla}'
    Color = clActiveCaption
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'Arial'
    Font.Style = [fsBold]
    ParentBackground = False
    ParentFont = False
    TabOrder = 0
    OnMouseDown = PanelTituloMouseDown
  end
  object CardPanelCRUDGenera: TCardPanel
    AlignWithMargins = True
    Left = 3
    Top = 30
    Width = 774
    Height = 734
    Margins.Top = 0
    Align = alClient
    ActiveCard = CardPrincipal
    Caption = 'CardPanelCRUDGenera'
    TabOrder = 1
    object CardPrincipal: TCard
      Left = 1
      Top = 1
      Width = 772
      Height = 732
      Caption = 'CardPrincipal'
      CardIndex = 0
      ParentBackground = False
      TabOrder = 0
      object LabelRegistros: TLabel
        AlignWithMargins = True
        Left = 10
        Top = 3
        Width = 759
        Height = 16
        Margins.Left = 10
        Align = alTop
        Caption = 'Registors de la tabla:'
        Font.Charset = ANSI_CHARSET
        Font.Color = clMaroon
        Font.Height = -13
        Font.Name = 'Arial'
        Font.Style = [fsBold]
        ParentFont = False
        ExplicitWidth = 134
      end
      object GridPanelBotones: TGridPanel
        Left = 0
        Top = 691
        Width = 772
        Height = 41
        Align = alBottom
        BevelOuter = bvNone
        ColumnCollection = <
          item
            Value = 12.500000000000000000
          end
          item
            Value = 12.500000000000000000
          end
          item
            Value = 12.500000000000000000
          end
          item
            Value = 12.500000000000000000
          end
          item
            Value = 12.500000000000000000
          end
          item
            Value = 12.500000000000000000
          end
          item
            Value = 12.500000000000000000
          end
          item
            Value = 12.500000000000000000
          end>
        ControlCollection = <
          item
            Column = 1
            Control = ButtonCrear
            Row = 0
          end
          item
            Column = 2
            Control = ButtonEditar
            Row = 0
          end
          item
            Column = 3
            Control = ButtonConsultar
            Row = 0
          end
          item
            Column = 4
            Control = ButtonFiltrar
            Row = 0
          end
          item
            Column = 5
            Control = ButtonBorrar
            Row = 0
          end
          item
            Column = 6
            Control = ButtonCerrar
            Row = 0
          end>
        RowCollection = <
          item
            Value = 100.000000000000000000
          end>
        TabOrder = 0
        DesignSize = (
          772
          41)
        object ButtonCrear: TButton
          Left = 107
          Top = 8
          Width = 75
          Height = 25
          Anchors = []
          Caption = 'Crear'
          TabOrder = 0
          OnClick = ButtonCrearClick
        end
        object ButtonEditar: TButton
          Left = 203
          Top = 8
          Width = 75
          Height = 25
          Anchors = []
          Caption = 'Editar'
          TabOrder = 1
          OnClick = ButtonEditarClick
        end
        object ButtonConsultar: TButton
          Left = 300
          Top = 8
          Width = 75
          Height = 25
          Anchors = []
          Caption = 'Consultar'
          TabOrder = 2
          OnClick = ButtonConsultarClick
        end
        object ButtonFiltrar: TButton
          Left = 396
          Top = 8
          Width = 75
          Height = 25
          Anchors = []
          Caption = 'Filtrar'
          TabOrder = 3
          OnClick = ButtonFiltrarClick
        end
        object ButtonBorrar: TButton
          Left = 493
          Top = 8
          Width = 75
          Height = 25
          Anchors = []
          Caption = 'Borrar'
          TabOrder = 4
          OnClick = ButtonBorrarClick
        end
        object ButtonCerrar: TButton
          Left = 589
          Top = 8
          Width = 75
          Height = 25
          Anchors = []
          Caption = 'Cerrar'
          TabOrder = 5
          OnClick = ButtonCerrarClick
        end
      end
      object StringGridListado: TStringGrid
        AlignWithMargins = True
        Left = 10
        Top = 27
        Width = 752
        Height = 659
        Margins.Left = 10
        Margins.Top = 5
        Margins.Right = 10
        Margins.Bottom = 5
        Align = alClient
        Ctl3D = False
        DrawingStyle = gdsGradient
        FixedColor = clNavy
        FixedCols = 0
        RowCount = 2
        GradientEndColor = clNavy
        GradientStartColor = clNavy
        Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goRangeSelect, goRowSelect, goFixedRowDefAlign]
        ParentCtl3D = False
        TabOrder = 1
        OnDblClick = ButtonEditarClick
        OnDrawCell = StringGridListadoDrawCell
      end
    end
    object CardEdicionConsulta: TCard
      Left = 1
      Top = 1
      Width = 772
      Height = 732
      Caption = 'CardEdicionConsulta'
      CardIndex = 1
      ParentBackground = False
      TabOrder = 1
      object PanelEdicion: TPanel
        Left = 48
        Top = 39
        Width = 681
        Height = 633
        Color = clActiveCaption
        ParentBackground = False
        TabOrder = 0
        object PanelSuperiorEdicion: TPanel
          Left = 1
          Top = 1
          Width = 679
          Height = 30
          Align = alTop
          BevelOuter = bvNone
          Caption = 'Titulo edicion'
          Font.Charset = ANSI_CHARSET
          Font.Color = clWindowText
          Font.Height = -16
          Font.Name = 'Arial'
          Font.Style = [fsBold]
          ParentBackground = False
          ParentColor = True
          ParentFont = False
          TabOrder = 0
        end
        object PanelCentralEdicion: TPanel
          AlignWithMargins = True
          Left = 4
          Top = 31
          Width = 673
          Height = 557
          Margins.Top = 0
          Margins.Bottom = 0
          Align = alClient
          BevelOuter = bvNone
          Color = clWhite
          ParentBackground = False
          TabOrder = 1{DefinicionComponentes}
        end
        object GridPanelBotonesEdicion: TGridPanel
          AlignWithMargins = True
          Left = 4
          Top = 588
          Width = 673
          Height = 41
          Margins.Top = 0
          Align = alBottom
          BevelOuter = bvNone
          Color = clWhite
          ColumnCollection = <
            item
              Value = 25.000000000000000000
            end
            item
              Value = 25.000000000000000000
            end
            item
              Value = 25.000000000000000000
            end
            item
              Value = 25.000000000000000000
            end>
          ControlCollection = <
            item
              Column = 1
              Control = ButtonEditarPnlEditar
              Row = 0
            end
            item
              Column = 2
              Control = ButtonCerrarPnlEditar
              Row = 0
            end>
          ParentBackground = False
          RowCollection = <
            item
              Value = 100.000000000000000000
            end>
          TabOrder = 2
          DesignSize = (
            673
            41)
          object ButtonEditarPnlEditar: TButton
            Left = 214
            Top = 8
            Width = 75
            Height = 25
            Anchors = []
            Caption = 'Grabar'
            TabOrder = 0
            OnClick = ButtonEditarPnlEditarClick
          end
          object ButtonCerrarPnlEditar: TButton
            Left = 383
            Top = 8
            Width = 75
            Height = 25
            Anchors = []
            Caption = 'Cerrar'
            TabOrder = 1
            OnClick = ButtonCerrarPnlEditarClick
          end
        end
      end
    end
    object CardFiltros: TCard
      Left = 1
      Top = 1
      Width = 772
      Height = 732
      Caption = 'CardFiltros'
      CardIndex = 2
      ParentBackground = False
      TabOrder = 2
      object PanelFiltros: TPanel
        Left = 48
        Top = 39
        Width = 681
        Height = 633
        Color = clActiveCaption
        ParentBackground = False
        TabOrder = 0
        object PanelTituloFiltros: TPanel
          Left = 1
          Top = 1
          Width = 679
          Height = 30
          Align = alTop
          BevelOuter = bvNone
          Caption = 'Titulo filtros'
          Font.Charset = ANSI_CHARSET
          Font.Color = clWindowText
          Font.Height = -16
          Font.Name = 'Arial'
          Font.Style = [fsBold]
          ParentBackground = False
          ParentColor = True
          ParentFont = False
          TabOrder = 0
        end
        object PanelCentralFiltros: TPanel
          AlignWithMargins = True
          Left = 4
          Top = 31
          Width = 673
          Height = 557
          Margins.Top = 0
          Margins.Bottom = 0
          Align = alClient
          BevelOuter = bvNone
          Color = clWhite
          ParentBackground = False
          TabOrder = 1
          object Label2: TLabel
            Left = 40
            Top = 40
            Width = 34
            Height = 15
            Caption = 'Label1'
          end
          object EditIdDesde: TEdit
            Left = 40
            Top = 61
            Width = 121
            Height = 23
            NumbersOnly = True
            TabOrder = 0
          end
          object EditIdHasta: TEdit
            Left = 40
            Top = 90
            Width = 121
            Height = 23
            NumbersOnly = True
            TabOrder = 1
          end
        end
        object GridPanelBotonesFiltro: TGridPanel
          AlignWithMargins = True
          Left = 4
          Top = 588
          Width = 673
          Height = 41
          Margins.Top = 0
          Align = alBottom
          BevelOuter = bvNone
          Color = clWhite
          ColumnCollection = <
            item
              Value = 25.000000000000000000
            end
            item
              Value = 25.000000000000000000
            end
            item
              Value = 25.000000000000000000
            end
            item
              Value = 25.000000000000000000
            end>
          ControlCollection = <
            item
              Column = 1
              Control = ButtonFiltrarFiltros
              Row = 0
            end
            item
              Column = 2
              Control = ButtonCerrarFiltros
              Row = 0
            end>
          ParentBackground = False
          RowCollection = <
            item
              Value = 100.000000000000000000
            end>
          TabOrder = 2
          DesignSize = (
            673
            41)
          object ButtonFiltrarFiltros: TButton
            Left = 214
            Top = 8
            Width = 75
            Height = 25
            Anchors = []
            Caption = 'Filtrar'
            TabOrder = 0
            OnClick = ButtonFiltrarFiltrosClick
          end
          object ButtonCerrarFiltros: TButton
            Left = 383
            Top = 8
            Width = 75
            Height = 25
            Anchors = []
            Caption = 'Cerrar'
            TabOrder = 1
            OnClick = ButtonCerrarFiltrosClick
          end
        end
      end
    end
  end
  object IBCSQLMonitor1: TIBCSQLMonitor
    Left = 460
    Top = 335
  end
end
";
    }
}
