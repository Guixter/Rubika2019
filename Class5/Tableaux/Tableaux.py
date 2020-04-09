######################################
#            TABLEAUX
######################################

# Un tableau peut contenir n'importe quel type, y compris des strings !
monTableau = ["Hello ", "World"];
print(len(monTableau));
print(monTableau[0]);
print(monTableau[1], "\n");

# Python plante si on essaie de lire un index qui n'existe pas dans le tableau
# print(monTableau[2]);

# On peut écrire dans le tableau pour remplacer des anciennes valeurs
monTableau[1] = "Rubika";
print(len(monTableau));
print(monTableau[0]);
print(monTableau[1], "\n");

# Écrire à un index non existant entrainera un plantage aussi
# monTableau[2] = "test";

# On peut ajouter un élément à la fin du tableau avec la fonction append
monTableau.append(" :) ");
print(monTableau, "\n");

# On peut aussi retirer un élément du tableau avec la fonction pop
monTableau.pop(1);
print(monTableau, "\n");


#### TODO
# Écrire un programme qui demande 3 valeurs à l'utilisateur, et les stocke dans un seul tableau.
# Ensuite, vous pourrez afficher le contenu du tableau.
