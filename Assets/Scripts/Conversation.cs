using System.Collections.Generic;

public class Conversation
{
    private int conversationPoint;
    private string name, dialogue;
    private string audio;
    private List<IndexedString> responses;

    public Conversation(int index, string speaker, string text, string dialogueAudio, List<IndexedString> playerResponses) 
    {
        conversationPoint = index;
        name = speaker;
        dialogue = text;
        responses = playerResponses;
        audio = dialogueAudio;
    }

    public int GetIndex() {
        return conversationPoint;
    }
    public string GetSpeakersName() {
        return name;
    }
    public string GetDialogue() {
        return dialogue;
    }
    public string GetAudio() {
        return audio;
    }
    public List<IndexedString> GetResponses() {
        return responses;
    }
}

public class IndexedString 
{
    public string str;
    public int index;

    public IndexedString(int _index, string _str) 
    {
        index = _index;
        str = _str;
    }
}