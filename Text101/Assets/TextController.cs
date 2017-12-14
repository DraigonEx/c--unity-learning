using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public Text text;
    public Text uiLife;
    public Text uiGold;

    private enum States {
        start, sign, road_0, forest_approach, town, news, tavern, dead,
        armory, bsmith, fountain, reset, drink, encounter, randomEncounter, battle_fled,
        battle_won
    }

    private enum Encounters {
        goblin_one, rat_one, thief_one, goblin_two, rat_two, thief_two
    }

    private States myState;
    private Encounters myEncounter;
    private bool haveSword;
    private bool sharpSword;
    private bool haveArmor;
    private bool tavVisit;
    private bool seenNews;
    private int gold;
    private int maxLife;
    private int life;
    private int level;
    private bool firstHeal;

    // Use this for initialization
    void Start () {
        myState = States.start;
        haveSword = true;
        sharpSword = false;
        haveArmor = false;
        tavVisit = false;
        seenNews = false;
        gold = 0;
        life = 10;
        level = 1;
        firstHeal = true;
	}
	
	// Update is called once per frame
	void Update () {
        maxLife = level * 10;
        uiLife.text = "" + life;
        uiGold.text = "" + gold;

        if (life <= 0 && myState != States.dead)
        {
            life = 0;
            myState = States.dead;
        }
        else if (myState == States.dead) { dead(); }
        else if (myState == States.start) { start(); }
        else if (myState == States.sign) { sign(); }
        else if (myState == States.forest_approach) { forest_approach(); }
        else if (myState == States.town) { town(); }
        else if (myState == States.road_0) { road_0(); }
        else if (myState == States.news) { news(); }
        else if (myState == States.tavern) { tavern(); }
        else if (myState == States.armory) { armory(); }
        else if (myState == States.bsmith) { bsmith(); }
        else if (myState == States.fountain) { fountain(); }
        else if (myState == States.drink) { drink(); }
        else if (myState == States.reset) { reset(); }
        else if (myState == States.encounter) { encounter(); }
        else if (myState == States.randomEncounter) { randomEncounter(); }
        else if (myState == States.battle_fled) { battleFled(); }
        else if (myState == States.battle_won) { battleWon(); }
    }

    private void battleWon()
    {
        text.text = "A hard fought battle leaves you tired and hurt, " +
            "but you found some gold. \n\n" +
            "Press T to head back to Town \n\n" +
            "Press F to continue scouting the Forest";
        if (Input.GetKeyDown(KeyCode.F)) { myState = States.forest_approach; }
        else if (Input.GetKeyDown(KeyCode.T)) { myState = States.town; }
    }

    private void battleFled()
    {
        text.text = "After lots of running, and a bit hiding in bushes or up trees, you've managed to get away.\n\n" +
            "Press T to head back to Town \n\n" +
            "Press F to continue scouting the Forest";
        if (Input.GetKeyDown(KeyCode.F)) { myState = States.forest_approach; }
        else if (Input.GetKeyDown(KeyCode.T)) { myState = States.town; }
    }

    private void reset()
    {
        haveSword = true;
        sharpSword = false;
        haveArmor = false;
        tavVisit = false;
        seenNews = false;
        gold = 0;
        level = 1;
        firstHeal = true;
        myState = States.start;
    }

    private void dead()
    {
        text.text = "Everything slowly goes dark. Perhaps the gods will be kind " +
                    "and give you better luck in the next life. \n\n" +
                    "Press T to Try again";
        if(Input.GetKeyDown(KeyCode.T))
        {
            life = 10;
            myState = States.reset;
        }
    }

    private void drink()
    {
        text.text = "The water is cold and refreshing.";

        if (firstHeal)
        {
            text.text += "Perhaps there's something " +
                         "magical about it because you're feeling completely well.";
            
        }
        text.text += "\n\n Press S to return to the town Square";
        life = maxLife;
        if (Input.GetKeyDown(KeyCode.S))
        {
            myState = States.town;
            if(firstHeal)
                firstHeal = false;
        }
    }

    private void fountain()
    {
        text.text = "The fountain runs cold and clear. \n\n";
        if(life < maxLife)
        {
            text.text += "You're so worn and tired and the water looks so good. \n\n" +
                        "Press D to Drink some water. \n\n";
        }
        text.text += "Press S to look at the town Square again";
        if      (life < maxLife && Input.GetKeyDown(KeyCode.D)) { myState = States.drink; }
        else if (Input.GetKeyDown(KeyCode.S))                   { myState = States.town; }
    }

    private void bsmith()
    {
        text.text = "The blacksmith looks up as you enter his shop. He grunts and motions " +
                    "towards a sign. \"Will sharpen or fix tools for 10 gold.\"\n\n" +
                    ListInventory();
        if(gold >= 10) { text.text += "Press P to Pay for your sword to be sharpened\n\n"; }
        text.text += "Press S to return to the town Square";
        if (Input.GetKeyDown(KeyCode.P))
        {
            haveSword = false;
            sharpSword = true;
            gold -= 10;
            myState = States.town;
        }
        else if (Input.GetKeyDown(KeyCode.S)) { myState = States.town; }
    }

    private void armory()
    {
        text.text = "Various pieces of armor line the walls. An old man sits at the " +
                    "bench working on a nearly finished set of leather armor.\n\n" +
                    "\"Some armor will keep you alive if you're headed towards" +
                    "the forest. Only 100 gold pieces!\" He grins smugly at you.\n\n" +
                    ListInventory();
        if (gold >= 100) { text.text += "Press B to buy yourself some armor\n\n"; }
        text.text += "Press S to return to the town Square";
        if (Input.GetKeyDown(KeyCode.B))
        {
            gold -= 100;
            haveArmor = true;
            myState = States.town;
        }
        if (Input.GetKeyDown(KeyCode.S)) { myState = States.town; }
    }

    private void tavern()
    {
        text.text = "As you enter the tavern, a large man by the door clears his throat " +
                    "and makes a threatening motion at you. Perhaps this has something to " +
                    "do with your fuzzy memory of last night. Maybe you'd better leave.\n\n" +
                    "Press S to Scurry back to the town Square";
        if      (Input.GetKeyDown(KeyCode.S))
        {
            tavVisit = true;
            myState = States.town;
        }
    }

    private void news()
    {
        text.text = "A large notice dominates the news board. Apparently " +
                    "the local princess has gotten herself carried off by brigands " +
                    "and is being held in the castle back beyond the forest. A large " +
                    "reward is being offered for her rescue.\n\n" +
                    "Press S to return to the town Square";
        seenNews = true;
        if      (Input.GetKeyDown(KeyCode.S)) { myState = States.town; }
    }

    private void road_0()
    {
        text.text = "Birds are singing and the countryside along the road appears peaceful. " +
                    "The road disappears into the distance in either direction. Town is off " +
                    "to the left, and a dark forest is to the right.\n\n" +
                    ListInventory() +
                    "Press R to follow the road to the Right to the forest \n\n" +
                    "Press L to follow the road to the Left to town";
        if      (Input.GetKeyDown(KeyCode.R)) { myState = States.forest_approach; }
        else if (Input.GetKeyDown(KeyCode.L)) { myState = States.town; }
    }

    private void town()
    {
        text.text = "The town's occupants watch you carefully. Around the town " +
                    "square you can see a tavern, a blacksmith, and an armory. A " +
                    "large board seems covered with various papers. In the center " +
                    "of the square rises a large fountain.\n\n"; // +
                    //ListInventory();
        if(!haveArmor && gold > 0)  { text.text += "Press A to visit the Armory \n\n"; }
        if(!sharpSword && gold > 0) { text.text += "Press B to visit the Blacksmith \n\n"; }
        text.text += "Press N to have a look at the News board \n\n" +
                    "Press F to stop by the Fountain \n\n" +
                    "Press R to Return to the Road \n\n";
        if (!tavVisit) { text.text += "Press T to Try the Tavern"; }
        if      (Input.GetKeyDown(KeyCode.N)) { myState = States.news; }
        else if (Input.GetKeyDown(KeyCode.F)) { myState = States.fountain; }
        else if (Input.GetKeyDown(KeyCode.R)) { myState = States.road_0; }
        else if (Input.GetKeyDown(KeyCode.T) && !tavVisit) { myState = States.tavern; }
        else if (Input.GetKeyDown(KeyCode.A)) { if (!haveArmor) { myState = States.armory; } }
        else if (Input.GetKeyDown(KeyCode.B)) { if (!sharpSword) { myState = States.bsmith; } }
    }

    private void forest_approach()
    {
        if (!haveArmor)
        {
            myState = States.encounter;
        }
        else
        {
            text.text = "The threats known to frequent this area seem to be put off " +
                        "by the appearance of your armor. Likely you're more prepared " +
                        "than they want to deal with. \n\n " +
                        "The forest looms ahead and the road leads through it to the " +
                        "castle beyond.\n\n" +
                        "Congratulations on making it through the forest!\n\n" +
                        "Press P to Play again";
            if (Input.GetKeyDown(KeyCode.P)) { myState = States.reset; }
        }
    }

    private void sign()
    {
        text.text = "The old road sign shows town is to your left and has warnings " +
                    "about the dark forest to the right.\n\n" +
                    ListInventory() +
                    "Press R to follow the road to the Right \n\n" +
                    "Press L to follow the road to the Left";
        if (Input.GetKeyDown(KeyCode.R)) { myState = States.forest_approach; }
        else if (Input.GetKeyDown(KeyCode.L)) { myState = States.town; }
    }

    private void start()
    {
        text.text = "Birds are singing in the trees above as you awake under a bright morning " +
                    "sky. Last night is kind of fuzzy and you find yourself sitting up " +
                    "next to a well worn road. There's a sign not far from you and the " +
                    "road disappears into the distance in either direction. You're sure " +
                    "town must be in one of those directions.\n\n" +
                    ListInventory() +
                    "Press S to look at the Sign \n\n" +
                    "Press R to follow the road to the Right \n\n" +
                    "Press L to follow the road to the Left";
        if      (Input.GetKeyDown(KeyCode.S)) { myState = States.sign; }
        else if (Input.GetKeyDown(KeyCode.R)) { myState = States.forest_approach; }
        else if (Input.GetKeyDown(KeyCode.L)) { myState = States.town; }
    }

    private string ListInventory()
    {
        string inventoryList = "";

        if (haveArmor) { inventoryList += "You wear a set of well-crafted leather armor.\n\n"; }
        if (haveSword) { inventoryList += "A simple sword hangs from your belt.\n\n"; }
        if (sharpSword) { inventoryList += "Your well-sharpened sword hangs from your belt.\n\n"; }

        return inventoryList;
    }

    private void encounter()
    {
        text.text = "At first, walking along under the trees is pleasant, but then" +
            "you hear rustling in the bushes nearby. Something's coming!\n\n" +
            "Press C to continue";

        if (Input.GetKeyDown(KeyCode.C))
        {
            System.Random random = new System.Random();
            Array myList = Enum.GetValues(typeof(Encounters));
            myEncounter = (Encounters)myList.GetValue(random.Next(myList.Length));
            myState = States.randomEncounter;
        }
    }

    private void randomEncounter()
    {
        //TODO: build multiple encounters to roll on
        if      (myEncounter == Encounters.goblin_one) { goblin_one(); }
        else if (myEncounter == Encounters.goblin_two) { goblin_two(); }
        else if (myEncounter == Encounters.rat_one)    { rat_one(); }
        else if (myEncounter == Encounters.rat_two)    { rat_two(); }
        else if (myEncounter == Encounters.thief_one)  { thief_one(); }
        else if (myEncounter == Encounters.thief_two)  { thief_two(); }
    }

    private void Victory(int goldEarned, int healthLost)
    {
        gold += goldEarned;
        life -= healthLost;
    }

    private void goblin_one()
    {
        text.text = "A lone goblin jumps out of the bushes grinning as " +
            "he brandishes a mean cudgel. \n\n" +
            "Do you Fight? (Press F) \n\n" +
            "Or do you Run? (Press R)";
        if (Input.GetKeyDown(KeyCode.F))
        {
            Victory(5, 2);
            myState = States.battle_won;
        }
        if (Input.GetKeyDown(KeyCode.R)) { myState = States.battle_fled; }
    }

    private void goblin_two()
    {
        text.text = "Goblins jump out from either side of the road howling in glee. \n\n" +
            "Do you Fight? (Press F) \n\n" +
            "Or do you Run? (Press R)";
        if (Input.GetKeyDown(KeyCode.F))
        {
            Victory(10, 4);
            myState = States.battle_won;
        }
        if (Input.GetKeyDown(KeyCode.R)) { myState = States.battle_fled; }
    }

    private void rat_one()
    {
        text.text = "A large rat snarls viciously at you. It looks rather larger than it should. \n\n" +
            "Do you Fight? (Press F) \n\n" +
            "Or do you Run? (Press R)";
        if (Input.GetKeyDown(KeyCode.F))
        {
            Victory(2, 1);
            myState = States.battle_won;
        }
        if (Input.GetKeyDown(KeyCode.R)) { myState = States.battle_fled; }
    }

    private void rat_two()
    {
        text.text = "Squeaks fill the air as rats swarm towards you with glowing red eyes! \n\n" +
            "Do you Fight? (Press F) \n\n" +
            "Or do you Run? (Press R)";
        if (Input.GetKeyDown(KeyCode.F))
        {
            Victory(3, 2);
            myState = States.battle_won;
        }
        if (Input.GetKeyDown(KeyCode.R)) { myState = States.battle_fled; }
    }

    private void thief_one()
    {
        text.text = "\"Your money or your life!\" growls a dirty looking " +
            "rogue who's stepped out from behind a tree. \n\n" +
            "Do you Fight? (Press F) \n\n" +
            "Or do you Run? (Press R)";
        if (Input.GetKeyDown(KeyCode.F))
        {
            Victory(25, 5);
            myState = States.battle_won;
        }
        if (Input.GetKeyDown(KeyCode.R)) { myState = States.battle_fled; }
    }

    private void thief_two()
    {
        text.text = "You've stumbled on a whole band of brigands. Their " +
            "camp fills with whoops as they grab knives and swords.\n\n" +
            "Do you Fight? (Press F) \n\n" +
            "Or do you Run? (Press R)";
        if (Input.GetKeyDown(KeyCode.F))
        {
            Victory(75, 9);
            myState = States.battle_won;
        }
        if (Input.GetKeyDown(KeyCode.R)) { myState = States.battle_fled; }
    }

}
