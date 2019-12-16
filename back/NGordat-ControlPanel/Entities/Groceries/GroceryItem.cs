namespace NGordatControlPanel.Entities.Groceries
{
  using System.Collections.Generic;

  using NGordatControlPanel.Entities.Db;

  /// <summary>
  /// <see cref="GroceryItem"/> class.
  /// Represents an item you can manage from your shopping list.
  /// </summary>
  public class GroceryItem : ADbTrackedEntity
  {
    /// <summary>
    /// Gets or sets the Name of the <see cref="GroceryItem"/>.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the list of Aliases of the <see cref="GroceryItem"/>.
    /// </summary>
    public IEnumerable<string> Aliases { get; set; }

    /// <summary>
    /// Gets or sets the Icon of the <see cref="GroceryItem"/>.
    /// </summary>
    public string Icon { get; set; }
  }
}
