﻿using Android.App;
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
    [Activity(Label = "GameActivity", MainLauncher = true)]

    public class GameActivity : Activity
    {
        //The word being guessed and the play button
        TextView tvWord;
        Button btnPlay;
        ImageView imgHangingMan;
        //First row of the keyboard
        Button btnQ;
        Button btnW;
        Button btnE;
        Button btnR;
        Button btnT;
        Button btnY;
        Button btnU;
        Button btnI;
        Button btnO;
        Button btnP;
        //Second row of the keyboard
        Button btnA;
        Button btnS;
        Button btnD;
        Button btnF;
        Button btnG;
        Button btnH;
        Button btnJ;
        Button btnK;
        Button btnL;
        //Third row of the keyboard
        Button btnZ;
        Button btnX;
        Button btnC;
        Button btnV;
        Button btnB;
        Button btnN;
        Button btnM;

        //setting up the char arrays for guessing the word + displaying word/underscores
        char[] ChosenWordArray;
        char[] ChosenWordUnderscoreArray;
        string ChosenWord;
        //a counter that is increased every time the user guesses an incorrect letter
        int IncorrectCounter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GamePlay);

            //calling the alphabet buttons method
            AlphabetButtons();
        }
        #region Button Bindings
        private void AlphabetButtons()
        {
            //Button bindings
            btnPlay = FindViewById<Button>(Resource.Id.btnPlay);
            tvWord = FindViewById<TextView>(Resource.Id.tvWord);
            imgHangingMan = FindViewById<ImageView>(Resource.Id.imgHangingMan);

            btnPlay.Click += BtnPlay_Click;
            //First row of the keyboard - Ids
            btnQ = FindViewById<Button>(Resource.Id.btnQ);
            btnW = FindViewById<Button>(Resource.Id.btnW);
            btnE = FindViewById<Button>(Resource.Id.btnE);
            btnR = FindViewById<Button>(Resource.Id.btnR);
            btnT = FindViewById<Button>(Resource.Id.btnT);
            btnY = FindViewById<Button>(Resource.Id.btnY);
            btnU = FindViewById<Button>(Resource.Id.btnU);
            btnI = FindViewById<Button>(Resource.Id.btnI);
            btnO = FindViewById<Button>(Resource.Id.btnO);
            btnP = FindViewById<Button>(Resource.Id.btnP);
            //First row of the keyboard - click events
            btnQ.Click += AllAlphaButton_Click;
            btnW.Click += AllAlphaButton_Click;
            btnE.Click += AllAlphaButton_Click;
            btnR.Click += AllAlphaButton_Click;
            btnT.Click += AllAlphaButton_Click;
            btnY.Click += AllAlphaButton_Click;
            btnU.Click += AllAlphaButton_Click;
            btnI.Click += AllAlphaButton_Click;
            btnO.Click += AllAlphaButton_Click;
            btnP.Click += AllAlphaButton_Click;

            //Second row of the keyboard Ids
            btnA = FindViewById<Button>(Resource.Id.btnA);
            btnS = FindViewById<Button>(Resource.Id.btnS);
            btnD = FindViewById<Button>(Resource.Id.btnD);
            btnF = FindViewById<Button>(Resource.Id.btnF);
            btnG = FindViewById<Button>(Resource.Id.btnG);
            btnH = FindViewById<Button>(Resource.Id.btnH);
            btnJ = FindViewById<Button>(Resource.Id.btnJ);
            btnK = FindViewById<Button>(Resource.Id.btnK);
            btnL = FindViewById<Button>(Resource.Id.btnL);
            //Second row of the keyboard - click events
            btnA.Click += AllAlphaButton_Click;
            btnS.Click += AllAlphaButton_Click;
            btnD.Click += AllAlphaButton_Click;
            btnF.Click += AllAlphaButton_Click;
            btnG.Click += AllAlphaButton_Click;
            btnH.Click += AllAlphaButton_Click;
            btnJ.Click += AllAlphaButton_Click;
            btnK.Click += AllAlphaButton_Click;
            btnL.Click += AllAlphaButton_Click;

            //Third row of the keyboard - Ids
            btnZ = FindViewById<Button>(Resource.Id.btnZ);
            btnX = FindViewById<Button>(Resource.Id.btnX);
            btnC = FindViewById<Button>(Resource.Id.btnC);
            btnV = FindViewById<Button>(Resource.Id.btnV);
            btnB = FindViewById<Button>(Resource.Id.btnB);
            btnN = FindViewById<Button>(Resource.Id.btnN);
            btnM = FindViewById<Button>(Resource.Id.btnM);
            //Third row fo the keyboard - click events
            btnZ.Click += AllAlphaButton_Click;
            btnX.Click += AllAlphaButton_Click;
            btnC.Click += AllAlphaButton_Click;
            btnV.Click += AllAlphaButton_Click;
            btnB.Click += AllAlphaButton_Click;
            btnN.Click += AllAlphaButton_Click;
            btnM.Click += AllAlphaButton_Click;
        }
