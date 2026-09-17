# ATTILA - Stage 1.5

Open `Assets/ATTILA/Scenes/Boot.unity` and enter Play Mode. The existing flow remains Boot -> MainMenu -> CharacterSelect -> AulPrototype. Use WASD to move. No scene rebuild or asset generation is needed; the generated PNGs, import settings, sprite data and material are included.

## Presentation

- Orthographic camera: X rotation 40 degrees, Y/Z 0; size 10; near/far .1/100.
- Camera follows the controller center, 24 units along the inverse camera forward axis: offset approximately (0, 15.427, -18.385). At spawn (0, 1, 0), camera starts at (0, 16.427, -18.385).
- Follow smoothing is .18 seconds, with immediate initial positioning. No perspective zoom or camera orbit.
- Existing aul positions, path, fence and four yurts remain. Unlit, matte color areas and two-tone shallow roofs replace sphere shading. Directional light intensity .55, flat ambient illumination, no fog, post-processing or cast shadows.
- All people in the aul use the same neutral temporary sprite set. PlayerController remains unchanged; CharacterController continues to handle ground and obstacles. Redundant primitive capsule colliders are no longer created for the people, and the redundant ground box collider was removed; the ground mesh collider remains.

## Character visual data

The unchanged controller continues to call `PlayerVisual.SetMovement(direction, moving)` with its movement vector. The visual component owns its child SpriteRenderer, fixed camera-facing rotation, facing state and frame playback. Its pivot sits at the controller feet. Idle retains the last movement direction rather than following the controller's smoothed yaw.

Direction order in `DirectionalSpriteSet` is N, NE, E, SE, S, SW, W, NW. +Z is N and +X is E, matching the fixed camera and existing controls. atan2(x, z), rounded into eight 45-degree sectors, chooses the direction. Each entry has one idle sprite and an optional walk array; missing walk frames fall back to idle. The temporary set uses two walk frames at 6 fps.

The included data is `Resources/Data/TemporaryDirections.asset`. Images are in `Art/Characters/RenderedSprites/Placeholder/`: 48 x 80 transparent PNGs, 30 pixels per unit, bottom-center pivot, point filtering, no mipmaps and no compression. They depict only a neutral shirt, trousers and shoes, with no historical identity or costume claim.

## Later Blender-rendered replacements

No Blender integration or scripts were added. Later, import consistently sized transparent PNG frames under `Art/Characters/RenderedSprites/`, use matching foot pivots and pixels per unit, and replace the references in the existing DirectionalSpriteSet asset. The arrays accept longer walk cycles. Adjust visualScale and framesPerSecond in the data if needed. Higher resolution art can use a proportionally higher pixels-per-unit value and bilinear filtering if desired. Movement, collision, camera and gameplay code need no changes. The serialized PlayerVisual spriteSet field also accepts another DirectionalSpriteSet for future prefabs.

`ATTILA/Generate Stage 1.5 Placeholder Sprites` is an optional editor-only regeneration command. It overwrites the TEMP_HERO placeholder PNGs and TemporaryDirections data; do not run it after replacing that data with final art. It does not rebuild scenes. `MvpSceneBuilder` was not changed or run.

## Validation

Unity 6000.6.0f1 compiled the project and ran the editor-only Stage15Validation scene-flow checks. They exercise Boot, Play, Back, Start, the enabled movement component, ground contact, controller displacement, camera follow/framing, yurt/NPC/rock/fence/tree collisions, all eight facings, idle persistence and both walk-frame references. Rendered menu, character-select and aul views were inspected. The runtime check reported no console errors. The pre-existing UiFactory obsolete-API warning remains.

The automated movement check drives CharacterController.Move; it does not inject physical keyboard events. Final manual check: open Boot, click Play and Start, use WASD and diagonal key pairs, release to confirm idle direction, and walk against a yurt. No setup steps are required.

To rerun automation, start Unity with `-batchmode -projectPath <project> -executeMethod ATTILA.Editor.Stage15Validation.Run -logFile <log>` (no `-quit` and no `-nographics`). It exits after checking; logs and QA PNGs are written under the ignored `Logs/Stage15/` directory. It never saves or rebuilds scenes or rewrites art data.

Stage 1.5 only. All character and environment art remains PLACEHOLDER / NEEDS HISTORICAL REVIEW. No Stage 2 systems were added.
