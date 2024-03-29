﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ToyProgramCH
{
	class Map
	{
		public int[] dimensions;

		public List<Node> cornerNodes;
		public List<Node> topNodes;
		public List<Node> bottomNodes;
		public List<Node> leftNodes;
		public List<Node> rightNodes;
		public List<Node> fullNodes;
		public Node[,] tableOfNodes;

		public Node departureNode;
		public Node arrivalNode;
		public List<Node> LeastCostPath;

		public int testCorridorx;
		public int testCorridory;
		public bool mapIsTestMap;

		public double minimalLinkCost;

		public Map(int[] userDimensions)
		{
			this.dimensions = userDimensions;
			cornerNodes = new List<Node>();
			topNodes = new List<Node>();
			bottomNodes = new List<Node>();
			leftNodes = new List<Node>();
			rightNodes = new List<Node>();
			fullNodes = new List<Node>();
			tableOfNodes = new Node[dimensions[0], dimensions[1]];
			LeastCostPath = new List<Node>();
			mapIsTestMap = false;
			minimalLinkCost = 1;
		}

		public void ReinitializeMap()
		{
			// We delete all references to the nodes, and so the links.
			cornerNodes.Clear();
			topNodes.Clear();
			bottomNodes.Clear();
			leftNodes.Clear();
			rightNodes.Clear();
			fullNodes.Clear();
			tableOfNodes = null;
			tableOfNodes = new Node[dimensions[0], dimensions[1]];
			departureNode = null;
			arrivalNode = null;
			LeastCostPath.Clear();
			mapIsTestMap = false;
			// We force the garbage collector to collect the garbage, meaning all objects without a reference to (nodes or links)
			GC.Collect();
		}

		// Not used anymore.
		public void ReinitializeLinks()
		{
			foreach (Node node in this.tableOfNodes) node.links.Clear();
			// We force the garbage collector to collect the garbage, meaning all objects without a reference to (nodes or links)
			GC.Collect();
		}


		public void InitializeNodes(int[,] arrayOfHeightValues)
		{
			Console.Write("Initializing Nodes...\n");

			// Creating the corner nodes
			Node topLeftCorner = new Node(0, 0, 0, arrayOfHeightValues[0,0]);
			Console.Write("Top left corner node : unique number = " + topLeftCorner.uniqueNumber + " Coordinates = "
				+ topLeftCorner.coordinates[0] + " " + topLeftCorner.coordinates[1] + "\n");
			Node toprightCorner = new Node(this.dimensions[0] - 1, this.dimensions[0] - 1, 0, arrayOfHeightValues[this.dimensions[0] - 1, 0]);
			Console.Write("Top right corner node : unique number = " + toprightCorner.uniqueNumber + " Coordinates = "
	+ toprightCorner.coordinates[0] + " " + toprightCorner.coordinates[1] + "\n");
			Node bottomLeftCorner = new Node((this.dimensions[0] * this.dimensions[1]) - (this.dimensions[0] - 1), 0, this.dimensions[1] - 1, arrayOfHeightValues[0, this.dimensions[1] - 1]);
			Console.Write("Bottom left corner node : unique number = " + bottomLeftCorner.uniqueNumber + " Coordinates = "
+ bottomLeftCorner.coordinates[0] + " " + bottomLeftCorner.coordinates[1] + "\n");
			Node bottomRightCorner = new Node((this.dimensions[0] * this.dimensions[1]), this.dimensions[0] - 1, this.dimensions[1] - 1, arrayOfHeightValues[this.dimensions[0] - 1, this.dimensions[1] - 1]);
			Console.Write("Bottom right corner node : unique number = " + bottomRightCorner.uniqueNumber + " Coordinates = "
+ bottomRightCorner.coordinates[0] + " " + bottomRightCorner.coordinates[1] + "\n");
			cornerNodes.Add(topLeftCorner);
			cornerNodes.Add(toprightCorner);
			cornerNodes.Add(bottomLeftCorner);
			cornerNodes.Add(bottomRightCorner);
			tableOfNodes[0, 0] = topLeftCorner;
			tableOfNodes[this.dimensions[0] - 1, 0] = toprightCorner;
			tableOfNodes[0, this.dimensions[1] - 1] = bottomLeftCorner;
			tableOfNodes[this.dimensions[0] - 1, this.dimensions[1] - 1] = bottomRightCorner;



			// Creating the top Nodes
			for (int x = 1; x <= (this.dimensions[0] - 2); x++)
			{
				Node topNode = new Node(x, x, 0, arrayOfHeightValues[x, 0]);
				topNodes.Add(topNode);
				tableOfNodes[x, 0] = topNode;
			}

			Console.Write("Corner nodes created.\n");

			// Creating the bottom Nodes
			for (int x = 1; x <= (this.dimensions[0] - 2); x++)
			{
				Node bottomNode = new Node(((this.dimensions[0] * this.dimensions[1]) - (this.dimensions[0] - 1) + x), x, this.dimensions[1] - 1, arrayOfHeightValues[x, this.dimensions[1] - 1]);
				bottomNodes.Add(bottomNode);
				tableOfNodes[x, this.dimensions[1] - 1] = bottomNode;
			}

			Console.Write("Bottom nodes created.\n");

			// Creating the left Nodes
			for (int y = 1; y <= (this.dimensions[1] - 2); y++)
			{
				Node leftNode = new Node(this.dimensions[1] * y, 0, y, arrayOfHeightValues[0, y]);
				leftNodes.Add(leftNode);
				tableOfNodes[0, y] = leftNode;
			}

			Console.Write("Left nodes created.\n");

			// Creating the right Nodes
			for (int y = 1; y <= (this.dimensions[1] - 2); y++)
			{
				Node rightNode = new Node((this.dimensions[1] * y) + (this.dimensions[0] - 1), this.dimensions[0] - 1, y, arrayOfHeightValues[this.dimensions[0] - 1, y]);
				rightNodes.Add(rightNode);
				tableOfNodes[this.dimensions[0] - 1, y] = rightNode;
			}

			Console.Write("Right nodes created.\n");

			// Creating the full nodes
			for (int y = 1; y <= this.dimensions[1] - 2; y++)
			{
				for (int x = 1; x <= this.dimensions[0] - 2; x++)
				{
					Node fullNode = new Node(x + (this.dimensions[0] * y), x, y, arrayOfHeightValues[x, y]);
					fullNodes.Add(fullNode);
					tableOfNodes[x, y] = fullNode;
				}
			}

			Console.Write("All the other nodes created.\n");
			Console.Write("[VALIDATION] There are :\n" 
				+ cornerNodes.Count() + " corner nodes (should be 4)\n"
				+ topNodes.Count() + " top nodes (should be " + (this.dimensions[0] - 2) + ")\n"
				+ bottomNodes.Count() + " bottom nodes (should be " + (this.dimensions[0] - 2) + ")\n"
				+ leftNodes.Count() + " left nodes (should be " + (this.dimensions[1] - 2) + ")\n"
				+ rightNodes.Count() + " right nodes (should be " + (this.dimensions[1] - 2) + ")\n"
				+ fullNodes.Count() + " full nodes (should be " + (this.dimensions[0]*this.dimensions[1] - ((this.dimensions[0] - 2)*2 + (this.dimensions[1] - 2)*2 + 4)) + ")\n");
		}



		public void InitializeLinks()
		{
			Console.Write("Initializing Links...\n");
			
			foreach (Node nodeToLink in tableOfNodes)
			{
				// Console.Write("Observed node : " + nodeToLink.uniqueNumber + ", coordinates : " + nodeToLink.coordinates[0] + ";" + nodeToLink.coordinates[1] + "\n");
				// If the node is NOT on the top of the grid, he has a top neighbour
				if (nodeToLink.coordinates[1] != 0)
				{
					// We select the neighbour
					Node topNeighbour = tableOfNodes[nodeToLink.coordinates[0], nodeToLink.coordinates[1] - 1];
					// We first check if this neighbor doesn't have a link with the node
					bool isLinkAlreadyThere = topNeighbour.links.Exists(Link => Link.nodes[0].uniqueNumber == nodeToLink.uniqueNumber || Link.nodes[1].uniqueNumber == nodeToLink.uniqueNumber);
					// if there is not current link witht the cell, we create one !
					if (!isLinkAlreadyThere) nodeToLink.links.Add(new Link(nodeToLink, topNeighbour));
					// if the neighbour already have the link, we take it and we add it to this node's link
					else nodeToLink.links.Add(topNeighbour.links.Find(Link => Link.nodes[0].uniqueNumber == nodeToLink.uniqueNumber || Link.nodes[1].uniqueNumber == nodeToLink.uniqueNumber));

					double linkCost = new Link(nodeToLink, topNeighbour).cost;
					if (linkCost < minimalLinkCost) minimalLinkCost = linkCost;
				}

				// If the node is NOT on the bottom of the grid, he has a bottom neighbour
				if (nodeToLink.coordinates[1] != tableOfNodes.GetLength(1) - 1)
				{
					// We select the neighbour
					Node bottomNeighbour = tableOfNodes[nodeToLink.coordinates[0], nodeToLink.coordinates[1] + 1];
					// We first check if this neighbor doesn't have a link with the cell
					bool isLinkAlreadyThere = bottomNeighbour.links.Exists(Link => Link.nodes[0].uniqueNumber == nodeToLink.uniqueNumber || Link.nodes[1].uniqueNumber == nodeToLink.uniqueNumber);
					// if there is not current link witht the cell, we create one !
					if (!isLinkAlreadyThere) nodeToLink.links.Add(new Link(nodeToLink, bottomNeighbour));
					else nodeToLink.links.Add(bottomNeighbour.links.Find(Link => Link.nodes[0].uniqueNumber == nodeToLink.uniqueNumber || Link.nodes[1].uniqueNumber == nodeToLink.uniqueNumber));

					double linkCost = new Link(nodeToLink, bottomNeighbour).cost;
					if (linkCost < minimalLinkCost) minimalLinkCost = linkCost;
				}

				// If the node is NOT on the left of the grid, he has a left neighbour
				if (nodeToLink.coordinates[0] != 0)
				{
					// We select the neighbour
					Node leftNeighbour = tableOfNodes[nodeToLink.coordinates[0] - 1, nodeToLink.coordinates[1]];
					// We first check if this neighbor doesn't have a link with the cell
					bool isLinkAlreadyThere = leftNeighbour.links.Exists(Link => Link.nodes[0].uniqueNumber == nodeToLink.uniqueNumber || Link.nodes[1].uniqueNumber == nodeToLink.uniqueNumber);
					// if there is not current link witht the cell, we create one !
					if (!isLinkAlreadyThere) nodeToLink.links.Add(new Link(nodeToLink, leftNeighbour));
					else nodeToLink.links.Add(leftNeighbour.links.Find(Link => Link.nodes[0].uniqueNumber == nodeToLink.uniqueNumber || Link.nodes[1].uniqueNumber == nodeToLink.uniqueNumber));

					double linkCost = new Link(nodeToLink, leftNeighbour).cost;
					if (linkCost < minimalLinkCost) minimalLinkCost = linkCost;
				}

				// If the node is NOT on the right of the grid, he has a right neighbour
				if (nodeToLink.coordinates[0] != tableOfNodes.GetLength(0) - 1)
				{
					// We select the neighbour
					Node rightNeighbour = tableOfNodes[nodeToLink.coordinates[0] + 1, nodeToLink.coordinates[1]];
					// We first check if this neighbor doesn't have a link with the cell
					bool isLinkAlreadyThere = rightNeighbour.links.Exists(Link => Link.nodes[0].uniqueNumber == nodeToLink.uniqueNumber || Link.nodes[1].uniqueNumber == nodeToLink.uniqueNumber);
					// if there is not current link witht the cell, we create one !
					if (!isLinkAlreadyThere) nodeToLink.links.Add(new Link(nodeToLink, rightNeighbour));
					else nodeToLink.links.Add(rightNeighbour.links.Find(Link => Link.nodes[0].uniqueNumber == nodeToLink.uniqueNumber || Link.nodes[1].uniqueNumber == nodeToLink.uniqueNumber));

					double linkCost = new Link(nodeToLink, rightNeighbour).cost;
					if (linkCost < minimalLinkCost) minimalLinkCost = linkCost;
				}
			}

			

			Console.Write("Links initialized.\n");
		}


		public void PreProcessMap()
		{
			Console.Write("Pre-processing map...\n");

			int progress = 0;
			foreach (Node nodeToProcess in tableOfNodes)
			{
				// For the first iteration, we are not going to
				// use a specific order for the nodes.
				nodeToProcess.order = nodeToProcess.uniqueNumber;

				/*
				// We select all of the neighbours of the node
				List<Node> listOfNeighbourNodes = new List<Node>();

				if (nodeToProcess.coordinates[1] != 0) listOfNeighbourNodes.Add(tableOfNodes[nodeToProcess.coordinates[0], nodeToProcess.coordinates[1] - 1]);
				if (nodeToProcess.coordinates[1] != tableOfNodes.GetLength(1) - 1) listOfNeighbourNodes.Add(tableOfNodes[nodeToProcess.coordinates[0], nodeToProcess.coordinates[1] + 1]);
				if (nodeToProcess.coordinates[0] != 0) listOfNeighbourNodes.Add(tableOfNodes[nodeToProcess.coordinates[0] - 1, nodeToProcess.coordinates[1]]);
				if (nodeToProcess.coordinates[0] != tableOfNodes.GetLength(0) - 1) listOfNeighbourNodes.Add(tableOfNodes[nodeToProcess.coordinates[0] + 1, nodeToProcess.coordinates[1]]);

	            */

				// We look at each neighbour in turn

				foreach (Node neighbourNode in nodeToProcess.GetNeighboursWithoutShortcuts())
				{
					// If the neighbour have been contracted already, we don't need to look at it. If not, we look at it.
					if (!neighbourNode.contracted)
					{
						// For all of the other neighbours
						foreach (Node otherNeighbour in nodeToProcess.GetNeighboursWithoutShortcuts().Except(new List<Node> { neighbourNode }).ToList())
						{
							// Is there a direct link between them (shortcut or not) ?
							bool isLinkAlreadyThere = neighbourNode.links.Exists(Link => Link.nodes[0] == otherNeighbour || Link.nodes[1] == otherNeighbour);

							// If not, we look if the path neighbour => node selected => other neighbour is the shortest path
							if (!isLinkAlreadyThere)
							{
								// Console.Write("Finding cost of path...\n");
								double potentialShortcutCost = pathFinding.FindCostOfPath(new List<Node> { neighbourNode, nodeToProcess, otherNeighbour });
								// Console.Write("Finding Dijkstra least cost path...\n");
								double realShortestDistance = pathFinding.DijkstraLeastCostPath(neighbourNode, otherNeighbour);

								// If it is, we create a new link between the neighbour and the other neighbour
								if (potentialShortcutCost <= realShortestDistance)
								{
									// The shortcut have the cost of the shortest path that goes from neighbour => node selected => other neighbour
									Link shortcut = new Link(neighbourNode, otherNeighbour, potentialShortcutCost);
									neighbourNode.links.Add(shortcut);
									otherNeighbour.links.Add(shortcut);
								}
							}
						}
					}
				}
				// Once all of the neighbours have been checked, we can look at another node.
				nodeToProcess.contracted = true;
				progress++;
				double perrcentage = Math.Round((double)((double)progress/(double)tableOfNodes.Length)*(double)100,2);
				Console.Write("\rProgress is : " + perrcentage + "%        ");
			}

			// Now that we have seen all of the nodes, we can say that preprocessing is finally over.
			Console.Write("\nMap is pre-processed.\n");

		}

		public void InitializeArrivalAndDeparture()
		{
			Random randomizer = new Random();
			if (this.mapIsTestMap)
			{
				this.departureNode = tableOfNodes[randomizer.Next(0, this.dimensions[0] / 3), this.testCorridory];
				this.arrivalNode = tableOfNodes[this.testCorridorx, randomizer.Next(0, this.dimensions[1])];
			}
			else
			{
				this.departureNode = tableOfNodes[randomizer.Next(0, this.dimensions[0] / 3), randomizer.Next(0, this.dimensions[1])];
				this.arrivalNode = tableOfNodes[randomizer.Next(this.dimensions[0] * 2 / 3, this.dimensions[0] - 1), randomizer.Next(0, this.dimensions[1])];
			}

		}

		public void findCHLeastCostPath(Form1 form)
		{
			Console.Write("Searching CH least-cost path...\n");
			LeastCostPath.Clear();

			LeastCostPath.AddRange(pathFinding.CHLeastCostPathList(this.departureNode, this.arrivalNode, form, this));

			Console.Write("CH least-cost path found.\n");
		}




		public void findDijkstraLeastCostPath(Form1 form)
		{
			Console.Write("Searching Dijkstra least-cost path...\n");
			LeastCostPath.Clear();

			LeastCostPath.AddRange(pathFinding.DijkstraLeastCostPathList(this.departureNode, this.arrivalNode, form, this));

			Console.Write("Dijkstra least-cost path found.\n");
		}

		public void findAStarLeastCostPath(Form1 form)
		{
			Console.Write("Searching A* least-cost path...\n");
			LeastCostPath.Clear();

			LeastCostPath.AddRange(pathFinding.AStarLeastCostPathList(this.departureNode, this.arrivalNode, form, this));

			Console.Write("A* least-cost path found.\n");
		}





		// End of class Map
	}
	// End of namespace
}
