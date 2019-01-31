﻿using Rage;

namespace DatasetGenerator.BoundingBoxes
{
    partial class BoundingBox
    {
        private const float WeaponScaleFactor = 1.1f;

        public static BoundingBox FromWeapon(Weapon weapon)
        {
            weapon.Model.GetDimensions(out var weaponBottomLeft, out var weaponTopRight);

            //Compute the size of the three sides of the box
            Vector3 size = (weaponTopRight - weaponBottomLeft)*WeaponScaleFactor;

            //Compute the offset of the weapon center relative to the origin of the model
            Vector3 centerOffset = (weaponTopRight + weaponBottomLeft) / 2.0f;

            //Now we must rotate the center offset computed on the model, to match the entity orientation
            var rotatedCenterOffset = centerOffset.Rotate(weapon.Orientation);

            var wireBoxCenter = weapon.Position + rotatedCenterOffset;
            return new BoundingBox(wireBoxCenter, size, weapon.Orientation, weapon);
        }
    }
}
