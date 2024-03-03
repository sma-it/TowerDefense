using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public Path path { get; set; }
    public GameObject target { get; set; }

    public int ID { get; set; }

    public float speed = 1f;
    public float health = 10f;
    public int points = 1;

    private int pathIndex = 1;

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);

        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            target = UFOSpawner.Get.RequestTarget(path, pathIndex);
            pathIndex++;

            if (target == null)
            {
                GameManager.Get.AttackGate();
                GameManager.Get.RemoveInGameUFO();
                Destroy(gameObject);
            }
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.Get.AddCredits(points);
            GameManager.Get.RemoveInGameUFO();
            Destroy(gameObject);       
        }
    }
}
