using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
    
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png,*.ppm) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.ppm;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName.Contains(".ppm"))
                {
                    try
                    {
                        int Red, Green, Blue;
                        int index = 0;
                        string[] Rows;
                        List<string> AllPixels = new List<string>();
                        string[] TmpPixels;
                        int height, width;
                        string AllFile, type;
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            type = sr.ReadLine();
                            string laststring;
                            while (true)
                            {
                                laststring = sr.ReadLine();
                                if (laststring[0] == '#')
                                    continue;
                                else
                                    break;
                            }
                            string[] line = laststring.Split();
                            width = Convert.ToInt32(line[0]);
                            height = Convert.ToInt32(line[1]);
                            sr.ReadLine();
                            AllFile = sr.ReadToEnd();
                        }
                        Rows = AllFile.Split('\n');
                        for (int i = 0; i < Rows.Length - 1; i++)
                        {
                            TmpPixels = Rows[i].Split();
                            for (int j = 0; j < TmpPixels.Length - 1; j++)
                            {
                                AllPixels.Add(TmpPixels[j]);

                            }
                        }
                        Bitmap bm = new Bitmap(width, height);
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                Red = Convert.ToInt32(AllPixels[index]);
                                index++;
                                Green = Convert.ToInt32(AllPixels[index]);
                                index++;
                                Blue = Convert.ToInt32(AllPixels[index]);
                                index++;
                                Color color = Color.FromArgb(Red, Green, Blue);
                                bm.SetPixel(j, i, color);
                            }
                        }
                        int Bufferindex = 0;
                        int[,] buffer = new int[width,height];
                        Matrix Mat = new Matrix();
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                buffer[j,i] = Convert.ToInt32(AllPixels[Bufferindex]);
                                Bufferindex++;
                            }
                        }

                        pictureBox1.Image = bm;
                   
                     
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
         
                }
                else
                {
                    Bitmap bitmap = new Bitmap(ofd.FileName);
                    pictureBox1.Image = bitmap;
                }

               
            }
        }
       
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter ="Image files (*.jpg,*.png,*.ppm) | *.jpg; *.png; *.ppm;";
            ImageFormat format = ImageFormat.Png;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                pictureBox1.Image.Save(sfd.FileName, format);
            }
        }


        private bool _selecting;
        private Rectangle _selection;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _selecting = true;
                _selection = new Rectangle(new Point(e.X, e.Y), new Size());
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_selecting)
            {
                _selection.Width = e.X - _selection.X;
                _selection.Height = e.Y - _selection.Y;

                // Redraw the picturebox:
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (_selecting)
            {
                // Draw a rectangle displaying the current selection
                Pen pen = Pens.GreenYellow;
                e.Graphics.DrawRectangle(pen, _selection);
            }
        }

        private void Crop_Click(object sender, EventArgs e)
        {
            if (_selecting == true)
            {
                Crop.Enabled = false;
            }
            Bitmap CroppedImage = Functions.CropImage((Bitmap)pictureBox1.Image, _selection);
            pictureBox1.Image = CroppedImage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            this.Refresh();
        }

        private void Flip_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipY);
            } 
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left &&_selecting &&_selection.Size != new Size())
            {
                _selecting = false;
            }
            else
                _selecting = false;
        }

        private void Rotate_Click(object sender, EventArgs e)
        {
           float angle= Functions.GetAngleFromPoint(new Point(Cursor.Position.X,Cursor.Position.Y),new Point((pictureBox1.Image.Width)/2,(pictureBox1.Image.Height)/2));
            pictureBox1.Image =Functions.RotateImage((Bitmap)pictureBox1.Image,angle);
        }

        }
    }



