using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationLoader : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI nameTextbox, textTextbox;
    [SerializeField] private GameObject optionButtonPrefab;
     private int ButtonSpacing = 100;
    private List<GameObject> optionButtons;
    private GameObject dialogueSystem;

    public ConversationController conversation;

    private void Start()
    {
        setup();
    }

    private void setup() {
        dialogueSystem = GameObject.Find("DialogueSystem");
        optionButtons = new List<GameObject>();
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        AudioClip clip = Resources.Load<AudioClip>("DialogueAudio/" + conversation.GetAudio());
        audioSource.PlayOneShot(clip);
    }

    public void NextConvo(int index) 
    {
        if (audioSource == null) {
            setup();
        }
        foreach (GameObject button in optionButtons)
        {
            GameObject.Destroy(button);
        }
        audioSource.Stop();
        conversation.NextOption(index);
        nameTextbox.text = conversation.GetSpeaker();
        textTextbox.text = conversation.GetDialogue();
        PlayAudio();
        int count = 0;
        foreach (IndexedString option in conversation.GetOptions()) 
        {
            GameObject btn = Instantiate(optionButtonPrefab);
            btn.GetComponent<OptionButton>().SetContext(option.index, option.str);
            btn.transform.SetParent(dialogueSystem.transform);
            btn.transform.Translate(new Vector3(784, 324, 0));
            btn.transform.Translate(Vector3.down * count * ButtonSpacing);
            optionButtons.Add(btn);
            count++;
        }
        if (conversation.GetOptions().Count == 0) 
        {
            CloseDialogue();
            //This next bit is hard coded and isn't propper at all
            SceneManager.LoadScene(2);
        } 
    }

    public void CloseDialogue() 
    {
        GameObject.Find("EventSystem").GetComponent<InGameMenuController>().CloseDialogue();
    }
 
    public void LoadConversation(string conversationName) {
        List<Conversation> convos = new List<Conversation>();
        string convoText = Resources.Load<TextAsset>("Conversations/" + conversationName).text;

        var delimiter2 = new char[] { '\n' };

        var lines = convoText.Split(delimiter2, StringSplitOptions.RemoveEmptyEntries);
        var delimiters = new char[] { '\t' };


        foreach (var line in lines)
        {
            int index = -1;
            string name = "", dialogue = "", audio = "";
            List<IndexedString> responses = new List<IndexedString>();

            int responseIndex = -1;
            //Seperate text by Tabs//
            var segments = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            int count = 0;
            foreach (var segment in segments) 
            {
                if (count == 0)
                {
                    index = Convert.ToInt32((string)segment.Trim());
                }
                else if (count == 1)
                {
                    name = segment;
                }
                else if (count == 2)
                {
                    dialogue = segment;
                }
                else if (count == 3) {
                    audio = segment;
                }
                // Repeats and goes to an array list //
                else if (count % 2 == 0)
                {
                    string seg = segment.Trim();
                    if (seg != "" && seg != null)
                    {
                        responseIndex = Convert.ToInt32((string)seg);
                    }
                }
                else if (count % 2 == 1)
                {
                    string seg = segment.Trim();
                    if (seg != "" && seg != null)
                    {
                        responses.Add(new IndexedString(responseIndex, seg));
                        responseIndex = -1;
                    }
                }
                count++;
            }
            convos.Add(new Conversation(index, name, dialogue, audio, responses));

        }

        conversation = new ConversationController(convos);
        NextConvo(0);
    }
}
