# unity-no-rl

## Comportements
Le comportement de chaque dinosaure est géré par un _BehaviourTree_.
Cette méthode détermine les objectifs et les actions, mais le déplacement et l'exécution des actions reste géré par une variante de `DinosaureBB`.

### Bases communes
Les dinosaures ne sont pas inactif et se promènent aléatoirement en cas d'absence de proies ou prédateurs.
Ce comportement est géré par le bloc `DinoWander`.

### Défense
Les dinosaures non carnivores ou de grosse tailles ne chassent pas mais doivent se défendre s'ils ne peuvent pas fuir le prédateur.
Pendant leur errance, ils contrôlent régulièrement la présence de prédateurs dans les environs.
Si un prédateurs se fait menaçant, la fuite se fait principalement dans la direction opposée.

C'est le cas du _parasaurolophus_ et le _triceratops_.

### Attaque
Les gros prédateurs se contentent de trouver une proie et l'attaquer.

Si le dinosaure n'a pas actuellement de proie, il en cherche dans son entourage.
La proie choisie est généralement la plus proche.
Il faut ensuite s'en approcher (`MoveToGameObject`) et l'attaquer dès qu'elle est à porté de machoires (bloc `Attack`).

### Intermédiaire
Le vélociraptor est un dinosaure de petite taille et rapide.
Il est la proie des gros dinosaures et attaque les petits.
En présence d'un prédateur, il peut continuer sa chasse si l'attaquant reste loin.

