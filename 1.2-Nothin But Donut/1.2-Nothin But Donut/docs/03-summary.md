---
week: 3
type: summary
title: "Week 3 Transcript Summary"
---

# CST 326 - Week 3 Transcript Summary

## Course Context

**Week Focus:** Sound & GUI, Pong Part 2  
**Sessions:**
- 03-1: Tuesday In-Person - Collision Modes, Audio Basics, "Nothing But Donut" Project
- 03-2: Thursday Online - Pong Solution Review, Spatial Audio, Audio Mixers, UI/TextMeshPro, Billboarding

---

## Session 03-1: Collision Modes, Audio, and Scene Exploration (Tuesday In-Person)

### Collision Detection Modes (Video Overview)

Unity provides 4 collision detection options with different trade-offs:

#### Discrete (Default)
- **Cost:** Cheapest computationally
- **How it works:** Checks for collisions at each physics step
- **Downside:** Fast-moving objects can tunnel through objects between steps
- **Mitigation:** Reducing fixed timestep (e.g., 0.02 → 0.01) doubles calculations but hurts performance

#### Continuous
- **How it works:** Sweeps collider bounds forward linearly (like a box cast) to projected next-frame position
- **Benefit:** Ensures collisions even at high speeds
- **Limitations:** 
  - More expensive than discrete
  - Only collides with **static objects** (no Rigidbody)
  - Doesn't handle angular velocity (rotation)

#### Continuous Dynamic
- **How it works:** Same as Continuous but also detects Rigidbody colliders
- **Cost:** Most expensive collision mode
- **Use case:** Multiple high-speed objects colliding (e.g., two opposing projectiles)

#### Continuous Speculative
- **How it works:** Draws bounding box around current and predicted future position (linear + angular motion)
- **Benefits:**
  - Works with both static and dynamic colliders
  - Cheaper than other continuous modes
  - Handles angular velocity
- **Drawback:** "Ghost collisions" - fast objects may appear to collide without touching

#### Collision Mode Selection Cheat Sheet
1. **Use Discrete** if possible (default)
2. If tunneling or fast angular motion → try **Speculative**
3. If ghost collisions or still tunneling → use **Continuous**
4. If multiple fast dynamic objects must collide → **Continuous Dynamic** (limit quantity)

---

### "Nothing But Donut" Project Exploration

The class explored a pre-made basketball/trampoline demonstration project to understand script architecture and Unity concepts.

#### Scene Components
- **Hand:** Controls ball release and reset
- **Ball:** Child of Hand, with Rigidbody added dynamically
- **Trampolines:** Bounce the ball toward a hoop
- **Hoop:** Torus with mesh collider and trigger zone for scoring
- **Score Manager:** Handles scoring and audio

#### Hand Script Analysis
```csharp
public class Hand : MonoBehaviour
{
    public Transform ball;  // Reference to ball's transform
    private Rigidbody ballRb;
    
    void Start()
    {
        // Dynamically add Rigidbody at runtime
        ballRb = ball.gameObject.AddComponent<Rigidbody>();
        ballRb.useGravity = false;  // Ball floats until released
    }
    
    public void Release()
    {
        ballRb.useGravity = true;  // "Let go" of ball
    }
    
    public void Reset()
    {
        ball.position = gameObject.transform.position;
        ballRb.linearVelocity = Vector3.zero;
    }
}
```

**Key Concepts:**
- `AddComponent<T>()` - Dynamically add components at runtime
- Referencing components on other objects (not just siblings)
- Controlling gravity via `useGravity` property

#### Input Manager Script
```csharp
public class InputManager : MonoBehaviour
{
    public Hand hand;
    public Trampoline trampoline;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            hand.Release();
        if (Input.GetKeyDown(KeyCode.R))
            hand.Reset();
        
        // Trampoline movement via axis
        float movement = Input.GetAxis("Trampoline");
        trampoline.transform.Translate(Vector3.right * movement * Time.deltaTime);
    }
}
```

#### Trampoline Script - Force Application
```csharp
public class Trampoline : MonoBehaviour
{
    public float impulseStrength = 14f;
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{name} collided with {collision.gameObject.name}");
        
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.up * impulseStrength, ForceMode.Impulse);
    }
}
```

**Key Concept: `transform.up` for Local Direction**
- `transform.up` returns the object's local "up" direction in world space
- Rotated objects have different local axes than world axes
- Use this for directional forces relative to object orientation
- Also available: `transform.forward`, `transform.right`

---

