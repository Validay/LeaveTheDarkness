using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        _direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        _direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (_direction.magnitude > 1)
            _direction = _direction.normalized;

        transform.position += _direction * speed * Time.deltaTime;
    }
}
