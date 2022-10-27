using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem _particleSystem;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        FireControl firehit = null;
        int hitCount = 0;
        int numCollisionEvents = _particleSystem.GetCollisionEvents(other, CollisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            var col = CollisionEvents[i].colliderComponent;
            var fire = col.GetComponent<FireControl>();
            if (fire != null)
            {
                hitCount++;
                firehit = fire;
            }

            if (firehit != null)
            {
                firehit.HitByExtinguishParticleCollision(hitCount);
            }
        }
    }
}