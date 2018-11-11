using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafProvider : MonoBehaviour {
    public int? value { 
        get { 
            Tile t = GetComponentInChildren<Tile>();
            return t == null ? (int?)null : t.value;
        }
    }
}
