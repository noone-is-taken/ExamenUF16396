using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace M09ExamenPolFerdman {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        WriteableBitmap bmp;

        public MainWindow() {
            InitializeComponent();
            crearImatges();
        }




        //apartat A)
        private void crearImatges() {
            int dpi = 96;
            int width = 14;
            int height = 15;
            int bitsPerPixel = 3;
            byte[] bytesImatge = new byte[width * height * bitsPerPixel];


            Random r = new Random();

            int nPixelActual = 0;
            int PixelsQueQuedenDunColor = 2;
            int colorAPintar = 0;

            for (int i = 0; i <  height; i++) {
                for (int j = 0; j < width; j++) {

                    int nByteActual = nPixelActual * bitsPerPixel;
                    if(colorAPintar == 0) {
                        for (int p = 0; p < bitsPerPixel; p++) {
                            bytesImatge[nByteActual +1] = 0;
                        }
                    } else {
                        for (int p = 0; p < bitsPerPixel; p++) {
                            bytesImatge[nByteActual +1] = 255;
                        }
                    }

                    nPixelActual += 1;


                }
                PixelsQueQuedenDunColor--;
                if(PixelsQueQuedenDunColor <= 0) {
                    PixelsQueQuedenDunColor = 2;
                    colorAPintar += 1;
                    if (colorAPintar >= 2) colorAPintar = 0;
                }
            }

            bmp = new WriteableBitmap(width, height, dpi, dpi, PixelFormats.Rgb24, null);

            bmp.WritePixels(new Int32Rect(0, 0, width, height), bytesImatge, width * bitsPerPixel, 0);

            this.MainImg.Stretch = Stretch.Uniform;
            this.MainImg.Source = bmp;
        }


        //apartat B)
        private void XifrarImatge() {
            int bytesPerPixel = bmp.Format.BitsPerPixel / 8;
            byte[] arrayByts = new byte[bmp.PixelWidth * bmp.PixelHeight * bytesPerPixel];


            int bytesPerLinia = bmp.PixelWidth * bytesPerPixel;

            bmp.CopyPixels(arrayByts, bytesPerLinia, 0);

            string misatge = "misatge xifrat";

            int[] rgb = enter2ByteRGB(misatge.Length, arrayByts[0], arrayByts[1], arrayByts[2]);
            int[] r = new int[8];
            int[] g = new int[8];
            int[] b = new int[8];

            Array.Copy(rgb, 0, r, 0, r.Length);
            Array.Copy(rgb, 8, g, 0, g.Length);
            Array.Copy(rgb, 16, b, 0, b.Length);

            arrayByts[0] = (byte)bin2Enter(r);
            arrayByts[1] = (byte)bin2Enter(g);
            arrayByts[2] = (byte)bin2Enter(b);

        }

        private int bin2Enter(int[] byteArr) {
            return byteArr[0] * 128 + byteArr[1] * 64 + byteArr[2] * 32 + byteArr[3] * 16 + byteArr[4] * 8 + byteArr[5] * 4 + byteArr[6] * 2 + byteArr[7];
        }

        private int[] enter2ByteRGB(int num, byte arr1, byte arr2, byte arr3) {
            int[] numBin = enter2Bin(num);
        }

        private int[] enter2Bin(int num) {
            if (num > 255)
                throw new Exception("Error");
            int[] result = new int[8];
            for (int i = 0; i < 8; i++) {
                int numm = num >> 7 - i;
                result[i] = numm & 1;
            }

            return result;
        }
    }
}
