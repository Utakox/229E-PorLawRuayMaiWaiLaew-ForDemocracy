using UnityEngine;

public class AirResistanceZone : MonoBehaviour
{
    public float zoneDragCoefficient = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BallotController bc = other.GetComponent<BallotController>();
            if (bc != null) bc.EnterZone(zoneDragCoefficient);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BallotController bc = other.GetComponent<BallotController>();
            if (bc != null) bc.ExitZone();
        }
    }
}