namespace NGordatControlPanel.Entities.Groceries
{
  using System.Collections.Generic;

  using NGordatControlPanel.Entities.Db;

  /// <summary>
  /// <see cref="GroceryAction"/> class.
  /// Class representing a kind of action available on the shopping list.
  /// </summary>
  public class GroceryAction : ADbTrackedEntity
  {
    /// <summary>
    /// Gets or sets the Name of the <see cref="GroceryAction"/>.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the list of Aliases of the <see cref="GroceryAction"/>.
    /// </summary>
    public IEnumerable<string> Aliases { get; set; }

    /// <summary>
    /// Gets or sets the Action of the <see cref="GroceryAction"/>.
    /// Can be add, remove, delete, ...
    /// </summary>
    public string Action { get; set; }

    /// <summary>
    /// Gets or sets the Icon of the <see cref="GroceryAction"/>.
    /// </summary>
    public string Icon { get; set; }
  }
}
