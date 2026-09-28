# Technical Design Document — Proyecto IA

## 1. Motor y herramientas

- Motor: Unity 6 LTS
- Render Pipeline: Universal Render Pipeline (URP)
- Lenguaje: C#
- Modelado 3D: Blender
- Control de versiones: Git + GitHub

## 2. Estructura del proyecto

Assets/
├── _Project/         ← Todo el contenido propio
│   ├── Art/
│   ├── Audio/
│   ├── Code/
│   ├── Prefabs/
│   ├── Scenes/
│   └── Settings/
├── ThirdParty/       ← Assets externos
└── Documentation/    ← Esta carpeta

## 3. Sistemas implementados

### Input System
- Paquete: com.unity.inputsystem (nuevo sistema)
- Asset: Assets/_Project/Settings/InputSystem_Actions.inputactions
- Clase generada: InputSystem_Actions.cs
- Action Map usado: Player
- Acciones activas: Move

### Movimiento del jugador
- Script: Assets/_Project/Code/Gameplay/PlayerMovement.cs
- Velocidad: 5 m/s
- Rotación: 15 (Slerp)
- Input: Vector2 desde acción "Move"
- Dirección: relativa a la cámara (camForward y camRight, ignorando pitch)
- Referencia: campo cameraTransform asignado en el Inspector

## 4. Convenciones

- Nombres de scripts: PascalCase (PlayerMovement)
- Nombres de variables privadas: camelCase (_moveSpeed)
- Nombres de escenas: Nombre_Subnombre (Dev_Room_01)
- Carpetas: PascalCase sin espacios (Gameplay, Characters)
- Ramas Git: main, dev, feature/nombre

## 5. Decisiones técnicas tomadas

| Fecha       | Decisión                                | Motivo |
|-------------|-----------------------------------------|--------|
| 2026-09-28  | Usar URP en lugar de Built-in           | Es el sistema moderno y escalable |
| 2026-09-28  | Usar el nuevo Input System              | Más flexible y multiplataforma |
| 2026-09-28  | Separar _Project de ThirdParty          | Claridad legal y organizativa |


### Detección de suelo
- Origen del raycast: transform.position + Vector3.down * 0.9f
- Distancia: 0.3 m
- Ground Mask: ~0 (todas las capas)
- Debug.DrawRay en desarrollo: verde (suelo) / rojo (aire)

### Salto
- Fuerza: 5 (ForceMode.Impulse)
- Solo si isGrounded es True