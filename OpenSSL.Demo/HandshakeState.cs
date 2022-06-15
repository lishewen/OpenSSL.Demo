using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSSL.Demo
{
    public enum HandshakeState
    {
        None,
        Renegotiate,
        InProcess,
        RenegotiateInProcess,
        Complete
    }
}
