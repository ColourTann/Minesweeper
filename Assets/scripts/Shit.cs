using UnityEngine;
using System.Collections;

public class Shit : MonoBehaviour {
    public int bollocks;
    public Sprite unrevealed;
    public Sprite revealed;
    public Sprite flag;
    public Sprite mine;
    public Sprite ooh;
    public Sprite face_ok;
    public Sprite face_ooh;
    public Sprite face_dead;
    public Sprite face_wow;

    private static Shit self;

    public static Shit get() {
        if (self == null) {
            self = ((((Shit)(FindObjectOfType(typeof(Shit))))));
        }
        return self;
    }
}
