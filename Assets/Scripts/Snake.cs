using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    Vector2 _direction = Vector2.right;

    List<Transform> _segments = new List<Transform>();
    
    [SerializeField] Transform _segmentsPiece;
    [SerializeField] int _initialSize = 3;

    Vector3 Rdirection = new Vector3(0,0,0);
    Vector3 Ldirection = new Vector3(0, 0, 180);
    Vector3 Udirection = new Vector3(0, 0, 90);
    Vector3 Ddirection = new Vector3(0, 0, -90);

    [SerializeField] bool goingUp;
    [SerializeField] bool goingDown;
    [SerializeField] bool goingLeft;
    [SerializeField] bool goingRight;



    


    // Start is called before the first frame update
    void Start()
    {
  
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            goingRight = false;
            goingLeft = false;

            if(!goingDown)
            {
                goingUp = true;
                _direction = Vector2.up;
                this.transform.rotation = Quaternion.Euler(Udirection);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            goingRight = false;
            goingLeft = false;

            if (!goingUp)
            {
                goingDown = true;
                _direction = Vector2.down;
                this.transform.rotation = Quaternion.Euler(Ddirection);

            }


        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            goingUp = false;
            goingDown = false;

            if (!goingRight)
            {
                goingLeft = true;
                _direction = Vector2.left;
                this.transform.rotation = Quaternion.Euler(Ldirection);
            }
            
            
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            goingUp = false;
            goingDown = false;

            if (!goingLeft)
            {
                goingRight = true;
                _direction = Vector2.right;
                this.transform.rotation = Quaternion.Euler(Rdirection);
            }
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

    private void ResetGame()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        //for (int i = 1; i < this._initialSize; i++)
        //{
        //    _segments.Add(Instantiate(this._segmentsPiece));
        //}

        _direction = Vector2.right;
        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.Euler(Rdirection);
        goingRight = true;
        goingLeft = false;
        goingDown = false;
        goingUp = false;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == References.food)
        {
            Grow();
        }

        if (other.tag != References.food)
        {
            ResetGame();
        }
    }

    
}
