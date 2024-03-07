using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;
    public int pointValue;
    public GameController gameController;

    public System.Action killed;

    private void Awake(){
        _spriteRenderer= GetComponent<SpriteRenderer>();
        gameController = FindObjectOfType<GameController>();
    }

    private void Start(){
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    // animation
    private void AnimateSprite(){
        _animationFrame++;

        if(_animationFrame >= this.animationSprites.Length){
            _animationFrame = 0;
        }

        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }

    // when hit by laser
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")){
            this.killed.Invoke();
            gameController.UpdateScore(pointValue);
            this.gameObject.SetActive(false);
        }
    }
}
