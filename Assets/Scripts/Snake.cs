using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    Vector2 _direction = Vector2.right;
    List<Transform> _segments = new List<Transform>();
    
    [SerializeField] Transform _segmentsPiece;

    // Start is called before the first frame update
    void Start()
    {
        _segments.Add(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }
    }

    void FixedUpdate()
    {
        //Spawns segments in reverse
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
            );
    }

    void Grow()
    {
        Transform segment = Instantiate(this._segmentsPiece);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == References.food)
        {
            Grow();
        }
    }
}
