using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _screenLimit = 7;

    [SerializeField]
    private PowerUpController _powerUpController;

    private Vector3 _targetPosition;


    private Camera _cam;
    private Camera Camera {
        get {
            if (_cam == null) {
                _cam = Camera.main;
            }
            return _cam;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _targetPosition.x = Camera.ScreenToWorldPoint(Input.mousePosition).x;
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, -_screenLimit, _screenLimit);
        _targetPosition.y = this.transform.position.y;

        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime* _speed);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        PowerUp powerUpHit;
        if (!collider.TryGetComponent(out powerUpHit)) {
            return;
        }
        powerUpHit.Hide();
        _powerUpController.Won(powerUpHit.GetId());
    }
}
