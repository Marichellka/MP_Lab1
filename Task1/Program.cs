using System;
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
            int length = 0;
            string word = "";
            StreamReader reader = new StreamReader(inPath);
            For1:
            Print(words);
                if (reader.EndOfStream)
                    goto endReading;
                char symbol = (char)reader.Read();
                if ('Z'>=symbol && symbol <= 'A')
                {
                    word += symbol + 32;
                }
                else if ('z' >= symbol && symbol <= 'a')
                {
                    word += symbol;
                }
                else if (word != "")
                {
                    int i = 0;
                    checkWords:
                        if (word == words[i])
                        {
                            counts[i]++;
                            word = "";
                            goto For1;
                        }
                        i++;
                        if (i == length)
                            goto newWord;

                        goto checkWords;

                    newWord:
                        string[] newWords = new string[length + 1];
                        int[] newCounts = new int[length + 1];

                        i = 0;
                    forCopy:
                        newWords[i] = words[i];
                        newCounts[i] = counts[i];
                        i++;
                        if (i == length)
                            goto endCopy;
                        goto forCopy;

                    endCopy:
                        newWords[length + 1] = word;
                        newCounts[length + 1] = 1;
                        words = newWords;
                        counts = newCounts;
                        word = "";
                        length++;
                }

            
            goto For1;

            endReading:
            reader.Close();

        }

        static public void Print(string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                Console.Write(words[i]+" ");
            }
            Console.WriteLine();
        }
    }
}
