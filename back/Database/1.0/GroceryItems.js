db = connect("localhost:27017/ControlPanelDb")
db.GroceryItems.insertOne({
	_id: UUID(),
	Name: "Pommes de terres",
	Aliases: ["Patates"],
	Icon: "identity"
});
db.GroceryItems.insertOne({
	_id: UUID(),
	Name: "Carottes",
	Aliases: [],
	Icon: "identity"
});