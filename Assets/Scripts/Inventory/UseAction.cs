using System;

namespace ItemRelated
{
    [Serializable]
    public struct UseAction
    {
        public UseMode UseMode;
        public ItemActivator TargetActivator;
    }
}