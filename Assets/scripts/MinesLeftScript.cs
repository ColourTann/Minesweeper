using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinesLeftScript : MonoBehaviour {
    int minesLeft;
	// Use this for initialization
	void Start () {
	
	}

    public void addMine() {
        minesLeft++;
        updateText();
    }

    public void removeMine() {
        minesLeft--;
        updateText();
    }

    public void setMines(int i) {
        minesLeft=i;
        updateText();
    }

    public void updateText() {
        string s = minesLeft.ToString();
        if (s.Length == 1) s = "0" + s;
        GetComponent<Text>().text = s;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
