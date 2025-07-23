# Reino Numérico - Tower Defense Matemático

Un videojuego educativo de tower defense donde los estudiantes practican matemáticas resolviendo ecuaciones lineales para derrotar enemigos.

## 📖 Descripción del Proyecto

**Reino Numérico** es un juego educativo tipo tower defense desarrollado en Unity que combina entretenimiento con aprendizaje de matemáticas. Los jugadores deben defender su castillo resolviendo problemas matemáticos, principalmente ecuaciones lineales, para eliminar enemigos que avanzan por el mapa.

### 🎯 Características Principales

- **Aprendizaje gamificado**: Resolución de ecuaciones lineales integrada en la mecánica de juego
- **Sistema de hechizos**: Utiliza maná para lanzar hechizos de rayo y congelamiento
- **Múltiples niveles**: Progresión a través de diferentes niveles de dificultad
- **Sistema de puntuación**: Registro de puntajes de estudiantes con tabla de líderes
- **Login estudiantil**: Sistema para identificar y guardar el progreso de cada estudiante
- **Generación dinámica de problemas**: Algoritmo que crea ecuaciones lineales aleatorias
- **Efectos visuales y sonoros**: Experiencia inmersiva con efectos especiales

### 🎮 Mecánicas de Juego

- **Tower Defense**: Los enemigos aparecen en oleadas y siguen un camino predefinido
- **Problemas matemáticos**: Cada enemigo presenta una ecuación lineal que debe resolverse
- **Sistema de maná**: Recurso limitado para usar hechizos especiales
- **Hechizos disponibles**:
  - **Rayo (F)**: Daña a todos los enemigos visibles (25 maná)
  - **Congelamiento (G)**: Congela a todos los enemigos temporalmente (30 maná)

## 🚀 Guía de Instalación

### Requisitos del Sistema

- **Sistema Operativo**: Windows 10/11 (64-bit)
- **Memoria RAM**: Mínimo 4 GB
- **Espacio en disco**: 500 MB disponibles
- **Resolución**: 1024x768 o superior

### Instalación Rápida (Ejecutable)

1. Navega a la carpeta `Ejecutable/`
2. Ejecuta `TDGame.exe`
3. ¡Listo para jugar!

### Instalación para Desarrollo

#### Requisitos de Desarrollo

- **Unity**: Versión 6000.1.8f1 o superior
- **Visual Studio** o **Visual Studio Code** (recomendado para scripts C#)
- **Git** (para control de versiones)

#### Pasos de Instalación

1. **Clonar el repositorio**:
   ```bash
   git clone https://github.com/Angelol72/DSJ_Videojuego.git
   cd DSJ_Videojuego
   ```

2. **Abrir en Unity**:
   - Abrir Unity Hub
   - Hacer clic en "Abrir proyecto"
   - Seleccionar la carpeta `TDGame/`
   - Unity detectará automáticamente la versión requerida

3. **Configurar el proyecto**:
   - Esperar a que Unity importe todos los assets
   - Verificar que no hay errores en la consola
   - Abrir la escena `Main Menu.unity` para comenzar

## 🎯 Guía Rápida de Uso

### Para Estudiantes

1. **Inicio del juego**:
   - Ejecutar `TDGame.exe`
   - Introducir nombre, apellidos y grado en la pantalla de login
   - Seleccionar nivel deseado

2. **Jugabilidad**:
   - Observar los enemigos que aparecen con ecuaciones matemáticas
   - Hacer clic en la respuesta correcta para eliminar el enemigo
   - Usar hechizos con las teclas **F** (rayo) y **G** (congelamiento)
   - Defender el castillo hasta completar todas las oleadas

3. **Navegación**:
   - **ESC**: Pausar/despausar juego
   - **F**: Hechizo de rayo
   - **G**: Hechizo de congelamiento

### Para Educadores

- **Tabla de líderes**: Acceder desde el menú principal para ver el progreso de los estudiantes
- **Datos guardados**: Los puntajes se almacenan automáticamente en `estudiantes.json`
- **Múltiples niveles**: Diferentes niveles de dificultad disponibles

## 🛠️ Guía de Contribución

### Estructura del Proyecto

```
TDGame/
├── Assets/
│   ├── Scenes/           # Escenas del juego
│   ├── Scripts/          # Código C#
│   │   ├── Mecs/        # Mecánicas principales
│   │   ├── Player/      # Controladores del jugador
│   │   ├── Enemy/       # Sistema de enemigos
│   │   ├── UI/          # Interfaz de usuario
│   │   ├── Data/        # Generación de problemas
│   │   └── LeaderBoard/ # Sistema de puntuaciones
│   ├── Prefabs/         # Objetos prefabricados
│   ├── Audio/           # Archivos de sonido
│   └── Art/             # Assets visuales
├── ProjectSettings/     # Configuración de Unity
└── Packages/           # Paquetes de Unity
```

### Para Desarrolladores

#### Configuración del Entorno

1. **Fork del repositorio**:
   ```bash
   git fork https://github.com/Angelol72/DSJ_Videojuego.git
   git clone https://github.com/TU_USUARIO/DSJ_Videojuego.git
   ```

2. **Crear rama de desarrollo**:
   ```bash
   git checkout -b feature/nueva-funcionalidad
   ```

3. **Configurar Unity**:
   - Instalar Unity 6000.1.8f1
   - Abrir el proyecto desde `TDGame/`
   - Verificar que el proyecto compila sin errores

#### Áreas de Contribución

- **Nuevos tipos de problemas matemáticos**: Expandir `ProblemGenerator.cs`
- **Mecánicas de juego**: Añadir nuevos hechizos o enemigos
- **Interfaz de usuario**: Mejorar la experiencia del usuario
- **Sistema de niveles**: Crear nuevos niveles y dificultades
- **Optimización**: Mejorar el rendimiento del juego

#### Proceso de Contribución

1. **Desarrollar la funcionalidad**
2. **Probar exhaustivamente**
3. **Documentar cambios**
4. **Commit y push**:
   ```bash
   git add .
   git commit -m "feat: descripción de la nueva funcionalidad"
   git push origin feature/nueva-funcionalidad
   ```
5. **Crear Pull Request**

### Scripts Principales

- **GameManager.cs**: Controlador principal del juego
- **ProblemGenerator.cs**: Generación de problemas matemáticos
- **TextBallon.cs**: Sistema de preguntas y respuestas
- **SpellController.cs**: Mecánicas de hechizos
- **EstudiantesManager.cs**: Sistema de puntuaciones y estudiantes

## 📊 Sistema de Datos

### Almacenamiento de Estudiantes

Los datos se guardan en formato JSON en `estudiantes.json`:

```json
[
  {
    "nombre": "Juan",
    "apellidos": "Pérez",
    "grado": "5to",
    "puntos": 1500
  }
]
```

### Generación de Problemas

El sistema genera ecuaciones lineales del tipo:
- `ax + b = c` donde el jugador debe encontrar `x`
- Valores aleatorios para `a`, `b` y `c`
- Opciones múltiples con respuestas incorrectas calculadas

## 🤝 Colaboradores

- **Pedro**: Desarrollo de mecánicas principales
- **Salomé**: Diseño de personajes y arte

## 📞 Soporte

Para reportar bugs o sugerir mejoras:
- Crear un issue en el repositorio de GitHub
- Incluir descripción detallada del problema
- Adjuntar capturas de pantalla si es necesario

## 📄 Licencia

Este proyecto es de uso educativo. Ver detalles en el archivo LICENSE.

---

**¡Aprende matemáticas mientras te diviertes defendiendo tu castillo!** 🏰⚡📚
