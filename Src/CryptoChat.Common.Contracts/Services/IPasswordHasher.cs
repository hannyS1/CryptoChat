﻿namespace CryptoChat.Common.Contracts.Services;

public interface IPasswordHasher
{
    public string Hash(string password);

    public bool Verify(string password, string hashedPassword);
}