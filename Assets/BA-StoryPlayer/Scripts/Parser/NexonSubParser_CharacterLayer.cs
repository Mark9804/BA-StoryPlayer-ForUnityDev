namespace BAStoryPlayer.NexonScriptParser
{
    /*
     处理角色操作以及文本对话部分
    脚本:character,na,hide
     */
    public class NexonSubParser_CharacterLayer : BNexonSubParser
    {
        public NexonSubParser_CharacterLayer(int weight) { this.weight = weight; }

        public override StoryUnit Parse(RawNexonStoryUnit rawStoryUnit, StoryUnit storyUnit = null)
        {
            if (storyUnit == null)
                storyUnit = new StoryUnit();

            // 防止多人说话
            bool isOccupied = false;

            for (int i = 0; i <rawStoryUnit.scriptList.Count; i++)
            {
                string[] args = rawStoryUnit.scriptList[i].script.Split(';');

                switch (rawStoryUnit.scriptList[i].tag)
                {
                    case ScriptTag.Character:
                            // 注意下标减1
                            if (args.Length == 3) // 无台词
                                storyUnit.action += () => {  
                                    BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.ActivateCharacter(int.Parse(args[0]) - 1, args[1], args[2]);
                                };
                            else if (args.Length == 4) // 有台词
                            {
                                if (isOccupied) continue;
                                storyUnit.UpdateType(weight, UnitType.Text);
                                storyUnit.action += () => { 
                                    BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.ActivateCharacter(int.Parse(args[0]) - 1, args[1], args[2], args[3]);
                                };
                                isOccupied = true;
                            }
                            break;
                    case ScriptTag.CharacterEffect:
                            switch (args[1])
                            {
                                case "em":
                                        HandleEmotion(storyUnit,int.Parse(args[0][1].ToString()) - 1, args[2]);
                                        break;
                                case "fx":
                                        // TODO
                                        break;
                                default:
                                        HandleAction(storyUnit, int.Parse(args[0][1].ToString()) - 1, args[1]);
                                        break;
                            }
                            break;
                    case ScriptTag.Na:
                            if (isOccupied) continue;
                            storyUnit.action += () => { 
                                BAStoryPlayerController.Instance.StoryPlayer.UIModule.SetSpeaker(); 
                                BAStoryPlayerController.Instance.StoryPlayer.UIModule.PrintText(args[1]); 
                            };
                            isOccupied = true;
                            storyUnit.UpdateType(weight, UnitType.Text);
                            break;
                    case ScriptTag.All:
                            switch (args[1])
                            {
                                case "hide":
                                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.HideAll(); };
                                        break;
                                default:break;
                            }
                            break;
                    default: continue;
                }
            }

            return nextParser == null ? storyUnit : nextParser.Parse(rawStoryUnit, storyUnit);
        }


        void HandleEmotion(StoryUnit storyUnit,int characterIndex, string emotionName)
        {
            switch (emotionName)
            {
                case "h":
                case "heart":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Heart); };
                        break;
                case "respond":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Respond); };
                        break;
                case "m":
                case "music":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Music); };
                        break;
                case "k":
                case "twinkle":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Twinkle); };
                        break;
                case "u":
                case "upset":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Upset); };
                        break;
                case "w":
                case "sweat":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Sweat); };
                        break;
                case "[...]":
                case "...":
                case "dot":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Dot); };
                        break;
                case "c":
                case "chat":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Chat); };
                        break;
                case "[!]":
                case "!":
                case "exclaim":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Exclaim); };
                        break;
                case "[?!]":
                case "?!":
                case "[!?]":
                case "!?":
                case "surprise":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Surprise); };
                        break;
                case "[?]":
                case "?":
                case "question":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Question); };
                        break;
                case "[///]":
                case "///":
                case "shy":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Shy); };
                        break;
                case "a":
                case "angry":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Angry); };
                        break;
                case "steam":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Steam); };
                        break;
                case "sigh":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Sigh); };
                        break;
                case "sad":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Sad); };
                        break;
                case "bulb":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Bulb); };
                        break;
                case "zzz":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Zzz); };
                        break;
                case "tear":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Tear); };
                        break;
                case "think":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetEmotion(characterIndex, CharacterEmotion.Think); };
                        break;
                default: return;
            }
        }
        void HandleAction(StoryUnit storyUnit,int characterIndex,string actionName)
        {
            switch (actionName){
                case "a":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.Appear); };
                        break;
                case "d":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.Disapper); };
                        break;
                case "dl":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.Disapper2Left); };
                        break;
                case "dr":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.Disapper2Right); };
                        break;
                case "ar":// 注意实际-1
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.AppearL2R,characterIndex); };
                        break;
                case "al":// 注意实际-1
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.AppearR2L, characterIndex); };
                        break;
                case "hophop":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.Hophop); };
                        break;
                case "greeting":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.Greeting); };
                        break;
                case "shake":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.Shake); };
                        break;
                case "m1": // 注意实际-1
                case "m2":
                case "m3":
                case "m4":
                case "m5":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.Move, int.Parse(actionName[1].ToString())-1); };
                        break;
                case "stiff":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.Stiff); };
                        break;
                case "closeup":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.Close); };
                        break;
                case "jump":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.Jump); };
                        break;
                case "falldownR":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.falldownR); };
                        break;
                case "hide":
                        storyUnit.action += () => { BAStoryPlayerController.Instance.StoryPlayer.CharacterModule.SetAction(characterIndex, CharacterAction.Hide); };
                        break;
                default:return;
            }
        }
    }
}