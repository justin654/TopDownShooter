// Enemy.cs
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float stoppingDistance = 2f;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // Assuming the player has a tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector2.MoveTowards(transform.position, player.position, step);
        }

        // Here, you could put other behavior logic such as attacking when close enough.
    }
}
