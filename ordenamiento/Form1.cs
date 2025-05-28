using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ordenamiento
{
    public partial class Form1 : Form
    {
        int size;
        int limitDown, limitUp;
        int groups = 5;

        // Datos
        int[] dataRandom;
        int[] dataNearlySortedAsc;
        int[] dataNearlySortedDesc;
        int[] dataSortedAsc;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clearcharts();
            dataGridView2.Rows.Clear();

            size = Convert.ToInt32(textBox1.Text);
            limitDown = Convert.ToInt32(textBox2.Text);
            limitUp = Convert.ToInt32(textBox3.Text);

            GenerateData();

            ShowDataInChart(dataRandom, chart1);
            ShowDataInChart(dataNearlySortedAsc, chart2);
            ShowDataInChart(dataNearlySortedDesc, chart3);
            ShowDataInChart(dataSortedAsc, chart4);

            // Calcular y mostrar estadísticas para cada grupo
            CalculateAndDisplayStats(dataRandom, "Datos Aleatorios");
            CalculateAndDisplayStats(dataNearlySortedAsc, "Levemente Ordenados Asc");
            CalculateAndDisplayStats(dataNearlySortedDesc, "Levemente Ordenados Desc");
            CalculateAndDisplayStats(dataSortedAsc, "Completamente Ordenados Asc");
        }

        private void GenerateData()
        {
            Random random = new Random();

            dataRandom = new int[size];
            dataNearlySortedAsc = new int[size];
            dataNearlySortedDesc = new int[size];
            dataSortedAsc = new int[size];

            // 1. Datos totalmente aleatorios
            for (int i = 0; i < size; i++)
            {
                dataRandom[i] = random.Next(limitDown, limitUp + 1);
            }

            // 2. Datos levemente ordenados ascendente en 5 grupos
            int groupSize = size / groups;
            int rangeSize = (limitUp - limitDown + 1) / groups;

            for (int g = 0; g < groups; g++)
            {
                int groupMin = limitDown + g * rangeSize;
                int groupMax = groupMin + rangeSize - 1;

                for (int i = 0; i < groupSize; i++)
                {
                    int index = g * groupSize + i;
                    if (index < size)
                        dataNearlySortedAsc[index] = random.Next(groupMin, groupMax + 1);
                }
            }

            // 3. Datos levemente ordenados descendente en 5 grupos
            for (int g = 0; g < groups; g++)
            {
                int groupMin = limitDown + g * rangeSize;
                int groupMax = groupMin + rangeSize - 1;

                int descGroupIndex = groups - 1 - g;

                for (int i = 0; i < groupSize; i++)
                {
                    int index = descGroupIndex * groupSize + i;
                    if (index < size)
                        dataNearlySortedDesc[index] = random.Next(groupMin, groupMax + 1);
                }
            }

            // 4. Datos completamente ordenados ascendente
            int step = (limitUp - limitDown) / Math.Max(size - 1, 1);
            for (int i = 0; i < size; i++)
            {
                dataSortedAsc[i] = limitDown + i * step;
            }
        }

        private void ShowDataInChart(int[] data, System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            chart.Series[0].Points.Clear();
            foreach (var value in data)
            {
                chart.Series[0].Points.Add(value);
            }
        }

        private void CalculateAndDisplayStats(int[] data, string label)
        {
            int min = data.Min();
            int max = data.Max();
            int countMin = data.Count(x => x == min);
            int countMax = data.Count(x => x == max);
            double average = data.Average();
            int sum = data.Sum();

            double median = CalculateMedian(data);

            int mode = CalculateMode(data);

            dataGridView2.Rows.Add(
                label,
                min,
                max,
                countMin,
                countMax,
                average.ToString("F2"),
                median.ToString("F2"),
                sum,
                mode
            );
        }

        private double CalculateMedian(int[] data)
        {
            int[] sortedData = (int[])data.Clone();
            Array.Sort(sortedData);

            int n = sortedData.Length;
            if (n % 2 == 1)
                return sortedData[n / 2];
            else
                return (sortedData[(n / 2) - 1] + sortedData[n / 2]) / 2.0;
        }

        private int CalculateMode(int[] data)
        {
            var groups = data.GroupBy(v => v)
                             .OrderByDescending(g => g.Count())
                             .ThenBy(g => g.Key)
                             .ToList();

            return groups.First().Key;
        }

        void Clearcharts()
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart3.Series[0].Points.Clear();
            chart4.Series[0].Points.Clear();
        }
    }
}
