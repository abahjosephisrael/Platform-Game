using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Game
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed, force, score = 0, playerSpeed = 5, horizontalSpeed = 5, verticalSpeed = 3, enemyOneSpeed = 5, enemyTwoSpeed = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {

            lblScore.Text = $"Score: {score}";

            player.Top += jumpSpeed;

            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }

            if (goRight == true)
            {
                player.Left += playerSpeed;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {

                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;

                            if ((string)x.Name == "horizontalPlatform" && goLeft == false
                                || (string)x.Name == "horizontalPlatform" && goRight == false
                                )
                            {
                                player.Left -= horizontalSpeed;
                            }
                        }

                        x.BringToFront();

                    }

                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            lblScore.Text = $"Score: {score} {Environment.NewLine} You were killed in your journey!";
                        }
                    }
                }
            }

            horizontalPlatform.Left -= horizontalSpeed;

            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            verticalPlatform.Top += verticalSpeed;

            if (verticalPlatform.Top < 148 || verticalPlatform.Top > 345)
            {
                verticalSpeed = -verticalSpeed;
            }

            enemyOne.Left -= enemyOneSpeed;
            if (enemyOne.Left < enemyOneHouse.Left || enemyOne.Left + enemyOne.Width > enemyOneHouse.Left + enemyOneHouse.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemyTwo.Left += enemyTwoSpeed;
            if (enemyTwo.Left < enemyTwoHouse.Left || enemyTwo.Left + enemyTwo.Width > enemyTwoHouse.Left + enemyTwoHouse.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                lblScore.Text = $"Score: {score} {Environment.NewLine} You fell to your death!";
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && score == 28)
            {
                gameTimer.Stop();
                isGameOver = true;
                lblScore.Text = $"Score: {score} {Environment.NewLine} Your quest is complete!";
            }
            else
            {
                lblScore.Text = $"Score: {score} {Environment.NewLine} You collected all coins!";
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }
        
        //Method to restart the game
        private void RestartGame()
        {


            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;

            lblScore.Text = $"Score: {score}";

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            // reset the position of player, platform and enemies

            player.Left = 23;
            player.Top = 401;

            enemyOne.Left = 326;
            enemyTwo.Left = 254;

            horizontalPlatform.Left = 194;
            verticalPlatform.Top = 345;

            gameTimer.Start();
        }
    }
}
