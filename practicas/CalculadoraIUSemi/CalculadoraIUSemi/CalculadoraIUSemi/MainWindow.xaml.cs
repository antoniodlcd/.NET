using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculadoraIUSemi
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string value = (string)button.Content;

            if (IsNumber(value))
            {
                HandleNumbers(value);
            } else if (IsOperator(value))
            {
                HandlerOperator(value);
            } else if (value == "C" || value == "CE")
            {
                HandleClear(value);
            } else if (value == "=")
            {
                HandleEquals(Screen.Text);
            }
        }

        // métodos auxiliares de botones

        private bool IsNumber(string possibleNumber)
        {
            return double.TryParse(possibleNumber, out _);
        }

        private void HandleNumbers(string value)
        {
            if (String.IsNullOrEmpty(Screen.Text))
            {
                Screen.Text = value;
            } else {
                Screen.Text += value;
            }
        }

        private bool IsOperator(string possibleOperator)
        {
            return possibleOperator == "+"    || possibleOperator == "-"   ||
                   possibleOperator == "*"    || possibleOperator == "/"   ||
                   possibleOperator == "Sqrt" || possibleOperator == "Log" ||
                   possibleOperator == "Lob"  || possibleOperator == "Sin" ||
                   possibleOperator == "Cos"  || possibleOperator == "Tan";
        }

        private bool ContainsOtherOperator(string screenContent)
        {
            return screenContent.Contains("+")    || screenContent.Contains("-")   ||
                   screenContent.Contains("*")    || screenContent.Contains("/")   ||
                   screenContent.Contains("Sqrt") || screenContent.Contains("Log") ||
                   screenContent.Contains("Lob")  || screenContent.Contains("Sin") ||
                   screenContent.Contains("Cos")  || screenContent.Contains("Tan") ||
                   screenContent.Contains("^");
        }


        // * Colocar correctamente las condiciones del if
        private void HandlerOperator(string value)
        {
            //if (!ContainsOtherOperator(Screen.Text) && !String.IsNullOrEmpty(Screen.Text))
            //{
            //    if (value == "Sqrt" || value == "Log" || value == "Lob" ||
            //        value == "Sin" || value == "Cos" || value == "Tan")
            //    {
            //        Screen.Text = value + " ";
            //    }
            //    else
            //    {
            //        Screen.Text += value;
            //    }
            //}
            
            if ((string.IsNullOrEmpty(Screen.Text)) && (value == "Sqrt" || value == "Log" || value == "Lob" ||
                                                        value == "Sin"  || value == "Cos" || value == "Tan")) 
            {
                Screen.Text = value + " ";
            }
            else if (!ContainsOtherOperator(Screen.Text) && !String.IsNullOrEmpty(Screen.Text) && !(value == "Sqrt" || value == "Log" || value == "Lob" ||
                                                                                                   value == "Sin" || value == "Cos" || value == "Tan"))
            {
                Screen.Text += value;
            }

        }

        private void HandleClear(string value)
        {
            if (value == "C")
            {
                if (Screen.Text.Length > 1)
                {
                    Screen.Text = Screen.Text.Remove(Screen.Text.Length - 1);
                }
                else
                {
                    Screen.Clear();
                }
            } else if (value == "CE")
            {
                Screen.Clear();
            }
        }

        private void HandleEquals(string screenContent)
        {
            string op = FindOperator(screenContent);

            if (!String.IsNullOrEmpty(op))
            {
                switch (op)
                {
                    case "+":
                        Screen.Text = Sum();
                        break;
                    case "-":
                        Screen.Text = Resta();
                        break;
                    case "*":
                        Screen.Text = Mult();
                        break;
                    case "/":
                        Screen.Text = Div();
                        break;
                    case "Sqrt":
                        Screen.Text = Sqrt();
                        break; 
                    case "Log":
                        Screen.Text = Log();
                        break;
                    case "Lob":
                        Screen.Text = Lob();
                        break;
                    case "Sin":
                        Screen.Text = Sin();
                        break;
                    case "Cos":
                        Screen.Text = Cos();
                        break;
                    case "Tan":
                        Screen.Text = Tan();
                        break;

                }
            }
        }

        private string FindOperator(string screenContent)
        {
            if (screenContent.Contains("Sqrt") || screenContent.Contains("Log") ||
                screenContent.Contains("Sin")  || screenContent.Contains("Cos") ||
                screenContent.Contains("Tan"))
            {
                return screenContent.Split(' ')[0];     // retornamos el primer elemento del array de string, que sería el operador busacado
            } else
            {
                foreach (char c in screenContent)
                {
                    if (IsOperator(c.ToString()))
                    {
                        return c.ToString();
                    }
                }
            }
            return String.Empty;
        }

        // operacioens

        private String Sum()
        {
            string[] numbers = Screen.Text.Split('+');

            double.TryParse(numbers[0], out double n1);
            double.TryParse(numbers[1], out double n2);

            return Math.Round(n1 + n2, 12).ToString();
        }

        private String Resta()
        {
            string[] numbers = Screen.Text.Split('-');

            double.TryParse(numbers[0], out double n1);
            double.TryParse(numbers[1], out double n2);

            return Math.Round(n1 - n2, 12).ToString();
        }

        private String Mult()
        {
            string[] numbers = Screen.Text.Split('*');

            double.TryParse(numbers[0], out double n1);
            double.TryParse(numbers[1], out double n2);

            return Math.Round(n1 * n2, 12).ToString();
        }

        private String Div()
        {
            string[] numbers = Screen.Text.Split('/');

            double.TryParse(numbers[0], out double n1);
            double.TryParse(numbers[1], out double n2);

            return Math.Round(n1 / n2, 12).ToString();
        }

        private String Sqrt()
        {
            string number = Screen.Text.Split(' ')[1];
            double.TryParse(number, out double n);

            return Math.Round(Math.Sqrt(n), 12).ToString();
        }

        private String Log()
        {
            string number = Screen.Text.Split(' ')[1];
            double.TryParse(number, out double n);

            return Math.Round(Math.Log(n), 12).ToString();
        }

        private String Lob()
        {
            string number = Screen.Text.Split(' ')[1];
            double.TryParse(number, out double n);

            return Math.Round(Math.Log10(n), 12).ToString();
        }

        private String Sin()
        {
            string number = Screen.Text.Split(' ')[1];
            double.TryParse(number, out double n);

            return Math.Round(Math.Sin(n), 12).ToString();
        }

        private String Cos()
        {
            string number = Screen.Text.Split(' ')[1];
            double.TryParse(number, out double n);

            return Math.Round(Math.Cos(n), 12).ToString();
        }

        private String Tan()
        {
            string number = Screen.Text.Split(' ')[1];
            double.TryParse(number, out double n);

            return Math.Round(Math.Tan(n), 12).ToString();
        }

        // menú


        private void ModoAvanzadoItem_Checked(object sender, RoutedEventArgs e)
        {
            EspacioModoAvanzado.Visibility = Visibility.Visible;
        }

        private void ModoAvanzadoItem_Unchecked(object sender, RoutedEventArgs e)
        {
            EspacioModoAvanzado.Visibility = Visibility.Collapsed;
        }

        private void SalirMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Seguro que quieres salir?", "Atención",
                                                        MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
