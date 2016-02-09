using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class binaryheapDemo : MonoBehaviour {

	public int tilemaplength;
	public int tilemapwidth;
	public GameObject[,] tilemap;
	List<int[]> pathlist = null; //list holding x and y values of a path, used in findpath, null if no path found

	public List<int[]> getpath()
	{
		return pathlist;
	}

	public void findpath(int startX, int startY, int endX, int endY, int range)
	{
		binaryheap openset = new binaryheap(new heapnode(0,
			estimatehcost(startX, startY, endX, endY), startX, startY, null,0));
		List<heapnode> closedset = new List<heapnode>();
		heapnode parentnode = openset.getnode(1);
		heapnode temp; //temporary node for recalculating better paths
		bool isinclosedset;

		if(tiles == null)
		{
			while(openset.getsize() != 0)
			{
				//check if the parent node is the goal, if so the best path is found
				if(parentnode.x == endX && parentnode.y == endY)
				{
					tracepath(closedset[closedset.Count-1]);
					return; //exit the method
				}
				//make the parent node the tile with the lowest fcost
				parentnode = openset.getlowestfcostnode();
				//remove parent node from the open set
				openset.removenode();
				//add parent node to the closed set
				closedset.Add(parentnode);

				//add all legal adjacent tiles to the list
				for(int i=-1; i<=1; i++)
				{
					for(int j=-1; j<=1; j++)
					{
						if(i != j) //don't check the parent tile or 1,1 or -1,-1, those are illegal
						{
							//checking to see if in closed list
							isinclosedset = false;
							for(int k=0; k<closedset.Count; k++)
							{
								if(closedset[k].x == parentnode.x+i && closedset[k].y == parentnode.y+j)
									isinclosedset = true;
							}
							//checks if........
							if((parentnode.x+i >=0 && parentnode.x+i < tilemap.GetLength(1)
								 && parentnode.y+j >=0 && parentnode.y+j < tilemap.GetLength(0) //A. tilemap goes that far
							   && tilemap[parentnode.x+i, parentnode.y+j] != null //that tilemap space has a gameobject
							   && tilemap[parentnode.x+i, parentnode.y+j].GetComponent<Tile>() != null //B. that tilemap space has a tile
							   && tilemap[parentnode.x+i,parentnode.y+j].GetComponent<Tile>().getifobstacle() == false //C.if not obstructed
							   && isinclosedset == false//D. the tile is not on the closed list
							   && parentnode.gcost+addtogcost(parentnode.x, parentnode.y, parentnode.x+i, parentnode.y+j)
								 	<= range) //the new gcost minus engagement penalty is not higher than the movement range
							   && (openset.getnode(parentnode.x+i, parentnode.y+j) == null//E. not on openlist
							   || parentnode.gcost+addtogcost(parentnode.x, parentnode.y, parentnode.x+i, parentnode.y+j)
								 	< parentnode.gcost))//F. gscore less than parent
							{
								//the outer if loop is important for tracing the path
								if(openset.getnode(parentnode.x+i, parentnode.y+j) == null) //if not on the open set
								{
									openset.addtoheap(parentnode.gcost+addtogcost
									(parentnode.x, parentnode.y, parentnode.x+i, parentnode.y+j),
									estimatehcost(parentnode.x+i, parentnode.y+j, endX, endY), parentnode.x+i, parentnode.y+j, parentnode);//add it
								}
								else if(parentnode.gcost+addtogcost(parentnode.x, parentnode.y, parentnode.x+i, parentnode.y+j)
								+estimatehcost(parentnode.x+i, parentnode.y+j, endX, endY)
								< openset.getnode(parentnode.x+i, parentnode.y+j).parent.fcost) //if on open set, see if this is the better path
								{
									//reset the parent to parentnode
									temp = openset.getnode(parentnode.x+i, parentnode.y+j);
									temp.parent = closedset[closedset.Count-1];
									//recalculate gcost and hcost
									temp.gcost = temp.parent.gcost+addtogcost(parentnode.x, parentnode.y, parentnode.x+i, parentnode.y+j);
									temp.hcost = estimatehcost(temp.x, temp.y, endX, endY);
									openset.replacenode(temp);
								}
							}
						}
					}
				}
			}
		}
		pathlist = null;
		return; //if a path is not found
	}

	int estimatehcost(int x, int y, int endX, int endY)
	{
		return Mathf.Abs(endX-x)+Mathf.Abs (endY-y);
	}

	int addtogcost(int parentx, int parenty, int x, int y)
	{
		int parentnodeheight = (int)(tilemap[parentx,parenty].GetComponent<Tile>().getheight(true));
		int nodeheight = (int)(tilemap[x,y].GetComponent<Tile>().getheight(true));
		if(parentnodeheight < nodeheight) //if the next tile is higher than the parent one
		{
			return 1+ nodeheight-parentnodeheight;
		}
		else //if the next tile is at the same height or lower than the parent
		{
			return 1;
		}
	}

	void tracepath(heapnode node) //trace the path
	{
		pathlist = new List<int[]>();
		if(node.parent != null)
		{
			tracepath(node.parent);
		}
		//Debug.Log(node.x + "," + node.y + " on path");
		pathlist.Add(new int[]{node.x, node.y}); //add the node to the list
		return;
	}
}
