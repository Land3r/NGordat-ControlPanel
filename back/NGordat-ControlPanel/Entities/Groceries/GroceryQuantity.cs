namespace NGordatControlPanel.Entities.Groceries
{
  using NGordatControlPanel.Entities.Db;

  /// <summary>
  /// <see cref="GroceryQuantity"/> class.
  /// Class representing an item quantity for groceries.
  /// </summary>
  public class GroceryQuantity : ADbTrackedEntity
  {
    /// <summary>
    /// Gets or sets the Name of the <see cref="GroceryQuantity"/>.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the Value of the <see cref="GroceryQuantity"/>.
    /// </summary>
    public string Value { get; set; }
  }
}
