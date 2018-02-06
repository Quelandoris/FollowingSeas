using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarGoal : MonoBehaviour {
    public TurnPillar[] pillars;
    public GameObject output;
	// Use this for initialization
	void Start () {
        output.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        CheckPillars();
    }

    void CheckPillars()
    {
        foreach(TurnPillar pillar in pillars)
        {
            if (!pillar.GetSolved())
            {
                return;
            }
        }
        output.SetActive(true);
    }
}
