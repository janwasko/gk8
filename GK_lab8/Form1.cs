using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GK_lab8
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Pen p1;
        private SolidBrush br1;
        private int n = 10;
        private Random rand;
        private Point[] randomPointsArray;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            p1 = new Pen(Color.Black);
            br1 = new SolidBrush(Color.Crimson);
            rand = new Random();
            randomPointsArray = new Point[n];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            for (int i = 0; i < n; i++)
            {
                randomPointsArray[i].X = rand.Next(0, 300);
                randomPointsArray[i].Y = rand.Next(0, 300);
                g.FillEllipse(br1, randomPointsArray[i].X, randomPointsArray[i].Y, 6, 6);
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            int l = 0;

            List<Point> otoczka = new List<Point>();

            for (int i = 0; i < n; i++)
            {
                if (randomPointsArray[i].X < randomPointsArray[l].X)
                {
                    l = i;
                }
            }

            int p = l;
            int q;
            int j = 0;

            do
            {
                otoczka.Add(randomPointsArray[p]);
                q = (p + 1) % n;
                for (int k = 0; k < n; k++)
                {
                    if (Orientation(randomPointsArray[p], randomPointsArray[k], randomPointsArray[q]) == 2)
                    {
                        q = k;
                    }
                        
                }
                p = q;
                j++;
            } while (p != l);

            for (int i = 1; i < otoczka.Count; i++)
            {
                g.DrawLine(p1, otoczka[i - 1], otoczka[i]);
                if (i == otoczka.Count - 1)
                {
                    g.DrawLine(p1, otoczka[i], otoczka[0]);
                }
            }
        }
        int Orientation(Point p, Point q, Point r)
        {
            int val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);
            if (val == 0)
            {
                return 0;
            }
            return (val > 0) ? 1 : 2;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Point rootPoint;
            Point theLowestPoint = randomPointsArray[0];
            Point pom = randomPointsArray[0];

            for (int i = 0; i < n; i++)
            {
                if (theLowestPoint.Y > randomPointsArray[i].Y || (theLowestPoint.Y == randomPointsArray[i].Y && theLowestPoint.X > randomPointsArray[i].X))
                {
                    theLowestPoint = randomPointsArray[i];
                    pom = randomPointsArray[0];
                    randomPointsArray[0] = randomPointsArray[i];
                    randomPointsArray[i] = pom;
                }
            }

            rootPoint = theLowestPoint;

            for (int i = 1; i < n; ++i)
            {
                for (int y = i + 1; y < n; ++y)
                {
                    Point point1 = randomPointsArray[i];
                    Point point2 = randomPointsArray[y];
                    int order = pointOrder(rootPoint, point1, point2);
                    if (order == 0)
                    {
                        if (distance(rootPoint, point2) <= distance(rootPoint, point1))
                        {
                            swap(randomPointsArray, i, y);

                        }
                    }
                    else if (order == 1)
                    {
                        swap(randomPointsArray, i, y);
                    }
                }
            }
            
            List<Point> points = new List<Point>();
            points.Add(randomPointsArray[0]);
            points.Add(randomPointsArray[1]);
            points.Add(randomPointsArray[2]);
            
            for (int i = 3; i < n; i++)
            {
                while (pointOrder(points[points.Count - 2], points[points.Count - 1], randomPointsArray[i]) != 2 && points.Count > 1)
                    points.Remove(points[points.Count - 1]);

                points.Add(randomPointsArray[i]);
            }

            for (int i = 1; i < points.Count; i++)
            {
                g.DrawLine(p1, points[i - 1], points[i]);
            }

            g.DrawLine(p1, points[0], points[points.Count - 1]);
        }
        int pointOrder(Point a, Point b, Point c)
        {
            float area = (b.Y - a.Y) * (c.X - b.X) - (b.X - a.X) * (c.Y - b.Y);
            if (area > 0)
                return 1;
            else if (area < 0)
                return 2;
            return 0;
        }
        float distance(Point a, Point b)
        {
            float xLength = a.X - b.X;
            float yLength = a.Y - b.Y;
            return xLength * xLength + yLength * yLength;
        }
        void swap(Point[] pointsArray, int a, int b)
        {
            Point tmp = pointsArray[a];
            pointsArray[a] = pointsArray[b];
            pointsArray[b] = tmp;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}