using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpTileGenerator : MonoBehaviour { 
    public GameObject addPrefab;
    public GameObject subPrefab;
    public GameObject multPrefab;
    public GameObject divPrefab;
    public GameObject eqPrefab; 
    
    public Tile generateTile(Vector3 location, string op) {
        GameObject prefab = null;
        switch (op) {
            case "+": prefab = addPrefab; break;
            case "-": prefab = subPrefab; break;
            case "*": prefab = multPrefab; break;
            case "/": prefab = divPrefab; break;
            case "=": prefab = eqPrefab; break;
            default: return null;
        }
        GameObject obj = Instantiate(prefab);
        obj.transform.position = location;
        obj.GetComponent<Tile>().layerToTarget = 20;
        
        return obj.GetComponent<Tile>();
    }
}
