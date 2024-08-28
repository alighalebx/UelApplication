using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UelApplication.Models;

public class Section
{
    public string Name { get; set; }
    public ObservableCollection<string> Files { get; } = new ObservableCollection<string>();
}