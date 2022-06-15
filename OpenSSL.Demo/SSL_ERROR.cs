using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSSL.Demo
{
    public enum SSL_ERROR
    {
        SSL_ERROR_NONE = 0,
        SSL_ERROR_SSL = 1,
        SSL_ERROR_WANT_READ = 2,
        SSL_ERROR_WANT_WRITE = 3,
        SSL_ERROR_WANT_X509_LOOKUP = 4,
        SSL_ERROR_SYSCALL = 5,
        SSL_ERROR_ZERO_RETURN = 6,
        SSL_ERROR_WANT_CONNECT = 7,
        SSL_ERROR_WANT_ACCEPT = 8,
        SSL_ERROR_WANT_ASYNC = 9,
        SSL_ERROR_WANT_ASYNC_JOB = 10,
        SSL_ERROR_WANT_CLIENT_HELLO_CB = 11,
        SSL_ERROR_WANT_RETRY_VERIFY = 12
    }
}