### Kinematic vs Dynamic vs Static Colliders

#### Collider Types
| Type | Components | Behavior |
|------|-----------|----------|
| **Static Collider** | Collider only (no Rigidbody) | Immovable, physics aware |
| **Dynamic Collider** | Collider + Rigidbody | Controlled by physics |
| **Kinematic Collider** | Collider + Rigidbody (isKinematic = true) | Script-controlled, others react to it |

#### When to Use Kinematic
- Object should affect other physics objects
- Object should NOT be controlled by physics (gravity, forces)
- Movement is script-driven (animation, player input)
- Example: Moving platforms, doors, elevators

**Historical Note:** "Kinematics" in physics describes motion without explaining forces (pre-Newton). In Unity, kinematic objects move by geometry (script), not by forces.

```csharp
// Making an object kinematic
Rigidbody rb = GetComponent<Rigidbody>();
rb.isKinematic = true;  // Physics won't control this, script will
```

---

### Camera Control

#### Align Camera with Scene View
Quick way to position camera at your current editor view:
- **Menu:** GameObject → Align With View
- **Shortcut:** `Ctrl + Shift + F`

#### Camera Look At Script
```csharp
public class CameraController : MonoBehaviour
{
    public Camera cam;
    public Transform ballTransform;
    
    void Update()
    {
        cam.transform.LookAt(ballTransform.position);
    }
}
```

**Tip:** `LookAt()` rotates the object so its positive Z-axis points at the target.

---

### Audio Fundamentals

#### Adding Audio to Unity

**Audio Source Component Properties:**
- **Audio Clip:** The sound file to play
- **Play On Awake:** Start playing when object activates
- **Loop:** Repeat continuously
- **Volume:** 0-1 range
- **Spatial Blend:** 0 (2D) to 1 (3D spatial)

#### Recording Custom Sounds (Audacity)
1. Record sound effect
2. Trim/edit as needed
3. Apply compression if needed (Effects → Compressor)
4. Export as MP3 or WAV
5. Drag file into Unity project

#### Audio File Formats
- **WAV:** Uncompressed, higher quality, larger file, lower latency
- **MP3:** Compressed, smaller file, slight quality loss

#### Audio Load Types
| Type | Memory | Latency | Use Case |
|------|--------|---------|----------|
| **Decompress On Load** | High | Low | Short sound effects (responsive) |
| **Compressed In Memory** | Medium | Medium | Medium sounds |
| **Streaming** | Low | High | Long music tracks |

**Rule:** For responsive sound effects, use "Decompress On Load"

#### Playing Sounds on Collision
```csharp
public class Trampoline : MonoBehaviour
{
    public AudioClip boingClip;
    
    void OnCollisionEnter(Collision collision)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = boingClip;
        audioSource.Play();
    }
}
```

**Important:** Each object needing sound requires its own Audio Source component.

---

### Score Detection with Triggers

#### Hoop Trigger Setup
- **Outer ring:** Mesh Collider (for physics bouncing)
- **Inner zone:** Box Collider with "Is Trigger" checked (for detection)

```csharp
public class Score : MonoBehaviour
{
    public ScoreManager scoreManager;
    
    void OnTriggerEnter(Collider other)
    {
        scoreManager.AddScore();
    }
}
```

#### Score Manager with Audio
```csharp
public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public AudioClip crowdCheer;
    private AudioSource audioSource;
    private int score = 0;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void AddScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
        audioSource.clip = crowdCheer;
        audioSource.Play();
    }
}
```

**`Awake()` vs `Start()`:**
- `Awake()` - Called immediately when object created (like constructor)
- `Start()` - Called on first frame object is enabled
- Use `Awake()` for initialization that other scripts depend on

---

## Session 03-2: Spatial Audio, Mixers, UI, and Billboarding (Thursday Online)

### Pong Solution Review (Key Highlights)

#### Game Manager Pattern
```csharp
public class GameManager : MonoBehaviour
{
    public Transform ball;
    public GoalTrigger leftGoal, rightGoal;
    private Vector3 ballStartPosition;
    private int leftScore = 0, rightScore = 0;
    
    void Start()
    {
        ballStartPosition = ball.position;
        // Launch ball to the right initially
        LaunchBall(1);
    }
    
    public void OnGoalScored(GoalTrigger goal)
    {
        if (goal == leftGoal)
            rightScore++;
        else
            leftScore++;
        
        // Ball goes toward scorer (who was scored on)
        int direction = (goal == leftGoal) ? -1 : 1;
        ResetBall(direction);
    }
    
    void ResetBall(int direction)
    {
        ball.position = ballStartPosition;
        // Random slight angle (20 degrees)
        Vector3 launchDir = Quaternion.Euler(0, 0, 20) * (Vector3.right * direction);
        ball.GetComponent<Rigidbody>().linearVelocity = launchDir * initialSpeed;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.GetComponent<TrailRenderer>()?.Clear();
    }
}
```

