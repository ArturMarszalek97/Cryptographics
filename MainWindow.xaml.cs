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

            result21.Text = " ";
            result22.Text = " ";
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

        // TASK 2

        public int[] initKey(int[] tab)
        {
            int[] array = tab;

            array[0] = 3;
            array[1] = 4;
            array[2] = 1;
            array[3] = 5;
            array[4] = 2;

            return array;
        }

        public string[,] addValues(string[,] tab, string text, int rows, int columns)
        {
            string[,] array = tab;
            string word = text;
            int k = 0;
            bool status = false;

            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    array[i, j] = word[k].ToString();
                    k++;

                    if(k == word.Length)
                    {
                        status = true;
                        break;
                    }
                }

                if(status == true)
                {
                    break;
                }
            }

            return array;
        }

        public string ResultTask2(string[,] tab, int[] key, int rows, int columns)
        {
            string result = "";

            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    result = result + tab[i, key[j]-1];
                }
            }

            return result;
        }

        private void Code2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = word21.Text;
                int[] array = new int[5];

                array = initKey(array);

                double row = (double)text.Length / 5;
                int rows = Convert.ToInt32(Math.Ceiling(row));

                string[,] tab = CreateTwoDimensionalTable(rows, 5);

                tab = addValues(tab, text, rows, 5);

                string result = ResultTask2(tab, array, rows, 5);

                result21.Text = result;
            }
            catch(Exception ec)
            {
                string text = "Wprowadzono niewłaściwe dane!\nSprawdź czy:\n- pole jest wypełnione poprawnie\n- pole nie jest puste";
                MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public string Result2Task2(string word, int[] tab, int cyc)
        {
            string result = "";
            string text = word;
            int[] key = tab;
            int cycles = (cyc - 1) * 5;
            int disparity = text.Length - cycles;
            int k = 0;
            string[,] helpArray = new string[cyc, 5];

            int[] disparity4 = new int[4];
            disparity4[0] = 3;
            disparity4[1] = 4;
            disparity4[2] = 1;
            disparity4[3] = 2;

            int[] disparity3 = new int[3];
            disparity3[0] = 3;
            disparity3[1] = 1;
            disparity3[2] = 2;

            int[] disparity2 = new int[2];
            disparity2[0] = 1;
            disparity2[1] = 2;

            for(int i = 0; i < cyc-1; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    helpArray[i, key[j]-1] = text[k].ToString();
                    k++;
                }
            }

            if(disparity == 4)
            {
                    for(int j = 0; j < 4; j++)
                    {
                        helpArray[cyc-1, disparity4[j] - 1] = text[k].ToString();
                        k++;
                    }
            }
            if(disparity == 3)
            {
                    for (int j = 0; j < 3; j++)
                    {
                        helpArray[cyc-1, disparity3[j] - 1] = text[k].ToString();
                        k++;
                    }
            }
            if(disparity == 2)
            {

                    for (int j = 0; j < 2; j++)
                    {
                        helpArray[cyc-1, disparity2[j] - 1] = text[k].ToString();
                        k++;
                    }
            }
            if (disparity == 1)
            {
                helpArray[cyc - 1, disparity2[0] - 1] = text[k].ToString();
            }

            for(int i = 0; i < cyc; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    result = result + helpArray[i, j];
                }
            }

            return result;
        }

        private void Decode2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = word22.Text;
                int[] array = new int[5];

                array = initKey(array);

                double row = (double)text.Length / 5;
                int rows = Convert.ToInt32(Math.Ceiling(row));

                string result = Result2Task2(text, array, rows);

                result22.Text = result;

            }
            catch(Exception ec)
            {
                string text = "Wprowadzono niewłaściwe dane!\nSprawdź czy:\n- pole jest wypełnione poprawnie\n- pole nie jest puste";
                MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
