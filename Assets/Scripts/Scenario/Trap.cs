using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    [SerializeField]
    string id = "";

    [SerializeField]
    GameObject DeadBodySprite;

    Collider2D trapCollider;

    [SerializeField]
    SpriteRenderer backColor, headColor, eyesColor;

    private void Awake() {
        if (id == "") {
            Debug.LogError("Trap without id ", gameObject);
        }

        trapCollider = GetComponent<Collider2D>();

        LoadTrapData();
    }

    private void LoadTrapData() {
        TrapScriptable trap = TrapManager.Instance.GetTrap(id);
        if (trap != null) {
            backColor.color = trap.backColor;
            headColor.color = trap.headColor;
            eyesColor.color = trap.eyesColor;
            DeadBodySprite.SetActive(true);
            trapCollider.enabled = false;
        } else {
            DeadBodySprite.SetActive(false);
            trapCollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == (int)Layers.Player) {
            PlayerColor playerColor = collision.gameObject.GetComponent<PlayerColor>();
            Color[] colors = playerColor.GetColors();

            TrapScriptable trapData = new TrapScriptable();
            trapData.id = id;
            trapData.backColor = colors[0];
            trapData.headColor = colors[1];
            trapData.eyesColor = colors[2];

            TrapManager.Instance.SaveTrap(trapData);

            // TODO: KILL PLAYER
            SceneManager.LoadScene((int)Scenes.TutorialLevel);
        }
    }
}
