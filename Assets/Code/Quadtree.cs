using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Define the Quadtree node class.
public class QuadtreeNode
{
    // Define the node boundaries and objects list.
    public Rect bounds;
    public List<ObjectOnBoard> objects;
    public int level;
    public const int MAX_OBJECTS_PER_NODE = 10;
    public const int MAX_DEPTH = 5;
    public QuadtreeNode[] children;

    public QuadtreeNode(Rect bounds, int level)
    {
        this.bounds = bounds;
        this.level = level;
        objects = new List<ObjectOnBoard>();
        children = new QuadtreeNode[4]; // Assuming a QuadtreeNode can have at most four children (for 2D Quadtree).
    }

    // Method to subdivide the node into child nodes.
    private void Subdivide()
    {
        float subWidth = bounds.width / 2f;
        float subHeight = bounds.height / 2f;
        float x = bounds.x;
        float y = bounds.y;

        // Create child nodes and set their boundaries.
        children[0] = new QuadtreeNode(new Rect(x, y, subWidth, subHeight), level + 1);
        children[1] = new QuadtreeNode(new Rect(x + subWidth, y, subWidth, subHeight), level + 1);
        children[2] = new QuadtreeNode(new Rect(x, y + subHeight, subWidth, subHeight), level + 1);
        children[3] = new QuadtreeNode(new Rect(x + subWidth, y + subHeight, subWidth, subHeight), level + 1);
    }

    // Method to check if the node has been subdivided into child nodes.
    public bool HasChildNodes()
    {
        return children[0] != null;
    }


    // Method to subdivide the node and insert ObjectOnBoard objects as needed.
    // This will involve recursively creating child nodes if necessary.
    public void Insert(ObjectOnBoard obj)
    {
        // If this node doesn't contain the object, check if it falls within child nodes' bounds.
        if (!bounds.Contains(obj.Place))
            return;

        // If this node can subdivide and is not already subdivided, do so.
        if (objects.Count > MAX_OBJECTS_PER_NODE && level < MAX_DEPTH && children[0] == null)
        {
            Subdivide();
            // Re-insert objects into child nodes.
            var objectsCopy = new List<ObjectOnBoard>(objects);
            objects.Clear();
            foreach (var objInNode in objectsCopy)
            {
                Insert(objInNode);
            }
        }

        // Insert the object into appropriate nodes.
        if (children[0] != null)
        {
            foreach (var child in children)
            {
                child.Insert(obj);
            }
        }
        else
        {
            objects.Add(obj);
        }
    }

    // Method to remove an ObjectOnBoard from this node and its children.
    public ObjectOnBoard Remove(ObjectOnBoard obj)
    {
        // If this node doesn't contain the object, return null or an appropriate value.
        if (!objects.Contains(obj))
            return null;

        // Remove the object from this node's list.
        objects.Remove(obj);

        // ... handle additional logic here if needed ...

        // Return the removed object.
        return obj;
    }

    // ... other code ...
}

public class Quadtree : MonoBehaviour
{
  


        private QuadtreeNode root;
        public List<ObjectOnBoard> objectsOnBoard;

        // Initialize the Quadtree with the world bounds.
        public Quadtree(Rect worldBounds, List<ObjectOnBoard> objectsOnBoard)
        {
            root = new QuadtreeNode(worldBounds, 0);
            this.objectsOnBoard = objectsOnBoard;
            BuildQuadtree(root, objectsOnBoard);
        }

        private void BuildQuadtree(QuadtreeNode node, List<ObjectOnBoard> objects)
        {
            foreach (var obj in objects)
            {
                node.Insert(obj);
            }
        }

        // Method to add a new ObjectOnBoard to the objectsOnBoard list and update the Quadtree.
        public void AddObjectOnBoard(ObjectOnBoard newObj)
        {
            objectsOnBoard.Add(newObj);
            root.Insert(newObj);
        }

        // Method to remove an ObjectOnBoard from the objectsOnBoard list and update the Quadtree.
        public void RemoveObjectOnBoard(ObjectOnBoard objToRemove)
        {
            objectsOnBoard.Remove(objToRemove);
            // If the object is still in the Quadtree, you need to remove it from the Quadtree as well.
            // Note: You may need to implement a way to check if an object exists in the Quadtree efficiently (e.g., using a dictionary or hash set).
            root.Remove(objToRemove);
        }

        // Method to retrieve nearby objects based on the player's position.
        public List<ObjectOnBoard> GetObjectsNearPlayer(Vector3 playerPosition, float radius)
        {
            // Implement the logic to retrieve nearby objects based on the player's position.
            // This will involve traversing the Quadtree and finding the cells that intersect with the player's area of interest.
            // Return a list of nearby ObjectOnBoard objects.

            List<ObjectOnBoard> nearbyObjects = new List<ObjectOnBoard>();
            GetObjectsNearPlayerRecursive(root, playerPosition, radius, ref nearbyObjects);
            return nearbyObjects;
        }

        // Method to recursively find nearby objects based on the player's position.
        private void GetObjectsNearPlayerRecursive(QuadtreeNode node, Vector3 playerPosition, float radius, ref List<ObjectOnBoard> result)
        {
            foreach (var obj in node.objects)
            {
                // You can further check if the object's position is within the actual radius (z-axis excluded).
                result.Add(obj);
            }


            if (node == null)
            {

                return;
            }

            // Check if the node intersects with the player's area of interest.
            if (Vector2.Distance(new Vector2(playerPosition.x, playerPosition.y), node.bounds.center) < radius + Mathf.Max(node.bounds.width, node.bounds.height) / 2f)
            {
                foreach (var obj in node.objects)
                {
                    // You can further check if the object's position is within the actual radius (z-axis excluded).
                    result.Add(obj);

                }

                // Continue searching in child nodes.
                if (node.HasChildNodes())
                {
                    foreach (var child in node.children)
                    {
                        GetObjectsNearPlayerRecursive(child, playerPosition, radius, ref result);
                    }
                }
            }
        }
    


}
