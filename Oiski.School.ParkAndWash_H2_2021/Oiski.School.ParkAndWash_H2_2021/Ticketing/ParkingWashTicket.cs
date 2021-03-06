﻿using Oiski.School.ParkAndWash_H2_2021.Washing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// Defines an <see cref="IMyTicket"/> for an <see cref="Parking.IMyParkingSpot"/> wthat includes a car wash
    /// </summary>
    internal class ParkingWashTicket : ParkingTicket
    {
        /// <summary>
        /// Creates a new instance of type <see cref="ParkingWashTicket"/> where the <paramref name="_parkingSpotID"/>, <paramref name="_pricePrHour"/> and <paramref name="_chargeCostPrKWH"/> is set
        /// </summary>
        /// <param name="_parkingSpotID"></param>
        /// <param name="_pricePrHour"></param>
        /// <param name="_chargeCostPrKWH"></param>
        public ParkingWashTicket ( int _parkingSpotID, decimal _pricePrHour, CarWashType _washType ) : base (_parkingSpotID, _pricePrHour)
        {
            WashType = _washType;
            TicketType = typeof (ParkingWashTicket);
        }

        public CarWashType WashType { get; set; }

        /// <summary>
        /// Save the current state of the <see cref="ParkingWashTicket"/>
        /// </summary>
        /// <returns>The current state of <see langword="this"/> <see cref="IMyRepositoryEntity{IDType, SaveType}"/> <see langword="object"/> as an instance of type <typeparamref name="SaveType"/></returns>
        public override string SaveEntity ()
        {
            return $"{base.SaveEntity ()},{( int ) WashType}";
        }

        /// <summary>
        /// Restore a previous state of the <see cref="ParkingWashTicket"/> based on the passed in <typeparamref name="SaveType"/> <see langword="value"/>
        /// </summary>
        /// <param name="_data"></param>
        public override void BuildEntity ( string _values )
        {
            base.BuildEntity (_values);

            string[] values = _values.Split (",");

            int.TryParse (values[ values.Length - 1 ], out int _type);
            WashType = ( CarWashType ) _type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>An <see cref="Array"/> of <see cref="KeyValuePair{TKey, TValue}"/> <see langword="objects"/> where <strong>key</strong> is the property name in <i>PascalCase</i></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArrayTypeMismatchException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="RankException"></exception>
        public override KeyValuePair<string, object>[] GetProperties ()
        {
            int basePropertyCount = base.GetProperties ().Length;

            KeyValuePair<string, object>[] properties = new KeyValuePair<string, object>[ basePropertyCount + 1 ];

            base.GetProperties ().CopyTo (properties, 0);
            properties[ basePropertyCount - 1 ] = KeyValuePair.Create ("ChargeCostPrKWH", ( object ) ParkingSpotID);

            return properties;
        }

        /// <summary>
        /// Set the <see langword="value"/> of a property
        /// </summary>
        /// <param name="_propertyName">The property name in <i>PascalCase</i></param>
        /// <param name="_value">The <see langword="value"/> to assign the property</param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="PropertyNotFoundException{T}"></exception>
        public override void SetProperty ( string _propertyName, object _value )
        {
            object property = null;

            try
            {
                base.SetProperty (_propertyName, _value);
            }
            catch ( PropertyNotFoundException<IMyParkingTicket> _propertyException )
            {
                try
                {
                    switch ( _propertyName )
                    {
                        case "WashType":
                            property = WashType;
                            WashType = ( CarWashType ) _value;
                            break;
                        default:
                            throw _propertyException;
                    }
                }
                catch ( InvalidCastException _invalidException )
                {
                    throw new InvalidCastException ($"Invalid Property Value: type of ({property})<{property.GetType ()}> is not equal to type of ({_value})<{_value.GetType ()}>", _invalidException);
                }
            }
        }
    }
}
