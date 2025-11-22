using UnityEngine;
using UnityEngine.Serialization;
using Color = System.Drawing.Color;

    public class SlowAffector:Affector
    {
        public  Color radiusEffectColor;

        public GameObject slowEffect;

        public Targetter towerTargetter;
        
        [Range(0, 1)]
        public float slowFactor;

        
        protected void Awake()
        {
            towerTargetter.targetEntersRange += OnTargetEntersRange;
            towerTargetter.targetExitsRange += OnTargetExitsRange;
        }

        /// <summary>
        /// Unsubsribes from the relevant targetter events
        /// </summary>
        void OnDestroy()
        {
            towerTargetter.targetEntersRange -= OnTargetEntersRange;
            towerTargetter.targetExitsRange -= OnTargetExitsRange;
        }
        
        protected void OnTargetEntersRange(Targetable other)
        {
            var agent = other as Agent;
            if (agent == null)
            {
                return;
            }
            var slowComponent = other.gameObject.AddComponent<AgentSlower>();
            if (slowComponent != null)
            {
                slowComponent.Initialize(slowFactor,slowEffect);
            }
        }

        /// <summary>
        /// Fired when the targetter aquires loses a targetable
        /// </summary>
        protected void OnTargetExitsRange(Targetable other)
        {
            var searchable = other as Agent;
            if (searchable == null)
            {
                return;
            }
            var slowComponent = other.gameObject.GetComponent<AgentSlower>();
            if (slowComponent != null)
            {
                slowComponent.RemoveSlow(slowFactor);
                Destroy(slowComponent);
            }
        }
    }
