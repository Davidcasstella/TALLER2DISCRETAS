using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ordenamiento
{
    public partial class Form1 : Form
    {
        // Configuración
        private int size;
        private int limitDown;
        private int limitUp;

        // Los 4 conjuntos de datos base
        private int[] datosCompletamenteAleatorios;
        private int[] datosLevementeOrdenadosAsc;
        private int[] datosLevementeOrdenadosDesc;
        private int[] datosCompletamenteOrdenadosAsc;

        private bool updatingRichTextBox = false;

        public Form1()
        {
            InitializeComponent();
            ConfigurarInterfaz();
        }

        private void ConfigurarInterfaz()
        {
            ConfigurarTitulosGraficos();
            ConfigurarDataGridViewEstadisticas();
            ConfigurarDataGridViewTiempos();
            ConfigurarGraficoComparativo();

            labelStatus.Text = "Listo para generar datos."; // Asegúrate de tener un Label llamado labelStatus en el formulario
        }

        private void ConfigurarTitulosGraficos()
        {
            chart1.Titles.Clear();
            chart1.Titles.Add("Datos Completamente Aleatorios - Bubble Sort");

            chart2.Titles.Clear();
            chart2.Titles.Add("Datos Levemente Ordenados Ascendente - Bubble Sort");

            chart3.Titles.Clear();
            chart3.Titles.Add("Datos Levemente Ordenados Descendente - Bubble Sort");

            chart4.Titles.Clear();
            chart4.Titles.Add("Datos Completamente Ordenados Ascendente - Bubble Sort");

            chart5.Titles.Clear();
            chart5.Titles.Add("Datos Completamente Aleatorios - Radix Sort");

            chart6.Titles.Clear();
            chart6.Titles.Add("Datos Levemente Ordenados Ascendente - Radix Sort");

            chart7.Titles.Clear();
            chart7.Titles.Add("Datos Levemente Ordenados Descendente - Radix Sort");

            chart8.Titles.Clear();
            chart8.Titles.Add("Datos Completamente Ordenados Ascendente - Radix Sort");
        }

        private void ConfigurarDataGridViewEstadisticas()
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("Funcion", "Función");
            dataGridView2.Columns.Add("Min", "Valor mínimo");
            dataGridView2.Columns.Add("Max", "Valor máximo");
            dataGridView2.Columns.Add("CountMin", "Veces valor mínimo");
            dataGridView2.Columns.Add("CountMax", "Veces valor máximo");
            dataGridView2.Columns.Add("Average", "Promedio");
            dataGridView2.Columns.Add("Median", "Mediana");
            dataGridView2.Columns.Add("Sum", "Suma total");
            dataGridView2.Columns.Add("Mode", "Moda");
        }

        private void ConfigurarDataGridViewTiempos()
        {
            dataGridViewTiming.Columns.Clear();
            dataGridViewTiming.Columns.Add("Grupo", "Tipo de Datos");
            dataGridViewTiming.Columns.Add("Bubble", "Bubble Sort (ms)");
            dataGridViewTiming.Columns.Add("Radix", "Radix Sort (ms)");
        }

        private void ConfigurarGraficoComparativo()
        {
            chartTiming.Series.Clear();
            chartTiming.ChartAreas.Clear();
            chartTiming.ChartAreas.Add(new ChartArea());
            chartTiming.Legends.Clear();
            chartTiming.Legends.Add(new Legend("Legend"));

            chartTiming.Series.Add(new Series("Bubble Sort") { ChartType = SeriesChartType.Column });
            chartTiming.Series.Add(new Series("Radix Sort") { ChartType = SeriesChartType.Column });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                labelStatus.Text = "Validando entradas...";
                if (!ValidarEntradas())
                {
                    return;
                }

                labelStatus.Text = "Generando conjuntos de datos...";
                LimpiarTodo();

                GenerarConjuntosDeDatos();

                labelStatus.Text = "Mostrando datos en gráficos...";
                MostrarDatosEnGraficos();

                labelStatus.Text = "Ordenando y calculando estadísticas...";
                OrdenarYMostrarEstadisticas();

                labelStatus.Text = "Midiendo tiempos de ejecución...";
                MedirYMostrarTiempos();

                labelStatus.Text = "Mostrando resumen estadístico...";
                MostrarResumenEstadistico();

                labelStatus.Text = "Proceso completado correctamente.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelStatus.Text = "Error durante el proceso.";
            }
        }

        private bool ValidarEntradas()
        {
            if (!int.TryParse(textBox1.Text, out size) || size <= 0)
            {
                MessageBox.Show("Ingrese un tamaño válido (entero positivo).", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                labelStatus.Text = "Error: Tamaño inválido.";
                return false;
            }

            if (!int.TryParse(textBox2.Text, out limitDown))
            {
                MessageBox.Show("Ingrese un límite inferior válido (entero).", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                labelStatus.Text = "Error: Límite inferior inválido.";
                return false;
            }

            if (!int.TryParse(textBox3.Text, out limitUp))
            {
                MessageBox.Show("Ingrese un límite superior válido (entero).", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                labelStatus.Text = "Error: Límite superior inválido.";
                return false;
            }

            if (limitDown >= limitUp)
            {
                MessageBox.Show("El límite inferior debe ser menor que el límite superior.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                labelStatus.Text = "Error: Rango inválido.";
                return false;
            }

            return true;
        }

        private void GenerarConjuntosDeDatos()
        {
            Random random = new Random();

            // 1. Datos completamente aleatorios
            datosCompletamenteAleatorios = GenerarDatosAleatorios(random);

            // 2. Datos levemente ordenados ascendente en 5 grupos
            datosLevementeOrdenadosAsc = GenerarDatosLevementeOrdenadosAsc5Grupos(random);

            // 3. Datos levemente ordenados descendente en 5 grupos
            datosLevementeOrdenadosDesc = GenerarDatosLevementeOrdenadosDesc5Grupos(random);

            // 4. Datos completamente ordenados ascendente
            datosCompletamenteOrdenadosAsc = GenerarDatosCompletamenteOrdenadosAsc();
        }

        private int[] GenerarDatosAleatorios(Random random)
        {
            int[] datos = new int[size];
            for (int i = 0; i < size; i++)
            {
                datos[i] = random.Next(limitDown, limitUp + 1);
            }
            return datos;
        }

        // Nueva implementación: Distribuir en 5 grupos ordenados ascendente con leve aleatoriedad dentro de cada grupo
        private int[] GenerarDatosLevementeOrdenadosAsc5Grupos(Random random)
        {
            int[] datos = new int[size];
            int grupos = 5;
            int grupoTamaño = size / grupos;
            int restante = size % grupos;
            int rangoTotal = limitUp - limitDown + 1;
            int rangoPorGrupo = rangoTotal / grupos;

            int index = 0;
            for (int g = 0; g < grupos; g++)
            {
                // Rango de valores para el grupo actual
                int grupoInicio = limitDown + g * rangoPorGrupo;
                int grupoFin = (g == grupos - 1) ? limitUp : grupoInicio + rangoPorGrupo - 1;

                int actualGrupoTamaño = grupoTamaño + (g < restante ? 1 : 0);

                // Generar valores dentro del rango para este grupo con leve aleatoriedad
                for (int i = 0; i < actualGrupoTamaño; i++, index++)
                {
                    int valor = random.Next(grupoInicio, grupoFin + 1);
                    datos[index] = valor;
                }
            }

            // Ordenar el array para que esté levemente ordenado (por grupos ya en orden)
            Array.Sort(datos);

            // Pequeñas permutaciones para romper orden perfecto
            int permutaciones = size / 20; // 5% permutaciones
            for (int i = 0; i < permutaciones; i++)
            {
                int pos1 = random.Next(size);
                int pos2 = random.Next(size);
                int temp = datos[pos1];
                datos[pos1] = datos[pos2];
                datos[pos2] = temp;
            }

            return datos;
        }

        // Similar pero descendente en 5 grupos
        private int[] GenerarDatosLevementeOrdenadosDesc5Grupos(Random random)
        {
            int[] datos = new int[size];
            int grupos = 5;
            int grupoTamaño = size / grupos;
            int restante = size % grupos;
            int rangoTotal = limitUp - limitDown + 1;
            int rangoPorGrupo = rangoTotal / grupos;

            int index = 0;
            for (int g = grupos - 1; g >= 0; g--)
            {
                int grupoInicio = limitDown + g * rangoPorGrupo;
                int grupoFin = (g == grupos - 1) ? limitUp : grupoInicio + rangoPorGrupo - 1;

                int actualGrupoTamaño = grupoTamaño + (g < restante ? 1 : 0);

                for (int i = 0; i < actualGrupoTamaño; i++, index++)
                {
                    int valor = random.Next(grupoInicio, grupoFin + 1);
                    datos[index] = valor;
                }
            }

            Array.Sort(datos);
            Array.Reverse(datos);

            int permutaciones = size / 20; // 5% permutaciones
            for (int i = 0; i < permutaciones; i++)
            {
                int pos1 = random.Next(size);
                int pos2 = random.Next(size);
                int temp = datos[pos1];
                datos[pos1] = datos[pos2];
                datos[pos2] = temp;
            }

            return datos;
        }

        private int[] GenerarDatosCompletamenteOrdenadosAsc()
        {
            int[] datos = new int[size];
            int rango = limitUp - limitDown;

            for (int i = 0; i < size; i++)
            {
                datos[i] = limitDown + (i * rango / size);
            }

            return datos;
        }

        private void MostrarDatosEnGraficos()
        {
            // Mostrar datos originales (sin ordenar) en gráficos
            MostrarDatosEnGrafico(datosCompletamenteAleatorios, chart1);
            MostrarDatosEnGrafico(datosLevementeOrdenadosAsc, chart2);
            MostrarDatosEnGrafico(datosLevementeOrdenadosDesc, chart3);
            MostrarDatosEnGrafico(datosCompletamenteOrdenadosAsc, chart4);

            MostrarDatosEnGrafico(datosCompletamenteAleatorios, chart5);
            MostrarDatosEnGrafico(datosLevementeOrdenadosAsc, chart6);
            MostrarDatosEnGrafico(datosLevementeOrdenadosDesc, chart7);
            MostrarDatosEnGrafico(datosCompletamenteOrdenadosAsc, chart8);
        }

        private void OrdenarYMostrarEstadisticas()
        {
            // Copias para Bubble Sort
            int[] bubbleAleatorios = (int[])datosCompletamenteAleatorios.Clone();
            int[] bubbleLevementeAsc = (int[])datosLevementeOrdenadosAsc.Clone();
            int[] bubbleLevementeDesc = (int[])datosLevementeOrdenadosDesc.Clone();
            int[] bubbleCompletamenteAsc = (int[])datosCompletamenteOrdenadosAsc.Clone();

            // Ordenar con Bubble Sort
            BubbleSort(bubbleAleatorios);
            BubbleSort(bubbleLevementeAsc);
            BubbleSort(bubbleLevementeDesc);
            BubbleSort(bubbleCompletamenteAsc);

            // Mostrar estadísticas Bubble Sort
            CalcularYMostrarEstadisticas(bubbleAleatorios, "Bubble Sort - Completamente Aleatorios");
            CalcularYMostrarEstadisticas(bubbleLevementeAsc, "Bubble Sort - Levemente Ordenados Asc");
            CalcularYMostrarEstadisticas(bubbleLevementeDesc, "Bubble Sort - Levemente Ordenados Desc");
            CalcularYMostrarEstadisticas(bubbleCompletamenteAsc, "Bubble Sort - Completamente Ordenados Asc");

            // Copias para Radix Sort
            int[] radixAleatorios = (int[])datosCompletamenteAleatorios.Clone();
            int[] radixLevementeAsc = (int[])datosLevementeOrdenadosAsc.Clone();
            int[] radixLevementeDesc = (int[])datosLevementeOrdenadosDesc.Clone();
            int[] radixCompletamenteAsc = (int[])datosCompletamenteOrdenadosAsc.Clone();

            // Ordenar con Radix Sort
            RadixSort(radixAleatorios);
            RadixSort(radixLevementeAsc);
            RadixSort(radixLevementeDesc);
            RadixSort(radixCompletamenteAsc);

            // Mostrar estadísticas Radix Sort
            CalcularYMostrarEstadisticas(radixAleatorios, "Radix Sort - Completamente Aleatorios");
            CalcularYMostrarEstadisticas(radixLevementeAsc, "Radix Sort - Levemente Ordenados Asc");
            CalcularYMostrarEstadisticas(radixLevementeDesc, "Radix Sort - Levemente Ordenados Desc");
            CalcularYMostrarEstadisticas(radixCompletamenteAsc, "Radix Sort - Completamente Ordenados Asc");
        }

        private void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }

        private void RadixSort(int[] arr)
        {
            if (arr.Length == 0) return;

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

        private void MostrarDatosEnGrafico(int[] datos, Chart chart)
        {
            if (chart.Series.Count == 0)
            {
                chart.Series.Add(new Series("Datos") { ChartType = SeriesChartType.Line });
            }
            chart.Series[0].Points.Clear();
            foreach (var valor in datos)
            {
                chart.Series[0].Points.Add(valor);
            }
        }

        private void CalcularYMostrarEstadisticas(int[] datos, string etiqueta)
        {
            int min = datos.Min();
            int max = datos.Max();
            int countMin = datos.Count(x => x == min);
            int countMax = datos.Count(x => x == max);
            double promedio = datos.Average();
            double mediana = CalcularMediana(datos);
            long suma = datos.Sum(x => (long)x);
            int moda = CalcularModa(datos);

            dataGridView2.Rows.Add(
                etiqueta,
                min,
                max,
                countMin,
                countMax,
                promedio.ToString("F2"),
                mediana.ToString("F2"),
                suma,
                moda
            );
        }

        private double CalcularMediana(int[] datos)
        {
            int[] datosOrdenados = (int[])datos.Clone();
            Array.Sort(datosOrdenados);

            int n = datosOrdenados.Length;
            if (n % 2 == 1)
                return datosOrdenados[n / 2];
            else
                return (datosOrdenados[(n / 2) - 1] + datosOrdenados[n / 2]) / 2.0;
        }

        private int CalcularModa(int[] datos)
        {
            var grupos = datos.GroupBy(v => v)
                             .OrderByDescending(g => g.Count())
                             .ThenBy(g => g.Key)
                             .ToList();

            return grupos.First().Key;
        }

        private void MedirYMostrarTiempos()
        {
            Stopwatch stopwatch = new Stopwatch();
            Dictionary<string, long> tiemposBubble = new Dictionary<string, long>();
            Dictionary<string, long> tiemposRadix = new Dictionary<string, long>();

            string[] tiposDatos = {
                "Completamente Aleatorios",
                "Levemente Ordenados Asc",
                "Levemente Ordenados Desc",
                "Completamente Ordenados Asc"
            };

            int[][] conjuntosDatos = {
                datosCompletamenteAleatorios,
                datosLevementeOrdenadosAsc,
                datosLevementeOrdenadosDesc,
                datosCompletamenteOrdenadosAsc
            };

            // Medir Bubble Sort
            for (int i = 0; i < tiposDatos.Length; i++)
            {
                int[] temp = (int[])conjuntosDatos[i].Clone();
                stopwatch.Restart();
                BubbleSort(temp);
                stopwatch.Stop();
                tiemposBubble[tiposDatos[i]] = stopwatch.ElapsedMilliseconds;
            }

            // Medir Radix Sort
            for (int i = 0; i < tiposDatos.Length; i++)
            {
                int[] temp = (int[])conjuntosDatos[i].Clone();
                stopwatch.Restart();
                RadixSort(temp);
                stopwatch.Stop();
                tiemposRadix[tiposDatos[i]] = stopwatch.ElapsedMilliseconds;
            }

            // Mostrar en tabla
            dataGridViewTiming.Rows.Clear();
            foreach (var tipo in tiposDatos)
            {
                dataGridViewTiming.Rows.Add(tipo, tiemposBubble[tipo], tiemposRadix[tipo]);
            }

            // Mostrar en gráfico
            var bubbleSeries = chartTiming.Series["Bubble Sort"];
            var radixSeries = chartTiming.Series["Radix Sort"];

            bubbleSeries.Points.Clear();
            radixSeries.Points.Clear();

            foreach (var tipo in tiposDatos)
            {
                bubbleSeries.Points.AddXY(tipo, tiemposBubble[tipo]);
                radixSeries.Points.AddXY(tipo, tiemposRadix[tipo]);
            }
        }

        private void MostrarResumenEstadistico()
        {
            updatingRichTextBox = true;
            richTextBoxStats.Clear();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;

                string funcion = row.Cells[0].Value?.ToString() ?? "";
                string min = row.Cells[1].Value?.ToString() ?? "";
                string max = row.Cells[2].Value?.ToString() ?? "";
                string countMin = row.Cells[3].Value?.ToString() ?? "";
                string countMax = row.Cells[4].Value?.ToString() ?? "";
                string promedio = row.Cells[5].Value?.ToString() ?? "";
                string mediana = row.Cells[6].Value?.ToString() ?? "";
                string suma = row.Cells[7].Value?.ToString() ?? "";
                string moda = row.Cells[8].Value?.ToString() ?? "";

                richTextBoxStats.AppendText($"Función: {funcion}\n");
                richTextBoxStats.AppendText($"  - Valor mínimo: {min}\n");
                richTextBoxStats.AppendText($"  - Valor máximo: {max}\n");
                richTextBoxStats.AppendText($"  - Veces que aparece el valor mínimo: {countMin}\n");
                richTextBoxStats.AppendText($"  - Veces que aparece el valor máximo: {countMax}\n");
                richTextBoxStats.AppendText($"  - Promedio: {promedio}\n");
                richTextBoxStats.AppendText($"  - Mediana: {mediana}\n");
                richTextBoxStats.AppendText($"  - Suma total: {suma}\n");
                richTextBoxStats.AppendText($"  - Moda: {moda}\n");
                richTextBoxStats.AppendText("\n");
            }

            updatingRichTextBox = false;
        }

        private void LimpiarTodo()
        {
            // Limpiar gráficos
            LimpiarGrafico(chart1);
            LimpiarGrafico(chart2);
            LimpiarGrafico(chart3);
            LimpiarGrafico(chart4);
            LimpiarGrafico(chart5);
            LimpiarGrafico(chart6);
            LimpiarGrafico(chart7);
            LimpiarGrafico(chart8);

            // Limpiar tablas
            dataGridView2.Rows.Clear();
            dataGridViewTiming.Rows.Clear();

            // Limpiar gráfico comparativo
            if (chartTiming.Series.IndexOf("Bubble Sort") >= 0)
                chartTiming.Series["Bubble Sort"].Points.Clear();
            if (chartTiming.Series.IndexOf("Radix Sort") >= 0)
                chartTiming.Series["Radix Sort"].Points.Clear();

            // Limpiar resumen
            richTextBoxStats.Clear();
        }

        private void LimpiarGrafico(Chart chart)
        {
            if (chart.Series.Count > 0)
                chart.Series[0].Points.Clear();
        }

        private void richTextBoxStats_TextChanged(object sender, EventArgs e)
        {
            if (updatingRichTextBox)
                return;
        }
        
                
    }
}
