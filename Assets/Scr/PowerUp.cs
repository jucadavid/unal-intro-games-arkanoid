using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType {
    MultiBall,
    ScorePoints_50,
    ScorePoints_100,
    ScorePoints_250,
    ScorePoints_500,
    Speed_Slow,
    Speed_Fast,
    Size_Small,
    Size_Big
}

public class PowerUp : MonoBehaviour
{
    private Collider2D _collider;
    private SpriteRenderer _renderer;

    private int _id;
    private PowerUpType _powerUpType;

    private const string POWERUP_SPRITE_PATH = "Sprites/PowerUps/";

    private void Start() {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = true;

        _renderer = GetComponentInChildren<SpriteRenderer>();

        int power = Random.Range(0,9);
        _powerUpType = (PowerUpType)power;

        string path = GetPowerUpSpritePath();
        _renderer.sprite = Resources.Load<Sprite>(path);
    }

    public void Create(int id) {
        _id = id;
    }

    private void Update() {
        Vector3 pos = transform.position;
        pos.y -= 1 * Time.deltaTime;
        transform.position = pos;
    }

    public void Hide() {
        _collider.enabled = false;
        Destroy(gameObject);
    }

    public int GetId() {
        return _id;
    }

    private string GetPowerUpSpritePath() {
        switch (_powerUpType) {
            case PowerUpType.MultiBall:
                return (POWERUP_SPRITE_PATH + "MultiBall/powerUp_MultiBall");
            case PowerUpType.ScorePoints_50:
                return (POWERUP_SPRITE_PATH + "ScorePoints/powerUp_50");
            case PowerUpType.ScorePoints_100:
                return (POWERUP_SPRITE_PATH + "ScorePoints/powerUp_100");
            case PowerUpType.ScorePoints_250:
                return (POWERUP_SPRITE_PATH + "ScorePoints/powerUp_250");
            case PowerUpType.ScorePoints_500:
                return (POWERUP_SPRITE_PATH + "ScorePoints/powerUp_500");
            case PowerUpType.Speed_Slow:
                return (POWERUP_SPRITE_PATH + "Speed/powerUp_Slow");
            case PowerUpType.Speed_Fast:
                return (POWERUP_SPRITE_PATH + "Speed/powerUp_Fast");
            case PowerUpType.Size_Small:
                return (POWERUP_SPRITE_PATH + "Size/powerUp_Small");
            case PowerUpType.Size_Big:
                return (POWERUP_SPRITE_PATH + "Size/powerUp_Big");
        }
        return string.Empty;
    }

    public PowerUpType GetPowerUpType() {
        return _powerUpType;
    }
}
