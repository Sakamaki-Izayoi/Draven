using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using LeagueSharp.Common.Data;
using SharpDX;
using Color = System.Drawing.Color;

namespace Draven
{
    class LuomRiu
    {
        private static Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        public static void LuomRiuTest()
        {
            float y = 300;
            var Qbuff = Player.Buffs.Find(b => b.Name.ToLower()=="dravenspinning");
            if (Program.RiuNo1 != null && Player.Distance(Program.RiuNo1.Position) > 70)
            {
                if (Game.CursorPos.Distance(Program.RiuNo1.Position) - Game.CursorPos.Distance(Player.Position) > y)
                {         
                    foreach (var riu in Program.Riu) { if (riu.NetworkId == Program.RiuNo1.NetworkId) { Program.Riu.Remove(riu); } }
                    Program.Orbwalker.SetOrbwalkingPoint(Game.CursorPos);
                    Program.Orbwalker.SetMovement(true);
                    Program.Orbwalker.SetAttack(true);   
                }
                else if (Player.Distance(Program.RiuNo1.Position)>=120 && Player.Distance(Program.RiuNo1.Position) - 50 > Player.MoveSpeed * (0 - Environment.TickCount + Program.RiuNo1.CreationTime + 1250) / 1000)
                {
                    foreach (var riu in Program.Riu) { if (riu.NetworkId == Program.RiuNo1.NetworkId) { Program.Riu.Remove(riu); } }
                    Program.Orbwalker.SetOrbwalkingPoint(Game.CursorPos);
                    Program.Orbwalker.SetMovement(true);
                    Program.Orbwalker.SetAttack(true); 
                }
                else
                {
                    Program.Orbwalker.SetOrbwalkingPoint(Program.RiuNo1.Position);
                    if (Qbuff == null)
                    {
                        if (Player.Distance(Program.RiuNo1.Position)+ 100 < Player.MoveSpeed * (0 - Environment.TickCount + Program.RiuNo1.CreationTime + 1250) / 1000)
                        { Program.Orbwalker.SetAttack(true); Program.Orbwalker.SetMovement(true); }
                        else { Program.Orbwalker.SetAttack(false); Program.Orbwalker.SetMovement(true); }
                    }
                    else
                    {
                        float a = Player.Distance(Game.CursorPos);
                        float b = Program.RiuNo1.Position.Distance(Game.CursorPos);
                        float c = Player.Distance(Program.RiuNo1.Position);
                        float B = (a * a + c * c - b * b) / (2 * a * c);
                        double d = Math.Acos(B)*(180/Math.PI);
                        if (d <= 45 && Qbuff != null)
                        {
                            if (Player.Distance(Program.RiuNo1.Position) + 100 < Player.MoveSpeed * (0 - Environment.TickCount + Program.RiuNo1.CreationTime + 1250) / 1000)
                            { Program.Orbwalker.SetAttack(true); Program.Orbwalker.SetMovement(true); }
                            else { Program.Orbwalker.SetAttack(false); Program.Orbwalker.SetMovement(true); }
                        }
                        else { Program.Orbwalker.SetAttack(false); Program.Orbwalker.SetMovement(true); }
                    }
                }
            }
            else if (Program.RiuNo1 != null && Player.Distance(Program.RiuNo1.Position) <= 70)
            {
                Player.IssueOrder(GameObjectOrder.HoldPosition, null);
                Program.Orbwalker.SetOrbwalkingPoint(Program.RiuNo1.Position);
                Program.Orbwalker.SetMovement(false);
                if (Qbuff == null) { Program.Orbwalker.SetAttack(true); }
                else if (Qbuff != null && Environment.TickCount - Program.RiuNo1.CreationTime < 1000) { Program.Orbwalker.SetAttack(true); }
                else { Program.Orbwalker.SetAttack(false); }
            }
            else { Program.Orbwalker.SetOrbwalkingPoint(Game.CursorPos); Program.Orbwalker.SetAttack(true); Program.Orbwalker.SetMovement(true); }
        }
    }
}
