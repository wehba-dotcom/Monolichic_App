﻿namespace Bornholm_Sleagts.Core.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }

        IRoleRepository Role { get; }
    }
}
