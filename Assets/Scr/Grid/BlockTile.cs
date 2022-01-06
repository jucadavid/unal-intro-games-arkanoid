using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Small,
    Big
}

public enum BlockColor {
    Green,
    Blue,
    Orange,
    Red,
    Purple
}

public class BlockTile : MonoBehaviour
{
    private const string BLOCK_BIG_PATH = "Sprites/BlockTiles/Big/Big_{0}_{1}";

    [SerializeField]
    private BlockType _type = BlockType.Big;
    [SerializeField]
    private BlockColor _color = BlockColor.Blue;
    [SerializeField]
    private int _score = 10;

    public int Score => _score;

    private SpriteRenderer _renderer;
    private Collider2D _collider;

    private int _totalHits;
    private int _currentHits;
    private int _id;

    public void Init() {
        _currentHits = 0;
        _totalHits = _type == BlockType.Big ? 2 : 1;

        _collider = GetComponent<Collider2D>();
        _collider.enabled = true;

        _renderer = GetComponentInChildren<SpriteRenderer>();

        _renderer.sprite = GetBlockSprite(_type, _color, 0);
    }

    public void OnHitCollision(ContactPoint2D contactPoint)
    {
        _currentHits++;
        if (_currentHits >= _totalHits)
        {
            _collider.enabled = false;
            gameObject.SetActive(false);
            ArkanoidEvent.OnBlockDestroyedEvent?.Invoke(_id);
        }
        _renderer.sprite = GetBlockSprite(_type, _color, _currentHits);
    }

    static Sprite GetBlockSprite(BlockType type, BlockColor color, int state) {
        string path = string.Empty;
        if (type == BlockType.Big)
        {
            path = string.Format(BLOCK_BIG_PATH, color, state);
        }
        else {
            path = string.Format(BLOCK_BIG_PATH, color, 0);
        }

        if (string.IsNullOrEmpty(path))
        {
            return null;
        }

        return Resources.Load<Sprite>(path);
    }

    public void SetData(int id, BlockColor color)
    {
        _id = id;
        _color = color;
    }
}
