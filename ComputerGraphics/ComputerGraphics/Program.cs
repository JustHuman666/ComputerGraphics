﻿using ComputerGraphics.Objects;
using ComputerGraphics.Types;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using ComputerGraphics.Scene;

namespace ComputerGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            //4k 3840 x 2160 pixel
            int width = 1280;
            int height = 720;

            Camera camera = new Camera(new Point(0, 0, -10), new Vector(0, 0, 1), height, width);
            Scene.Scene scene = new Scene.Scene(camera);
            Sphere sphere = new Sphere(new Point(2, -2, 100), 4);
            scene.AddObject(sphere);
            Sphere sphere2 = new Sphere(new Point(0, 2, 10), 2);
            scene.AddObject(sphere2);
            Light light = new Light(new Vector(-0.5, 0.5, 0.7));
            scene.AddLight(light);

            // Plane plane = new Plane(new Point(-1000, 1, 1), new Vector(-1, 0, 1));
            Plane plane = new Plane(new Point(-5, 1, 1), new Vector(-1, 0, 0));
            scene.AddObject(plane);

            double[,] screen = scene.getScreenArray();

            Console.WriteLine("render done");

            string filename = @"C:\Users\Denys\Desktop\imagse.ppm"; //destination
            StreamWriter destination = new StreamWriter(filename);
            destination.Write("P3\n{0} {1} {2}\n", width, height, 255);
            destination.Flush();

            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int a = Convert.ToInt32(screen[i, j] * 255);
                    if (screen[i, j] == 0)  //if no interception
                        a = 255;

                    if (a > 255)
                        a = 255;
                    if (a < 0)
                        a = 0;

                    output.Append(a);
                    output.Append(" ");
                    output.Append(a);
                    output.Append(" ");
                    output.Append(a);
                    output.Append(" ");
                }
                destination.WriteLine(output);
                output.Clear();

                Console.SetCursorPosition(0,0);
                Console.Write(Convert.ToInt32(i));
                Console.Write("/");
                Console.Write(height);
            }

            destination.Close();
            Console.WriteLine("file created");
        }
    }
}
