using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculadora
{
    public partial class MainPage : ContentPage
    {
        private bool puntoDecimalPermitido = true;

        public MainPage()
        {
            InitializeComponent();
        }
        private void numPresionado(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string text = button.Text;
            if (text == "." && !puntoDecimalPermitido)
            {
                return;
            }

            if (text == ".")
            {
                puntoDecimalPermitido = false;
            }

            if (pantalla.Text == "0" && text != ".")
            {
                pantalla.Text = text;
            }
            else
            {
                pantalla.Text += text;
            }
        }
        private void Operaciones(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string operador = button.Text;
            if (pantalla.Text.EndsWith("+") || pantalla.Text.EndsWith("-") ||
                pantalla.Text.EndsWith("X") || pantalla.Text.EndsWith("÷"))
            {
                DisplayAlert("Error", "No puedes usar más de un operador seguido.", "OK");
                return;
            }
            puntoDecimalPermitido = true;
            if (operador == "X") operador = "*";
            if (operador == "÷") operador = "/";

            pantalla.Text += operador;
        }
        private void Validacion(object sender, EventArgs e)
        {
            if (puntoDecimalPermitido)
            {
                pantalla.Text += ".";
                puntoDecimalPermitido = false; 
            }
        }
        private void borrarTodo(object sender, EventArgs e)
        {
            pantalla.Text = "0";
            puntoDecimalPermitido = true;
        }
        private void borrarDigito(object sender, EventArgs e)
        {
            if (pantalla.Text.Length > 1)
            {
                if (pantalla.Text.EndsWith("."))
                {
                    puntoDecimalPermitido = true;
                }

                pantalla.Text = pantalla.Text.Substring(0, pantalla.Text.Length - 1);
            }
            else
            {
                pantalla.Text = "0";
            }
        }
        private void Resultados(object sender, EventArgs e)
        {
            try
            {
                string expression = pantalla.Text.Replace("X", "*").Replace("÷", "/");
                var result = new DataTable().Compute(expression, null);
                pantalla.Text = result.ToString();
                puntoDecimalPermitido = true;
            }
            catch (DivideByZeroException)
            {
                DisplayAlert("Error", "No se puede dividir entre cero.", "OK");
                pantalla.Text = "0";
            }
            catch (Exception)
            {
                DisplayAlert("Error", "Syntax error", "OK");
                pantalla.Text = "0";
            }
        }
    }
}















