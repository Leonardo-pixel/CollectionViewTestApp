using MauiAppTestAppV2.Models;
using System.Collections.ObjectModel;

namespace MauiAppTestAppV2.Pages
{
    public partial class CVTest : ContentPage
    {
        private ObservableCollection<TestItem>? _items;
        public ObservableCollection<TestItem>? Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public Command? _addItemCommand;
        public Command? AddItemCommand
        {
            get { return _addItemCommand; }
            set
            {
                _addItemCommand = value;
                OnPropertyChanged(nameof(AddItemCommand));
            }
        }

        public Command? _removeItemCommand;
        public Command? RemoveItemCommand
        {
            get { return _removeItemCommand; }
            set
            {
                _removeItemCommand = value;
                OnPropertyChanged(nameof(RemoveItemCommand));
            }
        }

        public Command? _clearItemsCommand;
        public Command? ClearItemsCommand
        {
            get { return _clearItemsCommand; }
            set
            {
                _clearItemsCommand = value;
                OnPropertyChanged(nameof(ClearItemsCommand));
            }
        }

        public Command? _generateItemsCommand;
        public Command? GenerateItemsCommand
        {
            get { return _generateItemsCommand; }
            set
            {
                _generateItemsCommand = value;
                OnPropertyChanged(nameof(GenerateItemsCommand));
            }
        }

        public CVTest()
        {
            BindingContext = this;
            InitializeComponent();
            AddItemCommand = new Command(AddItem);
            RemoveItemCommand = new Command(RemoveItem);
            ClearItemsCommand = new Command(ClearItems);
            GenerateItemsCommand = new Command(() => GetTestData(10));
            GetTestData(10);
        }

        private void AddItem()
        {
            if (Items != null && Items.Count >= 20)
            {
                _ = DisplayAlertAsync("Item limit reached", "The max item count has been reached: 20 items!", "Ok");
                return;
            }
            if (Items == null)
            {
                Items = new ObservableCollection<TestItem>() { new TestItem(RandomString(20), 1, DisplayAlert) };
            }
            else
            {
                Items.Add(new TestItem(RandomString(20), Items.Count + 1, DisplayAlert));
            }
        }

        private void RemoveItem()
        {
            if (Items == null || Items.Count == 0)
            {
                _ = DisplayAlertAsync("No items to remove", "There are no items to remove!", "Ok");
                return;
            }
            Items.RemoveAt(Items.Count - 1);
        }

        private void ClearItems()
        {
            Items?.Clear();
        }

        private void GetTestData(int itemCount)
        {
            ObservableCollection<TestItem> items = new ObservableCollection<TestItem>();
            for (int i = 1; i <= itemCount; i++)
            {
                items.Add(new TestItem(RandomString(20), i, DisplayAlert));
            }
            Items = items;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void DisplayAlert(TestItem item)
        {
            _ = DisplayAlertAsync(item.Name, $"This item has value: {item.Value}", "Ok");
        }
    }
}
