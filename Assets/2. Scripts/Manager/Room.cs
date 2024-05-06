using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private int _addressX;
    private int _addressY;


    [Header("Nodes")]
    [SerializeField] GameObject _upNode;
    [SerializeField] GameObject _downNode;
    [SerializeField] GameObject _leftNode;
    [SerializeField] GameObject _rightNode;
    private List<GameObject> _nodeList;
    private Dictionary<NodeDir, GameObject> _map;

    [Header("Doors")]
    [SerializeField] GameObject _upDoor;
    [SerializeField] GameObject _downDoor;
    [SerializeField] GameObject _leftDoor;
    [SerializeField] GameObject _rightDoor;

}
