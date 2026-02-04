---
week: 3
type: extras
title: "Week 3 Anecdotes & Bonus Content"
description: "Colorful stories, advanced demos, and supplementary details from Week 3 lectures"
---

# Week 3 — Anecdotes & Bonus Content

This document captures colorful stories, advanced demonstrations, and supplementary details from the Week 3 lectures that enrich the core curriculum.

---

## Memorable Anecdotes

### The Annual TA "Boing" Recording

Every year, the instructor has a tradition of having the Teaching Assistant record the "Boing" sound effect used in the "Nothing But Donut" basketball demo:

> "Every year I give the TA the honor of giving a solid Boing, and this year is no difference."

The TA recorded their "Boing" into Audacity, which was then exported as an MP3 and imported into Unity for the trampoline collision sound.

### Class Crowd Cheering Sound

The entire class participated in recording a crowd cheering sound effect for when the ball scores in the hoop. The instructor set up the scenario:

> "How many of you guys like sports? Okay, some of you guys... Your team made it to the final, whatever it is of your sport, right? And it's a tie game. It's neck and neck in the very end. Your team is about to score, and then right at the buzzer your team wins and you jump up and you make that sound."

For the non-sports fans:

> "For the people that don't like sports, you are also watching the same game. The only difference is that you have a thousand dollars on this team that is about to win."

The class then made crowd cheering sounds together on cue, which was saved as "326 wins" and used as the scoring celebration sound.

### "Elon Comes In and Says You're Out of Here"

While demonstrating why the camera should track the ball, the instructor made a timely reference:

> "If this is a sports game and you're the cameraman, you just lost your job. Elon comes in and says you're out of here."

This was in reference to the ball flying off screen while the camera stayed stationary.

### Newton and the Etymology of "Kinematic"

When explaining kinematic Rigidbodies, the instructor gave a brief physics history lesson:

> "Physics history—you think about Newton. People were just thinking about things geometrically in terms of shapes. They'd be like, 'Oh, yeah, the planets are like ellipses, or whatever' and the terms for how things move—we call them kinematics as a description of the motion without being able to explain things through forces."

The naming convention in Unity reflects this: kinematic objects move by geometry (script), not by physics forces—like physics before Newton explained gravity and forces.

---

## Technical Discoveries & Gotchas

### The Doppler Effect Bug

During the live demo of 3D spatial audio, the instructor encountered an unexpected issue where the Doppler effect wasn't working despite being enabled:

**The Problem:** When a Rigidbody is on the same GameObject as an AudioSource, but the script moves the transform directly (not through physics), the Doppler effect won't work correctly.

**Why it Happens:** The AudioSource checks velocity from the Rigidbody component. If the Rigidbody isn't being used to move the object (kinematic, script-controlled), it reports zero velocity, so no Doppler pitch shift occurs.

**The Solution:** Remove the Rigidbody from the object with the AudioSource if you're moving it via script transforms. After removing it:

> "You can hear the Doppler effect. You just can't hear me now."

The instructor explained the fix during break:

> "Having a Rigidbody on the same component as the AudioSource meant that it was going to take the velocity values from the Rigidbody. But the Rigidbody wasn't controlling the speed—we were manually messing with the transform. So the Rigidbody doesn't really know about the velocity."

### Play Mode Settings Loss

The instructor reminded students about this classic Unity pitfall:

> "I think I was adjusting these settings while I was in Play mode, because the Spatial Blend is in 2D."

Settings changed during Play Mode are lost when you stop playing—a common trap that catches even experienced developers.

---

## Industry & Historical Insights

### TextMeshPro Was Third-Party

The instructor mentioned that TextMeshPro started as a third-party extension before being acquired by Unity:

> "Luckily, one of my favorite editor extensions, TextMeshPro, has now been acquired by Unity."

This is why the API has the "TMPro" namespace and why some naming conventions are different from native Unity components.

### Unity's Automatic API Updates

When importing older Asset Store packages, Unity can automatically update deprecated code:

> "Some of the project source files refer to an API that's changed. These can be automatically updated—it's a super nice feature of Unity. When they make changes and they deprecate things, they'll often programmatically modify your code so that it is up to date if they can do it."

---

## Sound Design Philosophy

### The Footstep Principle

When discussing sound variation for paddle hits:

> "Ever play a game where you're walking, you hear footsteps? Well, it's not playing the same footstep sound over and over again, right? That would sound unnatural. So you could have slight variations to make it feel a little bit more organic."

This principle applies to any repetitive game sounds—paddle hits, weapon sounds, UI clicks, etc.

### Console Hygiene

The instructor emphasized keeping the console clean:

> "You want to have a clean console, so that when something pops up on there, you know, it'll get your attention the way it should."

Warnings and errors should be addressed, not ignored. If you're ignoring warnings during development, you'll miss the important ones too.

---

## Free Resource Sites

### Freesound.org Tips

For finding loopable sound effects:

> "The things you're looking for, for looping sounds—you want to explicitly say 'loop' on the end, right? You want to see that the end of the sound looks like the beginning so it can just play over and over again."

The site requires creating a free account to download.

### Incompetech for Music

For royalty-free background music:

> "This guy makes lots of really high quality royalty-free music here. It's at incompetech... trying to Google for this stuff can be kind of painful. But this is a good one."

Kevin MacLeod's incompetech.com is a go-to resource for game jam background music.

---

## Editor Shortcuts & Tips

### Shift+F: Lock Scene View to Object

To follow a moving object in the Scene view while the game runs:

> "Put it on an object that's moving. And then inside of the Scene view, hit Shift+F, and then you'll see that the camera follows it."

This is incredibly useful for debugging moving objects without setting up a follow camera.

### Quick Console Visibility

Keep the console visible at all times to catch errors immediately:

