# Flappy Calabaza

Juego 2D tipo Flappy Bird con temática de Halloween, hecho en Unity 6 (6000.6.0f1) con URP 2D.

Una calabaza con alas de murciélago vuela por el panteón esquivando lápidas.

## Controles

| Acción | Tecla |
|---|---|
| Volar / empezar / reintentar | Espacio, W, Flecha arriba o clic izquierdo |
| Salir | Esc |

## Cómo se juega

- Cada lápida que pasas suma 1 punto.
- Si chocas con una lápida o con el suelo, pierdes.
- La velocidad sube poco a poco conforme haces puntos.
- El récord se guarda en PlayerPrefs.

## Estructura

- `Assets/Scenes/Juego.unity`: escena principal.
- `Assets/Scripts`:
  - `GameManager`: estados (inicio, jugando y fin), puntos, récord y UI.
  - `Jugador`: salto, gravedad, rotación, aleteo y choques.
  - `Generador`: crea las lápidas a una altura aleatoria.
  - `Obstaculo`: mueve las lápidas y las borra cuando salen de la pantalla.
  - `Desplazar`: hace el scroll infinito del fondo y del suelo.
- `Assets/Prefabs/Obstaculo.prefab`: par de lápidas con la zona de punto.
- `Assets/Sprites` y `Assets/Sonidos`: arte y efectos de sonido.

## Repositorio y MCP

- Asset Serialization está en **Force Text**.
- Se usa el `.gitignore` oficial de Unity.
- Se suben las carpetas `Assets`, `Packages` y `ProjectSettings`.
- MCP for Unity está instalado desde `https://github.com/CoplayDev/unity-mcp.git?path=/MCPForUnity#main`.
