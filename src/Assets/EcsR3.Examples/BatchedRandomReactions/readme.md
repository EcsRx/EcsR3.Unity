# Batched Random Reactions Example

If you havent already look at the normal Random Reactions one, this is that but done in a more optimal way where we use batched systems to update everything at once.

If you run the two scenes you may not notice much difference other than there is now spheres instead of cubes, but if you look in the profiler you will see that the average 
user script time has gone down by about 2/3 and your FPS will have doubled.

If you don't believe me you can test it, disable the "Entities" game object and watch the FPS/Profiler without anything to render and compare that between the two scenes.

Batches systems are the fastest out the box systems available, but they are only worth using if:
- The entities matching the group do not change frequently
- Entities are not being added/removed from the group constantly

This is because it will rebuild the batches any time the `ObservableGroup` changes, so the less the group changes, the better the performance, also you can make use of
`EntityPool` in the newer EcsR3 versions to allow you to re-use entities rather than removing them and re-creating them which also makes batches lifetime more stable.

It shows the basics of:

- Using Batched Systems