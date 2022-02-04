using System;
using System.IO;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            string inPath = "input.txt";
            string outPath = "output.txt";
            string[] words = new string[0];
            int[] counts = new int[0];
            int[][] pages = new int[0][];
            int currentPage = 1;
            int length = 0, i, strCount = 0;
            int pageLinesCount = 45;
            string word = "";
            StreamReader reader = new StreamReader(inPath);
            readFile:
            {
                if (reader.EndOfStream)
                    goto endReading;

                string str = reader.ReadLine();
                if (strCount == pageLinesCount)
                {
                    currentPage++;
                    strCount = 0;
                }
                strCount++;

                int j = 0;
                For1:
                {
                    if (j == str.Length)
                        goto endFor;
                    char symbol = str[j];
                    if ('Z' >= symbol && symbol >= 'A')
                    {
                        word += ((char)(symbol + 32));
                        if (j + 1 < str.Length)
                            goto endFor;
                    }
                    else if ('z' >= symbol && symbol >= 'a')
                    {
                        word += symbol;
                        if (j + 1 < str.Length)
                            goto endFor;
                    }

                    if (word != "" && symbol != '-' && symbol != '\'')
                    {
                        i = 0;
                        checkWords:
                        {
                            if (i == length)
                                goto newWord;
                            if (word == words[i])
                            {
                                word = "";
                                if (counts[i] == 100)
                                {
                                    goto endFor;
                                }
                                counts[i]++;
                                if (counts[i] <= pages[i].Length)
                                {
                                    pages[i][counts[i] - 1] = currentPage;
                                }
                                else
                                {
                                    int[] pagesTmp = new int[counts[i] * 2];
                                    int p = 0;
                                    copyPages:
                                    {
                                        pagesTmp[p] = pages[i][p];
                                        p++;
                                        if (p < counts[i] - 1)
                                            goto copyPages;
                                    }
                                    pages[i] = pagesTmp;
                                    pages[i][counts[i] - 1] = currentPage;
                                }
                                goto endFor;
                            }
                            i++;
                            goto checkWords;
                        }

                        newWord:
                        if (length == words.Length)
                        {
                            string[] newWords = new string[(length + 1) * 2];
                            int[] newCounts = new int[(length + 1) * 2];
                            int[][] newPages = new int[(length + 1) * 2][];

                            i = 0;
                            forCopy:
                            {
                                if (i == length)
                                {
                                    words = newWords;
                                    counts = newCounts;
                                    pages = newPages;
                                    goto endCopy;
                                }
                                newWords[i] = words[i];
                                newCounts[i] = counts[i];
                                newPages[i] = pages[i];
                                i++;
                                goto forCopy;
                            }
                        }

                        endCopy:
                        words[length] = word;
                        counts[length] = 1;
                        pages[length] = new int[] { currentPage };
                        length++;
                        word = "";
                    }

                    endFor:
                    j++;
                    if(j < str.Length)
                        goto For1;
                }

                if (!reader.EndOfStream)
                    goto readFile;
            }

            endReading:
            reader.Close();

            //Print(words);
            //sorting
            int curr, k;
            int[] currPages;
            i = 1;
            sort:
            {
                curr = counts[i];
                word = words[i];
                currPages = pages[i];
                k = i - 1;
                whileSort:
                {
                    if (k >= 0)
                    {
                        int symb = 0;
                        compWords:
                        {
                            if (symb == words[k].Length || words[k][symb] < word[symb])
                                goto endWhile;

                            if (symb + 1 < word.Length && words[k][symb] == word[symb])
                            {
                                symb++;
                                goto compWords;
                            }
                        }

                        counts[k + 1] = counts[k];
                        words[k + 1] = words[k];
                        pages[k + 1] = pages[k];
                        k--;
                        goto whileSort;
                    }
                }
                endWhile:
                counts[k + 1] = curr;
                words[k + 1] = word;
                pages[k + 1] = currPages;
                i++;
                if (i < length)
                    goto sort;
            }

            //output
            StreamWriter writer = new StreamWriter(outPath);
            i = 0;
            write:
            {
                if (counts[i] < 100) {
                    writer.Write(words[i] + " - " + pages[i][0]);
                    int j = 1;
                    outPages:
                    {
                        if (j == counts[i])
                            goto endOutPages;
                        if(pages[i][j] != pages[i][j - 1]) 
                            writer.Write(", "+pages[i][j]);
                        j++;
                        goto outPages;
                    }
                    endOutPages:
                    writer.WriteLine();
                }
                i++;
                if (i < length)
                    goto write;
            }

            writer.Close();
        }
    }
}
