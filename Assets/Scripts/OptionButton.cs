using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI optionBox;
    [SerializeField] private ConversationLoader convoManager;
    private int nextConvo = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        convoManager = GameObject.Find("DialogueSystem").GetComponent<ConversationLoader>();
    }

    public void SetContext(int index, string text) {
        nextConvo = index;
        optionBox.text = text;
    }

    public void NextConvo() {
        convoManager.NextConvo(nextConvo);
    }
}
