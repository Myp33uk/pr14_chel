using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibMas;
using System.Data;
using System.IO;

namespace практическая_работа__2
{

    public partial class MainWindow : Window
    {
        public int[] generatedArray;
        Password pass = new Password();
        public MainWindow()
        {
            InitializeComponent();
            

            pass.ShowDialog();
            if (pass._Auth != true)
            {
                Close();
            }
            try
            {
                using (StreamReader open1 = new StreamReader(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini")))
                {
                    int _columns = Convert.ToInt32(open1.ReadLine());
                    int _rows = Convert.ToInt32(open1.ReadLine());

                    for (int i = 0; i < _columns; i++)
                    {
                        res.Columns.Add("column " + i.ToString(), typeof(string));
                    }
                    for (int k = 0; k < _rows; k++)
                    {
                        res.Rows.Add();
                    }
                   
                }
                resultGrid.ItemsSource = res.DefaultView;
            }
            catch { MessageBox.Show("Не найден файл конфигурации, будут применены стандартные настройки"); }
        }
        private DataTable res = new DataTable();


        private void Close_Button(object sender, RoutedEventArgs e)
        {

            Close();

        }

        private void Info_Button(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Челяев Никита ИСП-31\nПо массиву A(5,6)получить массив В(6), присвоив его j-элементу значение true, если все элементы j - столбца массива А нулевые, и значение false иначе. ");

        }
        public int[,] generatedMatrix;

        private void FillMatrix_button(object sender, RoutedEventArgs e)
        {
            res = new DataTable();
            resultGrid.ItemsSource = null;
            resultGrid.Items.Clear();
            resultGrid.Columns.Clear();
            generatedMatrix = Matrixs.FillMatrix();

            for (int i = 0; i < generatedMatrix.GetLength(1); i++)
            {
                res.Columns.Add("column " + i.ToString(), typeof(string));
            }
            for (int i = 0; i < generatedMatrix.GetLength(0); i++)
            {

                DataRow row = res.NewRow();
                for (int j = 0; j < generatedMatrix.GetLength(1); j++)
                {
                    row[j] = generatedMatrix[i, j];
                }
                res.Rows.Add(row);
            }

            resultGrid.ItemsSource = res.DefaultView;
            tableSize.Text = "Размер матрицы:5:6";
        }

        private void ResultOutput_button(object sender, RoutedEventArgs e)
        {  if (generatedMatrix == null) return;
            res.Clear();
            Tasks.CreateArrayB(generatedMatrix,out int[] arrayB, out bool boolValue);
            DataRow row = res.NewRow();
                for (int j = 0; j < arrayB.Length; j++)
                {
                    row[j] = arrayB[j];
                }
                
                res.Rows.Add(row);
            resultGrid.ItemsSource = res.DefaultView;
            
            

            resultGrid.ItemsSource = res.DefaultView;
            tableSize.Text = "Размер матрицы:6";
            if (boolValue ==true)boolValueBox.Text = "Столбец нулевой";
            else boolValueBox.Text = "Столбец не нулевой";

        }

        private void saveMatrix_button(object sender, RoutedEventArgs e)
        {
            Matrixs.SaveMatrix1(generatedMatrix);
        }

        private void Loading_button(object sender, RoutedEventArgs e)
        {
            res = new DataTable();
            resultGrid.ItemsSource = null;
            resultGrid.Items.Clear();
            resultGrid.Columns.Clear();
            Matrixs.OpenMatrix(out int[,] savedMatrix);
            DataRow row = res.NewRow();

            for (int i = 0; i < savedMatrix.GetLength(1); i++)
            {
                res.Columns.Add("column " + i.ToString(), typeof(string));
            }
            for (int i = 0; i < savedMatrix.GetLength(0); i++)
            {

                DataRow row1 = res.NewRow();
                for (int j = 0; j < savedMatrix.GetLength(1); j++)
                {
                    row1[j] = savedMatrix[i, j];
                }
                res.Rows.Add(row1);
            }
            res.Rows.Add(row);
            resultGrid.ItemsSource = res.DefaultView;
        }
        private void FocusArray(object sender, MouseEventArgs e)
        {
            int _column = resultGrid.CurrentColumn.DisplayIndex;
            int _row = resultGrid.Items.IndexOf(resultGrid.CurrentItem);
            cellNumber.Text = $"[{_row + 1};{_column + 1}]";
        }

        private void SetsForm(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (pass._Auth == true)
            {
                MessageBoxResult res = MessageBox.Show("Завершить работу программы?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res != MessageBoxResult.Yes) e.Cancel = true;
            }

        }
    }

}
