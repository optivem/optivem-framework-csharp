﻿namespace Optivem.Infrastructure.System
{
    public class StringParser : BaseParser<string>
    {
        protected override string ParseInner(string value)
        {
            return value;
        }
    }
}