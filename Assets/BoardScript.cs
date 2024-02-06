using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public enum PieceType
{
    Player,Shop,Catapolt,CrossBolt
}
[System.Serializable]
public class BoardPieces
{
    public PieceType type;
    public GameObject prefab;
}
public class BoardScript : MonoBehaviour
{
    public static BoardScript instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;

        ItemList.CreateItems();
    }

    [SerializeField] Vector2Int size;
    [OnValueChanged("UpdateLocations")]
    [SerializeField] Vector2 offset;

    [SerializeField] GameObject spacePrefab;
    [ShowInInspector] public Dictionary<Vector2Int, SpaceScript> board = new Dictionary<Vector2Int, SpaceScript>();
    [SerializeField] BoardPieces[] piecePrefabs = new BoardPieces[0];

    // Start is called before the first frame update
    [Button]
    void CreateBoard()
    {
        if (board.Count > 0)
            foreach (SpaceScript spaceScript in board.Values)
                Destroy(spaceScript.gameObject);

        board.Clear();

        for(int x = 0; x < size.x;x++)
            for (int y = 0; y < size.y; y++)
            {
                GameObject obj = Instantiate(spacePrefab, transform);
                obj.GetComponent<SpaceScript>().loc = new Vector2Int(x, y);
                board.Add(new Vector2Int(x, y), obj.GetComponent<SpaceScript>());
            }

        UpdateLocations();
    }

    void UpdateLocations()
    {
        if (board.Count == 0)
            return;

        foreach (Vector2Int loc in board.Keys)
        {
            board[loc].transform.position = new Vector3(loc.x * offset.x, loc.y * offset.y);
        }
    }

    public bool CheckSpace(Vector2Int loc, out Vector3 newPos)
    {
        newPos = Vector3.zero;

        if (board.Count == 0)
        {
            Debug.LogError("Board is Empty!!");
            return false;
        }
        if (loc.x < 0 || loc.y < 0 || loc.x >= size.x || loc.y >= size.y)
            return false;

        if (!board.TryGetValue(loc, out SpaceScript space))
            return false;

        newPos = space.transform.position;

        return board[loc].available;
    }
}


