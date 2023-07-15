using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team 
{
    public string Name { get; set; }
    public Topic<List<Health>> TeamMembers = new Topic<List<Health>>(new List<Health>());

    public List<Health> FindTargetsNear(Vector3 position, float range)
    {
        List<Health> targets = new List<Health>();

        foreach (Health health in TeamMembers.Value)
        {
            float distance = Vector3.Distance(health.transform.position, position);

            if (distance <= range)
            {
                targets.Add(health);
            }
        }

        return targets;
    }
}
