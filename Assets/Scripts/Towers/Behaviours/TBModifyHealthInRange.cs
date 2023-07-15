using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Towers
{
    //TB = Tower Behaviour
    public class TBModifyHealthInRange : MonoBehaviour
    {
        public Tower Tower { get; set; }
        public float Range { get; set; }
        public float TimeBetweenHealing { get; set; }
        public float Amount { get; set; }
        public IEnumerable<Team> Teams { get; set; }

        //EW 15-07-23
        //If you want more complicated behaviour (e.g. delay start time, max healing in time period)
        //we can add them here and add new parameters in GameParameters

        private void Start()
        {
            StartCoroutine(ModifyLocalTargets());
        }

        private IEnumerator ModifyLocalTargets()
        {
            while (true)
            {
                IEnumerable<Health> targets = Teams.SelectMany(team => team.FindTargetsNear(Tower.Position.Value, Range));

                foreach (Health target in targets)
                {
                    float prevHealth = target.CurrentHealth;
                    float nextHealth = target.CurrentHealth + Amount;

                    if(nextHealth <= target.MaximumHealth)
                    {
                        target.SetHealth(nextHealth);
                        string verb = Amount > 0 ? "Healed" : "Damaged";
                        Debug.Log($"{verb} {target.name} for {Amount} from {prevHealth} to {target.CurrentHealth}");
                    }
                }

                yield return new WaitForSeconds(TimeBetweenHealing);
            }
        }
    }
}
