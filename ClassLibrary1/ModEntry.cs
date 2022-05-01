using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Tools;
using StardewValley.Objects;
using StardewValley.Locations;
using StardewValley.Network;

namespace YourProjectName
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.GameLoop.DayStarted += this.DebugDay;
            helper.Events.GameLoop.TimeChanged += this.MaintainBuffs;
        }
        private bool isTeleporting { get; set; } = false;

        private readonly Tuple<string, Point> SecretWoodsTuple = new Tuple<string, Point>("Woods", new Point(58, 15));
        private readonly Tuple<string, Point> MinesTouple = new Tuple<string, Point>("Mine", new Point(13, 10));
        private readonly Tuple<string, Point> BlacksmithTouple = new Tuple<string, Point>("Town", new Point(94, 82));
        private readonly Tuple<string, Point> RobinTouple = new Tuple<string, Point>("Mountain", new Point(12, 26));
        private readonly Tuple<string, Point> LavaTouple = new Tuple<string, Point>("VolcanoDungeon5", new Point(36, 32));
        private readonly Tuple<string, Point> SkullTouple = new Tuple<string, Point>("UndergroundMine121", new Point(0, 0));
        private readonly Tuple<string, Point> QuarryTouple = new Tuple<string, Point>("Mountain", new Point(127, 12));
        private readonly Tuple<string, Point> DockTouple = new Tuple<string, Point>("Mountain", new Point(127, 12));
        private readonly Tuple<string, Point> SewerTouple = new Tuple<string, Point>("Sewer", new Point(16, 21));
        private readonly Tuple<string, Point> IslandFarmTouple = new Tuple<string, Point>("IslandWest", new Point(77, 41));
        private readonly Tuple<string, Point> TownCenterTouple = new Tuple<string, Point>("Town", new Point(52, 20));

        private FakeWand fwand = new FakeWand();

        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;
            //Press secondary button and you will teleport
            if (isTeleporting)
            {
                isTeleporting = false;
                if (e.Button.ToString().Contains("DPadLeft"))
                {
                    fwand.DoFunction2(Game1.player.currentLocation, Game1.player.getTileLocationPoint().X, Game1.player.getTileLocationPoint().Y, Game1.player, SecretWoodsTuple);
                }
                if (e.Button.ToString().Contains("DPadDown"))
                {
                    fwand.DoFunction2(Game1.player.currentLocation, Game1.player.getTileLocationPoint().X, Game1.player.getTileLocationPoint().Y, Game1.player, MinesTouple);
                }
                if (e.Button.ToString().Contains("DPadRight"))
                {
                    fwand.DoFunction2(Game1.player.currentLocation, Game1.player.getTileLocationPoint().X, Game1.player.getTileLocationPoint().Y, Game1.player, BlacksmithTouple);
                }
                if (e.Button.ToString().Contains("DPadUp"))
                {
                    fwand.DoFunction2(Game1.player.currentLocation, Game1.player.getTileLocationPoint().X, Game1.player.getTileLocationPoint().Y, Game1.player, RobinTouple);
                }
                if (e.Button.ToString().Contains("ControllerY"))
                {
                    fwand.DoFunction2(Game1.player.currentLocation, Game1.player.getTileLocationPoint().X, Game1.player.getTileLocationPoint().Y, Game1.player, LavaTouple);
                }
                if (e.Button.ToString().Contains("LeftShoulder"))
                {
                    fwand.DoFunction2(Game1.player.currentLocation, Game1.player.getTileLocationPoint().X, Game1.player.getTileLocationPoint().Y, Game1.player, SkullTouple);
                }
                if (e.Button.ToString().Contains("RightShoulder"))
                {
                    fwand.DoFunction2(Game1.player.currentLocation, Game1.player.getTileLocationPoint().X, Game1.player.getTileLocationPoint().Y, Game1.player, QuarryTouple);
                }
                if (e.Button.ToString().Contains("ControllerA"))
                {
                    fwand.DoFunction2(Game1.player.currentLocation, Game1.player.getTileLocationPoint().X, Game1.player.getTileLocationPoint().Y, Game1.player, DockTouple);
                }
                if (e.Button.ToString().Contains("ControllerB"))
                {
                    fwand.DoFunction2(Game1.player.currentLocation, Game1.player.getTileLocationPoint().X, Game1.player.getTileLocationPoint().Y, Game1.player, SewerTouple);
                }
                if (e.Button.ToString().Contains("ControllerX"))
                {
                    fwand.DoFunction2(Game1.player.currentLocation, Game1.player.getTileLocationPoint().X, Game1.player.getTileLocationPoint().Y, Game1.player, IslandFarmTouple);
                }
                if (e.Button.ToString().Contains("RightTrigger"))
                {
                    fwand.DoFunction2(Game1.player.currentLocation, Game1.player.getTileLocationPoint().X, Game1.player.getTileLocationPoint().Y, Game1.player, TownCenterTouple);
                }
            }
            // print button presses to the console window
            //this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
            if (e.Button.ToString().Contains("LeftStick")){
                this.isTeleporting = true;
            }
            if (e.Button.ToString().Contains("OemCloseBrackets"))
            {
                //Making a combination of all rings that exist, twice
                CombinedRing cr = new CombinedRing(880);
                CombinedRing cr2 = new CombinedRing(880);


                //Combine all rings, the index is the item ID
                for (int i = 517; i < 534; i++)
                {
                    //If it is the wedding ring just forget it
                    if (i == 528) continue;
                    else
                    {
                        cr.combinedRings.Add(new Ring(i));
                        cr2.combinedRings.Add(new Ring(i));

                    }
                }
                //Now for all the new rings added in the island update and beyond
                for (int i = 800; i < 888; i++)
                {
                    switch (i)
                    {
                        case 888:
                        case 887:
                        case 863:
                        case 862:
                        case 861:
                        case 860:
                        case 859:
                        case 839:
                        case 811:
                        case 810: cr.combinedRings.Add(new Ring(i)); cr2.combinedRings.Add(new Ring(i)); break;
                        default: continue;
                    }
                }

                SwiftToolEnchantment se = new SwiftToolEnchantment();
                //GenerousEnchantment ge = new GenerousEnchantment();

                se.Level = 5000;
                //ge.Level = 5000;

                Pickaxe pic = new Pickaxe();
                pic.UpgradeLevel = 4;
                pic.AddEnchantment(se);

                Axe axe = new Axe();
                axe.UpgradeLevel = 4;
                axe.AddEnchantment(se);

                Hoe hoehoehoe = new Hoe();
                hoehoehoe.UpgradeLevel = 4;
                hoehoehoe.AddEnchantment(se);

                WateringCan wc = new WateringCan();
                wc.UpgradeLevel = 4;
                wc.AddEnchantment(se);

                MeleeWeapon mw = new MeleeWeapon(62);

                RubyEnchantment re = new RubyEnchantment();
                mw.AddEnchantment(re);
                mw.AddEnchantment(re);
                mw.AddEnchantment(re);
                mw.AddEnchantment(re);
                mw.AddEnchantment(re);

                Game1.player.clearBackpack();
                Game1.player.team.sharedDailyLuck.Set(10);
                Game1.player.addItemToInventoryBool(new Wand(), false);
                Game1.player.addItemToInventoryBool(pic, false);
                Game1.player.addItemToInventoryBool(mw, false);
                Game1.player.addItemToInventoryBool(axe, false);
                Game1.player.addItemToInventoryBool(hoehoehoe, false);
                Game1.player.addItemToInventoryBool(wc, false);
                Game1.player.addItemToInventoryBool(cr, false);
                Game1.player.addItemToInventoryBool(cr2, false);
            }
        }
        private void DebugDay(object sender, DayStartedEventArgs e)
        {
            MineShaft.mushroomLevelsGeneratedToday = new System.Collections.Generic.HashSet<int>();
            int[] buffs = { 10000, 10000, 10000, 0, 10000, 10000, 0, 0, 0, 7, 10000, 10000 };
            Game1.player.addBuffAttributes(buffs);
            //Game1.weatherForTomorrow = Game1.weather_lightning;
            Game1.player.team.sharedDailyLuck.Set(10);
        }
        private void MaintainBuffs(object sender, TimeChangedEventArgs e)
        {
            MineShaft.mushroomLevelsGeneratedToday = new System.Collections.Generic.HashSet<int>();
            int[] buffs = { 10000, 10000, 10000, 0, 10000, 10000, 10000, 0, 0, 7, 10000, 10000 };
            Game1.player.addBuffAttributes(buffs);
            Game1.player.team.sharedDailyLuck.Set(10);
        }
    }

    public class FakeWand : Wand
    {
        private Tuple<string, Point> namePlusCoords { get; set; }
        private void wandWarpForRealSpecific()
        {
            FarmHouse home = Utility.getHomeOfFarmer(Game1.player);
            if (home != null)
            {
                //Point position = home.getFrontDoorSpot();
                Game1.warpFarmer(this.namePlusCoords.Item1, this.namePlusCoords.Item2.X, this.namePlusCoords.Item2.Y, flip: false);
                if (!Game1.isStartingToGetDarkOut() && !Game1.isRaining)
                {
                    Game1.playMorningSong();
                }
                else
                {
                    Game1.changeMusicTrack("none");
                }
                Game1.fadeToBlackAlpha = 0.99f;
                Game1.screenGlow = false;
                Game1.player.temporarilyInvincible = false;
                Game1.player.temporaryInvincibilityTimer = 0;
                Game1.displayFarmer = true;
                //base.lastUser.CanMove = true;
            }
        }
        public  void DoFunction2(GameLocation location, int x, int y, Farmer who, Tuple<string, Point> namePlusCoordsPassed, int power = 1)
        {
            multiplayer2 spriteBroadcastTest = new multiplayer2();
            this.namePlusCoords = namePlusCoordsPassed;
            if (!who.bathingClothes && who.IsLocalPlayer && !who.onBridge.Value)
            {
                base.indexOfMenuItemView.Value = 2;
                base.CurrentParentTileIndex = 2;

                for (int i = 0; i < 12; i++)
                {
                    spriteBroadcastTest.broadcastSprites(who.currentLocation, new TemporaryAnimatedSprite(354, Game1.random.Next(25, 75), 6, 1, new Vector2(Game1.random.Next((int)who.position.X - 256, (int)who.position.X + 192), Game1.random.Next((int)who.position.Y - 256, (int)who.position.Y + 192)), flicker: false, Game1.random.NextDouble() < 0.5));
                }

                location.playSound("wand");
                Game1.displayFarmer = false;
                who.temporarilyInvincible = true;
                who.temporaryInvincibilityTimer = -2000;
                who.Halt();
                who.faceDirection(2);
                who.CanMove = false;
                who.freezePause = 2000;
                Game1.flashAlpha = 1f;
                DelayedAction.fadeAfterDelay(wandWarpForRealSpecific, 1000);
                
                new Rectangle(who.GetBoundingBox().X, who.GetBoundingBox().Y, 64, 64).Inflate(192, 192);
                int j = 0;
                for (int xTile = who.getTileX() + 8; xTile >= who.getTileX() - 8; xTile--)
                {
                    spriteBroadcastTest.broadcastSprites(who.currentLocation, new TemporaryAnimatedSprite(6, new Vector2(xTile, who.getTileY()) * 64f, Color.White, 8, flipped: false, 50f)
                    {
                        layerDepth = 1f,
                        delayBeforeAnimationStart = j * 25,
                        motion = new Vector2(-0.25f, 0f)
                    });
                    j++;
                }
                base.CurrentParentTileIndex = base.IndexOfMenuItemView;
            }
        }
        public class multiplayer2 : Multiplayer { };
    }
}