namespace GrindOMeter.Model.EntityStorage
{
    using System;

    public class EntityStorageException : Exception
    {
        public EntityStorageException(string msg) : base(msg) { }
    }
}