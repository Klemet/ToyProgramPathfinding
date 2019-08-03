using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace ToyProgramCH
{
	public partial class Form1 : Form
	{
		// Code to invoque the console
		// and the functions to make it move
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool AllocConsole();
		[DllImport("kernel32.dll", SetLastError = true)]
		static extern IntPtr GetConsoleWindow();
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		// Get the array ready for manipulations
		int[,] perlinMatrix;
		bool mapGenerated = false;
		bool preProcessingDone = false;
		bool departureArrivalGenerated = false;
		Map perlinMap;
		int initialControlsCount;
		int dimensions;
		int perlinFrequency;
		bool costMap = false;

		public Form1()
		{
			InitializeComponent();
			MapDimensionList.SelectedIndex = 4;
			Int32.TryParse(MapDimensionList.SelectedItem.ToString(), out dimensions);
			perlinFrequencyList.SelectedIndex = 3;
			Int32.TryParse(perlinFrequencyList.SelectedItem.ToString(), out perlinFrequency);
			perlinMatrix = new int[dimensions, dimensions];
			perlinMap = new Map(new int[] { dimensions, dimensions });
			initialControlsCount = this.Controls.Count;
			// we give a proper name to the form
			this.Text = "Toy Program for pathfinding algorithms (V.0.8)";
			// Give proper size to form and position
			this.Size = new System.Drawing.Size(1000, 1000);
			this.StartPosition = FormStartPosition.Manual;
			Location = new Point(200, 200);
			// Invoque the console
			AllocConsole();
			// Move the console to a good position to not hide behind the form
			IntPtr ptr = GetConsoleWindow();
			MoveWindow(ptr, 1200, 400, 600, 200, true);
			// Write stuff in the console
			// Console.Write("TEST");
			// Pop-up avec juste un message.
			// MessageBox.Show("Welcome !","Contraction Hierarchies Test Program " + (noisePerlin.Noise(0.04, 0.001, 0.003)));
			// Console.Write("Dimensions are " + dimensions + " (Text : " + MapDimensionList.SelectedText + ")\n");
		}


		private void MapDimensionList_SelectedIndexChanged(object sender, EventArgs e)
		{
			Int32.TryParse(MapDimensionList.SelectedItem.ToString(), out dimensions);
			perlinMap = new Map(new int[] { dimensions, dimensions });
			perlinMatrix = new int[dimensions, dimensions];
		}


		private void perlinFrequencyList_SelectedIndexChanged(object sender, EventArgs e)
		{
			Int32.TryParse(perlinFrequencyList.SelectedItem.ToString(), out perlinFrequency);
		}

		private void DisplayCostMap_CheckedChanged(object sender, EventArgs e)
		{
			if (DisplayCostMap.Checked == true) costMap = true;
			else costMap = false;
		}

		private void RandomPerlinButton_Click(object sender, EventArgs e)
		{
			// If there is already a picture box, we remove it.
			if (this.Controls.Count == initialControlsCount + 1) this.Controls.RemoveAt(initialControlsCount);
			// We create a new picture box
			PictureBox pb1 = new PictureBox();
			// We fill the perlinMatrix with perlin values
			Int32.TryParse(MapDimensionList.SelectedItem.ToString(), out dimensions);
			perlinMatrix = RasterFunctions.CreateArrayRandomPerlin(perlinFrequency, perlinMap);
			Console.Write("Minimum value of perlin Matrix : " + perlinMatrix.Cast<int>().Min() + ", maximum value : " + perlinMatrix.Cast<int>().Max() + "\n");
			// Initializing the nodes of the map
			perlinMap.ReinitializeMap();
			perlinMap.mapIsTestMap = false;
			perlinMap.InitializeNodes(perlinMatrix);
			perlinMap.InitializeLinks();
			// We transform the array to a bitmap
			pb1.Image = RasterFunctions.MapToBitmap(perlinMap, costMap);
			// We display everything nicely
			pb1.SizeMode = PictureBoxSizeMode.CenterImage;
			this.Size = new System.Drawing.Size(1000, 1000);
			pb1.Size = new System.Drawing.Size(1000, 1000);
			pb1.SizeMode = PictureBoxSizeMode.CenterImage;
			this.Controls.Add(pb1);



			// We also send the signal that pre-processing needs to be re-done but that the map
			// is now generated
			mapGenerated = true;
			preProcessingDone = false;
			departureArrivalGenerated = false;
		}

		private void CreateRandomTestMap_Click(object sender, EventArgs e)
		{
			if (this.Controls.Count == initialControlsCount + 1) this.Controls.RemoveAt(initialControlsCount);
			PictureBox pb1 = new PictureBox();
			Int32.TryParse(MapDimensionList.SelectedItem.ToString(), out dimensions);
			perlinMatrix = RasterFunctions.CreateTestArrayRandomPerlin(perlinFrequency, perlinMap);
			Console.Write("Minimum value of perlin Matrix : " + perlinMatrix.Cast<int>().Min() + ", maximum value : " + perlinMatrix.Cast<int>().Max() + "\n");
			// Initializing the nodes of the map
			perlinMap.ReinitializeMap();
			perlinMap.mapIsTestMap = true;
			perlinMap.InitializeNodes(perlinMatrix);
			perlinMap.InitializeLinks();
			// We transform the array to a bitmap
			pb1.Image = RasterFunctions.MapToBitmap(perlinMap, costMap);
			// We display everything nicely
			pb1.SizeMode = PictureBoxSizeMode.CenterImage;
			this.Size = new System.Drawing.Size(1000, 1000);
			pb1.Size = new System.Drawing.Size(1000, 1000);
			pb1.SizeMode = PictureBoxSizeMode.CenterImage;
			this.Controls.Add(pb1);

			// We also send the signal that pre-processing needs to be re-done but that the map
			// is now generated
			mapGenerated = true;
			preProcessingDone = false;
			departureArrivalGenerated = false;
		}

		private void PreProcessButton_Click(object sender, EventArgs e)
		{
			if (mapGenerated == false) MessageBox.Show("Map needs to be generated ! Please, generate a new map !", "ERROR");
			else if (preProcessingDone == true) MessageBox.Show("Pre-processing is already done ! Please, make a query, or make a new map !", "ERROR");
			else
			{
				perlinMap.PreProcessMap();
				preProcessingDone = true;
			}
		}

		private void GenerateDepartureArrival_Click(object sender, EventArgs e)
		{
			if (mapGenerated == false) MessageBox.Show("Map needs to be generated ! Please, generate a new map !", "ERROR");
			else
			{

				perlinMap.InitializeArrivalAndDeparture();
				// If there is already a picture box, we remove it.
				if (this.Controls.Count == initialControlsCount + 1) this.Controls.RemoveAt(initialControlsCount);
				// We create a new picture box
				PictureBox pb1 = new PictureBox();
				// We display the map, but with the departure and arrival points
				pb1.Image = RasterFunctions.MapToBitmapWithArrivalAndDeparture(perlinMap, costMap);
				// We display everything nicely
				pb1.SizeMode = PictureBoxSizeMode.CenterImage;
				this.Size = new System.Drawing.Size(1000, 1000);
				pb1.Size = new System.Drawing.Size(1000, 1000);
				pb1.SizeMode = PictureBoxSizeMode.CenterImage;
				this.Controls.Add(pb1);

				departureArrivalGenerated = true;
			}

		}

		// Contraction Hierarchies button
		private void FindLeastCost_Click(object sender, EventArgs e)
		{
			if (mapGenerated == false) MessageBox.Show("Map needs to be generated ! Please, generate a new map !", "ERROR");
			else if (preProcessingDone == false) MessageBox.Show("Pre-processing needs to be done ! Please,make the pre-processing !", "ERROR");
			else if (departureArrivalGenerated == false) MessageBox.Show("Departure and arrival points not generated ! Please, generate them !", "ERROR");
			else
			{
				if (this.Controls.Count == initialControlsCount + 1) this.Controls.RemoveAt(initialControlsCount);
				perlinMap.findCHLeastCostPath(this);
				// If there is already a picture box, we remove it.
				if (this.Controls.Count == initialControlsCount + 1) this.Controls.RemoveAt(initialControlsCount);
				// We create a new picture box
				PictureBox pb1 = new PictureBox();
				// We display the map, but with the determined least cost path
				pb1.Image = RasterFunctions.MapToBitmapWithLeastCost(perlinMap, costMap);
				// We display everything nicely
				pb1.SizeMode = PictureBoxSizeMode.CenterImage;
				this.Size = new System.Drawing.Size(1000, 1000);
				pb1.Size = new System.Drawing.Size(1000, 1000);
				pb1.SizeMode = PictureBoxSizeMode.CenterImage;
				this.Controls.Add(pb1);

			}

		}

		private void LeastCostDijkstra_Click(object sender, EventArgs e)
		{
			if (mapGenerated == false) MessageBox.Show("Map needs to be generated ! Please, generate a new map !", "ERROR");
			else if (departureArrivalGenerated == false) MessageBox.Show("Departure and arrival points not generated ! Please, generate them !", "ERROR");
			else
			{
				if (this.Controls.Count == initialControlsCount + 1) this.Controls.RemoveAt(initialControlsCount);
				perlinMap.findDijkstraLeastCostPath(this);
				// If there is already a picture box, we remove it.
				if (this.Controls.Count == initialControlsCount + 1) this.Controls.RemoveAt(initialControlsCount);
				// We create a new picture box
				PictureBox pb1 = new PictureBox();
				// We display the map, but with the determined least cost path
				pb1.Image = RasterFunctions.MapToBitmapWithLeastCost(perlinMap, costMap);
				// We display everything nicely
				pb1.SizeMode = PictureBoxSizeMode.CenterImage;
				this.Size = new System.Drawing.Size(1000, 1000);
				pb1.Size = new System.Drawing.Size(1000, 1000);
				pb1.SizeMode = PictureBoxSizeMode.CenterImage;
				this.Controls.Add(pb1);

			}
		}

		private void AStarLeastCostPath_Click(object sender, EventArgs e)
		{
			if (mapGenerated == false) MessageBox.Show("Map needs to be generated ! Please, generate a new map !", "ERROR");
			else if (departureArrivalGenerated == false) MessageBox.Show("Departure and arrival points not generated ! Please, generate them !", "ERROR");
			else
			{
				if (this.Controls.Count == initialControlsCount + 1) this.Controls.RemoveAt(initialControlsCount);
				// We make the calculus in a new thread so that the UI of the form doesn't hang out
				perlinMap.findAStarLeastCostPath(this);
				// If there is already a picture box, we remove it.
				if (this.Controls.Count == initialControlsCount + 1) this.Controls.RemoveAt(initialControlsCount);
				// We create a new picture box
				PictureBox pb1 = new PictureBox();
				// We display the map, but with the determined least cost path
				pb1.Image = RasterFunctions.MapToBitmapWithLeastCost(perlinMap, costMap);
				// We display everything nicely
				pb1.SizeMode = PictureBoxSizeMode.CenterImage;
				this.Size = new System.Drawing.Size(1000, 1000);
				pb1.Size = new System.Drawing.Size(1000, 1000);
				pb1.SizeMode = PictureBoxSizeMode.CenterImage;
				this.Controls.Add(pb1);

			}
		}


	}
}