#### Paddle Bounce Direction Math
```csharp
void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Ball"))
    {
        Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
        
        // Calculate where ball hit paddle (0-1 range)
        float hitY = collision.contacts[0].point.z;
        float paddleMin = transform.position.z - paddleHeight / 2;
        float paddleMax = transform.position.z + paddleHeight / 2;
        float hitPercent = (hitY - paddleMin) / (paddleMax - paddleMin);
        
        // Remap 0-1 to -1 to +1 (symmetric around center)
        float bounceDirection = (hitPercent - 0.5f) * 2f;
        
        // Calculate new velocity
        float currentSpeed = collision.relativeVelocity.magnitude;
        float newSpeed = currentSpeed * speedMultiplier;
        
        // Direction: flip X, rotate by bounce angle (max 60 degrees)
        Vector3 baseDir = new Vector3(-Mathf.Sign(ballRb.linearVelocity.x), 0, 0);
        Quaternion rotation = Quaternion.Euler(bounceDirection * maxBounceAngle, 0, 0);
        Vector3 newDir = rotation * baseDir;
        
        ballRb.linearVelocity = newDir.normalized * newSpeed;
    }
}
```

**Key Math Concept: Remapping Ranges**
- 0-1 range → Common for percentages
- -1 to +1 range → Common for symmetric values (perturbations)
- Formula: `newValue = (oldValue - 0.5f) * 2f`

#### F Suffix on Float Literals
```csharp
float x = 2.5f;   // Correct: explicitly float
float y = 2.5;    // Warning: 2.5 is double, implicit cast to float
```
- C# defaults decimal literals to `double` (64-bit)
- `f` suffix makes it `float` (32-bit)
- Unity uses `float` extensively

---

### Pong Part 2 Assignment Requirements

#### Sound Effects (10 points)
- **7 points:** Basic paddle hit sound
- **+3 points:** Sound variation (pitch change based on velocity, random variations)

**Footstep Sound Design Principle:** Never play identical sounds repeatedly—use slight variations for organic feel.

#### Power-Ups (10 points)
- Implement creative power-ups triggered during gameplay
- Examples: Shrink/grow paddles, zigzag ball, camera effects
- **Full points:** 2 different power-ups

#### Score Display (7 points)
- **5 points:** Show score on screen (overlay or world space)
- **+2 points:** Text effects (outline, underline, etc.)

#### Bonus "Juice" (up to 5 points)
- Camera shake on impact
- Particle effects (ball trail, impact puffs)
- AI paddle control
- Googly eyes that track ball
- Visual background effects after score
- Time manipulation (slow-mo)
- Custom shader effects
- High score persistence
- New Input System

---

### Unity Asset Store Workflow

#### Finding Free Assets
1. Go to assetstore.unity.com
2. Filter by "Free"
3. Search for desired asset
4. Click "Add to My Assets"
5. Open in Unity

#### Importing via Package Manager
1. **Window → Package Manager**
2. Select "My Assets" tab
3. Find asset and click "Import"
4. Select components to import

#### Render Pipeline Converter (Fix Magenta Materials)
Old assets may use incompatible shaders (Standard Shader → magenta).

**Fix:**
1. **Window → Rendering → Render Pipeline Converter**
2. Check all conversion options
3. Click "Initialize and Convert"

This automatically updates old materials to URP-compatible shaders.

---

### 3D Spatial Audio

#### Spatial Blend Setting
- **0 (2D):** Sound plays at constant volume regardless of position
- **1 (3D):** Sound volume/panning affected by distance and position

#### Audio Listener
- Usually attached to Main Camera
- Represents "ears" in the scene
- Only one should be active
- Distance from Audio Source to Listener affects volume

#### Distance Falloff Curves
Configure how volume changes with distance:

| Rolloff Type | Behavior |
|--------------|----------|
| **Logarithmic** | Realistic falloff (default) |
| **Linear** | Proportional distance reduction |
| **Custom** | Bezier curve for precise control |

**Properties:**
- **Min Distance:** Full volume range
- **Max Distance:** Sound no longer audible beyond this

