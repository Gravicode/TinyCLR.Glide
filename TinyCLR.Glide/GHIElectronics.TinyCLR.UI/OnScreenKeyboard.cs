// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.OnScreenKeyboard
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

using GHIElectronics.TinyCLR.UI.Controls;
using GHIElectronics.TinyCLR.UI.Input;
using GHIElectronics.TinyCLR.UI.Media;
using GHIElectronics.TinyCLR.UI.Media.Imaging;
using System;
using System.Collections;
using System.Drawing;

namespace GHIElectronics.TinyCLR.UI
{
    public class OnScreenKeyboard : Window
    {
        private Hashtable views;
        private TextBox source;
        private TextBox input;
        private GHIElectronics.TinyCLR.UI.Controls.Image image;
        private OnScreenKeyboard.KeyboardView active;
        private double scaleX;
        private double scaleY;
        private int offsetX;
        private int offsetY;

        public new static Font Font { get; set; }

        internal OnScreenKeyboard()
        {
            this.views = new Hashtable();
            this.Width = WindowManager.Instance.ActualWidth;
            this.Height = WindowManager.Instance.ActualHeight;
            this.Background = (GHIElectronics.TinyCLR.UI.Media.Brush)new SolidColorBrush(Colors.Red);
            StackPanel stackPanel = new StackPanel();
            TextBox textBox = new TextBox();
            textBox.ForOnScreenKeyboard = true;
            textBox.Font = OnScreenKeyboard.Font;
            this.input = textBox;
            stackPanel.Children.Add((UIElement)this.input);
            GHIElectronics.TinyCLR.UI.Controls.Image image = new GHIElectronics.TinyCLR.UI.Controls.Image();
            image.Source = (ImageSource)null;
            image.Width = WindowManager.Instance.ActualWidth;
            image.Height = WindowManager.Instance.ActualHeight - OnScreenKeyboard.Font.Height;
            image.Stretch = Stretch.Fill;
            this.image = image;
            this.image.TouchUp += new TouchEventHandler(this.OnTouchUp);
            stackPanel.Children.Add((UIElement)this.image);
            this.Child = (UIElement)stackPanel;
        }

        internal void ShowFor(TextBox textBox)
        {
            this.Width = WindowManager.Instance.ActualWidth;
            this.Height = WindowManager.Instance.ActualHeight;
            this.source = textBox;
            this.input.Text = this.source.Text;
            this.ShowView(OnScreenKeyboard.KeyboardViewId.Lowercase);
        }

        private void OnTouchUp(object sender, TouchEventArgs e)
        {
            int num1 = (int)((double)e.Touches[0].X * this.scaleX - (double)this.offsetX);
            int index1 = (int)((double)e.Touches[0].Y * this.scaleY - (double)this.offsetY) / this.active.RowHeight;
            int num2 = 0;
            int[] numArray = this.active.ColumnWidth[index1];
            int num3 = this.active.RowColumnOffset[index1];
            while (num2 < numArray.Length && num3 < num1)
                num3 += numArray[num2++];
            int index2;
            if ((index2 = num2 - 1) < 0 || num3 < num1)
                return;
            Action action;
            if ((action = this.active.SpecialKeys?[index1]?[index2]) != null)
                action();
            else
                this.Append(this.active.Keys[index1][index2]);
        }

        private void Backspace()
        {
            if (this.input.Text.Length <= 0)
                return;
            this.input.Text = this.input.Text.Substring(0, this.input.Text.Length - 1);
        }

        private void Append(char c)
        {
            this.input.Text += c.ToString();
        }

        private new void Close()
        {
            this.source.Text = this.input.Text;
            Application.Current.CloseOnScreenKeyboard();
        }

        private void ShowView(OnScreenKeyboard.KeyboardViewId id)
        {
            if (!this.views.Contains((object)id))
                this.CreateView(id);
            this.active = (OnScreenKeyboard.KeyboardView)this.views[(object)id];
            this.image.Source = (ImageSource)this.active.Image;
            this.image.InvalidateMeasure();
            this.scaleX = (double)this.active.Image.Width / (double)this.image.Width;
            this.scaleY = (double)this.active.Image.Height / (double)this.image.Height;
            this.offsetX = 0;
            this.offsetY = OnScreenKeyboard.Font.Height;
        }

