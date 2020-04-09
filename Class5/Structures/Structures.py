######################################
#        STRUCTURES DE DONNÉES
######################################

# Pile (stack) :
stack = [];
stack.append("Pikachu");
stack.append("Miaouss");
stack.append("Évoli");

result = stack.pop();
print(result);

# File d'attente (queue) :
queue = [];
queue.insert(0, "Pikachu");
queue.insert(0, "Miaouss");
queue.insert(0, "Évoli");

result = queue.pop();
print(result);

# Dictionnaire :
dictionary = {
	"eau": "Carapuce",
	"feu": "Salamèche",
	"plante": "Bulbizarre"
};

starterPlante = dictionary["plante"];
print(starterPlante);