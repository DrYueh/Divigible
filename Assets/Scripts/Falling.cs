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
    public GameManager manager;
    
    private void MoveY(float amount) {
        Vector3 pos = transform.position;
        float y = Mathf.Clamp(pos.y + amount, this.bottom, this.top); 
        transform.position = new Vector3(pos.x, y, pos.z);
    }

    private void Fall (){
        MoveY(- Time.deltaTime * this.fall_factor);
    }
    
    private void Rise (){
        MoveY(Time.deltaTime * this.rise_factor);
    }

    // Update is called once per frame
    void Update () {
        if (this.manager.puzzleSolved()){
            Rise();
        } else {
            Fall();
        }
    }
}
