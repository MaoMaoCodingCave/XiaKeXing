using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapChanger : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase newTile;
    public Transform player;
    public ToolBarController toolBarController;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        EventHandler.OnDigTile += DigTile;
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        EventHandler.OnDigTile -= DigTile;
    }

    private void DigTile(Vector3 mousepos)
    {
        Vector3 mouseWorldPos = mousepos;
        Vector3Int coordinate = tilemap.WorldToCell(mousepos);

        TileBase tile = tilemap.GetTile(coordinate);
        if (tile != null)
        {
            if (Vector3.Distance(player.position, coordinate) < 2f && toolBarController.ableToDig)
            {
                // Debug.Log("Too close to player");
                tilemap.SetTile(coordinate, newTile);
            }
            // Debug.Log("Tile at " + coordinate + " is " + tile.name);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
