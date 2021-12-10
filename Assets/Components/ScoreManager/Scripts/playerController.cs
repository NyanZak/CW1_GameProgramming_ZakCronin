using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public GameObject[] Players;
    [SerializeField] public GameObject CurrentPlayer;

    void Start()
    {
        for (int i = 1; i < Players.Length; i++)
        {
            Players[i].GetComponent<movementScript>().enabled = false;
        }
        CurrentPlayer = Players[0];
    }

    void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            GameObject otherPlayer = null;
            foreach (GameObject player in Players)
            {
                if (player != CurrentPlayer)
                {
                    otherPlayer = player;
                    break;
                }
            }
            ChangePlayer(otherPlayer);
            CurrentPlayer.GetComponent<movementScript>().enabled = true;
        }
    }

        public void ChangePlayer(GameObject player)
        {
            CurrentPlayer.GetComponent<movementScript>().enabled = false;
            CurrentPlayer = player;
        }
 }

