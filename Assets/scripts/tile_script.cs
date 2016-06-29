using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class tile_script : MonoBehaviour {
    public enum State {
        Unrevealed, Revealed, Flag

    };
    State state = State.Unrevealed;
    public bool mine;
    public int nearbyMines;
    public int x, y;
    public Transform TEXT_PREFAB;
    SpriteRenderer renderShit;
    bool mouseOver;
    Text text;
    static tile_script mousedScript;
    static face_controller faceHolder;
    static bool lost;
    public static int tilesLeftToReveal;
    // Use this for initialization

    public void Reset() {
        state = State.Unrevealed;
        mine = false;
        nearbyMines = 0;
        if (renderShit != null) {
            renderShit.sprite = Shit.get().unrevealed;
        }
        mouseOver = false;
        if (text != null) {
            text.text = "";
        }
        lost = false;
    }

    void Start() {
        if (faceHolder == null) {
            faceHolder = FindObjectOfType<face_controller>();
        }
        renderShit = GetComponent<SpriteRenderer>();
        renderShit.sprite = Shit.get().unrevealed;
        Transform textObject = (Transform)(Instantiate(TEXT_PREFAB, new Vector3(transform.position.x+16, transform.position.y+16, -3), Quaternion.identity));
        textObject.SetParent(transform);
        //textObject.parent = transform; 
        text = textObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (lost) return;
        if (mouseOver && Input.GetMouseButtonDown(1)) {
            MakeFlag();
        }
    }

    void MakeFlag() {
        if (state == State.Unrevealed) {
            FindObjectOfType<MinesLeftScript>().removeMine();
            state = State.Flag;
            renderShit.sprite = Shit.get().flag;
        }
        else if(state == State.Flag) {
            FindObjectOfType<MinesLeftScript>().addMine();
            state = State.Unrevealed;
            renderShit.sprite = Shit.get().unrevealed;
        }
    }

    public void SetPosition(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public void MakeMine() {
        mine = true;
        foreach (tile_script t in GetAdjacentTiles()) {
            t.nearbyMines++;
        }
    }

    public void reveal() {
        if (lost || state == State.Revealed || state == State.Flag) return;
        state = State.Revealed;
        FindObjectOfType<Countdown>().StartTimer();
        if (mine) {
            youFuckedUp();
            return;
        }
        tilesLeftToReveal--;
        if (tilesLeftToReveal == 0) {
            victory();
        }
        renderShit.sprite = Shit.get().revealed;
        if (nearbyMines != 0) {
            text.text = nearbyMines.ToString();
        }
        if (nearbyMines == 0) {
            foreach (tile_script t in GetAdjacentTiles()) {
                t.reveal();
            }
        }
    }

    void victory() {
        faceHolder.setState(face_controller.face_state.wow);
        FindObjectOfType<Countdown>().PauseTimer();
    }

    private void youFuckedUp() {
        renderShit.sprite = Shit.get().mine;
        faceHolder.setState(face_controller.face_state.dead);
        FindObjectOfType<Countdown>().PauseTimer();
        lost = true;
    }


    void OnMouseEnter() {
        mouseOver = true;
    }

    void OnMouseExit() {
        mouseOver = false;
    }

   
    void GetMouseButtonDown() {

    }

    void OnMouseUp() {
        if(state == State.Unrevealed) {
            renderShit.sprite = Shit.get().unrevealed;
        }
        faceHolder.setState(face_controller.face_state.ok);
        if (mouseOver) {
            reveal();
        }
    }

    void OnMouseDown() {
        if (lost) return;
        mousedScript = this;
        if(state == State.Unrevealed) {
            faceHolder.setState(face_controller.face_state.ooh);
            renderShit.sprite = Shit.get().ooh;
        }
    }

    List<tile_script> GetAdjacentTiles() {
        init_script script = FindObjectOfType<init_script>();
        List<tile_script> result = script.GetAdjacentTiles(this);
        return result;
    }
}
