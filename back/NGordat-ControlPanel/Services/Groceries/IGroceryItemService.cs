namespace NGordatControlPanel.Services.Groceries
{
  using NGordatControlPanel.Entities.Groceries;
  using NGordatControlPanel.Services.Core;

  /// <summary>
  /// <see cref="IGroceryItemService"/> interface.
  /// Interface service CRUD for <see cref="GroceryItem"/>.
  /// </summary>
  public interface IGroceryItemService : ICrudService<GroceryItem>
  {
  }
}