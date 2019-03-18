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

namespace Cryptographics
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            result1.Text = " ";
            result2.Text = " ";
        }

        public string[,] CreateTwoDimensionalTable(int rows, int columns)
        {
            string[,] table = new string[rows, columns];

            return table;
        }

        public string[,] wypelnienie(string[,] tab, string content, int key)
        {
            string[,] table = tab;
            string word = content;

            int k = 0;
            int j = 0;
            int direction = 1;

            for(int i = 0; i < word.Length; i++)
            {
                table[j, i] = word[k].ToString();

                if(direction == 1) //up
                {
                    if(j == key-1)
                    {
                        direction = 0;
                        j--;
                        k++;
                    }
                    else
                    {
                        j++;
                        k++;
                    }
                }
                else //down
                {
                    if(j == 0)
                    {
                        direction = 1;
                        j++;
                        k++;
                    }
                    else
                    {
                        j--;
                        k++;
                    }
                }
            }

            return table;
        }

        public string Result(string[,] tab, int row, int columns)
        {
            string[,] array = tab;
            string result = "";

            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    if(array[i, j] != null)
                    {
                        result = result + array[i, j];
                    }
                }
            }

            return result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Word = word.Text;
                int Key = Convert.ToInt32(key.Text);

                string[,] table = CreateTwoDimensionalTable(Key, Word.Length);
                table = wypelnienie(table, Word, Key);

                string result = Result(table, Key, Word.Length);

                result1.Text = result;
            }
            catch(Exception ec)
            {
                string text = "Wprowadzono niewłaściwe dane!\nSprawdź czy:\n- wszystkie pola są wypełnione\n- pole z wartością klucza jest liczbą\n- pole z wartością klucza jest większe od 1";
                MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public string[,] changes(string[,] array, int row, int columns, string text)
        {
            string[,] table = array;
            string word = text;
            int k = 0;

            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    if(table[i, j] != null)
                    {
                        table[i, j] = word[k].ToString();
                        k++;
                    }
                }
            }

            return table;
        }

        public string Result2(string[,] array, int rows, int columns, string text)
        {
            string result = "";
            string word = text;

            int j = 0;
            int direction = 1;

            for(int i = 0; i < text.Length; i++)
            {
                result = result + array[j, i];

                if(direction == 1)
                {
                    if(j == rows-1)
                    {
                        direction = 0;
                        j--;
                    }
                    else
                    {
                        j++;
                    }
                }
                else
                {
                    if(j == 0)
                    {
                        direction = 1;
                        j++;
                    }
                    else
                    {
                        j--;
                    }
                }
            }

            return result;
        }

        private void Decode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = word2.Text;
                int key = Convert.ToInt32(key2.Text);

                string[,] table = CreateTwoDimensionalTable(key, text.Length);
                table = wypelnienie(table, text, key);

                table = changes(table, key, text.Length, text);

                string result = Result2(table, key, text.Length, text);

                result2.Text = result;
            }
            catch(Exception ec)
            {
                string text = "Wprowadzono niewłaściwe dane!\nSprawdź czy:\n- wszystkie pola są wypełnione\n- pole z wartością klucza jest liczbą\n- pole z wartością klucza jest większe od 1";
                MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
