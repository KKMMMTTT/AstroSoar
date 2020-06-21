using Annex;
using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes;
using Annex.Events;
using Annex.Scenes.Components;
using Game.Scenes.DevMode.Elemenets;
using Game.Scenes.DevMode.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Scenes.DevMode
{
    public class DevMode : Scene
    {
        private readonly ButtonsBackground buttonBack;
        private readonly Placeholder placeholder;

        private readonly AddButton add;
        private readonly RemoveButton remove;
        private readonly MoveButton move;
        private readonly ModifyPositionButton mod;
        private readonly ModifySizeButton modSize;
        private readonly BackButton back;

        private readonly SetTextureButton cmdSetTexture;
        private readonly ExportAllButton cmdExportAll;

        private readonly List<Item> items;

        private Item? _selectedItem;
        private Flags Operation = Flags.None;
        private bool selectingItem;

        public enum Flags
        {
            None,
            DisplayItems,
            ModifyPositionItemSelection,
            MoveItemsSelection,
            MoveItems,
            ModifyItemPosition,
            DeleteItems,
            SetTextureSelection,
            ModifyItemSize
        };

        public DevMode() {
            this.items = new List<Item>();
            this.selectingItem = false;

            this.buttonBack = new ButtonsBackground();
            this.placeholder = new Placeholder();

            this.back = new BackButton() {
                OnClickHandler = Back,
                Visible = false
            };
            this.add = new AddButton() {
                OnClickHandler = AddItem
            };
            this.remove = new RemoveButton() {
                OnClickHandler = RemoveItem
            };
            this.move = new MoveButton() {
                OnClickHandler = MoveItem
            };
            this.mod = new ModifyPositionButton() {
                OnClickHandler = ModifyItemPosition
            };

            this.cmdExportAll = new ExportAllButton(this.OnExportAll);
            this.cmdSetTexture = new SetTextureButton(this.OnSetTexture);


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

            this.Events.AddEvent(PriorityType.INPUT, (e) => {
                if (!this.selectingItem) {
                    return;
                }
                var canvas = ServiceProvider.Canvas;
                var pos = canvas.GetRealMousePos();
                this._selectedItem = null;

                for (int i = this.items.Count - 1; i >= 0; i--) {
                    var item = this.items[i];
                    var itemPos = item.GetPosition();
                    var itemSize = item.GetSize();

                    item.isSelected(false);
                    if (this._selectedItem != null) {
                        continue;
                    }

                    if (pos.X >= itemPos.X && pos.Y >= itemPos.Y) {
                        if (pos.X <= itemPos.X + itemSize.X && pos.Y <= itemPos.Y + itemSize.Y) {
                            this._selectedItem = item;
                            item.isSelected(true);
                        }
                    }
                }

            }, 100, 0);
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {

            base.HandleMouseButtonPressed(e);

            if (this.selectingItem)
            {
                this.InvokeFlagOperation();
                this.Operation = Flags.None;
                this.selectingItem = false;
            }
        }

        private void OnExportAll(MouseButtonPressedEvent e) {

        }

        private void OnSetTexture(MouseButtonPressedEvent e) {
            SetFlag(Flags.SetTextureSelection);
        }

        private void Back(MouseButtonPressedEvent e) {
            this.Operation = Flags.None;
            this.back.Visible = false;
        }

        private void AddItem(MouseButtonPressedEvent e) {
            Console.WriteLine("Enter name of item: ");
            string input = Console.ReadLine();
            Item item = new Item();
            item.SetName(input);
            items.Add(item);
        }

        private void RemoveItem(MouseButtonPressedEvent e) {
            Console.WriteLine("Select item you wish to remove using UP and DOWN keys");
            Console.WriteLine("Press ENTER when you wish to remove the RED box");
            SetFlag(Flags.DeleteItems);
        }

        private void MoveItem(MouseButtonPressedEvent e) {
            Console.WriteLine("Select item you wish to move using UP and DOWN keys");

            SetFlag(Flags.MoveItemsSelection);
        }

        private void ModifyItemPosition(MouseButtonPressedEvent e) {
            Console.WriteLine("Select the item you would like to move and enter the new coordinates (ie 100,200)");
            SetFlag(Flags.ModifyItemPosition);
        }

        public void SetFlag(Flags operation) {
            this.Operation = operation;
            this.selectingItem = true;
        }

        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {
            if (this.Operation == Flags.MoveItems) {
                float dx = 0, dy = 0;
                float speed = e.ShiftDown ? 5 : 1;
                switch (e.Key) {
                    case KeyboardKey.Up:
                        dy = -speed;
                        break;
                    case KeyboardKey.Down:
                        dy = speed;
                        break;
                    case KeyboardKey.Left:
                        dx = -speed;
                        break;
                    case KeyboardKey.Right:
                        dx = speed;
                        break;
                    case KeyboardKey.Enter:
                        this.Operation = Flags.None;
                        return;
                }
                this._selectedItem?.GetPosition().Add(dx, dy);
            }
        }

        public void InvokeFlagOperation() {
            switch (this.Operation) {
                case Flags.DeleteItems: {
                    if (items.Count == 0) {
                        return;
                    }
                    items.Remove(this._selectedItem);
                    break;
                }
                case Flags.ModifyItemPosition:
                    {
                        string input = Console.ReadLine();
                        string[] coordinates = input.Split(',');
                        int x = Int16.Parse(coordinates[0]);
                        int y = Int16.Parse(coordinates[1]);
                        this._selectedItem?.SetPosition(x,y);

                        break;
                }
                case Flags.MoveItems:
                    Console.WriteLine("Use ARROW keys to move item");
                    Console.WriteLine("Hold down SHIFT to move faster");
                    back.Visible = true;
                    break;
            }
        }

        public override void HandleCloseButtonPressed() {
            ServiceProvider.SceneService.LoadGameClosingScene();
        }

        public override void Draw(ICanvas canvas) {
            foreach (IDrawableObject s in items) {
                s.Draw(canvas);
            }
            buttonBack.Draw(canvas);
            placeholder.Draw(canvas);
            base.Draw(canvas);
        }
    }
}
