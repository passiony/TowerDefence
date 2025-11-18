using UnityEngine;
    using Color = System.Drawing.Color;

    public class SlowAffector:Affector
    {
        public  Color radiusEffectColor;

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
        }
    }
