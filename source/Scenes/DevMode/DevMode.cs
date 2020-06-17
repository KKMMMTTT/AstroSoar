
using Annex;
using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Game.Scenes.DevMode
{

    class DevMode : Scene
    {

        private readonly ButtonsBackground buttonBack;
        private readonly Placeholder placeholder;

        private readonly AddButton add;
        private readonly RemoveButton remove;
        private readonly MoveButton move;
        private readonly ModifyButton mod;
        private readonly BackButton back;

        private readonly List<Item> items;
        private readonly List<ItemBox> boxes;

        private int displayItemsFlag = 0;
        private int displayFocus = 0;
        private int itemCount = 0;
        private int moveItemsFlag = 0;
        
        private enum Flags { DisplayItems, DisplayBoxes, MoveItemsSelection, MoveItems, ModifyItems, DeleteItems};


        public DevMode()
        {

            this.items = new List<Item>();
            this.boxes = new List<ItemBox>();

            this.buttonBack = new ButtonsBackground();
            this.placeholder = new Placeholder();


            this.back = new BackButton()
            {
                OnClickHandler = Return,
                Visible = false
            };
            this.add = new AddButton()
            {
                OnClickHandler = AddItem
            };
            this.remove = new RemoveButton()
            {
                OnClickHandler = RemoveItem
            };
            this.move = new MoveButton()
            {
                OnClickHandler = MoveItem
            };
            this.mod = new ModifyButton()
            {
                OnClickHandler = ModifyItem
            };


            this.add.Position.Set(0, 0);
            this.remove.Position.Set(100, 0);
            this.move.Position.Set(200, 0);
            this.mod.Position.Set(300, 0);
            this.back.Position.Set(400, 0);

            this.AddChild(this.add);
            this.AddChild(this.remove);
            this.AddChild(this.move);
            this.AddChild(this.mod);
            this.AddChild(this.back);
        }


        private void Return(MouseButtonPressedEvent e)
        {
            SetFlag((int)Flags.DisplayItems);
        }

        private void AddItem(MouseButtonPressedEvent e)
        {

            //take in a string 


            string input;
            Console.WriteLine("Enter name of item: ");
            input = Console.ReadLine();
            Item item = new Item();
            item.SetName(input);
            items.Add(item);

            //calculating y position based on amount of item instances
            //for now, can only interact with 12 items
            //we need a scroll bar or a page tool
            float y = ((itemCount % 8)+1) * 75;

            ItemBox ib;
            ib = new ItemBox(input, Vector.Create(0, y));
            boxes.Add(ib);
            ++itemCount;

            //sets the focus to the box at the top of the list
            if(itemCount == 1)
            {
                boxes.ElementAt(displayFocus).ChangeColour();
            }
        }

        private void RemoveItem(MouseButtonPressedEvent e)
        {
            Console.WriteLine("Select item you wish to remove using UP and DOWN keys");
            Console.WriteLine("Press ENTER when you wish to remove the RED box");
            SetFlag((int)Flags.DeleteItems);
        }

        private void MoveItem(MouseButtonPressedEvent e)
        {
            Console.WriteLine("Select item you wish to move using UP and DOWN keys");

            SetFlag((int)Flags.MoveItemsSelection);
        }

        private void ModifyItem(MouseButtonPressedEvent e)
        {
        }

        public void SetFlag(int mode)
        {
            //DisplayItems, DisplayBoxes, MoveItemsSelection, MoveItems, ModifyItems, DeleteItems
            switch (mode){
                case (int)Flags.DisplayItems:
                    displayItemsFlag = 0;
                    moveItemsFlag = 0;
                    back.Visible = false;
                    break;
                case (int)Flags.DeleteItems:
                    displayItemsFlag = 1;
                    moveItemsFlag = 0;
                    back.Visible = true;
                    break;
                case (int)Flags.MoveItemsSelection:
                    displayItemsFlag = 0;
                    moveItemsFlag = 1;
                    back.Visible = true;
                    break;
                case (int)Flags.MoveItems:
                    Console.WriteLine("Use ARROW keys to move item");
                    Console.WriteLine("Hold down SHIFT to move faster");
                    moveItemsFlag = 2;
                    displayItemsFlag = 0;
                    back.Visible = true;
                    break;
                case (int)Flags.DisplayBoxes:
                    break;
                case (int)Flags.ModifyItems:
                    break;
                default:
                    break;
            }
        }

        
        /*
        public void HandleMouseButtonPressed(MouseButtonPressedEvent e, Button b)
        {
            
        }
        */
        

        public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e)
        {

            /*just test code
            int xx = e.MouseX - x;
            int yy = e.MouseY - y;
            x += xx;
            y += yy;
            */

        }

        //i don;t know how else to handle keyboard presses, so i made modes
        //to control the different functionalities 
        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e)
        {

            //delete item mode
            if (displayItemsFlag == 1)
            {              
                switch (e.Key.ToString())
                {
                    case "Up":

                        if(displayFocus > 0)
                        {
                            //this changes the colour of the current item in focus back to blue
                            boxes.ElementAt(displayFocus).ChangeColour();
                            //move the focus down
                            --displayFocus;
                            //change the new one to red. easy!
                            boxes.ElementAt(displayFocus).ChangeColour();

                        }

                        break;
                    case "Down":
                        if (displayFocus < itemCount-1)
                        {
                            boxes.ElementAt(displayFocus).ChangeColour();
                            ++displayFocus;
                            boxes.ElementAt(displayFocus).ChangeColour();

                        }
                        break;
                    case "Return":
                        if(itemCount > 0) //check to see we can remove items
                        {
                            boxes.RemoveAt(displayFocus);
                            items.RemoveAt(displayFocus);
                            displayFocus = 0;
                            --itemCount;
                            if (itemCount != 0)
                            {
                                //now we need to re-order the boxes
                                int i = 0;
                                float y;
                                foreach (ItemBox ib in boxes)
                                {
                                    y = ((i % 8) + 1) * 75;
                                    ib.ChangePosition(Vector.Create(0, y));
                                    ++i;
                                }

                                boxes.ElementAt(displayFocus).ChangeColour();
                            }
                            SetFlag((int)Flags.DisplayItems);
                        }
                        break;

                    default:                       
                        break;
                }

            }
            //select item to move mode
            else if(moveItemsFlag == 1)
            {
                switch (e.Key.ToString())
                {
                    case "Up":
                        if (displayFocus > 0)
                        {
                            boxes.ElementAt(displayFocus).ChangeColour();
                            --displayFocus;
                            boxes.ElementAt(displayFocus).ChangeColour();
                        }
                        break;
                    case "Down":
                        if (displayFocus < itemCount - 1)
                        {
                            boxes.ElementAt(displayFocus).ChangeColour();
                            ++displayFocus;
                            boxes.ElementAt(displayFocus).ChangeColour();
                        }
                        break;
                    case "Return":
                        SetFlag((int)Flags.MoveItems);
                        break;
                    default:
                        break;
                }
            }
            else if (moveItemsFlag == 2)
            {
                if (e.ShiftDown)
                {
                    switch (e.Key.ToString())
                    {
                        case "Up":
                            items.ElementAt(displayFocus).SetPosition(items.ElementAt(displayFocus).GetPosition().X, items.ElementAt(displayFocus).GetPosition().Y - 5);
                            break;
                        case "Down":
                            items.ElementAt(displayFocus).SetPosition(items.ElementAt(displayFocus).GetPosition().X, items.ElementAt(displayFocus).GetPosition().Y + 5);
                            break;
                        case "Left":
                            items.ElementAt(displayFocus).SetPosition(items.ElementAt(displayFocus).GetPosition().X - 5, items.ElementAt(displayFocus).GetPosition().Y);
                            break;
                        case "Right":
                            items.ElementAt(displayFocus).SetPosition(items.ElementAt(displayFocus).GetPosition().X + 5, items.ElementAt(displayFocus).GetPosition().Y);
                            break;
                        case "Return":
                            SetFlag((int)Flags.DisplayItems);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (e.Key.ToString())
                    {
                        case "Up":
                            items.ElementAt(displayFocus).SetPosition(items.ElementAt(displayFocus).GetPosition().X, items.ElementAt(displayFocus).GetPosition().Y - 1);
                            break;
                        case "Down":
                            items.ElementAt(displayFocus).SetPosition(items.ElementAt(displayFocus).GetPosition().X, items.ElementAt(displayFocus).GetPosition().Y + 1);
                            break;
                        case "Left":
                            items.ElementAt(displayFocus).SetPosition(items.ElementAt(displayFocus).GetPosition().X - 1, items.ElementAt(displayFocus).GetPosition().Y);
                            break;
                        case "Right":
                            items.ElementAt(displayFocus).SetPosition(items.ElementAt(displayFocus).GetPosition().X + 1, items.ElementAt(displayFocus).GetPosition().Y);
                            break;
                        case "Return":
                            SetFlag((int)Flags.DisplayItems);
                            break;
                        default:             
                            break;
                    }
                }
            }
        }

        public override void HandleCloseButtonPressed()
        {
            ServiceProvider.SceneService.LoadGameClosingScene();
        }

        public override void Draw(ICanvas canvas)
        {
            //these are the cases where you want to look at the item boxes rather than items
            if (displayItemsFlag == 1 || moveItemsFlag == 1)
            {
                foreach (ItemBox ib in boxes)
                {
                    ib.Draw(canvas);
                }
                
            }
            else //make a screen to show all the items
            {
                foreach (IDrawableObject s in items)
                {
                    s.Draw(canvas);
                }
            }

            buttonBack.Draw(canvas);
            placeholder.Draw(canvas);
            base.Draw(canvas);
        }
    }
    

    //object is a representation of an item
    public class ItemBox : IDrawableObject
    {
        TextContext tctx;
        SolidRectangleContext srctx;
        bool isFocus = false;
        Vector vector;

        public ItemBox(string s, Vector v)
        {
            this.tctx = new TextContext(s, "uni0553.ttf")
            {
                RenderPosition = v,
                FontColor = RGBA.White,
                FontSize = 16
            };
            this.srctx = new SolidRectangleContext(RGBA.Blue)
            {
                RenderPosition = v,
                RenderBorderColor = RGBA.Black,
                RenderBorderSize = 1.5f,
                RenderSize = Vector.Create(200, 75)
            };
            this.vector = v;
        }

        public void ChangePosition(Vector v)
        {
            tctx.RenderPosition = v;
            srctx.RenderPosition = v;
            vector = v;
        }

        public void ChangeColour()
        {
            if(isFocus)
            {
                srctx = new SolidRectangleContext(RGBA.Blue)
                {
                    RenderPosition = vector,
                    RenderBorderColor = RGBA.Black,
                    RenderBorderSize = 1.5f,
                    RenderSize = Vector.Create(200, 75)
                };
                isFocus = false;
            }
            else
            {
                srctx = new SolidRectangleContext(RGBA.Red)
                {
                    RenderPosition = vector,
                    RenderBorderColor = RGBA.Black,
                    RenderBorderSize = 1.5f,
                    RenderSize = Vector.Create(200, 75)
                };
                isFocus = true;
            }
        }

        public void Draw(ICanvas canvas)
        {
            canvas.Draw(srctx);
            canvas.Draw(tctx);
        }
    }

    public class BackButton : Button
    {
        public BackButton()
        {
            this.Size.Set(100, 25);
            this.Caption.Set("back");
            this.Font.Set("uni0553.ttf");
            this.RenderText.FontColor = RGBA.White;
            this.RenderText.FontSize = 16;
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e)
        {
            OnClickHandler(e);
        }

        public Action<MouseButtonPressedEvent> OnClickHandler { get; set; }
    }

    public class AddButton : Button
    {
        public AddButton()
        {
            this.Size.Set(100, 25);
            this.Caption.Set("add");
            this.Font.Set("uni0553.ttf");
            this.RenderText.FontColor = RGBA.White;
            this.RenderText.FontSize = 16;
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e)
        {
            OnClickHandler(e);
        }

        public Action<MouseButtonPressedEvent> OnClickHandler { get; set; }
    }

    public class RemoveButton : Button
    {
        public RemoveButton()
        {
            this.Size.Set(100, 25);
            this.Caption.Set("remove");
            this.Font.Set("uni0553.ttf");
            this.RenderText.FontColor = RGBA.White;
            this.RenderText.FontSize = 16;
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e)
        {
            OnClickHandler(e);
        }

        public Action<MouseButtonPressedEvent> OnClickHandler { get; set; }
    }

    public class MoveButton : Button
    {
        public MoveButton()
        {
            this.Size.Set(100, 25);
            this.Caption.Set("move");
            this.Font.Set("uni0553.ttf");
            this.RenderText.FontColor = RGBA.White;
            this.RenderText.FontSize = 16;
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e)
        {
            OnClickHandler(e);
        }

        public Action<MouseButtonPressedEvent> OnClickHandler { get; set; }
    }

    public class ModifyButton : Button
    {
        public ModifyButton()
        {
            this.Size.Set(100, 25);
            this.Caption.Set("modify");
            this.Font.Set("uni0553.ttf");
            this.RenderText.FontColor = RGBA.White;
            this.RenderText.FontSize = 16;
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e)
        {
            OnClickHandler(e);
        }

        public Action<MouseButtonPressedEvent> OnClickHandler { get; set; }
    }

    //this is the purpple background for the buttons
    class ButtonsBackground : IDrawableObject
    {

        private readonly SolidRectangleContext _rectangleContext;

        public ButtonsBackground()
        {
            this._rectangleContext = new SolidRectangleContext(RGBA.Purple)
            {
                RenderPosition = Vector.Create(1, 0),
                RenderBorderColor = RGBA.White,
                RenderBorderSize = 1.5f,
                RenderSize = Vector.Create(1000, 30)
            };
        }

        public void Draw(ICanvas canvas)
        {
            canvas.Draw(this._rectangleContext);
        }
    }

    //this is the righthand purple bar
    class Placeholder : IDrawableObject
    {

        private readonly SolidRectangleContext _rectangleContext;

        public Placeholder()
        {
            this._rectangleContext = new SolidRectangleContext(RGBA.Purple)
            {
                RenderPosition = Vector.Create(900, 0),
                RenderBorderColor = RGBA.White,
                RenderBorderSize = 1.5f,
                RenderSize = Vector.Create(100, 650)
            };
        }

        public void UpdatePosition(float x, float y)
        {
            _rectangleContext.RenderPosition = Vector.Create(x, y);
        }

        public void Draw(ICanvas canvas)
        {
            canvas.Draw(this._rectangleContext);
        }
    }

    class Item : IDrawableObject
    {
        
        private readonly SolidRectangleContext _rectangleContext;
        private string name = "item";

        public Item()
        {
            this._rectangleContext = new SolidRectangleContext(RGBA.Red)
            {
                RenderPosition = Vector.Create(150, 540),
                RenderBorderColor = RGBA.White,
                RenderBorderSize = 1.5f,
                RenderSize = Vector.Create(200, 100)
                
            };

        }

        public void SetName(string s)
        {
            name = s;
        }

        public string GetName()
        {
            return name;
        }

        public void SetPosition(float x, float y)
        {
            _rectangleContext.RenderPosition = Vector.Create(x, y);
        }

        public Vector GetPosition()
        {
            return _rectangleContext.RenderPosition;
        }

        public void Draw(ICanvas canvas)
        {
            canvas.Draw(this._rectangleContext);
        }
    }

}
