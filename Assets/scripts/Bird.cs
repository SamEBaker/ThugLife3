using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 5;
    Vector2 _startPosition;
    Rigidbody2D _rb;
    SpriteRenderer _sr;
    public bool IsDragging { get; private set; }
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        _startPosition = _rb.position;
        _rb.isKinematic = true;
       
    }

    void OnMouseDown()
    {
        _sr.color = Color.red;
        IsDragging = true;
    }
  
    void OnMouseUp()
    {
        Vector2 currentPosition = GetComponent<Rigidbody2D>().position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        _rb.isKinematic = false;
        
        _rb.AddForce(direction * _launchForce);

        var audios = GetComponent<AudioSource>();
        audios.Play();

        _sr.color = Color.white;
        IsDragging = false;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 desiredPosition = mousePosition;
        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }
       
        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;
        
        _rb.position = desiredPosition;
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }
    
    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rb.position = _startPosition;
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
    }
}

