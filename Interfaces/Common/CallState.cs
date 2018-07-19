using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Interfaces
{
    public enum CallState
    { 
        /// <summary>
        /// Can't established connection to reciver
        /// </summary>
        Engaget,

        /// <summary>
        /// Connection established
        /// </summary>
        Connected, 

        /// <summary>
        /// Call is interrupted
        /// </summary>
        Disconnected, 

        /// <summary>
        /// Call is rejected due to debt
        /// </summary>
        Locked,

        /// <summary>
        /// Some error occured
        /// </summary>
        Error
    }
}
