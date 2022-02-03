﻿using System;
using System.IO;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] stopWords = new string[] { "the","in","a","an","for","to","on",
                "onto","at","with","about","before", "of"};
            string inPath = "input.txt";
            string outPath = "output.txt";
            string[] words = new string[0];
            int[] counts = new int[0];
            int length = 0, i;
            int maxWordsCount = 25;
            string word = "";
            StreamReader reader = new StreamReader(inPath);
            For1:
            {
                if (reader.EndOfStream)
                    goto endReading;
                char symbol = (char)reader.Read();
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
                            if (reader.EndOfStream)
                                goto endReading;
                            goto For1;
                        }
                        i++;
                        goto checkWords;
                    }

                    newWord:
                    string[] newWords = new string[length + 1];
                    int[] newCounts = new int[length + 1];

                    i = 0;
                    forCopy:
                    {
                        if (i == length)
                            goto endCopy;
                        newWords[i] = words[i];
                        newCounts[i] = counts[i];
                        i++;
                        goto forCopy;
                    }
                    
                    endCopy:
                    newWords[length] = word;
                    newCounts[length] = 1;
                    words = newWords;
                    counts = newCounts;
                    word = "";
                    length++;
                }

                if(!reader.EndOfStream)
                    goto For1;
            }

            endReading:
            reader.Close();

            Print(words);

            Print(counts);
            //sorting
            int  curr, k;
            i = 1;
            sort:
            {
                curr = counts[i];
                word = words[i];
                k = i - 1;
                whileSort:
                {
                    if (k >= 0 && counts[k] < curr)
                    {
                        counts[k + 1] = counts[k];
                        words[k + 1] = words[k];
                        k--;
                        goto whileSort;
                    }
                }
                
                counts[k + 1] = curr;
                words[k + 1] = word;
                i++;
                if (i < length)
                    goto sort;
            }

            Print(words);
            Print(counts);
            //output
            StreamWriter writer = new StreamWriter(outPath);
            i = 0;
            write:
            {
                writer.WriteLine(words[i] + " - " + counts[i]);
                i++;
                if (i <= maxWordsCount && i < length)
                    goto write;
            }

            writer.Close();
        }

        static public void Print(string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                Console.Write(words[i]+" ");
            }
            Console.WriteLine();
        }
        static public void Print(int[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                Console.Write(words[i] + " ");
            }
            Console.WriteLine();
        }
    }
}