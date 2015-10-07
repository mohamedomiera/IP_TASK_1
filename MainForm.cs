using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP_TASK_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public struct buff
        {
            public int r;
            public int g;
            public int b;
        };
        OpenFileDialog ofd = new OpenFileDialog();
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png,*.ppm) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.ppm;" ;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
        
                if (ofd.FileName.Contains(".ppm"))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                sr.ReadLine();
                            }
                            string[] line = sr.ReadLine().Split();
                            int height = Convert.ToInt32(line[0]);
                            int width = Convert.ToInt32(line[1]);
                            string AllFile = sr.ReadToEnd();
                           string[] FileRows = AllFile.Split('\n');
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("The file could not be read:");
                        Console.WriteLine(ex.Message);
                    }

                }
                else
                {
                    Bitmap bitmap = new Bitmap(ofd.FileName);
                    pictureBox1.Image = bitmap;
                }
            }
        }
    }
}
