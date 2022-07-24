using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] BoxCollider2D _gridArea;

    [SerializeField] SpriteRenderer _spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        RandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomSpawn()
    {
        Bounds gridBounds = _gridArea.bounds;

        float x = Random.Range(gridBounds.min.x, gridBounds.max.x);
        float y = Random.Range(gridBounds.min.y, gridBounds.max.y);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == References.player)
        {
            RandomSpawn();
        }
    }
}
