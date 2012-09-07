using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace CoupeImage
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string fileName = "";
                int possibiliteChoisi = -1;
                int quality = 74;

                if (args.Length >= 1)
                {
                    fileName = args[0];
                }
                if (args.Length >= 2)
                {
                    possibiliteChoisi = Convert.ToInt32(args[1]);
                }
                if (args.Length >= 3)
                {
                    quality = Convert.ToInt32(args[2]);
                }

                Console.Out.Write("Chargement de l'image ...");
                Bitmap src = Image.FromFile(fileName) as Bitmap;
                Console.Out.WriteLine("Charge.");
                Console.Out.WriteLine("Largeur de l'image : " + src.Width);
                Console.Out.WriteLine("Hauteur de l'image : " + src.Height);

                if (possibiliteChoisi >= 1)
                {
                    int width = src.Width / possibiliteChoisi;
                    int height = src.Height;
                    Console.Out.WriteLine("Generation des " + possibiliteChoisi + " images ...");
                    Console.Out.WriteLine(" avec une largeur :" + width + " et une hauteur : " + height);

                    for (int i = 0; i < possibiliteChoisi; i++)
                    {
                        Console.Out.WriteLine("Generation de l'image : " + (i + 1) + ".");

                        Rectangle area = new Rectangle(i * width, 0, width, height);

                        Image dest = CropImage(src, area);

                        //dest.Save(System.IO.Path.GetFileNameWithoutExtension(fileName) + "_" + (i + 1) + System.IO.Path.GetExtension(fileName));
                        SaveJpeg(System.IO.Path.GetFileNameWithoutExtension(fileName) + "_" + (i + 1) + System.IO.Path.GetExtension(fileName), dest, quality);
                    }
                }
                else
                {

                    List<int> possibilites = new List<int>();

                    for (int i = 1; i < src.Width; i++)
                    {
                        if (src.Width % i == 0)
                        {
                            possibilites.Add(i);
                        }
                    }

                    Console.Out.Write("Possibilite : ");
                    foreach (var possibilite in possibilites)
                    {
                        Console.Out.Write(possibilite + "(" + (src.Width / possibilite) + ")" + ", ");
                    }
                    Console.Out.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        private static Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return bmpCrop;
        }

        /// <summary> 
        /// Saves an image as a jpeg image, with the given quality 
        /// </summary> 
        /// <param name="path">Path to which the image would be saved.</param> 
        // <param name="quality">An integer from 0 to 100, with 100 being the 
        /// highest quality</param> 
        public static void SaveJpeg(string path, Image img, int quality)
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");


            // Encoder parameter for image quality 
            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            // Jpeg image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        } 
    }
}
