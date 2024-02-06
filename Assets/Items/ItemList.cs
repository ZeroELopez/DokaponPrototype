public static class ItemList{

public static List<IItem> prefabItems = new List<IItem>();

public static void CreateIteems(){
prefabItems.AddRange(
{
		new Arrow(
name = "Catapult";
damge = 20;
wait = 2;
),
new Arrow(
name = "Rain of Arrows";
damge = 1;
wait = 1;
),
new Food(
name = "";
damge = 20;
wait = 2;
),
new Arrow(
name = "Catapult";
damge = 20;
wait = 2;
),
new Arrow(
name = "Catapult";
damge = 20;
wait = 2;
),
}

);

}

}