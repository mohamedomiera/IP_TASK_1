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
                        string []Rows;
                        string[] Cols={};
                        string[] pixelsVal={};
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
                        }
                        Rows = AllFile.Split('\n');
                        for (int i = 0; i < Rows.Length - 1; i++)
                        {
                            Cols= Rows[i].Split();
                        }
                        richTextBox1.Text = pixelsVal[1] + "\n";
                        Bitmap bm = new Bitmap(width, height);
                        for (int i = 0; i < width; i++)
                            {
                                for (int j = 0; j < height; j++)
                                {
                                    // Red = Convert.ToInt32(AllFile[index]);
                                    //index++;
                                    // Green = Convert.ToInt32(AllFile[index]);
                                    //index++;
                                    // Blue = Convert.ToInt32(AllFile[index]);
                                    //index++;
                                    Color color = Color.FromArgb(i,j,j);
                                    bm.SetPixel(i, j, color);
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
