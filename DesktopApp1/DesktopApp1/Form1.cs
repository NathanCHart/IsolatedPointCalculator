using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Simple desktop app made for ease of use,made in Visual Studio Community 2017

namespace DesktopApp1
{
    public partial class Form1 : Form
    {    
        public List<Point> coordinates;
        public string answer;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            coordinates = readCoordinates(Path.GetFullPath("problem_small.txt"));
            answer = isolatedPoint();
            MessageBox.Show(answer + " is the most isolated point");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            coordinates = readCoordinates(Path.GetFullPath("problem_big.txt"));
            answer = isolatedPoint();
            MessageBox.Show(answer + " is the most isolated point");
        }

        [STAThread]
        private List<Point> readCoordinates(string filePath)
        {
            //re-initialises to make sure previous button clicks are not stored
            coordinates = new List<Point>();

            //read through file, replacing new line script with a space, then splitting and sorting data into an array
            StreamReader sr = new StreamReader(filePath);
            string allWords = sr.ReadToEnd();
            allWords = allWords.Replace("\r\n", " ");
            string[] fullFile = allWords.Split(' ');

            //finds each coordinate point, no need for place number as each place is stored in text file in numerical order so place will correlate to point in the list
            for(var i=0; i<fullFile.Length; ++i)
            {
                if (i%3==0 || i ==0)
                    continue;
                else
                {
                    int x = Int32.Parse(fullFile[i]);
                    int y = Int32.Parse(fullFile[i + 1]);
                    Point p = new Point(x, y);
                    coordinates.Add(p);
                    ++i;
                }
            }

            return coordinates;   
        }

        private string isolatedPoint()
        {
            //most isolated distance needs to be initialized before equation can start
            double distanceOfMostIsolated = 0;
            double distance;
            for (int i = 0; i< coordinates.Count; ++i)
            {
                //two loops to compare all points against eachother
                for (int j = 0; j< coordinates.Count; ++j)
                {
                    //point cannot be compared against itelf
                    if (i == j)
                    {
                        continue;
                    }
                    else
                    {
                        //as distance being compared is relative distances, there is no need to compare actual distances by square rooting the equation, faster time to compute
                        distance = Math.Pow((coordinates[j].X - coordinates[i].X) , 2) + Math.Pow((coordinates[j].Y - coordinates[i].Y), 2);
                        //breaks the inner loop if the distance is less than a most isolated distance, as that point can no longer be the most isolated
                        if (distance < distanceOfMostIsolated)
                        {
                            break;
                        }
                        else if (distance > distanceOfMostIsolated)
                        {
                            //updates the most isolated point, i is the point in the list where that point is, which correlates with the original place in the txt file
                            distanceOfMostIsolated = distance;
                            answer = ("place" + i);
                        }
                    }
                }
            }
            return answer;
        }

       
    }
}
