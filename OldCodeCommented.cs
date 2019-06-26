using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProgramCH
{
	class OldCodeCommented
	{
		/*

		// Code pour faire un bouton qui affiche une image.
		private void button1_Click(object sender, EventArgs e)
		{
		// Tout d'abord, on retire la picturebox qui peut se trouver deja
		// dans le form (une autre image). Pour ca, on regarde si elle est la
		// (en regardant le nombre de controls présents dans le form). Puis,
		// on retire la picture box qui doit être le dernier controle. Ce genre
		// de choses peut changer.
			if (this.Controls.Count == 4) this.Controls.RemoveAt(3);
			// On creer une boite a image (picturebox)
			PictureBox pb1 = new PictureBox();
			// On met une image dedans; soit en disant ou l'image est, soit en
			// insérant un objet qui hérite de la classe "Image" de System.Drawing
			// comme un bitmap.
			pb1.ImageLocation = "https://i.pinimg.com/originals/08/77/bd/0877bd4fb5a47467f1cdd8fc0446194a.jpg";
			// On change la taille du formulaire pour le mettre à la taille
			// de notre image.
			this.Size = new System.Drawing.Size(1000, 1000);
			// On donne la bonne taille pour notre boite a image
			pb1.Size = new System.Drawing.Size(1000, 1000);
			// On centre notre image dans la boite a image;
			// Si elle est plus grande que notre boite a image, 
			// on ne verra pas tout. Sinon, on peut utiliser
			// la fonction "StrechImage" pour la contraindre
			// a prendre la bonne taille.
			pb1.SizeMode = PictureBoxSizeMode.CenterImage;
			this.Controls.Add(pb1);
		}


				public static Bitmap CreateBitmapPerlin1()
		{

			Bitmap bmp = new Bitmap(800, 800);

			for (int x = 0; x < 800; x++)
			{
				for (int y = 0; y < 800; y++)
				{
					int calc = (int)(((Noise2d.Noise(x, y) + 1) / 2) * 255);
					bmp.SetPixel(x, y, Color.FromArgb(calc, calc, calc));
				}
			}

			return (bmp);

		}


		// Code de premier essai pour la recherche de CH
		this.CHLeastCostPath.Clear();
			// Departure is the first node we consider
			Node currentForwardNode = this.departureNode;
			Node previousForwardNode = this.departureNode;
			Node currentBackwardNode = this.arrivalNode;
			Node previousBackwardNode = this.arrivalNode;
			// While we haven't found the arrival node as the next node...
			bool forwardSearchIsDone = false;
			bool didTheyMeet = false;


			while (!forwardSearchIsDone)
			{
				Console.Write("Current forward node is " + currentForwardNode.uniqueNumber + "\n");
				// We add the considered neighbour to the path
				this.CHLeastCostPath.Add(currentForwardNode);
				// We look at all of the neighbours of the node considered
				// We take the neighbour with the highest order (smallest number)
				// and we make it the next considered node
				List<Node> neighboursOfCurrent = currentForwardNode.GetNeighbours();
				neighboursOfCurrent.OrderByDescending(Node => Node.order);
				if (neighboursOfCurrent[0].uniqueNumber == previousForwardNode.uniqueNumber) forwardSearchIsDone = true;
				else previousForwardNode = currentForwardNode; currentForwardNode = neighboursOfCurrent[0];
			}

			while (!didTheyMeet)
			{
				Console.Write("Current backward node is " + currentBackwardNode.uniqueNumber + "\n");
				Console.Write("Current forward node is " + currentForwardNode.uniqueNumber + "\n");
				// We add the considered neighbour to the path
				this.CHLeastCostPath.Add(currentBackwardNode);
				// We look at all of the neighbours of the node considered
				// We take the neighbour with the highest order (smallest number)
				// and we make it the next considered node
				List<Node> neighboursOfCurrent = currentBackwardNode.GetNeighbours();
				neighboursOfCurrent.OrderBy(Node => Node.order);
				if (neighboursOfCurrent[0].uniqueNumber == currentForwardNode.uniqueNumber) didTheyMeet = true;
				else previousBackwardNode = currentBackwardNode; currentBackwardNode = neighboursOfCurrent[0];
				// If the considered node the arrival ? If so, we end.
			}


	   */
	}
}
