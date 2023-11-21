using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D projectile;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Queue<GameObject> pool;
    [SerializeField] private GameObject parent;

    [SerializeField] private float lifeTime = 5, speedX = 3, speedY = 2;
    [SerializeField] private int damage = 2;

    private void Start()
    {
        projectile = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (parent.transform.localScale.x == 1)
            this.projectile.AddForce(new Vector2(speedX, speedY), ForceMode2D.Impulse);
        else
            this.projectile.AddForce(new Vector2(-speedX, speedY), ForceMode2D.Impulse);

        Invoke("ReturnToQueue", lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") | collision.gameObject.CompareTag("Projectile")) return;

        ParticleSystem particleGO = Instantiate(particle);
        particleGO.transform.position = this.transform.position;

        CameraEffects.Instance.ShakeCamera(0.15f, 0.07f);

        Destroy(particleGO.gameObject, 1);
        ReturnToQueue();
    }

    public void SetPoolReference(Queue<GameObject> pool)
    {
        this.pool = pool;
    }

    public void SetParentReference(GameObject parent)
    {
        this.parent = parent;
    }

    public void SetProjectileReference(Rigidbody2D projectile)
    {
        this.projectile = projectile;
    }

    private void ReturnToQueue()
    {
        pool.Enqueue(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
