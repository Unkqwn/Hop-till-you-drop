using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0f;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            health -= 1f;
            Destroy(other);
        }
    }
}
