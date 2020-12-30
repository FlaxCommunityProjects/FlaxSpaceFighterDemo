using FlaxEngine;

namespace Game
{
    public class LaserResizer : Script
    {
        public Actor Camera;
        private Vector3 InitialScale;

        public override void OnStart()
        {
            InitialScale = Actor.Scale;
        }

        public override void OnUpdate()
        {
            var distance = Vector3.Distance(Camera.Position, Actor.Position);
            distance *= 0.002f;
            Debug.Log(distance);
            var scale = Vector3.Max(distance * InitialScale, InitialScale);
            Actor.Scale = Vector3.Min(scale, InitialScale * new Vector3(100));
            Actor.Scale = new Vector3(Actor.Scale.X, InitialScale.Y, Actor.Scale.Z);
        }
    }
}