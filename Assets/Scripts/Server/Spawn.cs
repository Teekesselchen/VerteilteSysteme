using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public static Spawn Instance { get; private set; }
    public Transform[] spawns;
    List<PlayerCharacter> players = new List<PlayerCharacter>();

    private void Awake()
    {
        Spawn.Instance = this;
        Debug.Log("Spawn Instance registered.");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int registerPlayer(PlayerCharacter p)
    {
        players.Add(p);
        int id = players.Count - 1;
        Debug.Log("Added player id "+ id);
        spawnPlayer(id);
        return id;
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
            players[attackerId].creditKill();
        }
        spawnPlayer(deadPlayer);
    }
}
