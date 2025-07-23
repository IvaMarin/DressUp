# Dress Up Game (test assignment)
The hand is controlled via Drag (onDrag) to the face area.
- Transition of player control and automatic animations strictly according to the action area (face area).
- Smooth transitions between states without jerks and sudden movements.
- Interaction with three objects: cream, lipstick, blush.

### Mechanic: applying cream (removing pimples)
1. The player clicks on the "cream" object.
2. The "hand takes the cream" animation starts.
3. After taking:
    - The hand with the cream moves to a position between the face and the place where the cream was previously
    - Control passes to the player: the player drags the hand with the cream to the face.
4. If the player releases the finger outside the face area, nothing happens
5. If the player releases the finger in the face area:
    - The "applying cream to the face" animation starts. - The sprite changes from a girl with pimples to a girl without pimples
    - After the animation ends, the hand automatically returns the cream to its place.
    - The hand is in the default position.

<img src="https://github.com/user-attachments/assets/867b1362-3921-4f49-9e7b-1aa0982eaf47" width="150">

### Mechanic: applying blush (applying blush to a doll)
1. The player clicks on the "blush color" object.
2. The hand takes the brush and touches the color in the palette, as if applying blush to the brush (the tip of the brush is painted in the color that the brush was dipped in)
3. The hand is fixed at chest level, the brush is pointing up.
4. Control is transferred to the player: moves the hand with the brush to the face.
5. Releases in the face area:
    - Starts the "applying blush" animation. Quite a fast animation (see ref) - Applying blush to the character
    - The hand returns the brush and becomes in the default position.

<img src="https://github.com/user-attachments/assets/c3bf05ad-b559-436a-9168-444a39bd1f87" width="150">

### Mechanic: applying lipstick (painting lips)
1. Player presses lipstick
2. Hand takes lipstick
3. Fixed at chest level.
4. Player leads to face, rest as with blush (change lip color)

<img src="https://github.com/user-attachments/assets/4c1ee699-06c9-44a6-b535-2ee487d6d0ce" width="150">

### Extra mechanic: sponge
When you click on the blue sponge, makeup is erased automatically without any player action or animation.

<img src="https://github.com/user-attachments/assets/115a0ed9-960c-4c5e-8163-0ace18d703e6" width="150">
