using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSD03_Hangman
{
    static class Generator
    {
        //word bank -> a string array containing a bunch of possible words which will be randomly selected when starting the game

        private static string[] WordBank()
        {
            string[] Animals = new string[] { "cat", "dog", "pig", "cow", "duck", "horse", "chicken", "monkey", "sheep", "bat", "bear", "donkey", "rabbit", "frog", "fish", "turtle", "lion", "tiger", "zebra", "elephant", "cheetah", "panda", "fox" };
            return Animals;
        }

        private static IEnumerable<string> GetWords(string[] WordBank)
        {
            List<string> ListWords = new List<string>();

            foreach (var word in WordBank)
            {
                ListWords.Add(word.ToLower());
            }

            return ListWords;
        }

        private static int RandomNumber(int UpperLength)
        {
            Random rnd = new Random();

            int RandNum = rnd.Next(1, UpperLength);
            return RandNum;
        }

        public static string GetRandomWord()
        {
            List<string> Words = new List<string>();
            Words.AddRange(GetWords(WordBank()));
            int RandNum = RandomNumber(Words.Count);

            return Words[RandNum];
        }
    }
}