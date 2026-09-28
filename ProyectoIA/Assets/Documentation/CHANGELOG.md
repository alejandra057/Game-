# Changelog — Proyecto IA

Todos los cambios notables de este proyecto se documentan aquí.
Formato basado en Keep a Changelog y Semantic Versioning.

## [0.0.1] — 2026-09-28

### Añadido
- Estructura inicial de carpetas en Assets/_Project, ThirdParty y Documentation
- Proyecto Unity con URP y nuevo Input System
- Escena Dev_Room_01 con habitación básica (suelo, 4 paredes, techo)
- Iluminación: Directional Light + CeilingLight
- Personaje temporal (cápsula) con Rigidbody
- Script PlayerMovement usando el nuevo Input System
- Input Actions con Action Map "Player" y acción "Move"
- Documentación inicial: GDD, TDD, CHANGELOG, Licenses

### Cambiado
- Active Input Handling cambiado a "Input System Package (New)"

### Pendiente
- Cámara en tercera persona
- Reemplazar cápsula por modelo de Blender
- Sistema de desafíos


## [0.0.3] — 2026-09-28

### Añadido
- Movimiento relativo a la cámara (el jugador se mueve hacia donde mira la cámara)
- Referencia cameraTransform en PlayerMovement (inyección por Inspector)
- Normalización de vectores de movimiento horizontal
- Salto funcional con Space
- Sprint con Shift (x2 velocidad)
- Camera Collision (SphereCast)
- Escena MainMenu con botones Jugar, Opciones, Salir
- Escena MainMenu añadida a Build Settings (índice 0)
- HUD de juego con StatusText y InteractionPrompt (oculto por defecto)
- Menú de pausa con Continuar, Menú principal, Salir
- Pausa con Esc, libera el cursor y detiene el tiempo
- Scripts MainMenuController, GameHUDController, PauseMenuController

### Modificado
- PlayerMovement.cs reescrito para usar los ejes camForward y camRight
- rotationSpeed aumentado a 15 para giros más ágiles
- Raycast de suelo ahora parte desde los pies del jugador (no desde el centro)
- groundCheckDistance ajustado a 0.3
- Dev_Room_01 ahora carga desde el menú principal


