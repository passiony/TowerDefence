using System;
using UnityEngine;

namespace SimpleFrame.Tool.Level
{
    public class PlayerHome : DamageableBehaviour
    {
        public ParticleSystem attackPfx;
        public AudioSource attackSound;

        protected virtual void Start()
        {
            configuration.damaged += OnDamaged;
        }

        protected virtual void OnDestroy()
        {
            configuration.damaged -= OnDamaged;
        }

        protected virtual void OnDamaged(HealthChangeInfo obj)
        {
            if (attackPfx != null)
            {
                attackPfx.Play();
            }

            if (attackSound != null)
            {
                attackSound.Play();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var targetable = other.GetComponent<Targetable>();
            if (targetable)
            {
                TakeDamage(1, targetable.transform.position, targetable.configuration.camp);
            }
        }
    }
}