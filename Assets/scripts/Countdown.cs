using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    Text text;
    bool paused = true;
    // Use this for initialization
    void Start() {
        
    }
    float time;
    // Update is called once per frame
    void Update() {
        if (paused) return;
        time += Time.deltaTime;
        UpdateText();
    }

    void UpdateText() {
        text = GetComponent<Text>();
        text.text = ((int)time).ToString();
        while (text.text.Length < 3) text.text = "0" + text.text;
    }

    public void PauseTimer() {
        paused = true;
    }

    public void StartTimer() {
        paused = false;
    }

    public void reset() {
        time = 0;
        UpdateText();
    }
}
