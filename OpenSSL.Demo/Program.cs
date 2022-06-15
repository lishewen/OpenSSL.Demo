// See https://aka.ms/new-console-template for more information
using OpenSSL.Demo;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

Console.WriteLine("Hello, World!");

#region OpenSsl native functions

const string SslDllName = "libssl-1_1-x64.dll";

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static IntPtr TLS_method();

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static IntPtr TLSv1_method();

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static IntPtr TLSv1_1_method();

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static IntPtr TLSv1_2_method();

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static IntPtr TLS_client_method();

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static IntPtr TLS_server_method();

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static IntPtr SSL_CTX_new(IntPtr method);

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static void SSL_CTX_free(IntPtr ctx);

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static IntPtr SSL_new(IntPtr ctx);

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static void SSL_free(IntPtr ssl);

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static int SSL_set_fd(IntPtr ssl, int fd);

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static long SSL_ctrl(IntPtr ssl, int cmd, long larg, IntPtr parg);

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static int SSL_connect(IntPtr ssl);

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static int SSL_read(IntPtr ssl, IntPtr buf, int num);

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static int SSL_write(IntPtr ssl, IntPtr buf, int num);

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static SSL_ERROR SSL_get_error(IntPtr ssl, int ret);

[DllImport(SslDllName, CallingConvention = CallingConvention.Cdecl)]
extern static int SSL_do_handshake(IntPtr ssl);
#endregion

#region OpenSsl constants

const int SSL_CTRL_SET_TLSEXT_HOSTNAME = 55;

#endregion

HandshakeState handShakeState = HandshakeState.None;

string host = "www.baidu.com";
int port = 443;
IPHostEntry gist = Dns.GetHostEntry(host);
//得到所访问的网址的IP地址
IPAddress ip = gist.AddressList[0];
IPEndPoint ipEnd = new(ip, port);
Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
// socket.Blocking = false; //非阻塞模式

byte[] bytes;

socket.Connect(ipEnd);

if (!socket.Connected)
{
    Console.WriteLine("Socket connection error");
    return;
}

Console.WriteLine("Socket connected\r\n");

/* CONNECT request through proxy */

bytes = Encoding.UTF8.GetBytes($"CONNECT {host}:{port} HTTP/1.1\r\n\r\n");

socket.Send(bytes, bytes.Length, 0);
socket.Receive(Array.Empty<byte>());

Console.WriteLine("Connect request:\r\n" + Encoding.UTF8.GetString(bytes));

bytes = new byte[socket.Available];

socket.Receive(bytes);

Console.WriteLine("Connect response:\r\n" + Encoding.UTF8.GetString(bytes));

/* OpenSsl usage */

IntPtr tlsMethod = TLS_client_method();
IntPtr sslCtx = SSL_CTX_new(tlsMethod);
IntPtr ssl = SSL_new(sslCtx);
int setFdResult = SSL_set_fd(ssl, socket.Handle.ToInt32());

if (setFdResult == 0)
{
    Console.WriteLine("Set socket handle error");
    return;
}

SSL_ctrl(ssl, SSL_CTRL_SET_TLSEXT_HOSTNAME, 0, Marshal.StringToBSTR(host));

int connectResult = SSL_connect(ssl);

switch (connectResult)
{
    case 0:
        Console.WriteLine("SSL connection error\r\n");
        return;
    case 1:
        Console.WriteLine("SSL connection success\r\n");
        break;
    default:
        Console.WriteLine("SSL connection fatal error\r\n");
        // 获取具体错误信息
        var ex = SSL_get_error(ssl, connectResult);
        Console.WriteLine(ex);
        return;
}

bytes = Encoding.UTF8.GetBytes($"GET / HTTP/1.1\r\nHost: {host}\r\n\r\n");
IntPtr bytesPointer = Marshal.AllocHGlobal(bytes.Length);

Console.WriteLine("Ssl request:\r\n" + Encoding.UTF8.GetString(bytes));
Marshal.Copy(bytes, 0, bytesPointer, bytes.Length);

SSL_write(ssl, bytesPointer, bytes.Length);

socket.Receive(Array.Empty<byte>());

bytes = new byte[socket.Available];
bytesPointer = Marshal.AllocHGlobal(bytes.Length);

SSL_read(ssl, bytesPointer, bytes.Length);
Marshal.Copy(bytesPointer, bytes, 0, bytes.Length);

Console.WriteLine("Ssl response:\r\n" + Encoding.UTF8.GetString(bytes));

SSL_free(ssl);
SSL_CTX_free(sslCtx);

Console.ReadKey();