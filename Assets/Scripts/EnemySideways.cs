using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySideways : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private  float movementDistance;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    // Start is called before the first frame update
    void Start()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    // Update is called once per frame
    void Update()
    {   
    
        if(movingLeft){
            // moving left, flip the sprite to face left
            transform.localScale = new Vector3(-4, 4, 1);
            if(transform.position.x > leftEdge){
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime,transform.position.y, transform.position.z);

            }
            else{
                movingLeft = false;

            }
        }
        else{
            // moving right, flip sprite to face right
            transform.localScale = new Vector3(4, 4, 1);
            if(transform.position.x < rightEdge){
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime,transform.position.y, transform.position.z);
            }
            else{
                movingLeft = true;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player"){
            collision.GetComponent<KingHealth>().TakeDamage(damage);
        }
        
    }
}
