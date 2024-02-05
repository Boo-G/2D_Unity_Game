using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingHealth : MonoBehaviour
{

    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set;} // can get from any script but can only set value privately
    private Animator animate;
    private bool dead;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        animate = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            TakeDamage(1);
        }
        
    }

    public void TakeDamage(float _damage){
        // subtract health by damage with a minimum return of 0 and max of starting health
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0){
            // player hurt
            animate.SetTrigger("hurt");
            // add iframes
        }
        else{
            if (!dead){
                // player dead
                animate.SetTrigger("dead");
                GetComponent<KingMovementScript>().enabled = false;
                dead = true;
            }
        }
    }
}
