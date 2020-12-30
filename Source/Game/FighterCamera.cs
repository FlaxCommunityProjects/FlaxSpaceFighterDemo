using System;
using System.Collections.Generic;
using FlaxEditor;
using FlaxEngine;

namespace Game
{
    public class FighterCamera : Script
    {
        public Actor Camera;
        public JsonAsset PMaterial;
        private Vector3 Velocity;

        public override void OnStart()
        {
            base.OnStart();
            var boxcollider = new BoxCollider();
            boxcollider.Parent = Actor;
            boxcollider.Material = PMaterial;
        }

        public override void OnUpdate()
        {
            
#if FLAX_EDITOR
            var mPos = Input.MouseScreenPosition;
            mPos = Editor.Instance.Windows.GameWin.PointFromWindow(mPos / Platform.DpiScale);
            var screenSize = Editor.Instance.Windows.GameWin.Size;
#else
            var mPos = Input.MousePosition;
            var screenSize = Screen.Size;
#endif
            
            mPos /= screenSize;
            mPos *= 2;
            mPos -= 1;
            Vector3 final = new Vector3();
            if (Vector2.Distance(mPos, new Vector2(0, 0)) > 0.10f)
            {
                final.Z = -mPos.Y; 
                final.Y = mPos.X;
                final *= 62.5f;
            }

            final.Z = Input.GetAxis("Vertical") != 0 ? -Input.GetAxis("Vertical") * 50 : final.Z;
            final.X = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Horizontal") != 0 ? Input.GetAxis("Horizontal") * 50 : final.X;

            int thrust = 2000;

            if (Input.GetAction("Forward"))
                thrust = 7000;
            Actor.As<RigidBody>().AddRelativeForce(Vector3.Right * thrust);
            Actor.As<RigidBody>().AddRelativeTorque(final);
        }
    }
}
