# Stage 1 decisions

- The playable hero is `TEMP_HERO`. It has no invented historical name, era, clothing, or biography.
- The aul, yurts, NPCs, portrait, and world props are simple primitive placeholders. They intentionally make no claim of historical accuracy.
- No third-party asset is imported. This keeps the prototype runnable without a licence/login flow.
- Gameplay direction is stored in `PlayerVisual` as eight directions; its visual root can later be replaced with Blender-rendered directional sprites without changing movement code.


# Stage 1.5 decisions

- Authority: ATTILA_Game_Design_Spec_v2.docx supplied by the user, specifically visual direction and sprite pipeline; the user's Stage 1.5 limits supersede the document's broader MVP implementation instructions.
- Selected 40-degree orthographic view, size 10, .18-second follow smoothing; kept +Z as north and the existing world-axis movement.
- Chose a code-drawn neutral soft-pixel placeholder: eight idle views and two walk frames per direction, plain blue shirt and dark trousers, no cultural/historical claim. All art is NEEDS HISTORICAL REVIEW.
- Used existing URP with unlit flat materials and two roof colors, with cast shadows disabled. This deliberately avoids a custom renderer and realistic shading.
- Kept all yurt, tree, rock, fence, path and NPC locations. Replaced yurt dome visuals with a shallow twelve-sided cone; moved the previously detached door to the wall (the old -3.42 offset exceeded the base's 1.75 radius).
- Removed visible capsule meshes for player and existing NPC placeholders, sharing one sprite data set for now. Their CharacterControllers remain. Removed only the duplicate primitive character and ground colliders associated with their former visuals.
- Kept the existing menu and character-select UI unchanged, including its existing portrait placeholder. Did not rebuild scenes, change PlayerController or edit project settings as part of this stage.
- No decisions outside the approved Stage 1.5 scope; exact camera, sprite resolution, palette shades, frame rate and shared NPC placeholder art are implementation choices within that scope.
