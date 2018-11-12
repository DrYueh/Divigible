//This script handles the zeplin falling and rising up

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class Falling : MonoBehaviour {
    public float fall_factor;
    public float rise_factor;
    public float top;
    public float bottom;
    private float height;
    private float daTime;
    
    private void Fall (){
        float _y = this.height;
        _y -= (this.daTime*this.fall_factor);
        if (_y <= this.bottom){
            _y = this.bottom;
        }
        else if (_y >= this.top){
            _y = this.top;
        }
        else{
            this.height = _y;
        }
        
        Vector3 pos = transform.position;
        pos.y = _y;
        transform.position = pos;
    }
//    private float Rise (){ //pass score of puzzel into it?
//        int pos = this.height;
//        pos += 
//    }

    // Use this for initialization
    void Start () {
        this.daTime = Time.time;
        this.height = this.top;        
    }

    // Update is called once per frame
    void Update () {
        this.daTime = Time.deltaTime;
        Fall(); // update the timer object, check for Game end?
        //print(this.height);
    }
}
