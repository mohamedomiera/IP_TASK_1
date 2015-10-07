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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
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

                        int height,width;
                        string AllFile;
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                sr.ReadLine();
                            }
                            string[] line = sr.ReadLine().Split();
                            width = Convert.ToInt32(line[0]);
                            height = Convert.ToInt32(line[1]);
                            sr.ReadLine();
                            AllFile = sr.ReadToEnd();
                            richTextBox1.Text = AllFile;
                        }
                        Bitmap bm = new Bitmap(width, height);
                        int index = 0;
                        for (int i = 0; i < width; i++)
                            {
                                for (int j = 0; j < height; j++)
                                {
                                    int Red = Convert.ToInt32(AllFile[index]);
                                    index++;
                                    int Green = Convert.ToInt32(AllFile[index]);
                                    index++;
                                    int Blue = Convert.ToInt32(AllFile[index]);
                                    index++;
                                    Color Color = Color.FromArgb(Red, Green, Blue);
                                    bm.SetPixel(i,j,Color);
                                }
                            }
                            pictureBox1.Image = bm;
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
