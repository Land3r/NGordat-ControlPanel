namespace NGordatControlPanel.Services.Groceries
{
  using NGordatControlPanel.Entities.Groceries;
  using NGordatControlPanel.Services.Core;

  /// <summary>
  /// <see cref="IGroceryItemService"/> interface.
  /// Contract for operating with <see cref="GroceryItem"/>.
  /// </summary>
  public interface IGroceryItemService : ICrudService<GroceryItem>
  {
  }
}