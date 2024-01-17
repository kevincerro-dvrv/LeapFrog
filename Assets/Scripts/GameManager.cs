using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private int trialCount;
    private int totalJumpCount;
    private int currentTrialJumpCount;   

    public GameObject stonePrefab;

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start() {
        SpawnStones();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnStones() {
        for(int i=0; i<10; i++) {
            Instantiate(stonePrefab, new Vector3(xCoordinate(i), -2f, 0f), Quaternion.identity);
        }
    }

    public  float xCoordinate(int i) {
        return i * 1.8f - (9*1.8f)/2;
    }

    public void AddJump() {
        currentTrialJumpCount++;
    }

    public void EndTrial() {
        totalJumpCount += currentTrialJumpCount;
        trialCount++;
        currentTrialJumpCount = 0;
    }


    public void OnGUI() {
        int textBoxWidth = 120;
        int textBoxHeight = 20;
        int margin = 5;

        GUIStyle rightAlign = new GUIStyle();
        rightAlign.alignment = TextAnchor.MiddleRight;

        string media = "-";
        if(trialCount > 0) {
            media = (((float)totalJumpCount)/trialCount)+"";
        } 
        
        Label(10, 1, textBoxWidth, textBoxHeight, margin, "Numero de ensayos:", trialCount+"", rightAlign);
        Label(10, 2, textBoxWidth, textBoxHeight, margin,"Media hasta el momento:", media, rightAlign);
        Label(10, 3, textBoxWidth, textBoxHeight, margin,"Número saltos ensayo actual:", currentTrialJumpCount+"", rightAlign);
    }

    private void Label(int x, int row, int textBoxWidth, int textBoxHeight, int margin, string text, string value, GUIStyle style) {
        int y = row * (textBoxHeight + margin);
        int x2 = x + textBoxWidth + margin;
        GUI.Label(new Rect(x, y, textBoxWidth, textBoxHeight), text);
        GUI.Label(new Rect(x2, y, textBoxWidth, textBoxHeight), value, style);

    }
}
