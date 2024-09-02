using System.Collections;
using UnityEngine;

public class ThornyVines : MonoBehaviour
{
    public float maxDurability = 10f;  // Maximum durability of the trap
    public float durability = 10f;     // Current durability
    public float rechargeTime = 5f;    // Time to recharge before it can be used again
    public float slowEffect = 0.5f;    // How much to slow down the enemy (e.g., 0.5 = 50% slower)

    public Sprite activeVinesSprite;   // Sprite for the active thorny vines
    public Sprite inactiveVinesSprite; // Sprite for the inactive thorny vines

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private bool isActive = true;      // Track if the trap is active

    private void Start()
    {
        // Get the SpriteRenderer component attached to the same GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite to the active vines sprite
        spriteRenderer.sprite = activeVinesSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object is an enemy
        if (collision.CompareTag("Enemy") && isActive)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Enemy");
                StartCoroutine(SlowDownEnemy(enemy));
            }
        }
    }

    private IEnumerator SlowDownEnemy(Enemy enemy)
    {
        // Save the enemy's original speed
        float originalSpeed = enemy.GetSpeed();
        // Reduce the enemy's speed
        enemy.SetSpeed(originalSpeed * slowEffect);

        // Change the sprite to indicate the trap is deactivated
        spriteRenderer.sprite = inactiveVinesSprite;

        // While the trap is active and has durability left
        while (durability > 0)
        {
            durability -= Time.deltaTime; // Reduce durability over time
            yield return null;            // Wait for the next frame
        }

        // Restore the enemy's speed when done
        enemy.SetSpeed(originalSpeed);

        // If durability runs out, recharge the trap
        StartCoroutine(RechargeTrap());
    }

    private IEnumerator RechargeTrap()
    {
        isActive = false;                   // Set trap as inactive
        yield return new WaitForSeconds(rechargeTime); // Wait for recharge time
        durability = maxDurability;         // Reset durability
        isActive = true;                    // Set trap as active again

        // Change the sprite back to the active vines sprite
        spriteRenderer.sprite = activeVinesSprite;
    }
}
