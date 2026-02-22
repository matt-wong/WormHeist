using UnityEngine;
using UnityEngine.UI;

public class hudLogic : MonoBehaviour
{

    private Text levelStatusText;

    // Start is called before the first frame update
    void Start()
    {

        this.levelStatusText = this.gameObject.GetComponentInChildren<Text>();

        gameManager gm = gameManager.Instance;
        // gm.LevelCompletedEvent += this.UpdateLevelStatusText;

    }

    void UpdateLevelStatusText(bool test)
    {
        if (this.levelStatusText){
            this.levelStatusText.text = "WOOHOO!";   
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
