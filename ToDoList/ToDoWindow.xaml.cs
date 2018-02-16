#region using
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

#endregion

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for this application and ToDoWindow.xaml
    /// ToDo is a simple note app, where the user can make fast and easy "Todo's", i.e small tasks that needs to be done.
    /// </summary>
    public partial class ToDoWindow : Window
    {

        #region Properties
        public List<string> RestoreList { get; set; }
        public List<string> TodoList { get; set; }
        public TextBlock SelectedBlock { get; set; }
        public TextBlock BlockToUpdate { get; set; }
        public string OldBlockText { get; set; }
        public string FilePath { get; set; }

        private List<string> colorList = new List<string>
        {
            "#bacfef", "#bae8ef", "#baefdf", "#c9efba",
            "#eaefba", "#efd0ba", "#efbaba", "#efbaed",
            "#cabaef"
        };

        #endregion

        #region Constructor
        public ToDoWindow()
        {
            InitializeComponent();

            TodoList = new List<string>();
            SelectedBlock = null;
            BlockToUpdate = null;
            FilePath = @"C:\Users\Niko Tesla\Documents\todoList.xml";

            Load();
        }
        #endregion

        #region Functions
        public void newTodo(string text)
        {
            if (text.Length < 1)
                MessageBox.Show("ToDo too short");
            else
            {
                // clear default block
                if (TodoList.First().Equals("Add a new Todo"))
                    TodoList.Clear();

                TodoList.Add(text);
                updateToDoDisplay();
            }
        }

        public void updateToDoDisplay()
        {
            SelectedBlock = null;
            todoStack.Children.Clear();
            todoText.Text = "";

            Random rnd = new Random();
            Brush color = null;

            short i = 0;
            int x = 0;

            if (TodoList.Count == 0)
            {
                TodoList.Add("Add a new Todo");
            }


            foreach (var t in TodoList)
            {
                TextBlock block = new TextBlock();
                block.Text = t;
                block.MinHeight = 40;

                color = (Brush)(new BrushConverter().ConvertFrom(colorList[x++]));
                block.Background = color;

                block.MouseEnter += todoBlock_MouseEnter;
                block.MouseLeave += todoBlock_MouseLeave;

                // only add edit events if it isnt the default block
                if (!(TodoList.First().Equals("Add a new Todo")))
                {
                    block.MouseRightButtonDown += todoBlock_RightClick;
                    block.MouseLeftButtonDown += todoBlock_LeftClick;
                }

                block.MouseDown += Block_MidleMouseDown;
                block.TextWrapping = TextWrapping.Wrap;
                todoStack.Children.Add(block);

                if (++i == colorList.Count)
                    x = 0;
            }
            TextBlock bufferBlock = new TextBlock
            {
                MinHeight = 50
            };
            todoStack.Children.Add(bufferBlock);
            LinearGradientBrush brush = new LinearGradientBrush
            {
                EndPoint = new Point(0.5, 1),
                StartPoint = new Point(0.5, 0)
            };
            var c = (Color)ColorConverter.ConvertFromString(colorList[x]);
            var transparent = new Color
            {
                A = 0,
                R = 0,
                G = 0,
                B = 0
            };
            GradientStop bot = new GradientStop(transparent, 0.919);
            GradientStop top = new GradientStop(c, 0);
            GradientStopCollection gradient = new GradientStopCollection();
            gradient.Add(top);
            gradient.Add(bot);
            brush.GradientStops = gradient;
            newTodoBox.Fill = brush;
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(List<string>));
            if (File.Exists(FilePath))
                File.Delete(FilePath);

            using (var stream = File.OpenWrite(FilePath))
            {
                serializer.Serialize(stream, TodoList);
            }

        }

        public void Load()
        {
            var serializer = new XmlSerializer(typeof(List<string>));
            if (File.Exists(FilePath))
            {
                using (var stream = File.OpenRead(FilePath))
                {
                    var old = (List<string>)serializer.Deserialize(stream);
                    TodoList.Clear();
                    TodoList.AddRange(old);
                }
                updateToDoDisplay();
            }

        }

        #endregion

        #region Events
        // middle mouse click event to restore previously deleted todo
        private void Block_MidleMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                if (RestoreList != null)
                {
                    TodoList = RestoreList.ToList();
                    RestoreList = null;
                    updateToDoDisplay();
                }
            }

        }

        private void addToDo_Click(object sender, RoutedEventArgs e)
        {
            newTodo(todoText.Text);
        }

        // display help text event
        private void todoBlock_MouseEnter(object sender, RoutedEventArgs e)
        {
            SelectedBlock = sender as TextBlock;
            OldBlockText = SelectedBlock.Text;
            TextBlock bufferBlock = (TextBlock)todoStack.Children[todoStack.Children.Count - 1];
            bufferBlock.TextAlignment = TextAlignment.Left;
            bufferBlock.FontSize = 11;
            bufferBlock.Text += "#Left click to edit\n#Right click to remove\n#Middle Mouse to undo deletion";

        }

        private void todoBlock_LeftClick(object sender, RoutedEventArgs e)
        {
            if (SelectedBlock != null)
            {
                BlockToUpdate = SelectedBlock;
                todoText.Text = BlockToUpdate.Text;
                updateBtn.Visibility = Visibility.Visible;
                cancelUpdateBtn.Visibility = Visibility.Visible;
                addToDo.IsHitTestVisible = false;
            }
        }

        private void todoBlock_RightClick(object sender, RoutedEventArgs e)
        {
            int i = 0;
            if (SelectedBlock != null)
            {
                foreach (var b in todoStack.Children)
                {
                    if (SelectedBlock == b)
                    {
                        //save backup list if deletion was mistake
                        RestoreList = TodoList.ToList();

                        //clear event
                        SelectedBlock.MouseRightButtonDown -= todoBlock_RightClick;

                        TodoList.RemoveAt(i);
                        break;
                    }
                    i++;
                }

                updateToDoDisplay();
            }
        }

        private void todoBlock_MouseLeave(object sender, RoutedEventArgs e)
        {
            if (SelectedBlock != null)
                SelectedBlock.Text = OldBlockText;
            SelectedBlock = null;
            TextBlock buffer = (TextBlock)todoStack.Children[todoStack.Children.Count - 1];
            buffer.Text = "";
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            updateBtn.Visibility = Visibility.Collapsed;
            cancelUpdateBtn.Visibility = Visibility.Collapsed;
            int i = 0;
            if (BlockToUpdate != null)
            {
                foreach (TextBlock b in todoStack.Children)
                {
                    if (BlockToUpdate == b)
                    {
                        TodoList.RemoveAt(i);
                        TodoList.Insert(i, todoText.Text);
                        break;
                    }
                    i++;
                }
                BlockToUpdate = null;
                addToDo.IsHitTestVisible = true;
                updateToDoDisplay();
            }


        }

        private void cancelUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            BlockToUpdate = null;
            addToDo.IsHitTestVisible = true;
            updateBtn.Visibility = Visibility.Collapsed;
            cancelUpdateBtn.Visibility = Visibility.Collapsed;
            updateToDoDisplay();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
            Environment.Exit(1);
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        #endregion

    }

}
