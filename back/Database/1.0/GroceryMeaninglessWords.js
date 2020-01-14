db = connect("localhost:27017/ControlPanelDb");
db.GroceryMeaninglessWords.insertOne({
	_id: UUID(),
	Name: "de"
});
db.GroceryMeaninglessWords.insertOne({
	_id: UUID(),
	Name: "du"
});