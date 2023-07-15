using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamGameDefinitions : MonoBehaviour
{
    [SerializeField]
    private Health m_player;

    public Team PlayerTeam { get; private set; }
    public Team EnemyTeam { get; private set; }

    public void Create()
    {
        PlayerTeam = new Team()
        {
            Name = "Player Team",
        };

        PlayerTeam.TeamMembers.Value.Add(m_player);
        PlayerTeam.TeamMembers.Refresh();

        EnemyTeam = new Team()
        {
            Name = "Enemy Team"
        };
    }
}
