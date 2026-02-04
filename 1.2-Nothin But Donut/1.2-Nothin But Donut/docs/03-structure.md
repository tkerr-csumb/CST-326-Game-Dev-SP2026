---
week: 3
type: structure
title: "Week 3 Structure"
topics: ['Audio', 'UI']
project: "Pong Pt. 2"
---

# CST 326 – Week 3 Structure

## 1. Purpose & Tone (Unassessed Foundations)
- Transition from basic physics to audio and visual feedback systems.
- Build confidence with Unity's audio pipeline and UI frameworks.
- Reinforce the importance of polish and "juice" in game feel.
- Demonstrate how sound variation creates organic, non-repetitive experiences.
- Introduce event-based communication patterns for decoupled game architecture.
- Normalize iteration: tune values, listen, and adjust until it feels right.

## 2. Weekly Themes
**Tuesday:** Collision detection deep dive + Audio fundamentals + "Nothing But Donut" mini-project  
**Thursday:** Pong solution review + Spatial audio + Audio mixers + UI/TextMeshPro + Billboarding

## 3. Weekly Learning Outcomes (Measured)

### Outcome 1 — Collision Detection Mode Selection
Students can differentiate between Discrete, Continuous, Continuous Dynamic, and Continuous Speculative collision modes and select the appropriate mode based on object behavior and performance requirements.

### Outcome 2 — Audio System Fundamentals
Students can configure Audio Source components, assign Audio Clips, and trigger sounds programmatically via `Play()` and `PlayOneShot()` methods with appropriate load type settings for responsive playback.

### Outcome 3 — Spatial Audio Configuration
Students can configure 3D spatial audio using Spatial Blend, distance falloff curves, and Audio Listener placement to create positional sound effects.

### Outcome 4 — Audio Mixer Routing
Students can create Audio Mixer groups, route Audio Sources to specific channels, and apply effects for organized sound management.

### Outcome 5 — UI Canvas and TextMeshPro
Students can create UI elements using Canvas (Screen Space vs World Space), configure Rect Transform anchoring, and style text using TextMeshPro components with custom fonts and effects.

### Outcome 6 — Billboarding for World-Space UI
Students can implement billboarding (spherical and cylindrical) to keep UI elements facing the camera using `LookAt()` and Quaternion manipulation.

### Outcome 7 — Dynamic vs Kinematic Rigidbodies
Students can distinguish between dynamic, kinematic, and static colliders and select the appropriate configuration for script-controlled vs physics-controlled objects.

## 4. Activities (Not Outcomes)

### Tuesday – Collision Modes, Audio, and "Nothing But Donut" (In-Person)
- Video overview of collision detection modes (Discrete, Continuous, Continuous Dynamic, Continuous Speculative).
- Explore pre-made "Nothing But Donut" project to analyze script architecture:
  - Hand script with `AddComponent<Rigidbody>()` for runtime physics.
  - Trampoline script with force application using `transform.up`.
  - Score detection with trigger zones.
- Discuss kinematic vs dynamic Rigidbodies and when to use each.
- Introduction to Audio Source components and Audio Clips.
- Record custom sounds with Audacity (optional demonstration).
- Implement collision-triggered audio playback.
- Cover audio load types: Decompress On Load vs Streaming.

### Thursday – Spatial Audio, Mixers, UI, and Billboarding (Online)
- Review Pong Part 1 solution with discussion of:
  - Paddle bounce direction math (hit position percentage).
  - Speed multiplication on collision.
  - Remapping 0–1 range to -1 to +1 for symmetric values.
- Demonstrate spatial audio setup:
  - Spatial Blend slider (2D to 3D).
  - Distance falloff curves (Logarithmic, Linear, Custom).
  - Doppler effect configuration.
- Introduce Audio Mixers:
  - Creating mixer groups (SFX, Music, etc.).
  - Routing Audio Sources to mixer channels.
  - Adding effects (Reverb, Chorus, Pitch Shifter).
  - `PlayOneShot()` for layered audio without interruption.
- UI system walkthrough:
  - Canvas render modes (Screen Space - Overlay, Screen Space - Camera, World Space).
  - Rect Transform anchoring and positioning.
  - TextMeshPro setup with custom font assets.
  - Text effects (outline, underlay, glow).
- Billboarding techniques:
  - Basic `LookAt()` implementation.
  - Cylindrical billboarding (Y-axis only) for upright text.

## 5. Norms (Lightweight, Not Over-Structured)
- Emphasize sound variation: never play identical sounds repeatedly (footstep principle).
- Use `PlayOneShot()` for layered sounds that shouldn't interrupt main audio.
- Set Start Color to WHITE when using Color Over Lifetime in particles (colors multiply).
- Prefer "Decompress On Load" for responsive sound effects.
- Keep Console visible for debugging audio triggers.
- Use the Render Pipeline Converter to fix magenta materials from old assets.

