using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    public float speed = 20f;
    public int damage = 50;

    public void Start()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy bullet if the target is no longer valid
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Check for collision with the target
        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        // Here you would typically call a method on the enemy to deal damage
        // For example: target.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(gameObject); // Destroy the bullet after hitting the target
        target.GetComponent<UFO>().Damage(damage);
    }
}
