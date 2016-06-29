using UnityEngine;
using System.Collections;

public class face_controller : MonoBehaviour {

    public enum face_state {
        ok, ooh, dead, wow
    }
    face_state state;
    SpriteRenderer faceSprite;
	// Use this for initialization
	void Start () {
        
	}

    public void setState(face_state state) {
        if(faceSprite == null) {
            faceSprite = GetComponent<SpriteRenderer>();
        }
        if(this.state == face_state.dead) {
            return;
        }
        this.state = state;
        switch (state) {
            case face_state.ok:
                faceSprite.sprite = Shit.get().face_ok;
                break;
            case face_state.ooh:
                faceSprite.sprite = Shit.get().face_ooh;
                break;
            case face_state.dead:
                faceSprite.sprite = Shit.get().face_dead;
                break;
            case face_state.wow:
                faceSprite.sprite = Shit.get().face_wow;
                break;
        }
    }
	
    public void Reset() {
        this.state = face_state.ok;
        setState(face_state.ok);
    }

    void OnMouseDown() {
        print("clickface");
        FindObjectOfType<init_script>().Reset();
    }
    
	void Update () {
	
	}
}
