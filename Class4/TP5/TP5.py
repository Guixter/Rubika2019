from random import randint;

########## Exercice 5 :
# Le jeu du + ou -.
# Votre programme va commencer par choisir aléatoirement un nombre secret entre 0 et 1000 ; puis le jeu peut commencer.
# Le but du joueur est de trouver le nombre secret, en faisant le moins de tours possibles.
# Pour ce faire, à chaque tour l'utilisateur doit proposer un nombre, et le programme doit lui dire "c'est plus" ou "c'est moins",
# jusqu'à ce que l'utilisateur tombe sur le nombre secret.
# À la fin du jeu, votre programme doit afficher le nombre de tours utilisés par le joueur.
#
# BONUS : Une fois l'algorithme terminé, vous pouvez faire en sorte que le jeu recommence indéfiniment lorsqu'une partie est terminée...

nombreSecret = randint(0, 1000);

