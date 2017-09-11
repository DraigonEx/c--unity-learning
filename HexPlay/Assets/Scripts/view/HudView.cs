using UnityEngine;
using UnityEngine.UI;

class HudView : MonoBehaviour
{
    public void ToggleHud(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void progressTurn()
    {
        //next turn logic here...
    }
}
