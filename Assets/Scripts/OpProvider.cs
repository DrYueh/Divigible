using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpProvider : MonoBehaviour {
    public string op { 
        get { 
            Tile t = GetComponentInChildren<Tile>();
            return t == null ? null : t.tileOperator;
        }
    }
}
