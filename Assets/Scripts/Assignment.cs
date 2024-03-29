
/*
This RPG data streaming assignment was created by Fernando Restituto with 
pixel RPG characters created by Sean Browning.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using UnityEngine.UI;

#region Assignment Instructions

/*  Hello!  Welcome to your first lab :)

Wax on, wax off.

    The development of saving and loading systems shares much in common with that of networked gameplay development.  
    Both involve developing around data which is packaged and passed into (or gotten from) a stream.  
    Thus, prior to attacking the problems of development for networked games, you will strengthen your abilities to develop solutions using the easier to work with HD saving/loading frameworks.

    Try to understand not just the framework tools, but also, 
    seek to familiarize yourself with how we are able to break data down, pass it into a stream and then rebuild it from another stream.


Lab Part 1

    Begin by exploring the UI elements that you are presented with upon hitting play.
    You can roll a new party, view party stats and hit a save and load button, both of which do nothing.
    You are challenged to create the functions that will save and load the party data which is being displayed on screen for you.

    Below, a SavePartyButtonPressed and a LoadPartyButtonPressed function are provided for you.
    Both are being called by the internal systems when the respective button is hit.
    You must code the save/load functionality.
    Access to Party Character data is provided via demo usage in the save and load functions.

    The PartyCharacter class members are defined as follows.  */

public partial class PartyCharacter
{
    public int classID;

    public int health;
    public int mana;

    public int strength;
    public int agility;
    public int wisdom;

    public LinkedList<int> equipment;

}


/*
    Access to the on screen party data can be achieved via …..

    Once you have loaded party data from the HD, you can have it loaded on screen via …...

    These are the stream reader/writer that I want you to use.
    https://docs.microsoft.com/en-us/dotnet/api/system.io.streamwriter
    https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader

    Alright, that’s all you need to get started on the first part of this assignment, here are your functions, good luck and journey well!
*/


#endregion

#region Assignment Part 1

static public class AssignmentPart1
{
    static public void SavePartyButtonPressed()
    {
        foreach (PartyCharacter pc in GameContent.partyCharacters)
        {
            Debug.Log("PC class id == " + pc.classID);

            using (StreamWriter sw = new StreamWriter("Assets/SavedChars/SavedChars.txt"))
            {
                foreach (PartyCharacter pc1 in GameContent.partyCharacters)
                {
                    sw.WriteLine(pc1.classID);
                    sw.WriteLine(pc1.health);
                    sw.WriteLine(pc1.mana);
                    sw.WriteLine(pc1.strength);
                    sw.WriteLine(pc1.agility);
                    sw.WriteLine(pc1.wisdom);
                }
            }
        }
    }

    static public void LoadPartyButtonPressed()
    {
        // Clears Characters for Reset
        GameContent.partyCharacters.Clear();

        using (StreamReader sr = new StreamReader("Assets/SavedChars/SavedChars.txt"))
        {
            string classId;
            string health;
            string mana;
            string strength;
            string agility;
            string wisdom;

            while ((classId = sr.ReadLine()) != null && (health = sr.ReadLine()) != null 
               && (mana = sr.ReadLine()) != null && (strength = sr.ReadLine()) != null
               && (agility = sr.ReadLine()) != null && (wisdom = sr.ReadLine()) != null)
            {
                Debug.Log(classId);
                PartyCharacter pc = new PartyCharacter(int.Parse(classId), int.Parse(health), int.Parse(mana), int.Parse(strength), int.Parse(agility), int.Parse(wisdom));
                GameContent.partyCharacters.AddLast(pc);
            }
        }
        GameContent.RefreshUI();
    }
}

#endregion


#region Assignment Part 2

////  Before Proceeding!
////  To inform the internal systems that you are proceeding onto the second part of this assignment,
////  change the below value of AssignmentConfiguration.PartOfAssignmentInDevelopment from 1 to 2.
////  This will enable the needed UI/function calls for your to proceed with your assignment.
static public class AssignmentConfiguration
{
    public const int PartOfAssignmentThatIsInDevelopment = 2;
}

///*

