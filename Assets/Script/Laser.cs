using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _laserspeed = 8;
    private bool _isparentNotNull;

    private void Start()
    {
        _isparentNotNull = transform.parent != null;
    }

    void Update()
    {
        transform.Translate(Vector3.up * (Time.deltaTime * _laserspeed));

        if (transform.position.y > 8f)
        {
            if (_isparentNotNull)
            {
             Destroy(transform.parent.gameObject);   
            }
            Destroy(gameObject);
        }
    }
}
