using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueHeroSimulator
{
    internal enum GamePhase
    {
        //You'll have to try harder than THAT
        InitialDialogue,
        //Arrows
        ArrowAttack0,
        ArrowAttack1,
        ArrowAttack2,
        //Spears
        SpearAttack0, //Targeting spears
        SpearAttack1, //Spears from bottom
        //Arrows
        ArrowAttack3,
        ArrowAttack4, //Speedness
        ArrowAttack5, //LEFT-TOP w/ Inverted arrows
        //Spears
        SpearAttack2, //Spear circle
        SpearAttack3, //Same
        //Arrows
        ArrowAttack6, //Starting w/ inverted
        ArrowAttack7, //L-R-BB | L-R-TT
        ArrowAttack8, //Inverted from same spot, changing to opposite
        ArrowAttack9, //Slowness
        //Spears
        SpearAttack4, //Spear circle not moving
        SpearAttack5, //Spear circle
        SpearAttack6, //Targeting spears
        
        LastDialogue
    }
}