#### Doppler Effect
- Pitch shifts based on relative velocity between source and listener
- Moving toward → Higher pitch
- Moving away → Lower pitch

```csharp
// Doppler Level on Audio Source
// 0 = No doppler
// 1 = Normal doppler
// Higher = Exaggerated doppler
```

**Important Bug:** If Rigidbody is on same object but not controlling movement (script moves transform directly), doppler may not work correctly. Remove Rigidbody or use kinematic.

---

### Transform.forward for Local Direction Movement

```csharp
public class CarController : MonoBehaviour
{
    public float maxOffset = 25f;
    private Vector3 startingPosition;
    
    void Start()
    {
        startingPosition = transform.position;
    }
    
    void Update()
    {
        // Oscillate along local forward direction (works at any rotation)
        float oscillation = Mathf.Sin(Time.time);  // -1 to +1
        Vector3 forward = transform.forward;
        Vector3 offset = forward * oscillation * maxOffset;
        transform.position = startingPosition + offset;
    }
}
```

**Key Insight:** Using `transform.forward` makes movement work regardless of object rotation. Axis-specific code (just modifying Z) breaks when rotated.

---

### Audio Mixers

#### Purpose
- Coordinate multiple audio sources
- Group sounds by category (SFX, music, dialogue)
- Apply effects to entire groups
- Easier volume balancing

#### Creating Audio Mixer
1. **Project → Create → Audio Mixer**
2. Double-click to open Mixer window
3. Select "Master" group
4. Click "+" under Groups to add channels

#### Group Hierarchy Example
```
Master
├── Car Sounds
├── Background Music
└── SFX
```
Children are affected by parent volume changes.

#### Routing Audio Sources
1. Select Audio Source
2. Find "Output" property
3. Drag mixer group (e.g., "Car Sounds") into slot

#### Mixer Controls
| Button | Function |
|--------|----------|
| **S (Solo)** | Only hear this channel |
| **M (Mute)** | Silence this channel |
| **B (Bypass)** | Skip all effects on channel |

**Edit in Play Mode:** Click record button to adjust values while playing.

#### Audio Effects
Stack effects on mixer groups:
- **Reverb:** Echo/room simulation
- **Chorus:** Thickening effect
- **Pitch Shifter:** Raise/lower pitch
- Many more...

Effects are applied in order (top to bottom).

---

### One-Shot Audio (Layered Sounds)

When Audio Source is already playing something (e.g., engine), use `PlayOneShot()` to layer additional sounds without interrupting:

```csharp
public class CarController : MonoBehaviour
{
    public AudioClip hornClip;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(hornClip);  // Plays on top of current audio
        }
    }
}
```

**`Play()` vs `PlayOneShot()`:**
- `Play()` - Stops current clip, plays assigned clip
- `PlayOneShot(clip)` - Layers on top, doesn't interrupt

---

### UI System with TextMeshPro

#### Canvas Render Modes

| Mode | Description | Use Case |
|------|-------------|----------|
| **Screen Space - Overlay** | Always on top, scales with screen | HUD, menus |
| **Screen Space - Camera** | Rendered at distance from camera | UI with perspective |
| **World Space** | Exists in 3D space | Name tags, health bars |

#### Creating World Space UI
```
1. Right-click Hierarchy → UI → Text - TextMeshPro
2. Select Canvas → Set Render Mode to "World Space"
3. Assign Event Camera
4. Resize canvas (width/height in meters, not pixels!)
```

**Scale Difference:**
- Screen Space: 1 unit = 1 pixel
- World Space: 1 unit = 1 meter

#### Rect Transform Anchoring

**Anchor Presets (click icon in corner):**
- **Center 3×3:** Position relative to single point
- **Outer options:** Position relative to edges

**Anchor Triangles:**
- Define what coordinates are relative to
- Drag triangles to change reference point
- Edge-anchored elements stretch with canvas

**When anchored to edges:**
- Position becomes: Left, Top, Right, Bottom offsets
- Element stretches with parent

#### TextMeshPro Settings
| Setting | Effect |
|---------|--------|
| **Auto Size** | Fit text to bounding box |
| **Min/Max Font Size** | Limits for auto-sizing |
| **Alignment** | Horizontal/vertical positioning |
| **Wrapping** | Enable/disable line wrapping |
| **Overflow** | What happens when text exceeds bounds |

