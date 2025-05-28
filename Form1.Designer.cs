namespace ordenamiento
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox textBoxMin;
        private System.Windows.Forms.TextBox textBoxMax;
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.DataGridView dataGridViewStats;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartRandom;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNearlyAsc;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNearlyAscSorted;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNearlyDesc;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNearlyDescSorted;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRandomSorted;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSorted;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSortedSorted;

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // TextBoxes
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.textBoxMax = new System.Windows.Forms.TextBox();

            // DataGridViews
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.dataGridViewStats = new System.Windows.Forms.DataGridView();

            // Charts
            this.chartRandom = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartNearlyAsc = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartNearlyAscSorted = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartNearlyDesc = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartNearlyDescSorted = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartRandomSorted = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartSorted = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartSortedSorted = new System.Windows.Forms.DataVisualization.Charting.Chart();

            // 
            // textBoxMin
            // 
            this.textBoxMin.Location = new System.Drawing.Point(20, 20);
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(100, 26);
            // 
            // textBoxMax
            // 
            this.textBoxMax.Location = new System.Drawing.Point(140, 20);
            this.textBoxMax.Name = "textBoxMax";
            this.textBoxMax.Size = new System.Drawing.Size(100, 26);
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.Location = new System.Drawing.Point(20, 60);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.Size = new System.Drawing.Size(600, 300);
            // 
            // dataGridViewStats
            // 
            this.dataGridViewStats.Location = new System.Drawing.Point(640, 60);
            this.dataGridViewStats.Name = "dataGridViewStats";
            this.dataGridViewStats.Size = new System.Drawing.Size(600, 300);
            // 
            // chartRandom
            // 
            this.chartRandom.Location = new System.Drawing.Point(20, 380);
            this.chartRandom.Name = "chartRandom";
            this.chartRandom.Size = new System.Drawing.Size(400, 300);
            // 
            // chartNearlyAsc
            // 
            this.chartNearlyAsc.Location = new System.Drawing.Point(440, 380);
            this.chartNearlyAsc.Name = "chartNearlyAsc";
            this.chartNearlyAsc.Size = new System.Drawing.Size(400, 300);
            // 
            // chartNearlyAscSorted
            // 
            this.chartNearlyAscSorted.Location = new System.Drawing.Point(860, 380);
            this.chartNearlyAscSorted.Name = "chartNearlyAscSorted";
            this.chartNearlyAscSorted.Size = new System.Drawing.Size(400, 300);
            // 
            // chartNearlyDesc
            // 
            this.chartNearlyDesc.Location = new System.Drawing.Point(20, 700);
            this.chartNearlyDesc.Name = "chartNearlyDesc";
            this.chartNearlyDesc.Size = new System.Drawing.Size(400, 300);
            // 
            // chartNearlyDescSorted
            // 
            this.chartNearlyDescSorted.Location = new System.Drawing.Point(440, 700);
            this.chartNearlyDescSorted.Name = "chartNearlyDescSorted";
            this.chartNearlyDescSorted.Size = new System.Drawing.Size(400, 300);
            // 
            // chartRandomSorted
            // 
            this.chartRandomSorted.Location = new System.Drawing.Point(860, 700);
            this.chartRandomSorted.Name = "chartRandomSorted";
            this.chartRandomSorted.Size = new System.Drawing.Size(400, 300);
            // 
            // chartSorted
            // 
            this.chartSorted.Location = new System.Drawing.Point(1280, 380);
            this.chartSorted.Name = "chartSorted";
            this.chartSorted.Size = new System.Drawing.Size(400, 300);
            // 
            // chartSortedSorted
            // 
            this.chartSortedSorted.Location = new System.Drawing.Point(1280, 700);
            this.chartSortedSorted.Name = "chartSortedSorted";
            this.chartSortedSorted.Size = new System.Drawing.Size(400, 300);

            // Añadir controles al formulario
            this.Controls.Add(this.textBoxMin);
            this.Controls.Add(this.textBoxMax);
            this.Controls.Add(this.dataGridViewResults);
            this.Controls.Add(this.dataGridViewStats);

            this.Controls.Add(this.chartRandom);
            this.Controls.Add(this.chartNearlyAsc);
            this.Controls.Add(this.chartNearlyAscSorted);
            this.Controls.Add(this.chartNearlyDesc);
            this.Controls.Add(this.chartNearlyDescSorted);
            this.Controls.Add(this.chartRandomSorted);
            this.Controls.Add(this.chartSorted);
            this.Controls.Add(this.chartSortedSorted);

            // Ajustes adicionales del formulario (opcional)
            this.Text = "Ordenamiento y Análisis de Datos";
            this.ClientSize = new System.Drawing.Size(1700, 1050);
        }
    }
}
