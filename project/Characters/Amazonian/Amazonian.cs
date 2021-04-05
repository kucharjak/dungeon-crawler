using Godot;
using System;
using DungeonCrawler.Characters;
using DungeonCrawler.Controls;

public class Amazonian : Character
{
    public override void _Ready()
    {
        base._Ready();
        
        Controller = new SecondController();
    }
}
