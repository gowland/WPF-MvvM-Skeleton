using System;
using System.Windows;

namespace MvvMCore.Converters
{
    public class TemplateSelectorByTypeOption
    {
        public Type Type { get; set; }
        public DataTemplate Template { get; set; }
    }
}