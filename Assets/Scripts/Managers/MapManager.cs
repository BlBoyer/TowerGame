using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
#nullable enable

public class MapManager : MonoBehaviour
{
    //need to have a tile set to use for generation
    //I want this to be a scene loads that generates the map, so it looks like your going into a large area that you cannot see the edges of the same for exiting
    //we the distance of left/right/top/botom, and have those boundaries set for proper exits into other scences. So we manage the scene changer without doors
    //also set the position of any entering portal in the game data file, and use this for instantiation upon loading/generation of the area, if there is a border, generate that by the entrance

    public Tilemap tilemap;
    public Tile baseTile;
    public Vector3? startPosition = null;
    private Vector3 startingPosition;
    /// <summary>
    /// Position boundary trigger points for exiting the scene.
    /// </summary>
    public int generationFieldSize = 5;
    public Transform playerTransform;
    /// <summary>
    /// This position is the trigger point for exiting to previous scene.
    /// </summary>
    public Boundaries Boundaries;

    void Start()
    {
        tilemap.ClearAllTiles();

        if (startPosition is null)
        {
            startingPosition = playerTransform.position;
        }
    }

    async void Update()
    {
        await GenerateBaseArea();
    }

    private async Task GenerateBaseArea()
    {
        if (!playerTransform.position.IsWithinBounds(this.Boundaries))
            return;

        var playerCell = tilemap.WorldToCell(playerTransform.position);
        for (int i = 1; i <= generationFieldSize; i++)
        {
            //back out if tile exist, ToDo: remove cells leaving field size. We need bounding size to know when to not generate
            //so when we begin generating, we need to store a world position so that we can enter and leave generating scene at the correct time(this may be variable of course).
            var surroundingTiles = await GetSurroundingTileCellsByReference(playerCell, i);
            await FillTileArray(surroundingTiles, true, baseTile);
            Debug.Log("Tiles should be painted");
        }
    }

    /// <summary>
    /// Gets the sqaure outline of Tilemap cell positions up to distance of the offset, relative to specified reference position of the specified tile.
    /// </summary>
    /// <param name="referencePosition"></param>
    /// <param name="offset">The magnitude of vector offset, relative to the reference position.</param>
    /// <returns>Vector3Int[]</returns>
    public async Task<Vector3Int[]> GetSurroundingTileCellsByReference(Vector3Int referencePosition, int offset)
    {
        //ToDo:: we should not repeat for multiple cells because the corners are reused for each line
        //for i = 1 there should be 3 cells (add the reference cell [1], plus the distance x2 [either side of the reference])
        var lineLength = offset * 2 + 1;
        Vector3Int[] cellPositionValues = new Vector3Int[lineLength * 4];
        Vector3Int[] xValues = new Vector3Int[lineLength];
        Vector3Int[] yValues = new Vector3Int[lineLength];
        Vector3Int[] xNegValues = new Vector3Int[lineLength];
        Vector3Int[] yNegValues = new Vector3Int[lineLength];

        for (int i = 0; i < lineLength; i++)
        {
            var indexOfOffset = i - offset;
            xValues[i] = new Vector3Int(referencePosition.x + indexOfOffset, referencePosition.y + offset);
            xNegValues[i] = new Vector3Int(referencePosition.x + indexOfOffset, referencePosition.y - offset);
            yValues[i] = new Vector3Int(referencePosition.x + offset, referencePosition.y + indexOfOffset);
            yNegValues[i] = new Vector3Int(referencePosition.x - offset, referencePosition.y + indexOfOffset);
        }

        xValues.CopyTo(cellPositionValues, 0);
        yValues.CopyTo(cellPositionValues, lineLength);
        xNegValues.CopyTo(cellPositionValues, lineLength * 2);
        yNegValues.CopyTo(cellPositionValues, lineLength * 3);

        return cellPositionValues;
    }

    /// <summary>
    /// Fills all Tilemap cells in supplied array with the specified tile.
    /// </summary>
    /// <param name="cellPositions">The array of cell positions to fill.</param>
    /// <param name="fillWhenEmptyOnly">Use when only filling cells that are empty.</param>
    /// <param name="fillTile">The tile to use when filling cells.</param>
    /// <returns>Task</returns>
    public async Task FillTileArray(Vector3Int[] cellPositions, bool fillWhenEmptyOnly, Tile? fillTile)
    {
        var cellsToFill = cellPositions;

        if (fillWhenEmptyOnly)
            cellsToFill = cellPositions.Where(c => !tilemap.HasTile(c)).ToArray();

        foreach (var cell in cellsToFill)
        {
            tilemap.SetTile(cell, fillTile ?? baseTile);
        }
    }

    /// <summary>
    /// Fills all Tilemap cells in supplied array.
    /// </summary>
    /// <param name="cellPositions">The array of cell positions to fill.</param>
    /// <param name="fillTile">The tile to use when filling cells.</param>
    /// <returns>Task</returns>
    public async Task FillTileArray(Vector3Int[] cellPositions, Tile? fillTile)
    {
        foreach (var cell in cellPositions)
        {
            tilemap.SetTile(cell, fillTile ?? baseTile);
        }
    }

    public async Task ClearExoTiles()
    {
        throw new NotImplementedException();
    }
}
