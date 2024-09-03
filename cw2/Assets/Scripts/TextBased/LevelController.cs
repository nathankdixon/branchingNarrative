using System.Collections;
using System.Collections.Generic;
using TMPro;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    [SerializeField] private GameObject image1;
    [SerializeField] private GameObject image2;
    [SerializeField] private GameObject image3;
    [SerializeField] private GameObject image4;
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject button3;
    [SerializeField] private GameObject button4;

    [SerializeField] private TMP_Text text1;
    [SerializeField] private TMP_Text text2;
    [SerializeField] private TMP_Text text3;
    [SerializeField] private TMP_Text text4;

    [SerializeField] private TMP_Text textfield;
    [SerializeField] private TMP_Text textSceneField;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject endingsFound;

    private bool inText;
    private int storySection;
    private bool gameOver = false;
    private int gameEnding = 0;
    private bool bearInjured = false;
    private bool bearAlerted = false;
    private bool tooClose = false;

    //measures affected by decisions. As measured out of 100 as a percentage
    private int friendshipStrength;
    private int confidence;
    private int chanceOfKill;
    private int chanceOfHit;



    // Start is called before the first frame update
    void Start()
    {

        friendshipStrength = 50;
        confidence = 50;
        chanceOfHit = 10;
        chanceOfKill = 5;
        storySection = 0;
        HideButtons();
        textSceneField.text = "Entering the forest";
    }

    // Update is called once per frame
    void Update()
    {
        if (inText == false)
        {
            storyEngine(storySection, 0);
        }
        if (gameOver == true)
        {
            switch (gameEnding){
                case 1:
                    //both go home
                    EndingsFound.setComplete(1);
                    SceneManager.LoadScene(sceneName: "Ending1");
                    break;
                case 2:
                    //kill bear, both survive
                    EndingsFound.setComplete(2);
                    SceneManager.LoadScene(sceneName: "Ending2");
                    break;
                case 3:
                    //both escape bear when running away
                    EndingsFound.setComplete(3);
                    SceneManager.LoadScene(sceneName: "Ending3");
                    break;
                case 4:
                    //you run away, Jim dies
                    EndingsFound.setComplete(4);
                    SceneManager.LoadScene(sceneName: "Ending4");
                    break;
                case 5:
                    //you die, Jim runs away
                    EndingsFound.setComplete(5);
                    SceneManager.LoadScene(sceneName: "Ending5");
                    break;
                case 6:
                    //both die
                    EndingsFound.setComplete(6);
                    SceneManager.LoadScene(sceneName: "Ending6");
                    break;

            }
        }
        
    }

    private void storyEngine(int storyPart, int buttonClicked)
    {
        switch (storyPart)
        {
            case 0:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        StartCoroutine(introText());
                        break;
                    case 3:
                        friendshipStrength = friendshipStrength + 5;
                        confidence = confidence + 5;
                        storySection = 1;
                        inText = false;
                        break;
                    case 4:
                        friendshipStrength = friendshipStrength - 5;
                        confidence = confidence - 5;
                        storySection = 1;
                        inText = false;
                        break;
                }
                break;
            case 1:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        StartCoroutine(gameText2());
                        break;
                    case 3:
                        friendshipStrength = friendshipStrength + 5;
                        confidence = confidence + 5;
                        storySection = 2;
                        inText = false;
                        break;
                    case 4:
                        friendshipStrength = friendshipStrength - 5;
                        confidence = confidence - 5;
                        storySection = 2;
                        inText = false;
                        break;
                }
                textSceneField.text = "Bear tracks";
                break;
            case 2:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        StartCoroutine(gameText3());
                        break;
                    case 1:
                        //straight to bear
                        storySection = 8;
                        confidence = confidence + 5;
                        inText = false;
                        textSceneField.text = "Straight to it";
                        break;
                    case 2:
                        //high ground
                        storySection = 3;
                        inText = false;
                        textSceneField.text = "High Ground";
                        break;
                    case 3:
                        friendshipStrength = friendshipStrength + 5;
                        //let jim choose, random value
                        StartCoroutine(jimsChoice());
                        break;
                    case 4:
                        // go home
                        //Have multiple options here tho
                        gameOver = true;
                        gameEnding = 1;
                        inText = false;
                        break;
                }
                break;
            case 3:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        StartCoroutine(gameHighGround());
                        break;
                    case 3:
                        //moving a bit closer, but not from normal
                        storySection = 4;
                        inText = false;
                        break;
                    case 4:
                        //same as going straight to bear from before, but without confidence
                        storySection = 8;
                        textSceneField.text = "Straight to it";
                        inText = false;
                        break;
                }
                break;
            case 4:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        StartCoroutine(gameText4());
                        break;
                    case 3:
                        //long shot
                        storySection = 5;
                        inText = false;
                        break;
                    case 4:
                        //same as going straight to bear as before
                        storySection = 8;
                        textSceneField.text = "Straight to it";
                        inText = false;
                        break; ;
                }
                break;
            case 5:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        StartCoroutine(gameLongShot());
                        break;
                    case 1:
                        //aim for headshot
                        storySection = 6;
                        confidence = confidence + 5;
                        inText = false;
                        break;
                    case 2:
                        //aim for body shot
                        storySection = 7;
                        inText = false;
                        break;
                    case 3:
                        //let jim take shot
                        //TODO. Write Jim taking shot
                        StartCoroutine(jimShoots());
                        friendshipStrength = friendshipStrength + 5;
                        storySection = 6;
                        break;
                    case 4:
                        //same as going straight to bear as before
                        storySection = 8;
                        inText = false;
                        break;
                }
                break;
            case 6:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        StartCoroutine(longShotforHead());
                        break;
                    case 3:
                        //same as going straight to bear as before
                        storySection = 8;
                        friendshipStrength = friendshipStrength + 5;
                        inText = false;
                        break;
                    case 4:
                        // go home
                        //Have multiple options here tho
                        gameOver = true;
                        gameEnding = 1;
                        inText = false;
                        break;
                }
                break;
            case 7:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        StartCoroutine(longShotforBody());
                        break;
                    case 3:
                        //same as going straight to bear as before
                        storySection = 8;
                        inText = false;
                        friendshipStrength = friendshipStrength + 5;
                        break;
                    case 4:
                        // go home
                        //Have multiple options here tho
                        gameOver = true;
                        gameEnding = 1;
                        inText = false;
                        break;
                }
                break;
            case 8:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        textSceneField.text = "Close Behind";
                        chanceOfHit = chanceOfHit + 10;
                        StartCoroutine(followingBear());
                        break;
                    case 1:
                        //attack scene
                        storySection = 13;
                        inText = false;
                        break;
                    case 3:
                        chanceOfHit = chanceOfHit + 10;
                        chanceOfKill = chanceOfKill + 8;
                        //moves closer
                        storySection = 9;
                        inText = false;
                        break;
                    case 4:
                        //takes closer shot
                        textSceneField.text = "Taking the shot";
                        StartCoroutine(closeShot1());
                        break;
                }
                break;
            case 9:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        textSceneField.text = "Close Behind";
                        StartCoroutine(followingBear2());
                        break;
                    case 1:
                        //attack scene
                        storySection = 13;
                        inText = false;
                        break;
                    case 3:
                        chanceOfHit = chanceOfHit + 10;
                        chanceOfKill = chanceOfKill + 8;
                        //moves closer
                        storySection = 10;
                        inText = false;
                        break;
                    case 4:
                        //takes closer shot
                        textSceneField.text = "Taking the shot";
                        StartCoroutine(closeShot1());
                        break;
                }
                break;
            case 10:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        textSceneField.text = "Close Behind";
                        StartCoroutine(followingBear3());
                        break;
                    case 1:
                        //attack scene
                        storySection = 13;
                        inText = false;
                        break;
                    case 3:
                        chanceOfHit = chanceOfHit + 10;
                        chanceOfKill = chanceOfKill + 8;
                        friendshipStrength = friendshipStrength - 5;
                        confidence = confidence + 5;
                        //moves closer
                        storySection = 11;
                        inText = false;
                        break;
                    case 4:
                        //takes closer shot
                        textSceneField.text = "Taking the shot";
                        StartCoroutine(closeShot1());
                        break;
                }
                break;
            case 11:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        textSceneField.text = "Close Behind";
                        StartCoroutine(followingBear4());
                        break;
                    case 1:
                        //attack scene
                        storySection = 13;
                        inText = false;
                        break;
                    case 3:
                        tooClose = true;
                        chanceOfHit = chanceOfHit + 10;
                        chanceOfKill = chanceOfKill + 8;
                        friendshipStrength = friendshipStrength - 10;
                        confidence = confidence + 12;
                        //moves closer
                        storySection = 12;
                        inText = false;
                        break;
                    case 4:
                        //takes closer shot
                        textSceneField.text = "Taking the shot";
                        StartCoroutine(closeShot2());
                        break;
                }
                break;
            case 12:
                switch (buttonClicked)
                {
                    case 0:
                        inText = true;
                        textSceneField.text = "Close Behind";
                        StartCoroutine(followingBear5());
                        break;

                    case 1:
                        //attack scene
                        storySection = 13;
                        inText = false;
                        break;
                    case 3:
                        chanceOfHit = chanceOfHit + 10;
                        chanceOfKill = chanceOfKill + 8;
                        friendshipStrength = friendshipStrength - 15;
                        confidence = confidence + 10;
                        //moves too close
                        inText = false;
                        break;
                    case 4:
                        //takes closer shot
                        textSceneField.text = "Taking the shot";
                        StartCoroutine(closeShot2());
                        break;
                }
                break;
            case 13:
            switch(buttonClicked) {
                     case 0:
                        inText = true;
                        textSceneField.text = "Fight or Flight";
                        StartCoroutine(fightOrFlight());
                        break;
                     case 3:
                        //run, test to see if Jim stays
                        if (withOrWithoutJim() == true) {
                            //run with Jim
                            //Based on how close you were to the bear, either both die or both escape
                            if (tooClose == true) {
                                //Both die
                                StartCoroutine(runTogetherDie());
                                gameOver = true;
                                gameEnding = 6;
                            } else {
                                //BOTH SURVIVE
                                StartCoroutine(runTogetherSurvive());
                                gameOver = true;
                                gameEnding = 3;
                            }
                        } else {
                            //run alone
                            storySection = 14;
                            inText = false;
                        }
                        inText = false;
                        break;
                     case 4:
                        //fight, test to see if Jim fights as well
                         if (withOrWithoutJim() == true) {
                             //fight with Jim
                             storySection = 15;
                         } else {
                            //fight alone
                            //BAD FRIEND ENDING
                            gameOver = true;
                            gameEnding = 5;
                         }
                        inText = false;
                        break;
            }
            break;
            case 14:
            switch (buttonClicked) {
                    case 0:
                        inText = true;
                        textSceneField.text = "Running alone";
                        StartCoroutine(runAlone());
                        break;
                    case 3:
                        //go back, fight together.
                        storySection = 15;
                        inText = false;
                        break;
                    case 4:
                        //COWARD ENDING
                        StartCoroutine(runAlone2());
                        gameOver = true;
                        gameEnding = 4;
                        inText = false;
                        break;
            }
            break;
            case 15:
            switch (buttonClicked) {
                    case 0:
                        inText = true;
                        textSceneField.text = "Fight!";
                        StartCoroutine(fightTogether());
                        break;
                    case 3:
                        //Knife attack, too close, bear attacks you directly.
                        StartCoroutine(stabBear());
                        storySection = 16;
                        bearInjured = true;
                        inText = false;
                        break;
                    case 4:
                        //final shot.
                        if (didYouHit(chanceOfHit) == true)
                        {
                            StartCoroutine(hitBear());
                            gameOver = true;
                            gameEnding = 2;
                        } else {
                            StartCoroutine(missBear());
                            storySection = 16;
                        }
                        inText = false;
                        break;
            }
            break;
            case 16:
                switch (buttonClicked) {
                    case 0:
                        inText = true;
                        textSceneField.text = "Fight!";
                        StartCoroutine(bearOnYou());
                        break;
                    case 3:
                        //Jim shoots and kills bear
                        gameOver = true;
                        gameEnding = 2;
                        inText = false;
                        break;
                    case 4:
                        //Jim takes chance to run and you die.
                        gameOver = true;
                        gameEnding = 5;
                        inText = false;
                        break;
                }
                break;
        }
    }

    IEnumerator introText()
    {
        yield return new WaitForSeconds(3);
        //Add More intro
        writeNewText("You enter the woods, your friend Jim follows close behind. You've travelled these woods before, should be a good hunt.\n ");
        yield return new WaitForSeconds(5);
        writeMoreText("Jim: \"So... You ready?\" ");
        yield return new WaitForSeconds(2);
        Show2Options(" \"Of course. Always ready. \" ", " \" As ready as I'll ever be...\" ");
    }

    IEnumerator gameText2()
    {
        yield return new WaitForSeconds(3);
        writeNewText("Jim: \"Yeah well you always say that. Going to be a good day either way, I can feel it... Might even get a big'un\" \n ");
        yield return new WaitForSeconds(5);
        writeMoreText("Jim: \"Reckon it's time we stepped up, I've seen enough dead deers already. Got a big space on my wall I could do with filling. Big bear head there, would look good I reckon\" \n ");
        yield return new WaitForSeconds(6);
        writeMoreText("Jim: \"You're being awful quiet... Too scared are you?\"\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("Jim laughs. \n ");
        yield return new WaitForSeconds(3 );
        Show2Options(" \"Just thinkin... Probably time we upgraded\" ", " \"A deer won't rip my face off, nothing wrong with hunting deer.\" ");
    }

    IEnumerator gameText3()
    {
        yield return new WaitForSeconds(3);
        //Add More intro
        writeNewText("You emerge into a clearing. You can see two paths leading out the clearing and a set of tracks going along one. \n ");
        yield return new WaitForSeconds(5);
        writeMoreText("Jim: \"These are bear tracks! I told you! I told you today would be a good day!\"\n ");
        yield return new WaitForSeconds(5);

        if (friendshipStrength > 49)
        {
            writeMoreText("Jim: \"This is it! Our chance, we've got to take it.\"\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("Jim: \"Right, the left route takes us up, we'd be further away from it, but would have the high ground on it.\"\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("Jim: \"The right path would take us straight to it. Might be risky, but no way we lose it now.\"\n ");
            yield return new WaitForSeconds(4);
        } else
        {
            writeMoreText("Jim: \"This is it! Our chance, we've got to take it.\"\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("Jim: \"There's no way you're saying no to this now. We got two paths we can go. Straight to it or we go left and take the high ground\"\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("Jim: \"You better not bail now.\"\n ");
            yield return new WaitForSeconds(4);
        }

        Show4Options("Go straight to bear", "Go to high ground for clearer shot", "Let Jim decide", "Go home");
    }

    IEnumerator jimsChoice()
    {
        int randomPick = Random.Range(0, 100);
        if (randomPick > confidence)
        {
            writeNewText("Jim: \"I reckon we go high, clear shot, less likely to be seen\" ");
            yield return new WaitForSeconds(4);
            Option2();
        } else
        {
            writeNewText("Jim: \"Straight to it. This is the best chance we've had, I'm not throwing it away to go hide in the hills.\" ");
            yield return new WaitForSeconds(4);
            Option1();
        }
    }

    IEnumerator gameHighGround()
    {
        yield return new WaitForSeconds(3);
        //Add More intro
        writeNewText("The hill is steep but you both climb it quickly. It's a clear day and you can see for miles\n ");
        yield return new WaitForSeconds(5);
        writeMoreText("You see some movement. It's not clear what it is, but Jim already thinks it's the bear\n ");
        yield return new WaitForSeconds(5);
        writeMoreText("Jim: \"That's it, I'm certain. It's gotta be. We're still a bit far out though.\" ");
        yield return new WaitForSeconds(5);
        writeMoreText("Jim: \"What do you think the best move is?\" ");
        yield return new WaitForSeconds(5);
        Show2Options("Confirm it's a bear, get a bit closer", "Go back and then straight to the bear");
    }

    IEnumerator gameText4()
    {
        yield return new WaitForSeconds(2);
        writeNewText("You quickly move to clearer area closer to where you think the bear is. \n ");
        yield return new WaitForSeconds(5);
        writeMoreText("You're now very close. You stop to see if you can get a clear view. And then... \n ");
        yield return new WaitForSeconds(5);
        writeMoreText("You see it. \n ");
        yield return new WaitForSeconds(3);
        writeMoreText("Jim: \" There it is. My god...\"\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("Jim: \"We could hit it from there I reckon\"\n ");
        yield return new WaitForSeconds(3);


        Show2Options("Take shot from here", "Move closer");
    }

    IEnumerator gameLongShot()
    {
        yield return new WaitForSeconds(2);
        writeNewText("Both you and Jim break out your rifles. You find a rock to lean on and start eyeing up the target. \n ");
        yield return new WaitForSeconds(5);
        writeMoreText("You can see it clearly, a good line of sight even if it is far out. \n ");
        yield return new WaitForSeconds(5);

        if (friendshipStrength > 49)
        {
            writeMoreText("Jim: \" You've got this right? \"\n ");
            yield return new WaitForSeconds(3);
            if (confidence > 49)
            {
                writeMoreText(" You nod. \n ");
                yield return new WaitForSeconds(3);
                writeMoreText("Jim: \" Okay, you got this.\"\n ");
                yield return new WaitForSeconds(3);
            } else
            {
                writeMoreText(" You pause for a second, then ignore Jim \n ");
                yield return new WaitForSeconds(3);
                writeMoreText("Jim: \" Please tell me you've got this.\"\n ");
                yield return new WaitForSeconds(3);
                writeMoreText("You pause again, then nod\n ");
                yield return new WaitForSeconds(3);
                writeMoreText("Jim: \" Oh for god's sake.\"\n ");
                yield return new WaitForSeconds(3);
            }
        } else
        {
            writeMoreText("Jim: \" You taking this one?... You sure? \"\n ");
            yield return new WaitForSeconds(3);
            if (confidence > 49)
            {
                writeMoreText(" You nod. \n ");
                yield return new WaitForSeconds(3);
                writeMoreText("Jim: \" Okay cowboy, just don't ruin this...\"\n ");
                yield return new WaitForSeconds(3);
            }
            else
            {
                writeMoreText(" You pause for a second, then ignore Jim \n ");
                yield return new WaitForSeconds(4);
                writeMoreText("Jim: \" Please tell me you've got this.\"\n ");
                yield return new WaitForSeconds(4);
                writeMoreText("You ignore Jim again\n ");
                yield return new WaitForSeconds(4);
                writeMoreText("Jim: \" Oh for god's sake.\"\n ");
                yield return new WaitForSeconds(4);
            }
        }
        writeMoreText("Jim: \"well, here goes nothing\"\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("You line up to take the shot.\n ");
        yield return new WaitForSeconds(3);

        Show4Options("Aim for headshot (low chance of hitting, but will kill if hit)", "Aim for body shot (more likely to hit, but less likely to kill)", "Let Jim take the shot", "Get up and move closer");
    }

    IEnumerator longShotforHead()
    {
        yield return new WaitForSeconds(2);
        writeNewText("You take aim on the head.\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("You aim...\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("You shoot...\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("...\n ");
        if (didYouHit(chanceOfKill) == true)
        {
            yield return new WaitForSeconds(3);
            writeMoreText("Hit!\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("The bear drops dead immediately\n ");
            yield return new WaitForSeconds(3);
            writeMoreText("Jim screams in excitement and jumps up\n ");
            gameOver = true;
            gameEnding = 2;
        } else
        {
            bearAlerted = true;
            yield return new WaitForSeconds(3);
            writeMoreText("You miss.\n ");
            yield return new WaitForSeconds(3);
            writeMoreText("The bear is alerted and hurries away.\n ");
            yield return new WaitForSeconds(3);
            if (friendshipStrength > 49)
            {
                writeMoreText("Jim: \"ah, unlucky\"\n ");
                yield return new WaitForSeconds(3);
                writeMoreText("Jim: \"It's gone north though, can't be moving too fast, we could catch him I reckon. What you thinking?\"\n ");
                yield return new WaitForSeconds(3);
                Show2Options("Move closer", "Call it a day and move on");
            } else
            {
                confidence = confidence - 5;
                writeMoreText("Jim: \"ah, fuck\"\n ");
                yield return new WaitForSeconds(3);
                writeMoreText("Jim: \"Now look, could be hours before we find it again.\"\n ");
                yield return new WaitForSeconds(3);
                writeMoreText("Jim: \"Terrible shot...\"\n ");
                yield return new WaitForSeconds(3);
                Show2Options("Move closer, keep hunting", "Call it a day and move on");
            }
        }
    }

    IEnumerator longShotforBody()
    {
        yield return new WaitForSeconds(3);
        writeNewText("You take aim on the body.\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("You aim...\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("You shoot...\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("...\n ");
        if (didYouHit(chanceOfHit) == true)
        {
            confidence = confidence + 15;
            yield return new WaitForSeconds(3);
            writeMoreText("Hit!\n ");
            yield return new WaitForSeconds(3);
            writeMoreText("The bear screams in pain and runs into the forest\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("Jim jumps up in excitement.\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("Jim: \"It's gone north though, won't make it too far that fast, we have to follow it, now\"\n ");
            yield return new WaitForSeconds(5);
            writeMoreText("You both jump up and pack your stuff\n ");
            bearInjured = true;
            Option3();
        }
        else
        {
            bearAlerted = true;
            yield return new WaitForSeconds(3);
            writeMoreText("You miss.\n ");
            yield return new WaitForSeconds(3);
            writeMoreText("The bear is alerted and hurries away.\n ");
            yield return new WaitForSeconds(4);
            if (friendshipStrength > 49)
            {
                writeMoreText("Jim: \"ah, unlucky\"\n ");
                yield return new WaitForSeconds(3);
                writeMoreText("Jim: \"It's gone north though, can't be moving too fast, we could catch him I reckon. What you thinking?\"\n ");
                yield return new WaitForSeconds(5);
                Show2Options("Move closer", "Call it a day and move on");
            }
            else
            {
                writeMoreText("Jim: \"ah, fuck\"\n ");
                yield return new WaitForSeconds(3);
                writeMoreText("Jim: \"Now look, could be hours before we find it again.\"\n ");
                yield return new WaitForSeconds(4);
                writeMoreText("Jim: \"Terrible shot...\"\n ");
                yield return new WaitForSeconds(4);
                Show2Options("Move closer, keep hunting", "Call it a day and move on");
            }
        }
    }

    IEnumerator jimShoots()
    {
        yield return new WaitForSeconds(3);
        writeNewText("Jim: \"Ah, cheers.\"\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("He takes aim...\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("He shoots...\n ");
        audioSource.Play();
        yield return new WaitForSeconds(3);
        writeMoreText("...\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("He misses. \n ");
        yield return new WaitForSeconds(3);
        writeMoreText("The bear is alerted and hurries away.\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("You console Jim briefly, before planning the next move.\n ");
        yield return new WaitForSeconds(4);
        Show2Options("Move closer, keep hunting", "Call it a day and move on");
    }

    IEnumerator followingBear()
    { 
        writeNewText("You quickly move through the forest in search of the bear. You must be close\n ");
        yield return new WaitForSeconds(4);
        if (bearInjured == true)
        {
            writeMoreText("You find traces of blood, clearly the bear is hurt\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("You follow this for a while, until...\n ");
            yield return new WaitForSeconds(3);
        } else
        {
            writeMoreText("You find footprints leading north, deep prints, must be a big one.\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("You follow this for a while, until...\n ");
            yield return new WaitForSeconds(4);
        }

        writeMoreText("You see it\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("You're in a less dense patch of woods now, you see it's huge frame clearly.\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("Jim: \"I reckon we could hit it from here, might be tricky though\"\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("Jim: \"Any closer and we might be seen...\"\n ");
        yield return new WaitForSeconds(4);
        Show2Options("Move closer", "Take risky shot");

    }

    IEnumerator followingBear2()
    {
        writeNewText("You start slowly moving towards the bear. Get too much closer and it might see you\n ");
        yield return new WaitForSeconds(4);
        if (bearInjured == true)
        {
            writeMoreText("It's clearly injured, not mortally but is limping\n ");
            yield return new WaitForSeconds(4);
        }
        else
        {
            writeMoreText("It moves slowly, almost cautiously.\n ");
            yield return new WaitForSeconds(4);
        }

        writeMoreText("Jim: \"Still not a clean shot, but may be too risky to move closer\"\n ");
        yield return new WaitForSeconds(5);
        writeMoreText("Jim: \"I think we shoot from here.\"\n ");
        yield return new WaitForSeconds(4);
        Show2Options("Move closer", "Take shot");

    }

    IEnumerator followingBear3()
    {
        writeNewText("Even closer now, might even be able to hit with a stone if you threw it hard enough.\n ");
        yield return new WaitForSeconds(5);

        writeMoreText("Jim: \"Okay here is alright.\"\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("Jim: \"Please don't say you still want to move closer.\"\n ");
        yield return new WaitForSeconds(3);
        Show2Options("Move closer", "Take shot");

    }

    IEnumerator followingBear4()
    {
        writeNewText("You're really close now... possibly too close\n ");
        yield return new WaitForSeconds(3);

        writeMoreText("Jim: \"OKAY THIS IS GOOD!\"\n ");
        yield return new WaitForSeconds(2);
        writeMoreText("Jim: \"Do you want to die here? Take the shot!\"\n ");
        yield return new WaitForSeconds(4);
        Show2Options("Move closer", "Take the shot");

    }

    IEnumerator followingBear5()
    {
        writeNewText("You can hear its movement now. See its fur so clearly.\n ");
        yield return new WaitForSeconds(4);
        writeNewText("It's mesmerisingly close.\n ");
        yield return new WaitForSeconds(3);

        writeMoreText("Jim: \"okay stop now... this is too close\"\n ");
        yield return new WaitForSeconds(3);
        writeNewText("Jim is whispering now, not wanting to alert the bear\n ");
        yield return new WaitForSeconds(2);
        Show2Options("Move even closer", "Take the shot!");

    }

    IEnumerator closeShot1(){
        yield return new WaitForSeconds(4);
        writeNewText("You take position, hold steady.\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("You aim...\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("You shoot...\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("...\n ");
        if (didYouHit(chanceOfHit) == true)
        {
                yield return new WaitForSeconds(2);
                writeMoreText("Hit!\n ");
                yield return new WaitForSeconds(2);
                writeMoreText("The bear drops stumbles and falls, dying moments after.\n ");
                yield return new WaitForSeconds(5);
                writeMoreText("Jim screams in excitement and jumps up\n ");
                gameOver = true;
                gameEnding = 2;
        } else {

            //attack scene
            writeMoreText("You miss!\n ");
            yield return new WaitForSeconds(5);
            Option1();
        }
    }

    IEnumerator closeShot2(){
            yield return new WaitForSeconds(4);
            writeNewText("You take position, hold steady.\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("You're careful not to make too much movement\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("You take aim.\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("You shoot...\n ");
            if (didYouHit(chanceOfHit) == true)
            {
                    yield return new WaitForSeconds(3);
                    writeMoreText("Hit!\n ");
                    yield return new WaitForSeconds(4);
                    writeMoreText("The bear drops stumbles and falls. It stares at you momentarily, makes a move forward before collapsing.\n ");
                    yield return new WaitForSeconds(6);
                    writeMoreText("Jim screams in excitement and jumps up\n ");
                    gameOver = true;
                    gameEnding = 2;
            } else {
                //attack scene
                writeMoreText("You miss!\n ");
                yield return new WaitForSeconds(5);
                Option1();
            }
    }

    IEnumerator fightOrFlight()
        {
            writeNewText("The bear turns suddenly. You see it's eyes meet yours.\n ");
            yield return new WaitForSeconds(5);
            writeMoreText("Within a moment it launches towards you, gaining speed quickly.\n ");
            yield return new WaitForSeconds(5);
            writeMoreText("Jim screams\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("Your gun is still drawn and your knife is by your waist. Your only two options are to run or to fight.\n ");
            yield return new WaitForSeconds(6);
            writeMoreText("You don't have time to coordinate with Jim, you must act.\n ");
            yield return new WaitForSeconds(6);
            Show2Options("Run! With or without Jim", "Fight! Together you might stand a chance.");

        }

    IEnumerator runAlone()
        {
            writeNewText("You choose to run.\n ");
            yield return new WaitForSeconds(4);
            writeMoreText("You turn and start sprinting into the forest, you turn to check if Jim is with you.\n ");
            yield return new WaitForSeconds(5);
            writeMoreText("He is not.\n ");
            yield return new WaitForSeconds(3);
            writeMoreText("You hear him scream at the bear and a gunshot fly into the distance.\n ");
            yield return new WaitForSeconds(5);
            writeMoreText("It could be too late for Jim, but if you leave now he'll never make it on his own.\n ");
            yield return new WaitForSeconds(6);
            Show2Options("Go back to help Jim", "Run and save yourself.");

        }

    IEnumerator runAlone2()
    {
        writeNewText("You choose to save yourself.\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("You continue running through the woods. Running more from Jims screams than the bear.\n ");
        yield return new WaitForSeconds(6);
        writeMoreText("You left your friend...\n ");
        yield return new WaitForSeconds(5);
    }

    IEnumerator runTogetherDie()
    {
        writeNewText("You choose to run.\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("You turn and start sprinting into the forest, you turn to check if Jim is with you.\n ");
        yield return new WaitForSeconds(5);
        writeMoreText("He is.\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("The bear is gaining on you both.\n ");
        yield return new WaitForSeconds(5);
        writeMoreText("You went too close.\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("Within a flash, Jim is down and the bear is on him.");
        yield return new WaitForSeconds(5);
        writeMoreText("You hear him scream.");
        yield return new WaitForSeconds(3);
        writeMoreText("You continue to run, but with your bags you are simply not fast enough.");
        yield return new WaitForSeconds(6);
        writeMoreText("Before you know it, the bear is on you as well..");
        yield return new WaitForSeconds(4);
        inText = false;
    }

    IEnumerator runTogetherSurvive()
    {
        writeNewText("You choose to run.\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("You turn and start sprinting into the forest, you turn to check if Jim is with you.\n ");
        yield return new WaitForSeconds(5);
        writeMoreText("He is.\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("You have a significant headstart on the bear.\n ");
        yield return new WaitForSeconds(5);
        writeMoreText("You run through thick and thin, only looking back to make sure Jim is still with you. \n ");
        yield return new WaitForSeconds(6);
        writeMoreText("You make it to the far side of a clearing and take a quick look behind you.");
        yield return new WaitForSeconds(6);
        writeMoreText("The bear is nowhere to be seen.");
        yield return new WaitForSeconds(5);
        writeMoreText("You sigh in relief, you've survived.");
        yield return new WaitForSeconds(4);
        writeMoreText("You look to Jim, he looks to you, you share a brief chuckle and begin the walk home.");
        yield return new WaitForSeconds(5);
        inText = false;
    }


    IEnumerator fightTogether()
        {
            writeNewText("The bear turns lurches forward. You and Jim stand your ground\n ");
            yield return new WaitForSeconds(5);
            writeMoreText("Your knife by your side, your gun in your hand.\n ");
            yield return new WaitForSeconds(5);
            writeMoreText("You must act fast.\n ");
            Show2Options("Go for knife", "Take the shot.");

        }


    IEnumerator bearOnYou()
        {
            writeNewText("The bear jumps on you.\n ");
            yield return new WaitForSeconds(3);
            writeMoreText("It stands for a moment, towering over you.\n ");
            yield return new WaitForSeconds(3);
            writeMoreText("This surely means death, in the last moment you turn to look at Jim.\n ");
            yield return new WaitForSeconds(4);
            if (withOrWithoutJim() == true) {
                writeMoreText("His gun aimed straight at the bears head.\n ");
                yield return new WaitForSeconds(4);
                writeMoreText("He shoots...\n ");
                yield return new WaitForSeconds(3);
                writeMoreText("The bear drops dead, nearly crushing you on impact. Jim pulls you out and you stand over the bear to admire it.\n ");
                yield return new WaitForSeconds(6);
                Option3();
            } else {
                writeMoreText("He is nowhere to be seen.\n ");
                yield return new WaitForSeconds(3);
                writeMoreText("He saved himself.\n ");
                yield return new WaitForSeconds(2);
                writeMoreText("Your last thought is on your bad choices of friends.\n ");
                yield return new WaitForSeconds(4);
                Option4();
            }

        }

    IEnumerator stabBear()
    {
        writeNewText("You go for your knife.\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("As the bear lunges at you, you swing the knife and stab it in the shoulder.\n ");
        yield return new WaitForSeconds(6);
        writeMoreText("It howls in pain and charges you down, knocking you to the floor and then towering over you.\n ");
        yield return new WaitForSeconds(6);
    }

    IEnumerator hitBear()
    {
        writeNewText("You go for your gun.\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("You go for one final shot...\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("You hit.\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("The bear flops over, certainly dead.\n ");
        yield return new WaitForSeconds(2);
        writeMoreText("You breathe a sigh of relief, it's all over now\n ");
        yield return new WaitForSeconds(5);
    }

    IEnumerator missBear()
    {
        writeNewText("You go for your gun.\n ");
        yield return new WaitForSeconds(3);
        writeMoreText("You go for one final shot...\n ");
        yield return new WaitForSeconds(4);
        writeMoreText("You miss.\n ");
        yield return new WaitForSeconds(2);
        writeMoreText("It howls in pain and charges you down, knocking you to the floor and then towering over you.\n ");
        yield return new WaitForSeconds(5);
    }

    private bool didYouHit(int chance)
    {
        audioSource.Play();
        int randomPick = Random.Range(0, 99);
        int chancePlusConfidence = chance + confidence - 50; //balancing factor
        if (randomPick < chancePlusConfidence)
        {
            return true;
        } else
        {
            return false;
        }
    }

   public bool withOrWithoutJim(){
       int randomPick = Random.Range(0, 99);
        if (randomPick < friendshipStrength)
        {
            return true;
        } else
        {
            return false;
        }
   }


    public void Option1()
    {
        HideButtons();
        storyEngine(storySection, 1);
        clearText();
    }

    public void Option2()
    {
        HideButtons();
        storyEngine(storySection, 2);
        clearText();
    }

    public void Option3()
    {
        HideButtons();
        storyEngine(storySection, 3);
        clearText();
    }

    public void Option4()
    {
        HideButtons();
        storyEngine(storySection, 4);
        clearText();
    }

    public void Show4Options(string option1, string option2, string option3, string option4)
    {
        image1.SetActive(true);
        image2.SetActive(true);
        image3.SetActive(true);
        image4.SetActive(true);

        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        button4.SetActive(true);

        text1.text = option1;
        text2.text = option2;
        text3.text = option3;
        text4.text = option4;

    }

    public void Show2Options(string option1, string option2)
    {
        image3.SetActive(true);
        image4.SetActive(true);
        button3.SetActive(true);
        button4.SetActive(true);
        text3.text = option1;
        text4.text = option2;
    }

    void HideButtons()
    {
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(false);

        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);
    }

    private void writeNewText(string text)
    {
        textfield.text = text;
    }

    private void writeMoreText(string text)
    {
        textfield.text = textfield.text + text;
    }

    private void clearText()
    {
        textfield.text = "";
    }


}
