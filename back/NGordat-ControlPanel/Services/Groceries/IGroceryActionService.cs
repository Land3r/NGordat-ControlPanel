namespace NGordatControlPanel.Services.Groceries
{
  using NGordatControlPanel.Entities.Groceries;
  using NGordatControlPanel.Services.Core;

  /// <summary>
  /// <see cref="IGroceryActionService"/> interface.
  /// Interface service CRUD for <see cref="GroceryAction"/>.
  /// </summary>
  public interface IGroceryActionService : ICrudService<GroceryAction>
  {
  }
}