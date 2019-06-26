using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProgramCH
{
	class Link
	{
		public bool shortcut;
		public Node[] nodes;
		public double cost;

		// First way to create a link for two nodes next to one another
		public Link(Node node1, Node node2)
		{
			nodes = new Node[2] { node1, node2 };
			shortcut = false;
			cost = Link.InitializeCost(node1, node2);
		}

		// Second to create a link for when it is a shortcut
		public Link(Node node1, Node node2, double costOfShortcut)
		{
			nodes = new Node[2] { node1, node2 };
			shortcut = true;
			cost = costOfShortcut;
		}

		public static double InitializeCost(Node node1, Node node2)
		{
			double cost;

			// If one of the nodes is in water, it's going to be costly to go there)
			if (node1.height < 100 || node2.height < 100) cost = 8;
			else cost = 1 + Math.Pow(Math.Abs(node1.height - node2.height), 2);

			return (cost);
		}
	}
}
