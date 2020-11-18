﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int minRegen;
    public int maxRegen;
    public float countdown;
    private bool isTimerRunning;
    private CellGrid grid;
    int[] pos;

    // Start is called before the first frame update
    void Start()
    {
        countdown = Random.Range(minRegen, maxRegen);
        isTimerRunning = true;
        grid = gameObject.GetComponentInParent<CellGrid>();
        pos = grid.GetPosAtCoord(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                // If countdown is done then make the depleted tile into a regenerated tile
                countdown = 0;
                isTimerRunning = false;
                Destroy(gameObject);
                if (gameObject.GetComponent<TreeTile>() != null)
                {
                    grid.CreateTile(pos[0], pos[1], grid.treeTile);
                }
                else if (gameObject.GetComponent<MetalTile>() != null)
                {
                    grid.CreateTile(pos[0], pos[1], grid.metalTile);
                }
                else if (gameObject.GetComponent<StoneTile>() != null)
                {
                    grid.CreateTile(pos[0], pos[1], grid.stoneTile);
                }
            }
        }
    }
}
