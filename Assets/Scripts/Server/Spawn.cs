using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    const int MAXPLAYERS = 4;
    public static Spawn Instance { get; private set; }
    public Transform[] spawns;
    public Scorepanel[] scores;

    PlayerCharacter[] players = new PlayerCharacter[MAXPLAYERS];
    

    private void Awake()
    {
        Spawn.Instance = this;
        Debug.Log("Spawn Instance registered.");
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(Scorepanel sp in scores){
            sp.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int registerPlayer(PlayerCharacter p)
    {
        for (int i = 0; i< MAXPLAYERS; ++i)
        {
            if (players[i] == null)
            {
                players[i] = p;
                Debug.Log("Added player id " + i);
                spawnPlayer(i);
                scores[i].gameObject.SetActive(true);
                scores[i].setScore(0 , 0);
                return i;
            }
        }
        Debug.LogError("Error: Too many players");
        return -1;
    }

    public void deregisterPlayer(int id)
    {
        if (id >= 0)
        {
            players[id] = null;
            scores[id].gameObject.SetActive(false);
            Debug.Log("Removed player id " + id);
        }
    }

    void spawnPlayer(int i)
    {
        PlayerCharacter c = players[i];
        Transform s = spawns[i % spawns.Length];
        c.transform.position = s.position;
        s.transform.LookAt(this.transform, Vector3.up);
    }

    public void killPlayer(int deadPlayer, int attackerId)
    {
        if(attackerId >= 0)
        {
            PlayerCharacter attackerCharacter = players[attackerId];
            attackerCharacter.creditKill();
            scores[attackerId].setScore(attackerCharacter.getKills(), attackerCharacter.getDeaths());
        }
        PlayerCharacter deadCharacter = players[deadPlayer];
        scores[deadPlayer].setScore(deadCharacter.getKills(), deadCharacter.getDeaths());
        spawnPlayer(deadPlayer);
    }
}
