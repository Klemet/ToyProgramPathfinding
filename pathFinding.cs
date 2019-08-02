using System;
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
	class pathFinding
	{
		public static double FindCostOfPath(List<Node> listOfNodesOfThePath)
		{
			double cost = 0;

			for (int i = 0; i < listOfNodesOfThePath.Count - 1; i++)
			{
				Node nodeInThePath = listOfNodesOfThePath[i];
				Node nextNodeInThePath = listOfNodesOfThePath[i + 1];

				// Debug code
				/*
				Console.Write("Node " + nodeInThePath.uniqueNumber + "; Coordinates = " + nodeInThePath.coordinates[0] + ";" + nodeInThePath.coordinates[1]
					+ " ; Number of links contained : " + nodeInThePath.links.Count + "\n");
				Console.Write("Node " + nextNodeInThePath.uniqueNumber + "; Coordinates = " + nextNodeInThePath.coordinates[0] + ";" + nextNodeInThePath.coordinates[1]
							+ " ; Number of links contained : " + nextNodeInThePath.links.Count + "\n");
				Console.Write("Does link exists ?" + nodeInThePath.links.Exists(Link => Link.nodes[0] == nextNodeInThePath || Link.nodes[1] == nextNodeInThePath) + "\n");
				*/

				Link linkBetweenNodes = nodeInThePath.links.Find(Link => Link.nodes[0] == nextNodeInThePath || Link.nodes[1] == nextNodeInThePath);

				cost = cost + linkBetweenNodes.cost;

			}

			return (cost);
		}

		public static double DijkstraLeastCostPath(Node arrival, Node departure)
		{
			// Open the first node as a (Departure) "Node of pathfinding"
			NodeForPathfinding startingNode = new NodeForPathfinding(departure);
			// DebugFunctions.DisplayNodeInfoInConsole(startingNode, "startingNode");
			// DebugFunctions.DisplayLinksOfNode(startingNode, "startingNode");
			// DebugFunctions.DisplayAllNeighbours(startingNode, "startingNode");
			// Create the list of "closed" nodes
			List <NodeForPathfinding> listOfClosedNodes = new List<NodeForPathfinding>();
			List<NodeForPathfinding> listOfOpenNodes = new List<NodeForPathfinding>();
			// Look at all of his neighbours and register them as Nodes of pathfinding
			foreach (Node neighbourOfStartingNode in startingNode.GetNeighbours())
			{
				NodeForPathfinding neighbourAsPathfindingNode = new NodeForPathfinding(neighbourOfStartingNode);
				// For each, register the first node as a predecessor, and the cost to the start
				neighbourAsPathfindingNode.predecessor = startingNode;
				
				neighbourAsPathfindingNode.dijkstraDistance = neighbourAsPathfindingNode.findDistanceToStart(startingNode);
				listOfOpenNodes.Add(neighbourAsPathfindingNode);
			}
			listOfClosedNodes.Add(startingNode);
			bool foundTheGoal = false;
			// This line is just to circumvent a limitation of C# who doesn't like objects initialized in conditions.
			NodeForPathfinding arrivalAsPathfindingNode = startingNode;
			// Console.Write("Looking for arrival node number " + arrival.uniqueNumber + "...\n");
			while (!foundTheGoal)
			{
				// We take the open node with the smallest Dijkstra distance
				listOfOpenNodes = listOfOpenNodes.OrderBy(NodeForPathfinding => NodeForPathfinding.dijkstraDistance).ToList();
				NodeForPathfinding nodeToClose = listOfOpenNodes[0];
				// We remove it from the list, as we are going to close it.
				listOfOpenNodes.RemoveAt(0);

				// Console.Write("Looking at a cell with distance = " + nodeToClose.dijkstraDistance + "...\n");
				foreach (Node neighbourtoOpen in nodeToClose.GetNeighboursWithoutShortcuts())
				{
					// Console.Write("Looking at a neighbour number " + neighbourtoOpen.uniqueNumber + "...\n");
					// System.Threading.Thread.Sleep(1000);
					NodeForPathfinding neighbourAsPathfindingNode = new NodeForPathfinding(neighbourtoOpen);
					// Before creating a new NodePathfinding object, we check it it's not already in a list
					if (listOfClosedNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber) != null)
					{
						neighbourAsPathfindingNode = listOfClosedNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber);
					}
					else if (listOfOpenNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber) != null)
					{
						neighbourAsPathfindingNode = listOfOpenNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber);
					}
					// If he wasn't in any list, he needs to be open when we finish
					else { listOfOpenNodes.Add(neighbourAsPathfindingNode); }
					// If the node is already closed, don't consider it)
					if (!listOfClosedNodes.Contains(neighbourAsPathfindingNode))
					{
						// If the distance to start haven't been calculated yet, do it
						if (neighbourAsPathfindingNode.dijkstraDistance == double.PositiveInfinity)
						{
							neighbourAsPathfindingNode.predecessor = nodeToClose;
							neighbourAsPathfindingNode.dijkstraDistance = neighbourAsPathfindingNode.findDistanceToStart(startingNode);
						}
						// Else, check if the distance to start if we go throught nodeToClose isn't
						// smaller than the current registered one and change predecessor + distance if needed
						else
						{
							NodeForPathfinding predecessorWeMightKeep = neighbourAsPathfindingNode.predecessor;
							neighbourAsPathfindingNode.predecessor = nodeToClose;
							double distanceThroughtNodeToClose = neighbourAsPathfindingNode.findDistanceToStart(startingNode);
							if (distanceThroughtNodeToClose < neighbourAsPathfindingNode.dijkstraDistance) { neighbourAsPathfindingNode.dijkstraDistance = distanceThroughtNodeToClose; }
							else { neighbourAsPathfindingNode.predecessor = predecessorWeMightKeep; }
						}
						/* WHY THE HELL DID I WROTE THIS HERE ?????
						neighbourAsPathfindingNode.predecessor = startingNode;
						neighbourAsPathfindingNode.dijkstraDistance = neighbourAsPathfindingNode.findDistanceToStart(startingNode);
						listOfOpenNodes.Add(neighbourAsPathfindingNode);
						*/
					}

					// Now that the neighbour is opened (predecessor + distance to start),
					// He his also in the open list if he wasn't here before.
					// We are just going to check if he was the arrival node.
					foundTheGoal = (neighbourAsPathfindingNode.uniqueNumber == arrival.uniqueNumber);
					// Console.Write("foundTheGoal = " + foundTheGoal + "\n");
					if (foundTheGoal) { arrivalAsPathfindingNode = neighbourAsPathfindingNode; break; }
				}
				// We close the node now that all of his neighbours have been found
				listOfClosedNodes.Add(nodeToClose);
			}
			// Now, the arrival node have been found. The least-cost path is the dijkstradistance of this node, calculated when he was opened.
			// Console.Write(listOfClosedNodes.Count + "Were closed during this search.\n");
			return (arrivalAsPathfindingNode.dijkstraDistance);
		}

		public static List<Node> DijkstraLeastCostPathList(Node arrival, Node departure, Form1 form, Map mapOfPathFinding)
		{
			// We create a bitmap in the form with the departure/arrival points
			// If there is already a picture box, we remove it.
			// We create a new picture box
			PictureBox pb1 = new PictureBox();
			// We display the map, but with the departure and arrival points
			Bitmap bmpForPictureBox = RasterFunctions.MapToBitmapWithArrivalAndDeparture(mapOfPathFinding, false);
			pb1.Image = bmpForPictureBox;
			// We display everything nicely
			pb1.SizeMode = PictureBoxSizeMode.CenterImage;
			form.Size = new System.Drawing.Size(1000, 1000);
			pb1.Size = new System.Drawing.Size(1000, 1000);
			pb1.SizeMode = PictureBoxSizeMode.CenterImage;
			form.Controls.Add(pb1);

			// For comments, see previous function.
			NodeForPathfinding startingNode = new NodeForPathfinding(departure);
			List<NodeForPathfinding> listOfClosedNodes = new List<NodeForPathfinding>();
			List<NodeForPathfinding> listOfOpenNodes = new List<NodeForPathfinding>();
			foreach (Node neighbourOfStartingNode in startingNode.GetNeighbours())
			{
				NodeForPathfinding neighbourAsPathfindingNode = new NodeForPathfinding(neighbourOfStartingNode);
				neighbourAsPathfindingNode.predecessor = startingNode;

				neighbourAsPathfindingNode.dijkstraDistance = neighbourAsPathfindingNode.findDistanceToStart(startingNode);
				listOfOpenNodes.Add(neighbourAsPathfindingNode);
			}
			listOfClosedNodes.Add(startingNode);
			bool foundTheGoal = false;
			NodeForPathfinding arrivalAsPathfindingNode = startingNode;
			while (!foundTheGoal)
			{
				listOfOpenNodes = listOfOpenNodes.OrderBy(NodeForPathfinding => NodeForPathfinding.dijkstraDistance).ToList();
				NodeForPathfinding nodeToClose = listOfOpenNodes[0];
				listOfOpenNodes.RemoveAt(0);

				foreach (Node neighbourtoOpen in nodeToClose.GetNeighboursWithoutShortcuts())
				{
					NodeForPathfinding neighbourAsPathfindingNode = new NodeForPathfinding(neighbourtoOpen);
					if (listOfClosedNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber) != null)
					{
						neighbourAsPathfindingNode = listOfClosedNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber);
					}
					else if (listOfOpenNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber) != null)
					{
						neighbourAsPathfindingNode = listOfOpenNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber);
					}
					else { listOfOpenNodes.Add(neighbourAsPathfindingNode); }
					if (!listOfClosedNodes.Contains(neighbourAsPathfindingNode))
					{
						if (neighbourAsPathfindingNode.dijkstraDistance == double.PositiveInfinity)
						{
							neighbourAsPathfindingNode.predecessor = nodeToClose;
							neighbourAsPathfindingNode.dijkstraDistance = neighbourAsPathfindingNode.findDistanceToStart(startingNode);
						}
						else
						{
							NodeForPathfinding predecessorWeMightKeep = neighbourAsPathfindingNode.predecessor;
							neighbourAsPathfindingNode.predecessor = nodeToClose;
							double distanceThroughtNodeToClose = neighbourAsPathfindingNode.findDistanceToStart(startingNode);
							if (distanceThroughtNodeToClose < neighbourAsPathfindingNode.dijkstraDistance) { neighbourAsPathfindingNode.dijkstraDistance = distanceThroughtNodeToClose; }
							else { neighbourAsPathfindingNode.predecessor = predecessorWeMightKeep; }
						}
					}

					foundTheGoal = (neighbourAsPathfindingNode.uniqueNumber == arrival.uniqueNumber);
					if (foundTheGoal) { arrivalAsPathfindingNode = neighbourAsPathfindingNode; break; }
				}
				listOfClosedNodes.Add(nodeToClose);
				bmpForPictureBox.SetPixel(nodeToClose.coordinates[0], nodeToClose.coordinates[1], Color.FromArgb(0, 204, 204));
				pb1.Image = bmpForPictureBox;
				pb1.Refresh();
			}
			Console.Write(listOfClosedNodes.Count + " nodes were closed during this search.\n");
			return (arrivalAsPathfindingNode.findPathToStart(startingNode));
		}



		public static List<Node> AStarLeastCostPathList(Node arrival, Node departure, Form1 form, Map mapOfPathFinding)
		{
			// We create a bitmap in the form with the departure/arrival points
			// If there is already a picture box, we remove it.
			// We create a new picture box
			PictureBox pb1 = new PictureBox();
			// We display the map, but with the departure and arrival points
			Bitmap bmpForPictureBox = RasterFunctions.MapToBitmapWithArrivalAndDeparture(mapOfPathFinding, false);
			pb1.Image = bmpForPictureBox;
			// We display everything nicely
			pb1.SizeMode = PictureBoxSizeMode.CenterImage;
			pb1.Size = new System.Drawing.Size(1000, 1000);
			pb1.SizeMode = PictureBoxSizeMode.CenterImage;
			form.Controls.Add(pb1);


			// For comments, see previous functions.
			NodeForPathfinding startingNode = new NodeForPathfinding(departure);
			List<NodeForPathfinding> listOfClosedNodes = new List<NodeForPathfinding>();
			List<NodeForPathfinding> listOfOpenNodes = new List<NodeForPathfinding>();
			foreach (Node neighbourOfStartingNode in startingNode.GetNeighbours())
			{
				NodeForPathfinding neighbourAsPathfindingNode = new NodeForPathfinding(neighbourOfStartingNode);
				neighbourAsPathfindingNode.predecessor = startingNode;

				neighbourAsPathfindingNode.dijkstraDistance = neighbourAsPathfindingNode.findDistanceToStart(startingNode) + neighbourAsPathfindingNode.manhattanDistanceToNode(arrival);
				listOfOpenNodes.Add(neighbourAsPathfindingNode);
				// Console.Write("We opened node " + neighbourAsPathfindingNode.uniqueNumber + " . Distance to start was " + neighbourAsPathfindingNode.findDistanceToStart(startingNode)
	// + " and distance to finish is approximatly " + neighbourAsPathfindingNode.manhattanDistanceToNode(arrival) +
	// " Sum of distances is : " + neighbourAsPathfindingNode.dijkstraDistance + "\n");
			}
			listOfClosedNodes.Add(startingNode);
			bool foundTheGoal = false;
			NodeForPathfinding arrivalAsPathfindingNode = startingNode;
			while (!foundTheGoal)
			{
				listOfOpenNodes = listOfOpenNodes.OrderBy(NodeForPathfinding => NodeForPathfinding.dijkstraDistance).ToList();
				NodeForPathfinding nodeToClose = listOfOpenNodes[0];
				listOfOpenNodes.RemoveAt(0);
				// Console.Write("Closing cell " + nodeToClose.uniqueNumber + " with distance = " + nodeToClose.dijkstraDistance + "\n");

				foreach (Node neighbourtoOpen in nodeToClose.GetNeighboursWithoutShortcuts())
				{
					NodeForPathfinding neighbourAsPathfindingNode = new NodeForPathfinding(neighbourtoOpen);
					if (listOfClosedNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber) != null)
					{
						neighbourAsPathfindingNode = listOfClosedNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber);
					}
					else if (listOfOpenNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber) != null)
					{
						neighbourAsPathfindingNode = listOfOpenNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber);
					}
					else { listOfOpenNodes.Add(neighbourAsPathfindingNode); }
					if (!listOfClosedNodes.Select(NodeForPathfinding => NodeForPathfinding.uniqueNumber).Contains(neighbourAsPathfindingNode.uniqueNumber))
					{
						if (neighbourAsPathfindingNode.dijkstraDistance == double.PositiveInfinity)
						{
							neighbourAsPathfindingNode.predecessor = nodeToClose;
							neighbourAsPathfindingNode.dijkstraDistance = neighbourAsPathfindingNode.findDistanceToStart(startingNode) + neighbourAsPathfindingNode.manhattanDistanceToNode(arrival);
							// Console.Write("We opened node " + neighbourAsPathfindingNode.uniqueNumber + " . Distance to start was " + neighbourAsPathfindingNode.findDistanceToStart(startingNode)
								// + " and distance to finish is approximatly " + neighbourAsPathfindingNode.manhattanDistanceToNode(arrival) +
								// " Sum of distances is : " + neighbourAsPathfindingNode.dijkstraDistance + "\n");
						}
						else
						{
							NodeForPathfinding predecessorWeMightKeep = neighbourAsPathfindingNode.predecessor;
							neighbourAsPathfindingNode.predecessor = nodeToClose;
							double distanceThroughtNodeToClose = neighbourAsPathfindingNode.findDistanceToStart(startingNode) + neighbourAsPathfindingNode.manhattanDistanceToNode(arrival);
							if (distanceThroughtNodeToClose < neighbourAsPathfindingNode.dijkstraDistance) { neighbourAsPathfindingNode.dijkstraDistance = distanceThroughtNodeToClose; }
							else { neighbourAsPathfindingNode.predecessor = predecessorWeMightKeep; }
						}
					}

					foundTheGoal = (neighbourAsPathfindingNode.uniqueNumber == arrival.uniqueNumber);
					if (foundTheGoal) { arrivalAsPathfindingNode = neighbourAsPathfindingNode; break; }
				}
				listOfClosedNodes.Add(nodeToClose);
				// We update the imagebox to show the progression of the pathfinding
			    bmpForPictureBox.SetPixel(nodeToClose.coordinates[0], nodeToClose.coordinates[1], Color.FromArgb(0, 204, 204));
				pb1.Image = bmpForPictureBox;
				pb1.Refresh();

			}

			Console.Write(listOfClosedNodes.Count + " nodes were closed during this search.\n");
			return (arrivalAsPathfindingNode.findPathToStart(startingNode));
		}


		// Function containing the contraction hierarchies least-cost path algorithm
		// For this function to be used, map must be pre-processed. We check by looking at the "Map" object.

		public static List<Node> CHLeastCostPathList(Node arrival, Node departure, Form1 form, Map mapOfPathFinding)
		{
			// We create a bitmap in the form with the departure/arrival points
			// If there is already a picture box, we remove it.
			// We create a new picture box
			PictureBox pb1 = new PictureBox();
			// We display the map, but with the departure and arrival points
			Bitmap bmpForPictureBox = RasterFunctions.MapToBitmapWithArrivalAndDeparture(this, false);
			pb1.Image = bmpForPictureBox;
			// We display everything nicely
			pb1.SizeMode = PictureBoxSizeMode.CenterImage;
			form.Size = new System.Drawing.Size(1000, 1000);
			pb1.Size = new System.Drawing.Size(1000, 1000);
			pb1.SizeMode = PictureBoxSizeMode.CenterImage;
			form.Controls.Add(pb1);

			// We initialize what we need for the search
			List<NodeForPathfinding> forwardListOfOpenNodes = new List<NodeForPathfinding>();
			List<NodeForPathfinding> forwardListOfClosedNodes = new List<NodeForPathfinding>();
			NodeForPathfinding startingForwardNode = new NodeForPathfinding(this.departureNode);
			forwardListOfOpenNodes.Add(startingForwardNode);

			List<NodeForPathfinding> backwardListOfOpenNodes = new List<NodeForPathfinding>();
			List<NodeForPathfinding> backwardListOfClosedNodes = new List<NodeForPathfinding>();
			NodeForPathfinding startingBackwardNode = new NodeForPathfinding(this.arrivalNode);
			backwardListOfOpenNodes.Add(startingBackwardNode);

			bool didTheyMeet = false;

			while (!didTheyMeet)
			{
				// We do one step in the forward Dijkstra search (see Dijkstra search in pathFinding class for comments)
				// Console.Write("One step forward...\n");
				forwardListOfOpenNodes = forwardListOfOpenNodes.OrderByDescending(NodeForPathfinding => NodeForPathfinding.order).ToList();
				NodeForPathfinding nodeToClose = forwardListOfOpenNodes[0];
				forwardListOfOpenNodes.RemoveAt(0);
				// Console.Write("Forward : looking at Node " + nodeToClose.uniqueNumber + "\n");

				foreach (Node neighbourtoOpen in nodeToClose.GetNeighbours())
				{
					NodeForPathfinding neighbourAsPathfindingNode = new NodeForPathfinding(neighbourtoOpen);
					if (forwardListOfClosedNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber) != null)
					{
						neighbourAsPathfindingNode = forwardListOfClosedNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber);
					}
					else if (forwardListOfOpenNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber) != null)
					{
						neighbourAsPathfindingNode = forwardListOfOpenNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber);
					}
					else { forwardListOfOpenNodes.Add(neighbourAsPathfindingNode); }
					if (!forwardListOfClosedNodes.Contains(neighbourAsPathfindingNode))
					{
						if (neighbourAsPathfindingNode.dijkstraDistance == double.PositiveInfinity)
						{
							neighbourAsPathfindingNode.predecessor = nodeToClose;
							neighbourAsPathfindingNode.dijkstraDistance = neighbourAsPathfindingNode.findDistanceToStartCH(startingForwardNode);
						}
						else
						{
							NodeForPathfinding predecessorWeMightKeep = neighbourAsPathfindingNode.predecessor;
							neighbourAsPathfindingNode.predecessor = nodeToClose;
							double distanceThroughtNodeToClose = neighbourAsPathfindingNode.findDistanceToStart(startingForwardNode);
							if (distanceThroughtNodeToClose < neighbourAsPathfindingNode.dijkstraDistance) { neighbourAsPathfindingNode.dijkstraDistance = distanceThroughtNodeToClose; }
							else { neighbourAsPathfindingNode.predecessor = predecessorWeMightKeep; }
						}
					}
				}
				forwardListOfClosedNodes.Add(nodeToClose);
				bmpForPictureBox.SetPixel(nodeToClose.coordinates[0], nodeToClose.coordinates[1], Color.FromArgb(0, 204, 204));
				pb1.Image = bmpForPictureBox;
				pb1.Refresh();

				// We do one step in the backward Dijkstra search
				// Console.Write("One step backward...\n");

				backwardListOfOpenNodes = backwardListOfOpenNodes.OrderBy(NodeForPathfinding => NodeForPathfinding.order).ToList();
				nodeToClose = backwardListOfOpenNodes[0];
				backwardListOfOpenNodes.RemoveAt(0);
				// Console.Write("Backward : looking at Node " + nodeToClose.uniqueNumber + "\n");

				foreach (Node neighbourtoOpen in nodeToClose.GetNeighbours())
				{
					NodeForPathfinding neighbourAsPathfindingNode = new NodeForPathfinding(neighbourtoOpen);
					if (backwardListOfClosedNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber) != null)
					{
						neighbourAsPathfindingNode = backwardListOfClosedNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber);
					}
					else if (backwardListOfOpenNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber) != null)
					{
						neighbourAsPathfindingNode = backwardListOfOpenNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == neighbourtoOpen.uniqueNumber);
					}
					else { backwardListOfOpenNodes.Add(neighbourAsPathfindingNode); }
					if (!backwardListOfClosedNodes.Contains(neighbourAsPathfindingNode))
					{
						if (neighbourAsPathfindingNode.dijkstraDistance == double.PositiveInfinity)
						{
							neighbourAsPathfindingNode.predecessor = nodeToClose;
							neighbourAsPathfindingNode.dijkstraDistance = neighbourAsPathfindingNode.findDistanceToStartCH(startingBackwardNode);
						}
						else
						{
							NodeForPathfinding predecessorWeMightKeep = neighbourAsPathfindingNode.predecessor;
							neighbourAsPathfindingNode.predecessor = nodeToClose;
							double distanceThroughtNodeToClose = neighbourAsPathfindingNode.findDistanceToStart(startingBackwardNode);
							if (distanceThroughtNodeToClose < neighbourAsPathfindingNode.dijkstraDistance) { neighbourAsPathfindingNode.dijkstraDistance = distanceThroughtNodeToClose; }
							else { neighbourAsPathfindingNode.predecessor = predecessorWeMightKeep; }
						}
					}
				}
				backwardListOfClosedNodes.Add(nodeToClose);
				bmpForPictureBox.SetPixel(nodeToClose.coordinates[0], nodeToClose.coordinates[1], Color.FromArgb(0, 204, 204));
				pb1.Image = bmpForPictureBox;
				pb1.Refresh();

				// Is there an open cell that is both in the foward and backward search ?

				if (forwardListOfClosedNodes.Select(NodeForPathfinding => NodeForPathfinding.uniqueNumber).ToList().Intersect(backwardListOfClosedNodes.Select(NodeForPathfinding => NodeForPathfinding.uniqueNumber).ToList()).Count() != 0) didTheyMeet = true;
				// Console.Write("Intersect count : " + forwardListOfClosedNodes.Intersect(backwardListOfClosedNodes).Count() + "\n");
			}

			// If so, the search is other. We take the path using the predecessors.

			List<Node> unionListOfNodes = new List<Node>();

			// First, we have to treat the case in which the arrival or depature node was found uniquely by the backward or the forward search.
			if (forwardListOfClosedNodes[forwardListOfClosedNodes.Count - 1].uniqueNumber == arrivalNode.uniqueNumber)
			{
				// We allow the return of the path from the node just before the starting or arrival node to avoid errors of predecessors
				unionListOfNodes = forwardListOfClosedNodes[forwardListOfClosedNodes.Count - 2].findPathToStart(startingForwardNode);
			}
			else if (backwardListOfClosedNodes[backwardListOfClosedNodes.Count - 1].uniqueNumber == departureNode.uniqueNumber)
			{
				unionListOfNodes = backwardListOfClosedNodes[backwardListOfClosedNodes.Count - 2].findPathToStart(startingBackwardNode);
			}
			// Now, we treat the case in which arrival and departure found each other
			else
			{
				// First, we find the uniquen number of the node where they met.
				List<int> forwardUniqueNumbers = forwardListOfClosedNodes.Select(NodeForPathfinding => NodeForPathfinding.uniqueNumber).ToList();
				List<int> backwardUniqueNumbers = backwardListOfClosedNodes.Select(NodeForPathfinding => NodeForPathfinding.uniqueNumber).ToList();
				int nodeWhereTheyMet = forwardUniqueNumbers.Intersect(backwardUniqueNumbers).ToList()[0];

				// We take the path from this node to the start and to the arrival with each list
				List<Node> pathFromMeetingNodeToStart = forwardListOfClosedNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == nodeWhereTheyMet).findPathToStart(startingForwardNode);
				List<Node> pathFromMeetingNodeToEnd = backwardListOfClosedNodes.Find(NodeForPathfinding => NodeForPathfinding.uniqueNumber == nodeWhereTheyMet).findPathToStart(startingBackwardNode);

				// We join those two lists
				unionListOfNodes = pathFromMeetingNodeToStart;
				unionListOfNodes.AddRange(pathFromMeetingNodeToEnd);
			}


			// We add the first node of the list to the pathlist

			this.LeastCostPath.AddRange(unionListOfNodes);

			// We look at each node one by one in the list until we arrive
			// at the arrival node.


			// we look at the node at its link to the next node


			// If the link is a shortcut, we open the short and add every node inside it to the pathlist

			// Else, we just add the next node to the path list.

			// Current node becomes next node





			return (arrivalAsPathfindingNode.findPathToStart(startingNode));
		}






		// End of class "Pathfinding"
	}






	class NodeForPathfinding : Node
	{
		public double dijkstraDistance;
		public NodeForPathfinding predecessor;

		public NodeForPathfinding(Node nodeToTransform)
		{
			this.order = nodeToTransform.order;
			this.height = nodeToTransform.height;
			this.uniqueNumber = nodeToTransform.uniqueNumber;
			this.coordinates = nodeToTransform.coordinates;
			this.road = nodeToTransform.road;
			this.arrival = nodeToTransform.arrival;
			this.departure = nodeToTransform.departure;
			this.contracted = nodeToTransform.contracted;
			links = nodeToTransform.links;

			dijkstraDistance = double.PositiveInfinity;
		}

		public double findDistanceToStart(NodeForPathfinding startingNode)
		{
			// Console.Write("FINDING DISTANCE TO START FOR NODE " + this.uniqueNumber + " . STARTING NODE IS : " + startingNode.uniqueNumber + "\n");
			double distanceToStart = 0;
			NodeForPathfinding currentNode = this;
			NodeForPathfinding nextPredecessor = this.predecessor;
			bool foundStartingNode = false;

			while (!foundStartingNode)
			{
				// DebugFunctions.DisplayNodeInfoInConsole(currentNode, "currentNode");
				// DebugFunctions.DisplayNodeInfoInConsole(nextPredecessor, "nextPredecessor");
				// DebugFunctions.DisplayLinksOfNode(currentNode, "currentNode");
				Link linkBetweenNodes = currentNode.links.Find(Link => Link.nodes[0].uniqueNumber == nextPredecessor.uniqueNumber || Link.nodes[1].uniqueNumber == nextPredecessor.uniqueNumber);
				try { distanceToStart = distanceToStart + linkBetweenNodes.cost; }
				catch
				{
					Console.Write("\nERROR : LINK MISSING FROM A CERTAIN NODE\n");
					DebugFunctions.DisplayNodeInfoInConsole(currentNode, "currentNode");
					DebugFunctions.DisplayLinksOfNode(currentNode, "currentNode");
					DebugFunctions.DisplayNodeInfoInConsole(nextPredecessor, "nextPredecessor");
					DebugFunctions.DisplayLinksOfNode(nextPredecessor, "nextPredecessor");
				}
				if (nextPredecessor == startingNode) foundStartingNode = true;
				else
				{
					currentNode = nextPredecessor;
					nextPredecessor = currentNode.predecessor;
				}
			}

			return (distanceToStart);
		}


		public double findDistanceToStartCH(NodeForPathfinding startingNode)
		{
			// Console.Write("FINDING DISTANCE TO START FOR NODE " + this.uniqueNumber + " . STARTING NODE IS : " + startingNode.uniqueNumber + "\n");
			double distanceToStart = 0;
			NodeForPathfinding currentNode = this;
			NodeForPathfinding nextPredecessor = this.predecessor;
			bool foundStartingNode = false;

			while (!foundStartingNode)
			{
				// DebugFunctions.DisplayNodeInfoInConsole(currentNode, "currentNode");
				// DebugFunctions.DisplayNodeInfoInConsole(nextPredecessor, "nextPredecessor");
				// DebugFunctions.DisplayLinksOfNode(currentNode, "currentNode");
				Link linkBetweenNodes = currentNode.links.Find(Link => Link.nodes[0].uniqueNumber == nextPredecessor.uniqueNumber || Link.nodes[1].uniqueNumber == nextPredecessor.uniqueNumber);
				// we use linkBetweenNodes.cost/ linkBetweenNodes.cost just to have "1", but to make sure that the link exist. It will make an error if it doesnt.
				try { distanceToStart = distanceToStart + linkBetweenNodes.cost; }
				catch
				{
					Console.Write("\nERROR : LINK MISSING FROM A CERTAIN NODE\n");
					DebugFunctions.DisplayNodeInfoInConsole(currentNode, "currentNode");
					DebugFunctions.DisplayLinksOfNode(currentNode, "currentNode");
					DebugFunctions.DisplayNodeInfoInConsole(nextPredecessor, "nextPredecessor");
					DebugFunctions.DisplayLinksOfNode(nextPredecessor, "nextPredecessor");
				}
				if (nextPredecessor == startingNode) foundStartingNode = true;
				else
				{
					currentNode = nextPredecessor;
					nextPredecessor = currentNode.predecessor;
				}
			}

			return (distanceToStart);
		}

		public List<Node> findPathToStart(NodeForPathfinding startingNode)
		{
			// Console.Write("FINDING DISTANCE TO START FOR NODE " + this.uniqueNumber + " . STARTING NODE IS : " + startingNode.uniqueNumber + "\n");
			List<Node> pathToStart = new List<Node>();
			NodeForPathfinding currentNode = this;
			NodeForPathfinding nextPredecessor = this.predecessor;
			bool foundStartingNode = false;

			while (!foundStartingNode)
			{
				pathToStart.Add(currentNode);
				if (nextPredecessor == startingNode) foundStartingNode = true;
				else
				{
					currentNode = nextPredecessor;
					nextPredecessor = currentNode.predecessor;
				}
			}

			return (pathToStart);
		}

		public double euclidianDistanceToNode(Node otherNode)
		{
			double euclidianDistance = Math.Sqrt(Math.Pow((this.coordinates[0] - otherNode.coordinates[0]), 2) + Math.Pow((this.coordinates[1] - otherNode.coordinates[1]), 2));

			return (euclidianDistance);
		}

		public double manhattanDistanceToNode(Node otherNode)
		{
			double manhattanDistance = Math.Abs(this.coordinates[0] - otherNode.coordinates[0]) + Math.Abs(this.coordinates[1] - otherNode.coordinates[1]);

			return (manhattanDistance * (1.0 + (1/(manhattanDistance*2))));
		}


	}

}
