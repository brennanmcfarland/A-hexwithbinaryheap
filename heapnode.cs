public class heapnode {

	public int fcost, //fcost in pathfinding
		gcost, //gcost in pathfinding
		hcost, //hcost in pathfinding
		x,	   //x coordinate on map
		y,		//y coordinate on map

	public heapnode parent; //parent of this node
	public heapnode(int gcost, int hcost, int x,int y, heapnode parent)
	{
		this.gcost = gcost;
		this.hcost = hcost;
		calculatefcost();
		this.x = x;
		this.y = y;
		this.parent = parent;
	}
	public void calculatefcost()
	{
		fcost = gcost + hcost;
	}
	public string tostring()
	{
		return ("(" + x + "," + y + ")F:" + fcost);
	}
}
