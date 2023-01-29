using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float radius = 3f;
    [SerializeField]
    private AudioClip burningSFX, explodeSFX;
    [SerializeField] private Vector2 explosionForce = new Vector2(200f,100f);
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void ExplodeBomb()
    {
       Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));
       AudioSource.PlayClipAtPoint(explodeSFX, Camera.main.transform.position);

        if (playerCollider)
       {
           playerCollider.GetComponent<Rigidbody2D>().AddForce(explosionForce);
           playerCollider.GetComponent<Player>().PlayerHit();
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator.SetTrigger("Burn");
        AudioSource.PlayClipAtPoint(burningSFX, Camera.main.transform.position);

    }

    private void DestroyBomb()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
