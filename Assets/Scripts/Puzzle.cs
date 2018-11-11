using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Puzzle : MonoBehaviour {

	public int nLeaves;
	private int?[] leaves;
	private string[] ops;

	private bool[] givenLeaf;
	private bool[] givenOp;

	// Use this for initialization
	void Start () {
		setNLeaves (nLeaves);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setNLeaves(int nLeaves) {
		this.nLeaves = nLeaves;
		leaves = new int?[nLeaves];
		ops = new string[nLeaves - 1];
		givenLeaf = new bool[nLeaves];
		givenOp = new bool[nLeaves - 1];
	}

	public void clear() {
		for (int i = 0; i < nLeaves; ++i) {
			if (!givenLeaf[i]) leaves[i] = null;
		}
		for (int i = 0; i < nLeaves - 1; ++i) {
			if (!givenOp[i]) ops[i] = null;
		}
	}

	public void setLeaf(int i, int? v) {
		if (!givenLeaf[i]) {
			leaves [i] = v;
		} else {
			throw new LeafIsGivenException("You cannot assign to a leaf that is given as part of the puzzle.");
		}
	}

	public void setOp(int i, string v) {
		if (!givenOp[i]) {
			ops [i] = v;
		} else {
			throw new OpIsGivenException("You cannot assign to an op that is given as part of the puzzle.");
		}
	}

	public void setGivenLeaf(int i, bool given) {
		givenLeaf[i] = given;
	}

	public void setGivenOp(int i, bool given) {
		givenOp[i] = given;
	}

	public bool isLockedLeaf(int i) {
		return givenLeaf[i];
	}
	
	public bool isLockedOp(int i) {
		return givenOp[i];
	} 

	public bool eval() {
		List<int?> leaves = new List<int?> (this.leaves);
		List<string> ops = new List<string> (this.ops);

		// iterate through highest precedence operators first
		foreach (string[] opsNext in Operators.order) {
			for (int i = 0; i < ops.Count; ++i) {
				string op = ops [i];
				if (opsNext.Contains (op)) {

					int? a = leaves [i];
					int? b = leaves [i + 1];

					// always false if missing variables
					if (a == null || b == null) {
						return false;
					}

					object result = Operators.ops [op] (a.Value, b.Value);

					// return the boolean result if one is achieved.
					if (result is bool) {
						return (bool)result;
					// collapse the tree if an int is recieved.
					} else {
						leaves [i] = (int)result;
					}

					// consume operator and leaves.
					ops.RemoveAt (i);
					leaves.RemoveAt (i + 1);

					--i; // since we dropped an op, decrement
				}
			}
		}

		return false;
	}

	public override string ToString() {
		string s = leaves [0].ToString ();

		for (int i = 1; i < leaves.Count (); ++i) {
			s += " " + ops [i - 1] + " " + leaves [i].ToString();
		}

		return s;
	}
}

public static class Operators {
	public static string[][] order;
	public static Dictionary<string, Func<int, int, object>> ops;

	static Operators() {
		ops = new Dictionary<string, Func<int, int, object>>();
		order = new string[3][];

		order [0] = new string[] { "*", "/" };
		ops ["*"] = (a, b) => a * b;
		ops ["/"] = (a, b) => a / b;

		order [1] = new string[] { "+", "-" };
		ops ["+"] = (a, b) => a + b;
		ops ["-"] = (a, b) => a - b;

		order [2] = new string[] { "=" };
		ops ["="] = (a, b) => a == b;
	}
}

public class LeafIsGivenException : Exception {
	public LeafIsGivenException(string message) : base(message) {}
}

public class OpIsGivenException : Exception {
	public OpIsGivenException(string message) : base(message) {}
}