#### Custom Font Assets
1. Download .ttf or .otf font file
2. **Window → TextMeshPro → Font Asset Creator**
3. Drag font file to "Source Font"
4. Click "Generate Font Atlas"
5. Save asset
6. Assign to TextMeshPro component

#### Text Effects (Material Properties)
- **Face Color:** Main text color
- **Outline:** Border around characters
- **Underlay:** Shadow/glow behind text
- **Lighting/Bevel:** 3D appearance
- **Softness:** Blur edges

---

### Billboarding

#### What is Billboarding?
Making an object always face the camera (like roadside billboards).

**Types:**
- **Spherical:** Rotates on all axes to face camera
- **Cylindrical:** Only rotates around Y-axis (stays upright)

#### Basic Look-At Billboarding
```csharp
public class Billboard : MonoBehaviour
{
    public Camera cam;
    public Canvas distanceCanvas;
    
    void Update()
    {
        distanceCanvas.transform.LookAt(cam.transform.position);
    }
}
```

**Issue:** Text may appear backwards because `LookAt()` points positive Z toward target. Fix by scaling child text by -1 on one axis, or adjust calculation.

#### Cylindrical Billboard (Y-Axis Only)
```csharp
void Update()
{
    // Get direction to camera
    Vector3 toCamera = cam.transform.position - distanceCanvas.transform.position;
    
    // Create full look-at rotation
    Quaternion billboardRotation = Quaternion.LookRotation(toCamera);
    
    // Extract only Y rotation (yaw)
    Vector3 billboardEulers = billboardRotation.eulerAngles;
    
    // Apply only Y rotation (keeps upright)
    billboardRotation = Quaternion.Euler(0, billboardEulers.y, 0);
    distanceCanvas.transform.rotation = billboardRotation;
}
```

**Why Cylindrical?**
- Text stays upright regardless of camera height
- More readable
- Natural for name tags, labels

#### Updating UI Text from Script
```csharp
public class CarController : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    public Camera cam;
    
    void Update()
    {
        // Calculate distance
        Vector3 displacement = cam.transform.position - transform.position;
        float distance = displacement.magnitude;
        
        // Update text
        distanceText.text = $"Distance: {distance:F1}";  // F1 = 1 decimal place
    }
}
```

---

### Navigation Shortcuts Recap

| Shortcut | Action |
|----------|--------|
| `Ctrl + Shift + F` | Align selected camera to Scene view |
| `Shift + F` | Lock Scene view to follow selected object |
| `F` | Frame/focus on selected object |
| Double-click hierarchy | Enter prefab edit mode |

---

### Rider IDE Tips

#### Finding Script Usages
- Click "usages" annotation above methods
- Shows all places calling this method
- Click to navigate directly

#### Go to Declaration
- Right-click method/variable → "Go to Declaration"
- Jump to definition

#### Debug Mode in Inspector
- Right-click Inspector tab → Debug
- Shows internal properties and actual script class names

---

## Key Takeaways

1. **Collision Detection modes** trade off performance vs accuracy—use Discrete unless needed
2. **Kinematic Rigidbodies** let scripts control objects while maintaining physics interactions
3. **transform.up/forward/right** give local directions in world space—essential for orientation-independent code
4. **Audio Load Types** affect latency—use Decompress On Load for responsive sound effects
5. **Audio Mixers** organize sounds into manageable groups with shared controls and effects
6. **PlayOneShot()** layers sounds without interrupting current playback
7. **World Space Canvas** puts UI elements in 3D space (for billboards, name tags)
8. **Rect Transform anchors** control positioning relative to edges or points
9. **Billboarding** keeps objects facing camera—cylindrical version maintains upright orientation
10. **Euler angles** can be extracted from quaternions to isolate specific rotation axes

---

## Homework for Week 3

### Pong Part 2 (Due Next Week)
- Add paddle hit sound effects with variation
- Implement 2 creative power-ups
- Display score on screen with text effects
- Complete "Nothing But Donut" exercise (3 points)

### Video Showcase Requirements
- 60+ seconds gameplay demonstration
- 120+ seconds technical discussion
- Under 5 minutes total
- Explicitly call out extra credit features

---

## Assets and Resources Mentioned

### Free Sound Sites
- **freesound.org** - Sound effects with loop-friendly options
- **incompetech.com** - Royalty-free music by Kevin MacLeod

### Unity Asset Store
- Free car model (Promedia Car Controller)
- Filter by "Free" for no-cost assets

### Editor Tools
- **Audacity** - Free audio recording/editing software
- **Render Pipeline Converter** - Fix old materials for URP






