/*
 * Interactables Guide:
 * 
 * To create an interactable object:
 * 1. In Project > Scripts > Interactables, either use an existing interactable object script(skip to 4), or create a new one for the type of object desired.
 * 2. Derive from the InteractableObject class.
 * 3. Create override interact method, for game function desired.
 * 4. Create an empty object in the scene, set the name, and add the interactable script being used.
 * 5. In Project > Prefabs > UI > Interactables, add the desired Canvas type and size to the new object's interactable script.
 * 6. Add new text the TextMeshPro child of the added Canvas
 * 7. Ensure the Canvas is set inactive in order to move th object around in the editor.
 * 8. Drag the new object into Project > Prefabs > Interactables
 */