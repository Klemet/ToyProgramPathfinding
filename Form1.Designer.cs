namespace ToyProgramCH
{
	partial class Form1
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.RandomPerlinButton = new System.Windows.Forms.Button();
			this.PreProcessButton = new System.Windows.Forms.Button();
			this.GenerateDepartureArrival = new System.Windows.Forms.Button();
			this.FindLeastCost = new System.Windows.Forms.Button();
			this.LeastCostDijkstra = new System.Windows.Forms.Button();
			this.CreateRandomTestMap = new System.Windows.Forms.Button();
			this.AStarLeastCostPath = new System.Windows.Forms.Button();
			this.MapDimensionList = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.perlinFrequencyList = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.eventLog1 = new System.Diagnostics.EventLog();
			this.DisplayCostMap = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
			this.SuspendLayout();
			// 
			// RandomPerlinButton
			// 
			this.RandomPerlinButton.Location = new System.Drawing.Point(12, 12);
			this.RandomPerlinButton.Name = "RandomPerlinButton";
			this.RandomPerlinButton.Size = new System.Drawing.Size(105, 37);
			this.RandomPerlinButton.TabIndex = 3;
			this.RandomPerlinButton.Text = "Create random elevation map";
			this.RandomPerlinButton.UseVisualStyleBackColor = true;
			this.RandomPerlinButton.Click += new System.EventHandler(this.RandomPerlinButton_Click);
			// 
			// PreProcessButton
			// 
			this.PreProcessButton.Location = new System.Drawing.Point(257, 11);
			this.PreProcessButton.Name = "PreProcessButton";
			this.PreProcessButton.Size = new System.Drawing.Size(108, 37);
			this.PreProcessButton.TabIndex = 4;
			this.PreProcessButton.Text = "Pre-process CH algorithm";
			this.PreProcessButton.UseVisualStyleBackColor = true;
			this.PreProcessButton.Click += new System.EventHandler(this.PreProcessButton_Click);
			// 
			// GenerateDepartureArrival
			// 
			this.GenerateDepartureArrival.Location = new System.Drawing.Point(371, 11);
			this.GenerateDepartureArrival.Name = "GenerateDepartureArrival";
			this.GenerateDepartureArrival.Size = new System.Drawing.Size(110, 37);
			this.GenerateDepartureArrival.TabIndex = 5;
			this.GenerateDepartureArrival.Text = "Generate departure and arrival";
			this.GenerateDepartureArrival.UseVisualStyleBackColor = true;
			this.GenerateDepartureArrival.Click += new System.EventHandler(this.GenerateDepartureArrival_Click);
			// 
			// FindLeastCost
			// 
			this.FindLeastCost.Location = new System.Drawing.Point(487, 12);
			this.FindLeastCost.Name = "FindLeastCost";
			this.FindLeastCost.Size = new System.Drawing.Size(111, 36);
			this.FindLeastCost.TabIndex = 6;
			this.FindLeastCost.Text = "Find Least Cost Path with CH";
			this.FindLeastCost.UseVisualStyleBackColor = true;
			this.FindLeastCost.Click += new System.EventHandler(this.FindLeastCost_Click);
			// 
			// LeastCostDijkstra
			// 
			this.LeastCostDijkstra.Location = new System.Drawing.Point(604, 12);
			this.LeastCostDijkstra.Name = "LeastCostDijkstra";
			this.LeastCostDijkstra.Size = new System.Drawing.Size(116, 35);
			this.LeastCostDijkstra.TabIndex = 7;
			this.LeastCostDijkstra.Text = "Find Least Cost Path with Dijkstra";
			this.LeastCostDijkstra.UseVisualStyleBackColor = true;
			this.LeastCostDijkstra.Click += new System.EventHandler(this.LeastCostDijkstra_Click);
			// 
			// CreateRandomTestMap
			// 
			this.CreateRandomTestMap.Location = new System.Drawing.Point(128, 12);
			this.CreateRandomTestMap.Name = "CreateRandomTestMap";
			this.CreateRandomTestMap.Size = new System.Drawing.Size(94, 35);
			this.CreateRandomTestMap.TabIndex = 8;
			this.CreateRandomTestMap.Text = "Create random test map";
			this.CreateRandomTestMap.UseVisualStyleBackColor = true;
			this.CreateRandomTestMap.Click += new System.EventHandler(this.CreateRandomTestMap_Click);
			// 
			// AStarLeastCostPath
			// 
			this.AStarLeastCostPath.Location = new System.Drawing.Point(727, 12);
			this.AStarLeastCostPath.Name = "AStarLeastCostPath";
			this.AStarLeastCostPath.Size = new System.Drawing.Size(102, 35);
			this.AStarLeastCostPath.TabIndex = 9;
			this.AStarLeastCostPath.Text = "Find Least Cost Path with A*";
			this.AStarLeastCostPath.UseVisualStyleBackColor = true;
			this.AStarLeastCostPath.Click += new System.EventHandler(this.AStarLeastCostPath_Click);
			// 
			// MapDimensionList
			// 
			this.MapDimensionList.FormattingEnabled = true;
			this.MapDimensionList.Items.AddRange(new object[] {
            "20",
            "50",
            "100",
            "200",
            "400",
            "800",
            "1000"});
			this.MapDimensionList.Location = new System.Drawing.Point(101, 53);
			this.MapDimensionList.Name = "MapDimensionList";
			this.MapDimensionList.Size = new System.Drawing.Size(121, 21);
			this.MapDimensionList.TabIndex = 10;
			this.MapDimensionList.SelectedIndexChanged += new System.EventHandler(this.MapDimensionList_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 58);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Map dimensions";
			// 
			// perlinFrequencyList
			// 
			this.perlinFrequencyList.FormattingEnabled = true;
			this.perlinFrequencyList.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
			this.perlinFrequencyList.Location = new System.Drawing.Point(101, 80);
			this.perlinFrequencyList.Name = "perlinFrequencyList";
			this.perlinFrequencyList.Size = new System.Drawing.Size(121, 21);
			this.perlinFrequencyList.TabIndex = 12;
			this.perlinFrequencyList.SelectedIndexChanged += new System.EventHandler(this.perlinFrequencyList_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 83);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "Perlin frequency";
			// 
			// eventLog1
			// 
			this.eventLog1.SynchronizingObject = this;
			// 
			// DisplayCostMap
			// 
			this.DisplayCostMap.AutoSize = true;
			this.DisplayCostMap.Location = new System.Drawing.Point(243, 55);
			this.DisplayCostMap.Name = "DisplayCostMap";
			this.DisplayCostMap.Size = new System.Drawing.Size(108, 17);
			this.DisplayCostMap.TabIndex = 14;
			this.DisplayCostMap.Text = "Display Cost Map";
			this.DisplayCostMap.UseVisualStyleBackColor = true;
			this.DisplayCostMap.CheckedChanged += new System.EventHandler(this.DisplayCostMap_CheckedChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(932, 516);
			this.Controls.Add(this.DisplayCostMap);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.perlinFrequencyList);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.MapDimensionList);
			this.Controls.Add(this.AStarLeastCostPath);
			this.Controls.Add(this.CreateRandomTestMap);
			this.Controls.Add(this.LeastCostDijkstra);
			this.Controls.Add(this.FindLeastCost);
			this.Controls.Add(this.GenerateDepartureArrival);
			this.Controls.Add(this.PreProcessButton);
			this.Controls.Add(this.RandomPerlinButton);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button RandomPerlinButton;
		private System.Windows.Forms.Button PreProcessButton;
		private System.Windows.Forms.Button GenerateDepartureArrival;
		private System.Windows.Forms.Button FindLeastCost;
		private System.Windows.Forms.Button LeastCostDijkstra;
		private System.Windows.Forms.Button CreateRandomTestMap;
		private System.Windows.Forms.Button AStarLeastCostPath;
		private System.Windows.Forms.ComboBox MapDimensionList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox perlinFrequencyList;
		private System.Windows.Forms.Label label2;
		private System.Diagnostics.EventLog eventLog1;
		private System.Windows.Forms.CheckBox DisplayCostMap;
	}
}

