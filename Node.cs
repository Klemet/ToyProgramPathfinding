using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProgramCH
{
	class Node
	{
		public int uniqueNumber;
		public int[] coordinates;
		public int order;
		public int height;
		public bool road;
		public bool arrival;
		public bool departure;
		public bool contracted;

		public List<Link> links;

		public Node(int initialUniqueNumber, int coordX, int coordY, int initialHeight)
		{
			this.order = 0;
			this.height = initialHeight;
			this.uniqueNumber = initialUniqueNumber;
			this.coordinates = new int[2] { coordX, coordY };
			this.road = false;
			this.arrival = false;
			this.departure = false;
			this.contracted = false;
			links = new List<Link>();
		}

		// Empty constructor for inheritance
		public Node()
		{
		}

		// Easily get coordinates
		public int x
		{
			get { return (this.coordinates[0]); }
		}

		public int y
		{
			get { return (this.coordinates[1]); }
		}


		public List<Node> GetNeighbours()
		{
			List<Node> listOfNeighbours = new List<Node>();

			foreach (Link linkOfNode in this.links)
			{
				Node neighbour;
				if (linkOfNode.nodes[0].uniqueNumber == this.uniqueNumber) neighbour = linkOfNode.nodes[1];
				else neighbour = linkOfNode.nodes[0];
				// Doesn't seem to work ?
				// neighbour = linkOfNode.nodes. (new List<Node> { this }).First();
				bool isNeighbourInListOfNeighboursAlready = listOfNeighbours.Contains(neighbour);
				if (!isNeighbourInListOfNeighboursAlready) listOfNeighbours.Add(neighbour);
			}

			return (listOfNeighbours);
		}

		public List<Node> GetNeighboursWithoutShortcuts()
		{
			List<Node> listOfNeighbours = new List<Node>();

			foreach (Link linkOfNode in links.FindAll(Link => Link.shortcut == false))
			{
				Node neighbour;
				if (linkOfNode.nodes[0].uniqueNumber == this.uniqueNumber) neighbour = linkOfNode.nodes[1];
				else neighbour = linkOfNode.nodes[0];
				// Doesn't seem to work ?
				// neighbour = linkOfNode.nodes. (new List<Node> { this }).First();
				bool isNeighbourInListOfNeighboursAlready = listOfNeighbours.Contains(neighbour);
				if (!isNeighbourInListOfNeighboursAlready) listOfNeighbours.Add(neighbour);
			}

			return (listOfNeighbours);
		}

	}
}
