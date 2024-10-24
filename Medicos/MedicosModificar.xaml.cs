﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClinicaMedica {
	public partial class MedicosModificar : Window {
		private static Medico? SelectedMedico;

		//---------------------public.constructors-------------------//
		public MedicosModificar() //Crear medico
		{
			InitializeComponent();
			SelectedMedico = null;
			txtDiasDeAtencion.ItemsSource = (new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" }).Select(dia => new HorarioMedico { DiaSemana = dia }).ToList();
			//botonMultiUso.Click += ButtonCrearMedico;
			//botonMultiUso.Content = "Crear";
		}

		public MedicosModificar(Medico selectedMedico) //Modificar medico
		{
			InitializeComponent();
			SelectedMedico = selectedMedico;
			this.txtDiasDeAtencion.ItemsSource = SelectedMedico.GetDiasDeAtencionListForUI();
			this.txtNombre.Text = SelectedMedico.Name;
			this.txtApellido.Text = SelectedMedico.Lastname;
			this.txtDNI.Text = SelectedMedico.Dni;
			this.txtProvincia.Text = SelectedMedico.Provincia;
			this.txtDomicilio.Text = SelectedMedico.Domicilio;
			this.txtLocalidad.Text = SelectedMedico.Localidad;
			this.txtEspecialidad.Text = SelectedMedico.Specialidad;
			this.txtFechaIngreso.SelectedDate = SelectedMedico.FechaIngreso;
			this.txtSueldoMinGarant.Text = SelectedMedico.SueldoMinimoGarantizado.ToString();
			this.txtRealizaGuardia.IsChecked = SelectedMedico.Guardia;
			//botonMultiUso.Click += ButtonModificarMedico;
			//botonMultiUso.Content = "Modificar";
		}
		
		private bool CorroborarUserInputEsSeguro(){
			return !(string.IsNullOrEmpty(this.txtSueldoMinGarant.Text) ||
					 string.IsNullOrEmpty(this.txtDNI.Text) ||
					 this.txtFechaIngreso.SelectedDate is null ||
					 this.txtRealizaGuardia.IsChecked is null);
		}

		
		private bool CorroborarQueNoExistaDNI(){
			//if (MainWindow.DB_MODO == DatabaseType.JSON){
				return BaseDeDatosJSON.Database["medicos"].ContainsKey(this.txtDNI.Text);
			//}
		}
		//---------------------botones.GuardarCambios-------------------//
		private void ButtonGuardar(object sender, RoutedEventArgs e) {
			OperationCode operacion;
			//---------Crear-----------//
			if (SelectedMedico is null) {
				if (CorroborarUserInputEsSeguro()) {
					if (CorroborarQueNoExistaDNI()){
						operacion = OperationCode.YA_EXISTE;
					}
					else if (MainWindow.DB_MODO == DatabaseType.JSON) {
						SelectedMedico = new Medico(this);
						operacion = BaseDeDatosJSON.CreateMedico(SelectedMedico);
					}
					else {
						SelectedMedico = new Medico(this);
						operacion = BaseDeDatosSQL.CreateMedico(SelectedMedico);
					}
				}
				else {
					operacion = OperationCode.MISSING_FIELDS;
				}
			}
			//---------Modificar-----------//
			else {
				string originalDni = SelectedMedico.Dni;
				if (CorroborarUserInputEsSeguro()) {
					SelectedMedico.AsignarDatosFromWindow(this);
					if (MainWindow.DB_MODO == DatabaseType.JSON) {
						operacion = BaseDeDatosJSON.UpdateMedico(SelectedMedico, originalDni);
					}
					else {
						operacion = BaseDeDatosSQL.UpdateMedico(SelectedMedico, originalDni);
					}
				}
				else {
					operacion = OperationCode.MISSING_FIELDS;
				}
			}

			//---------Mensaje-----------//
			MessageBox.Show(operacion switch {
				OperationCode.CREATE_SUCCESS => $"Exito: Se ha creado la instancia de Medico: {SelectedMedico.Name} {SelectedMedico.Lastname}",
				OperationCode.UPDATE_SUCCESS => $"Exito: Se han actualizado los datos de: {SelectedMedico.Name} {SelectedMedico.Lastname}",
				OperationCode.DELETE_SUCCESS => $"Exito: Se ha eliminado a: {SelectedMedico.Name} {SelectedMedico.Lastname} de la base de Datos",
				OperationCode.YA_EXISTE => $"Error: Ya existe un médico con DNI: {this.txtDNI.Text}",
				OperationCode.MISSING_DNI => $"Error: El DNI es obligatorio.",
				OperationCode.MISSING_FIELDS => $"Error: Faltan datos obligatorios por completar.",
				_ => "Error: Sin definir"
			});
		}


		//---------------------botones.Eliminar-------------------//
		private void ButtonEliminar(object sender, RoutedEventArgs e) {
			//---------Checknulls-----------//
			if (SelectedMedico is null || SelectedMedico.Dni is null) {
				MessageBox.Show($"No hay item seleccionado.");
				return;
			}
			//---------confirmacion-----------//
			if (MessageBox.Show($"¿Está seguro que desea eliminar este médico? {SelectedMedico.Name}",
				"Confirmar Eliminación",
				MessageBoxButton.OKCancel,
				MessageBoxImage.Warning
			) != MessageBoxResult.OK) {
				return;
			}
			//---------Eliminar-----------//
			OperationCode operacion;
			if (MainWindow.DB_MODO == DatabaseType.JSON) {
				operacion = BaseDeDatosJSON.DeleteMedico(SelectedMedico.Dni); //MODO JSON
			}
			else {
				operacion = BaseDeDatosSQL.DeleteMedico(SelectedMedico.Dni);//MODO SQL
			}
			//---------Mensaje-----------//
			MessageBox.Show(operacion switch {
				OperationCode.DELETE_SUCCESS => $"Exito: Se han eliminado a: {SelectedMedico.Name} {SelectedMedico.Lastname} de la base de Datos",
				_ => "Error: Sin definir"
			});
		}
		//---------------------botones.VolverAtras-------------------//
		private void ButtonVolver(object sender, RoutedEventArgs e) {
			this.NavegarA<Medicos>();
		}

		//------------------------Fin.BaseDeDatosJSON----------------------//
	}
}
