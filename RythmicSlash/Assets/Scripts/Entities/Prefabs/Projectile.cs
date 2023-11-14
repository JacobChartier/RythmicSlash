using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D projectile;
    [SerializeField] private ParticleSystem particle;

    [SerializeField] private float lifeTime = 5, speedX, speedY;

    [SerializeField] private int damage = 2;

    private void Start()
    {
        projectile = GetComponent<Rigidbody2D>();

        if (projectile.rotation == 0)
            projectile.AddForce(new Vector2(speedX, speedY), ForceMode2D.Impulse);
        else
            projectile.AddForce(new Vector2(-speedX, speedY), ForceMode2D.Impulse);

        Destroy(this.gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;

        ParticleSystem particleGO = Instantiate(particle);
        particleGO.transform.position = this.transform.position;

        CameraEffects.Instance.ShakeCamera(0.15f, 0.07f);

        Destroy(particle, 1);
        Destroy(this.gameObject);
    }
}
