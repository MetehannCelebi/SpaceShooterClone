 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using Random = UnityEngine.Random;

 public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;

    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * (Time.deltaTime * _speed));
        if (transform.position.y <-5f)
        {
            float  randomX= Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX,7 , 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            
            Destroy(gameObject);
        }


        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            
            if (_player != null)
            {
                _player.AddScore(10);    
            }
            Destroy(gameObject);
        }
    }
}
