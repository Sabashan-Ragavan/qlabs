using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace QualiaLabs
{
    class Program
    {
        static void Main(string[] args)
        {
            //Various test cases for my implementation 
            string[] test_1 = { "acb", "abc", "bca" };
            string order_1 = "abc";
            string[] sorted_1 = Sort(test_1, order_1);
            foreach (var word in sorted_1)
                Console.Write(word + " ");
            Console.WriteLine(); 

            string[] test_2 = { "acb", "abc", "bca" };
            string order_2 = "cba";
            string[] sorted_2 = Sort(test_2, order_2);
            foreach (var word in sorted_2)
                Console.Write(word + " ");
            Console.WriteLine(); 

            string[] test_3 = { "", "a", "aaa" };
            string order_3 = "a";
            string[] sorted_3 = Sort(test_3, order_3);
            foreach (var word in sorted_3)
                Console.Write(word + " ");
            Console.WriteLine(); 

            string[] test_4 = { "mynameissabashan", "mynameisbehroz", "mynameissean" };
            string order_4 = "abcdefghijklmnopqrstuvwxyz";
            string[] sorted_4 = Sort(test_4, order_4);
            foreach (var word in sorted_4)
                Console.Write(word + " ");
            Console.WriteLine(); 

            string[] test_5 = { "", "ac", "acb", "aba", "abb", "abbbbbbb", "abbbbbb", "ab", "a", "bbb", "bbc", "aab", "ccc", "ccca", "cba" };
            string order_5 = "cba";
            string[] sorted_5 = Sort(test_5, order_5);
            foreach (var word in sorted_5)
                Console.Write(word + " ");
            Console.WriteLine(); 

            string[] test_6 = { "", " ", "  ", "aaaaaaaaaaaaaaaaaaaaaaaaaaaa", "aaaaaaaaaaaaaaaaaaaaaaaaaaba", "bbbbbbbbbbbbbbbbbaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaccccccccccccccccccbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb", "c", "aa", "ab", "ac", "ba", "bb", "bc", "ca", "cb", "cc", "aaa", "aab", "aac", "aba", "abb", "abc", "acc", "bac", "bba", "bab", "bac", "bbc", "bca", "bcb", "bcc", "caa", "cab", "cbb", "cbc", "ccc", "aaaa" };
            string order_6 = "abcdefghijklmnopqrstuvwxyz";
            string[] sorted_6 = Sort(test_6, order_6);
            foreach (var word in sorted_6)
                Console.Write(word + " ");
            Console.WriteLine(); 

            Console.ReadLine();
        }

        //The below function is my implementation for question 2. 
        //
        //:p is the size of the lexicographical order, 
        //:n is the number of strings in the string array,
        //:m is the size of the strings
        //
        //Run-Time: F(n) = p+n*(m+logn)= O(n*logn) or O(n*m) depending if logn > m or m > logn and noting that p can be at maximum, 
        //the size of the alphabet, and thus essentially a constant, 
        //
        public static String[] Sort(String[] un_sorted, string order)
        {
            Dictionary<string, int> priority = new Dictionary<string, int>();
            BST string_score = new BST();

            //iterate through every charcter in the user-specified lexicographical ordering 
            //assign a priority to each character corresponding to their index in the string+1
            priority.Add(" ", 0); 
            for (int i = 0; i < order.Length; i++) //O(p)
            {
                priority.Add(order[i].ToString(), i + 1);
            }

            for (int i = 0; i < un_sorted.Length; i++) //O(n)
            {
                string word = un_sorted[i];
                if (word != null) 
                {
                    double score = -1.0;
                    if (word.Length > 0)
                    {
                        score = 0.0;
                        //Calculate a score similar to calculting the value of a base N character array
                        //In this case, our N would be order.Length + 1 
                        for (int j = 0; j < word.Length; j++) //O(m)
                        {
                            if (priority.ContainsKey(word[j].ToString()))
                                score += priority[word[j].ToString()] * (double)Math.Pow(order.Length + 1, order.Length - j); 
                            else
                                throw new Exception();
                        }
                    }
                    //insert each score with their corresponding string as a key,value pair into a BST. This ensures the strings are sorted based on their score form lowest to greatest, 
                    //when an in-order traveral is performed
                    string_score.insert(score, word); //O(logn)
                }
            }
            return string_score.inOrderTraversal().ToArray();
        }
    }
}
