using UnityEngine;

public class Node : MonoBehaviour
{
    public Node nextNode;

    public Node GetNextNode()
        => nextNode;
}
