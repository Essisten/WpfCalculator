using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Convert;
using static System.Math;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public double Numbers(string line)
        {
            List<string> elements = new List<string>();
            if (line == "" || line == null)
                return 0;
            for (int i = 0; i < line.Length; i++)   //Перемещение операторов и операндов в список
            {
                if (line[i] >= '0' && line[i] <= '9')
                {
                    elements.Add(Counting(i, line));
                    i += Counting(i, line).Length - 1;
                }
                else
                    elements.Add(Convert.ToString(line[i]));
            }
            for (int i = 0; i < elements.Count; i++)   //Вычисление содержимого скобок
            {
                if (elements[i] == "(")
                {
                    elements.RemoveAt(i);
                    string newline = "";
                    for (int g = i; g < elements.Count; g++)
                    {
                        if (elements[g] != ")")
                        {
                            newline += Convert.ToString(elements[g]);
                            elements.RemoveAt(g);
                            g--;
                        }
                        else
                        {
                            elements.RemoveAt(g);
                            break;
                        }
                    }
                    elements.Insert(i, Convert.ToString(Numbers(newline)));
                }
            }
            for (int i = 0; i < elements.Count; i++)    //Вычисления наиболее приоритетных операторов
            {
                string a = "", b = "";
                if (elements[i] == "*" || elements[i] == "/" || elements[i] == "^")
                {
                    a = elements[i - 1];
                    b = elements[i + 1];
                }
                else if (elements[i] == "!")
                    a = elements[i - 1];
                if (elements[i] == "")
                    elements.RemoveAt(i);
                if (double.TryParse(a, out double _))
                {
                    switch (elements[i])
                    {
                        case "":
                            elements.RemoveAt(i);
                            break;
                        case "*":
                            elements[i] = Convert.ToString(ToDouble(a) * ToDouble(b));
                            elements.RemoveAt(i - 1);
                            elements.RemoveAt(i);
                            break;
                        case "/":
                            elements[i] = Convert.ToString(ToDouble(a) / ToDouble(b));
                            elements.RemoveAt(i - 1);
                            elements.RemoveAt(i);
                            break;
                        case "^":
                            elements[i] = Convert.ToString(Pow(ToDouble(a), ToDouble(b)));
                            elements.RemoveAt(i - 1);
                            elements.RemoveAt(i);
                            break;
                        case "!":
                            elements[i] = Convert.ToString(Factorial(ToDouble(a)));
                            elements.RemoveAt(i - 1);
                            break;
                    }
                    i = 0;
                }
            }
            for (int i = 0; i < elements.Count; i++)    //Вычисление менее приоритетных операторов
            {
                string a = "", b = "";
                if (elements[i] == "+" || elements[i] == "-")
                {
                    a = elements[i - 1];
                    b = elements[i + 1];
                }
                if (double.TryParse(a, out double _) && double.TryParse(b, out double _))
                {
                    switch (elements[i])
                    {
                        case "+":
                            elements[i] = Convert.ToString(ToDouble(a) + ToDouble(b));
                            elements.RemoveAt(i - 1);
                            break;
                        case "-":
                            elements[i] = Convert.ToString(ToDouble(a) - ToDouble(b));
                            elements.RemoveAt(i - 1);
                            break;
                    }
                    elements.RemoveAt(i);
                    i = 0;
                }
            }
            if (double.TryParse(elements[0], out double _))
                return ToDouble(elements[0]);
            else
                return 0;
        }
        public string Counting(int index, string line)  //Помещение операнда в элемент списка
        {
            string result = "";
            while (index <= line.Length - 1)
            {
                if (((line[index] >= '0') && (line[index] <= '9')) || (line[index] == '.') || (line[index] == ','))
                {
                    result += Convert.ToString(line[index]);
                    index++;
                }
                else
                    break;
            }
            return result;
        }
        private void EqualsButton_Click(object sender, RoutedEventArgs e) => TextBox.Text = Convert.ToString(Numbers(TextBox.Text));
        public double Factorial(double number)
        {
            if (number <= 1)
                return 1;
            else
                return number * Factorial(number - 1);
        }
        private void OneButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "1";
        }
        private void TwoButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "2";
        }
        private void ThreeButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "3";
        }
        private void FourButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "4";
        }
        private void FiveButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "5";
        }
        private void SixButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "6";
        }
        private void SevenButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "7";
        }
        private void EightButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "8";
        }
        private void NineButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "9";
        }
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "+";
        }
        private void ZeroButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "0";
        }
        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "-";
        }
        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "*";
        }
        private void DevideButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "/";
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e) => TextBox.Clear();

        private void PowButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "^";
        }
        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text.Length > 0)
                TextBox.Text = TextBox.Text.Remove(TextBox.Text.Length - 1);
        }

        private void FactorialButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "!";
        }

        private void ScobkaButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += "(";
        }

        private void ScobkaButton2_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Text += ")";
        }
    }
}
