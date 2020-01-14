namespace NGordatControlPanel.Services.Groceries
{
  using NGordatControlPanel.Entities.Groceries;
  using NGordatControlPanel.Services.Core;

  /// <summary>
  /// <see cref="IGroceryQuantityService"/> interface.
  /// Interface service CRUD for <see cref="GroceryQuantity"/>.
  /// </summary>
  public interface IGroceryQuantityService : ICrudService<GroceryQuantity>
  {
  }
}