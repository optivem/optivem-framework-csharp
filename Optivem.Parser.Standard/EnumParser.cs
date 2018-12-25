﻿using Optivem.Parser.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Optivem.Parser.Standard
{
    // TODO: VC: Base enum constraint

    public class EnumParser<T> : BaseParser<T?> where T : struct
    {
        protected override T? ParseInner(string value)
        {
            var type = typeof(T);

            return (T)Enum.Parse(type, value);
        }
    }
}