//In this part of the assignment you are challenged to expand on the functionality that you have already created.  
//    You are being challenged to save, load and manage multiple parties.
//    You are being challenged to identify each party via a string name (a member of the Party class).

//To aid you in this challenge, the UI has been altered.  

//    The load button has been replaced with a drop down list.  
//    When this load party drop down list is changed, LoadPartyDropDownChanged(string selectedName) will be called.  
//    When this drop down is created, it will be populated with the return value of GetListOfPartyNames().

//    GameStart() is called when the program starts.

//    For quality of life, a new SavePartyButtonPressed() has been provided to you below.

//    An new/delete button has been added, you will also find below NewPartyButtonPressed() and DeletePartyButtonPressed()

//Again, you are being challenged to develop the ability to save and load multiple parties.
//    This challenge is different from the previous.
//    In the above challenge, what you had to develop was much more directly named.
//    With this challenge however, there is a much more predicate process required.
//    Let me ask you,
//        What do you need to program to produce the saving, loading and management of multiple parties?
//        What are the variables that you will need to declare?
//        What are the things that you will need to do?  
//    So much of development is just breaking problems down into smaller parts.
//    Take the time to name each part of what you will create and then, do it.

//Good luck, journey well.

//*/

static public class AssignmentPart2
{
    static List<string> listOfPartyNames;

    static public void GameStart()
    {
        listOfPartyNames = new List<string>();
        GameContent.RefreshUI();
    }

    static public List<string> GetListOfPartyNames()
    {
        return listOfPartyNames;
    }

    static public void LoadPartyDropDownChanged(string selectedName)
    {
        // Clear the existing party members before loading the new party.
        GameContent.partyCharacters.Clear();

        // Implement this function to load the selected party data.
        // You can use the provided example to read the data from a file.
        using (StreamReader sr = new StreamReader($"Assets/SavedChars/{selectedName}.txt"))
        {
            while (!sr.EndOfStream)
            {
                int classId = int.Parse(sr.ReadLine());
                int health = int.Parse(sr.ReadLine());
                int mana = int.Parse(sr.ReadLine());
                int strength = int.Parse(sr.ReadLine());
                int agility = int.Parse(sr.ReadLine());
                int wisdom = int.Parse(sr.ReadLine());
                // Add more properties as needed.

                // Create a new PartyCharacter with all properties.
                PartyCharacter pc = new PartyCharacter(classId, health, mana, strength, agility, wisdom);
                GameContent.partyCharacters.AddLast(pc);
            }
        }

        // Refresh the UI to display the new party members and remove the old ones.
        GameContent.RefreshUI();
    }

    static public void SavePartyButtonPressed(string partyName)
    {
        // Modify this function to save the party with the given name.
        using (StreamWriter sw = new StreamWriter($"Assets/SavedChars/{partyName}.txt"))
        {
            // Write the party data to the file.
            foreach (PartyCharacter pc in GameContent.partyCharacters)
            {
                sw.WriteLine(pc.classID);
                sw.WriteLine(pc.health);
                sw.WriteLine(pc.mana);
                sw.WriteLine(pc.strength);
                sw.WriteLine(pc.agility);
                sw.WriteLine(pc.wisdom);
                // Write other party data properties here.
            }
        }
        // Update the list of party names.
        listOfPartyNames.Add(partyName);
        GameContent.RefreshUI();
    }


    //static public void NewPartyButtonPressed(string partyName)
    //{
    //    // Add the new party name to the list.
    //    listOfPartyNames.Add(partyName);
    //    GameContent.RefreshUI();
    //}

    // Function to create a new party with the given party name
    static public void CreateNewParty(string partyName)
    {
        // Initialize the list of party names if it's not already
        if (listOfPartyNames == null)
            listOfPartyNames = new List<string>();

        // Add the new party name to the list
        listOfPartyNames.Add(partyName);
    }

    static public void DeletePartyButtonPressed(string partyName)
    {
        // Remove the selected party name from the list.
        listOfPartyNames.Remove(partyName);
        // Delete the corresponding file (optional).
        File.Delete($"Assets/SavedChars/{partyName}.txt");
        GameContent.RefreshUI();
    }
}

#endregion