> "I highly recommend you do this—don't allow the console to be hidden."

Dock it somewhere always visible.

### Inspector Debug Mode

To find the actual class name of any Unity component:

1. Right-click on the Inspector tab
2. Select "Debug"
3. Expand the component to see internal class names

> "Underneath the textmesh pro section, you can come in here and you can see... the actual class is called TextMeshProUGUI."

This is helpful when you need to reference components from code but don't know the exact type name.

### Edit Script Shortcut

Another way to find component class names:

1. Click the three dots (ellipses) on any component
2. Select "Edit Script"
3. View the decompiled/source code to see the class name

---

## Advanced Demo: Cylindrical Billboarding

The summary covers basic billboarding, but the instructor showed a specific technique for cylindrical (Y-axis only) billboarding that's worth preserving in more detail:

### The Problem with Spherical Billboarding

Basic `LookAt()` creates spherical billboarding—the text tilts up/down when the camera moves vertically. This looks unnatural for name tags and labels.

### The Solution: Extract Only Y Rotation

```csharp
// Step 1: Get the full "look at camera" rotation
Vector3 toCamera = cam.transform.position - distanceCanvas.transform.position;
Quaternion billboardRotation = Quaternion.LookRotation(toCamera);

// Step 2: Extract only the Y euler angle (yaw)
Vector3 billboardEulers = billboardRotation.eulerAngles;

// Step 3: Create new rotation with only Y, zeroing X and Z
billboardRotation = Quaternion.Euler(0, billboardEulers.y, 0);

// Step 4: Apply to canvas
distanceCanvas.transform.rotation = billboardRotation;
```

**Result:** The text always faces the camera horizontally but stays upright regardless of camera height.

### The LookAt() Gotcha

`LookAt()` points the object's positive Z-axis toward the target. If your text faces the negative Z direction (common with UI), it appears backwards.

**Quick Fix:** Set the text's scale X to -1 to mirror it.

> "I'm going to take the easy way out this time by going to the text, and I can just reflect the text across—negative one in the scale."

---

## Quick Reference: transform.forward vs Axis Math

The instructor demonstrated the difference between world-axis-aligned code and local-direction code:

### Bad: Hardcoded Axis
```csharp
// Only works if car points along Z-axis
transform.position = new Vector3(currentPos.x, currentPos.y, newZ);
```

### Good: Using Local Direction
```csharp
// Works regardless of car orientation
Vector3 forward = transform.forward;
Vector3 offset = forward * oscillationPos * maxOffset;
transform.position = startingPosition + offset;
```

> "I wrote this in kind of a crude, non-vector-y way, and it's really specific to this axis. I don't like that at all. Your track could be going any direction—you shouldn't have to be axis aligned."

---

## Assignment Context

### Power-Up vs Juice Distinction

The instructor clarified the difference:

> "One thing that may be a little bit ambiguous is like, what's a power-up and what's a juicy thing? I think some of this can overlap. The only thing that makes the power-up different is that it's something that gets triggered. And it's not like a normal part of the game... The juicy stuff would probably be more like adding interesting things that happen when you're in the normal course of play."

**Power-ups:** Triggered modifications to gameplay (ball touches floating power-up → paddles shrink)

**Juice:** Polish effects during normal play (camera shake on hit, particle trails, screen effects)

### Video Showcase Tip

> "Take care to make sure you show all of these requirements being fulfilled when you do your video showcase. It's probably a good idea to specifically call out all of your extra credit... so that you can be sure that he catches it."

Don't assume the grader will notice features—explicitly demonstrate and explain them.

---

## Debugging Tips from Class

### The Classic "Script Not Attached" Problem

When the instructor's oscillating car didn't move:

> "What's the problem here? It's the classic problem, which is I wrote a script and then I'm not using the script."

Always verify:
1. Script is compiled without errors
2. Script is attached to the correct GameObject
3. Required references are assigned in the Inspector

### Fighting with Physics

When the car fell through the ground:

> "I'm fighting with the physics system. Physics is trying to control the transform, and I'm also trying to control the transform. I don't want to fight with physics."

**Solution:** Set the Rigidbody to `isKinematic = true` if you want script control instead of physics control.

---

## Code Snippets Not in Summary

### Play One Shot for Layered Audio

When you need to play a sound without interrupting the current audio:

```csharp
void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        AudioSource source = GetComponent<AudioSource>();
        source.PlayOneShot(hornClip);  // Layers on top of engine sound
    }
}
```

> "Instead of overwriting the clip, I'm gonna say... play one shot. Hey, you know what? Don't worry about what's already playing. Just play this thing on top."

### String Formatting for Distance Display

```csharp
distanceText.text = $"Distance: {distance:F1}";  // F1 = 1 decimal place
```

The instructor didn't format the decimals in class, noting:

> "I could do without the extreme decimal places. But I'm sure you guys can figure that out."

---

## Student Q&A Highlights

### What's the "f" Suffix on Float Values?

**Q:** "What's the purpose of the F's after the float value?"

**A:** C# defaults decimal literals to `double` (64-bit). The `f` suffix explicitly makes it a `float` (32-bit).

```csharp
float x = 2.5f;   // Correct: explicitly float
float y = 2.5;    // Warning: 2.5 is double, implicit cast
```

> "Strongly typed languages want to distinguish between what's a 32-bit float and a 64-bit double. If you don't have the F, it assumes that you mean double."

### Why Don't We Specify the Car for transform?

**Q:** "Why don't we specify the car object for its transform in the script?"

**A:** Because the script is a component on the car GameObject. When you say `transform` in a script, it's shorthand for "get the Transform component on the same GameObject I'm attached to."

> "When I just say dot transform and I'm in the car, effectively what I'm saying is... get component for my sibling transform."


