using UnityEngine;
using System.Collections.Generic;

public class GridController : MonoBehaviour
{
    [SerializeField] 
    private Vector2 _offset = new Vector2(-5.45f, 4);

    private LevelData _currentLevelData;
    private Dictionary<int, BlockTile> _blockTiles = new Dictionary<int, BlockTile>();

    public void BuildGrid(LevelData levelData) {
        _currentLevelData = levelData;
        clearGrid();
        buildGrid();
    }

    private void clearGrid() {
        int totalChildren = transform.childCount;
        for (int i = 0; i < totalChildren; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
        _blockTiles.Clear();
    }

    private void buildGrid()
    {
        int id = 0;

        int rowCount = _currentLevelData.RowCount;
        float verticalSpacing = _currentLevelData.rowSpacing;

        for (int j = 0; j < rowCount; j++)
        {
            GridRowData rowData = _currentLevelData.Rows[j];
            
            int blockCount = rowData.BlockAmount;
            float horizontalSpacing = rowData.blockSpacing;

            Vector2 blockSize = GetBlockSize(rowData.BlockType);
            BlockTile blockTilePerfab = Resources.Load<BlockTile>(GetBlockPath(rowData.BlockType));
            BlockColor blockColor = rowData.BlockColor;

            if (blockTilePerfab == null)
            {
                return;
            }
            
            for (int i = 0; i < blockCount; i++)
            {
                BlockTile blockTile = Instantiate<BlockTile>(blockTilePerfab, transform);
                float x = _offset.x + blockSize.x/2 + (blockSize.x + horizontalSpacing) * i;
                float y = _offset.y - (blockSize.y + verticalSpacing) * j;
                blockTile.transform.position = new Vector3(x, y, 0);
                
                blockTile.SetData(id, blockColor);
                blockTile.Init();

                _blockTiles.Add(id, blockTile);
                id++;
            }
        }
    }

    private Vector2 GetBlockSize(BlockType type)
    {
        if (type == BlockType.Big)
        {
            return new Vector2(1.5f, 0.5f);
        }
        else if (type == BlockType.Small) {
            return new Vector2(1.5f, 0.5f);
        }
        Debug.Log("Block type not found ( GetBlockSize )");
        return Vector2.zero;
    }

    private string GetBlockPath(BlockType type)
    {
        if (type == BlockType.Big)
        {
            return "Prefabs/BigBlockTile";
        }
        else if (type == BlockType.Small) {
            return "Prefabs/SmallBlockTile";
        }
        Debug.Log("Block type not found ( GetBlockPath )");
        return string.Empty;
    }

    public int GetBlocksActive()
    {
        int totalActiveBlocks = 0;
        foreach (BlockTile block in _blockTiles.Values)
        {
            if (block.gameObject.activeSelf)
            {
                totalActiveBlocks++;
            }
        }

        return totalActiveBlocks;
    }
    public BlockTile GetBlockBy(int id) {
        if (_blockTiles.TryGetValue(id, out BlockTile block)) {
            return block;
        }
        return null;
    }
}
