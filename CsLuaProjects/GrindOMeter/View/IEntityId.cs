﻿namespace GrindOMeter.View
{
    using Model.Entity;

    public interface IEntityId
    {
        int Id { get; }
        EntityType Type { get; }
    }
}
