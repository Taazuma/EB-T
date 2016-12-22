namespace RivenSharp
{
    using Eclipse;
    using static Eclipse.Menus;
    #region

    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    #endregion

    internal class MenuConfig : Core.Core
    {
        #region Public Properties
        
        #region Animation
        public static bool CancelPing => AniMenu["CancelPing"].Cast<CheckBox>().CurrentValue;
        public static string EmoteList
        {
            get
            {
                switch (AniMenu["EmoteList"].Cast<ComboBox>().CurrentValue)
                {
                    case 0:
                    {
                        return "Laugh";
                    }
                    break;

                    case 1:
                    {
                        return "Taunt";
                    }
                    break;

                    case 2:
                    {
                        return "Joke";
                    }
                    break;

                    default:
                    case 3:
                    {
                        return "Dance";
                    }
                    break;

                    case 4:
                    {
                        return "None";
                    }
                    break;
                }
            }
        }
        public static bool AnimDance => EmoteList == "Dance";
        public static bool AnimLaugh => EmoteList == "Laugh";
        public static bool AnimTalk => EmoteList == "Joke";
        public static bool AnimTaunt => EmoteList == "Taunt";
        #endregion
        
        #region Combo
        public static bool Q3Wall => ComboMenu["Q3Wall"].Cast<CheckBox>().CurrentValue;
        public static bool Flash => ComboMenu["FlashOften"].Cast<CheckBox>().CurrentValue;
        public static bool OverKillCheck => ComboMenu["OverKillCheck"].Cast<CheckBox>().CurrentValue;
        public static bool Doublecast => ComboMenu["Doublecast"].Cast<CheckBox>().CurrentValue;
        public static bool AlwaysF => ComboMenu["AlwaysF"].Cast<KeyBind>().CurrentValue;
        public static bool AlwaysR => ComboMenu["AlwaysR"].Cast<KeyBind>().CurrentValue;
        public static bool BurstEnabled => ComboMenu["BurstEnabled"].Cast<KeyBind>().CurrentValue;
        #endregion

        #region Lane
        public static bool LaneEnemy => LaneClearMenu["LaneEnemy"].Cast<CheckBox>().CurrentValue;
        public static bool LaneQFast => LaneClearMenu["laneQFast"].Cast<CheckBox>().CurrentValue;
        public static bool LaneQ => LaneClearMenu["LaneQ"].Cast<CheckBox>().CurrentValue;
        public static bool LaneW => LaneClearMenu["LaneW"].Cast<CheckBox>().CurrentValue;
        public static bool LaneE => LaneClearMenu["LaneE"].Cast<CheckBox>().CurrentValue;
        #endregion

        #region Jungle
        public static bool JnglQ => JungleClearMenu["JungleQ"].Cast<CheckBox>().CurrentValue;
        public static bool JnglW => JungleClearMenu["JungleW"].Cast<CheckBox>().CurrentValue;
        public static bool JnglE => JungleClearMenu["JungleE"].Cast<CheckBox>().CurrentValue;
        #endregion

        #region Killsteal
        public static bool Ignite => KillStealMenu["ignite"].Cast<CheckBox>().CurrentValue;
        public static bool KsW => KillStealMenu["ksW"].Cast<CheckBox>().CurrentValue;
        public static bool KsR2 => KillStealMenu["ksR2"].Cast<CheckBox>().CurrentValue;
        public static bool KsQ => KillStealMenu["ksQ"].Cast<CheckBox>().CurrentValue;
        #endregion

        #region Misc
        public static bool GapcloserMenu => MiscMenu["GapcloserMenu"].Cast<CheckBox>().CurrentValue;
        public static bool InterruptMenu => MiscMenu["InterruptMenu"].Cast<CheckBox>().CurrentValue;
        public static bool KeepQ => MiscMenu["KeepQ"].Cast<CheckBox>().CurrentValue;
        public static bool QMove => MiscMenu["QMove"].Cast<KeyBind>().CurrentValue;
        #endregion

        #region Draw
        public static bool FleeSpot => DrawingsMenu["FleeSpot"].Cast<CheckBox>().CurrentValue;
        public static bool Dind => DrawingsMenu["Dind"].Cast<CheckBox>().CurrentValue;
        public static bool ForceFlash => DrawingsMenu["DrawForceFlash"].Cast<CheckBox>().CurrentValue;
        public static bool DrawAlwaysR => DrawingsMenu["DrawAlwaysR"].Cast<CheckBox>().CurrentValue;
        public static bool DrawBurst => DrawingsMenu["DrawBurst"].Cast<CheckBox>().CurrentValue;
        public static bool DrawCb => DrawingsMenu["DrawCB"].Cast<CheckBox>().CurrentValue;
        public static bool DrawBt => DrawingsMenu["DrawBT"].Cast<CheckBox>().CurrentValue;
        #endregion

        #region Flee
        public static bool WallFlee => FleeMenu["WallFlee"].Cast<CheckBox>().CurrentValue;
        public static bool FleeYomuu => FleeMenu["FleeYoumuu"].Cast<CheckBox>().CurrentValue;
        #endregion
        
        #endregion
    }
}