﻿namespace JendStore.Client.Service.IService
{
    public interface ITokenProvider
    {
        void ClearToken();
        string? GetToken();
        void SetToken(string token);
    }
}
