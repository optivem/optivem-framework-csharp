﻿namespace Optivem.Framework.Core.Common.Parsing
{
    public interface IParsingService
    {
        T Parse<T>(string value);
    }
}
