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
                    char symbol = str[j];
                    if ('Z' >= symbol && symbol >= 'A')
                    {
                        word += ((char)(symbol + 32)).ToString();
                        if (!reader.EndOfStream)
                            goto For1;
                    }
                    else if ('z' >= symbol && symbol >= 'a')
                    {
                        word += symbol;
                        if (!reader.EndOfStream)
                            goto For1;
                    }

                    if (word != "")
                    {
                        i = 0;
                        checkWords:
                        {
                            if (i == length)
                                goto newWord;
                            if (word == words[i])
                            {
                                counts[i]++;
                                word = "";
                                if (counts[i] <= pages[i].Length)
                                {
                                    pages[i][counts[i] - 1] = currentPage;
                                }
                                else
                                {
                                    int[] pagesTmp = new int[counts[i] * 2];
                                    int k = 0;
                                    copyPages:
                                    {
                                        pagesTmp[k] = pages[i][k];
                                        k++;
                                        if (k < counts[i])
                                            goto copyPages;
                                    }
                                }
                                /*if (j==str.Length-1)???
                                    goto ???;*/

                                goto For1;
                            }
                            i++;
                            goto checkWords;
                        }

                        newWord:
                        if (length == words.Length)
                        {
                            string[] newWords = new string[length * 2];
                            int[] newCounts = new int[length * 2];
                            int[][] newPages = new int[length * 2][];

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
                        length++;
                    }

                    if(j!=str.Length-1)
                        goto For1;
                }
                

                if (!reader.EndOfStream)
                    goto readFile;
            }

            endReading:
            reader.Close();

            //sorting

            //output
            StreamWriter writer = new StreamWriter(outPath);
            i = 0;
            write:
            {
                writer.Write(words[i] + " - " );
                int j = 0;
                outPages:
                {
                    if (j == counts[i] - 1)
                        goto endOutPages;
                    writer.Write(pages[i][j] + ", ");
                    j++;
                    goto outPages;
                }
                endOutPages:
                writer.WriteLine(pages[i][counts[i]-1]);
                i++;
                if (i < length)
                    goto write;
            }

            writer.Close();
        }
    }
}
