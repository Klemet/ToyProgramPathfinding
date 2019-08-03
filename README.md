## A "Toy" program to test several pathfinding algorithms


This program allows the user to easily test several pathfinding algorithms to create simple roads on procedurally generated landscapes.

Features :
- A clear UI that is easy to use
- A visualisation of procedurally generated landscapes with lakes and elevation
- The possibility to customize the procedural generation of the landscape (that is based on perlin noise)
- The possibility to display the mean cost associated with every pixel of the map
- A console displaying informations concerning the functionning of the algorithms for pathfinding
- 3 different pathfinding algorithms to try : Dijkstra (slowest, but simplest), A* (Dijkstra with a heuristic), and contraction hierarchies.
- Animation of the search process of the algorithms to easily visualise their progress (might freeze after a while)

CAUTION : The Contraction Hierarchies algorithm is currently NON FONCTIONNAL.  
 
![Interface](screenshots/screenshot1.png)
![RoadInConstruction](screenshots/screenshot2.png)
![RoadFinished](screenshots/screenshot3.png)
 
 
**Author:**

Clément Hardy

PhD Student at the Université du Québec à Montréal

Mail : clem.hardy@outlook.fr


**Acknowledgments:**

Many problems during the programming of this program were fixed thanks to answers available on Stack Overflow. I thank the community for their amazing work.

The perlin noise code comes from Mattias Fagerlund's Coding Blog at https://lotsacode.wordpress.com/2010/02/24/perlin-noise-in-c/ . Thank you for his clear translation of the functions in C#.