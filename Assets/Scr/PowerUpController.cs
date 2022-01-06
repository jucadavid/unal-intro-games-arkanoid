using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField]
    private ArkanoidController _arkanoidController;

    public List<int> ScoreTypes;
    private PowerUp powerUpPrefab;
    private Dictionary<int, PowerUp> _powerUpList;
    private int _powerUpCount;

    private void Start() {
        powerUpPrefab = Resources.Load<PowerUp>("Prefabs/PowerUp");
        Init();
    }

    public void Init() {
        _powerUpList = new Dictionary<int, PowerUp>();
        _powerUpCount = 0;
    }

    public void Spawn(Vector3 position) {
        PowerUp powerUp = Instantiate<PowerUp>(powerUpPrefab, transform);
        powerUp.transform.position = position;

        _powerUpCount += 1;
        powerUp.Create(_powerUpCount);
        _powerUpList.Add(_powerUpCount, powerUp);
    }

    public void Lose(int id) {
        Debug.Log("Lose: "+id);
        _powerUpList.Remove(id);
        Debug.Log(_powerUpList);
    }

    public void Won(int id) {
        PowerEffect(_powerUpList[id]);
        _powerUpList.Remove(id);
    }

    private void PowerEffect(PowerUp powerUp) {
        switch (powerUp.GetPowerUpType()) {
            case PowerUpType.MultiBall:
                _arkanoidController.MultiplyBalls();
                break;
            case PowerUpType.ScorePoints_50:
                _arkanoidController.ScorePoints(50);
                break;
            case PowerUpType.ScorePoints_100:
                _arkanoidController.ScorePoints(100);
                break;
            case PowerUpType.ScorePoints_250:
                _arkanoidController.ScorePoints(250);
                break;
            case PowerUpType.ScorePoints_500:
                _arkanoidController.ScorePoints(500);
                break;
            case PowerUpType.Speed_Slow:
                _arkanoidController.SpeedDown();
                break;
            case PowerUpType.Speed_Fast:
                _arkanoidController.SpeedUp();
                break;
            case PowerUpType.Size_Small:
                _arkanoidController.SmallerPaddle();
                break;
            case PowerUpType.Size_Big:
                _arkanoidController.BiggerPaddle();
                break;
        }
    }

    public void ClearPowerUps() {
        List<int> powerKeys = new List<int>(_powerUpList.Keys);
        foreach (int i in powerKeys) {
            _powerUpList[i].Hide();
            _powerUpList.Remove(i);
        }
    }
}
