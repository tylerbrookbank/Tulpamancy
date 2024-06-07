using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Globals
{
    // item IDs
    public const int NONE = -1;
    public const int WEIRD_BOOK = 0;
    public const int UPSIDEDOWN_BOOK = 1;
    public const int CUBE_BOOK = 2;
    public const int SHOT_GLASS = 3;
    public const int FRIDGE = 4;
    public const int TEQUILLA = 5;
    public const int BEER = 6;
    public const int TEQUILLA_SHOT = 7;
    public const int CHARACTER = 1000;

    // layers
    public const int INTEREST = 6;
    public const int ITEM = 7;
    public const int WORLDITEM = 8;
    public const int TRANSISTION = 9;

}

public enum ScreenID
{
    Bedroom,
    BedroomTable,
    Kitchen,
    KitchenFridge,
    ApartmentHallway,
    ApartmentFrontDoor
}

public enum TransitionDirection
{
    Forward,
    Left,
    Right,
    Backward
}

