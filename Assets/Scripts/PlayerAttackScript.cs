using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator animate;
    private PlayerMovementScript playerMovement ;
    private float cooldownTimer = Mathf.Infinity; // make it so that the timer doesnts start at zero 

    // new test

    // Start is called before the first frame update
    void Start()
    {
        animate = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovementScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()){
            Attack();
        }

        cooldownTimer += Time.deltaTime;
        
    }

    private void Attack(){
        animate.SetTrigger("attack");
        cooldownTimer = 0;

        // object pooling: create invisable objects that become visable upon attack
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<ProjectileScript>().SetDirection(Mathf.Sign(transform.localScale.x));


    }

    private int FindFireball(){
        // check all the fireballs to find one that is not active so we can use it to fire more than one fireball
        for(int i = 0; i < fireballs.Length; i++){
            if(!fireballs[i].activeInHierarchy){
                return i;
            }
        }

        return 0;
    }

}
