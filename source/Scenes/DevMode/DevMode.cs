using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Events;
using Annex.Scenes;
using Annex.Scenes.Components;
using Game.Definitions;
using Game.Scenes.DevMode.Elements;
using System;
using System.Collections.Generic;
using System.IO;

namespace Game.Scenes.DevMode
{
    public class DevMode : Scene
    {
        public sealed class ElementID
        {
            public const string ADD_BUTTON = "AddButton";
            public const string MOVE_BUTTON = "MoveButton";
            public const string REMOVE_BUTTON = "RemoveButton";
            public const string MODIFY_POSITION_BUTTON = "ModifyPositionButton";
            public const string MODIFY_SIZE_BUTTON = "ModifySizeButton";
            public const string BACK_BUTTON = "BackButton";
            public const string EXPORT_ALL_BUTTON = "ExportAllButton";
            public const string IMPORT_BUTTON = "ImportButton";
        }

        private readonly ButtonsBackground buttonBack;
        private readonly Placeholder placeholder;

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
            ModifyItemPosition,
            DeleteItems,
            ModifyItemSize
        };

        public DevMode() {
            this.items = new List<Item>();
            this.selectingItem = false;

            this.buttonBack = new ButtonsBackground();
            this.placeholder = new Placeholder();

            this.AddChild(new NavbarButton(ElementID.ADD_BUTTON, "Add", this.AddItem));
            this.AddChild(new NavbarButton(ElementID.REMOVE_BUTTON, "Remove", this.RemoveItem));
            this.AddChild(new NavbarButton(ElementID.MOVE_BUTTON, "Move", this.MoveItem));
            this.AddChild(new NavbarButton(ElementID.MODIFY_POSITION_BUTTON, "Set Position", this.ModifyItemPosition));
            this.AddChild(new NavbarButton(ElementID.MODIFY_SIZE_BUTTON, "Resize", this.ModifyItemSize));
            this.AddChild(new NavbarButton(ElementID.EXPORT_ALL_BUTTON, "Export All", this.OnExportAll));
            this.AddChild(new NavbarButton(ElementID.IMPORT_BUTTON, "Import", this.OnImport));
            this.AddChild(new NavbarButton(ElementID.BACK_BUTTON, "Back", this.Back));


            this.GetElementById(ElementID.BACK_BUTTON).Visible = false;

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

            if (e.Handled) {
                return;
            }

            if (this.selectingItem && this._selectedItem != null) {
                Console.WriteLine("Selected " + _selectedItem.Name);
                var operation = this.Operation;
                this.InvokeFlagOperation(ref operation);
                this.Operation = operation;
                this.selectingItem = false;


                if (this.Operation == Flags.None) {
                    FinishOperation();
                }
                return;
            }

            if (this.Operation == Flags.MoveItemsSelection) {
                this._selectedItem?.SetPosition(e.MouseX, e.MouseY);
            }
        }

        private void FinishOperation() {
            this.Operation = Flags.None;
            foreach (var item in this.items){
                item.isSelected(false);
            }
            this.GetElementById(ElementID.BACK_BUTTON).Visible = false;
        }

        private void OnExportAll(MouseButtonPressedEvent e) {
            var service = AstroSoarServiceProvider.DefinitionService;
            Console.WriteLine("Enter name of save");
            string input = Console.ReadLine();

            foreach (var item in this.items) {
                service.SaveToAssets($"{input}/{item.Name}", item, DefinitionType.UI);
                service.SaveToBin($"{input}/{item.Name}", item, DefinitionType.UI);
            }

            Console.WriteLine("Saved " + input);
        }

        private void OnImport(MouseButtonPressedEvent e){
            var service = AstroSoarServiceProvider.DefinitionService;

            Console.WriteLine("Enter name of save to load");
            string input = Console.ReadLine();

            string path = Path.Combine(Paths.SolutionFolder, "assets/definitions/ui/"+ input);

            if (!Directory.Exists(path)) {
                Console.WriteLine($"{path} doesn't exist.");
                return;
            }

            items.Clear();

            foreach (var file in Directory.GetFiles(path)) {
                var fi = new FileInfo(file);
                var item = service.LoadFromBin<Item>(DefinitionType.UI, $"{input}/{fi.Name[0..^5]}");
                items.Add(item);
            }


        }

        private void Back(MouseButtonPressedEvent e) {
            FinishOperation();
        }

        private void AddItem(MouseButtonPressedEvent e) {
            Console.WriteLine("Enter name of item and the image you are adding: foo,foo.png ");
            string input = Console.ReadLine();
            string[] image = input.Split(',');
            string name = image[0].Trim();
            string texture = image[1].Trim();
            Item item = new Item(texture);
            item.Name = name;
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

        private void ModifyItemSize(MouseButtonPressedEvent e){
            Console.WriteLine("Select the item you would like to resize and enter the new dimensions (ie 100,200)");
            SetFlag(Flags.ModifyItemSize);
        }

        public void SetFlag(Flags operation) {
            this.Operation = operation;
            this.selectingItem = true;
        }

        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {
            if (e.Key == KeyboardKey.Tilde) {
                Debug.ToggleDebugOverlay();
                return;
            }

            base.HandleKeyboardKeyPressed(e);

            if (e.Handled){
                return;
            }

            if (this.Operation == Flags.MoveItemsSelection) {
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
                        FinishOperation();
                        return;
                }
                this._selectedItem?.GetPosition().Add(dx, dy);
            }
        }

        public void InvokeFlagOperation(ref Flags flag) {

            // By default
            var postOperationFlag = Flags.None;

            switch (flag) {
                case Flags.DeleteItems: {
                    if (items.Count == 0) {
                        return;
                    }
                    items.Remove(this._selectedItem);
                    break;
                }
                case Flags.ModifyItemPosition: {
                    string input = Console.ReadLine();
                    string[] coordinates = input.Split(',');
                    int x = Int16.Parse(coordinates[0].Trim());
                    int y = Int16.Parse(coordinates[1].Trim());
                    this._selectedItem?.SetPosition(x, y);

                    break;
                }
                case Flags.ModifyItemSize:{
                    string input = Console.ReadLine();
                    string[] dimensions = input.Split(',');
                    int x = Int16.Parse(dimensions[0].Trim());
                    int y = Int16.Parse(dimensions[1].Trim());
                    this._selectedItem?.SetSize(x, y);
                    break;
                }
                case Flags.MoveItemsSelection:
                    Console.WriteLine("Use ARROW keys to move item");
                    Console.WriteLine("Hold down SHIFT to move faster");
                    this.GetElementById(ElementID.BACK_BUTTON).Visible = true;
                    postOperationFlag = Flags.MoveItemsSelection;
                    break;
            }

            flag = postOperationFlag;
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