#endregion
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            //run the method that chooses and loads the word when play button is pressed
            LoadWord();

            imgHangingMan.SetImageResource(Resource.Drawable.hangmanstart);
            //set the counter for the times an incorrect letter is guessed to 0
            IncorrectCounter = 0;

            //re-enable all the buttons when play button is pressed
            //top row
            btnQ.Enabled = true;
            btnW.Enabled = true;
            btnE.Enabled = true;
            btnR.Enabled = true;
            btnT.Enabled = true;
            btnY.Enabled = true;
            btnU.Enabled = true;
            btnI.Enabled = true;
            btnO.Enabled = true;
            btnP.Enabled = true;
            //middle row
            btnA.Enabled = true;
            btnS.Enabled = true;
            btnD.Enabled = true;
            btnF.Enabled = true;
            btnG.Enabled = true;
            btnH.Enabled = true;
            btnJ.Enabled = true;
            btnK.Enabled = true;
            btnL.Enabled = true;
            //bottom row
            btnZ.Enabled = true;
            btnX.Enabled = true;
            btnC.Enabled = true;
            btnV.Enabled = true;
            btnB.Enabled = true;
            btnN.Enabled = true;
            btnM.Enabled = true;
        }
        
        private void AllAlphaButton_Click(object sender, EventArgs e)
        {
            Button fakeBtn = (Button)sender;

            GamePlay(fakeBtn.Text.ToLower());

            fakeBtn.Enabled = false;
        }

        private void LoadWord()
        {
            // load the word from the generator class, break the word into an array, create an array of equal length filled with underscores

            tvWord.Text = string.Empty; //empty the text view

            ChosenWord = Generator.GetRandomWord(); //get the random word from the generator class

            //break the word into letter and put it into an array
            ChosenWordArray = ChosenWord.ToCharArray();
            ChosenWordUnderscoreArray = new char[ChosenWordArray.Length];
            for (int i = 0; i < ChosenWordUnderscoreArray.Length; i++)
            {
                tvWord.Text += "_ ";
                ChosenWordUnderscoreArray[i] = '_';
            }
        }

        private void GamePlay(string Letter)
        {
            //letter -> the keyboard button that has been pressed
            if (ChosenWord.Contains(Letter)) //if the pressed letter is in the chosen word
            {
                for (int i = 0; i < ChosenWordArray.Length; i++)
                {
                    if (Letter == ChosenWordArray[i].ToString())
                    {
                        ChosenWordUnderscoreArray[i] = Convert.ToChar(Letter);
                    }
                }
            }
            else //if the pressed letter is not in the chosen word
            {
                IncorrectCounter += 1;
                switch (IncorrectCounter)
                {
                    case 0:
                        imgHangingMan.SetImageResource(Resource.Drawable.hangmanstart);
                        return;
                    case 1:
                        imgHangingMan.SetImageResource(Resource.Drawable.hangmanhead);
                        return;
                    case 2:
                        imgHangingMan.SetImageResource(Resource.Drawable.hangmanbody);
                        return;
                    case 3:
                        imgHangingMan.SetImageResource(Resource.Drawable.hangmanleft);
                        return;
                    case 4:
                        imgHangingMan.SetImageResource(Resource.Drawable.hangmanright);
                        return;
                    case 5:
                        imgHangingMan.SetImageResource(Resource.Drawable.hangmanfinish);
                        return;
                    default:
                        break;
                }
                return; //move on to the next letter
            }

            tvWord.Text = string.Empty;

            foreach (var letter in ChosenWordUnderscoreArray)
            {
                tvWord.Text += (letter + " "); //showing the word on the screen with as many correct letter as have been guessed, or underscores for unguessed letters
            }
        }
        
    }
}