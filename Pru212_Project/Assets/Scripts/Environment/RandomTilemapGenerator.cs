using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTilemapGenerator : MonoBehaviour
{
    [Space(10)]
    // Reference to the Tilemap component.
    public Tilemap tilemap;
    // Array of random tiles to choose from.
    public TileBase[] randomTiles;

    [Space(10)]
    // Width of the tilemap.
    public int width = 1000;
    // Height of the tilemap. 
    public int height = 1000;

    [Space(10)]
    // Frequency of random tile placement.
    public float randomTileFrequency = 0.02f;

    void Start()
    {
        GenerateTilemap();
    }

    void GenerateTilemap()
    {
        // Get the camera position in world space
        Vector3 camPos = Camera.main.transform.position;

        // Convert to integer grid coordinates
        int startX = Mathf.FloorToInt(camPos.x) - (width / 2);
        int startY = Mathf.FloorToInt(camPos.y) - (height / 2);

        for (int x = startX; x < startX + width; x++)
        {
            for (int y = startY; y < startY + height; y++)
            {
                bool occupiedNeighbor = CheckOccupiedNeighbors(x, y);

                if (!occupiedNeighbor && Random.value < randomTileFrequency)
                {
                    TileBase randomTile = randomTiles[Random.Range(0, randomTiles.Length)];
                    tilemap.SetTile(new Vector3Int(x, y, 0), randomTile);
                }
            }
        }
    }


    // Method to check if any neighboring tiles are occupied.
    bool CheckOccupiedNeighbors(int x, int y)
    {
        // Loop through the nine cells around the current cell (including diagonals).
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                Vector3Int neighbor = new Vector3Int(i, j, 0);

                // If the tile is occupied, return true.
                if (tilemap.GetTile(neighbor) != null)
                {
                    return true;
                }
            }
        }

        // If no neighboring tiles are occupied, return false.
        return false;
    }
}
