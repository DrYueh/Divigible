using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpTileGenerator : MonoBehaviour { 
    public GameObject addPrefab;
    public GameObject subPrefab;
    public GameObject multPrefab;
    public GameObject divPrefab;
    public GameObject eqPrefab; 
    
    public Tile generateTile(Transform parent, string op) {
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
        obj.transform.position = parent.transform.position;
        obj.transform.rotation = parent.transform.rotation;
        obj.GetComponent<Tile>().layerToTarget = 20;
        
        return obj.GetComponent<Tile>();
    }
}
