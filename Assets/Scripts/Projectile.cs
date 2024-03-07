using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;
    private GameController gameController;

    void Start(){
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    private void Update(){
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    //  when projectile hits anything, destroy it
    private void OnTriggerEnter2D(Collider2D other){
    
        if(this.destroyed != null){
            this.destroyed.Invoke();
        }
        if (other.CompareTag("Barrier'"))
        {
            // Destroy the barrier
            Destroy(other.gameObject);
        }
        Destroy(this.gameObject);
    }
}
