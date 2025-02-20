using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BitmapConverter
{
    public partial class Form1 : Form
    {

        [DllImport("BITMAP_DLL_ASM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern int ConvertASM(byte* inputPtr, byte* outputPtr, int inputWidth, int inputHeight,
                                      int outputWidth, int outputHeight,
                                      int mode);

        [DllImport("BITMAP_DLL_CPP.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern int ConvertCPP(char* inputPtr, char* outputPtr, int inputWidth, int inputHeight,
                                      int outputWidth, int outputHeight,
                                      int mode);

        private Image img;
        private byte[] rawimage;
        private byte[] outputBuffer;
        int TargetWidth;
        int TargetHeight;
        int mode;

        Stopwatch stopwatch = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                textBox1.Text = filePath;
                img = Image.FromFile(filePath);
                pictureBox1.Image = img;
                rawimage = rawImage(img);
            }
        }

        private byte[] rawImage(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            int width = bitmap.Width;
            int height = bitmap.Height;
            byte[] rgbData = new byte[width * height * 3];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    int index = (y * width + x) * 3;
                    rgbData[index] = pixel.R;
                    rgbData[index + 1] = pixel.G;
                    rgbData[index + 2] = pixel.B;
                }
            }

            return rgbData;
        }

        private void asmButton_Click(object sender, EventArgs e)
        {
            unsafe
            {
                fixed (byte* inputPtr = rawimage)
                fixed (byte* outputPtr = outputBuffer)
                {
                    stopwatch.Start();
                    ConvertASM(inputPtr, outputPtr, img.Width, img.Height, TargetWidth, TargetHeight, mode);
                    stopwatch.Stop();

                    double microseconds = stopwatch.ElapsedTicks * (1_000_000.0 / Stopwatch.Frequency);
                    label7.Text = $"Time: {microseconds} microseconds";
                    stopwatch.Reset();
                    Bitmap bitmap;
                    switch (mode)
                    {
                        case 0:
                            bitmap = CreateBitmapFromRawData(outputBuffer, TargetWidth, TargetHeight, PixelFormat.Format1bppIndexed);
                            break;
                        case 565:
                            bitmap = CreateBitmapFromRawData(outputBuffer, TargetWidth, TargetHeight, PixelFormat.Format16bppRgb565);
                            break;
                        case 666:
                            bitmap = CreateBitmapFromRawData(outputBuffer, TargetWidth, TargetHeight, PixelFormat.Format24bppRgb);
                            break;
                        case 888:
                            bitmap = CreateBitmapFromRawData(outputBuffer, TargetWidth, TargetHeight, PixelFormat.Format24bppRgb);
                            break;
                        default:
                            bitmap = null;
                            break;
                    }
                    //ShowImageInWindow(bitmap);
                    ShowBitmapInWindow(outputBuffer);
                    resultButton.Enabled = true;
                }
            }
        }
        private void cButton_Click(object sender, EventArgs e)
        {
            unsafe
            {
                fixed (byte* inputPtr = rawimage)
                fixed (byte* outputPtr = outputBuffer)
                {
                    stopwatch.Start();
                    ConvertCPP((char*)inputPtr, (char*)outputPtr, img.Width, img.Height, TargetWidth, TargetHeight, mode);
                    stopwatch.Stop();

                    double microseconds = stopwatch.ElapsedTicks * (1_000_000.0 / Stopwatch.Frequency);
                    label7.Text = $"Time: {microseconds} microseconds";
                    stopwatch.Reset();
                    Bitmap bitmap;
                    switch (mode)
                    {
                        case 0:
                            bitmap = CreateBitmapFromRawData(outputBuffer, TargetWidth, TargetHeight, PixelFormat.Format1bppIndexed);
                            break;
                        case 565:
                            bitmap = CreateBitmapFromRawData(outputBuffer, TargetWidth, TargetHeight, PixelFormat.Format16bppRgb565);
                            break;
                        case 888:
                            bitmap = CreateBitmapFromRawData(outputBuffer, TargetWidth, TargetHeight, PixelFormat.Format24bppRgb);
                            break;
                        default:
                            bitmap = null;
                            break;
                    }
                    //ShowImageInWindow(bitmap);
                    ShowBitmapInWindow(outputBuffer);
                    resultButton.Enabled = true;
                }
            }
        }

        static Bitmap CreateBitmapFromRawData(byte[] rawData, int width, int height, PixelFormat format)
        {

            Bitmap bitmap = new Bitmap(width, height, format);
            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly,
                format
            );
            Marshal.Copy(rawData, 0, bitmapData.Scan0, rawData.Length);
            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }

        static void ShowImageInWindow(Image image)
        {
            Form form = new Form
            {
                Text = "Podgląd obrazu",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            PictureBox pictureBox = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.AutoSize
            };

            form.Controls.Add(pictureBox);

            form.Show();
        }

        private void ShowBitmapInWindow(byte[] bitmap)
        {
            Form textForm = new Form
            {
                Text = "Bitmap",
                AutoSize = true
            };

            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("const unsigned char bitmap [] PROGMEM = {");

            for (int i = 0; i < bitmap.Length; i++)
            {
                if (i > 0 && i % TargetWidth == 0)
                    sb.AppendLine();

                sb.Append("0x").Append(bitmap[i].ToString("X2"));

                if (i != bitmap.Length - 1)
                    sb.Append(", ");
            }

            sb.AppendLine();
            sb.AppendLine("};");

            textBox.Text = sb.ToString();

            System.Windows.Forms.Button copyButton = new System.Windows.Forms.Button
            {
                Text = "Copy",
                Dock = DockStyle.Bottom
            };

            copyButton.Click += (sender, e) =>
            {
                Clipboard.SetText(textBox.Text);
                MessageBox.Show("Copied to clipboard", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            textForm.Controls.Add(textBox);
            textForm.Controls.Add(copyButton);
            textForm.Size = new System.Drawing.Size(600, 400);
            textForm.ShowDialog();
        }

        private void Display_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Display.SelectedIndex == 0)
            {
                TargetStyle.Items.Clear();
                TargetStyle.Items.Add("Monochromatic");

                TargetWidth = 128;
                TargetHeight = 64;

                TargetStyle.SelectedIndex = 0;
            }
            else if (Display.SelectedIndex == 1)
            {
                TargetStyle.Items.Clear();
                TargetStyle.Items.Add("Monochromatic");
                TargetStyle.Items.Add("RGB565");

                TargetWidth = 128;
                TargetHeight = 160;
                TargetStyle.SelectedIndex = 0;
            }
            else if (Display.SelectedIndex == 2)
            {
                TargetStyle.Items.Clear();
                TargetStyle.Items.Add("Monochromatic");
                TargetStyle.Items.Add("RGB565");
                TargetStyle.Items.Add("RGB888");

                TargetWidth = 240;
                TargetHeight = 320;
                TargetStyle.SelectedIndex = 0;
            }
        }
        private void TargetStyle_TextChanged(object sender, EventArgs e)
        {
            if (TargetStyle.Text == "Monochromatic")
            {
                mode = 0;
                outputBuffer = new byte[TargetWidth * TargetHeight / 8];
            }
            else if (TargetStyle.Text == "RGB565")
            {
                mode = 565;
                outputBuffer = new byte[TargetWidth * TargetHeight * 2];
            }
            else if (TargetStyle.Text == "RGB888")
            {
                mode = 888;
                outputBuffer = new byte[TargetWidth * TargetHeight * 3];
            }
        }

        private void resultButton_Click(object sender, EventArgs e)
        {
            ShowBitmapInWindow(outputBuffer);
        }

    }
}