        private void CreateView(OnScreenKeyboard.KeyboardViewId id)
        {
            int num1 = 16;
            int num2 = 32;
            int num3 = 48;
            int[] numArray = new int[10]
            {
        num2,
        num2,
        num2,
        num2,
        num2,
        num2,
        num2,
        num2,
        num2,
        num2
            };
            System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)null;
            OnScreenKeyboard.KeyboardView keyboardView1 = new OnScreenKeyboard.KeyboardView()
            {
                RowHeight = num2
            };
            switch (id)
            {
                case OnScreenKeyboard.KeyboardViewId.Lowercase:
                    bitmap = Resources.GetBitmap(Resources.BitmapResources.Keyboard_Lowercase);
                    keyboardView1.RowColumnOffset = new int[4]
                    {
            0,
            num1,
            0,
            0
                    };
                    keyboardView1.ColumnWidth = new int[4][]
                    {
            numArray,
            new int[9]
            {
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2
            },
            new int[9]
            {
              num3,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num3
            },
            new int[5]{ num3, num2, num2 * 5, num2, num3 }
                    };
                    keyboardView1.Keys = new char[4][]
                    {
            new char[10]
            {
              'q',
              'w',
              'e',
              'r',
              't',
              'y',
              'u',
              'i',
              'o',
              'p'
            },
            new char[9]{ 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l' },
            new char[9]
            {
              char.MinValue,
              'z',
              'x',
              'c',
              'v',
              'b',
              'n',
              'm',
              char.MinValue
            },
            new char[5]{ char.MinValue, ',', ' ', '.', char.MinValue }
                    };
                    OnScreenKeyboard.KeyboardView keyboardView2 = keyboardView1;
                    Action[][] actionArray1 = new Action[4][];
                    int index1 = 2;
                    Action[] actionArray2 = new Action[9];
                    actionArray2[0] = (Action)(() => this.ShowView(OnScreenKeyboard.KeyboardViewId.Uppercase));
                    actionArray2[8] = (Action)(() => this.Backspace());
                    actionArray1[index1] = actionArray2;
                    actionArray1[3] = new Action[5]
                    {
            (Action) (() => this.ShowView(OnScreenKeyboard.KeyboardViewId.Numbers)),
            null,
            null,
            null,
            (Action) (() => this.Close())
                    };
                    keyboardView2.SpecialKeys = actionArray1;
                    break;
                case OnScreenKeyboard.KeyboardViewId.Uppercase:
                    bitmap = Resources.GetBitmap(Resources.BitmapResources.Keyboard_Uppercase);
                    keyboardView1.RowColumnOffset = new int[4]
                    {
            0,
            num1,
            0,
            0
                    };
                    keyboardView1.ColumnWidth = new int[4][]
                    {
            numArray,
            new int[9]
            {
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2
            },
            new int[9]
            {
              num3,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num3
            },
            new int[5]{ num3, num2, num2 * 5, num2, num3 }
                    };
                    keyboardView1.Keys = new char[4][]
                    {
            new char[10]
            {
              'Q',
              'W',
              'E',
              'R',
              'T',
              'Y',
              'U',
              'I',
              'O',
              'P'
            },
            new char[9]{ 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L' },
            new char[9]
            {
              char.MinValue,
              'Z',
              'X',
              'C',
              'V',
              'B',
              'N',
              'M',
              char.MinValue
            },
            new char[5]{ char.MinValue, ',', ' ', '.', char.MinValue }
                    };
                    OnScreenKeyboard.KeyboardView keyboardView3 = keyboardView1;
                    Action[][] actionArray3 = new Action[4][];
                    int index2 = 2;
                    Action[] actionArray4 = new Action[9];
                    actionArray4[0] = (Action)(() => this.ShowView(OnScreenKeyboard.KeyboardViewId.Lowercase));
                    actionArray4[8] = (Action)(() => this.Backspace());
                    actionArray3[index2] = actionArray4;
                    actionArray3[3] = new Action[5]
                    {
            (Action) (() => this.ShowView(OnScreenKeyboard.KeyboardViewId.Numbers)),
            null,
            null,
            null,
            (Action) (() => this.Close())
                    };
                    keyboardView3.SpecialKeys = actionArray3;
                    break;
                case OnScreenKeyboard.KeyboardViewId.Numbers:
                    bitmap = Resources.GetBitmap(Resources.BitmapResources.Keyboard_Numbers);
                    keyboardView1.RowColumnOffset = new int[4];
                    keyboardView1.ColumnWidth = new int[4][]
                    {
            numArray,
            numArray,
            new int[9]
            {
              num3,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num3
            },
            new int[5]{ num3, num2, num2 * 5, num2, num3 }
                    };
                    keyboardView1.Keys = new char[4][]
                    {
            new char[10]
            {
              '1',
              '2',
              '3',
              '4',
              '5',
              '6',
              '7',
              '8',
              '9',
              '0'
            },
            new char[10]
            {
              '@',
              '#',
              '$',
              '%',
              '&',
              '*',
              '-',
              '+',
              '(',
              ')'
            },
            new char[9]
            {
              char.MinValue,
              '!',
              '"',
              '\'',
              ':',
              ';',
              '/',
              '?',
              char.MinValue
            },
            new char[5]{ char.MinValue, ',', ' ', '.', char.MinValue }
                    };
                    OnScreenKeyboard.KeyboardView keyboardView4 = keyboardView1;
                    Action[][] actionArray5 = new Action[4][];
                    int index3 = 2;
                    Action[] actionArray6 = new Action[9];
                    actionArray6[0] = (Action)(() => this.ShowView(OnScreenKeyboard.KeyboardViewId.Symbols));
                    actionArray6[8] = (Action)(() => this.Backspace());
                    actionArray5[index3] = actionArray6;
                    actionArray5[3] = new Action[5]
                    {
            (Action) (() => this.ShowView(OnScreenKeyboard.KeyboardViewId.Lowercase)),
            null,
            null,
            null,
            (Action) (() => this.Close())
                    };
                    keyboardView4.SpecialKeys = actionArray5;
                    break;
                case OnScreenKeyboard.KeyboardViewId.Symbols:
                    bitmap = Resources.GetBitmap(Resources.BitmapResources.Keyboard_Symbols);
                    keyboardView1.RowColumnOffset = new int[4];
                    keyboardView1.ColumnWidth = new int[4][]
                    {
            numArray,
            numArray,
            new int[9]
            {
              num3,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num2,
              num3
            },
            new int[5]{ num3, num2, num2 * 5, num2, num3 }
                    };
                    keyboardView1.Keys = new char[4][]
                    {
            new char[10]
            {
              '~',
              '`',
              '|',
              '•',
              '√',
              'π',
              '÷',
              '×',
              '{',
              '}'
            },
            new char[10]
            {
              '\t',
              '£',
              '¢',
              '€',
              'º',
              '^',
              '_',
              '=',
              '[',
              ']'
            },
            new char[9]
            {
              char.MinValue,
              '™',
              '®',
              '©',
              '¶',
              '\\',
              '<',
              '>',
              char.MinValue
            },
            new char[5]{ char.MinValue, ',', ' ', '.', char.MinValue }
                    };
                    OnScreenKeyboard.KeyboardView keyboardView5 = keyboardView1;
                    Action[][] actionArray7 = new Action[4][];
                    int index4 = 2;
                    Action[] actionArray8 = new Action[9];
                    actionArray8[0] = (Action)(() => this.ShowView(OnScreenKeyboard.KeyboardViewId.Numbers));
                    actionArray8[8] = (Action)(() => this.Backspace());
                    actionArray7[index4] = actionArray8;
                    actionArray7[3] = new Action[5]
                    {
            (Action) (() => this.ShowView(OnScreenKeyboard.KeyboardViewId.Lowercase)),
            null,
            null,
            null,
            (Action) (() => this.Close())
                    };
                    keyboardView5.SpecialKeys = actionArray7;
                    break;
            }
            keyboardView1.Image = BitmapImage.FromGraphics(Graphics.FromImage((System.Drawing.Image)bitmap));
            this.views.Add((object)id, (object)keyboardView1);
        }

        private enum KeyboardViewId
        {
            Lowercase,
            Uppercase,
            Numbers,
            Symbols,
        }

        private class KeyboardView
        {
            public BitmapImage Image { get; set; }

            public int RowHeight { get; set; }

            public int[] RowColumnOffset { get; set; }

            public int[][] ColumnWidth { get; set; }

            public char[][] Keys { get; set; }

            public Action[][] SpecialKeys { get; set; }
        }
    }
}
