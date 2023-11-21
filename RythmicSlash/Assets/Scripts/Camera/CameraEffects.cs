using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public static CameraEffects Instance;

    [SerializeField] private float duration, intensity, timer;
    [SerializeField] private bool isShaking;

    [SerializeField] private Vector3 initialPosition;

    [SerializeField] private Transform target;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Update()
    { 
        FollowTarget();

        if (!isShaking) return;

        timer += Time.deltaTime;

        this.transform.position = initialPosition + (Random.insideUnitSphere * intensity);

        if (timer > duration)
        {
            isShaking = false;
            timer = 0;
        }
    }

    public void FollowTarget()
    {
        if (!target) return;

        Vector3 targetPosition = Vector3.zero;
        targetPosition = Vector3.Lerp(this.transform.position, target.position, 1 * Time.deltaTime);
        targetPosition.z = -10;

        this.transform.position = targetPosition;
    }

    public void ShakeCamera(float duration, float intensity)
    {
        this.duration = duration;
        this.intensity = intensity;

        initialPosition = this.transform.position;
        isShaking = true;
    }
}
