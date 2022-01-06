using UnityEngine;
public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private PowerUpController _powerUpController;

    private void OnCollisionEnter2D(Collision2D other)
    {
       if (other.transform.TryGetComponent<Ball>(out Ball ball))
       {
           ArkanoidEvent.OnBallReachDeadZoneEvent?.Invoke(ball);
       }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        PowerUp powerUpHit;
        if (!collider.TryGetComponent(out powerUpHit)) {
            return;
        }
        powerUpHit.Hide();
        _powerUpController.Lose(powerUpHit.GetId());
    }
}
