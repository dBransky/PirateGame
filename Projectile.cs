using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    [SerializeField]
    private LifeManager _lifeManager;
    [SerializeField]
    private bool _shotFromPlayerOne;
   
    // Start is called before the first frame update
    void Start()
    {
        _lifeManager= GameObject.Find("LifeManager").GetComponent<LifeManager>();
        if (transform.parent != null)
        {
            if (transform.parent.name == "Pivot Anchor")
                _shotFromPlayerOne = true;
            else
                _shotFromPlayerOne = false;
            transform.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 10f||transform.position.y<=0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag != null)
        {
            if (collision.tag == "Player" && !_shotFromPlayerOne)
            { _lifeManager.Hit(1);
                Destroy(this.gameObject);

            }
            if (collision.tag == "PlayerTwo" && _shotFromPlayerOne)
            { _lifeManager.Hit(2);
                Destroy(this.gameObject);

            }
        }
     
    }
}