## 6. Deliverables for Week 3

### Tuesday
- Complete "Nothing But Donut" exercise with:
  - Ball release and reset via keyboard input.
  - Trampolines applying force on collision.
  - Score detection with trigger zones.
  - Audio feedback on scoring.

### Thursday
- Continue Project 1: Pong Part 2 (GitHub repo + Video Showcase)
- Requirements structure (30 points + 5 bonus):
  - Sound Design (10 pts): Paddle hit sounds with variation (pitch/position-based).
  - Power-Ups (10 pts): 2+ game-altering power-ups (speed change, paddle size, etc.).
  - Scoring Display (7 pts): Score on Canvas with text effects.
  - "Nothing But Donut" (3 pts): Complete in-class exercise shown in video.
  - Bonus "Juice" (up to 5 pts): Camera shake (Perlin noise), impact effects, AI paddle, googly eyes, background changes, time manipulation, custom shaders, high score persistence.

### Video Showcase Requirements
- 60+ seconds gameplay demonstration.
- 120+ seconds (2 min) technical discussion with code shown.
- Under 5 minutes total.
- Unlisted YouTube link (not Google Drive).

## 7. Expected Time Commitment
- 2 classes per week (1.5–2 hours each).
- ~8–12 hours outside class:
  - Complete "Nothing But Donut" exercise.
  - Implement Pong Pt. 2 requirements (sound, power-ups, UI).
  - Record and edit video showcase.
  - Review preparation materials for next week.

## 8. Key Technical Concepts Introduced

### Collision Detection Modes
| Mode | Cost | Behavior | Use Case |
|------|------|----------|----------|
| **Discrete** | Cheapest | Checks at physics steps only | Most objects, low speed |
| **Continuous** | Medium | Sweeps against static colliders | Fast objects, static environment |
| **Continuous Dynamic** | Most expensive | Sweeps against all colliders | Fast objects colliding with each other |
| **Continuous Speculative** | Medium | Bounding box prediction (linear + angular) | Fast rotating objects, general fast movement |

### Audio System
| Component | Purpose |
|-----------|---------|
| **Audio Source** | Plays audio clips, attached to emitting objects |
| **Audio Clip** | The sound file asset |
| **Audio Listener** | Receives audio (usually on camera) |
| **Audio Mixer** | Groups and processes audio channels |

### Audio Methods
| Method | Behavior |
|--------|----------|
| `audioSource.Play()` | Stops current, plays assigned clip |
| `audioSource.PlayOneShot(clip)` | Layers on top, no interruption |

### Audio Load Types
| Type | Memory | Latency | Use Case |
|------|--------|---------|----------|
| **Decompress On Load** | High | Low | Short sound effects (responsive) |
| **Compressed In Memory** | Medium | Medium | Medium-length sounds |
| **Streaming** | Low | High | Long music tracks |

### Canvas Render Modes
| Mode | Description | Use Case |
|------|-------------|----------|
| **Screen Space - Overlay** | Always on top, no 3D | HUD, menus |
| **Screen Space - Camera** | Rendered at camera distance | UI with perspective |
| **World Space** | Exists in 3D world | Name tags, health bars |

### Rigidbody Types
| Type | Components | Behavior |
|------|-----------|----------|
| **Static Collider** | Collider only | Immovable, physics aware |
| **Dynamic Collider** | Collider + Rigidbody | Physics-controlled movement |
| **Kinematic Collider** | Collider + Rigidbody (isKinematic) | Script-controlled, affects others |

## 9. Preparation Materials (Before Tuesday)
- Introduction to Audio (~15m video)
- GUI, Canvas Tutorial (~10m video)

### Before Thursday
- Audio Mixer and Audio Mixer Groups (~10m)
- How to Use Custom Fonts in TextMeshPro (~3m)

### Recommended
- Audio Mixer with Scripting (~15m)
- UI Canvas Scaler (~15m)

## 10. Common Issues and Solutions

| Issue | Solution |
|-------|----------|
| Sound not playing | Check Audio Source component attached, clip assigned |
| Sound too quiet/loud | Check Audio Listener exists on camera, adjust volume |
| Doppler not working | Remove or set to kinematic any unused Rigidbody on Audio Source object |
| UI text not visible | Check Canvas order, text color vs background |
| Billboard text backwards | Scale child by -1 or adjust LookAt direction |
| Old asset materials magenta | Window → Rendering → Render Pipeline Converter |
| PlayOneShot not audible | Ensure Audio Source component exists on the calling object |








