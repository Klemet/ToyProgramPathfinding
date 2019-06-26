using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ToyProgramCH
{
	class RasterFunctions
	{

		// public static Bitmap

			public static Bitmap CreateBitmapFlag()
		{

			Bitmap flag = new Bitmap(200, 100);
			Graphics flagGraphics = Graphics.FromImage(flag);
			int red = 0;
			int white = 11;
			while (white <= 100)
			{
				flagGraphics.FillRectangle(Brushes.Red, 0, red, 200, 10);
				flagGraphics.FillRectangle(Brushes.White, 0, white, 200, 10);
				red += 20;
				white += 20;
			}

			return (flag);

		}


		// Avec le code de https://stackoverflow.com/questions/8659351/2d-perlin-noise/8659483#8659483
		public static Bitmap CreateBitmapPerlin()
		{

			Bitmap bmp = new Bitmap(800, 800);
			PerlinNoise noisePerlin = new PerlinNoise(1);

			//Generate basic terrain sine
			for (int i = 0; i < 800; i++)
			{
				for (int j = 0; j < 800; j++)
				{

					double height = Math.Abs(noisePerlin.Noise((double)i/(double)800, (double)j / (double)800, 0.22) * 255);
					bmp.SetPixel(i, j, Color.FromArgb((int)height, (int)height, (int)height));
				}
			}

			return (bmp);

		}


		public static Bitmap CreateBitmapRandomPerlin()
		{

			Bitmap bmp = new Bitmap(800, 800);
			Random randomizer = new Random();
			PerlinNoise noisePerlin = new PerlinNoise((randomizer.Next()));

			//Generate basic terrain sine
			for (int i = 0; i < 800; i++)
			{
				for (int j = 0; j < 800; j++)
				{

					double height = Math.Abs(noisePerlin.Noise((double)i / (double)800, (double)j / (double)800, 0.22) * 255);
					bmp.SetPixel(i, j, Color.FromArgb((int)height, (int)height, (int)height));
				}
			}

			return (bmp);


		}

		public static Bitmap CreateBitmapRandomPerlin2()
		{

			Bitmap bmp = new Bitmap(800, 800);
			Random randomizer = new Random();
			PerlinNoise noisePerlin = new PerlinNoise((randomizer.Next()));
			double widthDivisor = 1 / (double)800;
			double heightDivisor = 1 / (double)800;

			for (int i = 0; i < 800; i++)
			{
				for (int j = 0; j < 800; j++)
				{

					double v =
			// First octave
			(noisePerlin.Noise(2 * i * widthDivisor, 2 * j * heightDivisor, -0.5) + 1) / 2 * 0.7 +
			// Second octave
			(noisePerlin.Noise(4 * i * widthDivisor, 4 * j * heightDivisor, 0) + 1) / 2 * 0.2 +
			// Third octave
			(noisePerlin.Noise(8 * i * widthDivisor, 8 * j * heightDivisor, +0.5) + 1) / 2 * 0.1;

					v = Math.Min(1, Math.Max(0, v));
					byte b = (byte)(v * 255);
					bmp.SetPixel(i, j, Color.FromArgb((int)b, (int)b, (int)b));
				}
			}

			return (bmp);
		}

		public static int[,] CreateArrayRandomPerlin(int dim1, int dim2)
		{

			int[,] matrixOfHeightValues = new int[dim1, dim2];
			Random randomizer = new Random();
			PerlinNoise noisePerlin = new PerlinNoise((randomizer.Next()));
			double widthDivisor = 1 / (double)dim1;
			double heightDivisor = 1 / (double)dim2;

			for (int i = 0; i < dim1; i++)
			{
				for (int j = 0; j < dim2; j++)
				{

					double v =
			// First octave
			(noisePerlin.Noise(2 * i * widthDivisor, 2 * j * heightDivisor, -0.5) + 1) / 2 * 0.7 +
			// Second octave
			(noisePerlin.Noise(4 * i * widthDivisor, 4 * j * heightDivisor, 0) + 1) / 2 * 0.2 +
			// Third octave
			(noisePerlin.Noise(8 * i * widthDivisor, 8 * j * heightDivisor, +0.5) + 1) / 2 * 0.1;

					v = Math.Min(1, Math.Max(0, v));
					byte b = (byte)(v * 255);
					matrixOfHeightValues[i,j] = b;
				}
			}

			return (matrixOfHeightValues);
		}

		public static int[,] CreateArrayRandomPerlin(int frequencyMultiplicator, Map map)
		{

			int[,] matrixOfHeightValues = new int[map.dimensions[0], map.dimensions[1]];
			Random randomizer = new Random();
			PerlinNoise noisePerlin = new PerlinNoise((randomizer.Next()));
			double widthDivisor = 1 / (double)map.dimensions[0];
			double heightDivisor = 1 / (double)map.dimensions[1];

			for (int i = 0; i < map.dimensions[0]; i++)
			{
				for (int j = 0; j < map.dimensions[1]; j++)
				{

					double v =
			// First octave
			(noisePerlin.Noise(2 * frequencyMultiplicator * i * widthDivisor, 2 * frequencyMultiplicator * j * heightDivisor, -0.5) + 1) / 2 * 0.7 +
			// Second octave
			(noisePerlin.Noise(4 * frequencyMultiplicator * i * widthDivisor, 4 * frequencyMultiplicator * j * heightDivisor, 0) + 1) / 2 * 0.2 +
			// Third octave
			(noisePerlin.Noise(8 * frequencyMultiplicator * i * widthDivisor, 8 * frequencyMultiplicator * j * heightDivisor, +0.5) + 1) / 2 * 0.1;

					v = Math.Min(1, Math.Max(0, v));
					byte b = (byte)(v * 255);
					matrixOfHeightValues[i, j] = b;
				}
			}

			return (matrixOfHeightValues);
		}

		public static int[,] CreateTestArrayRandomPerlin(int frequencyMultiplicator, Map map)
		{

			int[,] matrixOfHeightValues = new int[map.dimensions[0], map.dimensions[1]];
			Random randomizer = new Random();
			PerlinNoise noisePerlin = new PerlinNoise((randomizer.Next()));
			double widthDivisor = 1 / (double)map.dimensions[0];
			double heightDivisor = 1 / (double)map.dimensions[1];
			// We Add test corridors and link them to the map
			map.testCorridorx = randomizer.Next(0, map.dimensions[0] - 1);
			map.testCorridory = randomizer.Next(0, map.dimensions[1] - 1);

			for (int i = 0; i < map.dimensions[0]; i++)
			{
				for (int j = 0; j < map.dimensions[1]; j++)
				{

					double v =
			// First octave
			(noisePerlin.Noise(2 * frequencyMultiplicator * i * widthDivisor, 2 * frequencyMultiplicator * j * heightDivisor, -0.5) + 1) / 2 * 0.7 +
			// Second octave
			(noisePerlin.Noise(4 * frequencyMultiplicator * i * widthDivisor, 4 * frequencyMultiplicator * j * heightDivisor, 0) + 1) / 2 * 0.2 +
			// Third octave
			(noisePerlin.Noise(8 * frequencyMultiplicator * i * widthDivisor, 8 * frequencyMultiplicator * j * heightDivisor, +0.5) + 1) / 2 * 0.1;

					v = Math.Min(1, Math.Max(0, v));
					byte b = (byte)(v * 255);
					matrixOfHeightValues[i, j] = b;
				}
			}

			// Then, we write the values of the test corridor

			for(int i = 0; i < map.dimensions[1] - 1; i++) matrixOfHeightValues[map.testCorridorx, i] = 100;
			for (int i = 0; i < map.dimensions[0] - 1; i++) matrixOfHeightValues[i, map.testCorridory] = 100;



			return (matrixOfHeightValues);
		}

		public static void chooseColorOfPixel(Bitmap bmp, int x, int y, int height)
		{
			// First color : water
			if (height < 100) bmp.SetPixel(x, y, Color.FromArgb(0, 0, 204));
			// Second color : grass
			else if (100 <= height && height < 150)
			{
				int[] colorAtHeight20 = new int[3] { 0, 100, 0 };
				//int[] colorAtHeight150 = new int[3] { 204, 255, 204 };
				int[] colorAtHeight150 = new int[3] { 192, 178, 109 };
				double[] colorAtGivenHeight = new double[3];
				for (int colorParameter = 0; colorParameter < 3; colorParameter++)
				{
					colorAtGivenHeight[colorParameter] = (double)colorAtHeight20[colorParameter] + (double)((double)((double)height - (double)20) / ((double)150 - (double)20)) * (double)(colorAtHeight150[colorParameter] - colorAtHeight20[colorParameter]);
				}
				bmp.SetPixel(x, y, Color.FromArgb((int)colorAtGivenHeight[0], (int)colorAtGivenHeight[1], (int)colorAtGivenHeight[2]));
			}
			// Third color : pastures/snow
			else
			{
				int[] colorAtHeight150 = new int[3] { 192, 178, 109 };
				int[] colorAtHeight255 = new int[3] { 255, 255, 255 };
				double[] colorAtGivenHeight = new double[3];
				for (int colorParameter = 0; colorParameter < 3; colorParameter++)
				{
					colorAtGivenHeight[colorParameter] = (double)colorAtHeight150[colorParameter] + ((double)((double)height - (double)150) / ((double)255 - (double)150)) * (double)(colorAtHeight255[colorParameter] - colorAtHeight150[colorParameter]);
				}
				bmp.SetPixel(x, y, Color.FromArgb((int)colorAtGivenHeight[0], (int)colorAtGivenHeight[1], (int)colorAtGivenHeight[2]));
			}
		}

		public static void generateCostMap(Bitmap bmp, Map map)
		{
			double[,] meanCostOfCells = new double[map.dimensions[0], map.dimensions[1]];
			// First loop to have all of the values, so that we can determine a maximum.
			for (int x = 0; x < map.dimensions[0]; x++)
			{
				for (int y = 0; y < map.dimensions[1]; y++)
				{
					meanCostOfCells[x, y] = (double)map.tableOfNodes[x, y].links.Select(Link => Link.cost).Sum() / (double)map.tableOfNodes[x, y].links.Count();
				}
			}
			double min = meanCostOfCells.Cast<Double>().Min();
			double max = meanCostOfCells.Cast<Double>().Max();

			// Second loop to adapt the values to the colors, and register them in the bitmap
			for (int x = 0; x < map.dimensions[0]; x++)
			{
				for (int y = 0; y < map.dimensions[1]; y++)
				{
					double meanCostOfCellNormalized = ((meanCostOfCells[x, y] - min) / (max - min));
					int[] colorAtHeightMin = new int[3] { 0, 0, 255 };
					int[] colorAtHeightMax = new int[3] { 255, 0, 0 };
					double[] colorAtGivenHeight = new double[3];
					for (int colorParameter = 0; colorParameter < 3; colorParameter++)
					{
						colorAtGivenHeight[colorParameter] = (double)colorAtHeightMin[colorParameter] + (double)meanCostOfCellNormalized * (double)((double)colorAtHeightMax[colorParameter] - (double)colorAtHeightMin[colorParameter]);
					}
					bmp.SetPixel(x, y, Color.FromArgb((int)colorAtGivenHeight[0], (int)colorAtGivenHeight[1], (int)colorAtGivenHeight[2]));
				}
			}
		}

		// Outdated; we make the bitmap from the map directly now
		public static Bitmap ArrayToBitmap(int[,] arrayOfHeightValues)
		{

			Bitmap bmp = new Bitmap(arrayOfHeightValues.GetLength(0), arrayOfHeightValues.GetLength(1));

			for (int x = 0; x < arrayOfHeightValues.GetLength(0); x++)
			{
				for (int y = 0; y < arrayOfHeightValues.GetLength(1); y++)
				{
					int height = (int)arrayOfHeightValues[x, y];
					RasterFunctions.chooseColorOfPixel(bmp, x, y, height);
				}
			}

			return (bmp);
		}


		public static Bitmap MapToBitmap(Map mapWithheightValues, bool costMap)
		{
			Bitmap bmp = new Bitmap(mapWithheightValues.dimensions[0], mapWithheightValues.dimensions[1]);

			if (costMap)
			{
				RasterFunctions.generateCostMap(bmp, mapWithheightValues);
			}
			else
			{
				for (int y = 0; y < mapWithheightValues.dimensions[0]; y++)
				{
					for (int x = 0; x < mapWithheightValues.dimensions[1]; x++)
					{

						// We find the node that have these coordinates in the list of all the nodes of the map
						int height = mapWithheightValues.tableOfNodes[x, y].height;
						RasterFunctions.chooseColorOfPixel(bmp, x, y, height);
					}
				}
			}
			return (bmp);
		}

		public static Bitmap MapToBitmapWithArrivalAndDeparture(Map mapWithheightValues, bool costMap)
		{

			Bitmap bmp = new Bitmap(mapWithheightValues.dimensions[0], mapWithheightValues.dimensions[1]);

			if (costMap)
			{
				RasterFunctions.generateCostMap(bmp, mapWithheightValues);
			}
			else
			{
				for (int y = 0; y < mapWithheightValues.dimensions[0]; y++)
				{
					for (int x = 0; x < mapWithheightValues.dimensions[1]; x++)
					{
						// We find the node that have these coordinates in the list of all the nodes of the map
						int height = mapWithheightValues.tableOfNodes[x, y].height;
						RasterFunctions.chooseColorOfPixel(bmp, x, y, height);
					}
				}

			}
			// we display the departure node and the surroundings in orange
			for(int x = -5; x < 6; x++) for (int y = -5; y < 6; y++) if((mapWithheightValues.departureNode.coordinates[0] + x >= 0) && (mapWithheightValues.departureNode.coordinates[0] + x <= (mapWithheightValues.dimensions[0] - 1)) && (mapWithheightValues.departureNode.coordinates[1] + y >= 0) && (mapWithheightValues.departureNode.coordinates[1] + y <= (mapWithheightValues.dimensions[1] - 1))) bmp.SetPixel(mapWithheightValues.departureNode.coordinates[0]+x, mapWithheightValues.departureNode.coordinates[1]+y, Color.FromArgb((int)255, (int)128, (int)0));

			// We display the arrival node and the surroundings in yellow
			for (int x = -5; x < 6; x++) for (int y = -5; y < 6; y++) if ((mapWithheightValues.arrivalNode.coordinates[0] + x >= 0) && (mapWithheightValues.arrivalNode.coordinates[0] + x <= (mapWithheightValues.dimensions[0] - 1)) && (mapWithheightValues.arrivalNode.coordinates[1] + y >= 0) && (mapWithheightValues.arrivalNode.coordinates[1] + y <= (mapWithheightValues.dimensions[1] - 1)))  bmp.SetPixel(mapWithheightValues.arrivalNode.coordinates[0]+x, mapWithheightValues.arrivalNode.coordinates[1]+y, Color.FromArgb((int)255, (int)239, (int)0));

			return (bmp);
		}

		public static Bitmap MapToBitmapWithLeastCost(Map mapWithheightValues, bool costMap)
		{

			Bitmap bmp = new Bitmap(mapWithheightValues.dimensions[0], mapWithheightValues.dimensions[1]);

			if (costMap)
			{
				RasterFunctions.generateCostMap(bmp, mapWithheightValues);
			}
			else
			{
				for (int y = 0; y < mapWithheightValues.dimensions[0]; y++)
				{
					for (int x = 0; x < mapWithheightValues.dimensions[1]; x++)
					{
						// We find the node that have these coordinates in the list of all the nodes of the map
						int height = mapWithheightValues.tableOfNodes[x, y].height;
						RasterFunctions.chooseColorOfPixel(bmp, x, y, height);
					}
				}
			}



			// we display the departure node and the surroundings in blue
			for (int x = -5; x < 6; x++) for (int y = -5; y < 6; y++) if (mapWithheightValues.departureNode.coordinates[0] + x >= 0 && (mapWithheightValues.departureNode.coordinates[0] + x <= (mapWithheightValues.dimensions[0] - 1)) && mapWithheightValues.departureNode.coordinates[1] + y >= 0 && mapWithheightValues.departureNode.coordinates[1] + y <= mapWithheightValues.dimensions[1] - 1) bmp.SetPixel(mapWithheightValues.departureNode.coordinates[0] + x, mapWithheightValues.departureNode.coordinates[1] + y, Color.FromArgb((int)0, (int)77, (int)255));

			// We display the arrival node and the surroundings in yellow
			for (int x = -5; x < 6; x++) for (int y = -5; y < 6; y++) if (mapWithheightValues.arrivalNode.coordinates[0] + x >= 0 && (mapWithheightValues.arrivalNode.coordinates[0] + x <= (mapWithheightValues.dimensions[0] - 1)) && mapWithheightValues.arrivalNode.coordinates[1] + y >= 0 && mapWithheightValues.arrivalNode.coordinates[1] + y <= mapWithheightValues.dimensions[1] - 1) bmp.SetPixel(mapWithheightValues.arrivalNode.coordinates[0] + x, mapWithheightValues.arrivalNode.coordinates[1] + y, Color.FromArgb((int)255, (int)239, (int)0));

			// We display the path in red
			foreach (Node nodeInPath in mapWithheightValues.LeastCostPath)
			{
				bmp.SetPixel(nodeInPath.coordinates[0], nodeInPath.coordinates[1], Color.FromArgb((int)255, (int)0, (int)127));
			}

			return (bmp);
		}


	}
}
