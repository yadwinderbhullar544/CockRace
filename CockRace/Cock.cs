using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CockRace
{
    public partial class Cock : Form
    {
        //This Cock race Code developed by Yadwinder Singh
        //Creating the objects of classes
        CockGreyhound[] cockGreyhounds = new CockGreyhound[4];//instance of greyhound class
        CockClient[] cockClients = new CockClient[3];//object of guy class
        public Cock()
        {
            InitializeComponent();
            RaceTrackSetting();//calling the set race track funtion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CockRacingStart();
        }
        private void RaceTrackSetting()//this funtion is for setting the race track
        {
            minimumBetLabel.Text = string.Format("Minimum Bet $1", (int)numericUpDown1.Minimum);//Showing the minimum bet rate in label

            int sPosition = pictureBox2.Right - pictureBox1.Left; //Setting the variable for starting position from cock 1
            int rTLength = pictureBox1.Size.Width;//Setting the variable of length of racetrack

            //Setting values of the array of the class greyhound for racing for the first part of the game as suggested in assignment
            cockGreyhounds[0] = new CockGreyhound()
            {
                MPBox = pictureBox2,
                RTLen = rTLength,
                SPos = sPosition
            };

            cockGreyhounds[1] = new CockGreyhound()
            {
                MPBox = pictureBox3,
                RTLen = rTLength,
                SPos = sPosition
            };
            cockGreyhounds[2] = new CockGreyhound()
            {
                MPBox = pictureBox4,
                RTLen = rTLength,
                SPos = sPosition
            };
            cockGreyhounds[3] = new CockGreyhound()
            {
                MPBox = pictureBox5,
                RTLen = rTLength,
                SPos = sPosition
            };

            //this part is for assigning the constructor values which is created in guy class
            Punter[] punters = new Punter[3];
            punters[0] = Factory.CreatePunter("Yadwinder");
            punters[0].SetPunter(null, 45, yadwinderRadioButton, yadwinderBetLabel);
            cockClients[0] = new CockClient("Yadwinder", null, 50, yadwinderRadioButton, yadwinderBetLabel);
            punters[1] = Factory.CreatePunter("Love");
            punters[1].SetPunter(null, 75, loveRadioButton, loveBetLabel);
            cockClients[1] = new CockClient("Love", null, 75, loveRadioButton, loveBetLabel);
            punters[2] = Factory.CreatePunter("Rajinder");
            punters[2].SetPunter(null, 45, rajinderRadioButton, rajinderBetLabel);
            cockClients[2] = new CockClient("Rajinder", null, 45, rajinderRadioButton, rajinderBetLabel);

            foreach (CockClient guy in cockClients)
            {
                guy.UpdateLabels();//using the foreach loop for assigning the values of labels for bet
            }
        }

        private void CockRacingStart()//this is function for starting the race
        {
            bool NoWinner = true;
            int winningCock;
            button1.Enabled = false;//Button will be false whenever race is continue

            while (NoWinner)
            {
                Application.DoEvents();
                for (int i = 0; i < cockGreyhounds.Length; i++)//loop start and it will continue whenever length of greyhound class or track is not finished
                {
                    Thread.Sleep(1);//sleep function for the speed of cars
                    if (cockGreyhounds[i].Run())//here run function is called from greyhound class for running the cars and condition is checked for three random cars
                    {
                        winningCock = i + 1;
                        NoWinner = false;
                        MessageBox.Show("We have a winner - Cock #" + winningCock);
                        foreach (CockClient guy in cockClients)
                        {
                            if (guy.MyCockBet != null)//condition is checked for betting
                            {
                                guy.Collect(winningCock);
                                guy.MyCockBet = null;
                                guy.UpdateLabels();
                            }
                        }

                        foreach (CockGreyhound cock in cockGreyhounds)
                        {
                            cock.TakeSPos();
                        }

                        break;
                    }
                }

            }

            button1.Enabled = false;//here race button is enabled when race is finished

        }

        private void Bets_Click(object sender, EventArgs e)
        {
            int GuyNumber = 0;

            if (yadwinderRadioButton.Checked)
            {
                GuyNumber = 0;//when radio button yadwinder is checked then set its id is 0
            }
            if (loveRadioButton.Checked)
            {
                GuyNumber = 1;//when radio button love is checked then set its id is 1
            }
            if (rajinderRadioButton.Checked)
            {
                GuyNumber = 2;//when radio button rajinder is checked then set its id is 2
            }

            cockClients[GuyNumber].PlaceCockBet((int)numericUpDown1.Value, (int)numericUpDown2.Value);//when any radio button is checked then place bet function is called and bet is placed and show amount and car number
            cockClients[GuyNumber].UpdateLabels();//with this code line the labels are updated
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            foreach (CockGreyhound cock in cockGreyhounds)
            {
                cock.TakeSPos();
            }
            if (yadwinderBetLabel.Text == "BUSTED" && loveBetLabel.Text == "BUSTED" && rajinderBetLabel.Text == "BUSTED")
            {
                RaceTrackSetting();

                cockClients[0] = new CockClient("Yadwinder", null, 50, yadwinderRadioButton, yadwinderBetLabel);
                cockClients[1] = new CockClient("Love", null, 75, loveRadioButton, loveBetLabel);
                cockClients[2] = new CockClient("Rajinder", null, 45, rajinderRadioButton, rajinderBetLabel);

                foreach (CockClient punter in cockClients)
                {
                    punter.UpdateLabels();
                }
                yadwinderBetLabel.ForeColor = System.Drawing.Color.White;
                rajinderBetLabel.ForeColor = System.Drawing.Color.White;
                loveBetLabel.ForeColor = System.Drawing.Color.White;
                yadwinderRadioButton.Enabled = true;
                loveRadioButton.Enabled = true;
                rajinderRadioButton.Enabled = true;
                numericUpDown1.Value = 1;
                numericUpDown2.Value = 1;

            }
            {
                Application.Restart();
            }
        }
    }
}
