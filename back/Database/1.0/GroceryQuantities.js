db = connect("localhost:27017/ControlPanelDb");
db.GroceryQuantities.insertOne({
	_id: UUID(),
	Name: "de",
	Value: "1"
});
db.GroceryQuantities.insertOne({
	_id: UUID(),
	Name: "du",
	Value: "1"
});