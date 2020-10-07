using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        FilterInfoCollection filter;
        VideoCaptureDevice videoDevice;

        private void button1_Click(object sender, EventArgs e)
        {
            videoDevice = new VideoCaptureDevice(filter[comboBox1.SelectedIndex].MonikerString);
            videoDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoDevice.Start();
        }

        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filter = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterinfo in filter)
            {
                comboBox1.Items.Add(filterinfo.Name);
            }
            comboBox1.SelectedIndex = 0;
            videoDevice = new VideoCaptureDevice();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoDevice.IsRunning == true)
                videoDevice.Stop();
        }
    }
}
