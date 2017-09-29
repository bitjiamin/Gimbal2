using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using BitMiracle.LibTiff.Classic;


namespace Gimbal
{
    class ProcessTif
    {
        
        
        string curpath = System.IO.Directory.GetCurrentDirectory();
        public int height = 2472;
        public int width = 3296;
        public int progress;
        public Bitmap bmpdisp;
        public int n = 10;

        public float[][] read_tif(string imagepath)
        {
            Tiff tiff = Tiff.Open(imagepath, "r");
            //int width = tiff.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
            //int height = tiff.GetField(TiffTag.IMAGELENGTH)[0].ToInt();
            int sampleperpixel = tiff.GetField(TiffTag.SAMPLESPERPIXEL)[0].ToInt();
            int bitspersample = tiff.GetField(TiffTag.BITSPERSAMPLE)[0].ToInt();
            int photo = tiff.GetField(TiffTag.PHOTOMETRIC)[0].ToInt();

            //double dpiX = tiff.GetField(TiffTag.XRESOLUTION)[0].ToDouble();
            //double dpiY = tiff.GetField(TiffTag.YRESOLUTION)[0].ToDouble();
            byte[] scanline = new byte[tiff.ScanlineSize()];
            int scanlinesize = tiff.ScanlineSize();
            byte[][] buffer = new byte[height][];

            //ushort[] temp = new ushort[scanlinesize / 2];
            //float[] ftemp = new float[scanlinesize / 2];
            float[][] ftemp = new float[height][];
            ushort[][] temp = new ushort[height][];

            for (int i = 0; i < height; i++)
            {
                buffer[i] = new byte[scanlinesize];
                temp[i] = new ushort[scanlinesize / 2];
                ftemp[i] = new float[scanlinesize / 2];

                tiff.ReadScanline(buffer[i], i);
                Buffer.BlockCopy(buffer[i], 0, temp[i], 0, scanline.Length);
                float factor = 1;
                for (int j = 0; j < width; j++)
                {
                    ftemp[i][j] = temp[i][j]*factor;
                }
            }
            return ftemp;
        }

        public float[,] ReadAndMeanTiff(string path,int n)
        {
            Form1 main = new Form1();
            float[,] datasum = new float[height, width];
            float[][] datasingle = new float[height][];
            //读取100张图片进行累加
            for (int i = 0; i < n; i++)
            {
                datasingle = read_tif(path + (i + 1).ToString() + ".tiff");
                for (int x = 0; x < height; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        datasum[x, y] = datasum[x, y] + datasingle[x][y];
                    }
                }
                progress += 1;

            }

