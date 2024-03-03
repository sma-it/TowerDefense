using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 5f; // Range within which the tower can detect and attack enemies
    public float attackRate = 1f; // How often the tower attacks (attacks per second)
    public int attackDamage = 3;
    public float attackSize = 1f;

    private float lastAttackTime = 0; // The last time the tower attacked
    public GameObject bulletPrefab; // The bullet prefab the tower will shoot
    public TowerType type;

    // Update is called once per frame
    void Update()
    {
        // Check if enough time has passed since the last attack
        if (Time.time - lastAttackTime >= 1f / attackRate)
        {
            // Scan for enemies
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange);
            Collider2D target = null;
            int lowestID = int.MaxValue;

            foreach (var hitCollider in hitEnemies)
            {
                if (hitCollider.CompareTag("Enemy")) // Make sure to tag your enemy GameObjects with "Enemy" in the editor
                {
                    var script = hitCollider.gameObject.GetComponent<UFO>();
                    if (target == null || script.ID < lowestID)
                    {
                        target = hitCollider;
                        lowestID = script.ID;
                    }
                }
            }

            if (target != null)
            {
                Shoot(target.transform);
                lastAttackTime = Time.time;
            }
        }
    }

    void Shoot(Transform enemy)
    {
        // Instantiate projectile and set it to shoot towards the enemy
        GameObject projectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        projectile.transform.localScale = new Vector3(attackSize, attackSize, attackSize);
        Projectile script = projectile.GetComponent<Projectile>(); // Assuming your bullet has a script named "Bullet" for moving towards the target
        if (script != null)
        {
            script.damage = attackDamage;
            script.SetTarget(enemy);
        }
        SoundManager.Get.PlayTowerSound(type);
    }

    // Draw the attack range in the editor for easier debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
