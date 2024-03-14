using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public int rows = 5;
    public int columns = 11;
    public AnimationCurve speed;
    private Vector3 _direction = Vector2.right;
    public Projectile missilePrefab;
    public float missileAttackRate = 1.0f;
    private Invader type;
    private GameController gameController;
    public AudioClip shootingSound;
    private AudioSource audioSource;

    // for calculating speed
    public int amountKilled {get; private set; }
    public int amountAlive => this.totalInvaders - this.amountKilled;
    public int totalInvaders => this.rows * this.columns;
    public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders;

    private void Awake(){
        ResetGame();
    }

    private void Start(){
        type = FindObjectOfType<Invader>();
        gameController = FindObjectOfType<GameController>();
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = shootingSound;
    }

    // for moving invaders forward
    private void Update(){
        this.transform.position  += _direction * this.speed.Evaluate(this.percentKilled) *Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach(Transform invader in this.transform){
            if(!invader.gameObject.activeInHierarchy){
                continue;
            }

            // if an invader hits the right or left side
            if(_direction == Vector3.right && invader.position.x >= rightEdge.x - 1.0f){
                AdvanceRow();
            }
            else if(_direction == Vector3.left && invader.position.x <= leftEdge.x + 1.0f){
                AdvanceRow();
            }
        }
    }

    
    // move down the grid
    private void AdvanceRow(){
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    // random value for invader missiles
    private void MissileAttack(){
    
        foreach(Transform invader in this.transform){
        if(!invader.gameObject.activeInHierarchy){
            continue;
        }

        if(Random.value < (1.0f / (float)this.amountAlive)){
            audioSource.Play();
            Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
            break;
        }
        }
    
    }
    
    public void InvaderKilled(){
        this.amountKilled++;
    }

    public void ResetGame()
    {
        // Reset game state
        amountKilled = 0;
        ResetInvadersPosition();
    }

    private void ResetInvadersPosition()
    {

        // Reset position of all invader objects
        for (int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector3 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);
            for (int col = 0; col < this.columns; col++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }

}