            //取100张图片均值
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    datasum[x, y] = datasum[x, y] / n;
                }
            }
            return datasum;
        }

        public void FloatToTif(float[,] data,string filename)
        {
            using (Tiff output = Tiff.Open(filename, "w"))
            {
                if (output == null)
                    return;

                //byte[][] buffer1 = new byte[height][];
                byte[] scanline = new byte[width * 4];

                output.SetField(TiffTag.IMAGEWIDTH, width);
                output.SetField(TiffTag.IMAGELENGTH, height);
                output.SetField(TiffTag.BITSPERSAMPLE, 32);
                output.SetField(TiffTag.SAMPLESPERPIXEL, 1);

                output.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);
                output.SetField(TiffTag.PHOTOMETRIC, 1);

                output.SetField(TiffTag.ROWSPERSTRIP, 1);
                output.SetField(TiffTag.COMPRESSION, Compression.LZW);
                output.SetField(TiffTag.DATATYPE, TiffType.SHORT);

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        scanline[4 * j] = BitConverter.GetBytes(data[i, j])[0];
                        scanline[4 * j + 1] = BitConverter.GetBytes(data[i, j])[1];
                        scanline[4 * j + 2] = BitConverter.GetBytes(data[i, j])[2];
                        scanline[4 * j + 3] = BitConverter.GetBytes(data[i, j])[3];

                        //Single test = BitConverter.ToSingle(scanline1, 4000);
                        //float testf = ftemp[j];
                    }
                    output.WriteScanline(scanline, i);
                }
            }
            //Process.Start("processed.tif");
        }

        public Bitmap FloatTo8bmp(float[,] data)
        {
            //
            ushort[] data8bit = new ushort[width];
            byte[] scanline = new byte[width*2];
            byte[] scanline8 = new byte[height*width];
            float max = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (data[i, j] > max)
                    {
                        max = data[i, j];
                    }
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    
                    data[i, j] = 256*data[i, j]/max;
                    Math.Round(data[i, j], 0); 
                    data8bit[j] = (ushort)data[i, j];
                    //Single test = BitConverter.ToSingle(scanline1, 4000);
                    //float testf = ftemp[j];
                }
                    
                Buffer.BlockCopy(data8bit, 0, scanline, 0, scanline.Length);
                for (int k = 0; k < width; k++)
                {
                    scanline8[i*width+k] = scanline[2*k];
                }
            }
            Bitmap bmp = ToGrayBitmap(scanline8, width, height);
            bmp.Save(@"test.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            return bmp;
            //Process.Start("test.png");
        }

        public Bitmap UshortTo8bmp(ushort[,] data)
        {
            //
            ushort[] data8bit = new ushort[width];
            byte[] scanline = new byte[width * 2];
            byte[] scanline8 = new byte[height * width];
            ushort max = 1;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (data[i, j] > max)
                    {
                        max = data[i, j];
                    }
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    data[i, j] = (ushort)(255 *data[i, j] / max);
                    data8bit[j] = data[i, j];
                    //Single test = BitConverter.ToSingle(scanline1, 4000);
                    //float testf = ftemp[j];
                }

                Buffer.BlockCopy(data8bit, 0, scanline, 0, scanline.Length);
                for (int k = 0; k < width; k++)
                {
                    scanline8[i * width + k] = scanline[2 * k];
                }
            }
            Bitmap bmp = ToGrayBitmap(scanline8, width, height);
            bmp.Save(@"test.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            return bmp;
            //Process.Start("test.png");
        }

        public void ByteToTiff(byte[][] data, string filename)
        {
            using (Tiff output = Tiff.Open(filename, "w"))
            {
                if (output == null)
                    return;

                output.SetField(TiffTag.IMAGEWIDTH, width);
                output.SetField(TiffTag.IMAGELENGTH, height);
                output.SetField(TiffTag.BITSPERSAMPLE, 16);
                output.SetField(TiffTag.SAMPLESPERPIXEL, 1);
                output.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISBLACK);
                output.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);
                output.SetField(TiffTag.ROWSPERSTRIP, 1);
                output.SetField(TiffTag.COMPRESSION, Compression.LZW);
                byte[] scanline = new byte[width*2];
                
                for (int i = 0; i < height; i++)
                {
                    scanline = data[i];
                    output.WriteScanline(scanline, i);
                }
            }
        }

        public static Bitmap ToGrayBitmap(byte[] rawValues, int width, int height)
        {
            //// 申请目标位图的变量，并将其内存区域锁定
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            //// 获取图像参数
            int stride = bmpData.Stride;　 // 扫描线的宽度
            int offset = stride - width;　 // 显示宽度与扫描线宽度的间隙
            IntPtr iptr = bmpData.Scan0;　 // 获取bmpData的内存起始位置
            int scanBytes = stride * height;　　 // 用stride宽度，表示这是内存区域的大小

            //// 下面把原始的显示大小字节数组转换为内存中实际存放的字节数组
            int posScan = 0, posReal = 0;　　 // 分别配置两个位置指针，指向源数组和目标数组
            byte[] pixelValues = new byte[scanBytes];　 //为目标数组分配内存

            for (int x = 0; x < height; x++)
            {
                //// 下面的循环节是模拟行扫描
                for (int y = 0; y < width; y++)
                {
                    pixelValues[posScan++] = rawValues[posReal++];
                }
                posScan += offset;　 //行扫描结束，要将目标位置指针移过那段“间隙” ;
            }

            //// 用Marshal的Copy要领，将刚才得到的内存字节数组复制到BitmapData中
            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, iptr, scanBytes);
            bmp.UnlockBits(bmpData);　 // 解锁内存区域

            //// 下面的代码是为了修改生成位图的索引表，从伪彩修改为灰度
            ColorPalette tempPalette;
            using (Bitmap tempBmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                tempPalette = tempBmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                tempPalette.Entries[i] = Color.FromArgb(i, i, i);
            }

            bmp.Palette = tempPalette;

            //// 算法到此结束，返回结果
            return bmp;
        }

        public void Calibration()
        {
            float[,] datasum = new float[height, width];
            float[,] datasumdark = new float[height, width];
            //读取亮图像并取平均
            datasum = ReadAndMeanTiff(curpath+@"\image\bright\",n);
            datasumdark = ReadAndMeanTiff(curpath + @"\image\dark\", n);

           
            //减去暗电流后取最大值
            float max = 0;
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {

                    //减去暗电流
                    datasum[x, y] = datasum[x, y] - datasumdark[x, y];

                    if (datasum[x, y] > max)
                    {
                        //取最大值
                        max = datasum[x, y];
                    }
                }
            }
            //除最大值并取倒数
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    datasum[x, y] = max / datasum[x, y];
                }
            }
            //浮点数组转Tif并保存
            AvtCam avt = new AvtCam();
            string cameraID = avt.GetCameraID();
            //FloatToTif(datasumdark, @"image\result\" + cameraID + "_DC.tiff");
            FloatToTif(datasum, @"image\result\" + cameraID + "_FFC.tiff");
            bmpdisp = FloatTo8bmp(datasum);
            MessageBox.Show("Calibration completed!!", "Notice", MessageBoxButtons.OK);
        }

    }
}
