using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Puzzle))]
[ExecuteInEditMode]
public class PuzzleProvider : MonoBehaviour {
    public int nLeaves = 4;
    public string wildLeaf = "?";
    public string wildOp = ".";
    public char separator = ' ';
    public string definition = "? . ? . ? = ?";
    [SerializeField] private LeafProvider[] leafProviders;
    [SerializeField] private OpProvider[] opProviders;
    public Puzzle puzzle { get { return GetComponent<Puzzle>(); } }

	// Use this for initialization
	void Start () {
#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif
        string[] bits = definition.Split(separator);
        
        puzzle.setNLeaves(bits.Length - 1 / 2);
        puzzle.setLeaf(0, leaf(bits[0]));
        for (int i = 0; i < bits.Length; i += 2) {
            if (i % 2 == 0) {
                int? l = leaf(bits[i]);
                puzzle.setLeaf(i / 2, l);
                puzzle.setGivenLeaf(i / 2, l != null);
                
                if (l != null) {
                    Tile tile = createTile(leafProviders[i / 2].transform.position, (int)l);
                    tile.canDrag = false;
                }
            } else {
                string o = op(bits[i]);
                puzzle.setOp((i - 1) / 2, o);
                puzzle.setGivenOp((i - 1) / 2, o != null);

                if (o != null) {
                    // TODO generate tile
                }
            } 
        }
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
        Puzzle p = puzzle;
        if ((leafProviders == null && nLeaves > 0) || (leafProviders != null && leafProviders.Length != nLeaves)) {
            LeafProvider[] old = leafProviders;
            leafProviders = new LeafProvider[nLeaves];
            if (nLeaves > 0) {
                if (old != null) Array.Copy(old, leafProviders, Math.Min(old.Length, leafProviders.Length));
            } else {
                leafProviders = null;
            }
        }
        if ((opProviders == null && nLeaves > 1) || (opProviders != null && opProviders.Length != nLeaves - 1)) {
            OpProvider[] old = opProviders;
            if (nLeaves > 1) {
                opProviders = new OpProvider[nLeaves - 1];
                if (old != null) Array.Copy(old, opProviders, Math.Min(old.Length, opProviders.Length));
            } else {
                opProviders = null;
            }
        }
        if (p.getNLeaves() != nLeaves) {
            p.setNLeaves(nLeaves);
        }
#endif
	}
    
    public void UpdatePuzzle() {
        Puzzle p = puzzle;
        
        for (int i = 0; i < leafProviders.Length; ++i) {
            int? value = leafProviders[i] == null ? (int?)null : leafProviders[i].value;
            p.setLeaf(i, value);
        }
        for (int i = 0; i < opProviders.Length; ++i) {
            string op = opProviders[i] == null ? null : opProviders[i].op;
            p.setOp(i, op);
        }
    }
    
    int? leaf(string s) {
        return s == "?" ? (int?)null : int.Parse(s);
    }
    
    string op(string s) {
        return s == "." ? null : s;
    }
    
    Tile createTile(Vector3 location, int value) {
        return GameObject.Find("TileCreator").GetComponent<DigitTileGenerator>().generateTile(location, value);
    }
}
