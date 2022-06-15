using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSSL.Demo
{
	/// <summary>
	/// Options enumeration for Options property
	/// </summary>
	[Flags]
	public enum SslOptions
	{
		SSL_OP_MICROSOFT_SESS_ID_BUG = 0x00000001,
		SSL_OP_NETSCAPE_CHALLENGE_BUG = 0x00000002,
		SSL_OP_NETSCAPE_REUSE_CIPHER_CHANGE_BUG = 0x00000008,
		SSL_OP_SSLREF2_REUSE_CERT_TYPE_BUG = 0x00000010,
		SSL_OP_MICROSOFT_BIG_SSLV3_BUFFER = 0x00000020,
		/// <summary>
		/// no effect since 0.9.7h and 0.9.8b
		/// </summary>
		SSL_OP_MSIE_SSLV2_RSA_PADDING = 0x00000040,
		SSL_OP_SSLEAY_080_CLIENT_DH_BUG = 0x00000080,
		SSL_OP_TLS_D5_BUG = 0x00000100,
		SSL_OP_TLS_BLOCK_PADDING_BUG = 0x00000200,

		/* Disable SSL 3.0/TLS 1.0 CBC vulnerability workaround that was added
		 * in OpenSSL 0.9.6d.  Usually (depending on the application protocol)
		 * the workaround is not needed.  Unfortunately some broken SSL/TLS
		 * implementations cannot handle it at all, which is why we include
		 * it in SSL_OP_ALL. */
		SSL_OP_DONT_INSERT_EMPTY_FRAGMENTS = 0x00000800, /* added in 0.9.6e */

		/* SSL_OP_ALL: various bug workarounds that should be rather harmless.
		 *             This used to be 0x000FFFFFL before 0.9.7. */
		SSL_OP_ALL = (0x00000FFF ^ SSL_OP_NETSCAPE_REUSE_CIPHER_CHANGE_BUG),

		/* As server, disallow session resumption on renegotiation */
		SSL_OP_NO_SESSION_RESUMPTION_ON_RENEGOTIATION = 0x00010000,
		/* If set, always create a new key when using tmp_dh parameters */
		SSL_OP_SINGLE_DH_USE = 0x00100000,
		/* Set to always use the tmp_rsa key when doing RSA operations,
		 * even when this violates protocol specs */
		SSL_OP_EPHEMERAL_RSA = 0x00200000,
		/* Set on servers to choose the cipher according to the server's
		 * preferences */
		SSL_OP_CIPHER_SERVER_PREFERENCE = 0x00400000,
		/* If set, a server will allow a client to issue a SSLv3.0 version number
		 * as latest version supported in the premaster secret, even when TLSv1.0
		 * (version 3.1) was announced in the client hello. Normally this is
		 * forbidden to prevent version rollback attacks. */
		SSL_OP_TLS_ROLLBACK_BUG = 0x00800000,

		SSL_OP_NO_SSLv2 = 0x01000000,
		SSL_OP_NO_SSLv3 = 0x02000000,
		SSL_OP_NO_TLSv1 = 0x04000000,

		/// <summary>
		/// The next flag deliberately changes the ciphertest, this is a check
		/// for the PKCS#1 attack
		/// </summary>
		SSL_OP_PKCS1_CHECK_1 = 0x08000000,
		SSL_OP_PKCS1_CHECK_2 = 0x10000000,
		SSL_OP_NETSCAPE_CA_DN_BUG = 0x20000000,
		SSL_OP_NETSCAPE_DEMO_CIPHER_CHANGE_BUG = 0x40000000,
	}
}
