using UnityEngine;
using System.Collections.Generic;

public class init_script : MonoBehaviour {
    public Transform tile;
    int tileSize = 32;
    const int tilesAcross = 10;
    const int tilesDown = 10;
   
    public tile_script[,] grid = new tile_script[tilesAcross, tilesDown];
    // Use this for initialization
    void Start() {
        print("whit");
        PlayerPrefs.DeleteAll();
        for (int y = 0; y < tilesAcross; y++) {
            for (int x = 0; x < tilesDown; x++) {
                Transform newTile = (Transform)(Instantiate(tile, new Vector3(x * tileSize, y * tileSize, 0), Quaternion.identity));
                grid[x, y] = newTile.GetComponent<tile_script>();
                grid[x, y].SetPosition(x, y);
                newTile.SetParent(transform);
                //newTile.parent = transform;
            }
        }
        print("what");
        Reset();

    }

    public void Reset() {
        print("hi");
        for (int y = 0; y < tilesAcross; y++) {
            for (int x = 0; x < tilesDown; x++) {
                grid[x, y].Reset();
            }
        }
        int numMines = 20;
        for (int i = 0; i < numMines; i++) {
            print("ho");
            tile_script tile;
            do {
                tile = grid[Random.Range(0, tilesAcross), Random.Range(0, tilesDown)];
            }
            while (tile.mine);
            tile.MakeMine();
        }
        tile_script.tilesLeftToReveal = tilesAcross * tilesDown - numMines;
        FindObjectOfType<MinesLeftScript>().setMines(numMines);
        FindObjectOfType<Countdown>().reset();
        FindObjectOfType<face_controller>().Reset();
    }

    public List<tile_script> GetAdjacentTiles(tile_script origin) {
        int startX = origin.x;
        int startY = origin.y;
        List<tile_script> result = new List<tile_script>();
        for (int dx = -1; dx <= 1; dx++) {
            int newX = startX + dx;
            if (newX < 0 || newX >= tilesAcross) {
                continue;
            }
            for (int dy = -1; dy <= 1; dy++) {
                int newY = startY + dy;
                if (newY < 0 || newY >= tilesDown) {
                    continue;
                }
                if (dx == 0 && dy == 0) {
                    continue;
                }
                result.Add(grid[newX, newY]);
            }
        }

        return result;
    }

    // Update is called once per frame
    void Update() {

    }
}
