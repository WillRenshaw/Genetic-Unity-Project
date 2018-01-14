using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundManager : MonoBehaviour {
    public int creatureNum = 20;
    public GameObject creaturePrefab;
    public float delay = 0.1f;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        Helper.userPrefs = (UserPrefs)Helper.ReadData("userprefs.gd");
        StartCoroutine(SpawnCreatures());
        
	}


    IEnumerator SpawnCreatures()
    {
        for (int i = 0; i < creatureNum; i++)
        {
            
            GameObject c = Instantiate(creaturePrefab, this.transform);
            c.GetComponent<CreatureController>().genes = Helper.CreateRandomCreature(1, 1);
            c.GetComponent<CreatureController>().running = true;
            yield return new WaitForSeconds(delay);
        }


    }

    // Update is called once per frame
    void Update () {
		
	}
}
