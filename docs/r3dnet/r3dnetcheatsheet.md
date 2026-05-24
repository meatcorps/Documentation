# R3D.NET 0.9.1 unofficial Cheatsheet
## What is R3D?
R3D is an extension library for <a href="https://www.raylib.com/">raylib</a> that expands its 3D capabilities, including rendering, lighting, kinematics, mesh utilities, and related helpers, without turning raylib into a full engine.
### Key Features

- **Hybrid Renderer**: Deferred pipeline with forward rendering for transparency.
- **Advanced Materials**: Complete PBR material system (Burley/SchlickGGX)
- **Custom Shaders**: Support for surface shaders (materials/decals) and screen shaders.
- **Dynamic Lighting**: Directional, spot, and omni lights with soft shadows
- **Image-Based Lighting**: Supports environment IBL and reflection probes.
- **Post-Processing**: SSAO, SSR, DoF, bloom, fog, tonemapping, and more
- **Kinematics Support**: Basic kinematic system with capsule and mesh-based colliders.
- **Mesh Utilities**: Mesh generation, manipulation, and helper utilities.
- **Model Loading**: Assimp integration with animations and mesh generation
- **Performance**: Built-in frustum culling, instanced rendering, and more

Information source: [Bigfoot71/r3d GitHub](https://github.com/Bigfoot71/r3d)
## What is this and why should I care?
This is a cheatsheet for the R3D.NET library. Keep in mind this is not manually written. Also it's not the official one. I just did this because the lack of a nice overview. So, I made a script that extracted this information. Anyway! Enjoy!

For more information or how to install, please visit the [GitHub repository](https://github.com/graphnode/r3d-cs).

## Examples
Below is a list of all the examples in the library. Those will link directly towards the source code on GitHub.

- [Animation.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Animation.cs)

- [AnimTree.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/AnimTree.cs)

- [Basic.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Basic.cs)

- [Billboards.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Billboards.cs)

- [Bloom.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Bloom.cs)

- [CustomMesh.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/CustomMesh.cs)

- [Decal.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Decal.cs)

- [Dof.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Dof.cs)

- [Instanced.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Instanced.cs)

- [Kinematics.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Kinematics.cs)

- [Lights.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Lights.cs)

- [Particles.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Particles.cs)

- [Pbr.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Pbr.cs)

- [Probe.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Probe.cs)

- [Resize.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Resize.cs)

- [Shader.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Shader.cs)

- [Skybox.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Skybox.cs)

- [Sponza.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Sponza.cs)

- [Sprite.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Sprite.cs)

- [Sun.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Sun.cs)

- [Transparency.cs](https://github.com/graphnode/r3d-cs/blob/master/Examples/Transparency.cs)

## Functions
Below is a list of all the functions in the library.
### R3D.ambient_map Functions
```csharp
/* Loads a ambient map from an image file.
The layout parameter tells how faces are arranged inside the source image. */
AmbientMap R3D.LoadAmbientMap(string fileName, CubemapLayout layout, AmbientFlags flags);

/* Builds a ambient map from an existing Image.
Same behavior as R3D_LoadAmbientMap(), but without loading from disk. */
AmbientMap R3D.LoadAmbientMapFromImage(Image image, CubemapLayout layout, AmbientFlags flags);

/* Generates an ambient map from a cubemap.
The source cubemap should usually be an HDR sky/environment.
Depending on the provided flags, this function:
- convolves the cubemap into diffuse irradiance
- builds a mipmapped prefiltered cubemap for reflections */
AmbientMap R3D.GenAmbientMap(Cubemap cubemap, AmbientFlags flags);

/* Frees the textures used by an ambient map.
After this call, the ambient map is no longer valid. */
void R3D.UnloadAmbientMap(AmbientMap ambientMap);

/* Rebuilds an existing ambient map from a new cubemap.
Use this when the environment changes dynamically (time of day, weather, interior/exterior transitions, etc).
Only the components enabled in `ambientMap.flags` are regenerated. */
void R3D.UpdateAmbientMap(AmbientMap ambientMap, Cubemap cubemap);

```
### R3D.animation_player Functions
```csharp
/* Creates an animation player for a skeleton and animation library.
Allocates memory for animation states and pose buffers. */
AnimationPlayer R3D.LoadAnimationPlayer(Skeleton skeleton, AnimationLib animLib);

/* Releases all resources used by an animation player. */
void R3D.UnloadAnimationPlayer(AnimationPlayer player);

/* Checks whether an animation player is valid. */
bool R3D.IsAnimationPlayerValid(AnimationPlayer player);

/* Returns whether an animation is currently playing. */
bool R3D.IsAnimationPlaying(AnimationPlayer player);

/* Starts playback of the specified animation. */
void R3D.PlayAnimation(ref AnimationPlayer player, int animIndex);

/* Pauses the current animation. */
void R3D.PauseAnimation(ref AnimationPlayer player);

/* Stops the current animation and clamps its time. */
void R3D.StopAnimation(ref AnimationPlayer player);

/* Rewinds the animation to the start or end depending on playback direction. */
void R3D.RewindAnimation(ref AnimationPlayer player);

/* Gets the current playback time of an animation. */
float R3D.GetAnimationTime(AnimationPlayer player, int animIndex);

/* Sets the current playback time of an animation. */
void R3D.SetAnimationTime(ref AnimationPlayer player, int animIndex, float time);

/* Gets the playback speed of an animation. */
float R3D.GetAnimationSpeed(AnimationPlayer player, int animIndex);

/* Sets the playback speed of an animation.
Negative values play the animation backwards. If setting a negative speed on a stopped animation, consider calling RewindAnimation() to start at the end. */
void R3D.SetAnimationSpeed(ref AnimationPlayer player, int animIndex, float speed);

/* Gets whether the animation is set to loop. */
bool R3D.GetAnimationLoop(AnimationPlayer player, int animIndex);

/* Enables or disables looping for the animation. */
void R3D.SetAnimationLoop(ref AnimationPlayer player, int animIndex, [MarshalAs(UnmanagedType.I1)] bool loop);

/* Advances the time of the current animation.
Updates animation timer based on speed and delta time. Does NOT recalculate the skeleton pose. */
void R3D.AdvanceAnimationTime(ref AnimationPlayer player, float dt);

/* Computes the local-space transform of each bone for the current animation.
Samples and interpolates the current animation keyframes at the current playback time, and stores the resulting bone transforms in local space intoplayer->localPose. Does NOT advance animation time, and does NOT compute model-space transforms. */
void R3D.ComputeAnimationLocalPose(ref AnimationPlayer player);

/* Computes the model-space transform of each bone from the current local pose.
Traverses the bone hierarchy and accumulates local transforms into model-space matrices, stored intoplayer->modelPose. This assumesplayer->localPose is already up-to-date. Does NOT sample animation keyframes, and does NOT advance animation time. */
void R3D.ComputeAnimationModelPose(ref AnimationPlayer player);

/* Computes both the local and model-space transforms for the current animation.
Equivalent to calling R3D_ComputeAnimationLocalPose() followed by R3D_ComputeAnimationModelPose(). Does NOT advance animation time. */
void R3D.ComputeAnimationPose(ref AnimationPlayer player);

/* Computes the final skinning matrices and uploads them to the GPU.
Multiplies each bone's model-space transform by its inverse bind matrix to produce the skinning matrices, then uploads them to the GPU skin texture. This assumesplayer->modelPose is already up-to-date. */
void R3D.UploadAnimationPose(ref AnimationPlayer player);

/* Updates the animation player: calculates and upload the current pose pose, then advances time.
Equivalent to calling R3D_ComputeAnimationLocalPose() followed by R3D_ComputeAnimationModelPose() and R3D_AdvanceAnimationTime(). */
void R3D.UpdateAnimationPlayer(ref AnimationPlayer player, float dt);

```
### R3D.animation_tree Functions
```csharp
/* Creates an animation tree using given animation player.
Allocates memory for animation nodes pool. */
AnimationTree R3D.LoadAnimationTree(AnimationPlayer player, int maxSize);

/* Creates an animation tree using given animation player, with optional root motion support.
Allocates memory for animation nodes pool and sets root bone index for root motion. */
AnimationTree R3D.LoadAnimationTreeEx(AnimationPlayer player, int maxSize, int rootBone);

/* Creates an animation tree using given animation player, with optional root motion support and callback.
Allocates memory for animation nodes pool, sets root bone index and update callback. */
AnimationTree R3D.LoadAnimationTreePro(AnimationPlayer player, int maxSize, int rootBone, AnimationTreeCallback updateCallback, IntPtr updateUserData);

/* Releases all resources used by an animation tree, including all animation nodes. */
void R3D.UnloadAnimationTree(AnimationTree tree);

/* Updates the animation tree: calculates blended pose, sets and uploads the pose through associated animation player. */
void R3D.UpdateAnimationTree(ref AnimationTree tree, float dt);

/* Updates the animation tree: calculates blended pose, sets and uploads the pose through associated animation player.
Set the rootMotion and/or rootDistance pointers to get root motion information. Divide rootMotion translation vector by dt to get root bone speed. */
void R3D.UpdateAnimationTreeEx(ref AnimationTree tree, float dt, ref Transform rootMotion, ref Transform rootDistance);

/* Sets root animation node of the animation tree. */
void R3D.AddRootAnimationNode(ref AnimationTree tree, AnimationTreeNode node);

/* Connects two animation nodes of animation tree hierarchy. */
bool R3D.AddAnimationNode(AnimationTreeNode parent, AnimationTreeNode node, int inputIndex);

/* Creates animation node of type Animation.
Animation is a leaf node, holding the R3D_Animation structure. */
AnimationTreeNode R3D.CreateAnimationNode(ref AnimationTree tree, AnimationNodeParams @params);

/* Creates animation node of type Animation, optionally sets animation currentTime.
Animation is a leaf node, holding the R3D_Animation structure. */
AnimationTreeNode R3D.CreateAnimationNodeEx(ref AnimationTree tree, AnimationNodeParams @params, [MarshalAs(UnmanagedType.I1)] bool setTime);

/* Creates animation node of type Blend2.
Blend2 node blends channels of any two animation nodes together, with respecting optional bone mask. */
AnimationTreeNode R3D.CreateBlend2Node(ref AnimationTree tree, Blend2NodeParams @params);

/* Creates animation node of type Add2.
Add2 node adds channels of any two animation nodes together, with respecting optional bone mask. */
AnimationTreeNode R3D.CreateAdd2Node(ref AnimationTree tree, Add2NodeParams @params);

/* Creates animation node of type Switch.
Switch node allows instant or blended/faded transition between any animation nodes connected to inputs. */
AnimationTreeNode R3D.CreateSwitchNode(ref AnimationTree tree, int inputCount, SwitchNodeParams @params);

/* Creates animation node of type State Machine (Stm).
Stm node allows to create a state machine graph of any animation nodes. */
AnimationTreeNode R3D.CreateStmNode(ref AnimationTree tree, int statesCount, int edgesCount);

/* Creates animation node of type State Machine (Stm), with option to disable travel feature (enabled by default). */
AnimationTreeNode R3D.CreateStmNodeEx(ref AnimationTree tree, int statesCount, int edgesCount, [MarshalAs(UnmanagedType.I1)] bool enableTravel);

/* Creates animation node of type State Machine Stop/Done (StmX).
StmX is a helper animation node, that allows stopping state machine evaluation in the current update. Sets animation node done status of the state machine to true. */
AnimationTreeNode R3D.CreateStmXNode(ref AnimationTree tree, AnimationTreeNode nestedNode);

/* Creates state in a State Machine animation node. */
AnimationStmIndex R3D.CreateStmNodeState(AnimationTreeNode stmNode, AnimationTreeNode stateNode, int outEdgesCount);

/* Creates edge in a State Machine animation node. */
AnimationStmIndex R3D.CreateStmNodeEdge(AnimationTreeNode stmNode, AnimationStmIndex beginStateIndex, AnimationStmIndex endStateIndex, StmEdgeParams @params);

/* Sets parameters of animation node Animation. */
void R3D.SetAnimationNodeParams(AnimationTreeNode node, AnimationNodeParams @params);

/* Gets parameters of animation node Animation. */
AnimationNodeParams R3D.GetAnimationNodeParams(AnimationTreeNode node);

/* Sets parameters of animation node Blend2. */
void R3D.SetBlend2NodeParams(AnimationTreeNode node, Blend2NodeParams @params);

/* Gets parameters of animation node Blend2. */
Blend2NodeParams R3D.GetBlend2NodeParams(AnimationTreeNode node);

/* Sets parameters of animation node Add2. */
void R3D.SetAdd2NodeParams(AnimationTreeNode node, Add2NodeParams @params);

/* Gets parameters of animation node Add2. */
Add2NodeParams R3D.GetAdd2NodeParams(AnimationTreeNode node);

/* Sets parameters of animation node Switch. */
void R3D.SetSwitchNodeParams(AnimationTreeNode node, SwitchNodeParams @params);

/* Gets parameters of animation node Switch. */
SwitchNodeParams R3D.GetSwitchNodeParams(AnimationTreeNode node);

/* Sets parameters of State Machine edge. */
void R3D.SetStmNodeEdgeParams(AnimationTreeNode node, AnimationStmIndex edgeIndex, StmEdgeParams @params);

/* Gets parameters of State Machine edge. */
StmEdgeParams R3D.GetStmNodeEdgeParams(AnimationTreeNode node, AnimationStmIndex edgeIndex);

/* Gets active state index of State Machine. */
AnimationStmIndex R3D.GetStmStateActiveIndex(AnimationTreeNode node);

/* Sets travel path inside State Machine, from current state to target.
If travel path is not found, target is set as current state instantly (teleport). */
void R3D.TravelToStmState(AnimationTreeNode node, AnimationStmIndex targetStateIndex);

/* Computes bone mask from list of bone names.
Only listed bones will be included in evaluation of animation node with this bone mask. Can be used in Blend2 and Add2 animation nodes. */
BoneMask R3D.ComputeBoneMask(ref Skeleton skeleton, byte** boneNames, int boneNameCount);

```
### R3D.animation Functions
```csharp
/* Loads animations from a model file. */
AnimationLib R3D.LoadAnimationLib(string filePath);

/* Loads animations from memory data. */
AnimationLib R3D.LoadAnimationLibFromMemory(void* data, uint size, string hint);

/* Loads animations from an existing importer. */
AnimationLib R3D.LoadAnimationLibFromImporter(Importer importer);

/* Releases all resources associated with an animation library. */
void R3D.UnloadAnimationLib(AnimationLib animLib);

/* Returns the index of an animation by name. */
int R3D.GetAnimationIndex(AnimationLib animLib, string name);

/* Retrieves an animation by name. */
Animation* R3D.GetAnimation(AnimationLib animLib, string name);

```
### R3D.core Functions
```csharp
/* Initializes the rendering engine.
This function sets up the internal rendering system with the provided resolution. */
bool R3D.Init(int resWidth, int resHeight);

/* Closes the rendering engine and deallocates all resources.
This function shuts down the rendering system and frees all allocated memory, including the resources associated with the created lights. */
void R3D.Close();

/* Gets the current internal resolution.
This function retrieves the current internal resolution being used by the rendering engine. */
void R3D.GetResolution(out int width, out int height);

/* Sets the internal rendering resolution.
Reallocates all internal render targets to the new resolution. This operation may cause a stall, this is acceptable when called infrequently (like window resize events), but should never be called per-frame. */
void R3D.SetResolution(int width, int height);

/* Retrieves the current anti-aliasing mode used for rendering. */
AntiAliasingMode R3D.GetAntiAliasingMode();

/* Sets the anti-aliasing mode for rendering.
The new mode takes effect on subsequent frames. */
void R3D.SetAntiAliasingMode(AntiAliasingMode mode);

/* Retrieves the current anti-aliasing quality preset. */
AntiAliasingPreset R3D.GetAntiAliasingPreset();

/* Sets the anti-aliasing quality preset.
Changing the preset triggers an internal shader recompilation. Compiled variants are cached and reused if the preset is set again. */
void R3D.SetAntiAliasingPreset(AntiAliasingPreset preset);

/* Retrieves the current aspect ratio handling mode. */
AspectMode R3D.GetAspectMode();

/* Sets the aspect ratio handling mode for rendering. */
void R3D.SetAspectMode(AspectMode mode);

/* Retrieves the current upscaling/filtering method. */
UpscaleMode R3D.GetUpscaleMode();

/* Sets the upscaling/filtering method for rendering output. */
void R3D.SetUpscaleMode(UpscaleMode mode);

/* Retrieves the current downscaling mode used for rendering. */
DownscaleMode R3D.GetDownscaleMode();

/* Sets the downscaling mode for rendering output. */
void R3D.SetDownscaleMode(DownscaleMode mode);

/* Gets the current output mode. */
OutputMode R3D.GetOutputMode();

/* Sets the output mode for rendering. */
void R3D.SetOutputMode(OutputMode mode);

/* Sets the default texture filtering mode.
This function defines the default texture filter that will be applied to all subsequently loaded textures, including those used in materials, sprites, and other resources.
If a trilinear or anisotropic filter is selected, mipmaps will be automatically generated for the textures, but they will not be generated when using nearest or bilinear filtering.
The default texture filter mode is `TEXTURE_FILTER_TRILINEAR`. */
void R3D.SetTextureFilter(TextureFilter filter);

/* Sets the default texture wrap mode.
This function only affects textures that are loaded manually for material maps. Textures loaded automatically during model import will use the wrap mode defined in the model file itself.
The default texture wrap mode is `TEXTURE_WRAP_CLAMP`. */
void R3D.SetTextureWrap(TextureWrap wrap);

/* Set the working color space for user-provided surface colors and color textures.
Defines how all *color inputs* should be interpreted:
- surface colors provided in materials (e.g. albedo/emission tints)
- color textures such as albedo and emission maps
When set to sRGB, these values are converted to linear before shading. When set to linear, values are used as-is.
This does NOT affect lighting inputs (direct or indirect light), which are always expected to be provided in linear space.
The default color space is `R3D_COLORSPACE_SRGB`. */
void R3D.SetColorSpace(ColorSpace space);

/* Get the currently active global rendering layers.
Returns the bitfield representing the currently active layers in the renderer. By default, the internal active layers are set to 0, which means that any non-zero layer assigned to an object will NOT be rendered unless explicitly activated. */
Layer R3D.GetActiveLayers();

/* Set the active global rendering layers.
Replaces the current set of active layers with the given bitfield. */
void R3D.SetActiveLayers(Layer bitfield);

/* Enable one or more layers without affecting other active layers.
This function sets the bits in the global active layers corresponding to the bits in the provided bitfield. Layers already active remain active. */
void R3D.EnableLayers(Layer bitfield);

/* Disable one or more layers without affecting other active layers.
This function clears the bits in the global active layers corresponding to the bits in the provided bitfield. Layers not included in the bitfield remain unchanged. */
void R3D.DisableLayers(Layer bitfield);

```
### R3D.cubemap Functions
```csharp
/* Loads a cubemap from an image file.
The layout parameter tells how faces are arranged inside the source image. */
Cubemap R3D.LoadCubemap(string fileName, CubemapLayout layout);

/* Builds a cubemap from an existing Image.
Same behavior as R3D_LoadCubemap(), but without loading from disk. */
Cubemap R3D.LoadCubemapFromImage(Image image, CubemapLayout layout);

/* Releases GPU resources associated with a cubemap. */
void R3D.UnloadCubemap(Cubemap cubemap);

```
### R3D.decal Functions
```csharp
/* Unload all map textures assigned to a R3D_Decal.
Frees all underlying textures in a R3D_Decal that are not a default texture. */
void R3D.UnloadDecalMaps(Decal decal);

```
### R3D.draw Functions
```csharp
/* Begins a rendering session using the given camera.
Rendering output is directed to the default framebuffer. */
void R3D.Begin(Camera3D camera);

/* Begins a rendering session with a custom render target.
If the render target is invalid (ID = 0), rendering goes to the screen. */
void R3D.BeginEx(RenderTexture2D target, Camera3D camera);

/* Ends the current rendering session.
This function is the one that actually performs the full rendering of the described scene. It carries out culling, sorting, shadow rendering, scene rendering, and screen / post-processing effects. */
void R3D.End();

/* Begins a clustered draw pass.
All draw calls submitted in this pass are first tested against the cluster AABB. If the cluster fails the scene/shadow frustum test, none of the contained objects are tested or drawn. */
void R3D.BeginCluster(BoundingBox aabb);

/* Ends the current clustered draw pass.
Stops submitting draw calls to the active cluster. */
void R3D.EndCluster();

/* Queues a mesh draw command with position and uniform scale.
The command is executed during R3D_End(). */
void R3D.DrawMesh(Mesh mesh, Material material, Vector3 position, float scale);

/* Queues a mesh draw command with position, rotation and non-uniform scale.
The command is executed during R3D_End(). */
void R3D.DrawMeshEx(Mesh mesh, Material material, Vector3 position, Quaternion rotation, Vector3 scale);

/* Queues a mesh draw command using a full transform matrix.
The command is executed during R3D_End(). */
void R3D.DrawMeshPro(Mesh mesh, Material material, Matrix4x4 transform);

/* Queues an instanced mesh draw command.
Draws multiple instances using the provided instance buffer. Does nothing if the number of instances is < = 0.
The command is executed during R3D_End(). */
void R3D.DrawMeshInstanced(Mesh mesh, Material material, InstanceBuffer instances, int count);

/* Queues an instanced mesh draw command with an instance range.
Draws 'count' instances starting at 'offset' in the instance buffer. Both 'offset' and 'count' are clamped to stay within [0, instances.capacity]:
- offset is clamped to [0, capacity]
- count is clamped to [0, capacity - offset] Does nothing if the resulting count is < = 0.
The command is executed during R3D_End(). */
void R3D.DrawMeshInstancedEx(Mesh mesh, Material material, InstanceBuffer instances, int offset, int count);

/* Queues an instanced mesh draw command with an instance range and an additional transform.
Draws 'count' instances starting at 'offset' in the instance buffer. Both 'offset' and 'count' are clamped to stay within [0, instances.capacity]:
- offset is clamped to [0, capacity]
- count is clamped to [0, capacity - offset] Does nothing if the resulting count is < = 0. The transform is applied to all instances.
The command is executed during R3D_End(). */
void R3D.DrawMeshInstancedPro(Mesh mesh, Material material, InstanceBuffer instances, int offset, int count, Matrix4x4 transform);

/* Queues a model draw command with position and uniform scale.
The command is executed during R3D_End(). */
void R3D.DrawModel(Model model, Vector3 position, float scale);

/* Queues a model draw command with position, rotation and non-uniform scale.
The command is executed during R3D_End(). */
void R3D.DrawModelEx(Model model, Vector3 position, Quaternion rotation, Vector3 scale);

/* Queues a model draw command using a full transform matrix.
The command is executed during R3D_End(). */
void R3D.DrawModelPro(Model model, Matrix4x4 transform);

/* Queues an instanced model draw command.
Draws multiple instances using the provided instance buffer. Does nothing if the number of instances is < = 0.
The command is executed during R3D_End(). */
void R3D.DrawModelInstanced(Model model, InstanceBuffer instances, int count);

/* Queues an instanced model draw command with an instance range.
Draws 'count' instances starting at 'offset' in the instance buffer. Both 'offset' and 'count' are clamped to stay within [0, instances.capacity]:
- offset is clamped to [0, capacity]
- count is clamped to [0, capacity - offset] Does nothing if the resulting count is < = 0.
The command is executed during R3D_End(). */
void R3D.DrawModelInstancedEx(Model model, InstanceBuffer instances, int offset, int count);

/* Queues an instanced model draw command with an instance range and an additional transform.
Draws 'count' instances starting at 'offset' in the instance buffer. Both 'offset' and 'count' are clamped to stay within [0, instances.capacity]:
- offset is clamped to [0, capacity]
- count is clamped to [0, capacity - offset] Does nothing if the resulting count is < = 0. The transform is applied to all instances.
The command is executed during R3D_End(). */
void R3D.DrawModelInstancedPro(Model model, InstanceBuffer instances, int offset, int count, Matrix4x4 transform);

/* Queues an animated model draw command.
Uses the provided animation player to compute the pose.
The command is executed during R3D_End(). */
void R3D.DrawAnimatedModel(Model model, AnimationPlayer player, Vector3 position, float scale);

/* Queues an animated model draw command with position, rotation and non-uniform scale.
Uses the provided animation player to compute the pose.
The command is executed during R3D_End(). */
void R3D.DrawAnimatedModelEx(Model model, AnimationPlayer player, Vector3 position, Quaternion rotation, Vector3 scale);

/* Queues an animated model draw command using a full transform matrix.
The command is executed during R3D_End(). */
void R3D.DrawAnimatedModelPro(Model model, AnimationPlayer player, Matrix4x4 transform);

/* Queues an instanced animated model draw command.
Draws multiple animated instances using the provided instance buffer. Does nothing if the number of instances is < = 0.
The command is executed during R3D_End(). */
void R3D.DrawAnimatedModelInstanced(Model model, AnimationPlayer player, InstanceBuffer instances, int count);

/* Queues an instanced animated model draw command with an instance range.
Draws 'count' animated instances starting at 'offset' in the instance buffer. Both 'offset' and 'count' are clamped to stay within [0, instances.capacity]:
- offset is clamped to [0, capacity]
- count is clamped to [0, capacity - offset] Does nothing if the resulting count is < = 0.
The command is executed during R3D_End(). */
void R3D.DrawAnimatedModelInstancedEx(Model model, AnimationPlayer player, InstanceBuffer instances, int offset, int count);

/* Queues an instanced animated model draw command with an instance range and an additional transform.
Draws 'count' animated instances starting at 'offset' in the instance buffer. Both 'offset' and 'count' are clamped to stay within [0, instances.capacity]:
- offset is clamped to [0, capacity]
- count is clamped to [0, capacity - offset] Does nothing if the resulting count is < = 0. The transform is applied to all instances.
The command is executed during R3D_End(). */
void R3D.DrawAnimatedModelInstancedPro(Model model, AnimationPlayer player, InstanceBuffer instances, int offset, int count, Matrix4x4 transform);

/* Queues a decal draw command with position and uniform scale.
The command is executed during R3D_End(). */
void R3D.DrawDecal(Decal decal, Vector3 position, float scale);

/* Queues a decal draw command with position, rotation and non-uniform scale.
The command is executed during R3D_End(). */
void R3D.DrawDecalEx(Decal decal, Vector3 position, Quaternion rotation, Vector3 scale);

/* Queues a decal draw command using a full transform matrix.
The command is executed during R3D_End(). */
void R3D.DrawDecalPro(Decal decal, Matrix4x4 transform);

/* Queues an instanced decal draw command.
Draws multiple instances using the provided instance buffer. Does nothing if the number of instances is < = 0.
The command is executed during R3D_End(). */
void R3D.DrawDecalInstanced(Decal decal, InstanceBuffer instances, int count);

/* Queues an instanced decal draw command with an instance range.
Draws 'count' instances starting at 'offset' in the instance buffer. Both 'offset' and 'count' are clamped to stay within [0, instances.capacity]:
- offset is clamped to [0, capacity]
- count is clamped to [0, capacity - offset] Does nothing if the resulting count is < = 0.
The command is executed during R3D_End(). */
void R3D.DrawDecalInstancedEx(Decal decal, InstanceBuffer instances, int offset, int count);

/* Queues an instanced decal draw command with an instance range and an additional transform.
Draws 'count' instances starting at 'offset' in the instance buffer. Both 'offset' and 'count' are clamped to stay within [0, instances.capacity]:
- offset is clamped to [0, capacity]
- count is clamped to [0, capacity - offset] Does nothing if the resulting count is < = 0. The transform is applied to all instances.
The command is executed during R3D_End(). */
void R3D.DrawDecalInstancedPro(Decal decal, InstanceBuffer instances, int offset, int count, Matrix4x4 transform);

```
### R3D.environment Functions
```csharp
/* Retrieves a pointer to the current environment configuration.
Provides direct read/write access to environment settings. Modifications take effect immediately. */
Environment* R3D.GetEnvironment();

/* Replaces the entire environment configuration.
Copies all settings from the provided structure to the active environment. Useful for switching between presets or restoring saved states. */
void R3D.SetEnvironment(Environment* env);

```
### R3D.importer Functions
```csharp
/* Load an importer from a file.
Creates an importer instance from the specified file path. The file is parsed once and can be reused to extract multiple resources such as models and animations. */
Importer R3D.LoadImporter(string filePath, ImportFlags flags);

/* Load an importer from a memory buffer.
Creates an importer instance from in-memory asset data. This is useful for embedded assets or streamed content. */
Importer R3D.LoadImporterFromMemory(void* data, uint size, string hint, ImportFlags flags);

/* Destroy an importer instance.
Frees all resources associated with the importer. Any models or animations extracted from it remain valid. */
void R3D.UnloadImporter(Importer importer);

```
### R3D.instance Functions
```csharp
/* Create instance buffers on the GPU. */
InstanceBuffer R3D.LoadInstanceBuffer(int capacity, InstanceFlags flags);

/* Destroy all GPU buffers owned by this instance buffer. */
void R3D.UnloadInstanceBuffer(InstanceBuffer buffer);

/* Upload a contiguous range of instance data. */
void R3D.UploadInstances(InstanceBuffer buffer, InstanceFlags flag, int offset, int count, IntPtr data);

/* Map an attribute buffer for CPU write access. */
IntPtr R3D.MapInstances(InstanceBuffer buffer, InstanceFlags flag);

/* Unmap one or more previously mapped attribute buffers. */
void R3D.UnmapInstances(InstanceBuffer buffer, InstanceFlags flags);

```
### R3D.kinematics Functions
```csharp
/* Check if capsule intersects with box */
bool R3D.CheckCollisionCapsuleBox(Capsule capsule, BoundingBox box);

/* Check if capsule intersects with sphere */
bool R3D.CheckCollisionCapsuleSphere(Capsule capsule, Vector3 center, float radius);

/* Check if two capsules intersect */
bool R3D.CheckCollisionCapsules(Capsule a, Capsule b);

/* Check if capsule intersects with mesh */
bool R3D.CheckCollisionCapsuleMesh(Capsule capsule, MeshData mesh, Matrix4x4 transform);

/* Check penetration between capsule and box */
Penetration R3D.CheckPenetrationCapsuleBox(Capsule capsule, BoundingBox box);

/* Check penetration between capsule and sphere */
Penetration R3D.CheckPenetrationCapsuleSphere(Capsule capsule, Vector3 center, float radius);

/* Check penetration between two capsules */
Penetration R3D.CheckPenetrationCapsules(Capsule a, Capsule b);

/* Calculate slide velocity along surface */
Vector3 R3D.SlideVelocity(Vector3 velocity, Vector3 normal);

/* Calculate bounce velocity after collision */
Vector3 R3D.BounceVelocity(Vector3 velocity, Vector3 normal, float bounciness);

/* Slide sphere along box surface, resolving collisions */
Vector3 R3D.SlideSphereBox(Vector3 center, float radius, Vector3 velocity, BoundingBox box, ref Vector3 outNormal);

/* Slide sphere along mesh surface, resolving collisions */
Vector3 R3D.SlideSphereMesh(Vector3 center, float radius, Vector3 velocity, MeshData mesh, Matrix4x4 transform, ref Vector3 outNormal);

/* Slide capsule along box surface, resolving collisions */
Vector3 R3D.SlideCapsuleBox(Capsule capsule, Vector3 velocity, BoundingBox box, ref Vector3 outNormal);

/* Slide capsule along mesh surface, resolving collisions */
Vector3 R3D.SlideCapsuleMesh(Capsule capsule, Vector3 velocity, MeshData mesh, Matrix4x4 transform, ref Vector3 outNormal);

/* Push sphere out of box if penetrating */
bool R3D.DepenetrateSphereBox(ref Vector3 center, float radius, BoundingBox box, ref float outPenetration);

/* Push capsule out of box if penetrating */
bool R3D.DepenetrateCapsuleBox(ref Capsule capsule, BoundingBox box, ref float outPenetration);

/* Cast a ray against mesh geometry */
RayCollision R3D.RaycastMesh(Ray ray, MeshData mesh, Matrix4x4 transform);

/* Cast a ray against a model (tests all meshes) */
RayCollision R3D.RaycastModel(Ray ray, Model model, Matrix4x4 transform);

/* Sweep sphere against single point */
SweepCollision R3D.SweepSpherePoint(Vector3 center, float radius, Vector3 velocity, Vector3 point);

/* Sweep sphere against line segment */
SweepCollision R3D.SweepSphereSegment(Vector3 center, float radius, Vector3 velocity, Vector3 a, Vector3 b);

/* Sweep sphere against triangle plane (no edge/vertex clipping) */
SweepCollision R3D.SweepSphereTrianglePlane(Vector3 center, float radius, Vector3 velocity, Vector3 a, Vector3 b, Vector3 c);

/* Sweep sphere against triangle with edge/vertex handling */
SweepCollision R3D.SweepSphereTriangle(Vector3 center, float radius, Vector3 velocity, Vector3 a, Vector3 b, Vector3 c);

/* Sweep sphere along velocity vector */
SweepCollision R3D.SweepSphereBox(Vector3 center, float radius, Vector3 velocity, BoundingBox box);

/* Sweep sphere along velocity vector against mesh geometry */
SweepCollision R3D.SweepSphereMesh(Vector3 center, float radius, Vector3 velocity, MeshData mesh, Matrix4x4 transform);

/* Sweep capsule along velocity vector */
SweepCollision R3D.SweepCapsuleBox(Capsule capsule, Vector3 velocity, BoundingBox box);

/* Sweep capsule along velocity vector against mesh geometry */
SweepCollision R3D.SweepCapsuleMesh(Capsule capsule, Vector3 velocity, MeshData mesh, Matrix4x4 transform);

/* Check if sphere is grounded against a box */
bool R3D.IsSphereGroundedBox(Vector3 center, float radius, float checkDistance, BoundingBox ground, ref RayCollision outGround);

/* Check if sphere is grounded against mesh geometry */
bool R3D.IsSphereGroundedMesh(Vector3 center, float radius, float checkDistance, MeshData mesh, Matrix4x4 transform, ref RayCollision outGround);

/* Check if capsule is grounded against a box */
bool R3D.IsCapsuleGroundedBox(Capsule capsule, float checkDistance, BoundingBox ground, ref RayCollision outGround);

/* Check if capsule is grounded against mesh geometry */
bool R3D.IsCapsuleGroundedMesh(Capsule capsule, float checkDistance, MeshData mesh, Matrix4x4 transform, ref RayCollision outGround);

/* Find closest point on line segment to given point */
Vector3 R3D.ClosestPointOnSegment(Vector3 point, Vector3 start, Vector3 end);

/* Find closest point on triangle to given point */
Vector3 R3D.ClosestPointOnTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c);

/* Find closest point on box surface to given point */
Vector3 R3D.ClosestPointOnBox(Vector3 point, BoundingBox box);

```
### R3D.lighting Functions
```csharp
/* Creates a new light of the specified type.
This function creates a light of the given type. The light must be destroyed manually when no longer needed by calling `R3D_DestroyLight`. */
Light R3D.CreateLight(LightType type);

/* Destroys the specified light.
This function deallocates the resources associated with the light and makes the light ID invalid. It must be called after the light is no longer needed. */
void R3D.DestroyLight(Light id);

/* Checks if a light exists.
This function checks if the specified light ID is valid and if the light exists. */
bool R3D.IsLightExist(Light id);

/* Gets the type of a light.
This function returns the type of the specified light (directional, spot or omni-directional). */
LightType R3D.GetLightType(Light id);

/* Checks if a light is active.
This function checks whether the specified light is currently active (enabled or disabled). */
bool R3D.IsLightActive(Light id);

/* Toggles the state of a light (active or inactive).
This function toggles the state of the specified light, turning it on if it is off, or off if it is on. */
void R3D.ToggleLight(Light id);

/* Sets the active state of a light.
This function allows manually turning a light on or off by specifying its active state. */
void R3D.SetLightActive(Light id, [MarshalAs(UnmanagedType.I1)] bool active);

/* Gets the color of a light.
This function retrieves the color of the specified light as a `Color` structure. */
Color R3D.GetLightColor(Light id);

/* Gets the color of a light as a `Vector3`.
This function retrieves the color of the specified light as a `Vector3`, where each component (x, y, z) represents the RGB values of the light. */
Vector3 R3D.GetLightColorV(Light id);

/* Sets the color of a light.
This function sets the color of the specified light using a `Color` structure. */
void R3D.SetLightColor(Light id, Color color);

/* Sets the color of a light using a `Vector3`.
This function sets the color of the specified light using a `Vector3`, where each component (x, y, z) represents the RGB values of the light. */
void R3D.SetLightColorV(Light id, Vector3 color);

/* Gets the position of a light.
This function retrieves the position of the specified light. Only applicable to spot lights or omni-lights. */
Vector3 R3D.GetLightPosition(Light id);

/* Sets the position of a light.
This function sets the position of the specified light. Only applicable to spot lights or omni-lights. */
void R3D.SetLightPosition(Light id, Vector3 position);

/* Gets the direction of a light.
This function retrieves the direction of the specified light. Only applicable to directional lights or spot lights. */
Vector3 R3D.GetLightDirection(Light id);

/* Sets the direction of a light.
This function sets the direction of the specified light. Only applicable to directional lights or spot lights. */
void R3D.SetLightDirection(Light id, Vector3 direction);

/* Sets the position and direction of a light to look at a target point.
This function sets both the position and the direction of the specified light, causing it to &quot;look at&quot; a given target point. */
void R3D.LightLookAt(Light id, Vector3 position, Vector3 target);

/* Gets the energy level of a light.
This function retrieves the energy level (intensity) of the specified light. Energy typically affects the brightness of the light. */
float R3D.GetLightEnergy(Light id);

/* Sets the energy level of a light.
This function sets the energy (intensity) of the specified light. A higher energy value will result in a brighter light. */
void R3D.SetLightEnergy(Light id, float energy);

/* Gets the specular intensity of a light.
This function retrieves the current specular intensity of the specified light. Specular intensity affects how shiny surfaces appear when reflecting the light. */
float R3D.GetLightSpecular(Light id);

/* Sets the specular intensity of a light.
This function sets the specular intensity of the specified light. Higher specular values result in stronger and sharper highlights on reflective surfaces. */
void R3D.SetLightSpecular(Light id, float specular);

/* Gets the range of a light.
This function retrieves the range of the specified light, which determines how far the light can affect. Only applicable to spot lights or omni-lights. */
float R3D.GetLightRange(Light id);

/* Sets the range parameter of a light.
For spot and omni lights, this defines the maximum illumination distance. For directional lights, this defines the shadow rendering radius around the camera. */
void R3D.SetLightRange(Light id, float range);

/* Gets the attenuation factor of a light.
This function retrieves the attenuation factor of the specified light. Attenuation controls how the intensity of a light decreases with distance. Only applicable to spot lights or omni-lights. */
float R3D.GetLightAttenuation(Light id);

/* Sets the attenuation factor of a light.
This function sets the attenuation factor of the specified light. A higher attenuation value causes the light to lose intensity more quickly as the distance increases. For a realistic effect, an attenuation factor of 2.0f is typically used. Only applicable to spot lights or omni-lights. */
void R3D.SetLightAttenuation(Light id, float attenuation);

/* Gets the inner cutoff angle of a spotlight.
This function retrieves the inner cutoff angle of a spotlight. The inner cutoff defines the cone of light where the light is at full intensity. */
float R3D.GetLightInnerCutOff(Light id);

/* Sets the inner cutoff angle of a spotlight.
This function sets the inner cutoff angle of a spotlight. The inner cutoff angle defines the cone where the light is at full intensity. Anything outside this cone starts to fade. */
void R3D.SetLightInnerCutOff(Light id, float degrees);

/* Gets the outer cutoff angle of a spotlight.
This function retrieves the outer cutoff angle of a spotlight. The outer cutoff defines the outer boundary of the light's cone, where the light starts to fade. */
float R3D.GetLightOuterCutOff(Light id);

/* Sets the outer cutoff angle of a spotlight.
This function sets the outer cutoff angle of a spotlight. The outer cutoff defines the boundary of the light's cone where the light intensity starts to gradually decrease. */
void R3D.SetLightOuterCutOff(Light id, float degrees);

/* Enables shadow rendering for a light.
Turns on shadow rendering for the light. The engine will allocate a shadow map if needed, or reuse one previously allocated for another light.
Shadow map resolutions are fixed: 2048x2048 for spot and point lights, and 4096x4096 for directional lights. */
void R3D.EnableShadow(Light id);

/* Disables shadow rendering for a light.
Turns off shadow rendering for the light. The associated shadow map is kept in memory and may later be reused by another light. */
void R3D.DisableShadow(Light id);

/* Checks if shadow casting is enabled for a light.
This function checks if shadow casting is currently enabled for the specified light. */
bool R3D.IsShadowEnabled(Light id);

/* Gets the shadow map update mode of a light.
This function retrieves the current mode for updating the shadow map of a light. The mode can be:
- Interval: Updates the shadow map at a fixed interval.
- Continuous: Updates the shadow map continuously.
- Manual: Updates the shadow map manually (via explicit function calls). */
ShadowUpdateMode R3D.GetShadowUpdateMode(Light id);

/* Sets the shadow map update mode of a light.
This function sets the mode for updating the shadow map of the specified light. The update mode controls when and how often the shadow map is refreshed. */
void R3D.SetShadowUpdateMode(Light id, ShadowUpdateMode mode);

/* Gets the frequency of shadow map updates for the interval update mode.
This function retrieves the frequency (in milliseconds) at which the shadow map should be updated when the interval update mode is enabled. This function is only relevant if the shadow map update mode is set to &quot;Interval&quot;. */
int R3D.GetShadowUpdateFrequency(Light id);

/* Sets the frequency of shadow map updates for the interval update mode.
This function sets the frequency (in milliseconds) at which the shadow map should be updated when the interval update mode is enabled. This function is only relevant if the shadow map update mode is set to &quot;Interval&quot;. */
void R3D.SetShadowUpdateFrequency(Light id, int msec);

/* Forces an immediate update of the shadow map during the next rendering pass.
This function forces the shadow map of the specified light to be updated during the next call to `R3D_End`. This is primarily used for the manual update mode, but may also work for the interval mode. */
void R3D.UpdateShadowMap(Light id);

/* Retrieves the softness radius used to simulate penumbra in shadows.
The softness is expressed as a sampling radius in texels within the shadow map. */
float R3D.GetShadowSoftness(Light id);

/* Sets the softness radius used to simulate penumbra in shadows.
This function adjusts the softness of the shadow edges for the specified light. The softness value corresponds to a number of texels in the shadow map, independent of its resolution. Larger values increase the blur radius, resulting in softer, more diffuse shadows, while smaller values yield sharper shadows. */
void R3D.SetShadowSoftness(Light id, float softness);

/* Gets the shadow depth bias value. */
float R3D.GetShadowDepthBias(Light id);

/* Sets the shadow depth bias value.
A higher bias helps reduce &quot;shadow acne&quot; artifacts (shadows flickering or appearing misaligned on surfaces). Be careful: too large values may cause shadows to look detached or floating away from objects. */
void R3D.SetShadowDepthBias(Light id, float value);

/* Gets the shadow slope bias value. */
float R3D.GetShadowSlopeBias(Light id);

/* Sets the shadow slope bias value.
This bias mainly compensates artifacts on surfaces angled relative to the light. It helps prevent shadows from incorrectly appearing or disappearing along object edges. */
void R3D.SetShadowSlopeBias(Light id, float value);

/* Returns the bounding box encompassing the light's area of influence.
This function computes the axis-aligned bounding box (AABB) that encloses the volume affected by the specified light, based on its type:
- For spotlights, the bounding box encloses the light cone.
- For omni-directional lights, it encloses a sphere representing the light's range.
- For directional lights, it returns an infinite bounding box to represent global influence.
This bounding box is primarily useful for spatial partitioning, culling, or visual debugging. */
BoundingBox R3D.GetLightBoundingBox(Light light);

/* Draws the area of influence of the light in 3D space.
This function visualizes the area affected by a light in 3D space. It draws the light's influence, such as the cone for spotlights or the volume for omni-lights. This function is only relevant for spotlights and omni-lights. */
void R3D.DrawLightShape(Light id);

```
### R3D.material Functions
```csharp
/* Get the default material configuration.
Returns `R3D_MATERIAL_BASE` by default, or the material defined via `R3D_SetDefaultMaterial()`. */
Material R3D.GetDefaultMaterial();

/* Set the default material configuration.
Allows you to override the default material. The default material will be used as the basis for loading 3D models. */
void R3D.SetDefaultMaterial(Material material);

/* Load materials from a file.
Parses a 3D model file and loads its associated materials. */
Material* R3D.LoadMaterials(string filePath, ref int materialCount);

/* Load materials from memory.
Loads materials directly from a memory buffer containing 3D model data. */
Material* R3D.LoadMaterialsFromMemory(void* data, uint size, string hint, ref int materialCount);

/* Load materials from an importer.
Loads materials that were previously imported via an R3D_Importer instance. */
Material* R3D.LoadMaterialsFromImporter(Importer importer, ref int materialCount);

/* Unload a material and its associated textures.
Frees all memory associated with a material, including its textures. This function will unload all textures that are not default textures. */
void R3D.UnloadMaterial(Material material);

/* Load an albedo (base color) map from file.
Loads an image, uploads it as an sRGB texture (if enabled), and applies the provided tint color. */
AlbedoMap R3D.LoadAlbedoMap(string fileName, Color color);

/* Load an albedo (base color) map from memory.
Same behavior as R3D_LoadAlbedoMap(), but reads from memory instead of disk. */
AlbedoMap R3D.LoadAlbedoMapFromMemory(string fileType, void* fileData, int dataSize, Color color);

/* Unload an albedo map texture.
Frees the underlying texture unless it is a default texture. */
void R3D.UnloadAlbedoMap(AlbedoMap map);

/* Load an emission map from file.
Loads an emissive texture (sRGB if enabled) and sets color + energy. */
EmissionMap R3D.LoadEmissionMap(string fileName, Color color, float energy);

/* Load an emission map from memory.
Same behavior as R3D_LoadEmissionMap(), but reads from memory. */
EmissionMap R3D.LoadEmissionMapFromMemory(string fileType, void* fileData, int dataSize, Color color, float energy);

/* Unload an emission map texture.
Frees the texture unless it is a default texture. */
void R3D.UnloadEmissionMap(EmissionMap map);

/* Load a normal map from file.
Uploads the texture in linear space and stores the normal scale factor. */
NormalMap R3D.LoadNormalMap(string fileName, float scale);

/* Load a normal map from memory.
Same behavior as R3D_LoadNormalMap(), but reads from memory. */
NormalMap R3D.LoadNormalMapFromMemory(string fileType, void* fileData, int dataSize, float scale);

/* Unload a normal map texture.
Frees the texture unless it is a default texture. */
void R3D.UnloadNormalMap(NormalMap map);

/* Load a combined ORM (Occlusion-Roughness-Metalness) map from file.
Uploads the texture in linear space and applies the provided multipliers. */
OrmMap R3D.LoadOrmMap(string fileName, float occlusion, float roughness, float metalness);

/* Load a combined ORM (Occlusion-Roughness-Metalness) map from memory.
Same behavior as R3D_LoadOrmMap(), but reads from memory. */
OrmMap R3D.LoadOrmMapFromMemory(string fileType, void* fileData, int dataSize, float occlusion, float roughness, float metalness);

/* Unload an ORM map texture.
Frees the texture unless it is a default texture. */
void R3D.UnloadOrmMap(OrmMap map);

```
### R3D.mesh_data Functions
```csharp
/* Allocates a mesh data container with the given capacity.
This function allocates CPU-side buffers for vertices and indices, but does NOT initialize the mesh with any actual data. The returned R3D_MeshData has:
- vertexCapacity and indexCapacity set to the requested sizes
- vertexCount and indexCount set to 0
You must manually set vertexCount/indexCount after filling the buffers, or use helper functions like R3D_AppendMeshData() to populate the mesh.
All allocated memory is zero-initialized. */
MeshData R3D.LoadMeshData(int vertexCount, int indexCount);

/* Releases memory used by a mesh data container. */
void R3D.UnloadMeshData(MeshData meshData);

/* Check if mesh data is valid.
Returns true if the mesh data contains at least one vertex buffer with a positive number of vertices. */
bool R3D.IsMeshDataValid(MeshData meshData);

/* Generate a quad mesh with specified dimensions, resolution, and orientation.
Creates a flat rectangular quad mesh with customizable facing direction. The mesh can be subdivided for higher resolution or displacement mapping. The quad is centered at the origin and oriented according to the frontDir parameter, which defines both the face direction and the surface normal. */
MeshData R3D.GenMeshDataQuad(float width, float length, int resX, int resZ, Vector3 frontDir);

/* Generate a plane mesh with specified dimensions and resolution.
Creates a flat plane mesh in the XZ plane, centered at the origin. The mesh can be subdivided for higher resolution or displacement mapping. */
MeshData R3D.GenMeshDataPlane(float width, float length, int resX, int resZ);

/* Generate a polygon mesh with specified number of sides.
Creates a regular polygon mesh centered at the origin in the XY plane. The polygon is generated with vertices evenly distributed around a circle. */
MeshData R3D.GenMeshDataPoly(int sides, float radius, Vector3 frontDir);

/* Generate a cube mesh with specified dimensions.
Creates a cube mesh centered at the origin with the specified width, height, and length. Each face consists of two triangles with proper normals and texture coordinates. */
MeshData R3D.GenMeshDataCube(float width, float height, float length);

/* Generate a subdivided cube mesh with specified dimensions.
Extension of R3D_GenMeshDataCube() allowing per-axis subdivision. Each face can be tessellated along the X, Y, and Z axes according to the provided resolutions. */
MeshData R3D.GenMeshDataCubeEx(float width, float height, float length, int resX, int resY, int resZ);

/* Generate a slope mesh by cutting a cube with a plane.
Creates a slope mesh by slicing a cube with a plane that passes through the origin. The plane is defined by its normal vector, and the portion of the cube on the side opposite to the normal direction is kept. This allows creating ramps, wedges, and angled surfaces with arbitrary orientations. */
MeshData R3D.GenMeshDataSlope(float width, float height, float length, Vector3 slopeNormal);

/* Generate a sphere mesh with specified parameters.
Creates a UV sphere mesh centered at the origin using latitude-longitude subdivision. Higher ring and slice counts produce smoother spheres but with more vertices. */
MeshData R3D.GenMeshDataSphere(float radius, int rings, int slices);

/* Generate a hemisphere mesh with specified parameters.
Creates a half-sphere mesh (dome) centered at the origin, extending upward in the Y axis. Uses the same UV sphere generation technique as R3D_GenMeshSphere but only the upper half. */
MeshData R3D.GenMeshDataHemiSphere(float radius, int rings, int slices);

/* Generates a cylinder mesh centered at the origin along the Y axis.
Both caps are included. For a cone or truncated cone, use R3D_GenMeshDataCylinderEx. */
MeshData R3D.GenMeshDataCylinder(float radius, float height, int slices);

/* Generates a cylinder, cone, or truncated cone mesh centered at the origin along the Y axis.
The bottom cap sits at Y = -height/2 and the top cap at Y = +height/2. Setting one radius to 0 produces a cone; caps can be toggled independently. */
MeshData R3D.GenMeshDataCylinderEx(float bottomRadius, float topRadius, float height, int slices, int stacks, [MarshalAs(UnmanagedType.I1)] bool bottomCap, [MarshalAs(UnmanagedType.I1)] bool topCap);

/* Generate a capsule mesh with specified parameters.
Creates a capsule mesh centered at the origin, extending along the Y axis. The capsule consists of a cylindrical body with hemispherical caps on both ends. The total height of the capsule is height + 2 * radius. */
MeshData R3D.GenMeshDataCapsule(float radius, float height, int rings, int slices);

/* Generate a torus mesh with specified parameters.
Creates a torus (donut shape) mesh centered at the origin in the XZ plane. The torus is defined by a major radius (distance from center to tube center) and a minor radius (tube thickness). */
MeshData R3D.GenMeshDataTorus(float radius, float size, int radSeg, int sides);

/* Generate a trefoil knot mesh with specified parameters.
Creates a trefoil knot mesh, which is a mathematical knot shape. Similar to a torus but with a twisted, knotted topology. */
MeshData R3D.GenMeshDataKnot(float radius, float size, int radSeg, int sides);

/* Generate a terrain mesh from a heightmap image.
Creates a terrain mesh by interpreting the brightness values of a heightmap image as height values. The resulting mesh represents a 3D terrain surface. */
MeshData R3D.GenMeshDataHeightmap(Image heightmap, Vector3 size);

/* Generate a voxel-style mesh from a cubicmap image.
Creates a mesh composed of cubes based on a cubicmap image, where each pixel represents the presence or absence of a cube at that position. Useful for creating voxel-based or block-based geometry. */
MeshData R3D.GenMeshDataCubicmap(Image cubicmap, Vector3 cubeSize);

/* Reserves memory for the specified number of vertices and indices. */
void R3D.ReserveMeshData(ref MeshData meshData, int vertexCount, int indexCount);

/* Shrinks allocated memory to fit the current vertex and index counts. */
void R3D.ShrinkMeshData(ref MeshData meshData);

/* Clears all vertices and indices without releasing allocated memory. */
void R3D.ResetMeshData(ref MeshData meshData);

/* Creates a deep copy of an existing mesh data container. */
MeshData R3D.CopyMeshData(MeshData meshData);

/* Merges two mesh data containers into a single one. */
MeshData R3D.MergeMeshData(MeshData a, MeshData b);

/* Appends vertices and indices to the mesh data container. */
void R3D.AppendMeshData(ref MeshData meshData, ref Vertex vertices, int vertexCount, ref uint indices, int indexCount);

/* Applies a transformation matrix to all vertices in the mesh data. */
void R3D.TransformMeshData(ref MeshData meshData, Matrix4x4 transform);

/* Translates all vertices by a given offset. */
void R3D.TranslateMeshData(ref MeshData meshData, Vector3 translation);

/* Rotates all vertices using a quaternion. */
void R3D.RotateMeshData(ref MeshData meshData, Quaternion rotation);

/* Scales all vertices by given factors. */
void R3D.ScaleMeshData(ref MeshData meshData, Vector3 scale);

/* Generates planar UV coordinates. */
void R3D.GenMeshDataUVsPlanar(ref MeshData meshData, Vector2 uvScale, Vector3 axis);

/* Generates spherical UV coordinates. */
void R3D.GenMeshDataUVsSpherical(ref MeshData meshData);

/* Generates cylindrical UV coordinates. */
void R3D.GenMeshDataUVsCylindrical(ref MeshData meshData);

/* Computes vertex normals from triangle geometry. */
void R3D.GenMeshDataNormals(ref MeshData meshData, PrimitiveType type);

/* Computes vertex tangents based on existing normals and UVs. */
void R3D.GenMeshDataTangents(ref MeshData meshData, PrimitiveType type);

/* Calculates the axis-aligned bounding box of the mesh. */
BoundingBox R3D.CalculateMeshDataBoundingBox(MeshData meshData);

```
### R3D.mesh Functions
```csharp
/* Creates a 3D mesh from CPU-side mesh data. */
Mesh R3D.LoadMesh(PrimitiveType type, MeshData data, BoundingBox* aabb, MeshUsage usage);

/* Destroys a 3D mesh and frees its resources. */
void R3D.UnloadMesh(Mesh mesh);

/* Check if a mesh is valid for rendering.
Returns true if the mesh has a valid VAO and VBO. */
bool R3D.IsMeshValid(Mesh mesh);

/* Generate a quad mesh with orientation. */
Mesh R3D.GenMeshQuad(float width, float length, int resX, int resZ, Vector3 frontDir);

/* Generate a plane mesh. */
Mesh R3D.GenMeshPlane(float width, float length, int resX, int resZ);

/* Generate a polygon mesh. */
Mesh R3D.GenMeshPoly(int sides, float radius, Vector3 frontDir);

/* Generate a cube mesh. */
Mesh R3D.GenMeshCube(float width, float height, float length);

/* Generate a subdivided cube mesh. */
Mesh R3D.GenMeshCubeEx(float width, float height, float length, int resX, int resY, int resZ);

/* Generate a slope mesh. */
Mesh R3D.GenMeshSlope(float width, float height, float length, Vector3 slopeNormal);

/* Generate a sphere mesh. */
Mesh R3D.GenMeshSphere(float radius, int rings, int slices);

/* Generate a hemisphere mesh. */
Mesh R3D.GenMeshHemiSphere(float radius, int rings, int slices);

/* Generate a cylinder mesh. */
Mesh R3D.GenMeshCylinder(float radius, float height, int slices);

/* Generate a cylinder, cone or truncated cone mesh. */
Mesh R3D.GenMeshCylinderEx(float bottomRadius, float topRadius, float height, int slices, int stacks, [MarshalAs(UnmanagedType.I1)] bool bottomCap, [MarshalAs(UnmanagedType.I1)] bool topCap);

/* Generate a capsule mesh. */
Mesh R3D.GenMeshCapsule(float radius, float height, int rings, int slices);

/* Generate a torus mesh. */
Mesh R3D.GenMeshTorus(float radius, float size, int radSeg, int sides);

/* Generate a trefoil knot mesh. */
Mesh R3D.GenMeshKnot(float radius, float size, int radSeg, int sides);

/* Generate a heightmap terrain mesh. */
Mesh R3D.GenMeshHeightmap(Image heightmap, Vector3 size);

/* Generate a cubicmap voxel mesh. */
Mesh R3D.GenMeshCubicmap(Image cubicmap, Vector3 cubeSize);

/* Upload a mesh data on the GPU.
This function uploads a mesh's vertex and optional index data to the GPU.
If `aabb` is provided, it will be used as the mesh's bounding box; if null, the bounding box is automatically recalculated from the vertex data. */
bool R3D.UpdateMesh(ref Mesh mesh, MeshData data, BoundingBox* aabb);

```
### R3D.model Functions
```csharp
/* Load a 3D model from a file.
Loads a 3D model from the specified file path. Supports various 3D file formats and automatically parses meshes, materials, and texture references. */
Model R3D.LoadModel(string filePath);

/* Load a 3D model from a file with import flags.
Extended version of R3D_LoadModel() allowing control over the import process through additional flags. */
Model R3D.LoadModelEx(string filePath, ImportFlags flags);

/* Load a 3D model from memory buffer.
Loads a 3D model from a memory buffer containing the file data. Useful for loading models from embedded resources or network streams. */
Model R3D.LoadModelFromMemory(void* data, uint size, string hint);

/* Load a 3D model from a memory buffer with import flags.
Extended version of R3D_LoadModelFromMemory() allowing control over the import process through additional flags. */
Model R3D.LoadModelFromMemoryEx(void* data, uint size, string hint, ImportFlags flags);

/* Load a 3D model from an existing importer.
Creates a model from a previously loaded importer instance. This avoids re-importing the source file. */
Model R3D.LoadModelFromImporter(Importer importer);

/* Unload a model and optionally its materials.
Frees all memory associated with a model, including its meshes. Materials can be optionally unloaded as well. */
void R3D.UnloadModel(Model model, [MarshalAs(UnmanagedType.I1)] bool unloadMaterials);

```
### R3D.probe Functions
```csharp
/* Creates a new probe of the specified type.
The returned probe must be destroyed using ::R3D_DestroyProbe when it is no longer needed. */
Probe R3D.CreateProbe(ProbeFlags flags);

/* Destroys a probe and frees its resources. */
void R3D.DestroyProbe(Probe id);

/* Returns whether a probe exists. */
bool R3D.IsProbeExist(Probe id);

/* Returns the probe flags. */
ProbeFlags R3D.GetProbeFlags(Probe id);

/* Returns whether a probe is currently active.
Inactive probes do not contribute to lighting. */
bool R3D.IsProbeActive(Probe id);

/* Enables or disables a probe. */
void R3D.SetProbeActive(Probe id, [MarshalAs(UnmanagedType.I1)] bool active);

/* Gets the probe update mode.
- R3D_PROBE_UPDATE_ONCE: Captured once, then reused unless its state changes.
- R3D_PROBE_UPDATE_ALWAYS: Recaptured every frame.
Use &quot;ONCE&quot; for static scenes, &quot;ALWAYS&quot; for highly dynamic scenes. */
ProbeUpdateMode R3D.GetProbeUpdateMode(Probe id);

/* Sets the probe update mode.
Controls when the probe capture is refreshed. */
void R3D.SetProbeUpdateMode(Probe id, ProbeUpdateMode mode);

/* Returns whether the probe is considered indoors.
Indoor probes do not sample skybox or environment maps. Instead they rely only on ambient and background colors.
Use this for rooms, caves, tunnels, etc... where outside lighting should not bleed inside. */
bool R3D.GetProbeInterior(Probe id);

/* Enables or disables indoor mode for the probe. */
void R3D.SetProbeInterior(Probe id, [MarshalAs(UnmanagedType.I1)] bool active);

/* Returns whether shadows are captured by this probe.
When enabled, shadowing is baked into the captured lighting. This improves realism, but increases capture cost. */
bool R3D.GetProbeShadows(Probe id);

/* Enables or disables shadow rendering during probe capture. */
void R3D.SetProbeShadows(Probe id, [MarshalAs(UnmanagedType.I1)] bool active);

/* Gets the world position of the probe. */
Vector3 R3D.GetProbePosition(Probe id);

/* Sets the world position of the probe. */
void R3D.SetProbePosition(Probe id, Vector3 position);

/* Gets the effective range of the probe.
The range defines the radius (in world units) within which this probe contributes to lighting. Objects outside this sphere receive no influence. */
float R3D.GetProbeRange(Probe id);

/* Sets the effective range of the probe. */
void R3D.SetProbeRange(Probe id, float range);

/* Gets the falloff factor applied to probe contributions.
Falloff controls how lighting fades as distance increases.
Internally this uses a power curve: attenuation = 1.0 - pow(dist / probe.range, probe.falloff)
Effects:
- falloff = 1 -> linear fade
- falloff > 1 -> light stays strong near the probe, drops faster at the edge
- falloff < 1 -> softer fade across the whole range */
float R3D.GetProbeFalloff(Probe id);

/* Sets the falloff factor used for distance attenuation.
Larger values make the probe feel more localized. */
void R3D.SetProbeFalloff(Probe id, float falloff);

```
### R3D.screen_shader Functions
```csharp
/* Loads a screen shader from a file.
The shader must define a single entry point: `void fragment()`. Any other entry point, such as `vertex()`, or any varyings will be ignored. */
ScreenShader R3D.LoadScreenShader(string filePath);

/* Loads a screen shader from a source code string in memory.
The shader must define a single entry point: `void fragment()`. Any other entry point, such as `vertex()`, or any varyings will be ignored. */
ScreenShader R3D.LoadScreenShaderFromMemory(string code);

/* Creates an alias of an existing screen shader.
The alias shares the same compiled program as the original but holds its own independent uniform and sampler state. Typical use cases include pre-configuring aliases for distinct effects (e.g. different convolution kernels), or running the same shader multiple times in a post-process chain with different parameters at each pass.
Uniform and sampler state is copied from the original at the moment this function is called, not from the shader source defaults. Any values set on the original after compilation but before this call will be reflected in the alias; values set afterward will not. */
ScreenShader R3D.LoadScreenShaderAlias(ScreenShader shader);

/* Unloads and destroys a screen shader.
If the shader owns its program shaders (i.e. it was created withR3D_LoadScreenShader orR3D_LoadScreenShaderFromMemory), they are deleted. Aliases created from this shader viaR3D_LoadScreenShaderAlias must be unloaded beforehand, as they share the same programs and will be left with dangling references. */
void R3D.UnloadScreenShader(ScreenShader shader);

/* Sets a uniform value for the current frame.
Once a uniform is set, it remains valid for the all frames. If an uniform is set multiple times during the same frame, the last value defined before R3D_End() is used.
Supported types: bool, int, float, ivec2, ivec3, ivec4, vec2, vec3, vec4, mat2, mat3, mat4 */
void R3D.SetScreenShaderUniform(ScreenShader shader, string name, void* value);

/* Sets a texture sampler for the current frame.
Once a sampler is set, it remains valid for all frames. If a sampler is set multiple times during the same frame, the last value defined before R3D_End() is used.
Supported samplers: sampler1D, sampler2D, sampler3D, samplerCube */
void R3D.SetScreenShaderSampler(ScreenShader shader, string name, Texture2D texture);

/* Sets the list of screen shaders to execute at the end of the frame.
The maximum number of shaders is defined by `R3D_MAX_SCREEN_SHADERS`. If the provided count exceeds this limit, a warning is emitted and only the first `R3D_MAX_SCREEN_SHADERS` shaders are used.
Shader pointers are copied internally, so the original array can be modified or freed after the call. NULL entries are allowed safely within the list.
Calling this function resets all internal screen shaders before copying the new list. To disable all screen shaders, call this function with `shaders = NULL` and/or `count = 0`. */
void R3D.SetScreenShaderChain(ScreenShader* shaders, int count);

```
### R3D.skeleton Functions
```csharp
/* Loads a skeleton hierarchy from a 3D model file.
Skeletons are automatically loaded when importing a model, but can be loaded manually for advanced use cases. */
Skeleton R3D.LoadSkeleton(string filePath);

/* Loads a skeleton hierarchy from memory data.
Allows manual loading of skeletons directly from a memory buffer. Typically used for advanced or custom asset loading workflows. */
Skeleton R3D.LoadSkeletonFromMemory(void* data, uint size, string hint);

/* Loads a skeleton hierarchy from an existing importer.
Extracts the skeleton data from a previously loaded importer instance. */
Skeleton R3D.LoadSkeletonFromImporter(Importer importer);

/* Frees the memory allocated for a skeleton. */
void R3D.UnloadSkeleton(Skeleton skeleton);

/* Check if a skeleton is valid.
Returns true if atleast the texBindPose is greater than zero. */
bool R3D.IsSkeletonValid(Skeleton skeleton);

/* Returns the index of the bone with the given name. */
int R3D.GetSkeletonBoneIndex(Skeleton skeleton, string boneName);

/* Returns a pointer to the bone with the given name. */
BoneInfo* R3D.GetSkeletonBone(Skeleton skeleton, string boneName);

```
### R3D.sky_shader Functions
```csharp
/* Loads a sky shader from a file.
The shader must define a single entry point: `void fragment()`. Any other entry point, such as `vertex()`, or any varyings will be ignored. */
SkyShader R3D.LoadSkyShader(string filePath);

/* Loads a sky shader from a source code string in memory.
The shader must define a single entry point: `void fragment()`. Any other entry point, such as `vertex()`, or any varyings will be ignored. */
SkyShader R3D.LoadSkyShaderFromMemory(string code);

/* Creates an alias of an existing sky shader.
The alias shares the same compiled program as the original but holds its own independent uniform and sampler state. A typical use case is to pre-configure several aliases with different uniforms or textures, avoiding the need to reconfigure the shader on every skybox switch.
Uniform and sampler state is copied from the original at the moment this function is called, not from the shader source defaults. Any values set on the original after compilation but before this call will be reflected in the alias; values set afterward will not. */
SkyShader R3D.LoadSkyShaderAlias(SkyShader shader);

/* Unloads and destroys a sky shader.
If the shader owns its program shaders (i.e. it was created withR3D_LoadSkyShader orR3D_LoadSkyShaderFromMemory), they are deleted. Aliases created from this shader viaR3D_LoadSkyShaderAlias must be unloaded beforehand, as they share the same programs and will be left with dangling references. */
void R3D.UnloadSkyShader(SkyShader shader);

/* Sets a uniform value for all subsequent sky generations.
Supported types: bool, int, float, ivec2, ivec3, ivec4, vec2, vec3, vec4, mat2, mat3, mat4 */
void R3D.SetSkyShaderUniform(SkyShader shader, string name, void* value);

/* Sets a uniform value for all subsequent sky generations.
Supported samplers: sampler1D, sampler2D, sampler3D, samplerCube */
void R3D.SetSkyShaderSampler(SkyShader shader, string name, Texture2D texture);

```
### R3D.sky Functions
```csharp
/* Generates a procedural sky cubemap.
Creates a GPU cubemap with procedural gradient sky and sun rendering. The cubemap is ready for use as environment map or IBL source. */
Cubemap R3D.GenProceduralSky(int size, ProceduralSky @params);

/* Generates a custom sky cubemap.
Creates a GPU cubemap rendered using the provided sky shader. The cubemap is ready for use as environment map or IBL source. */
Cubemap R3D.GenCustomSky(int size, SkyShader shader);

/* Updates an existing procedural sky cubemap.
Re-renders the cubemap with new parameters. Faster than unload + generate when animating sky conditions (time of day, weather, etc.). */
void R3D.UpdateProceduralSky(ref Cubemap cubemap, ProceduralSky @params);

/* Updates an existing custom sky cubemap.
Re-renders the cubemap using the provided sky shader. Faster than unload + generate when animating sky conditions or updating shader uniforms (time, clouds, stars, etc.). */
void R3D.UpdateCustomSky(ref Cubemap cubemap, SkyShader shader);

```
### R3D.surface_shader Functions
```csharp
/* Loads a surface shader from a file.
The shader must define at least one entry point: `void vertex()` or `void fragment()`. It can define either one or both. */
SurfaceShader R3D.LoadSurfaceShader(string filePath);

/* Loads a surface shader from a source code string in memory.
The shader must define at least one entry point: `void vertex()` or `void fragment()`. It can define either one or both. */
SurfaceShader R3D.LoadSurfaceShaderFromMemory(string code);

/* Creates an alias of an existing surface shader.
The alias shares the same compiled program as the original but holds its own independent uniform and sampler state, allowing the same shader to be used multiple times within a single frame with different values.
Uniform and sampler state is copied from the original at the moment this function is called, not from the shader source defaults. Any values set on the original after compilation but before this call will be reflected in the alias; values set afterward will not. */
SurfaceShader R3D.LoadSurfaceShaderAlias(SurfaceShader shader);

/* Unloads and destroys a surface shader.
If the shader owns its program shaders (i.e. it was created withR3D_LoadSurfaceShader orR3D_LoadSurfaceShaderFromMemory), they are deleted. Aliases created from this shader viaR3D_LoadSurfaceShaderAlias must be unloaded beforehand, as they share the same programs and will be left with dangling references. */
void R3D.UnloadSurfaceShader(SurfaceShader shader);

/* Sets a uniform value for the current frame.
Once a uniform is set, it remains valid for the all frames. If an uniform is set multiple times during the same frame, the last value defined before R3D_End() is used.
Supported types: bool, int, float, ivec2, ivec3, ivec4, vec2, vec3, vec4, mat2, mat3, mat4 */
void R3D.SetSurfaceShaderUniform(SurfaceShader shader, string name, void* value);

/* Sets a texture sampler for the current frame.
Once a sampler is set, it remains valid for all frames. If a sampler is set multiple times during the same frame, the last value defined before R3D_End() is used.
Supported samplers: sampler1D, sampler2D, sampler3D, samplerCube */
void R3D.SetSurfaceShaderSampler(SurfaceShader shader, string name, Texture2D texture);

```
### R3D.Utilities.cs Functions
```csharp
/* Handwritten utility methods extending the generated R3D bindings.
This file is preserved when regenerating bindings. */
public R3D.static unsafe partial class R3D

/* Gets a default material with sensible base values.
Use this as a starting point when creating new materials. */
public R3D.static Material MATERIAL_BASE =>

/* Gets default procedural sky parameters for cubemap generation.
Use this as a starting point when creating procedural skyboxes with <see cref="GenProceduralSky" />. */
public R3D.static ProceduralSky PROCEDURAL_SKY_BASE =>

/* Gets default decal parameters.
Use this as a starting point when creating decals. */
public R3D.static Decal DECAL_BASE =>

/* Gets a reference to the current environment settings.
Allows direct modification of environment properties without copying. */
public R3D.static ref Environment GetEnvironmentEx()

/* Updates the current environment settings using a callback.
This is the recommended way to modify environment settings. */
public R3D.static void SetEnvironmentEx(EnvironmentUpdater updater)

/* Sets the environment from a struct value.
Useful for restoring a previously saved environment. */
public R3D.static void SetEnvironmentEx(Environment env)

/* Maps instance buffer data to a typed span for direct CPU access.
Call <see cref="UnmapInstances" /> when done writing. */
public R3D.static Span<T> MapInstances<T>(InstanceBuffer buffer, InstanceFlags flag) where T : unmanaged

/* Uploads instance data from a span to the GPU. */
public R3D.static void UploadInstances<T>(InstanceBuffer buffer, InstanceFlags flag, int offset, ReadOnlySpan<T> data, int? count = null) where T : unmanaged

/* Sets a uniform value on a screen shader using a typed ref instead of void*. */
public R3D.static void SetScreenShaderUniform<T>(ScreenShader shader, string name, ref T value) where T : unmanaged

/* Sets a uniform value on a surface shader using a typed ref instead of void*. */
public R3D.static void SetSurfaceShaderUniform<T>(SurfaceShader shader, string name, ref T value) where T : unmanaged

/* Sets a uniform value on a sky shader using a typed ref instead of void*. */
public R3D.static void SetSkyShaderUniform<T>(SkyShader shader, string name, ref T value) where T : unmanaged

/* Creates a mesh data container with counts pre-set to the requested sizes.
Unlike <see cref="LoadMeshData" /> (which starts with count 0 for append workflows),
this method sets vertex/index counts equal to their capacities so that
<see cref="MeshData.Vertices" /> and <see cref="MeshData.Indices" /> are
immediately writable at full size. */
public R3D.static MeshData CreateMeshData(int vertexCount, int indexCount)

/* Creates a 3D mesh from CPU-side mesh data, computing the bounding box automatically. */
public R3D.static Mesh LoadMesh(PrimitiveType type, MeshData data, MeshUsage usage)

/* Creates a 3D mesh from CPU-side mesh data with an explicit bounding box. */
public R3D.static Mesh LoadMesh(PrimitiveType type, MeshData data, ref BoundingBox aabb, MeshUsage usage)

/* Updates an existing mesh with new CPU-side data, computing the bounding box automatically. */
public R3D.static bool UpdateMesh(ref Mesh mesh, MeshData data)

/* Updates an existing mesh with new CPU-side data and an explicit bounding box. */
public R3D.static bool UpdateMesh(ref Mesh mesh, MeshData data, ref BoundingBox aabb)

/* Sets the screen shader chain from a span of shaders. */
public R3D.static void SetScreenShaderChain(ReadOnlySpan<ScreenShader> shaders)

```
### R3D.utils Functions
```csharp
/* Retrieves a default white texture.
This texture is fully white (1,1,1,1), useful for default material properties. */
Texture2D R3D.GetWhiteTexture();

/* Retrieves a default black texture.
This texture is fully black (0,0,0,1), useful for masking or default values. */
Texture2D R3D.GetBlackTexture();

/* Retrieves a default normal map texture.
This texture represents a neutral normal map (0.5, 0.5, 1.0), which applies no normal variation. */
Texture2D R3D.GetNormalTexture();

/* Retrieves the buffer containing the scene's normal data.
This texture stores octahedral-compressed normals using two 16-bit per-channel RG components. */
Texture2D R3D.GetBufferNormal();

/* Retrieves the final depth buffer.
This texture is an R16 texture containing a linear depth value normalized between the near and far clipping planes.
The texture is intended for post-processing effects outside of R3D that require access to linear depth information. */
Texture2D R3D.GetBufferDepth();

/* Retrieves the view matrix.
This matrix represents the camera's transformation from world space to view space. It is updated at the last call to 'R3D_Begin'. */
Matrix4x4 R3D.GetMatrixView();

/* Retrieves the inverse view matrix.
This matrix transforms coordinates from view space back to world space. It is updated at the last call to 'R3D_Begin'. */
Matrix4x4 R3D.GetMatrixInvView();

/* Retrieves the projection matrix.
This matrix defines the transformation from view space to clip space. It is updated at the last call to 'R3D_Begin'. */
Matrix4x4 R3D.GetMatrixProjection();

/* Retrieves the inverse projection matrix.
This matrix transforms coordinates from clip space back to view space. It is updated at the last call to 'R3D_Begin'. */
Matrix4x4 R3D.GetMatrixInvProjection();

/* Retrieves the view-projection matrix.
This matrix represents the transformation from world space to clip space. It is updated at the last call to 'R3D_Begin'. */
Matrix4x4 R3D.GetMatrixViewProjection();

```
### R3D.visibility Functions
```csharp
/* Checks if a point is inside the view frustum.
Tests whether a 3D point lies within the camera's frustum by checking against all six planes. You can call this only after `R3D_Begin`, which updates the internal frustum state. */
bool R3D.IsPointVisible(Vector3 position);

/* Checks if a sphere is inside the view frustum.
Tests whether a sphere intersects the camera's frustum using plane-sphere tests. You can call this only after `R3D_Begin`, which updates the internal frustum state. */
bool R3D.IsSphereVisible(Vector3 position, float radius);

/* Checks if an AABB is inside the view frustum.
Determines whether an axis-aligned bounding box intersects the frustum. You can call this only after `R3D_Begin`, which updates the internal frustum state. */
bool R3D.IsBoundingBoxVisible(BoundingBox aabb);

/* Checks if an OBB is inside the view frustum.
Tests an oriented bounding box (transformed AABB) for frustum intersection. You can call this only after `R3D_Begin`, which updates the internal frustum state. */
bool R3D.IsOrientedBoxVisible(BoundingBox aabb, Matrix4x4 transform);

```
## Enums
Below is a list of all the enums in the library.
### AmbientFlags enum
```csharp
/// Bit-flags controlling what components are generated.
///  - R3D_AMBIENT_ILLUMINATION -> generate diffuse irradiance
///  - R3D_AMBIENT_REFLECTION   -> generate specular prefiltered map
[Flags]
public enum AmbientFlags : uint
{
    Illumination = (1<<0),
    Reflection = (1<<1),
}
```
### AnimationEvent enum
```csharp
/// Types of events that an animation player can emit.
public enum AnimationEvent
{
    /// Animation has finished playing (non-looping).
    AnimEventFinished,
    /// Animation has completed a loop.
    AnimEventLooped,
}
```
### AntiAliasingMode enum
```csharp
/// Anti-aliasing modes used during rendering.
/// Anti-aliasing reduces visible jagged edges (aliasing artifacts) in the final rendered image.
public enum AntiAliasingMode
{
    /// No anti-aliasing. Best performance, visible jagged edges.
    None,
    /// Fast Approximate AA. Smooths edges efficiently but may appear blurry.
    Fxaa,
    /// Subpixel Morphological AA. Sharper than FXAA, moderate performance cost.
    Smaa,
}
```
### AntiAliasingPreset enum
```csharp
/// Quality presets for anti-aliasing.
/// Presets adjust internal algorithm parameters (e.g. edge detection, search steps, thresholds). Higher presets increase quality and GPU cost.
public enum AntiAliasingPreset
{
    /// Performance-oriented preset with reduced quality.
    Low,
    /// Balanced quality/performance preset.
    Medium,
    /// High quality preset with increased GPU cost.
    High,
    /// Maximum quality preset, highest performance cost.
    Ultra,
    /// Number of presets (not a valid preset value).
    Count,
}
```
### AspectMode enum
```csharp
/// Aspect ratio handling modes for rendering.
public enum AspectMode
{
    /// Expands the rendered output to fully fill the target (render texture or window).
    Expand,
    /// Preserves the target's aspect ratio without distortion, adding empty gaps if necessary.
    Keep,
}
```
### BillboardMode enum
```csharp
/// Billboard modes.
/// This enumeration defines how a 3D object aligns itself relative to the camera. It provides options to disable billboarding or to enable specific modes of alignment.
public enum BillboardMode
{
    /// Billboarding is disabled; the object retains its original orientation.
    Disabled,
    /// Full billboarding; the object fully faces the camera, rotating on all axes.
    Front,
    /// Y-axis constrained billboarding; the object rotates only around the Y-axis, keeping its &quot;up&quot; orientation fixed. This is suitable for upright objects like characters or signs.
    YAxis,
}
```
### BlendMode enum
```csharp
/// Blend modes.
/// Defines common blending modes used in 3D rendering to combine source and destination colors.
public enum BlendMode
{
    /// Default mode: the result will be opaque or alpha blended depending on the transparency mode.
    Mix,
    /// Additive blending: source color is added to the destination, making bright effects.
    Additive,
    /// Multiply blending: source color is multiplied with the destination, darkening the image.
    Multiply,
    /// Premultiplied alpha blending: source color is blended with the destination assuming the source color is already multiplied by its alpha.
    PremultipliedAlpha,
}
```
### Bloom enum
```csharp
/// Bloom effect modes.
/// Different blending methods for the bloom glow effect.
public enum Bloom
{
    /// No bloom effect applied
    Disabled,
    /// Linear interpolation blend between scene and bloom
    Mix,
    /// Additive blending, intensifying bright regions
    Additive,
    /// Screen blending for softer highlight enhancement
    Screen,
}
```
### ColorSpace enum
```csharp
/// Specifies the color space for user-provided colors and color textures.
/// This enum defines how colors are interpreted for material inputs:
///  - Surface colors (e.g., albedo or emission tint)
///  - Color textures (albedo, emission maps)
/// Lighting values (direct or indirect light) are always linear and are not affected by this setting.
/// Used with `R3D_SetColorSpace()` to control whether input colors should be treated as linear or sRGB.
public enum ColorSpace
{
    /// Linear color space: values are used as-is.
    Linear,
    /// sRGB color space: values are converted to linear on load.
    Srgb,
}
```
### CompareMode enum
```csharp
/// Comparison modes.
/// Defines how fragments are tested against the depth/stencil buffer during rendering.
public enum CompareMode
{
    /// Passes if 'value' < 'buffer' (default)
    Less,
    /// Passes if 'value' < = 'buffer'
    Lequal,
    /// Passes if 'value' == 'buffer'
    Equal,
    /// Passes if 'value' >  'buffer'
    Greater,
    /// Passes if 'value' >= 'buffer'
    Gequal,
    /// Passes if 'value' != 'buffer'
    Notequal,
    /// Always passes
    Always,
    /// Never passes
    Never,
}
```
### CubemapLayout enum
```csharp
/// Supported cubemap source layouts.
/// Used when converting an image into a cubemap. AUTO_DETECT tries to guess the layout based on image dimensions.
public enum CubemapLayout
{
    /// Automatically detect layout type
    AutoDetect,
    /// Layout is defined by a vertical line with faces
    LineVertical,
    /// Layout is defined by a horizontal line with faces
    LineHorizontal,
    /// Layout is defined by a 3x4 cross with cubemap faces
    CrossThreeByFour,
    /// Layout is defined by a 4x3 cross with cubemap faces
    CrossFourByThree,
    /// Layout is defined by an equirectangular panorama
    Panorama,
}
```
### CullMode enum
```csharp
/// Face culling modes.
/// Specifies which faces of a geometry are discarded during rendering based on their winding order.
public enum CullMode
{
    /// No culling; all faces are rendered.
    None,
    /// Cull back-facing polygons (faces with clockwise winding order).
    Back,
    /// Cull front-facing polygons (faces with counter-clockwise winding order).
    Front,
}
```
### DoF enum
```csharp
/// Depth of field modes.
public enum DoF
{
    /// No depth of field effect
    Disabled,
    /// Depth of field enabled with focus point and blur
    Enabled,
}
```
### DownscaleMode enum
```csharp
/// Downscaling/filtering methods for rendering output.
/// Downscale mode to apply when the output window is smaller than the internal render resolution.
public enum DownscaleMode
{
    /// Nearest-neighbor downscaling: very fast, but produces aliasing.
    Nearest,
    /// Bilinear filtering. Fast, may show moire on high-frequency content.
    Linear,
    /// 4-sample supersampling. Reduces aliasing and moire, low cost. Recommended default.
    Rgss,
    /// 16-sample supersampling. Better color accuracy than RGSS, higher cost.
    Pdss,
}
```
### Fog enum
```csharp
/// Fog effect modes.
/// Distance-based fog density distribution methods.
public enum Fog
{
    /// No fog effect
    Disabled,
    /// Linear density increase between start and end distances
    Linear,
    /// Exponential squared density (exp2), more realistic
    EXP2,
    /// Simple exponential density increase
    Exp,
}
```
### ImportFlags enum
```csharp
/// Flags controlling importer behavior.
/// These flags define how the importer processes the source asset.
[Flags]
public enum ImportFlags : uint
{
    /// When enabled, raw mesh data is preserved in RAM after model import.
    MeshData = (1<<0),
    /// When disabled, a faster import preset is used, suitable for runtime.
    Quality = (1<<1),
}
```
### InstanceFlags enum
```csharp
/// Bitmask defining which instance attributes are present.
[Flags]
public enum InstanceFlags : uint
{
    Position = (1<<0),
    Rotation = (1<<1),
    Scale = (1<<2),
    Color = (1<<3),
    Custom = (1<<4),
}
```
### Layer enum
```csharp
/// Bitfield type used to specify rendering layers for 3D objects.
/// This type is used by `R3D_Mesh` and `R3D_Sprite` objects to indicate which rendering layer(s) they belong to. Active layers are controlled globally via the functions:
///  - void R3D_EnableLayers(R3D_Layer bitfield);
///  - void R3D_DisableLayers(R3D_Layer bitfield);
/// A mesh or sprite will be rendered if at least one of its assigned layers is active.
/// For simplicity, 16 layers are defined in this header, but the maximum number of layers is 32 for an uint32_t.
[Flags]
public enum Layer : uint
{
    Layer01 = (1<<0),
    Layer02 = (1<<1),
    Layer03 = (1<<2),
    Layer04 = (1<<3),
    Layer05 = (1<<4),
    Layer06 = (1<<5),
    Layer07 = (1<<6),
    Layer08 = (1<<7),
    Layer09 = (1<<8),
    Layer10 = (1<<9),
    Layer11 = (1<<10),
    Layer12 = (1<<11),
    Layer13 = (1<<12),
    Layer14 = (1<<13),
    Layer15 = (1<<14),
    Layer16 = (1<<15),
    All = 0xFFFFFFFF,
}
```
### LightType enum
```csharp
/// Types of lights supported by the rendering engine.
/// Each light type has different behaviors and use cases.
public enum LightType
{
    /// Directional light, affects the entire scene with parallel rays.
    Dir,
    /// Spot light, emits light in a cone shape.
    Spot,
    /// Omni light, emits light in all directions from a single point.
    Omni,
    Count,
}
```
### MeshUsage enum
```csharp
/// Hint on how a mesh will be used.
public enum MeshUsage
{
    /// Will never be updated.
    StaticMesh,
    /// Will be updated occasionally.
    DynamicMesh,
    /// Will be update on each frame.
    StreamedMesh,
}
```
### OutputMode enum
```csharp
/// Defines the buffer to output (render texture or window).
public enum OutputMode
{
    Scene,
    Albedo,
    Normal,
    Orm,
    Diffuse,
    Specular,
    Ssao,
    Ssil,
    Ssgi,
    Ssr,
    Bloom,
    Dof,
}
```
### PrimitiveType enum
```csharp
/// Defines the geometric primitive type.
public enum PrimitiveType
{
    /// Each vertex represents a single point.
    Points,
    /// Each pair of vertices forms an independent line segment.
    Lines,
    /// Connected series of line segments sharing vertices.
    LineStrip,
    /// Closed loop of connected line segments.
    LineLoop,
    /// Each set of three vertices forms an independent triangle.
    Triangles,
    /// Connected strip of triangles sharing vertices.
    TriangleStrip,
    /// Fan of triangles sharing the first vertex.
    TriangleFan,
}
```
### ProbeFlags enum
```csharp
/// Bit-flags controlling what components are generated.
///  - R3D_PROBE_ILLUMINATION -> generate diffuse irradiance
///  - R3D_PROBE_REFLECTION   -> generate specular prefiltered map
[Flags]
public enum ProbeFlags : uint
{
    Illumination = (1<<0),
    Reflection = (1<<1),
}
```
### ProbeUpdateMode enum
```csharp
/// Modes for updating probes.
/// Controls how often probe captures are refreshed.
public enum ProbeUpdateMode
{
    /// Updated only when its state or content changes
    Once,
    /// Updated during every frames
    Always,
}
```
### ShadowCastMode enum
```csharp
/// Shadow casting modes for objects.
/// Controls how an object interacts with the shadow mapping system. These modes determine whether the object contributes to shadows, and if so, whether it is also rendered in the main pass.
public enum ShadowCastMode
{
    /// The object casts shadows; the faces used are determined by the material's culling mode.
    OnAuto,
    /// The object casts shadows with both front and back faces, ignoring face culling.
    OnDoubleSided,
    /// The object casts shadows with only front faces, culling back faces.
    OnFrontSide,
    /// The object casts shadows with only back faces, culling front faces.
    OnBackSide,
    /// The object only casts shadows; the faces used are determined by the material's culling mode.
    OnlyAuto,
    /// The object only casts shadows with both front and back faces, ignoring face culling.
    OnlyDoubleSided,
    /// The object only casts shadows with only front faces, culling back faces.
    OnlyFrontSide,
    /// The object only casts shadows with only back faces, culling front faces.
    OnlyBackSide,
    /// The object does not cast shadows at all.
    Disabled,
}
```
### ShadowUpdateMode enum
```csharp
/// Modes for updating shadow maps.
/// Determines how often the shadow maps are refreshed.
public enum ShadowUpdateMode
{
    /// Shadow maps update only when explicitly requested.
    Manual,
    /// Shadow maps update at defined time intervals.
    Interval,
    /// Shadow maps update every frame for real-time accuracy.
    Continuous,
}
```
### StencilOp enum
```csharp
/// Stencil buffer operations.
/// Defines how the stencil buffer value is modified based on test results.
public enum StencilOp
{
    /// Keep the current stencil value
    StencilKeep,
    /// Set stencil value to 0
    StencilZero,
    /// Replace with reference value
    StencilReplace,
    /// Increment stencil value (clamped)
    StencilIncr,
    /// Decrement stencil value (clamped)
    StencilDecr,
}
```
### StmEdgeMode enum
```csharp
/// Types of operation modes for state machine edge.
public enum StmEdgeMode
{
    /// Switch to next state instantly, with respecting cross fade time.
    Instant,
    /// Switch to next state when associated animation is done or looped with looper parameter set true.
    Ondone,
}
```
### StmEdgeStatus enum
```csharp
/// Types of travel status for state machine edge.
public enum StmEdgeStatus
{
    /// Edge is traversable by travel function.
    On,
    /// Edge is traversable automatically and by travel function.
    Auto,
    /// Edge is traversable automatically and by travel function, but only once; edge status changes to nextStatus when traversed.
    Once,
    /// Edge is not traversable.
    Off,
}
```
### Tonemap enum
```csharp
/// Tone mapping algorithms.
/// HDR to LDR color compression methods.
public enum Tonemap
{
    /// Direct linear mapping (no compression)
    Linear,
    /// Reinhard operator, balanced HDR compression
    Reinhard,
    /// Film-like response curve
    Filmic,
    /// Academy Color Encoding System (cinematic standard)
    Aces,
    /// Modern algorithm preserving highlights and shadows
    Agx,
    /// Internal: number of tonemap modes
    Count,
}
```
### TransparencyMode enum
```csharp
/// Transparency modes.
/// This enumeration defines how a material handles transparency during rendering. It controls whether transparency is disabled, rendered using a depth pre-pass, or rendered with standard alpha blending.
public enum TransparencyMode
{
    /// No transparency, supports alpha cutoff.
    Disabled,
    /// Supports transparency with shadows. Writes shadows for alpha > 0.1 and depth for alpha > 0.99.
    Prepass,
    /// Standard transparency without shadows or depth writes.
    Alpha,
}
```
### UpscaleMode enum
```csharp
/// Upscaling/filtering methods for rendering output.
/// Upscale mode to apply when the output window is larger than the internal render resolution.
public enum UpscaleMode
{
    /// Nearest-neighbor upscaling: very fast, but produces blocky pixels.
    Nearest,
    /// Bilinear upscaling: very fast, smoother than nearest, but can appear blurry.
    Linear,
    /// Bicubic upscaling: slower, smoother, and less blurry than linear.
    Bicubic,
    /// Lanczos-2 upscaling: preserves more fine details, but is the most expensive.
    Lanczos,
}
```
## Struct / delegate types
Below is a list of all the types in the library.
### Add2NodeParams   struct
```csharp
/// Parameters for animation tree node Add2.
/// Add2 node adds channels of any two animation nodes together, with respecting optional bone mask.
public unsafe struct Add2NodeParams
{
    /// Pointer to bone mask structure, can be NULL; calculated by R3D_ComputeBoneMask().
    internal BoneMask* _boneMask;
    /// Add weight value, can be in interval from 0.0 to 1.0.
    public float Weight;
}
```
### AlbedoMap   struct
```csharp
/// Albedo (base color) map.
/// Provides the base color texture and a color multiplier.
public struct AlbedoMap
{
    /// Base color texture (default: WHITE)
    public Texture2D Texture;
    /// Color multiplier (default: WHITE)
    public Color Color;
}
```
### AmbientMap   struct
```csharp
/// Global environment lighting data.
/// An ambient map is built from a cubemap (like a skybox) and preprocessed into two specialized textures:
///  - irradiance: Low-frequency lighting used for diffuse IBL. Captures soft ambient light from all directions.
///  - prefilter: Mipmapped environment used for specular reflections. Higher mip levels simulate rougher surfaces.
/// Both textures are derived from the same source cubemap, but serve different shading purposes.
public struct AmbientMap
{
    /// Components generated for this map
    public AmbientFlags Flags;
    /// Diffuse IBL cubemap (may be 0 if not generated)
    public uint Irradiance;
    /// Specular prefiltered cubemap (may be 0 if not generated)
    public uint Prefilter;
}
```
### Animation   struct
```csharp
/// Represents a skeletal animation for a model.
/// Contains all animation channels required to animate a skeleton. Each channel corresponds to one bone and defines its transformation (translation, rotation, scale) over time.
public unsafe struct Animation
{
    /// Array of animation channels, one per animated bone.
    internal AnimationChannel* _channels;
    /// Total number of channels in this animation.
    internal int ChannelCount;
    /// Playback rate; number of animation ticks per second.
    public float TicksPerSecond;
    /// Total length of the animation, in ticks.
    public float Duration;
    /// Number of bones in the target skeleton.
    public int BoneCount;
    /// Animation name (null-terminated string).
    internal fixed byte _name[32];
    /// <inheritdoc cref="_name"/>
    public string Name
    {
        get
        {
            fixed (byte* ptr = _name)
            {
                int len = 0;
                while (len < 32 && ptr[len] != 0) len++;
                return Encoding.UTF8.GetString(ptr, len);
            }
        }
        set
        {
            byte[] utf8 = Encoding.UTF8.GetBytes(value);
            int len = Math.Min(utf8.Length, 31);
            for (int i = 0; i < len; i++) _name[i] = utf8[i];
            _name[len] = 0;
        }
    }
    /// <see cref="Channels"/> as a <see cref="Span{T}"/>.
    public Span<AnimationChannel> Channels => _channels != null ? new(_channels, ChannelCount) : default;
}
```
### AnimationChannel   struct
```csharp
/// Animation channel controlling a single bone.
/// Contains animation tracks for translation, rotation and scale. The sampled tracks are combined to produce the bone local transform.
public struct AnimationChannel
{
    /// Translation track (Vector3).
    public AnimationTrack Translation;
    /// Rotation track (Quaternion).
    public AnimationTrack Rotation;
    /// Scale track (Vector3).
    public AnimationTrack Scale;
    /// Index of the affected bone.
    public int BoneIndex;
}
```
### AnimationEventCallback   delegate
```csharp
/// Callback type for receiving animation events.
/// <param name="player">Pointer to the animation player emitting the event.</param>
/// <param name="eventType">Type of the event (finished, looped).</param>
/// <param name="animIndex">Index of the animation triggering the event.</param>
/// <param name="userData">Optional user-defined data passed when the callback was registered.</param>
public unsafe delegate void AnimationEventCallback(AnimationPlayer* player, AnimationEvent eventType, int animIndex, IntPtr userData);
```
### AnimationLib   struct
```csharp
/// Represents a collection of skeletal animations sharing the same skeleton.
/// Holds multiple animations that can be applied to compatible models or skeletons. Typically loaded together from a single 3D model file (e.g., GLTF, FBX) containing several animation clips.
public unsafe struct AnimationLib
{
    /// Array of animations included in this library.
    internal Animation* _animations;
    /// Number of animations contained in the library.
    internal int Count;
    /// <see cref="Animations"/> as a <see cref="Span{T}"/>.
    public Span<Animation> Animations => _animations != null ? new(_animations, Count) : default;
}
```
### AnimationNodeCallback   delegate
```csharp
/// Callback type for manipulating the animation before it is used by the animation tree.
/// <param name="animation">Pointer to the processed animation.</param>
/// <param name="state">Current animation state.</param>
/// <param name="boneIndex">Index of the processed bone.</param>
/// <param name="out">Transformation of the processed bone.</param>
/// <param name="userData">Optional user-defined data passed when the callback was registered.</param>
public unsafe delegate void AnimationNodeCallback(Animation* animation, AnimationState state, int boneIndex, Transform* @out, IntPtr userData);
```
### AnimationNodeParams   struct
```csharp
/// Parameters for animation tree node Animation.
/// Animation is a leaf node, holding the R3D_Animation structure.
public unsafe struct AnimationNodeParams
{
    /// Animation name (null-terminated string).
    internal fixed byte _name[32];
    /// <inheritdoc cref="_name"/>
    public string Name
    {
        get
        {
            fixed (byte* ptr = _name)
            {
                int len = 0;
                while (len < 32 && ptr[len] != 0) len++;
                return Encoding.UTF8.GetString(ptr, len);
            }
        }
        set
        {
            byte[] utf8 = Encoding.UTF8.GetBytes(value);
            int len = Math.Min(utf8.Length, 31);
            for (int i = 0; i < len; i++) _name[i] = utf8[i];
            _name[len] = 0;
        }
    }
    /// Animation state.
    public AnimationState State;
    /// Flag to control whether the animation is considered done when looped; yes when true.
    public bool Looper;
    /// Callback function to receive and modify animation transformation before been used.
    public IntPtr EvalCallback;
    /// Optional user data pointer passed to the callback.
    public IntPtr EvalUserData;
}
```
### AnimationPlayer   struct
```csharp
/// Manages playback of multiple animations for a skeleton.
/// The animation player updates animation states, interpolates keyframes, blends animations according to their weights, and stores the resulting local and global bone transforms. It also supports GPU skinning by uploading the global pose into a texture.
public unsafe struct AnimationPlayer
{
    /// Animation library providing the available animations.
    public AnimationLib AnimLib;
    /// Skeleton to animate.
    public Skeleton Skeleton;
    /// Array of animation states, one per animation.
    internal AnimationState* _states;
    /// Index of the current animation.
    public int ActiveAnimIndex;
    /// Array of bone transforms representing the blended local pose.
    internal Matrix4x4* _localPose;
    /// Array of bone transforms in model space, obtained by hierarchical accumulation.
    internal Matrix4x4* _modelPose;
    /// Array of final skinning matrices (invBind * modelPose), sent to the GPU.
    internal Matrix4x4* _skinBuffer;
    /// GPU texture ID storing the skinning matrices as a 1D RGBA16F texture.
    public uint SkinTexture;
    /// Callback function to receive animation events.
    public IntPtr EventCallback;
    /// Optional user data pointer passed to the callback.
    public IntPtr EventUserData;
    /// <see cref="States"/> as a <see cref="Span{T}"/>.
    public Span<AnimationState> States => _states != null ? new(_states, AnimLib.Count) : default;
    /// <see cref="LocalPose"/> as a <see cref="Span{T}"/>.
    public Span<Matrix4x4> LocalPose => _localPose != null ? new(_localPose, Skeleton.BoneCount) : default;
    /// <see cref="ModelPose"/> as a <see cref="Span{T}"/>.
    public Span<Matrix4x4> ModelPose => _modelPose != null ? new(_modelPose, Skeleton.BoneCount) : default;
    /// <see cref="SkinBuffer"/> as a <see cref="Span{T}"/>.
    public Span<Matrix4x4> SkinBuffer => _skinBuffer != null ? new(_skinBuffer, Skeleton.BoneCount) : default;
}
```
### AnimationState   struct
```csharp
/// Describes the playback state of a single animation within a player.
/// Tracks the current time, speed, play/pause state, and looping behavior.
public struct AnimationState
{
    /// Current playback time in animation ticks.
    public float CurrentTime;
    /// Playback speed; can be negative for reverse playback.
    public float Speed;
    /// Whether the animation is currently playing.
    public bool Play;
    /// True to enable looping playback.
    public bool Loop;
}
```
### AnimationStmIndex   struct
```csharp
public struct AnimationStmIndex
{
    internal int id;
    public static explicit operator int(AnimationStmIndex handle) => handle.id;
    public static explicit operator AnimationStmIndex(int id) => new() { id = id };
}
```
### AnimationTrack   struct
```csharp
/// Animation track storing keyframe times and values.
/// Represents a single animated property (translation, rotation or scale). Keys are sampled by time and interpolated at runtime.
public unsafe struct AnimationTrack
{
    /// Keyframe times (sorted, in animation ticks).
    internal float* _times;
    /// Keyframe values (Vector3 or Quaternion).
    public void* Values;
    /// Number of keyframes.
    internal int Count;
    /// <see cref="Times"/> as a <see cref="Span{T}"/>.
    public Span<float> Times => _times != null ? new(_times, Count) : default;
    /// <see cref="Values"/> cast to the specified type as a <see cref="Span{T}"/>.
    public Span<T> ValuesAs<T>() where T : unmanaged => Values != null ? new((T*)Values, Count) : default;
}
```
### AnimationTree   struct
```csharp
/// Manages a tree structure of animation nodes.
/// Animation tree allows to define complex logic for switching and blending animations of associated animation player. It supports 5 different animation node types: Animation, Blend2, Add2, Switch and State Machine. It also fully supports root motion and bone masking in Blend2/Add2.
public struct AnimationTree
{
    /// Animation player and skeleton used by all animation nodes.
    public AnimationPlayer Player;
    /// Pointer to root animation node of the tree.
    public AnimationTreeNode RootNode;
    /// Animation node pool of size nodePoolMaxSize.
    public AnimationTreeNode NodePool;
    /// Current animation node pool size.
    public int NodePoolSize;
    /// Maximum number of animation nodes, defined during load.
    public int NodePoolMaxSize;
    /// Optional root bone index, -1 if not defined.
    public int RootBone;
    /// Callback function to receive and modify final animation transformation.
    public IntPtr UpdateCallback;
    /// Optional user data pointer passed to the callback.
    public IntPtr UpdateUserData;
}
```
### AnimationTreeCallback   delegate
```csharp
/// Callback type for manipulating the final animation.
/// <param name="player">Pointer to the animation player used by the animation tree.</param>
/// <param name="boneIndex">Index of the processed bone.</param>
/// <param name="out">Transformation of the processed bone.</param>
/// <param name="userData">Optional user-defined data passed when the callback was registered.</param>
public unsafe delegate void AnimationTreeCallback(AnimationPlayer* player, int boneIndex, Transform* @out, IntPtr userData);
```
### AnimationTreeNode   struct
```csharp
public struct AnimationTreeNode
{
    private nint _handle;
}
```
### Blend2NodeParams   struct
```csharp
/// Parameters for animation tree node Blend2.
/// Blend2 node blends channels of any two animation nodes together, with respecting optional bone mask.
public unsafe struct Blend2NodeParams
{
    /// Pointer to bone mask structure, can be NULL; calculated by R3D_ComputeBoneMask().
    internal BoneMask* _boneMask;
    /// Blend weight value, can be in interval from 0.0 to 1.0.
    public float Blend;
}
```
### BoneInfo   struct
```csharp
/// Stores bone information for skeletal animation.
/// Contains the bone name and the index of its parent bone.
public unsafe struct BoneInfo
{
    /// Bone name (max 31 characters + null terminator).
    internal fixed byte _name[32];
    /// <inheritdoc cref="_name"/>
    public string Name
    {
        get
        {
            fixed (byte* ptr = _name)
            {
                int len = 0;
                while (len < 32 && ptr[len] != 0) len++;
                return Encoding.UTF8.GetString(ptr, len);
            }
        }
        set
        {
            byte[] utf8 = Encoding.UTF8.GetBytes(value);
            int len = Math.Min(utf8.Length, 31);
            for (int i = 0; i < len; i++) _name[i] = utf8[i];
            _name[len] = 0;
        }
    }
    /// Index of the parent bone (-1 if root).
    public int Parent;
}
```
### BoneMask   struct
```csharp
/// Bone mask for Blend2 and Add2 animation nodes.
/// Bone mask structure, can by created by R3D_ComputeBoneMask.
public unsafe struct BoneMask
{
    /// Bit mask buffer for maximum of 256 bones (32bits * 8).
    public fixed int Mask[8];
    /// Actual bones count.
    public int BoneCount;
}
```
### Capsule   struct
```csharp
/// Capsule shape defined by two endpoints and radius
public struct Capsule
{
    /// Start point of capsule axis
    public Vector3 Start;
    /// End point of capsule axis
    public Vector3 End;
    /// Capsule radius
    public float Radius;
}
```
### Cubemap   struct
```csharp
/// GPU cubemap texture.
/// Holds the OpenGL texture handle and its base resolution (per face).
public struct Cubemap
{
    public uint Texture;
    public uint Fbo;
    public int Size;
}
```
### Decal   struct
```csharp
/// Represents a decal and its properties.
/// This structure defines a decal that can be projected onto geometry that has already been rendered.
public struct Decal
{
    /// Albedo map (if the texture is undefined, implicitly treat `applyColor` as false, with alpha = 1.0)
    public AlbedoMap Albedo;
    /// Emission map
    public EmissionMap Emission;
    /// Normal map
    public NormalMap Normal;
    /// Occlusion-Roughness-Metalness map
    public OrmMap Orm;
    /// UV offset (default: {0.0f, 0.0f})
    public Vector2 UvOffset;
    /// UV scale (default: {1.0f, 1.0f})
    public Vector2 UvScale;
    /// Alpha cutoff threshold (default: 0.01f)
    public float AlphaCutoff;
    /// Maximum angle against the surface normal to draw decal. 0.0f disables threshold. (default: 0.0f)
    public float NormalThreshold;
    /// The width of fading along the normal threshold (default: 0.0f)
    public float FadeWidth;
    /// Indicates that the albedo color will not be rendered, only the alpha component of the albedo will be used as a mask. (default: true)
    public bool ApplyColor;
    /// Custom shader applied to the decal (default: NULL)
    public SurfaceShader Shader;
}
```
### DepthState   struct
```csharp
/// Depth buffer state configuration.
/// Controls how fragments interact with the depth buffer during rendering..
public struct DepthState
{
    /// Comparison function for depth test (default: LESS)
    public CompareMode Mode;
    /// Scales the maximum depth slope for polygon offset (default: 0.0f)
    public float OffsetFactor;
    /// Constant depth offset value (default: 0.0f)
    public float OffsetUnits;
    /// Near clipping plane for depth range mapping (default: 0.0f)
    public float RangeNear;
    /// Far clipping plane for depth range mapping (default: 1.0f)
    public float RangeFar;
}
```
### EmissionMap   struct
```csharp
/// Emission map.
/// Provides emission texture, color, and energy multiplier.
public struct EmissionMap
{
    /// Emission texture (default: WHITE)
    public Texture2D Texture;
    /// Emission color (default: WHITE)
    public Color Color;
    /// Emission strength (default: 0.0f)
    public float Energy;
}
```
### EnvAmbient   struct
```csharp
/// Ambient lighting configuration.
public struct EnvAmbient
{
    /// Ambient light color when there is no ambient map
    public Color Color;
    /// Energy multiplier for ambient light (map or color)
    public float Energy;
    /// IBL environment map, can be generated from skybox
    public AmbientMap Map;
}
```
### EnvBackground   struct
```csharp
/// Background and skybox configuration.
public struct EnvBackground
{
    /// Background color when there is no skybox
    public Color Color;
    /// Energy multiplier applied to background (skybox or color)
    public float Energy;
    /// Sky blur factor [0,1], based on mipmaps, very fast
    public float SkyBlur;
    /// Skybox asset (used if ID is non-zero)
    public Cubemap Sky;
    /// Skybox rotation (pitch, yaw, roll as quaternion)
    public Quaternion Rotation;
}
```
### EnvBloom   struct
```csharp
/// Bloom post-processing settings.
/// Glow effect around bright areas in the scene.
public struct EnvBloom
{
    /// Bloom blending mode (default: R3D_BLOOM_DISABLED)
    public Bloom Mode;
    /// Mipmap spread factor [0-1]: higher = wider glow (default: 0.5)
    public float Levels;
    /// Bloom strength multiplier (default: 0.05)
    public float Intensity;
    /// Minimum brightness to trigger bloom (default: 0.0)
    public float Threshold;
    /// Softness of brightness cutoff transition (default: 0.5)
    public float SoftThreshold;
    /// Blur filter radius during upscaling (default: 1.0)
    public float FilterRadius;
}
```
### EnvColor   struct
```csharp
/// Color grading adjustments.
/// Final color correction applied after all other effects.
public struct EnvColor
{
    /// Overall brightness multiplier (default: 1.0)
    public float Brightness;
    /// Contrast between dark and bright areas (default: 1.0)
    public float Contrast;
    /// Color intensity (default: 1.0)
    public float Saturation;
}
```
### EnvDoF   struct
```csharp
/// Depth of Field (DoF) camera focus settings.
/// Blurs objects outside the focal plane.
public struct EnvDoF
{
    /// Enable/disable state (default: R3D_DOF_DISABLED)
    public DoF Mode;
    /// Focus distance in meters from camera (default: 10.0)
    public float FocusPoint;
    /// Depth of field depth: lower = shallower (default: 1.0)
    public float FocusScale;
    /// Near blur intensity: 0.0 = disabled, 1.0 = symmetric to far (default: 1.0)
    public float NearScale;
    /// Maximum blur radius, similar to aperture (default: 20.0)
    public float MaxBlurSize;
}
```
### EnvFog   struct
```csharp
/// Fog atmospheric effect settings.
public struct EnvFog
{
    /// Fog distribution mode (default: R3D_FOG_DISABLED)
    public Fog Mode;
    /// Fog tint color (default: white)
    public Color Color;
    /// Linear mode: distance where fog begins (default: 1.0)
    public float Start;
    /// Linear mode: distance of full fog density (default: 50.0)
    public float End;
    /// Exponential modes: fog thickness factor (default: 0.05)
    public float Density;
    /// Fog influence on skybox [0-1] (default: 0.5)
    public float SkyAffect;
}
```
### Environment   struct
```csharp
/// Complete environment configuration structure.
/// Contains all rendering environment parameters: background, lighting, and post-processing effects. Initialize with R3D_ENVIRONMENT_BASE for default values.
public struct Environment
{
    /// Background and skybox settings
    public EnvBackground Background;
    /// Ambient lighting configuration
    public EnvAmbient Ambient;
    /// Screen space ambient occlusion
    public EnvSSAO Ssao;
    /// Screen space indirect lighting
    public EnvSSIL Ssil;
    /// Screen space global illumination
    public EnvSSGI Ssgi;
    /// Screen space reflections
    public EnvSSR Ssr;
    /// Bloom glow effect
    public EnvBloom Bloom;
    /// Atmospheric fog
    public EnvFog Fog;
    /// Depth of field focus effect
    public EnvDoF Dof;
    /// HDR tone mapping
    public EnvTonemap Tonemap;
    /// Color grading adjustments
    public EnvColor Color;
}
```
### EnvSSAO   struct
```csharp
/// Screen Space Ambient Occlusion (SSAO) settings.
/// Darkens areas where surfaces are close together, such as corners and crevices.
public struct EnvSSAO
{
    /// Number of samples to compute SSAO (default: 16)
    public int SampleCount;
    /// Base occlusion strength multiplier (default: 1.0)
    public float Intensity;
    /// Exponential falloff for sharper darkening (default: 1.5)
    public float Power;
    /// Sampling radius in world space (default: 0.25)
    public float Radius;
    /// Depth bias to prevent self-shadowing, good value is ~2% of the radius (default: 0.007)
    public float Bias;
    /// Enable/disable SSAO effect (default: false)
    public bool Enabled;
}
```
### EnvSSGI   struct
```csharp
/// Screen Space Global Illumination (SSGI) settings.
/// Real-time global illlumination calculated in screen space.
public struct EnvSSGI
{
    /// Number of rays per pixel (default: 2)
    public int SampleCount;
    /// Maximum ray marching steps (default: 32)
    public int MaxRaySteps;
    /// Ray step size (default: 0.125)
    public float StepSize;
    /// Depth tolerance for valid hits (default: 1.0)
    public float Thickness;
    /// Maximum ray distance (default: 4.0)
    public float MaxDistance;
    /// GI intensity multiplier (default: 3.0)
    public float Intensity;
    /// Distance at which the GI fade begins (default: 8.0)
    public float FadeStart;
    /// Distance at which GI is fully faded (default: 16.0)
    public float FadeEnd;
    /// Number of denoiser iterations (default: 5)
    public int DenoiseSteps;
    /// Enable/disable SSGI (default: false)
    public bool Enabled;
}
```
### EnvSSIL   struct
```csharp
/// Screen Space Indirect Lighting (SSIL) settings.
/// Approximates indirect lighting by gathering light from nearby visible surfaces in screen space.
/// With a small radius, SSIL behaves like an extension of SSAO, producing a very subtle local blending of light and surface hues. With a larger radius, it becomes a better complement to SSGI, reinforcing indirect lighting over a wider area.
public struct EnvSSIL
{
    /// Number of samples to compute indirect lighting (default: 2)
    public int SampleCount;
    /// Number of depth slices for accumulation (default: 4)
    public int SliceCount;
    /// Maximum distance to gather light from (default: 2.0)
    public float Radius;
    /// Thickness threshold for occluders (default: 1.0)
    public float Thickness;
    /// IL intensity multiplier (default: 1.0)
    public float Intensity;
    /// AO exponent/power (default: 1.0)
    public float AoPower;
    /// Number of denoiser iterations (default: 4)
    public int DenoiseSteps;
    /// Enable/disable SSIL effect (default: false)
    public bool Enabled;
}
```
### EnvSSR   struct
```csharp
/// Screen Space Reflections (SSR) settings.
/// Real-time reflections calculated in screen space.
public struct EnvSSR
{
    /// Maximum ray marching steps (default: 32)
    public int MaxRaySteps;
    /// Binary search refinement steps (default: 4)
    public int BinarySteps;
    /// Ray step size (default: 0.125)
    public float StepSize;
    /// Depth tolerance for valid hits (default: 0.2)
    public float Thickness;
    /// Maximum ray distance (default: 4.0)
    public float MaxDistance;
    /// Screen edge fade start [0,1] (default: 0.25)
    public float EdgeFade;
    /// Enable/disable SSR (default: false)
    public bool Enabled;
}
```
### EnvTonemap   struct
```csharp
/// Tone mapping and exposure settings.
/// Converts HDR colors to displayable LDR range.
public struct EnvTonemap
{
    /// Tone mapping algorithm (default: R3D_TONEMAP_LINEAR)
    public Tonemap Mode;
    /// Scene brightness multiplier (default: 1.0)
    public float Exposure;
    /// Reference white point (not used for AGX) (default: 1.0)
    public float White;
}
```
### Importer   struct
```csharp
/// Opaque importer handle.
/// Represents a loaded asset file that can be used to extract multiple resources (models, skeletons, animations) without re-importing the file.
public struct Importer
{
    private nint _handle;
}
```
### InstanceBuffer   struct
```csharp
/// GPU buffers storing instance attribute streams.
/// buffers: One VBO per attribute (indexed by flag order). capcity: Maximum number of instances. flags: Enabled attribute mask.
public unsafe struct InstanceBuffer
{
    public fixed uint Buffers[5];
    public InstanceFlags Flags;
    public int Capacity;
}
```
### Light   struct
```csharp
/// Unique identifier for an R3D light.
/// ID type used to reference a light. A negative value indicates an invalid light.
public struct Light
{
    internal int id;
    public static explicit operator int(Light handle) => handle.id;
    public static explicit operator Light(int id) => new() { id = id };
}
```
### Material   struct
```csharp
/// Material definition.
/// Combines multiple texture maps and rendering parameters for shading.
public struct Material
{
    /// Albedo map
    public AlbedoMap Albedo;
    /// Emission map
    public EmissionMap Emission;
    /// Normal map
    public NormalMap Normal;
    /// Occlusion-Roughness-Metalness map
    public OrmMap Orm;
    /// UV offset (default: {0.0f, 0.0f})
    public Vector2 UvOffset;
    /// UV scale (default: {1.0f, 1.0f})
    public Vector2 UvScale;
    /// Alpha cutoff threshold (default: 0.01f)
    public float AlphaCutoff;
    /// Depth test configuration (default: standard)
    public DepthState Depth;
    /// Stencil test configuration (default: disabled)
    public StencilState Stencil;
    /// Transparency mode (default: DISABLED)
    public TransparencyMode TransparencyMode;
    /// Billboard mode (default: DISABLED)
    public BillboardMode BillboardMode;
    /// Blend mode (default: MIX)
    public BlendMode BlendMode;
    /// Face culling mode (default: BACK)
    public CullMode CullMode;
    /// If true, material does not participate in lighting (default: false)
    public bool Unlit;
    /// Custom shader applied to the material (default: NULL)
    public SurfaceShader Shader;
}
```
### Mesh   struct
```csharp
/// Represents a 3D mesh.
/// Stores vertex and index data, shadow casting settings, bounding box, and layer information. Can represent a static or skinned mesh.
public struct Mesh
{
    /// OpenGL objects handles.
    public uint Vao;
    /// OpenGL objects handles.
    public uint Vbo;
    /// OpenGL objects handles.
    public uint Ebo;
    /// Number of vertices allocated in GPU buffers.
    public int VertexCapacity;
    /// Number of indices allocated in GPU buffers.
    public int IndexCapacity;
    /// Number of vertices currently in use.
    public int VertexCount;
    /// Number of indices currently in use.
    public int IndexCount;
    /// Shadow casting mode for the mesh.
    public ShadowCastMode ShadowCastMode;
    /// Type of primitive that constitutes the vertices.
    public PrimitiveType PrimitiveType;
    /// Hint about the usage of the mesh, retained in case of update if there is a reallocation.
    public MeshUsage Usage;
    /// Bitfield indicating the rendering layer(s) of this mesh.
    public Layer LayerMask;
    /// Axis-Aligned Bounding Box in local space.
    public BoundingBox Aabb;
}
```
### MeshData   struct
```csharp
/// Represents a mesh stored in CPU memory.
/// R3D_MeshData is the CPU-side container of a mesh. It stores vertex and index data, and provides utility functions to generate, transform, and process geometry before uploading it to the GPU as an R3D_Mesh.
/// Think of it as a toolbox for procedural or dynamic mesh generation on the CPU.
public unsafe struct MeshData
{
    /// Pointer to vertex data in CPU memory.
    internal Vertex* _vertices;
    /// Pointer to index data in CPU memory.
    internal uint* _indices;
    /// Capacity of vertices.
    internal int VertexCapacity;
    /// Capacity of indices.
    internal int IndexCapacity;
    /// Number of vertices.
    public int VertexCount;
    /// Number of indices.
    public int IndexCount;
    /// <see cref="Vertices"/> as a <see cref="Span{T}"/>.
    public Span<Vertex> Vertices => _vertices != null ? new(_vertices, VertexCapacity) : default;
    /// <see cref="Indices"/> as a <see cref="Span{T}"/>.
    public Span<uint> Indices => _indices != null ? new(_indices, IndexCapacity) : default;
}
```
### Model   struct
```csharp
/// Represents a complete 3D model with meshes and materials.
/// Contains multiple meshes and their associated materials, along with animation and bounding information.
public unsafe struct Model
{
    /// Array of meshes composing the model.
    internal Mesh* _meshes;
    /// Array of meshes data in RAM (optional, can be NULL).
    internal MeshData* _meshData;
    /// Array of materials used by the model.
    internal Material* _materials;
    /// Array of material indices, one per mesh.
    internal int* _meshMaterials;
    /// Number of meshes.
    internal int MeshCount;
    /// Number of materials.
    internal int MaterialCount;
    /// Axis-Aligned Bounding Box encompassing the whole model.
    public BoundingBox Aabb;
    /// Skeleton hierarchy and bind pose used for skinning (NULL if non-skinned).
    public Skeleton Skeleton;
    /// <see cref="Meshes"/> as a <see cref="Span{T}"/>.
    public Span<Mesh> Meshes => _meshes != null ? new(_meshes, MeshCount) : default;
    /// <see cref="MeshData"/> as a <see cref="Span{T}"/>.
    public Span<MeshData> MeshData => _meshData != null ? new(_meshData, MeshCount) : default;
    /// <see cref="Materials"/> as a <see cref="Span{T}"/>.
    public Span<Material> Materials => _materials != null ? new(_materials, MaterialCount) : default;
    /// <see cref="MeshMaterials"/> as a <see cref="Span{T}"/>.
    public Span<int> MeshMaterials => _meshMaterials != null ? new(_meshMaterials, MeshCount) : default;
}
```
### NormalMap   struct
```csharp
/// Normal map.
/// Provides normal map texture and scale factor.
public struct NormalMap
{
    /// Normal map texture (default: Front Facing)
    public Texture2D Texture;
    /// Normal scale (default: 1.0f)
    public float Scale;
}
```
### OrmMap   struct
```csharp
/// Combined Occlusion-Roughness-Metalness (ORM) map.
/// Provides texture and individual multipliers for occlusion, roughness, and metalness.
public struct OrmMap
{
    /// ORM texture (default: WHITE)
    public Texture2D Texture;
    /// Occlusion multiplier (default: 1.0f)
    public float Occlusion;
    /// Roughness multiplier (default: 1.0f)
    public float Roughness;
    /// Metalness multiplier (default: 0.0f)
    public float Metalness;
}
```
### Penetration   struct
```csharp
/// Penetration information from an overlap test
public struct Penetration
{
    /// Whether shapes are overlapping
    public bool Collides;
    /// Penetration depth
    public float Depth;
    /// Collision normal (direction to resolve penetration)
    public Vector3 Normal;
    /// Minimum Translation Vector (normal * depth)
    public Vector3 Mtv;
}
```
### Probe   struct
```csharp
/// Unique identifier for an R3D probe.
/// Negative values indicate an invalid probe.
public struct Probe
{
    internal int id;
    public static explicit operator int(Probe handle) => handle.id;
    public static explicit operator Probe(int id) => new() { id = id };
}
```
### ProceduralSky   struct
```csharp
/// Parameters for procedural sky generation.
/// Curves control gradient falloff (lower = sharper transition at horizon).
public struct ProceduralSky
{
    /// Sky color at zenith
    public Color SkyTopColor;
    /// Sky color at horizon
    public Color SkyHorizonColor;
    /// Gradient curve exponent (0.01 - 1.0, typical: 0.15)
    public float SkyHorizonCurve;
    /// Sky brightness multiplier
    public float SkyEnergy;
    /// Ground color at nadir
    public Color GroundBottomColor;
    /// Ground color at horizon
    public Color GroundHorizonColor;
    /// Gradient curve exponent (typical: 0.02)
    public float GroundHorizonCurve;
    /// Ground brightness multiplier
    public float GroundEnergy;
    /// Direction from which light comes (can take not normalized)
    public Vector3 SunDirection;
    /// Sun disk color
    public Color SunColor;
    /// Sun angular size in radians (real sun: ~0.0087 rad = 0.5°)
    public float SunSize;
    /// Sun edge softness exponent (typical: 0.15)
    public float SunCurve;
    /// Sun brightness multiplier
    public float SunEnergy;
}
```
### ScreenShader   struct
```csharp
public struct ScreenShader
{
    private nint _handle;
}
```
### ShaderCustom   struct
```csharp
public struct ShaderCustom
{
    private nint _handle;
}
```
### Skeleton   struct
```csharp
/// Represents a skeletal hierarchy used for skinning.
/// Defines the bone structure, reference poses, and inverse bind matrices required for skeletal animation. The skeleton provides both local and global bind poses used during skinning and animation playback.
public unsafe struct Skeleton
{
    /// Array of bone descriptors defining the hierarchy and names.
    internal BoneInfo* _bones;
    /// Total number of bones in the skeleton.
    internal int BoneCount;
    /// Bind pose matrices relative to parent
    internal Matrix4x4* _localBind;
    /// Bind pose matrices in model/global space
    internal Matrix4x4* _modelBind;
    /// Inverse bind matrices (model space) for skinning
    internal Matrix4x4* _invBind;
    /// Root correction if local bind is not identity
    public Matrix4x4 RootBind;
    /// Texture ID that contains the bind pose for GPU skinning. This is a 1D Texture RGBA16F 4*boneCount.
    public uint SkinTexture;
    /// <see cref="Bones"/> as a <see cref="Span{T}"/>.
    public Span<BoneInfo> Bones => _bones != null ? new(_bones, BoneCount) : default;
    /// <see cref="LocalBind"/> as a <see cref="Span{T}"/>.
    public Span<Matrix4x4> LocalBind => _localBind != null ? new(_localBind, BoneCount) : default;
    /// <see cref="ModelBind"/> as a <see cref="Span{T}"/>.
    public Span<Matrix4x4> ModelBind => _modelBind != null ? new(_modelBind, BoneCount) : default;
    /// <see cref="InvBind"/> as a <see cref="Span{T}"/>.
    public Span<Matrix4x4> InvBind => _invBind != null ? new(_invBind, BoneCount) : default;
}
```
### SkyShader   struct
```csharp
public struct SkyShader
{
    private nint _handle;
}
```
### StencilState   struct
```csharp
/// Stencil buffer state configuration.
/// Controls how fragments interact with the stencil buffer during rendering. The stencil buffer can be used for effects like x-ray vision, outlines, portals, and masking.
public struct StencilState
{
    /// Comparison function for stencil test (default: ALWAYS)
    public CompareMode Mode;
    /// Reference value (0-255) for comparison and replace operations (default: 0x00)
    public byte Ref;
    /// Bit mask applied to both reference and stencil values during comparison (default: 0xFF)
    public byte Mask;
    /// Operation when stencil test fails (default: KEEP)
    public StencilOp OpFail;
    /// Operation when stencil test passes but depth test fails (default: KEEP)
    public StencilOp OpZFail;
    /// Operation when both stencil and depth tests pass (default: REPLACE)
    public StencilOp OpPass;
}
```
### StmEdgeParams   struct
```csharp
/// Parameters for animation state machine edge.
public struct StmEdgeParams
{
    /// Operation mode.
    public StmEdgeMode Mode;
    /// Current travel status.
    public StmEdgeStatus Status;
    /// Travel status used after machine traversed through this edge with status set to R3D_STM_EDGE_ONCE.
    public StmEdgeStatus NextStatus;
    /// Cross fade blending time between connected animation nodes, in seconds.
    public float XFadeTime;
}
```
### SurfaceShader   struct
```csharp
public struct SurfaceShader
{
    private nint _handle;
}
```
### SweepCollision   struct
```csharp
/// Collision information from a sweep test
public struct SweepCollision
{
    /// Whether a collision occurred
    public bool Hit;
    /// Time of impact [0-1], fraction along velocity vector
    public float Time;
    /// World space collision point
    public Vector3 Point;
    /// Surface normal at collision point
    public Vector3 Normal;
}
```
### SwitchNodeParams   struct
```csharp
/// Parameters for animation tree node Switch.
/// Switch node allows instant or blended/faded transition between any animation nodes connected to inputs.
public struct SwitchNodeParams
{
    /// Flag to control input animation nodes synchronization; activated input is reset when false.
    public bool Synced;
    /// Active input zero-based index.
    public int ActiveInput;
    /// Animation nodes cross fade blending time, in seconds.
    public float XFadeTime;
}
```
### Vertex   struct
```csharp
/// Represents a vertex and all its attributes for a mesh.
public unsafe struct Vertex
{
    /// The 3D position of the vertex in object space.
    public Vector3 Position;
    /// The 2D texture coordinates (UV) for mapping textures.
    public Vector2 Texcoord;
    /// The normal vector used for lighting calculations.
    public Vector3 Normal;
    /// Vertex color, in RGBA32.
    public Color Color;
    /// The tangent vector, used in normal mapping (often with a handedness in w).
    public Vector4 Tangent;
    /// Indices of up to 4 bones that influence this vertex (for skinning).
    public fixed int BoneIds[4];
    /// Corresponding bone weights (should sum to 1.0). Defines the influence of each bone.
    public fixed float Weights[4];
}
```
