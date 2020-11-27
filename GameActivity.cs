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
    [Activity(Label = "GameActivity", MainLauncher = true)]

    public class GameActivity : Activity
    {
        //The word being guessed, play button, images, and win/loss info
        TextView tvWord;
        Button btnPlay;
        ImageView imgHangingMan;
        TextView tvWin;
        TextView tvLose;
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
        //counters for the amount of times the user has won or lost the game and set to 0
        int WinCounter = 0;
        int LoseCounter = 0;

        //setting data for the winning or losing toast messages
        private void ToastMessage(int WinLose) { 
            //WinLose is an int that is set to 1 if the user has lost the game or 2 if the user has won the game
            Context context = Application.Context;
            ToastLength duration = ToastLength.Long;
            if (WinLose == 1) //toast message to inform the user that they have lost the game
            {
                string text = "YOU LOSE! To play again, tap on the play button!";
                Toast toast = Toast.MakeText(context, text, duration);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();
            }
            else //toast message to inform the user they have won the game
            {
                string text = "YOU WIN! To play again, tap on the play button!";
                var toast = Toast.MakeText(context, text, duration);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();
            }
        }


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
            tvWin = FindViewById<TextView>(Resource.Id.tvWins);
            tvLose = FindViewById<TextView>(Resource.Id.tvLosses);

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

            //re-enable all the keyboard buttons when play button is pressed
            //top row of the keyboard
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
            //middle row of the keyboard
            btnA.Enabled = true;
            btnS.Enabled = true;
            btnD.Enabled = true;
            btnF.Enabled = true;
            btnG.Enabled = true;
            btnH.Enabled = true;
            btnJ.Enabled = true;
            btnK.Enabled = true;
            btnL.Enabled = true;
            //bottom row of the keyboard
            btnZ.Enabled = true;
            btnX.Enabled = true;
            btnC.Enabled = true;
            btnV.Enabled = true;
            btnB.Enabled = true;
            btnN.Enabled = true;
            btnM.Enabled = true;
        }
        
        private void AllAlphaButton_Click(object sender, EventArgs e) //when any letter on the keyboard is pressed
        {
            Button fakeBtn = (Button)sender;

            //run the method that checks whether or not the guess is correct
            GamePlay(fakeBtn.Text.ToLower());

            //disable the letter that has just been pressed as no letter can be guessed twice
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
                //displaying the "word" as underscores on game start
                tvWord.Text += "_ ";
                //setting the contents of the underscore array to underscores (for display purposes mostly)
                ChosenWordUnderscoreArray[i] = '_';
            }
        }

        private bool GameWon() //method to check if the user has won
        {
            //for loop that runs through the length of both the chosen word array and the underscore array (as they are the same length)
            for (int i = 0; i < ChosenWordArray.Length; i++)
            {
                //check to see if each letter in the arrays is the same
                if (ChosenWordArray[i] != ChosenWordUnderscoreArray[i])
                {
                    //if there are any letters that do not match, that means the underscore array still contains at least one underscore so the whole word has not been guessed yet, therefore the user has not won yet
                    return false;
                }
            }
            //if all of the letters match, the user has won
            return true;
        }

        private void DisableKeyboard()
        {
            //disable all keyboard buttons
            //top row
            btnQ.Enabled = false;
            btnW.Enabled = false;
            btnE.Enabled = false;
            btnR.Enabled = false;
            btnT.Enabled = false;
            btnY.Enabled = false;
            btnU.Enabled = false;
            btnI.Enabled = false;
            btnO.Enabled = false;
            btnP.Enabled = false;
            //middle row
            btnA.Enabled = false;
            btnS.Enabled = false;
            btnD.Enabled = false;
            btnF.Enabled = false;
            btnG.Enabled = false;
            btnH.Enabled = false;
            btnJ.Enabled = false;
            btnK.Enabled = false;
            btnL.Enabled = false;
            //bottom row
            btnZ.Enabled = false;
            btnX.Enabled = false;
            btnC.Enabled = false;
            btnV.Enabled = false;
            btnB.Enabled = false;
            btnN.Enabled = false;
            btnM.Enabled = false;
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
                        //replacing the underscore in the array with the correctly guessed letter
                        ChosenWordUnderscoreArray[i] = Convert.ToChar(Letter);
                    }
                }
                GameWon();
            }
            else //if the pressed letter is not in the chosen word
            {
                //add to the incorrect guess counter
                IncorrectCounter += 1;
                switch (IncorrectCounter)
                {
                    //setting the images due to how many incorrect guesses the user has made
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
                    case 5: //this is the last possible incorrect guess. If the code gets to this point the user has lost the game.
                        imgHangingMan.SetImageResource(Resource.Drawable.hangmanfinish);
                        //update the loss counter and the textview displaying it
                        LoseCounter += 1;
                        tvLose.Text = ("LOSSES: " + LoseCounter);
                        //display toast message informing the user that they have lost the game
                        ToastMessage(1);
                        //disable all keyboard buttons as the game is now over
                        DisableKeyboard();
                        
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
            
            if (GameWon() == true)
            {
                //add to the win counter
                WinCounter += 1;
                //update the textview displaying the score
                tvWin.Text = ("WINS: " + WinCounter);
                //display the toast message informing the user that they have won the game
                ToastMessage(2);

                //disable all keyboard buttons as the game is now over
                DisableKeyboard();
            }

        }
        
    }
}