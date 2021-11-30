using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FPSUnlockAPI; //Calling the API

namespace FPS_Unlock_UI_Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FPSUnlockAPI.FPSUnlockAPI fps = new FPSUnlockAPI.FPSUnlockAPI(); //Create the [ FPSUnlockAPI.FPSUnlockAPI ]  object to call the following methods from


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            double value = trackBar1.Value; //Getting the Value of your Slider or Trackball
            fps.SendFPSValue(value); // Send the Value to the Pipe. 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try //Exception Handler ofc ( because some ppl gonna accident putting letter A to Z or special characters that cannot be converted to double )
            {
                string valuestring = textBox1.Text;
                double value = Convert.ToDouble(valuestring); //Convert the String to Double since even you type No. on text, it will show as a string, so it gonna convert it to double. 0-9, 
            }
            catch (Exception)
            {
                MessageBox.Show("Thats Not a Number");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* this is the Default One */
            /* ---------------------------------------------- */
            fps.RunFPSUnlock();

            /* Custom One, if you want to replace the download link with your own FPS Unlock */
            /* ---------------------------------------------- */
            // fps.RunFPSUnlock(true, "Download Link of your own FPS Unlock");
        }
    }
}
