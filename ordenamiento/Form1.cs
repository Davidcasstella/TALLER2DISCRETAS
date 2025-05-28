using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ordenamiento
{
    public partial class Form1 : Form
    {
        int[] listFewUnique, listNearlysorted, listReversed;
        int size;
        int limitDown, limitUp;
        Stopwatch stopwatch;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Clearcharts();

            size = Convert.ToInt32(textBox1.Text);
            limitDown = Convert.ToInt32(textBox2.Text);
            limitUp = Convert.ToInt32(textBox3.Text);
            listFewUnique = new int[size];

            stopwatch = new Stopwatch();
            stopwatch.Start();

            Random random = new Random();

            for (int i = 0; i < size; i++) 
            {
                listFewUnique[i] = random.Next(limitDown, limitUp + 1);
                //dataGridView1.Rows.Add(list[i].ToString());
                chart1.Series[0].Points.Add(listFewUnique[i]);
            }

            listNearlysorted = new int[size];

            random = new Random();

            for (int i = 0; i < size; i++)
            {
                listNearlysorted[i] = random.Next( (i/10) * 10 , ( (i / 10) * 10 ) + 11 );
                //dataGridView1.Rows.Add(list[i].ToString());
                chart2.Series[0].Points.Add(listNearlysorted[i]);
            }

            listReversed = listNearlysorted;
            Array.Sort(listReversed);
            Array.Reverse(listReversed);

            random = new Random();

            for (int i = 0; i < size; i++)
            {
                chart3.Series[0].Points.Add(listReversed[i]);
            }

            stopwatch.Stop();

            dataGridView2.Rows.Add("Genrador de Reversed",
                stopwatch.Elapsed.Seconds.ToString() + ": " + stopwatch.Elapsed.Milliseconds.ToString(),
                stopwatch.ElapsedMilliseconds.ToString(),
                size.ToString()
                );

            //Insertion Sort

            int[] insertionFewUnique = listFewUnique;
            int[] insertionNearlySorted = listNearlysorted;
            int[] insertionReversed = listReversed;

            stopwatch = new Stopwatch();
            stopwatch.Start();

            insertionFewUnique = InsertionSort(insertionFewUnique);

            stopwatch.Stop();

            dataGridView2.Rows.Add("Insertion Fewunique",
                stopwatch.Elapsed.Seconds.ToString() + ": " + stopwatch.Elapsed.Milliseconds.ToString(),
                stopwatch.ElapsedMilliseconds.ToString(),
                size.ToString()
                );

            stopwatch = new Stopwatch();
            stopwatch.Start();

            insertionNearlySorted = InsertionSort(insertionNearlySorted);

            stopwatch.Stop();

            dataGridView2.Rows.Add("Insertion NearlySorted",
                stopwatch.Elapsed.Seconds.ToString() + ": " + stopwatch.Elapsed.Milliseconds.ToString(),
                stopwatch.ElapsedMilliseconds.ToString(),
                size.ToString()
                );

            stopwatch = new Stopwatch();
            stopwatch.Start();

            insertionReversed = InsertionSort(insertionReversed);

            stopwatch.Stop();

            dataGridView2.Rows.Add("Insertion reversed",
                stopwatch.Elapsed.Seconds.ToString() + ": " + stopwatch.Elapsed.Milliseconds.ToString(),
                stopwatch.ElapsedMilliseconds.ToString(),
                size.ToString()
                );

            for (int i = 0; i < size; i++)
            {
                chart4.Series[0].Points.Add(insertionFewUnique[i]);
                chart5.Series[0].Points.Add(insertionNearlySorted[i]);
                chart6.Series[0].Points.Add(insertionReversed[i]);
            }

            //bubbleSort

            int[] BubbleFewUnique = listFewUnique;
            int[] BubbleNearlySorted = listNearlysorted;
            int[] BubbleReversed = listReversed;

            stopwatch = new Stopwatch();
            stopwatch.Start();

            BubbleFewUnique = BubbleSort(BubbleFewUnique);

            stopwatch.Stop();

            dataGridView2.Rows.Add("Bubble Fewunique",
                stopwatch.Elapsed.Seconds.ToString() + ": " + stopwatch.Elapsed.Milliseconds.ToString(),
                stopwatch.ElapsedMilliseconds.ToString(),
                size.ToString()
                );

            stopwatch = new Stopwatch();
            stopwatch.Start();

            BubbleNearlySorted = BubbleSort(BubbleNearlySorted);

            stopwatch.Stop();

            dataGridView2.Rows.Add("Bubble NearlySorted",
                stopwatch.Elapsed.Seconds.ToString() + ": " + stopwatch.Elapsed.Milliseconds.ToString(),
                stopwatch.ElapsedMilliseconds.ToString(),
                size.ToString()
                );

            stopwatch = new Stopwatch();
            stopwatch.Start();

            BubbleReversed = BubbleSort(BubbleReversed);

            stopwatch.Stop();

            dataGridView2.Rows.Add("Bubble reversed",
                stopwatch.Elapsed.Seconds.ToString() + ": " + stopwatch.Elapsed.Milliseconds.ToString(),
                stopwatch.ElapsedMilliseconds.ToString(),
                size.ToString()
                );

            for (int i = 0; i < size; i++)
            {
                chart7.Series[0].Points.Add(BubbleFewUnique[i]);
                chart8.Series[0].Points.Add(BubbleNearlySorted[i]);
                chart9.Series[0].Points.Add(BubbleReversed[i]);
            }


        }

        void Clearcharts()
        {
            dataGridView1.Rows.Clear();
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart3.Series[0].Points.Clear();
            chart4.Series[0].Points.Clear();
            chart5.Series[0].Points.Clear();
            chart6.Series[0].Points.Clear();
            chart7.Series[0].Points.Clear();
            chart8.Series[0].Points.Clear();
            chart9.Series[0].Points.Clear();
        }


        int[] InsertionSort(int[] inputArray)
        {
            for (int i = 1; i < inputArray.Length; i++)
            {
                int key = inputArray[i];
                int j = i - 1;
                while (j >= 0 && inputArray[j] > key)
                {
                    inputArray[j + 1] = inputArray[j];
                    j--;
                }
                inputArray[j + 1] = key;
            }
            return inputArray;
        }

        int[] BubbleSort(int[] inputArray)
        {
            int n = inputArray.Length;
            for (int i = 0; i < n - 1; i++)
            {
                //bool swapped = false;
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (inputArray[j] > inputArray[j + 1])
                    {
                        int temp = inputArray[j];
                        inputArray[j] = inputArray[j + 1];
                        inputArray[j + 1] = temp;
                        //swapped = true;
                    }
                }
                //if (!swapped) break;
            }
            return inputArray;
        }
    }
}
