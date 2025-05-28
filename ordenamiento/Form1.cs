using System;
using System.Linq;
using System.Windows.Forms;

namespace ordenamiento
{
    public partial class Form1 : Form
    {
        int size;
        int limitDown, limitUp;
        int groups = 5;


        // Datos originales
        int[] dataRandom;
        int[] dataNearlySortedAsc;
        int[] dataNearlySortedDesc;
        int[] dataSortedAsc;

        public Form1()
        {
            InitializeComponent();

            // Opcional: configurar los títulos al cargar el formulario
            SetupChartTitles();
        }

        private void SetupChartTitles()
        {// Títulos gráficos sin ordenar (tableLayoutPanel1) con Bubble Sort (aunque aquí no ordenamos realmente, pones el nombre)
            chart1.Titles.Clear();
            chart1.Titles.Add("Datos totalmente aleatorios - Bubble Sort");

            chart2.Titles.Clear();
            chart2.Titles.Add("Datos aleatorios levemente ordenados en forma ascendente - Bubble Sort");

            chart3.Titles.Clear();
            chart3.Titles.Add("Datos aleatorios levemente ordenados en forma descendente - Bubble Sort");

            chart4.Titles.Clear();
            chart4.Titles.Add("Datos ordenados completamente en forma ascendente - Bubble Sort");

            // Títulos gráficos ordenados Radix Sort (tableLayoutPanel2)
            chart5.Titles.Clear();
            chart5.Titles.Add("Datos totalmente aleatorios - Radix Sort");

            chart6.Titles.Clear();
            chart6.Titles.Add("Datos aleatorios levemente ordenados en forma ascendente - Radix Sort");

            chart7.Titles.Clear();
            chart7.Titles.Add("Datos aleatorios levemente ordenados en forma descendente - Radix Sort");

            chart8.Titles.Clear();
            chart8.Titles.Add("Datos ordenados completamente en forma ascendente - Radix Sort");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Clearcharts();
            dataGridView2.Rows.Clear();

            size = Convert.ToInt32(textBox1.Text);
            limitDown = Convert.ToInt32(textBox2.Text);
            limitUp = Convert.ToInt32(textBox3.Text);

            GenerateData();

            // Mostrar datos originales sin ordenar en charts 1-4
            ShowDataInChart(dataRandom, chart1);
            ShowDataInChart(dataNearlySortedAsc, chart2);
            ShowDataInChart(dataNearlySortedDesc, chart3);
            ShowDataInChart(dataSortedAsc, chart4);

            // Estadísticas datos sin ordenar
            CalculateAndDisplayStats(dataRandom, "Datos Aleatorios");
            CalculateAndDisplayStats(dataNearlySortedAsc, "Levemente Ordenados Asc");
            CalculateAndDisplayStats(dataNearlySortedDesc, "Levemente Ordenados Desc");
            CalculateAndDisplayStats(dataSortedAsc, "Completamente Ordenados Asc");

            // Clonar datos para ordenar con Radix Sort
            int[] dataRandomRadix = (int[])dataRandom.Clone();
            int[] dataNearlySortedAscRadix = (int[])dataNearlySortedAsc.Clone();
            int[] dataNearlySortedDescRadix = (int[])dataNearlySortedDesc.Clone();
            int[] dataSortedAscRadix = (int[])dataSortedAsc.Clone();

            // Ordenar con Radix Sort
            RadixSort(dataRandomRadix);
            RadixSort(dataNearlySortedAscRadix);
            RadixSort(dataNearlySortedDescRadix);
            RadixSort(dataSortedAscRadix);

            // Mostrar datos ordenados con Radix Sort en charts 5-8
            ShowDataInChart(dataRandom, chart5);            // Datos totalmente aleatorios
            ShowDataInChart(dataNearlySortedAsc, chart6);   // Datos aleatorios levemente ordenados ascendente
            ShowDataInChart(dataNearlySortedDesc, chart7);  // Datos aleatorios levemente ordenados descendente
            ShowDataInChart(dataSortedAsc, chart8);         // Datos completamente ordenados ascendente


            // Estadísticas datos ordenados Radix Sort
            CalculateAndDisplayStats(dataRandomRadix, "Radix Sort - Datos Aleatorios");
            CalculateAndDisplayStats(dataNearlySortedAscRadix, "Radix Sort - Levemente Ordenados Asc");
            CalculateAndDisplayStats(dataNearlySortedDescRadix, "Radix Sort - Levemente Ordenados Desc");
            CalculateAndDisplayStats(dataSortedAscRadix, "Radix Sort - Completamente Ordenados Asc");
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

            // 2. Datos levemente ordenados ascendente en 5 grupos según rango
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

            // 3. Datos levemente ordenados descendente en 5 grupos según rango
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

        private void RadixSort(int[] arr)
        {
            int max = arr.Max();

            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountingSortByDigit(arr, exp);
            }
        }

        private void CountingSortByDigit(int[] arr, int exp)
        {
            int n = arr.Length;
            int[] output = new int[n];
            int[] count = new int[10];

            for (int i = 0; i < n; i++)
                count[(arr[i] / exp) % 10]++;

            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            for (int i = n - 1; i >= 0; i--)
            {
                int digit = (arr[i] / exp) % 10;
                output[count[digit] - 1] = arr[i];
                count[digit]--;
            }

            for (int i = 0; i < n; i++)
                arr[i] = output[i];
        }

        void Clearcharts()
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart3.Series[0].Points.Clear();
            chart4.Series[0].Points.Clear();
            chart5.Series[0].Points.Clear();
            chart6.Series[0].Points.Clear();
            chart7.Series[0].Points.Clear();
            chart8.Series[0].Points.Clear();
        }
    }
}
