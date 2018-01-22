using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundManager : MonoBehaviour {
    private int creatureNum = 20; //How many creatures to make
    public GameObject creaturePrefab; //The creature prefab
    private float delay = 0.1f; //How long between creature spawns


	void Start () {
        Time.timeScale = 1; //Set time to default
        Time.fixedDeltaTime = 0.02f;

        StartCoroutine(SpawnCreatures());
	}


    IEnumerator SpawnCreatures()
    {
        for (int i = 0; i < creatureNum; i++)
        {
            //Spawn random creature and set running
            GameObject c = Instantiate(creaturePrefab, this.transform);
            c.GetComponent<CreatureController>().genes = Helper.CreateRandomCreature(1, 1);
			c.GetComponent<CreatureController>().Run(true);
            yield return new WaitForSeconds(delay); //Wait for delay time
        }
    }
}
