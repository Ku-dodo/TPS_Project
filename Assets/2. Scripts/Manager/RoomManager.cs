using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Room Pool")]
    [SerializeField] int _width;
    [SerializeField] int _height;

    private int[,] RoomPositions;


    private void Awake()
    {
        RoomPositions = new int[_width, _height];
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}

public enum NodeDir
{
    UP, DOWN, LEFT, RIGHT
}