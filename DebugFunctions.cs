using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProgramCH
{
	class DebugFunctions
	{
		public static void DisplayNodeInfoInConsole(Node nodeToDisplay, string nodeType)
		{
			Console.Write(nodeType + " : " + nodeToDisplay.uniqueNumber
				+ " ; Coordinates : " + nodeToDisplay.coordinates[0] + ";" + nodeToDisplay.coordinates[1]
				+ " ; Number of links : " + nodeToDisplay.links.Count + "\n");

		}

		public static void DisplayNodeInfoInConsole(NodeForPathfinding nodeToDisplay, string nodeType)
		{
			Console.Write(nodeType + " : " + nodeToDisplay.uniqueNumber
				+ " ; Coordinates : " + nodeToDisplay.coordinates[0] + ";" + nodeToDisplay.coordinates[1]
				+ " ; Number of links : " + nodeToDisplay.links.Count + "\n");

		}

		public static void DisplayLinksOfNode(Node nodeToDisplay, string nodeType)
		{
			Console.Write("Links of " + nodeType + " :\n");
			int i = 1;
			foreach (Link linkOfNode in nodeToDisplay.links)
			{
				Console.Write("Link " + i + " : Between node " + linkOfNode.nodes[0].uniqueNumber
					+ " and " + linkOfNode.nodes[1].uniqueNumber + ", cost = " + linkOfNode.cost +
					", shortcut = " + linkOfNode.shortcut +
					", difference in x : " + (linkOfNode.nodes[0].coordinates[0] - linkOfNode.nodes[1].coordinates[0]) +
					", difference in y : " + (linkOfNode.nodes[0].coordinates[1] - linkOfNode.nodes[1].coordinates[1]) + "\n");
				i++;
			}

		}

		public static void DisplayLinksOfNode(NodeForPathfinding nodeToDisplay, string nodeType)
		{
			Console.Write("Links of " + nodeType + " :\n");
			int i = 1;
			foreach (Link linkOfNode in nodeToDisplay.links)
			{
				Console.Write("Link " + i + " : Between node " + linkOfNode.nodes[0].uniqueNumber
					+ " and " + linkOfNode.nodes[1].uniqueNumber + ", cost = " + linkOfNode.cost +
					", shortcut = " + linkOfNode.shortcut +
					", difference in x : " + (linkOfNode.nodes[0].coordinates[0] - linkOfNode.nodes[1].coordinates[0]) +
					", difference in y : " + (linkOfNode.nodes[0].coordinates[1] - linkOfNode.nodes[1].coordinates[1]) + "\n");
				i++;
			}

		}

		public static void DisplayAllNeighbours(NodeForPathfinding nodeToDisplay, string nodeType)
		{
			Console.Write("Neighbours of " + nodeType + " :\n");
			int i = 1;
			foreach (Node neighbour in nodeToDisplay.GetNeighbours())
			{
				DebugFunctions.DisplayNodeInfoInConsole(neighbour, "Neighbour " + i);
				i++;
			}

		}


		public static void DisplayAllNeighbours(Node nodeToDisplay, string nodeType)
		{
			Console.Write("Neighbours of " + nodeType + " :\n");
			int i = 1;
			foreach (Node neighbour in nodeToDisplay.GetNeighbours())
			{
				DebugFunctions.DisplayNodeInfoInConsole(neighbour, "Neighbour " + i);
				i++;
			}

		}

	}
}
