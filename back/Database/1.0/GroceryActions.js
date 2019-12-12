db = connect("localhost:27017/ControlPanelDb");
db.GroceryActions.insertOne({
	_id: UUID(),
	Name: "Ajoute {quantity} {item}",
	Aliases: [
		"Mets {quantity} {item}",
		"Rajoute {quantity} {item}"
	],
	Action: "Add",
	Icon: "add_circle_outline"
});
db.GroceryActions.insertOne({
	_id: UUID(),
	Name: "Retire {quantity} {item}",
	Aliases: [
		"Supprime {quantity} {item}",
		"Efface {quantity} {item}"
	],
	Action: "Remove",
	Icon: "remove_circle_outline"
});