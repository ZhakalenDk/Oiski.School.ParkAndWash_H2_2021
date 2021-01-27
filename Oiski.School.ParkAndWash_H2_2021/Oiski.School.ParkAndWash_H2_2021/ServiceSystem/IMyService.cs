﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    public interface IMyService<T> : IMyServiceBase, IMyServiceItemCollection<T>
    {
        T RequestServiceItem<ValueType> (ValueType _value);
        bool CancelServiceItem<IDType> (IDType _itemID);
        bool ValidateServiceitem<IDType> (IDType _itemID);
    }
}