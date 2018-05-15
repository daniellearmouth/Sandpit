using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadSpawn : MonoBehaviour {

    public List<GameObject> lstRoads;   // Here are the roads
    int iRoadSelect;                    // Select the lstRoads entry here
    static int iRoadType = -1,          // Creates a 'roadA', 'roadB' and 'roadC' for easy destruction later
               iBearing = 0;            // Current compass heading: 0123 = NESW respectively

    static float fPlaceAlongX,          // These three control where we instantiate the prefab
                 //fPlaceAlongY,
                 fPlaceAlongZ = 200,
                 fRoadRotationY;        // This is the only rotation property we need - it rotates to the horizon

    public void OnTriggerEnter(Collider other)
    {
        /*      ROADS
         * 
         * There are several types of roads that will be instantiated throughout this game.
         * They consist of a straight road, two chicanes and two sweeping turns.
         * 
         * When spawning the straight, the road will simply be placed 200 units (metres) ahead of the previous spawn.
         * When spawning either chicane, the road will be placed 200 units ahead, with the next road being 200 metres
         * offset either left or right, based on the chicane itself.
         * When spawning either turn, it will result in shifting the direction to account for this. If it's a left turn,
         * the compass bearing (iBearing) will be decremented, whilst right turns will increment the bearing, and forces.
         * each next road prefab to be rotated along the Y axis at 90 degree increments/decrements.
         */

        iRoadSelect = Random.Range(0, 5);       // Determines which road comes next.

        // Determines bearing
        switch (iBearing) {
            case 0: // NORTH
                fRoadRotationY = 0;
                break;
            case 1: // EAST
                fRoadRotationY = 90;
                break; 
            case 2: // SOUTH
                fRoadRotationY = 180;
                break;
            case 3: // WEST
                fRoadRotationY = 270;
                break;
            default:
                break;
        }

        Debug.Log("Bearing: " + iBearing + " - Spawn next at: " + fPlaceAlongX + "," + 0 + "," + fPlaceAlongZ);

        GameObject goNewRoad = Instantiate(lstRoads[iRoadSelect],
                                           new Vector3(fPlaceAlongX, 0, fPlaceAlongZ),
                                           Quaternion.Euler(0, fRoadRotationY, 0));

        /*      ROAD MANAGEMENT Part 1
         *
         * As part of this level, road prefabs will be instantiated and destroyed every few seconds (probably; it
         * depends on how well you do), so doing this smoothly is contingent upon naming each piece of road in a
         * logical way as it is instantiated, and destroying it when needed.
         *
         * Originally, there was only a "roadA" and "roadB", however an issue arose when you needed to spawn the
         * same prefab used as the prefab before last; it was destroyed in the script and so couldn't be spawned. 
         * To counteract this, "roadC" was implemented to allow for a three-road cycle.
         *
         * A description of how this aspect of the function works:
         * - Destroy the previous instance
         * - Name the newly-created instance
         * - Increment/return to zero the 'iRoadType' which helps determine what to name the next road.
         */
        if (iRoadType == -1) {
            Destroy(GameObject.Find("Road_Start"));
            goNewRoad.name = "roadC";
            iRoadType++;
        } else if (iRoadType == 0) {
            Destroy(GameObject.Find("roadB"));
            goNewRoad.name = "roadA";
            iRoadType++;
        } else if (iRoadType == 1) {
            Destroy(GameObject.Find("roadC"));
            goNewRoad.name = "roadB";
            iRoadType++;
        } else if (iRoadType == 2) {
            Destroy(GameObject.Find("roadA"));
            goNewRoad.name = "roadC";
            iRoadType = 0;
        }

        /*      ROAD MANAGEMENT Part 2
         *
         * You may see underneath that there is an alarming amount of if() statements and else if() statements.
         * Do not be alarmed; there isn't really much of an elegant way to deal with this kind of thing.
         * 
         * The structure of what goes on below is as follows:
         * - Check the current bearing
         * - Check the currently selected road prefab
         * - Add or negate a certain amount (200 as standard) along the X and Z axes where necessary
         *
         * What this does is it places a spawn point for when you pass through a spawn trigger, allowing a new
         * piece of road to appear when needed in the correct place.
         */
        if (iBearing == 0) {
            if (iRoadSelect == 0) {
                fPlaceAlongZ = fPlaceAlongZ + 200;
            } else if (iRoadSelect == 1) {
                fPlaceAlongX = fPlaceAlongX - 200;
                fPlaceAlongZ = fPlaceAlongZ + 200;
            } else if (iRoadSelect == 2) {
                fPlaceAlongX = fPlaceAlongX + 200;
                fPlaceAlongZ = fPlaceAlongZ + 200;
            } else if (iRoadSelect == 3) {
                fPlaceAlongX = fPlaceAlongX - 200;
                fPlaceAlongZ = fPlaceAlongZ + 200;
                iBearing = 3;
            } else if (iRoadSelect == 4) {
                fPlaceAlongX = fPlaceAlongX + 200;
                fPlaceAlongZ = fPlaceAlongZ + 200;
                iBearing = 1;
            }
        } else if (iBearing == 1) {
            if (iRoadSelect == 0) {
                fPlaceAlongX = fPlaceAlongX + 200;
            } else if (iRoadSelect == 1) {
                fPlaceAlongX = fPlaceAlongX + 200;
                fPlaceAlongZ = fPlaceAlongZ + 200;
            } else if (iRoadSelect == 2) {
                fPlaceAlongX = fPlaceAlongX + 200;
                fPlaceAlongZ = fPlaceAlongZ - 200;
            } else if (iRoadSelect == 3) {
                fPlaceAlongX = fPlaceAlongX + 200;
                fPlaceAlongZ = fPlaceAlongZ + 200;
                iBearing = 0;
            } else if (iRoadSelect == 4) {
                fPlaceAlongX = fPlaceAlongX + 200;
                fPlaceAlongZ = fPlaceAlongZ - 200;
                iBearing = 2;
            }
        } else if (iBearing == 2) {
            if (iRoadSelect == 0) {
                fPlaceAlongZ = fPlaceAlongZ - 200;
            } else if (iRoadSelect == 1) {
                fPlaceAlongX = fPlaceAlongX + 200;
                fPlaceAlongZ = fPlaceAlongZ - 200;
            } else if (iRoadSelect == 2) {
                fPlaceAlongX = fPlaceAlongX - 200;
                fPlaceAlongZ = fPlaceAlongZ - 200;
            } else if (iRoadSelect == 3) {
                fPlaceAlongX = fPlaceAlongX + 200;
                fPlaceAlongZ = fPlaceAlongZ - 200;
                iBearing = 1;
            } else if (iRoadSelect == 4) {
                fPlaceAlongX = fPlaceAlongX - 200;
                fPlaceAlongZ = fPlaceAlongZ - 200;
                iBearing = 3;
            }
        } else if (iBearing == 3) {
            if (iRoadSelect == 0) {
                fPlaceAlongX = fPlaceAlongX - 200;
            } else if (iRoadSelect == 1) {
                fPlaceAlongX = fPlaceAlongX - 200;
                fPlaceAlongZ = fPlaceAlongZ - 200;
            } else if (iRoadSelect == 2) {
                fPlaceAlongX = fPlaceAlongX - 200;
                fPlaceAlongZ = fPlaceAlongZ + 200;
            } else if (iRoadSelect == 3) {
                fPlaceAlongX = fPlaceAlongX - 200;
                fPlaceAlongZ = fPlaceAlongZ - 200;
                iBearing = 2;
            } else if (iRoadSelect == 4) {
                fPlaceAlongX = fPlaceAlongX - 200;
                fPlaceAlongZ = fPlaceAlongZ + 200;
                iBearing = 0;
            }
        }
    }
}
