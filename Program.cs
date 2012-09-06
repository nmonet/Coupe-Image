using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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

                if (args.Length >= 1)
                {
                    fileName = args[0];
                }
                if (args.Length >= 2)
                {
                    possibiliteChoisi = Convert.ToInt32(args[1]);
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

                        dest.Save(System.IO.Path.GetFileNameWithoutExtension(fileName) + "_" + (i + 1) + System.IO.Path.GetExtension(fileName));
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
    }
}
