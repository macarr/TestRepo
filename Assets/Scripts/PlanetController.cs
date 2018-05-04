using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

    public GameObject[] planets;

    Queue<GameObject> availablePlanets = new Queue<GameObject>();

	// Use this for initialization
	void Start () {
        availablePlanets.Enqueue(planets[0]);
        availablePlanets.Enqueue(planets[1]);
        availablePlanets.Enqueue(planets[2]);

        InvokeRepeating("MovePlanetDown", 0, 20f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Dequeue a planet and set isMoving to true so that it starts scrolling down the screen
    void MovePlanetDown()
    {
        EnqueuePlanets();
        if(availablePlanets.Count == 0)
        {
            return;
        }
        GameObject aPlanet = availablePlanets.Dequeue();

        aPlanet.GetComponent<Planet>().isMoving = true;
    }

    //Enequeue planets that are below the screen and not moving
    void EnqueuePlanets()
    {
        foreach(GameObject aPlanet in planets)
        {
            if(aPlanet.transform.position.y < 0 && !aPlanet.GetComponent<Planet>().isMoving) {
                aPlanet.GetComponent<Planet>().ResetPosition();
                availablePlanets.Enqueue(aPlanet);
            }
        }
    }
}
