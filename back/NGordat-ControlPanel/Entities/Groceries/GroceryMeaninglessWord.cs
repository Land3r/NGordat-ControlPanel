namespace NGordatControlPanel.Entities.Groceries
{
  using NGordatControlPanel.Entities.Db;

  /// <summary>
  /// <see cref="GroceryQuantity"/> class.
  /// Class representing a meaningless word (that doesn't add anything to the context).
  /// </summary>
  public class GroceryMeaninglessWord : ADbTrackedEntity
  {
    /// <summary>
    /// Gets or sets the Name of the <see cref="GroceryMeaninglessWord"/>.
    /// </summary>
    public string Name { get; set; }
  }
}
