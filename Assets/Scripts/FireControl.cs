using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    [SerializeField] AudioSource FireSource;
    [SerializeField] ParticleSystem FireParticleSystem;
    [SerializeField] ParticleSystem SmokeParticleSystem;
    [SerializeField] float FireStartingTime = 2.0f;
    [SerializeField] float FireLife = 100f;
    [SerializeField] float MaxFireLife = 100f;
    [SerializeField] float RecoveryRate = 40f;
    [SerializeField] float ExtinguishRate = 20f;
    protected bool isExtinguish;
    protected Collider BoxCollider;
    public static int FireNum = 20;

    int ParticleCollisionCount;
    [SerializeField] float UpdateTime = 1.0f;
    [SerializeField] float ToExtinguishRateRatio = 0.1f;

    void Start()
    {
        BoxCollider = GetComponent<Collider>();
        BoxCollider.isTrigger = false;
        isExtinguish = true;
        FireParticleSystem.Stop();
        SmokeParticleSystem.Stop();
        StartCoroutine(StartingFire());
        StartCoroutine(Reset());
        FireSource.loop = true;
    }

    IEnumerator StartingFire()
    {
        FireParticleSystem.time = 0;
        FireParticleSystem.Play();
        SmokeParticleSystem.Play();

        FireSource.Play();
        float elapsedTime = 0.0f;
        while (elapsedTime < FireStartingTime)
        {
            float ratio = Mathf.Min(1.0f, (elapsedTime / FireStartingTime));
            FireParticleSystem.transform.localScale = Vector3.one * ratio;
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        //FireParticleSystem.transform.localScale = Vector3.one;
        isExtinguish = false;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(UpdateTime);
        ParticleCollisionCount = 0;
        ExtinguishRate = 0;
        StartCoroutine(Reset());
    }

    IEnumerator Extinguish()
    {
        FireParticleSystem.Stop();
        FireSource.Stop();
        BoxCollider.isTrigger = true;
        yield return new WaitForSeconds(FireStartingTime);
        SmokeParticleSystem.Stop();
        FireNum--;
        //FireParticleSystem.transform.localScale = Vector3.one;
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(1.0f);
    }

    void Update()
    {
        if (!isExtinguish && (FireLife < MaxFireLife || ExtinguishRate != 0f))
        {
            FireLife += (RecoveryRate - ExtinguishRate) * Time.deltaTime;

            if (FireLife <= 0)
            {
                //StartCoroutine(Load());
                FireLife = 0f;
                isExtinguish = true;
                StartCoroutine(Extinguish());
            }

            if (FireLife >= MaxFireLife)
            {
                FireLife = MaxFireLife;
            }

            StartCoroutine(Load());
            //FireParticleSystem.transform.localScale = Vector3.one * FireLife / MaxFireLife;
            FireParticleSystem.maxParticles = Mathf.RoundToInt(FireLife);
        }
    }

    public void HitByExtinguishParticleCollision(int points)
    {
        ParticleCollisionCount += points;
        ExtinguishRate = ParticleCollisionCount * ToExtinguishRateRatio * 1f;
    }
}