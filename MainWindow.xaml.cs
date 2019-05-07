using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Threading;

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
            Init();
        }
        #region zad 1
        public void Init()
        {
            result1.Text = " ";
            result2.Text = " ";

            result21.Text = " ";
            result22.Text = " ";


            randomv.Text = " ";

            result31.Text = " ";
            result32.Text = " ";

            result2c.Text = " ";
            result32c.Text = " ";

            cryptogram.Text = " ";
            cryptogramBack.Text = " ";

            cryptogramV.Text = " ";
            cryptogramVBack.Text = " ";

        }

        public string[,] CreateTwoDimensionalTable(int rows, int columns)
        {
            string[,] table = new string[rows, columns];

            return table;
        }
        public string[,] Wypelnienie(string[,] tab, string content, int key)
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
                table = Wypelnienie(table, Word, Key);

                string result = Result(table, Key, Word.Length);

                result1.Text = result;
            }
            catch(Exception ec)
            {
                string text = "Wprowadzono niewłaściwe dane!\nSprawdź czy:\n- wszystkie pola są wypełnione\n- pole z wartością klucza jest liczbą\n- pole z wartością klucza jest większe od 1";
                MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        public string[,] Changes(string[,] array, int row, int columns, string text)
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
                table = Wypelnienie(table, text, key);

                table = Changes(table, key, text.Length, text);

                string result = Result2(table, key, text.Length, text);

                result2.Text = result;
            }
            catch(Exception ec)
            {
                string text = "Wprowadzono niewłaściwe dane!\nSprawdź czy:\n- wszystkie pola są wypełnione\n- pole z wartością klucza jest liczbą\n- pole z wartością klucza jest większe od 1";
                MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
        #region zad 2
        // TASK 2

        public int[] InitKey(int[] tab)
        {
            int[] array = tab;

            array[0] = 3;
            array[1] = 4;
            array[2] = 1;
            array[3] = 5;
            array[4] = 2;

            return array;
        }
        public string[,] AddValues(string[,] tab, string text, int rows, int columns)
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

                array = InitKey(array);

                double row = (double)text.Length / 5;
                int rows = Convert.ToInt32(Math.Ceiling(row));

                string[,] tab = CreateTwoDimensionalTable(rows, 5);

                tab = AddValues(tab, text, rows, 5);

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

                array = InitKey(array);

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
        #endregion
        #region zad 3 
        // TASK 3 2b

        private void Code31_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string KEY = key31.Text;
                string text = word31.Text;

                char[] Chartext = KEY.Distinct().ToArray();
                string pomoc = "";
                Array.Sort(Chartext);

                for (int i = 0; i < Chartext.Length; i++)
                {
                    pomoc = pomoc + Chartext[i];
                }

                int[] keyTable = new int[KEY.Length];
                int k = 1;

                for (int i = 0; i < pomoc.Length; i++)
                {
                    for (int j = 0; j < KEY.Length; j++)
                    {
                        if (pomoc[i] == KEY[j])
                        {
                            keyTable[j] = k;
                            k++;
                        }
                    }
                }

                double row = (double)text.Length / KEY.Length;
                int rows = Convert.ToInt32(Math.Ceiling(row));

                string[,] array = CreateTwoDimensionalTable(rows, KEY.Length);

                array = AddValues(array, text, rows, KEY.Length);

                string result = "";

                for (int i = 0; i < KEY.Length; i++)
                {
                    for (int j = 0; j < keyTable.Length; j++)
                    {
                        if (i == keyTable[j] - 1)
                        {
                            for (int l = 0; l < rows; l++)
                            {
                                result = result + array[l, j];
                            }
                        }
                    }
                }

                result31.Text = result;
            }
            catch
            {

            }

        }

        private void Decode32_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = word32.Text;
                string key = key32.Text;

                double row = (double)text.Length / key.Length;
                int number_of_rows = Convert.ToInt32(Math.Ceiling(row));

                int[] keyTable = new int[key.Length];

                char[] Chartext = key.Distinct().ToArray();
                string pomoc = "";
                Array.Sort(Chartext);

                for (int i = 0; i < Chartext.Length; i++)
                {
                    pomoc = pomoc + Chartext[i];
                }

                int k = 1;

                for (int i = 0; i < pomoc.Length; i++)
                {
                    for (int j = 0; j < key.Length; j++)
                    {
                        if (pomoc[i] == key[j])
                        {
                            keyTable[j] = k;
                            k++;
                        }
                    }
                }

                string[,] array = CreateTwoDimensionalTable(number_of_rows, key.Length);

                int disparity = text.Length - (number_of_rows - 1) * key.Length;

                int[] number_of_letters_in_column = new int[key.Length];

                for (int i = 0; i < key.Length; i++)
                {
                    if (i < disparity || disparity == 0)
                    {
                        number_of_letters_in_column[i] = number_of_rows;
                    }
                    else
                    {
                        number_of_letters_in_column[i] = number_of_rows - 1;
                    }
                }

                // tu zaczyna sie test
                int group = 1;
                int position_in_column = 0;
                int z = 0;

                while(z < text.Length) // przechodzimy przez tekst
                {
                    for(int j = 0; j < keyTable.Length; j++) // sprawdzamy w ktorym miejscu jest grupa w kolumnie
                    {
                        if(keyTable[j] == group)
                        {
                            position_in_column = j;
                            group++;
                            break;
                        }
                    }

                    for(int l = 0; l < number_of_letters_in_column[position_in_column]; l++)
                    {
                        array[l, position_in_column] = text[z].ToString();
                        z++;
                    }
                }

                string result = "";

                for(int i = 0; i < number_of_rows; i++)
                {
                    for(int j = 0; j < keyTable.Length; j++)
                    {
                        result = result + array[i, j];
                    }
                }

                result32.Text = result;
            }
            catch
            {

            }

        }

        //Task 3 2c

        private void Code2c_Click(object sender, RoutedEventArgs e)
        {
            string text = word2c.Text;
            string key = key2c.Text;

            int column_and_row = key.Length;

            int[] keyTable = new int[key.Length];

            char[] Chartext = key.Distinct().ToArray();
            string pomoc = "";
            Array.Sort(Chartext);

            for (int i = 0; i < Chartext.Length; i++)
            {
                pomoc = pomoc + Chartext[i];
            }

            int k = 1;

            for (int i = 0; i < pomoc.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (pomoc[i] == key[j])
                    {
                        keyTable[j] = k;
                        k++;
                    }
                }
            }

            string[,] array = CreateTwoDimensionalTable(column_and_row, column_and_row);

            int z = 1;
            int index = 0;
            int t = 0;
            bool end = false;
            string result = "";
            int position = 1;
            int index2 = 0;

            for (int i = 0; i < column_and_row; i++)
            {
                for(int j = 0; j < keyTable.Length; j++) // szukamy indeksu
                {
                    if(keyTable[j] == z)
                    {
                        index = j;
                        z++;
                        break;
                    }
                }

                for(int j = 0; j <= index; j++)
                {
                    if(t >= text.Length)
                    {
                        end = true;
                        break;
                    }
                    else
                    {
                        array[i, j] = text[t].ToString();
                        t++;
                    }
                }

                if(end == true)
                {
                    break;
                }
            }

            for(int i = 0; i < column_and_row; i++)
            {
                for(int j = 0; j < keyTable.Length; j++) //szukamy indeksu
                {
                    if (keyTable[j] == position)
                    {
                        index2 = j;
                    }
                }

                for(int j = 0; j < column_and_row; j++)
                {
                    result = result + array[j, index2];
                }

                position++;
            }

            result2c.Text = result;

        }

        private void Decode2c_Click(object sender, RoutedEventArgs e)
        {
            string text = word32c.Text;
            string key = key32c.Text;

            int column_and_row = key.Length;

            int[] keyTable = new int[key.Length];

            char[] Chartext = key.Distinct().ToArray();
            string pomoc = "";
            Array.Sort(Chartext);

            for (int i = 0; i < Chartext.Length; i++)
            {
                pomoc = pomoc + Chartext[i];
            }

            int k = 1;

            for (int i = 0; i < pomoc.Length; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (pomoc[i] == key[j])
                    {
                        keyTable[j] = k;
                        k++;
                    }
                }
            }

            int[] number_of_letters_in_each_row = new int[key.Length];
            int number_of_rows = 0;
            int z = 1;
            int sum = 0;
            bool end_text = false;

            for(int i = 0; i < key.Length; i++) // uzupełniamy nową tablicę
            {
                for(int j = 0; j < key.Length; j++) // szukamy indexu
                {
                    if(keyTable[j] == z)
                    {
                        number_of_letters_in_each_row[i] = j + 1;
                        sum = sum + j + 1;
                        z++;
                        break;
                    }

                    if(sum >= text.Length)
                    {
                        end_text = true;
                        break;
                    }
                }

                if(end_text == true)
                {
                    number_of_rows = i + 1;
                    break;
                }
            }

            int licz = 0;
            bool en = false;

            string[,] array = CreateTwoDimensionalTable(number_of_rows, column_and_row);

            for (int i = 0; i < column_and_row; i++)
            {
                for(int j = 0; j < number_of_letters_in_each_row[i]; j++)
                {
                    array[i, j] = "a";
                    licz++;

                    if(licz == text.Length)
                    {
                        en = true;
                        break;
                    }
                }

                if(en == true)
                {
                    break;
                }
            }

            int[] number_of_letters_in_each_column = new int[key.Length];

            int counter = 0;
            z = 1;
            int index = 0;

            for(int i = 0; i < key.Length; i++)
            {
                counter = 0;

                for(int j = 0; j < key.Length; j++) //szukamy indexu
                {
                    if(keyTable[j] == z)
                    {
                        index = j;
                        z++;
                        break;
                    }
                }

                for(int j = 0; j < number_of_rows; j++)
                {
                    if(array[j, index] == "a")
                    {
                        counter++;
                    }
                }

                number_of_letters_in_each_column[i] = counter;
            }

            z = 1;
            index = 0;
            int m = 0;

            for(int i = 0; i < key.Length; i++)
            {
                for(int j = 0; j < key.Length; j++) //szukamy indexu
                {
                    if(keyTable[j] == z)
                    {
                        index = j;
                        z++;
                        break;
                    }
                }

                for(int j = 0; j < number_of_rows; j++)
                {
                    if(array[j, index] == "a")
                    {
                        array[j, index] = text[m].ToString();
                        //MessageBox.Show("wiersz: " + j + " kolumna: " + index + " litera: " + array[j, index]);
                        m++;
                    }
                }
            }

            string result = "";

            for(int i = 0; i < number_of_rows; i++)
            {
                for(int j = 0; j < key.Length; j++)
                {
                    if(array[i, j] != null)
                    {
                        result = result + array[i, j];
                    }
                }
            }

            result32c.Text = result;
        }
        #endregion
        #region zad 4

        //Task 4 

        private int K1, K0, N = 21;
        //fi=12;
        char[] alphabet = new char[21];
        static bool IsPrime(int n)
        {
            if (n > 1)
            {
                return Enumerable.Range(1, n).Where(x => n % x == 0)
                                 .SequenceEqual(new[] { 1, n });
            }

            return false;
        }
        
        private void K1_TextChanged(object sender, TextChangedEventArgs e)
        {
            int model;
            if (k1.Text == "")
            {
                model = 0;
            }
            else
            {
                model = int.Parse(k1.Text);
            }

            if(IsPrime(model))
             {
                K1 = model;               
            }
            else
            {
                k1.Clear();
                string text = "K1 musi być liczbą pierwszą!";
                MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
        }
        private void K0_TextChanged(object sender, TextChangedEventArgs e)
        {
            int model;
            if (k0.Text == "")
            {
                model = 0;
            }
            else
            {
                model = int.Parse(k0.Text);
            }

            if (IsPrime(model))
            {
                K0 = model;
            }
            else
            {
                k0.Clear();
                string text = "K2 musi być liczbą pierwszą!";
                MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void K1Back_TextChanged(object sender, TextChangedEventArgs e)
        {
            int model;
            if (k1Back.Text == "")
            {
                model = 0;
            }
            else
            {
                model = int.Parse(k1Back.Text);
            }

            if (IsPrime(model))
            {
                K1 = model;
            }
            else
            {
                k1Back.Clear();
                string text = "K1 musi być liczbą pierwszą!";
                MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void K0Back_TextChanged(object sender, TextChangedEventArgs e)
        {
            int model;
            if (k0Back.Text == "")
            {
                model = 0;
            }
            else
            {
                model = int.Parse(k0Back.Text);
            }

            if (IsPrime(model))
            {
                K0 = model;
            }
            else

            {
                k0Back.Clear();
                string text = "K2 musi być liczbą pierwszą!";
                MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CaesarInit()
        {
            char letter = 'A';
            for (int i = 0; i < N; i++)
            {
                alphabet[i] = letter++;              
            }
          
        }
        private char FindLetterInAlphabet(char letter)
        {
            CaesarInit();
            for (int i = 0; i < N; i++)
            {
                if (letter == alphabet[i])
                {
                    int j = i * K1 + K0;
                    int r = j % N;

                    return alphabet[r];
                }              
            }
            return '.';
        }
        private char FindLetterInAlphabetBack(char letter)
        {
            CaesarInit();
            for (int i = 0; i < N; i++)
            {
                if (letter == alphabet[i])
                {
                    /* int p = (int)Math.Pow(K1, fi - 1);
                     int j = (i + (N - K0)) * p;*/
                    int j = (i - K0) / K1;
                    int r = j % N;

                    return alphabet[r];
                }
            }
            return '.';
        }

        private void CaesarCodeClick(Object sender, RoutedEventArgs e )
        {
            char[] plainTextArray =  plaintext.Text.ToArray();
            char[] cryptogramArray = new char[plainTextArray.Length];
            for (int i=0; i <plainTextArray.Length; i++)
            {
                cryptogramArray[i] = FindLetterInAlphabet(plainTextArray[i]);
            }
            string result = new string(cryptogramArray);
            cryptogram.Text = result;
        }        
        private void CaesarDecodeClick(Object sender, RoutedEventArgs e)
        {
            char[] plainTextArray = plaintextBack.Text.ToArray();
            char[] cryptogramArray = new char[plainTextArray.Length];
            for (int i = 0; i < plainTextArray.Length; i++)
            {
                cryptogramArray[i] = FindLetterInAlphabetBack(plainTextArray[i]);
            }
            string result = new string(cryptogramArray);
            cryptogramBack.Text = result;
        }
        #endregion
        #region zad 5
        //TASK 5
        private void VigenereInit(Object sender, RoutedEventArgs e)
        {
            string k = keyV.Text.ToString();
            string t = plaintextV.Text.ToString();
            cryptogramV.Text = Vigenere(t, k);

        }
        private string Vigenere(string Text, string Key)
        {
            int x;
            int y;
            string NewKey = string.Empty;
            string Results = string.Empty;
            string AvailableCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int [,] znak = new int[26, 26];

            NewKey = Key;
            if (Text.Length > Key.Length)
            {
                for (int i = 0; i < Text.Length / Key.Length; i++)
                {
                    NewKey += Key;
                }
            }
            Key = NewKey;

            for (int i = 0; i < Text.Length; i++)
            {

                x = AvailableCharacters.IndexOf(Text[i]);
                y = AvailableCharacters.IndexOf(Key[i]);
                
                znak[x, y] = ((0 + x) + y) % 26;

                Results += AvailableCharacters[znak[x, y]];
              
            }

            return Results;
        }

        private void VigenereInitBack(Object sender, RoutedEventArgs e)
        {
            string k = keyVBack.Text.ToString();
            string t = plaintextBackV.Text.ToString();
            cryptogramVBack.Text = VigenereBack(t, k);

        }
        private string VigenereBack(string Text, string Key)
        {
            int z;
            int k;
            string NewKey = string.Empty;
            string Results = string.Empty;
            string AvailableCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int t;
            int[,] tekst = new int[26, 26];

            NewKey = Key;
            if (Text.Length > Key.Length)
            {
                for (int i = 0; i < Text.Length / Key.Length; i++)
                {
                    NewKey += Key;
                }
            }
            Key = NewKey;

            for (int i = 0; i < Text.Length; i++)
            {
                z = AvailableCharacters.IndexOf(Text[i]);
                k = AvailableCharacters.IndexOf(Key[i]);

                t = (z-k);
                if(t < 0)
                {
                   t = 26 + t;
                }
                Results += AvailableCharacters[t];
            }
            return Results;
        }




        // LFSR generator

        bool status = true;
        List<int> list = new List<int>();

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string data = powers.Text;

                string[] data_without_commas = data.Split(',');

                // find max value

                int grain = int.Parse(data_without_commas[0]);
                int k = 0;

                int[] data_in_int = new int[data_without_commas.Length];

                foreach (string o in data_without_commas)
                {
                    data_in_int[k] = int.Parse(o);
                    k++;

                    if (int.Parse(o) > grain)
                    {
                        grain = int.Parse(o);
                    }
                }

                // new array with random values

                string random_values = "";

                int[] array = new int[grain];

                Random random = new Random();

                for (int i = 0; i < grain; i++)
                {
                    array[i] = random.Next(0, 2);
                    random_values = random_values + array[i];
                }

                // show values in array

                randomv.Text = "Wylosowany ciąg: " + random_values;

                // shifts

                int[] shifts = new int[grain];
                shifts = array;
                int sum = 0;
                int result;
                int[] help_array = new int[grain - 1];

                double period = Math.Pow(2, grain) - 1;
                double counter = 0;
                bool start_loop = true;
                int p = 0;

                while (p < 50)
                {
                    p++;

                    if (counter % period == 0)
                    {
                        counter = 0;
                        shifts = array;
                    }

                    counter++;
                    sum = 0;

                    for (int i = 0; i < data_without_commas.Length; i++)
                    {
                        sum = sum + shifts[data_in_int[i] - 1];
                    }

                    result = sum % 2;

                    list.Add(result);

                    for (int i = 0; i < grain - 1; i++)
                    {
                        help_array[i] = shifts[i];
                    }

                    shifts[0] = result;

                    for (int i = 1; i < grain; i++)
                    {
                        shifts[i] = help_array[i - 1];
                    }
                }
            }
            catch
            {
                MessageBox.Show("Wprowadzono niewłaściwe dane!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            status = false;
            string result = " ";

            foreach(int o in list)
            {
                result = result + o.ToString();
            }

            MessageBox.Show(result, "Wynik", MessageBoxButton.OK, MessageBoxImage.Information);

            list.Clear();
        }

        #endregion

        #region CipherText
        // CipherText

        //każdy string to jeden znak 
        public string[] binaryFile;
        public int[] power = new int[4];
        public int[] resultTableCiphertext;
        int[] STableResult = new int[4];
        public string plainTextb;

        private byte XOR (byte p , byte q)
        {
            if (p == 0)
            {
                if(q == 0) { return 0; }
                else { return 1; }
            }
            else 
            {
                if (q == 0) { return 1; }
                else  { return 0; }
            }
        }

        private void GenerateFunction(object sender, RoutedEventArgs e)
        {
            wielomian.Text = null;
            
            Random rand = new Random();
            string textY;
            for (int i = 0; i < 4; i++)
            {
                power[i] = rand.Next(0, 2);
                if (i == 3)
                {
                    textY = power[i] + "x^" + i;
                    wielomian.Text += textY;
                }
                else
                {
                    textY = power[i] + "x^" + i + " + ";
                    wielomian.Text += textY;
                }
            }
        }

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            byte[] bytes;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    // Create a byte array of file stream length
                    bytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                    //Read block of bytes from stream into the byte array
                    fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));
                    //Close the File Stream
                    fs.Close();
                }
                binaryFile = new string[bytes.Length];
                for (int i = 0; i < bytes.Length; i++)
                {
                    int b = bytes[i];
                    binaryFile[i] = Convert.ToString(b, 2);
                }

                string plaintext = string.Join("", binaryFile);
                plainTextb = plaintext;
                plainTextCh.Text = "Plain text: " + plaintext;
            }
        }

        private void Ciphertext(object sender, RoutedEventArgs e)
        {          
            int resultSizeModel = 0;            
            int[] STable = new int[4];
            int index = 0;

            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                STable[i] = rand.Next(0, 2);
                STableResult[i] = STable[i];

            }

            s0.Text = "S0: " + STable[0].ToString();
            s1.Text = "S1: " + STable[1].ToString();
            s2.Text = "S2: " + STable[2].ToString();
            s3.Text = "S3: " + STable[3].ToString();

            for (int i = 0; i < binaryFile.Length; i++)
            {
                int modelSize = binaryFile[i].Length;
                resultSizeModel += modelSize; 
            }

            int[] resultTable = new int[resultSizeModel];
            
            
            //operacja 1
            byte op1 = XOR(Convert.ToByte(STable[0]), Convert.ToByte(STable[3]));
           
            for (int i = 0; i < binaryFile.Length; i++)
            {
                string model = binaryFile[i];
                int modelSize = binaryFile[i].Length;
                int[] modelTable = new int[resultSizeModel];
                int[] helpTable = new int[modelSize];


                for (int j = 0; j < modelSize; j++)
                {
                    char[] tab = model.ToArray();
                    for(int k = 0; k < tab.Length; k++)
                    {
                        if(tab[k] =='1') { modelTable[k] = 1; }
                        else { modelTable[k] = 0; }
                    }


                    byte c = Convert.ToByte(modelTable[j]);
                    //operacja 2
                    byte op2 = XOR(c, op1);

                    //operacja 3                    
                    STable[3] = STable[2];
                    STable[2] = STable[1];
                    STable[1] = STable[0];
                    STable[0] = op2;
                          
                    helpTable[j] = STable[3]; 
                }

             
                if (i == 0)
                {
                    for(int j = 0; j < helpTable.Length; j++)
                    {
                        index = j;
                        resultTable[j] = helpTable[j]; 
                    }
                }               
                else
                {
                    for (long j = 0; j < helpTable.Length; j++)
                    {                       
                        resultTable[index +1] = helpTable[j];                        
                        index++;
                    }

                }
                string result = string.Join(" ", resultTable);
                ciphertext.Text = "Wynik: " + result;

                resultTableCiphertext = new int[resultTable.Length];
                Array.Copy(resultTable, resultTableCiphertext,resultTable.Length);
                string plaintTextback = string.Join(" ", resultTableCiphertext);
                plainTextChBack.Text = "Plain Text back: " + plaintTextback;
            }


        }

        private void CipherTextBack(object sender, RoutedEventArgs e)
        {                       
           int[] resultTable = new int[plainTextb.Length];

            //operacja 1
            byte op1 = XOR(Convert.ToByte(STableResult[0]), Convert.ToByte(STableResult[3]));
            for (int i = 0; i < resultTableCiphertext.Length; i++)
            { 
                
                byte c = Convert.ToByte(resultTableCiphertext[i]);
                //operacja 2
                byte op2 = XOR(c, op1);

                //operacja 3                    
                STableResult[3] = STableResult[2];
                STableResult[2] = STableResult[1];
                STableResult[1] = STableResult[0];
                STableResult[0] = op2;

                resultTable[i] = STableResult[3];
            }
            
            string result = string.Join(" ", plainTextb);
            ciphertextBack.Text = "Wynik: " + result;
            }
        #endregion


    }
}
