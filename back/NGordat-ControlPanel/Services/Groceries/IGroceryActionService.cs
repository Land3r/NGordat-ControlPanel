namespace NGordatControlPanel.Services.Groceries
{
  using NGordatControlPanel.Entities.Groceries;
  using NGordatControlPanel.Services.Core;

  /// <summary>
  /// <see cref="IGroceryActionService"/> interface.
  /// Contract for operating <see cref="GroceryAction"/>.
  /// </summary>
  public interface IGroceryActionService : ICrudService<GroceryAction>
  {
  }
}