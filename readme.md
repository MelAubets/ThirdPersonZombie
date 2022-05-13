# PEC3 - Zombie Platform
## Mel Aubets

En esta PEC se ha realizado un juego de zombis en tercera persona, para hacerlo se han configurado las animaciones correspondientes, gestionadas mediante booleanos. También se han creado distintos scripts. A continuación se explicarán las distintias implementaciones realizadas.

### Animator:

Para generar las animaciones se han creado tres _animator controllers_.

El primero, llamado **character**, es el que gestionará todas las animaciones del personaje durante el juego. Empieza con un estado _Idle_ y puede pasar a un estado _Walking_ o _Jump_, se puede observas que el personaje solo puede correr después de andar, se ha hecho de esta forma para hacer el movimiento más natural y orgánico. También se puede ver que hay dos estados a los que se puede acceder desde cualquier otro; _Hitted_ y _Dying_, esto es así porqué el personaje puede ser herido o asesinado en cualquier momento del juego.

El segundo _animator controller_ que se podrá encontrar se llama **GameOver** y solo tiene dos posibles salidas de _Idle_; _Win_(con una animación de baile) o _Lose_ (con una animación de derrota).

El último controlador es el llamado **Zombie** y es el que gestionará las animaciones del Zombi, este solo podrá andar con _Walking_, atacar con _Attack_, recibir impacto con _Hurted_ y morir con _Die_. Al igual que los anteriores, las distintas transiciones se controlan mediante booleanos.

### Scripts:

En esta PAC se han reutilizado algunos de los scripts realizados en la PAC anterior añadiendo pequeñas modificaciones, también se han creado cuatro scripts totalmente nuevos, pasaremos a explicarlos brevemente a continuación.

Los scripts **ExitGame**, **RestartGame** y **GameOver** son muy simples y sirven para gestionar las escenas no jugables y sus botones. En **GameOver** se utilizan PlayerPrefs definidos en **PlayerHealth** para determinar si el jugador ha ganado o ha perdido.

Los scripts **EnemyAI**, **EnemyHealth**, **FieldOfView** y **PlayerHealth** son reutilizados de la PAC anterior, pero algunos de ellos tienen ciertas modificaciones.

En **EnemyAI** se ha añadido el estado _Dead_ a la máquina de estados y se ha intercambiado la función _Shoot()_ por una función nueva llamada _Attack()_ que gestionará la animación de ataque y el daño realizado al jugador. También se ha creado una coroutine que pondrá el booleano _isHurtedZombi_ a false cada segundo, para asegurar que vuelva a las animaciones normales después de recibir un impacto.

En **EnemyHealth** la única diferencia es la gestión del animator para indicar cuando el zombi está herido y cuando está muerto. En **FieldOfView** no se ha realizado ninguna modificación. En **PlayerHealth**, igual que en **EnemyHealth**, no se han realizado demasiadas modificaciones, se ha añadido gestión de las animaciones y un _PlayerPref_ que será 0 si el jugador pierde y 1 si gana.

Finalmente se encuentra el script **ThirdPersonMovement** que es el más complejo de todos los realizados. En él se reciben los inputs del jugador que indicarán al personaje la animación que debe realizar y también la dirección y velocidad del movimiento, así como el salto y el disparo. Para la gestión del movimiento se ha añadido un _Rigidbody_ al que se le añadirá la velocidad correspondiente al movimiento y una fuerza vertical en caso de salto. También se ha añadido un cálculo sencillo que determinará la dirección a la que debe mirar el personaje.

Finalmente, se han añadido algunas funciones disponibles en el script **GunSystem** de la PAC anterior, cómo _Shoot()_, _ResetShoot()_, _Reload()_ y _ReloadFinished()_. La función _Shoot()_ se ha modificado para que los disparos tengan cierto rango angular y de esta manera facilitar ligeramente el juego.

### Detalles finales:

Finalmente se han añadido dos sistemas de partículas a los disparos, un _MuzzleFlash_ que aparecerá en el arma y un _Flare_ que aparecerá en el enemigo impactado. También se han añadido efectos de sonido para los disparos y la recarga y una música de fondo para hacer el juego más divertido.

### [Vídeo demostrativo](https://youtu.be/LL-hn_3tHGg)

### Assets Utilizados en esta PEC:
- [Toon Gas Station](https://assetstore.unity.com/packages/3d/environments/urban/toon-gas-station-155369)
- [Personajes y Animaciones](https://www.mixamo.com/)
- [Weapons Soldiers Sounds Pack](https://assetstore.unity.com/packages/audio/sound-fx/weapons/weapon-soldier-sounds-pack-29662)
- [First Aid Set](https://assetstore.unity.com/packages/3d/props/first-aid-set-160073)
- [Sci-Fi Weapons](https://devassets.com/assets/sci-fi-weapons/)
- [12x70 Rem Ammo Box](https://assetstore.unity.com/packages/3d/props/weapons/12x70-rem-ammo-box-193342)
- [Standard Assets](https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351)
- [Unity Particle Pack 5.x](https://assetstore.unity.com/packages/essentials/asset-packs/unity-particle-pack-5-x-73777)
- [Basic Metal 6 - Heavy Metal - Royalty Free Music](https://www.youtube.com/watch?v=azBtXTg4DQc&ab_channel=TeknoAXE%27sRoyaltyFreeMusic)

### Tutoriales utilizados en esta PEC:
- [Creating a Third Person Camera using Cinemachine in Unity! (Tutorial)](https://www.youtube.com/watch?v=537B1kJp9YQ&t=147s&ab_channel=Unity)
- [THIRD PERSON MOVEMENT in Unity - Brackeys](https://www.youtube.com/watch?v=4HpC--2iowE&ab_channel=Brackeys)
- [Easy Animations with Mixamo, Jump On Block (to use in Unity) - Omar Santiago](https://www.youtube.com/watch?v=aLUh2jtgQLg&t=2s&ab_channel=OmarSantiago)
- [Unity Tutorial - Combine Animations - Firemind](https://www.youtube.com/watch?v=PCNxZSeNhA4&t=243s&ab_channel=Firemind)
- [Humanoid Avatars - Unity Official Tutorials](https://www.youtube.com/watch?v=pbaOGZzth6g&ab_channel=Unity)