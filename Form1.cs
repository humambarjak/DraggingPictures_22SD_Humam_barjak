using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraggingPictures_22SD_Humam_barjak
{
    public partial class Form1 : Form
    {
        PictureBox pcbForm = null;
        PictureBox pcbTo = null;

        Fighter fighterOne = null;
        Fighter fighterTwo = null;
        Fighter fighterThree = null;

        int correctAnswerCount = 0;
        Random randomGenerator = new Random();
        public Form1()
        {
            InitializeComponent();

            // Assigning Tag values to PictureBox controls
            picBoxPlayer1.Tag = "1";
            picBoxPlayer2.Tag = "2";
            picBoxPlayer3.Tag = "3";
        }
        private void pcboptions_MouseDown(object sender, MouseEventArgs e)
        {
            pcbForm = (PictureBox)sender;

            //Only allows you to pick up an image when the backcolor is transparent
            if (pcbForm.BackColor == Color.Transparent)
            {
                //make all picturboxes that dont have an image green
                foreach (PictureBox pictureBox in gbxPlayer.Controls.OfType<PictureBox>())
                {
                    if (pictureBox.Image == null)
                    {
                        pictureBox.BackColor = Color.Green;
                    }
                }
                //This actualy lets you hold the image while you keep your mouse down
                pcbForm.DoDragDrop(pcbForm.Image, DragDropEffects.Copy);
            }
        }
        private void pcbAllPlayer_DragDrop(object sender, DragEventArgs e)
        {
            //Drops the image your holding into the new picturebox
            pcbTo = (PictureBox)sender;
            Image getPicture = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            pcbTo.Image = getPicture;
            if (pcbTo.Tag != null && pcbTo.Tag.ToString() == Convert.ToString(1))
            {
                //intialize the fighterOne object with a name. the name is the last part of the picturebox (rook, paper or scissor
                fighterOne = new Fighter(pcbForm.Name.Remove(0, 3));
                //the location of the object on board is 1
                fighterOne.SetOnBoard(true, 1);
            }
            if (pcbTo.Tag != null && pcbTo.Tag.ToString() == Convert.ToString(2))
            {
                fighterTwo = new Fighter(pcbForm.Name.Remove(0, 3));
                fighterTwo.SetOnBoard(true, 2);
            }
            else if (pcbTo.Tag.ToString() == Convert.ToString(3))
            {
                fighterThree = new Fighter(pcbForm.Name.Remove(0, 3));
                fighterThree.SetOnBoard(true, 3);
            }
            //When you drop an image all backcolores of the player pictureboxes are ret to transparent again
            ClearBoardColors();

            // if all fighters are initialized you can check who wins
            if (fighterOne != null && fighterTwo != null && fighterThree != null)
            {   
                btnCheck.Enabled = true;
            }
        }
        //clear all the images backcolor of the gbxPlayer to transparent
        private void ClearBoardColors()
        {
            foreach (PictureBox pb in gbxPlayer.Controls.OfType<PictureBox>())
            {
                pb.BackColor = Color.Transparent;
            }
        }
        private void pcbAllPlayer_DragOver(object sender, DragEventArgs e)
        {
            //only last te image be dropped if pcb == green
            pcbTo = (PictureBox)sender;
            if (pcbTo.BackColor == Color.Green)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Assigning Tag values to PictureBox controls
            picBoxPlayer1.Tag = "1";
            picBoxPlayer2.Tag = "2";
            picBoxPlayer3.Tag = "3";

            ResetGame();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            //Resets the chosen fighters
            fighterOne = null;
            fighterTwo = null;
            fighterThree = null;

            foreach (PictureBox pictureBox in gbxPlayer.Controls.OfType<PictureBox>())
            {
                pictureBox.BackColor = Color.Transparent;
            }
            //This sets all picturebox backgrounds back to transparent
            foreach (PictureBox pictureBox in gbxOptions.Controls.OfType<PictureBox>())
            {
                pictureBox.BackColor = Color.Transparent;
            }
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            correctAnswerCount = 0;
            // checks for each of the bot pictureboxes if the player picked the same image
            CheckCorrect(pcbBotOne, fighterOne);
            CheckCorrect(pcbBotTwo, fighterTwo);
            CheckCorrect(pcbBotThree, fighterThree);

            if (correctAnswerCount == 0)
            {
                MessageBox.Show("to bad , you did nothing correct.");
            }
            else if (correctAnswerCount == 1 || correctAnswerCount == 2)
            {
                MessageBox.Show("Weldone, you guessed" + correctAnswerCount + "Correct.");
            }
            else
            {
                MessageBox.Show("Very nice! You did it perfectly!");
            }
            ResetGame();
        }
        private void CheckCorrect(PictureBox pcbBotChange, Fighter fighter) 
        {
            // Get a random image from the possible options
            int randomNumber = randomGenerator.Next(1, 4);
            string randomName = "";

            if (randomNumber == 1) 
            {
                randomName = "rook";
            }
            else if (randomNumber == 2) 
            {
                randomName = "papier";
            }
            else if (randomNumber == 3) 
            {
                randomName = "sisssor";
            }
            Bitmap bm = new Bitmap("C:\\Users\\strow\\Downloads\\DraggingPictures_22SD_Humam_barjak\\" + randomName +".png");

            //Sets thee image to the actual picturebox
            pcbBotChange.Image = bm;

            // if the picked picturbox is the same as the  one of the  playerlist you guessed this one correct
            if (randomName == fighter.GetName()) 
            {
                correctAnswerCount++;
                pcbBotChange.BackColor = Color.LightGreen;
            }
            else 
            {
                pcbBotChange.BackColor= Color.Red;
            }
        }
        public void ResetGame() 
        {
            btnCheck.Enabled = false;
            btnStart.Enabled = true;
            foreach(PictureBox pb in gbxPlayer.Controls.OfType<PictureBox>()) 
            {
                pb.BackColor = Color.Gray;
                pb.Image = null;
                pb.AllowDrop = true;
            }
            foreach (PictureBox pb in gbxBot.Controls.OfType<PictureBox>()) 
            {
                pb.BackColor = Color.Gray;
                pb.Image = null;
            }
            foreach(PictureBox pb in gbxOptions.Controls.OfType<PictureBox>()) 
            {
                pb.BackColor = Color.Gray;
            }
        }
        /// <summary>
        /// When pressing and holding down the mousebutton above one of the option boxes the
        /// application shows where you're allowed to drop an image. Only on the green pictureboxes are you allowed to drop an image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    }
}
