using MolluskRecognition.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolluskRecognition.Extensions
{
    /// <summary>
    /// Extension methods for sculpture type
    /// </summary>
    public static class SculptureTypeExtension
    {
        /// <summary>
        /// Get name of the scuplture type
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetName(this SculptureType input)
        {
            string result = string.Empty;
            switch (input)
            {
                case SculptureType.Concentric:
                    {
                        result = Properties.Resources.ConcentricSculptureType;
                        break;
                    }
                case SculptureType.Radial:
                    {
                        result = Properties.Resources.RadialSculptureType;
                        break;
                    }
                default:
                    {
                        result = Properties.Resources.RadialConcentricSculptureType;
                        break;
                    }
            }
            return result;
        }
    }
}
