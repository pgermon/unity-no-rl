# unity-no-rl

Jeu de survie dans un environnement préhistorique peuplé de dinosaures.

## Génération du terrain
Le terrain qui compose l'environnement est généré de manière procédurale afin d'être le plus naturel possible. Il présente différentes zones qui dépendent de la hauteur telles des plages, des plaines et des montagnes. La génération procédurale du terrain se base sur une carte de hauteur définie à partir d'un bruit de Perlin.

## Gameplay
Dans ce jeu solo, le joueur incarne un TRex initialement petit et son but est de survivre et de grossir en tuant des proies. Le jeu se déroule dans un environnement peuplé de différents types de dinosaures formant une chaîne alimentaire. Certains dinosaures sont des prédateurs carnivores à la recherche de proies tandis que d'autres sont herbivores.  
Les dinosaures peuvent se déplacer librement dans l'environnement et intéragir entre eux en s'attaquant ou s'évitant selon leurs comportements.  
Le but ultime du joueur est de vaincre le boss final incarné par un énorme TRex. Pour cela il devra chasser des proies pour grossir tout en évitant de se faire tuer.  

## Comportements des dinosaures
Chaque dinosaure présente un comportement propre qui est implémneté par un arbre de compoprtement.

### Bases communes
Les dinosaures ne sont pas inactifs et se promènent aléatoirement en cas d'absence de proies ou prédateurs. Certaines stratégies d'attaque ou de défense peuvent également être communes à plusieurs dinosaures.

### Défense
Les dinosaures non carnivores ne chassent pas mais peuvent se défendre en contre-attaquand s'ils ne peuvent pas fuir un prédateur et s'ils sont suffisamment imposants.
Pendant leur errance, ils contrôlent régulièrement la présence de prédateurs dans les environs.
Si un prédateurs se fait menaçant, la fuite se fait principalement dans la direction opposée.

### Attaque
Les prédateurs sont constamment à la recherche d'une proie et ils passent à l'attaque lorsqu'ils en trouvent une.

