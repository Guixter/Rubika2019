######################################
#            CONDITIONS
######################################

ageUtilisateur = int(input("Quel est votre âge ?"));

if ageUtilisateur > 18:
	print("Vous êtes majeur.");
elif ageUtilisateur > 21:
	print("Vous avez le droit de boire de l'alcool aux USA.");
else:
	print("Vous êtes mineur.");

#### TODO
# Écrire un programme qui demande une valeur à l'utilisateur.
# Si la valeur entrée est paire, vous devez afficher "Ce nombre est pair".
# Si la valeur entrée est impaire, vous devez afficher "Ce nombre est impair."