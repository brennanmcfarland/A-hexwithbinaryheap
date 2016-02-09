using System.Collections;
using System.Collections.Generic;

public class binaryheap : Object{

	const int MAXHEAPSIZE = 100; //maximum size for the array, max # nodes
	int nodecounter = 1; //points to next open spot in heaparray

	heapnode[] heaparray = new heapnode[MAXHEAPSIZE];
	heapnode tempnode = null; //temporary storage pointer for nodes

	public binaryheap()
	{

	}

	public binaryheap(heapnode firstnode)
	{
		heaparray[1] = firstnode;
		nodecounter++;
	}
	public void addtoheap(int gcost, int hcost, int x, int y, heapnode parent)
	{
		//append to the end of the array
		heaparray[nodecounter] = new heapnode(gcost, hcost, x, y, parent);
		nodecounter++;
		//compare to other values in the array until it is sorted
		int index = nodecounter-1; //index of the current node
		while(index>1 && heaparray[index].fcost < heaparray[index/2].fcost)
		{
			swapwithparent(index);
			index = index/2;
		}
	}
	public void addtoheap(heapnode node)
	{
		//append to the end of the array
		heaparray[nodecounter] = node;
		nodecounter++;
		//compare to other values in the array until it is sorted
		int index = nodecounter-1; //index of the current node
		if(nodecounter>2) //if there's at least 2 elements in the array
		{
			while(index>1 && heaparray[index].fcost < heaparray[index/2].fcost)
			{
				swapwithparent(index);
				index = index/2;
			}
		}
	}
	public int getsize()
	{
		return nodecounter-1;
	}
	public heapnode getnode(int index)
	{
		return heaparray[index];
	}
	public heapnode getnode(int x, int y)
	{
		for(int i=1; i<nodecounter; i++)
		{
			if(heaparray[i].x == x && heaparray[i].y == y)
			{
				return heaparray[i];
			}
		}
		return null;
	}

	public void replacenode(heapnode newnode)
	{
		for(int i=1; i<nodecounter; i++)
		{
			if(heaparray[i].x == newnode.x && heaparray[i].y == newnode.y)
			{
				heaparray[i] = newnode;
				return ;
			}
		}
	}

	public heapnode getlowestfcostnode()
	{
		if(nodecounter > 1)
		{
			return heaparray[1];
		}
		else
		{
			return null;
		}
	}
	public void removenode()
	{
		heaparray[1] = heaparray[nodecounter-1]; //replace old first element with last element
		nodecounter--; //delete last element

		//compare to other values in the array until it's sorted
		minheapify(this, 1);
	}
	void minheapify(binaryheap heap, int i)
	{
		int left = 2*i;
		int right = 2*i+1;
		int smallest = i;

		if(left <= heap.getsize() && heap.getnode(left).fcost < heap.getnode(smallest).fcost)
			smallest = left;
		if(right <= heap.getsize() && heap.getnode(right).fcost < heap.getnode(smallest).fcost)
			smallest = right;
		if(i != smallest)
		{
			heap.swapwithparent(smallest);//swap heap[i] and heap[smallest]
			minheapify(heap, smallest);
		}
	}
	public void swapwithparent(int index) //swaps a node with it's parent
	{
		//swap positions in the array
		tempnode = heaparray[index]; //save current node to temporary location
		heaparray[index] = heaparray[index/2]; //replace current node with parent node
		heaparray[index/2] = tempnode; //replace parent node with saved current node
	}
	public List<int[]> tracepath(heapnode node) //trace the path
	{
		List<int[]> pathlist = new List<int[]>();
		if(node.parent != null)
		{
			pathlist.AddRange(tracepath(node.parent));
		}
		pathlist.Add(new int[]{node.x, node.y}); //add the node to the list
		return pathlist;
	}
	public void debugheap()
	{
		for(int i=0; i<5; i++)
		{
			string temp = "";
			for(int j=(int)Mathf.Pow(2f,(float)(i)); j<(int)Mathf.Pow(2f,(float)(i+1)); j++)
			{
				if(heaparray[j] != null)
					temp += heaparray[j].tostring();
			}
		}
	}
}
