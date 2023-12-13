using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private Vector2 playerPosition;
    public Transform playerTransform;
    public Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) {
            playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
            int PlayerControl = ClickAgentController.PlayerControl;
            int Relic = Inventory.Relic;
            PlayerPrefs.SetFloat("playerPositionX", playerPosition.x);
            PlayerPrefs.SetFloat("playerPositionY", playerPosition.y);
            PlayerPrefs.SetInt("PlayerControl", PlayerControl);
            PlayerPrefs.SetInt("Relic", Relic);
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            float playerPositionX = PlayerPrefs.GetFloat("playerPositionX");
            float playerPositionY = PlayerPrefs.GetFloat("playerPositionY");
            int PlayerControl = PlayerPrefs.GetInt("PlayerControl");
            int Relic = PlayerPrefs.GetInt("Relic");
            Vector2 playerPosition = new Vector2(playerPositionX,playerPositionY);
            ClickAgentController.target = playerPosition;
            this.transform.position = playerPosition;
            Vector3 CameraPosition = new Vector3(playerPositionX,playerPositionY,-10f);
            CameraMovement.CameraPosition = CameraPosition;
            ClickAgentController.PlayerControl = PlayerControl;
        }
    }

    private class SaveObject{

        
    }
}
