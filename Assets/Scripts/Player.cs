using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Projectile laserPrefab;
    public float speed = 5.0f;
    public bool _laserActive;
    public AudioClip shootingSound;
    private AudioSource audioSource;

    private void Start(){
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = shootingSound;
    }
    // moving the player
    private void Update(){
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            Shoot();
        }
    }

    // shoot when no projectile exists
    private void Shoot(){
        if(!_laserActive){
            audioSource.Play();
            Debug.Log("Played");
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
            
        }
    }

    private void LaserDestroyed(){
        _laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Invader")|| other.gameObject.layer == LayerMask.NameToLayer("Missile")){
            SceneManager.LoadScene("CreditScene");
        }
    }

}
