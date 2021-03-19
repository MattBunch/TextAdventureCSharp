using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure
{
    class Action
    {
        string name;
        char hotkey;

        public Action(string name, char hotkey)
        {
            this.name = name;
            this.hotkey = hotkey;
        }

        public string ToString()
        {
            return (this.hotkey + " : " + this.name);
        }

        public char getHotkey()
        {
            return this.hotkey;
        }

    }

    class MoveNorth: Action
    {
        public MoveNorth() : base(
            "Move North",
            'n'
            )
        {

        }
    }

    class MoveSouth : Action
    {
        public MoveSouth() : base (
            "Move South", 
            's')
        {

        }
    }

    class MoveEast : Action
    {
        public MoveEast() : base(
            "Move East",
            'e')
        {

        }
    }


    class MoveWest : Action
    {
        public MoveWest() : base(
            "Move West",
            'w')
        {

        }
    }

    class ViewInventory : Action
    {
        public ViewInventory() : base (
            "View inventory",
            'i')
        {

        }

    }

    class DrinkHealthPotion : Action
    {
        public DrinkHealthPotion() : base (
            "Drink health potion",
            'h')
        {

        }
    }

    class Attack : Action
    {
        public Attack() : base(
            "attack",
            'a')
        {

        }
    }

    class Flee : Action
    {
        public Flee() : base (
            "Flee",
            'f')
        {

        }
    }

}
