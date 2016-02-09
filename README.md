# A-hexwithbinaryheap
An implementation of the A* algorithm for a hexagon grid, using a binary heap

Taken from an independent Unity game I'm currently working on.  The Demo class contains an implementation of the A* algorithm for
a hexagon tilemap, able to route around obstacles, stay in the bounds of the map and check for path differences such as height
discrepancies.  Since it's a snippet taken from a much larger project, it won't compile by itself, so it's more of a proof of
concept, however it does indeed work very well when implemented.  The real work is done by the findpath() method.

The algorithm, of course, relies on a binary heap which I've also written (and which is itself complete here).  It contains the
standard operations of a binary heap: addtoheap() given a node and a node's data, getsize(), getnode() by its index or by its
x and y position, replacenode(), minheapify() (optimize), etc.  The heap is organized by lowest fcost, ie, the shortest distance
from the start.  I tried to use as many standard pathfinding and binary heap terms as possible in order to avoid confusion.  Each
node contains its path cost, and is immutable once created.
