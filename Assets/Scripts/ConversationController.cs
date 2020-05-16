using System;
using System.Collections.Generic;

[Serializable]
public class ConversationController
{
    private int currentIndex;
    private Conversation onConvo;
    private List<Conversation> conversations;

    public ConversationController(List<Conversation> convos) {
        conversations = convos;
        currentIndex = 0;
        onConvo = getConvo(currentIndex);
    }

    public string GetSpeaker()
    {
        return onConvo.GetSpeakersName();
    }

    public string GetDialogue()
    {
        return onConvo.GetDialogue();
    }

    public List<IndexedString> GetOptions() 
    {
        return onConvo.GetResponses();
    }

    public void NextOption(int nextIndex) 
    {
        currentIndex = nextIndex;
        onConvo = getConvo(currentIndex);
    }

    public string GetAudio()
    {
        return onConvo.GetAudio();
    }

    private Conversation getConvo(int index) {
        foreach (Conversation convo in conversations) 
        {
            if (convo.GetIndex() == index) {
                return convo;
            }
        }
        return conversations[0];
    }
    
}
