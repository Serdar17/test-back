﻿using Logic.Models;

namespace Logic.Service;

public interface ITokenService
{
    string BuildToken(string key, string issuer, User user);
    bool IsTokenValid(string key, string issuer, string token);
}