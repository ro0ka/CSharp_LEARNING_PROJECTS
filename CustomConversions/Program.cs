﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomConversions
{
    class Program
    {
        public struct Rectangle
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public Rectangle(int w, int h) : this()
            {
                Width = w;
                Height = h;
            }
            public void Draw()
            {
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine();
                }
            }
            public override string ToString() => $"[Width = {Width}; Height = {Height}]";

            public static implicit operator Rectangle(Square s)
            {
                Rectangle rec = new Rectangle
                {
                    Height = s.Length ,
                    Width = s.Length * 2
                };
                return rec;
            }
        }

        public struct Square
        {
            public int Length { get; set; }
            public Square(int l) : this()
            {
                Length = l;
            }
            public void Draw()
            {
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Length; j++)
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine();
                }
            }
            public override string ToString() => $"[Length = {Length}]";
            // Rectangles can be explicitly converted into Squares.
            public static explicit operator Square(Rectangle r)
            {
                Square s = new Square { Length = r.Height };
                return s;
            }
            public static explicit operator Square(int sideLength)
            {
                Square newSq = new Square { Length = sideLength };
                return newSq;
            }
            public static explicit operator int(Square s) => s.Length;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Conversions *****\n");
            // Make a Rectangle.
            Rectangle r = new Rectangle(15, 4);
            Console.WriteLine(r.ToString());
            r.Draw();
            Console.WriteLine();
            // Convert r into a Square,
            // based on the height of the Rectangle.
            Square s = (Square)r;
            Console.WriteLine(s.ToString());
            s.Draw();

            // Convert Rectangle to Square to invoke method.
            Rectangle rect = new Rectangle(10, 5);
            DrawSquare((Square)rect);

            // Converting an int to a Square.
            Square sq2 = (Square)90;
            Console.WriteLine("sq2 = {0}", sq2);
            // Converting a Square to an int.
            int side = (int)sq2;
            Console.WriteLine("Side length of sq2 = {0}", side);

            Square s3 = new Square { Length = 83 };
            // Attempt to make an implicit cast?
            Rectangle rect2 = s3;

            Console.WriteLine("rect2 = {0}", rect2);
            // Explicit cast syntax still OK!
            Square s4 = new Square { Length = 3 };
            Rectangle rect3 = (Rectangle)s4;
            Console.WriteLine("rect3 = {0}", rect3);

            Console.ReadLine();
        }
        // This method requires a Square type.
        static void DrawSquare(Square sq)
        {
            Console.WriteLine(sq.ToString());
            sq.Draw();
        }
    }
}
