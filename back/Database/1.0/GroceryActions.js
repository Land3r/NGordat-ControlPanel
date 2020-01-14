db = connect("localhost:27017/ControlPanelDb");
db.GroceryActions.insertOne({
	_id: UUID(),
	Name: "Ajoute {quantity} {item}",
	Action: "Add",
	Icon: "add_circle_outline"
});
db.GroceryActions.insertOne({
	_id: UUID(),
	Name: "Mets {quantity} {item}",
	Action: "Add",
	Icon: "add_circle_outline"
});
db.GroceryActions.insertOne({
	_id: UUID(),
	Name: "Rajoute {quantity} {item}",
	Action: "Add",
	Icon: "add_circle_outline"
});
db.GroceryActions.insertOne({
	_id: UUID(),
	Name: "Retire {quantity} {item}",
	Action: "Remove",
	Icon: "remove_circle_outline"
});
db.GroceryActions.insertOne({
	_id: UUID(),
	Name: "Supprime {quantity} {item}",
	Action: "Remove",
	Icon: "remove_circle_outline"
});
db.GroceryActions.insertOne({
	_id: UUID(),
	Name: "Efface {quantity} {item}",
	Action: "Remove",
	Icon: "remove_circle_outline"
});