using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppTestAppV2.Models
{
    public class TestItem
    {
        public int Value { get; set; }
        public string Name { get; set; }
        public Command ButtonDetailCommand { get; set; }

        public TestItem(string name, int value, Action<TestItem> displayDetails)
        {
            Name = name;
            Value = value;
            ButtonDetailCommand = new Command(() => displayDetails(this));
        }
    }
}
