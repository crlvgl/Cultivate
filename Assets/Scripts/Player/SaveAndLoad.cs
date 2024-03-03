using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private Vector2 playerPosition;
    public Transform playerTransform;
    public Transform cameraTransform;
    public int Bridge1 = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) {
            playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
            PlayerPrefs.SetFloat("playerPositionX", playerPosition.x);
            PlayerPrefs.SetFloat("playerPositionY", playerPosition.y);
            PlayerPrefs.SetInt("PlayerControl", ClickAgentController.PlayerControl);
            PlayerPrefs.SetInt("Relic", Inventory.Relic);
            PlayerPrefs.SetInt("Wood", Inventory.Wood);
            if (Bridge.Bridge1Unlocked == true)
            {Bridge1 = 1;}
            else 
            {Bridge1 = 0;}
            PlayerPrefs.SetInt("Bridge1", Bridge1);
            PlayerPrefs.SetInt("Flower", Inventory.Flower);
            PlayerPrefs.SetInt("Altar", Inventory.Altar);
            PlayerPrefs.SetFloat("Exhaustion", Exhaustion.exhaustionPoints);
        }

        if (Input.GetKeyDown(KeyCode.L) || staticInfoClass.loadScene == true) {
            staticInfoClass.loadScene = false;
            //Player and Camera position
            float playerPositionX = PlayerPrefs.GetFloat("playerPositionX");
            float playerPositionY = PlayerPrefs.GetFloat("playerPositionY");
            Vector2 playerPosition = new Vector2(playerPositionX,playerPositionY);
            ClickAgentController.target = playerPosition;
            this.transform.position = playerPosition;
            Vector3 CameraPosition = new Vector3(playerPositionX,playerPositionY,-10f);
            CameraMovement.CameraPosition = CameraPosition;
            //everything else
            ClickAgentController.PlayerControl = PlayerPrefs.GetInt("PlayerControl");
            Inventory.Relic = PlayerPrefs.GetInt("Relic");
            Inventory.Wood = PlayerPrefs.GetInt("Wood");
            Inventory.Flower = PlayerPrefs.GetInt("Flower");
            Bridge1 = PlayerPrefs.GetInt("Bridge1");
            if (Bridge1 == 1)
            {Bridge.Bridge1Unlocked = true;}
            else
            {Bridge.Bridge1Unlocked = false;}
            Inventory.Altar = PlayerPrefs.GetInt("Altar");
            Exhaustion.exhaustionPoints = PlayerPrefs.GetFloat("Exhaustion");
        }
    }

}
