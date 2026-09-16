# Stage 1 decisions

- The playable hero is `TEMP_HERO`. It has no invented historical name, era, clothing, or biography.
- The aul, yurts, NPCs, portrait, and world props are simple primitive placeholders. They intentionally make no claim of historical accuracy.
- No third-party asset is imported. This keeps the prototype runnable without a licence/login flow.
- Gameplay direction is stored in `PlayerVisual` as eight directions; its visual root can later be replaced with Blender-rendered directional sprites without changing movement code.
